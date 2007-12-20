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
using System.Net.Mime;

namespace Telesophy.Alexandria.Extensions.Playlist
{
	public class M3uFormat //: BaseMediaFormat
	{
		#region Constructors
		public M3uFormat() //: base("MP3 Playlist Format", "")
		{
			//this.ContentTypes.Add(new ContentType("application/m3u"));
			//this.ContentTypes.Add(new ContentType("audio/x-mpegurl"));
			//this.ContentTypes.Add(new ContentType("audio/mpeg-url"));
			//this.ContentTypes.Add(new ContentType("application/x-winamp-playlist"));
			//this.ContentTypes.Add(new ContentType("audio/scpls"));
			//this.ContentTypes.Add(new ContentType("audio/x-scpls"));
			//this.FileExtensions.Add("m3u");
		}
		#endregion
	}
}
