using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.MediaFormats
{
	public class Wav : MediaFileFormat
	{
		#region Constructors
		public Wav() : base("PCM Wave", new System.Net.Mime.ContentType("audio/x-wav"), false)
		{
			List<string> extensions = new List<string>();
			extensions.Add("wav");
			extensions.Add("pcm");
			
			this.AllowedFileExtensions = extensions;
			this.DefaultFileExtension = "wav";
		}
		#endregion
	}
}
