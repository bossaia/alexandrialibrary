using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.MediaFormats
{
	public class Mp3 : MediaFileFormat
	{
		#region Constructors
		public Mp3() : base("Motion Picture Experts Group Layer 3", new System.Net.Mime.ContentType("audio/mp3"), true)
		{
			List<string> extensions = new List<string>();
			extensions.Add("mp3");
			
			this.AllowedFileExtensions = extensions;
			this.DefaultFileExtension = "mp3";
		}
		#endregion
	}
}
