using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.AlbumArtDownloader
{
	public class TaskEventArgs : EventArgs
	{	
		#region Constructors	
		public TaskEventArgs(Task task)
		{
			this.task = task;
		}
		#endregion
		
		#region Private Fields
		private Task task;
		#endregion
		
		#region Public Properties
		public Task Task
		{
			get { return task; }
		}
		#endregion
	}
}
