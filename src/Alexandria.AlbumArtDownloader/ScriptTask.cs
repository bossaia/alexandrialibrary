using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Alexandria.AlbumArtDownloader
{
	public class ScriptTask
	{
		#region Constructors
		public ScriptTask(Script s, Task t, ArtDownloader a)
		{
			script = s;
			task = t;
			artdownloader = a;
		}
		#endregion
		
		#region Private Fields
		private int estimate;
		private Script script;
		private Task task;
		private ArtDownloader artdownloader;
		private int rescount;
		#endregion
		
		#region Public Properties
		public bool IsDebug
		{
			get { return task.IsDebug; }
		}
		
		public int Estimate
		{
			get { return estimate; }
			set { estimate = value; }
		}
		
		public Script Script
		{
			get { return script; }
		}
		
		public Task Task
		{
			get { return task; }
		}
		
		public int ResourceCount
		{
			get { return rescount; }
			set { rescount = value; }
		}
		#endregion
		
		#region Public Methods
		
		#region AddThumb
		public void AddThumb(string thumb, string name, int width, int height, object callback)
		{
			if (task.ShouldAbort())
				throw new AbortedException();
			long size = new long();
			Stream http = ArtDownloader.GetHTTPStream(thumb, ref size);
			ThumbRes result = new ThumbRes();
			result.Width = width;
			result.Height = height;
			result.CallbackData = callback;
			result.OwnerTask = task;
			result.ScriptOwner = script;
			if (callback == null)
			{
				result.FullSize = new MemoryStream();
				artdownloader.GetStreamToStream(result.FullSize, http, 0, false);
				result.Thumb = System.Drawing.Bitmap.FromStream(result.FullSize);
				result.FullSize.Seek(0, SeekOrigin.Begin);
			}
			else
				result.Thumb = System.Drawing.Bitmap.FromStream(http);
			result.Name = name;
			task.AddResult(result);
			++rescount;
			//artdownloader.mainForm.BeginInvoke(artdownloader.mainForm.taskupdate, task, script, estimate == 0 ? string.Format("{0}", rescount) : string.Format("{0}/{1}", rescount, estimate));
		}
		
		public void AddThumb(Stream thumb, string name, int width, int height, object callback)
		{
			if (task.ShouldAbort())
				throw new AbortedException();
			ThumbRes result = new ThumbRes();
			result.Width = width;
			result.Height = height;
			result.CallbackData = callback;
			if (callback == null)
			{
				result.FullSize = new MemoryStream();
				artdownloader.GetStreamToStream(result.FullSize, thumb, 0, false);
				result.Thumb = System.Drawing.Bitmap.FromStream(result.FullSize);
				result.FullSize.Seek(0, SeekOrigin.Begin);
			}
			else
				result.Thumb = System.Drawing.Bitmap.FromStream(thumb);
			result.OwnerTask = task;
			result.ScriptOwner = script;
			result.Name = name;
			task.AddResult(result);
			++rescount;
			//artdownloader.mainForm.BeginInvoke(artdownloader.mainForm.taskupdate, task, script, estimate == 0 ? string.Format("{0}", rescount) : string.Format("{0}/{1}", rescount, estimate));
		}
		
		public void AddThumb(System.Drawing.Image thumb, string name, int width, int height, object callback)
		{
			if (task.ShouldAbort())
				throw new AbortedException();
			ThumbRes result = new ThumbRes();
			result.Width = width;
			result.Height = height;
			result.Thumb = thumb;
			result.OwnerTask = task;
			result.CallbackData = callback;
			result.ScriptOwner = script;
			result.Name = name;
			task.AddResult(result);
			++rescount;
			//artdownloader.mainForm.BeginInvoke(artdownloader.mainForm.taskupdate, task, script, estimate == 0 ? string.Format("{0}", rescount) : string.Format("{0}/{1}", rescount, estimate));
		}
		#endregion
		
		#region DebugHtml
		public void DebugHTML(string html)
		{
			if (IsDebug)
			{
				//artdownloader.mainForm.BeginInvoke(artdownloader.mainForm.dohtml, false, html);
			}
		}
		#endregion
		
		#region DebugUrl
		public void DebugURL(string html)
		{
			if (IsDebug)
			{
				//artdownloader.mainForm.BeginInvoke(artdownloader.mainForm.dohtml, true, html);
			}
		}
		#endregion
		
		#endregion
	}
}
