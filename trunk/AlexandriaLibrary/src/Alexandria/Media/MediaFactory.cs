#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;
using Alexandria;

namespace Alexandria.Media
{
	public class MediaFactory
	{
		#region Constructors
		public MediaFactory()
		{
		}
		#endregion
		
		#region Private Methods
		private ContentType GetContentType(string path)
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
		
		#region Public Methods
		public IMedia LookupMedia(Uri path)
		{
			return null;
		}		
		#endregion
	}
}
