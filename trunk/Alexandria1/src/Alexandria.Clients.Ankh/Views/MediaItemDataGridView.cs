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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Telesophy.Alexandria.Clients.Ankh.Properties;
using Telesophy.Alexandria.Clients.Ankh.Controllers;
using Telesophy.Alexandria.Clients.Ankh.Views.Data;

namespace Telesophy.Alexandria.Clients.Ankh.Views
{
	public class MediaItemDataGridView : AdvancedDataGridView<MediaItemData>
	{
		#region Constructors
		public MediaItemDataGridView()
		{
			CellFormatting += new DataGridViewCellFormattingEventHandler(MediaItemDataGridView_CellFormatting);
			
			smallImageList = new ImageList();
			musicImage = Resources.music;
			pictureImage = Resources.picture;
			bookImage = Resources.book_open;
			filmImage = Resources.film;
			televisionImage = Resources.television;
			
			smallImageList.Images.Add(musicImage);
			smallImageList.Images.Add(pictureImage);
			smallImageList.Images.Add(bookImage);
			smallImageList.Images.Add(filmImage);
			smallImageList.Images.Add(televisionImage);
		}
		#endregion
	
		#region Private Fields
		private ImageList smallImageList;
		Image musicImage;
		Image pictureImage;
		Image bookImage;
		Image filmImage;
		Image televisionImage;
		#endregion

		#region Private Event Methods
		private void MediaItemDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (Columns[e.ColumnIndex].Name == Controllers.QueueColumns.Type.Name)
			{
				string value = (e.Value != null) ? e.Value.ToString() : string.Empty;

				switch (value)
				{
					case MediaTypes.Audio:
						e.Value = musicImage;
						break;
                    case MediaTypes.Text:
						e.Value = bookImage;
						break;
					case MediaTypes.Image:
						e.Value = pictureImage;
						break;
					case MediaTypes.Video:
						e.Value = filmImage; 
						break;
					default:
						e.Value = televisionImage;
						break;
				}
			}
			if (Columns[e.ColumnIndex].Name == Controllers.QueueColumns.Duration.Name)
			{
				TimeSpan duration = (e.Value != null) ? (TimeSpan)e.Value : TimeSpan.Zero;
				if (duration.Hours > 0)
				{
					e.Value = string.Format("{0:00}:{1:00}:{2:00}", duration.Hours, duration.Minutes, duration.Seconds);
				}
				else if (duration.Minutes > 0)
				{
					e.Value = string.Format("{0:00}:{1:00}", duration.Minutes, duration.Seconds);
				}
				else
				{
					e.Value = string.Format("0:{0:00}", duration.Seconds);
				}
			}
			if (Columns[e.ColumnIndex].Name == Controllers.QueueColumns.Path.Name)
			{
				Uri path = e.Value as Uri;
				if (path != null)
				{
					string absolutePath = path.ToString();
					e.Value = absolutePath;
				}
				else e.Value = string.Empty;
			}
		}
		#endregion

		#region Public Properties
		public ImageList SmallImageList
		{
			get { return smallImageList; }
			//set { smallImageList = value; }
		}
		#endregion
	}
}
