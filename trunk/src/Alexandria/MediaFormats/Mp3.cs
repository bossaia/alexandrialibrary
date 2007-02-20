using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.MediaFormats
{
	public class Mp3 : MediaFileFormat
	{
		#region Constructors
		public Mp3() : base("Motion Picture Experts Group Layer 3", new MimeType(ContentType.Audio, "mp3"), true)
		{
			List<FileExtension> extensions = new List<FileExtension>();
			extensions.Add(FileExtension.Mp3);
			
			this.AllowedFileExtensions = extensions;
			this.DefaultFileExtension = FileExtension.Mp3;
		}
		#endregion
	}
}
