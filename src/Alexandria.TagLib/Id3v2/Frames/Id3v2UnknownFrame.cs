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

namespace AlexandriaOrg.Alexandria.TagLib
{
	public class Id3v2UnknownFrame : Id3v2Frame
	{
		#region Constructors
		public Id3v2UnknownFrame(ByteVector data) : base(data)
		{
			//fieldData = null;
			//SetData(data);
			ParseHeader(data);
			ParseUnknownFields(data);
		}

		protected internal Id3v2UnknownFrame(ByteVector data, Id3v2FrameHeader header) : base(header)
		{
			//fieldData = null;
			//ParseFields(FieldData(data));
			ParseUnknownFields(FieldData(data));
		}
		#endregion
		
		#region Private Fields
		private ByteVector fieldData;
		#endregion
		
		#region Private Methods
		private void ParseUnknownFields(ByteVector data)
		{
			fieldData = data;
		}
		
		private ByteVector RenderUnknownFields()
		{
			return fieldData;
		}
		#endregion
		
		#region Protected Methods
		protected override void ParseFields(ByteVector data)
		{
			ParseUnknownFields(data);
		}

		protected override ByteVector RenderFields()
		{
			return RenderUnknownFields();
		}
		#endregion
		
		#region Public Properties
		public ByteVector Data
		{
			get { return fieldData; }
		}
		#endregion
		
		#region Public Methods
		public override string ToString()
		{
			return null;
		}
		#endregion
	}
}
