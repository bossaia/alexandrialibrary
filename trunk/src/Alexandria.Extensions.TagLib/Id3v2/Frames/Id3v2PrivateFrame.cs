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
	public class Id3v2PrivateFrame : Id3v2Frame
	{
		#region Constructors
		public Id3v2PrivateFrame(string owner, ByteVector data) : base("PRIV")
		{
			this.owner = owner;
			this.data = data;
		}

		public Id3v2PrivateFrame(string owner) : this(owner, null)
		{
		}

		public Id3v2PrivateFrame(ByteVector data) : base(data)
		{
			//this.owner = null;
			//this.data = null;
			//SetData(data);
			ParseHeader(data);
			ParsePrivateFields(data);
		}
		
		protected internal Id3v2PrivateFrame(ByteVector data, Id3v2FrameHeader header) : base(header)
		{
			//this.owner = null;
			//this.data = null;
			//ParseFields(FieldData(data));
			ParsePrivateFields(FieldData(data));
		}
		#endregion
		
		#region Private Fields
		private string owner;
		private ByteVector data;
		#endregion
		
		#region Private Methods
		private void ParsePrivateFields(ByteVector data)
		{
			if (data.Count < 1)
			{
				TagLibDebugger.Debug("A private frame must contain at least 1 byte.");
				return;
			}

			ByteVectorCollection list = ByteVectorCollection.Split(data, TextDelimiter(StringType.Latin1), 1, 2);

			if (list.Count == 2)
			{
				owner = list[0].ToString(StringType.Latin1);
				data = list[1];
			}
		}
		
		private ByteVector RenderPrivateFields()
		{
			ByteVector vector = new ByteVector();

			vector.Add(ByteVector.FromString(owner, StringType.Latin1));
			vector.Add(TextDelimiter(StringType.Latin1));
			vector.Add(data);

			return vector;
		}
		#endregion
		
		#region Protected Methods
		protected override void ParseFields(ByteVector data)
		{
			ParsePrivateFields(data);
		}

		protected override ByteVector RenderFields()
		{
			return RenderPrivateFields();
		}
		#endregion
		
		#region Public Properties
		public string Owner
		{
			get { return owner; }
		}

		public ByteVector PrivateData
		{
			get { return data; }
			set { data = value; }
		}
		#endregion
		
		#region Public Methods
		public override string ToString()
		{
			return owner;
		}
		#endregion
		
		#region Public Static Methods
		public static Id3v2PrivateFrame Find(Id3v2Tag tag, string owner)
		{
			if (tag != null)
			{
				foreach (Id3v2PrivateFrame frame in tag.GetFrames("PRIV"))
					if (frame != null && frame.Owner == owner)
						return frame;
				return null;
			}
			else throw new ArgumentNullException("tag");
		}
		#endregion
	}
}
