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
using System.Data;
using System.Threading;

using Telesophy.Alexandria.Model;
using Telesophy.Alexandria.Clients.Ankh.Views;
using Telesophy.Alexandria.Clients.Ankh.Views.Data;

namespace Telesophy.Alexandria.Clients.Ankh.Controllers
{
	public class ToolController
	{
		#region Constructors
		public ToolController()
		{
		}
		#endregion
				
		#region Private Fields
		private PersistenceController persistenceController;
		#endregion
		
		#region Private Methods
		private IMediaSet GetNewPlaylist()
		{
			Guid id = Guid.NewGuid();
            string source = Values.Source.Catalog;
			int number = 0;
			string title = ModelConstants.PLAYLIST_TITLE_DEFAULT;
			string artist = ModelConstants.ARTIST_NAME_VARIOUS;
			DateTime date = DateTime.Now;
			string format = ModelConstants.PLAYLIST_FORMAT_DEFAULT;
			Uri path = new Uri(ModelConstants.PLAYLIST_PATH_DEFAULT + id.ToString());

			return new Playlist(id, source, number, title, artist, date, format, path, null);
		}
		
		private MediaItemData GetMediaItemData(IMediaItem item)
		{
			if (item != null)
			{
				return new MediaItemData(item.Id, item.Type, item.Source, item.Number, item.Title, item.Artist, item.Album, item.Duration, item.Date, item.Format, item.Path);
			}
			
			return null;
		}
		#endregion
		
		#region Public Properties
		[CLSCompliant(false)]
		public PersistenceController PersistenceController
		{
			get { return persistenceController; }
			set { persistenceController = value; }
		}
		#endregion
		
		#region Public Methods
		public PlaylistSave CreatePlaylist()
		{
			IMediaSet playlist = GetNewPlaylist();
			return EditPlaylist(playlist);
		}
		
		[CLSCompliant(false)]
		public PlaylistSave EditPlaylist(IMediaSet playlist)
		{
			PlaylistSave control = null;
		
			if (playlist != null)
			{
				control = new PlaylistSave();
				control.Identifier = playlist.Id;
				control.Title = playlist.Title;
				control.Artist = playlist.Artist;
				control.Number = playlist.Number;
				control.Source = playlist.Source;
				control.Date = playlist.Date;
				control.Format = playlist.Format;
				control.Path = playlist.Path;
				
				foreach (IMediaItem item in playlist.Items)
				{
					control.AddItem(GetMediaItemData(item));
				}
			}
			
			control.ToolController = this;
			
			return control;
		}

		private void ProcessSavePlaylistResult(IAsyncResult result)
		{
		}

		private void SavePlaylistInternal(PlaylistSave control)
		{
			if (control != null)
			{
				Guid id = control.Identifier;
				if (id != default(Guid))
				{
					IMediaSet playlist = new Playlist();
					playlist.Id = id;
					playlist.Title = control.Title;
					playlist.Artist = control.Artist;
					playlist.Number = control.Number;
					playlist.Source = control.Source;
					playlist.Date = control.Date;
					playlist.Format = control.Format;
					playlist.Path = control.Path;

					IList<MediaItemData> itemData = control.GetItemData();
					IList<IMediaItem> items = persistenceController.CreateMediaItems(itemData);

					foreach (IMediaItem item in items)
					{
						playlist.Items.Add(item);
					}

					PersistenceController.SaveMediaSet(playlist);
				}
			}
		}
		
		public void SavePlaylist(PlaylistSave control)
		{
			SavePlaylistInternal(control);
			//Thread thread = new Thread(new ParameterizedThreadStart(SavePlaylistInternal));
			//thread.IsBackground = true;
			//SavePlaylistAsync handle = new SavePlaylistAsync(SavePlaylistInternal);
			//handle.BeginInvoke(control, new AsyncCallback(ProcessSavePlaylistResult), null);
		}
		
		private delegate void SavePlaylistAsync(PlaylistSave control);
		#endregion
	}
}
