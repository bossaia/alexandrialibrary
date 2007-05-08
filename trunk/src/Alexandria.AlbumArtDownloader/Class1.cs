/*  This file is part of Album Art Downloader.
 *  CoverDownloader is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2 of the License, or
 *  (at your option) any later version.
 *
 *  CoverDownloader is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with CoverDownloader; if not, write to the Free Software             
 *  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA  */

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

namespace Alexandria.AlbumArtDownloader
{
	public class TaskThread
	{
		#region Constructors
		public TaskThread(ArtDownloader a)
		{
			artdownloader = a;
			e_abort = new ManualResetEvent(false);
			e_wakeup = new AutoResetEvent(false);
			thread = new Thread(new ThreadStart(this.Run));
			a.RegisterThread(this);
		}
		#endregion
		
		#region Private Fields
		public Thread thread;
		ManualResetEvent e_abort;
		AutoResetEvent e_wakeup;
		ArtDownloader artdownloader;
		#endregion
		
		#region Private Methods
		private void Run()
		{
			while (!e_abort.WaitOne(0, false))
			{
				ScriptTask sTask = artdownloader.FetchTask();
				if (sTask != null)
				{
					try
					{
						if (sTask.Script.Enabled)
						{
							sTask.Script.GetThumbs(sTask, sTask.Task.Artist, sTask.Task.Album);
							lock (sTask.Task.results)
							{
								string s = string.Format("{0}/{1}", sTask.ResourceCount, sTask.ResourceCount);
								artdownloader.MainForm.BeginInvoke(artdownloader.MainForm.taskupdate, sTask.Task, sTask.Script, s);
							}
						}
						else
						{
							artdownloader.MainForm.BeginInvoke(artdownloader.MainForm.taskupdate, sTask.Task, sTask.Script, "Script disabled");
						}
					}
					catch (AbortedException)
					{
						continue;
					}
					catch (Exception e)
					{
						artdownloader.MainForm.BeginInvoke(artdownloader.MainForm.taskupdate, sTask.Task, sTask.Script, e.Message);
					}
					finally
					{
						--sTask.Task.ScriptsToGo;
					}
				}
				else
					e_wakeup.WaitOne();
			}
			artdownloader.UnRegisterThread(this);
		}
		#endregion
		
		#region Public Methods
		public void Start()
		{
			thread.Start();
		}
		
		public void End()
		{
			e_abort.Set();
			WakeUp();
		}
		
		public void WakeUp()
		{
			e_wakeup.Set();
		}
		#endregion		
	}
}