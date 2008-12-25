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
using System.IO;
using System.Text;

using Telesophy.Alexandria.Model;

namespace Telesophy.Alexandria.Extensions.Playlist.M3u
{
	public class M3uPlaylistFactory
	{
		public IMediaSet LoadPlaylistFile(Uri path)
		{
			IMediaSet playlist = null;
		
			if (path != null && File.Exists(path.LocalPath))
			{
			    FileInfo file = new FileInfo(path.LocalPath);
			    if (file.Extension.Equals(PlaylistConstants.PLAYLIST_EXT_M3U, StringComparison.OrdinalIgnoreCase))
			    {
					Guid id = Guid.NewGuid();
					string source = Values.Source.File;
					int number = 0;
					string title = file.Name;
					string artist = ModelConstants.ARTIST_NAME_UNKNOWN;
					DateTime date = DateTime.MinValue;
					string format = PlaylistConstants.PLAYLIST_FORMAT_M3U;
					
					playlist = new Telesophy.Alexandria.Model.Playlist(id, source, number, title, artist, date, format, path, null);
			    
					UriBuilder uriBuilder = new UriBuilder();
			    
					StreamReader reader = file.OpenText();
					while (!reader.EndOfStream)
					{
						string trackName = reader.ReadLine();
						if (!string.IsNullOrEmpty(trackName))
						{
							
							Guid trackId = Guid.NewGuid();
							
							IMediaItem track = null;
							//IPlaylistItem item = new PlaylistItem(new Uri(fileName));
							//Items.Add(item);
							
							playlist.Items.Add(track);
						}
					}
				}
			}
			
			return playlist;
		}
		
		public void SavePlaylist(IMediaSet playlist)
		{
		
		}
	}
}
