using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.LastFM
{
	#region StatusCallback
	public struct StatusCallback
	{
		#region Public Fields
		public int requestId;
		public bool error;
		public string message;
		public IntPtr userData;
		#endregion
	}
	#endregion
}
