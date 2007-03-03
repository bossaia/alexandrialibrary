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
			List<FileExtension> extensions = new List<FileExtension>();
			extensions.Add(FileExtension.Ogg);
			
			this.AllowedFileExtensions = extensions;
			this.DefaultFileExtension = FileExtension.Ogg;
		}
		#endregion
	}
}
