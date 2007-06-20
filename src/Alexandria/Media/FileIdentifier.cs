using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;
using Alexandria;

namespace Alexandria.FileTools
{
	public static class FileIdentifier
	{
		#region Public Static Methods
		public static System.Net.Mime.ContentType GetContentType(string path)
		{
			try
			{
				System.Net.Mime.ContentType contentType = null;

				using (FileStream stream = new FileStream(path, FileMode.Open))
				{
					BinaryReader reader = new BinaryReader(stream);

					byte[] header = new byte[64];
					reader.Read(header, 0, 64);

					//index  offset    string     hexadecimal

					//0      4         OggS       0x4F 0x67 0x67 0x53             
					if (header[0] == 0x4F && header[1] == 0x67 && header[2] == 0x67 && header[3] == 0x53)
						contentType = new System.Net.Mime.ContentType("application/ogg");
					//29     6         vorbis     0x76 0x6F 0x72 0x62 0x69 0x73
					else if (header[28] == 0x76 && header[29] == 0x6F && header[30] == 0x72 && header[31] == 0x62 && header[32] == 0x69 && header[33] == 0x73)
						contentType = new System.Net.Mime.ContentType("audio/vorbis");
					//0      4                    0xFF 0xF0 0xFF 0xF0
					else if (header[0] == 0xFF && header[1] == 0xF0 && header[2] == 0xFF && header[3] == 0xF0)
						contentType = new System.Net.Mime.ContentType("audio/mpeg");
					//0      4                    0x49 0x44 0x33 0x2|0x3
					else if (header[0] == 0x49 && header[1] == 0x44 && header[2] == 0x33 && (header[3] == 0x2 || header[3] == 0x3)) //0x49 0x44 0x33 0x2|0x3 :: 73, 68, 51, 2|3 VBR?					
						contentType = new System.Net.Mime.ContentType("audio/mp3");
					//0      16                   0x30 0x26 0xB2 0x75 0x8E 0x66 0xCF 0x11 0xA6 0xD9 0x00 0xAA 0x00 0x62 0xCE 0x6C
					else if (header[0] == 0x30 && header[1] == 0x26 && header[2] == 0xb2 && header[3] == 0x75 && header[4] == 0x8e && header[5] == 0x66 && header[6] == 0xCF && header[7] == 0x11 && header[8] == 0xA6 && header[9] == 0xD9 && header[10] == 0x00 && header[11] == 0xAA && header[12] == 0x00 && header[13] == 0x62 && header[14] == 0xCE && header[15] == 0x6C)
						contentType = new System.Net.Mime.ContentType("video/x-ms-asf"); //"audio/x-ms-wma"; //ASF GUID: F8699E40-5B4D-11CF-A8FD-00805F5C442B

					return contentType;
				}
			}
			catch (Exception ex)
			{
				throw new AlexandriaException(ex);
			}
		}
		#endregion
	}
}
