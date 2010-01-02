/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : 
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

namespace Alexandria.TagLib
{
	[SupportedMimeType("taglib/wma")]
	[SupportedMimeType("taglib/asf")]
	[SupportedMimeType("audio/x-ms-wma")]
	[SupportedMimeType("video/x-ms-asf")]
	public class AsfFile : File
	{
		#region Constructors
		public AsfFile(string file, ReadStyle propertiesStyle) : base(file)
		{
			//asfTag = null;
			//properties = null;

			try
			{
				Mode = FileAccessMode.Read; 
			}
			catch (TagLibException)
			{
				return;
			}

			Read(propertiesStyle);

			Mode = FileAccessMode.Closed;
		}

		public AsfFile(string file) : this(file, ReadStyle.Average)
		{
		}
		#endregion
		
		#region Private Fields
		private AsfTag asfTag;
		private AsfProperties properties;
		#endregion
		
		#region Private Methods
		private void Read(ReadStyle propertiesStyle)
		{
			AsfHeaderObject header = new AsfHeaderObject(this, 0);

			asfTag = new AsfTag(header);

			if (propertiesStyle != ReadStyle.None)
				properties = new AsfProperties(header, propertiesStyle);
		}
		#endregion
		
		#region Public Properties
		public override Tag Tag
		{
			get {return asfTag;}
		}

		public override AudioProperties AudioProperties
		{
			get {return properties;}
		}
		#endregion
		
		#region Public Methods
		public override void Save()
		{
			if (IsReadOnly)
				throw new ReadOnlyException();

			Mode = FileAccessMode.Write;

			AsfHeaderObject header = new AsfHeaderObject(this, 0);
			header.AddUniqueObject(asfTag.ContentDescriptionObject);
			header.AddUniqueObject(asfTag.ExtendedContentDescriptionObject);

			Insert(header.Render(), 0, header.OriginalSize);

			Mode = FileAccessMode.Closed;
		}

		public override Tag FindTag(TagTypes type, bool create)
		{
			if (type == TagTypes.Asf)
				return asfTag;

			return null;
		}

		public short ReadWord()
		{
			ByteVector v = ReadBlock(2);
			return v.ToShort(false);
		}

		[System.CLSCompliant(false)]
		public uint ReadDWord()
		{
			ByteVector v = ReadBlock(4);
			return v.ToUInt(false);
		}

		public long ReadQWord()
		{
			ByteVector v = ReadBlock(8);
			return v.ToLong(false);
		}

		public AsfGuid ReadGuid()
		{
			ByteVector v = ReadBlock(16);
			return new AsfGuid(v);
		}

		public string ReadUnicode(int length)
		{
			ByteVector data = ReadBlock(length);
			string output = data.ToString(StringType.UTF16LE);
			int i = output.IndexOf('\0');
			return (i >= 0) ? output.Substring(0, i) : output;
		}

		[System.CLSCompliant(false)]
		public AsfObject[] ReadObjects(uint count, long position)
		{
			ArrayList l = new ArrayList();
			for (int i = 0; i < (int)count; i++)
			{
				Seek(position);
				AsfGuid id = ReadGuid();

				AsfObject obj;

				if (id.Equals(AsfGuid.AsfFilePropertiesObject))
					obj = new AsfFilePropertiesObject(this, position);
				else if (id.Equals(AsfGuid.AsfStreamPropertiesObject))
					obj = new AsfStreamPropertiesObject(this, position);
				else if (id.Equals(AsfGuid.AsfContentDescriptionObject))
					obj = new AsfContentDescriptionObject(this, position);
				else if (id.Equals(AsfGuid.AsfExtendedContentDescriptionObject))
					obj = new AsfExtendedContentDescriptionObject(this, position);
				else if (id.Equals(AsfGuid.AsfPaddingObject))
					obj = new AsfPaddingObject(this, position);
				else
					obj = new AsfUnknownObject(this, position);

				l.Add(obj);
				position += obj.OriginalSize;
			}

			return (AsfObject[])l.ToArray(typeof(AsfObject));
		}
		#endregion
	}
}
