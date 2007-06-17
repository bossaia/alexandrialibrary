/***************************************************************************
    copyright            : (C) 2005 Novell, Inc.
    email                : Aaron Bockover <abockover@novell.com>
 ***************************************************************************/

/***************************************************************************
 *   This library is free software; you can redistribute it and/or modify  *
 *   it  under the terms of the GNU Lesser General Public License version  *
 *   2.1 as published by the Free Software Foundation.                     *
 *                                                                         *
 *   This library is distributed in the hope that it will be useful, but   *
 *   WITHOUT ANY WARRANTY; without even the implied warranty of            *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU     *
 *   Lesser General Public License for more details.                       *
 *                                                                         *
 *   You should have received a copy of the GNU Lesser General Public      *
 *   License along with this library; if not, write to the Free Software   *
 *   Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  *
 *   USA                                                                   *
 ***************************************************************************/

using System;
using System.Collections.Generic;
using Alexandria;
using Alexandria.Media;

namespace Alexandria.TagLib
{
    public class Picture : IPicture, IImage
    {
		#region Constructors
		public Picture()
		{
			this.resourceFormat = null;
		}
		#endregion
    
		#region Private Fields
        private string mimeType;
        private PictureType type;
        private string description;
        private ByteVector data;
        private Guid guid = Guid.NewGuid();
        //private Uri uri;
        private IMediaFormat resourceFormat;
        
        private Guid id = Guid.NewGuid();
        #endregion
   
		#region Public Properties
        public string MimeType
        {
            get { return mimeType; }
            set { mimeType = value; }
        }
      
        public PictureType Type
        {
            get { return type; }
            set { type = value; }
        }
      
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
      
        public ByteVector Data
        {
            get { return data; }
            set { data = value; }
        }
        #endregion
		
		#region Public Static Methods
		public static Picture CreateFromUri(System.Uri uri)
		{
			if (uri != null)
			{
				byte[] fc;
				string filename = null;
				string mimeType = "image/jpeg";
				string ext = "jpg";

				try
				{
					//Uri uriParse = new Uri(uri);
					//string path = uriParse.LocalPath;
					string path = uri.LocalPath;
					filename = System.IO.Path.GetFileName(path);
					if (filename.Length == 0)
						filename = null;
				}
				catch (System.IO.IOException)
				{
				}

				Picture picture = new Picture();
				picture.Data = ByteVector.FromUri(uri, out fc, true);

				if (fc.Length >= 4 && (fc[1] == 'P' && fc[2] == 'N' && fc[3] == 'G'))
				{
					mimeType = "image/png";
					ext = "png";
				}

				picture.MimeType = mimeType;
				picture.Type = PictureType.FrontCover;
				picture.Description = filename == null ? ("cover." + ext) : mimeType;

				return picture;
			}
			else throw new ArgumentNullException("uri");
		}
		#endregion

		#region IImage Members
		public void Load()
		{
		}
		
		public System.Drawing.Image Image
		{
			//TODO: finish implementing this
			get { return null; }
		}
		#endregion

		#region IVisible
		public float Hue
		{
			get { return 0f; }
		}

		public float Saturation
		{
			get { return 0f; }
		}

		public float Brightness
		{
			get { return 0f; }
		}

		public float Contrast
		{
			get { return 0f; }
		}
		#endregion

		#region IMedia Members
		public Guid Id
		{
			get { return id; }
		}

		public ILocation Location
		{
			get { return null; }
		}

		public IMediaFormat Format
		{
			get { return resourceFormat; }
		}
		#endregion

		#region IEntity Members


		public string Name
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		#endregion
	}
}
