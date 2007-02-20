using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.MediaFormats
{
	public class Wav : MediaFileFormat
	{
		#region Constructors
		public Wav() : base("PCM Wave", new MimeType(ContentType.Audio, "x-wav"), false)
		{
			List<FileExtension> extensions = new List<FileExtension>();
			extensions.Add(FileExtension.Wav);
			extensions.Add(FileExtension.Pcm);
			
			this.AllowedFileExtensions = extensions;
			this.DefaultFileExtension = FileExtension.Wav;
		}
		#endregion
	}
}
