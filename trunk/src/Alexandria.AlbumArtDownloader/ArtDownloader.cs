using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Alexandria.AlbumArtDownloader
{
	public class ArtDownloader
	{
		#region Constructors
		public ArtDownloader(MainForm form)
		{
			threads = new System.Collections.Generic.List<TaskThread>();
			mainForm = form;
			e_abort = new ManualResetEvent(false);
			tasks = new List<Task>();
			scripts = new List<Script>();
			otherthreads = new List<Thread>();
		}
		#endregion
		
		#region Private Fields
		private Task selectedtask;
		private MainForm mainForm;
		private ManualResetEvent e_abort;
		private List<Task> tasks;
		private event TaskEventHandler TaskAdded;
		private event TaskEventHandler TaskRemoved;
		private event ThreadEventHandler ThreadCountChanged;
		private List<TaskThread> threads;
		private List<Script> scripts;
		private List<Thread> otherthreads;
		#endregion

		#region Private Methods
		
		#region DoSaveArt
		private void DoSaveArt(object o)
		{
			FileStream f = null;
			try
			{
				SaveData s = (SaveData)o;
				ThumbRes thumb;
				thumb = s.indexn;
				lock (thumb)
				{
					PopupateFullSizeStream(thumb);
					if (s.filetargetn != null)
						f = File.Create(s.filetargetn);
					Image i = ProcessStream(ref f, s, thumb);
					if (i != null)
					{
						mainForm.CoolBeginInvoke(mainForm.previewgot, i, thumb);
						mainForm.CoolBeginInvoke(mainForm.statupdate, "Complete", null, true, Thread.CurrentThread);
					}
					else
					{
						mainForm.CoolBeginInvoke(mainForm.statupdate, "File Saved.", thumb.OwnerTask, true, Thread.CurrentThread);
					}


				}

			}
			catch (AbortedException)
			{

			}
			catch (Exception e)
			{
				//throw new AlexandriaException("Retrieving Art Failed", e);
				MessageBox.Show(e.Message, "Retrieving Art Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			finally
			{
				if (f != null)
				{
					f.Close();
				}
			}

#region IFDEF (I'm not sure why it's here)
#if false
            try
            {
                SaveData s = (SaveData)o;
                ThumbRes baseurl;
                System.IO.FileStream f = null;
                System.Drawing.Image i = null;
                baseurl = s.indexn;
                if (s.filetargetn != null)
                    f = System.IO.File.Create(s.filetargetn);
                try
                {
                    mainForm.CoolInvoke(mainForm.statupdate, "Retrieving image...", false, false, Thread.CurrentThread);
                    mainForm.CoolInvoke(mainForm.produpdate, (int)(0));
                    object output = baseurl.script.GetResult(baseurl.callbackdata);
                    if (output is System.IO.Stream)
                    {
                        Stream ss = output as Stream;
                        if (f != null) GetStreamToFile(f, ss, ss.Length);
                        else
                        {
                            i = new System.Drawing.Bitmap(ss);
                        }
                    }
                    else if (output is String)
                    {
                        long size = new long();
                        Stream ss = GetHTTPStream(output as String, ref size);
                        if (f != null) GetStreamToFile(f, ss, size);
                        else
                        {
                            i = new System.Drawing.Bitmap(ss);
                        }
                    }
                }
                catch (Exception e)
                {
                    if (f != null)
                        f.Close();
                    throw e;
                }
                if (f != null)
                    f.Close();
                mainForm.CoolInvoke(mainForm.produpdate, (int)(-1));
                if (i != null)
                    mainForm.CoolBeginInvoke(mainForm.previewgot, i, baseurl.name);
                mainForm.CoolBeginInvoke(mainForm.statupdate, f == null ? "Complete." : "File Saved.", false, true, Thread.CurrentThread); 

            }
            catch (AbortedException)
            {
                mainForm.CoolNoThrowBeginInvoke(mainForm.statupdate, "Aborted.", false, true, Thread.CurrentThread);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                mainForm.CoolInvoke(mainForm.statupdate, "Failed with error.", false, true, Thread.CurrentThread);


            }
#endif
#endregion

			ThreadEnded(System.Threading.Thread.CurrentThread);
		}
		#endregion
		
		#region ProcessStream
		private Image ProcessStream(ref FileStream f, SaveData s, ThumbRes thumb)
		{
			if (f != null)
			{
				thumb.FullSize.Seek(0, SeekOrigin.Begin);
				GetStreamToStream(f, thumb.FullSize, thumb.FullSize.Length, false);
				return null;
			}
			else
			{
				thumb.FullSize.Seek(0, SeekOrigin.Begin);
				return System.Drawing.Bitmap.FromStream(thumb.FullSize);
			}

		}
		#endregion
		
		#endregion

		#region Internal Methods
		internal ScriptTask FetchTask()
		{
			lock (tasks)
			{
				foreach (Task t in tasks)
				{
					lock (t)
					{
						lock (t.Scripts)
						{
							if (t.Scripts.Count > 0)
							{
								Script s = t.Scripts[0];
								t.Scripts.Remove(s);
								return new ScriptTask(s, t, this);
							}
						}
					}
				}
			}
			return null;
		}

		internal void UnRegisterThread(TaskThread taskThread)
		{
			int newcount = 0;
			lock (threads)
			{
				threads.Remove(taskThread);
				newcount = threads.Count;
				if (!ThreadsActive() && e_abort.WaitOne(0, false))
				{
					mainForm.BeginInvoke(mainForm.threadended);
				}
			}
		}

		internal void RegisterThread(TaskThread taskThread)
		{
			int newcount = 0;
			lock (threads)
			{
				threads.Add(taskThread);
				newcount = threads.Count;
			}
			lock (ThreadCountChanged)
			{
				if (ThreadCountChanged != null)
				{
					//ThreadCountChanged.(this, new ThreadEventArgs(newcount));
				}
			}
		}

		internal void CompileScripts()
		{
			try
			{
				string path = System.Windows.Forms.Application.StartupPath;
				string[] files = System.IO.Directory.GetFiles(System.Windows.Forms.Application.StartupPath + "\\scripts", "*.boo");
				Dictionary<string, DateTime> oldtimes = new Dictionary<string, DateTime>();
				
				//TODO: figure out how to work around this
				//string[] fileinfolist = Properties.Settings.Default.ScriptInfo.Split('|');
				/*
				foreach (string ss in fileinfolist)
				{
					string[] filedetail = ss.Split(':');
					if (filedetail.Length == 2)
					{
						long parseres;
						if (long.TryParse(filedetail[1], out parseres))
							oldtimes[filedetail[0]] = new DateTime(parseres, DateTimeKind.Utc);
					}
				}
				bool compileneeded = files.Length != fileinfolist.Length;
				*/
				bool compileneeded = true;
				
				if (!compileneeded)
				{
					if (!System.IO.File.Exists(System.Windows.Forms.Application.StartupPath + "\\scripts\\scriptcache.dll"))
					{
						compileneeded = true;
					}
				}
				List<string> newkey = new List<string>();
				foreach (string file in files)
				{
					DateTime spec = System.IO.File.GetLastWriteTimeUtc(file);
					if (!oldtimes.ContainsKey(System.IO.Path.GetFileName(file).ToLowerInvariant()) || oldtimes[System.IO.Path.GetFileName(file)] != spec)
					{
						compileneeded = true;
					}
					newkey.Add(System.IO.Path.GetFileName(file) + ":" + spec.Ticks);
				}
				if (compileneeded)
				{
					ScriptCompilerForm ss = new ScriptCompilerForm(string.Join("|", newkey.ToArray()));
					ss.ShowDialog();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}
		#endregion

		#region Public Properties
		public MainForm MainForm
		{
			get { return mainForm; }
		}
		
		public Task SelectedTask
		{
			get
			{
				lock (this)
				{
					return selectedtask;
				}
			}
			set
			{
				lock (this)
				{
					selectedtask = value;
					lock (selectedtask.results)
					{
						foreach (ThumbRes r in selectedtask.results)
						{
							SendThumb(r);
						}
					}
				}
			}
		}
		#endregion
		
		#region Public Methods
		public void SendThumb(ThumbRes r)
		{
			mainForm.BeginInvoke(mainForm.thumbupdate, r);
		}

		public void AddTask(Task t)
		{
			lock (scripts)
			{
				t.Scripts.AddRange(scripts);
				t.ScriptsToGo = scripts.Count;
			}
			lock (tasks)
			{
				tasks.Add(t);
			}
			if (TaskAdded != null)
			{
				TaskAdded(this, new TaskEventArgs(t));
			}
			lock (threads)
			{
				foreach (TaskThread tt in threads)
				{
					tt.WakeUp();
				}
			}
		}
		
		public void SetThreads()
		{
			if (!e_abort.WaitOne(0, false))
			{
				//WARNING: changing this may cause threading problems
				int difference = 0; //(int)Properties.Settings.Default.ThreadCount - threads.Count;
				lock (threads)
				{
					while (difference < 0)
					{
						TaskThread victim = threads[0];
						victim.End();
						++difference;
					}
					while (difference > 0)
					{
						TaskThread t = new TaskThread(this);
						t.Start();
						--difference;
					}
				}
			}
			else
			{
				lock (threads)
				{
					foreach (TaskThread tt in threads)
					{
						tt.End();
					}
				}
			}
		}
		
		public void RemoveTask(Task t)
		{
			lock (tasks)
			{
				t.Abort();
				if (tasks.Contains(t))
					tasks.Remove(t);
			}
			if (TaskRemoved != null)
			{
				TaskRemoved(this, new TaskEventArgs(t));
			}
		}
		
		public void LoadScripts()
		{
			string path = System.Windows.Forms.Application.StartupPath + "\\scripts\\scriptcache.dll";
			Assembly ass = Assembly.LoadFile(path);
			Type[] types = ass.GetTypes();

			foreach (Type m in types)
			{
				if (m.Namespace == "CoverSources")
				{
					try
					{
						scripts.Add(new Script(this, m));
					}
					catch (Exception e)
					{
						MessageBox.Show(string.Format("Loading Source named \'{0}\' failed because: {1}", m.Name, e.Message));
					}
				}
			}
			#region Commented Out
			// string[] files = System.IO.Directory.GetFiles(System.Windows.Forms.Application.StartupPath + "\\scripts", "*.py");
			/*     foreach (string file in files)
				 {
					 if (System.IO.Path.GetFileName(file)[0] != '_')
					 {
						 Script s;
						 try
						 {
							 s = new Script(this, file);
							 scripts.Add(s);
             
						 }
						 catch (Exception e)
						 {
							 MessageBox.Show(e.Message, System.IO.Path.GetFileNameWithoutExtension(file));
						 }
					 }

				 }*/
			#endregion
			mainForm.ScriptsLoaded(scripts);

			lock (tasks)
			{
				foreach (Task t in tasks)
				{
					t.scripts.AddRange(scripts);
				}
			}

		}
		
		public void StatusUpdate(Task t)
		{

		}

		public void Abort()
		{
			e_abort.Set();
			lock (threads)
			{
				foreach (TaskThread t in threads)
				{
					t.End();
				}
				lock (tasks)
				{
					foreach (Task tt in tasks)
					{
						tt.Abort();
					}
				}
			}
		}

		public bool ShouldAbort()
		{
			return (e_abort.WaitOne(0, false));
		}

		public void SaveArt(ThumbRes result, string filetarget)
		{
			Thread t;
			lock (this)
			{
				SaveData s = new SaveData();
				s.indexn = result;
				s.filetargetn = filetarget;
				t = new Thread(new ParameterizedThreadStart(this.DoSaveArt));
				otherthreads.Add(t);
				t.Start(s);
			}
		}

		public void PopupateFullSizeStream(ThumbRes thumb)
		{
			if (thumb.FullSize == null)
			{
				mainForm.CoolInvoke(mainForm.statupdate, "Retrieving image...", null, false, Thread.CurrentThread);
				mainForm.CoolInvoke(mainForm.produpdate, (int)(0));
				object output = thumb.ScriptOwner.GetResult(thumb.CallbackData);
				if (output is System.IO.Stream)
				{
					long length = new long();

					Stream ss = output as Stream;

					if (ss.CanSeek)
						length = ss.Length;
					else
						length = 0;

					if (length > 50 * 1024 * 1024)
					{
						throw new Exception("Script returned preposterous Stream (greater than 50MB.)");
					}
					thumb.FullSize = new MemoryStream((int)(length));
					GetStreamToStream(thumb.FullSize, ss, length, true);

				}
				else if (output is String)
				{
					long size = new long();

					Stream ss = GetHTTPStream(output as String, ref size);

					if (!ss.CanSeek)
						size = 0;

					if (size > 50 * 1024 * 1024)
					{
						throw new Exception("Server returned preposterous Content-Length (greater than 50MB.)");
					}
					thumb.FullSize = new MemoryStream((int)(size));
					GetStreamToStream(thumb.FullSize, ss, size, true);

				}
			}
		}



		public void GetStreamToStream(Stream f, Stream ss, long size, bool sendprogress)
		{
			byte[] buf = new byte[4096];
			int read;
			int total = 0;
			while ((read = ss.Read(buf, 0, buf.Length)) != 0)
			{
				f.Write(buf, 0, read);
				total += read;
				if (total > 50 * 1024 * 1024)
				{
					throw new Exception("Stream is ridiculously long (50MB read so far). Stopping before something bad happens.");
				}
				if (sendprogress && size != 0)
					mainForm.CoolInvoke(mainForm.produpdate, (int)(total * 100 / size));
			}
		}

		public bool ThreadsActive()
		{
			lock (threads)
			{
				if (threads.Count > 0) return true;
			}
			lock (otherthreads)
			{
				if (otherthreads.Count > 0) return true;
			}
			return false;
		}

		public void ThreadEnded(Thread from)
		{
			lock (otherthreads)
			{
				//   System.Diagnostics.Debug.Assert(threads.Contains(from));
				otherthreads.Remove(from);
				if (e_abort.WaitOne(0, false) && !ThreadsActive())
					mainForm.BeginInvoke(mainForm.threadended);
			}
		}
		#endregion
		
		#region Public Static Methods
		static public Stream GetHTTPStream(string url, ref long size)
		{
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			size = response.ContentLength;
			return response.GetResponseStream();
		}
		#endregion
	}
}
