/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : id3v2header.cpp from TagLib
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
	public class Id3v2Footer
	{
		#region Constructors
		public Id3v2Footer()
		{
			//majorVersion          = 0;
			//revisionNumber        = 0;
			//desynchronization      = false;
			//extendedHeader        = false;
			//experimentalIndicator = false;
			//tagSize               = 0;
		}

		public Id3v2Footer(ByteVector data) : this()
		{
			Parse(data);
		}

		public Id3v2Footer(Id3v2Header header) : this()
		{
			if (header != null)
			{
				majorVersion = header.MajorVersion;
				revisionNumber = header.RevisionNumber;
				desynchronization = header.Desynchronization;
				extendedHeader = header.ExtendedHeader;
				experimentalIndicator = header.ExperimentalIndicator;
				tagSize = header.TagSize;
			}
		}
		#endregion
		
		#region Private Fields
		private uint majorVersion;
		private uint revisionNumber;
		private bool desynchronization;
		private bool extendedHeader;
		private bool experimentalIndicator;
		private uint tagSize;
		#endregion
		
		#region Private Static Fields
		private static uint size = 10;
		private static ByteVector fileIdentifier = "3DI";
		#endregion

		#region Protected Methods
		protected void Parse(ByteVector data)
		{
			if (data.Count < Size)
				return;

			// do some sanity checking -- even in ID3v2.3.0 and less the tag size is a
			// synch-safe integer, so all bytes must be less than 128.  If this is not
			// true then this is an invalid tag.

			// note that we're doing things a little out of order here -- the size is
			// later in the bytestream than the version

			ByteVector sizeData = data.Mid(6, 4);

			if (sizeData.Count != 4)
			{
				tagSize = 0;
				TagLibDebugger.Debug("ID3v2.Footer.Parse () - The tag size as read was 0 bytes!");
				return;
			}

			foreach (byte b in sizeData)
			{
				if (b >= 128)
				{
					tagSize = 0;
					TagLibDebugger.Debug("ID3v2.Footer.Parse () - One of the size bytes in the id3v2 header was greater than the allowed 128.");
					return;
				}
			}

			// The first three bytes, data[0..2], are the File Identifier, "ID3". (structure 3.1 "file identifier")

			// Read the version number from the fourth and fifth bytes.
			majorVersion = data[3];   // (structure 3.1 "major version")
			revisionNumber = data[4]; // (structure 3.1 "revision number")

			// Read the flags, the first four bits of the sixth byte.
			byte flags = data[5];

			desynchronization = ((flags >> 7) & 1) == 1; // (structure 3.1.a)
			extendedHeader = ((flags >> 6) & 1) == 1; // (structure 3.1.b)
			experimentalIndicator = ((flags >> 5) & 1) == 1; // (structure 3.1.channelMode)

			// Get the size from the remaining four bytes (read above)

			tagSize = Id3v2SynchData.ToUInt(sizeData); // (structure 3.1 "size")
		}
		#endregion

		#region Public Properties
		[System.CLSCompliant(false)]
		public uint MajorVersion
		{
			get { return majorVersion; }
			set { majorVersion = value; }
		}

		[System.CLSCompliant(false)]
		public uint RevisionNumber
		{
			get { return revisionNumber; }
			set { revisionNumber = value; }
		}

		public bool Desynchronization
		{
			get { return desynchronization; }
			set { desynchronization = value; }
		}

		public bool ExtendedHeader
		{
			get { return extendedHeader; }
			set { extendedHeader = value; }
		}

		public bool ExperimentalIndicator
		{
			get { return experimentalIndicator; }
			set { experimentalIndicator = value; }
		}

		//public bool FooterPresent
		//{
			//get { return true; }
		//}

		[System.CLSCompliant(false)]
		public uint TagSize
		{
			get { return tagSize; }
			set { tagSize = value; }
		}

		[System.CLSCompliant(false)]
		public uint CompleteTagSize
		{
			get { return TagSize + Id3v2Header.Size + Size; }
		}
		#endregion

		#region Public Static Properties
		public static ByteVector FileIdentifier
		{
			get { return fileIdentifier; }
		}

		[System.CLSCompliant(false)]
		public static uint Size
		{
			get { return size; }
		}
		#endregion	
		
		#region Public Methods
		public void SetData(ByteVector data)
		{
			Parse(data);
		}

		public ByteVector Render()
		{
			ByteVector vector = new ByteVector();

			// add the file identifier -- "3DI"
			vector.Add(FileIdentifier);

			// add the version number -- we always render a 2.4.0 tag regardless of what
			// the tag originally was.
			vector.Add((byte)4);
			vector.Add((byte)0);

			// render and add the flags
			byte flags = 0;
			if (Desynchronization) flags |= 128;
			if (ExtendedHeader) flags |= 64;
			if (ExperimentalIndicator) flags |= 32;
			flags |= 16;
			vector.Add(flags);

			// add the size
			vector.Add(Id3v2SynchData.FromUInt(TagSize));

			return vector;
		}
		#endregion
	}
}
