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

namespace Alexandria.TagLib
{
	public class Id3v2Frame
	{
		#region Constructors
		protected Id3v2Frame(ByteVector data)
		{
			this.header = new Id3v2FrameHeader(data);
		}

		protected Id3v2Frame(Id3v2FrameHeader header)
		{
			this.header = header;
		}
		#endregion
		
		#region Private Fields
		private Id3v2FrameHeader header;
		#endregion
								
		#region Protected Properties
		protected internal Id3v2FrameHeader Header
		{
			get { return header; }
			set { header = value; }
		}
		#endregion
		
		#region Protected Methods
		protected void ParseHeader(ByteVector data)
		{
			if (header != null)
				header.SetData(data);
			else
				header = new Id3v2FrameHeader(data);
		}
		
		protected void Parse(ByteVector data)
		{
			ParseHeader(data);

			ParseFields(FieldData(data));
		}

		protected virtual void ParseFields(ByteVector data)
		{
		}
		
		protected virtual ByteVector RenderFields()
		{
			return new ByteVector();
		}
		
		protected ByteVector FieldData(ByteVector frameData)
		{
			if (frameData != null)
			{
				uint headerSize = Id3v2FrameHeader.Size(header.Version);

				uint frameDataOffset = headerSize;
				uint frameDataLength = Size;

				if (header.Compression || header.DataLengthIndicator)
				{
					frameDataLength = frameData.Mid((int)headerSize, 4).ToUInt();
					frameDataLength += 4;
				}

				// FIXME: Impliment compression and encrpytion.
				/*
				#if HAVE_ZLIB
				   if(d->header->compression()) {
					  ByteVector data(frameDataLength);
					  uLongf uLongTmp = frameDataLength;
					  ::uncompress((Bytef *) data.data(),
								  (uLongf *) &uLongTmp,
								  (Bytef *) frameData.data() + frameDataOffset,
								  size());
					  return data;
				   }
				   else
				#endif
				*/

				return frameData.Mid((int)frameDataOffset, (int)frameDataLength);
			}
			else throw new ArgumentNullException("frameData");
		}
		#endregion
		
		#region Public Properties
		public ByteVector FrameId
		{
			get { return (header != null) ? header.FrameId : null; }
		}

		[System.CLSCompliant(false)]
		public uint Size
		{
			get { return (header != null) ? header.FrameSize : 0; }
		}
		#endregion
		
		#region Public Methods
		public void SetData(ByteVector data)
		{
			Parse(data);
		}

		public virtual void SetText(string text)
		{
		}

		public ByteVector Render()
		{
			ByteVector fieldData = RenderFields();
			header.FrameSize = (uint)fieldData.Count;
			ByteVector headerData = header.Render();

			return headerData + fieldData;
		}
		#endregion
	
		#region Public Static Methods
		public static ByteVector TextDelimiter(StringType type)
		{
			return new ByteVector((type == StringType.UTF16 || type == StringType.UTF16BE) ? 2 : 1, (byte)0);
		}
		#endregion
	}
}
