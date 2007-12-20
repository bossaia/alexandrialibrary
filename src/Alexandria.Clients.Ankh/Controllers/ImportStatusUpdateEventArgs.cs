#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;

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
