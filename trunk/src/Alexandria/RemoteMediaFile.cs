using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class RemoteMediaFile : MediaFile
	{
		#region Constructors
		public RemoteMediaFile(string path) : base(path, false)
		{
		}
		
		public RemoteMediaFile(string path, string length) : base(path, false, MediaFile.ParseLength(length))
		{		
		}
		#endregion
	}
}
