/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : id3v2frame.cpp from TagLib
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
using System.Collections;
using System.Collections.Generic;
using Alexandria;

namespace Alexandria.TagLib
{   
	public class Id3v2AttachedPictureFrame : Id3v2Frame, IPicture, IImage
	{
		#region Constructors
		public Id3v2AttachedPictureFrame() : base("APIC")
		{
			//textEncoding = StringType.UTF8;
			//mimeType = null;
			type = PictureType.None;
			//fields = null;
			//data = null;
		}

		public Id3v2AttachedPictureFrame(IPicture picture) : base("APIC")
		{
			if (picture != null)
			{
				//textEncoding = StringType.UTF8;
				mimeType = picture.MimeType;
				type = picture.Type;
				description = picture.Description;
				data = picture.Data;
			}
		}

		public Id3v2AttachedPictureFrame(ByteVector data) : base(data)
		{
			//textEncoding = StringType.UTF8;
			//mimeType = null;
			//type = PictureType.None;
			//fields = null;
			//this.data = null;

			//SetData(data);
			ParseHeader(data);
			ParseAttachedPictureFields(data);
		}

		protected internal Id3v2AttachedPictureFrame(ByteVector data, Id3v2FrameHeader header) : base(header)
		{
			//textEncoding = StringType.UTF8;
			//mimeType = null;
			//type = PictureType.None;
			//fields = null;
			//this.data = null;

			ParseAttachedPictureFields(FieldData(data));
		}
		#endregion
		
		#region Private Fields
		private StringType textEncoding = StringType.UTF8;
		private string mimeType;
		private PictureType type = PictureType.None;
		private string description;
		private ByteVector data;
		private Guid guid = Guid.NewGuid();
		//private Uri uri;
		private IMediaFormat resourceFormat;
		#endregion
		
		#region Private Methods
		private void ParseAttachedPictureFields(ByteVector data)
		{
			if (data != null)
			{
				if (data.Count < 5)
				{
					TagLibDebugger.Debug("A picture frame must contain at least 5 bytes.");
					return;
				}

				int pos = 0;

				textEncoding = (StringType)data[pos];
				pos += 1;

				int offset;

				if (Header.Version > 2)
				{
					offset = data.Find(TextDelimiter(StringType.Latin1), pos);

					if (offset < pos)
						return;

					mimeType = data.Mid(pos, offset - pos).ToString(StringType.Latin1);
					pos = offset + 1;
				}
				else
				{
					ByteVector ext = data.Mid(pos, 3);
					if (ext == "JPG")
						mimeType = "image/jpeg";
					else if (ext == "PNG")
						mimeType = "image/png";
					else
						mimeType = "image/unknown";

					pos += 3;
				}

				type = (PictureType)data[pos];
				pos += 1;

				offset = data.Find(TextDelimiter(textEncoding), pos);

				if (offset < pos)
					return;

				description = data.Mid(pos, offset - pos).ToString(textEncoding);
				pos = offset + 1;

				this.data = data.Mid(pos);			
			}
			else throw new ArgumentNullException("data");
		}
		
		private ByteVector RenderAttachedPictureFields()
		{
			ByteVector data = new ByteVector();

			data.Add((byte)TextEncoding);
			data.Add(ByteVector.FromString(MimeType, TextEncoding));
			data.Add(TextDelimiter(StringType.Latin1));
			data.Add((byte)type);
			data.Add(ByteVector.FromString(Description, TextEncoding));
			data.Add(TextDelimiter(TextEncoding));
			data.Add(this.data);

			return data;
		}
		#endregion
		
		#region Protected Methods
		protected override void ParseFields(ByteVector data)
		{
			ParseAttachedPictureFields(data);
		}

		protected override ByteVector RenderFields()
		{
			return RenderAttachedPictureFields();
		}
		#endregion
		
		#region Public Properties
		public StringType TextEncoding
		{
			get {return textEncoding;}
			set {textEncoding = value;}
		}

		public string MimeType
		{
			get {return mimeType;}
			set {mimeType = value;}
		}

		public PictureType Type
		{
			get {return type;}
			set {type = value;}
		}

		public string Description
		{
			get {return description;}
			set {description = value;}
		}

		public ByteVector Data
		{
			get {return data;}
			set {data = value;}
		}
		#endregion
		
		#region Public Methods
		public override string ToString()
		{
			string s = "[" + mimeType + "]";
			return description != null ? s : description + " " + s;
		}
		#endregion
		
		#region Public Static Methods
		public static Id3v2AttachedPictureFrame Find(Id3v2Tag tag, string description)
		{
			if (tag != null)
			{
				foreach (Id3v2AttachedPictureFrame frame in tag.GetFrames("APIC"))
					if (frame != null && frame.Description == description)
						return frame;
				return null;
			}
			else throw new ArgumentNullException("tag");
		}

		public static Id3v2AttachedPictureFrame Find(Id3v2Tag tag, PictureType type)
		{
			if (tag != null)
			{
				foreach (Id3v2AttachedPictureFrame frame in tag.GetFrames("APIC"))
					if (frame != null && frame.Type == type)
						return frame;
				return null;
			}
			else throw new ArgumentNullException("tag");
		}

		public static Id3v2AttachedPictureFrame Find(Id3v2Tag tag, string description, PictureType type)
		{
			if (tag != null)
			{
				foreach (Id3v2AttachedPictureFrame frame in tag.GetFrames("APIC"))
					if (frame != null && frame.Description == description && frame.Type == type)
						return frame;
				return null;
			}
			else throw new ArgumentNullException("tag");
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

		#region IResource Members
		public IIdentifier Id
		{
			get { return null; }
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