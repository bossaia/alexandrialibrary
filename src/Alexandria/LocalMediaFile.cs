using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class LocalMediaFile : MediaFile
	{
		#region Constructors
		public LocalMediaFile(string path) : base(path, true)
		{
		}
		
		public LocalMediaFile(string path, string length) : base(path, true, MediaFile.ParseLength(length))
		{
		}
		#endregion
	}
}
