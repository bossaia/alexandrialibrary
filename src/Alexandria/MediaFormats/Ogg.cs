using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.MediaFormats
{
	public class Ogg : MediaFileFormat
	{
		#region Constructors
		public Ogg() : base("Ogg Vorbis", new MimeType(ContentType.Application, "ogg"), true)
		{
			List<FileExtension> extensions = new List<FileExtension>();
			extensions.Add(FileExtension.Ogg);
			
			this.AllowedFileExtensions = extensions;
			this.DefaultFileExtension = FileExtension.Ogg;
		}
		#endregion
	}
}
