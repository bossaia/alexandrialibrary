using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Client
{
	public class UpdateStatusEventArgs : EventArgs
	{
		public UpdateStatusEventArgs(string status, string description)
		{
			this.status = status;
			this.description = description;
		}
		
		private string status;
		private string description;
		
		public string Status
		{
			get { return status; }
		}
		
		public string Description
		{
			get { return description; }
		}
	}
}
