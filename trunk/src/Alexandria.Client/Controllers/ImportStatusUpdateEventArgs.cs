using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Client.Controllers
{
	public class ImportStatusUpdateEventArgs : EventArgs
	{
		public ImportStatusUpdateEventArgs(int scanCount, int importCount, int errorCount, string path)
		{
			this.scanCount = scanCount;
			this.importCount = importCount;
			this.errorCount = errorCount;
			this.path = path;
			this.completed = false;
			this.completedTime = TimeSpan.Zero;
		}
		
		public ImportStatusUpdateEventArgs(int scanCount, int importCount, int errorCount, TimeSpan completedTime)
		{
			this.scanCount = scanCount;
			this.importCount = importCount;
			this.errorCount = errorCount;
			this.path = "COMPLETED";
			this.completed = true;
			this.completedTime = completedTime;
		}

		private int scanCount;
		private int importCount;
		private int errorCount;
		private string path;
		private bool completed;
		private TimeSpan completedTime;

		public int ScanCount
		{
			get { return scanCount; }
		}

		public int ImportCount
		{
			get { return importCount; }
		}

		public int ErrorCount
		{
			get { return errorCount; }
		}

		public string Path
		{
			get { return path; }
		}
		
		public bool Completed
		{
			get { return completed; }
		}
		
		public TimeSpan CompletedTime
		{
			get { return completedTime; }
		}
	}
}
