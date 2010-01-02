using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.TagLib
{
	public interface IPicture
	{
		string MimeType { get; set; }
		PictureType Type { get; set; }
		string Description { get; set; }
		ByteVector Data { get; set; }
	}
}
