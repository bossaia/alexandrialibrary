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
	public class ApeFooter
	{
		#region Constructors
		public ApeFooter()
		{
			//version = 0;
			footerPresent = true;
			//headerPresent = false;
			//isHeader = false;
			//itemCount = 0;
			//tagSize = 0;
		}

		public ApeFooter(ByteVector data) : this()
		{
			Parse(data);
		}
		#endregion
		
		#region Private Fields
		private uint version;
		private bool footerPresent;
		private bool headerPresent;
		private bool isHeader;
		private uint itemCount;
		private uint tagSize;
		private static uint size = 32;
		#endregion
	
		#region Private Static Fields
		private static ByteVector fileIdentifier = "APETAGEX";
		#endregion
		
		#region Protected Methods
		protected void Parse(ByteVector data)
		{
			if (data != null)
			{
				if (data.Count < Size)
					return;

				// The first eight buffer, data[0..7], are the File Identifier, "APETAGEX".

				// Read the version number
				version = data.Mid(8, 4).ToUInt(false);

				// Read the tag size
				tagSize = data.Mid(12, 4).ToUInt(false);

				// Read the item count
				itemCount = data.Mid(16, 4).ToUInt(false);

				// Read the flags

				uint flags = data.Mid(20, 4).ToUInt(false);

				headerPresent = (flags >> 31) == 1;
				footerPresent = (flags >> 30) != 1;
				isHeader = (flags >> 29) == 1;
			}
			else throw new ArgumentNullException("data");
		}

		protected ByteVector Render(bool isHeader)
		{
			ByteVector vector = new ByteVector();

			// add the file identifier -- "APETAGEX"

			vector.Add(FileIdentifier);

			// add the version number -- we always render a 2.000 tag regardless of what
			// the tag originally was.
			vector.Add(ByteVector.FromUInt(2000, false));

			// add the tag size
			vector.Add(ByteVector.FromUInt(tagSize, false));

			// add the item count
			vector.Add(ByteVector.FromUInt(itemCount, false));

			// render and add the flags
			uint flags = 0;

			flags |= (uint)((HeaderPresent ? 1 : 0) << 31);
			// footer is always present
			flags |= (uint)((isHeader ? 1 : 0) << 29);

			vector.Add(ByteVector.FromUInt(flags, false));

			// add the reserved 64bit

			vector.Add(ByteVector.FromLong(0));

			return vector;
		}
		#endregion
		
		#region Public Properties
		[System.CLSCompliant(false)]
		public uint Version
		{
			get { return version; }
		}
		
		public bool FooterPresent
		{
			get { return footerPresent; }
		}
		
		public bool IsHeader
		{
			get { return isHeader; }
		}

		public bool HeaderPresent
		{
			get { return headerPresent; }
			set { headerPresent = value; }
		}

		[System.CLSCompliant(false)]
		public uint ItemCount
		{
			get { return itemCount; }
			set { itemCount = value; }
		}

		[System.CLSCompliant(false)]
		public uint TagSize
		{
			get { return tagSize; }
			set { tagSize = value; }
		}

		[System.CLSCompliant(false)]
		public uint CompleteTagSize
		{
			get { return TagSize + (HeaderPresent ? Size : 0); }
		}
		#endregion
		
		#region Public Methods
		public void SetData(ByteVector data)
		{
			Parse(data);
		}

		public ByteVector RenderFooter()
		{
			return Render(false);
		}

		public ByteVector RenderHeader()
		{
			return HeaderPresent ? Render(true) : new ByteVector();
		}
		#endregion
		
		#region Public Static Properties
		public static ByteVector FileIdentifier
		{
			get {return fileIdentifier;}
		}

		[System.CLSCompliant(false)]
		public static uint Size
		{
			get { return size; }
		}
		#endregion
	}
}
