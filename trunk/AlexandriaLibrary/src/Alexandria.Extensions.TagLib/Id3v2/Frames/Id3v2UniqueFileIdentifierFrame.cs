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
using System.Collections.Generic;

namespace Alexandria.TagLib
{
	public class Id3v2UniqueFileIdentifierFrame : Id3v2Frame
	{
		#region Constructors
		public Id3v2UniqueFileIdentifierFrame(ByteVector data) : base(data)
		{
			//owner = null;
			//identifier = null;
			//SetData(data);
			ParseHeader(data);
			ParseUniqueFields(data);
		}

		public Id3v2UniqueFileIdentifierFrame(string owner, ByteVector id) : base("UFID")
		{
			this.owner = owner;
			identifier = id;
		}

		protected internal Id3v2UniqueFileIdentifierFrame(ByteVector data, Id3v2FrameHeader header) : base(header)
		{
			//owner = null;
			//identifier = null;
			//ParseFields(FieldData(data));
			ParseUniqueFields(FieldData(data));
		}
		#endregion
		
		#region Private Fields
		private string owner;
		private ByteVector identifier;
		#endregion

		#region Private Methods
		private void ParseUniqueFields(ByteVector data)
		{
			ByteVectorCollection fields = ByteVectorCollection.Split(data, (byte)0);

			if (fields.Count != 2)
				return;

			owner = fields[0].ToString(StringType.Latin1);
			identifier = fields[1];
		}
		
		private ByteVector RenderUniqueFields()
		{
			ByteVector data = new ByteVector();

			data.Add(ByteVector.FromString(owner, StringType.Latin1));
			data.Add(TextDelimiter(StringType.Latin1));
			data.Add(identifier);

			return data;
		}
		#endregion

		#region Protected Methods
		protected override void ParseFields(ByteVector data)
		{
			ParseUniqueFields(data);
		}

		protected override ByteVector RenderFields()
		{
			return RenderUniqueFields();
		}
		#endregion

		#region Public Properties
		public string Owner
		{
			get { return owner; }
			set { owner = value; }
		}

		public ByteVector Identifier
		{
			get { return identifier; }
			set { identifier = value; }
		}
		#endregion
	}
}
