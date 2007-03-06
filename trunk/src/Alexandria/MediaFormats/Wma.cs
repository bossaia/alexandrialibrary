using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.MediaFormats
{
	public class Wma : MediaFileFormat
	{
		public Wma() : base("Windows Media Audio", new System.Net.Mime.ContentType("audio/wma"), true)
		{
			List<string> extensions = new List<string>();
			extensions.Add("wma");
			
			this.AllowedFileExtensions = extensions;
			this.DefaultFileExtension = "wma";
		}
	}
}
