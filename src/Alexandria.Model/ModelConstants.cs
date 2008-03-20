#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2008 Dan Poage
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
using System.Text;

namespace Telesophy.Alexandria.Model
{
	public static class ModelConstants
	{
		// Media Type Constants
		public const string MEDIA_TYPE_AUDIO = "Audio";
		public const string MEDIA_TYPE_VIDEO = "Video";
		public const string MEDIA_TYPE_IMAGE = "Image";
		public const string MEDIA_TYPE_PLAYLIST = "Playlist";
		
		// Artist Type Constants
		public const string ARTIST_TYPE_PERSON = "Person";
		public const string ARTIST_TYPE_GROUP = "Group";
		
		// Source Constants
		public const string SOURCE_CATALOG = "Catalog";
		public const string SOURCE_FILE = "File";
		public const string SOURCE_WEB = "Web";
		
		// Unknown Constants
		public const string UNKNOWN_ARTIST = "Unknown Artist";
	}
}
