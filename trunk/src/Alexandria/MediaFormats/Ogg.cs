using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.MediaFormats
{
	public class Ogg : MediaFileFormat
	{
		#region Constructors
		public Ogg() : base("Ogg Vorbis", new System.Net.Mime.ContentType("application/ogg"), true)
		{
			List<string> extensions = new List<string>();
			extensions.Add("ogg");
			
			this.AllowedFileExtensions = extensions;
			this.DefaultFileExtension = "ogg";
		}
		#endregion
	}
}
