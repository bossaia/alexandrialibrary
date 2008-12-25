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
    public class Values
    {
        public static class Source
        {
            public const string Catalog = "Catalog";
            public const string File = "File";
            public const string Web = "Web";
        }
    }

	public static class ModelConstants
	{
        // Media Type Constants
        public const string MEDIA_TYPE_AUDIO = "Audio";
        public const string MEDIA_TYPE_VIDEO = "Video";
        public const string MEDIA_TYPE_IMAGE = "Image";
        public const string MEDIA_TYPE_PLAYLIST = "Playlist";
        public const string MEDIA_TYPE_TEXT = "Text";

        // Artist Type Constants
        public const string ARTIST_TYPE_PERSON = "Person";
        public const string ARTIST_TYPE_GROUP = "Group";

        // Artist Constants
        public const string ARTIST_NAME_UNKNOWN = "Unknown Artist";
        public const string ARTIST_NAME_VARIOUS = "Various Artists";

        // Playlist Constants
        public const string PLAYLIST_TITLE_DEFAULT = "New Playlist";
        public const string PLAYLIST_FORMAT_DEFAULT = "application/x-alexandria-playlist";
        public const string PLAYLIST_PATH_DEFAULT = "http://www.telesophy.org/schema/1.0/playlist/";

        // MediaItem Column Constants
        public const string MEDIA_ITEM_COLUMN_ID = "Id";
        public const string MEDIA_ITEM_COLUMN_TYPE = "Type";
        public const string MEDIA_ITEM_COLUMN_SOURCE = "Source";
        public const string MEDIA_ITEM_COLUMN_NUMBER = "Number";
        public const string MEDIA_ITEM_COLUMN_TITLE = "Title";
        public const string MEDIA_ITEM_COLUMN_ARTIST = "Artist";
        public const string MEDIA_ITEM_COLUMN_ALBUM = "Album";
        public const string MEDIA_ITEM_COLUMN_DURATION = "Duration";
        public const string MEDIA_ITEM_COLUMN_DATE = "Date";
        public const string MEDIA_ITEM_COLUMN_FORMAT = "Format";
        public const string MEDIA_ITEM_COLUMN_PATH = "Path";
	}
}
