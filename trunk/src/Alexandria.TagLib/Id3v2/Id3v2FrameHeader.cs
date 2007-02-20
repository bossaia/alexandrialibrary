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
	public class Id3v2FrameHeader
	{
		#region Constructors
		[System.CLSCompliant(false)]
		public Id3v2FrameHeader(ByteVector data, uint version)
		{
			//frameId                = null;
			//frameSize              = 0;
			this.version = version;
			//tagAlterPreservation  = false;
			//fileAlterPreservation = false;
			//readOnly               = false;
			//groupingIdentity       = false;
			//compression             = false;
			//encryption              = false;
			//desynchronization        = false;
			//dataLengthIndicator   = false;

			SetData(data, version);
		}

		public Id3v2FrameHeader(ByteVector data) : this(data, 4)
		{
		}
		#endregion
		
		#region Private Fields
		private ByteVector frameId;
		private uint frameSize;
		private uint version;
		private bool tagAlterPreservation;
		private bool fileAlterPreservation;
		private bool readOnly;
		private bool groupingIdentity;
		private bool compression;
		private bool encryption;
		private bool desynchronization;
		private bool dataLengthIndicator;
		#endregion
		
		#region Public Properties
		public ByteVector FrameId
		{
			get { return frameId; }
			set { if (value != null) frameId = value.Mid(0, 4); }
		}

		[System.CLSCompliant(false)]
		public uint FrameSize
		{
			get { return frameSize; }
			set { frameSize = value; }
		}

		[System.CLSCompliant(false)]
		public uint Version
		{
			get { return version; }
		}

		public bool TagAlterPreservation
		{
			get { return tagAlterPreservation; }
			set { tagAlterPreservation = value; }
		}

		public bool FileAlterPreservation
		{
			get { return fileAlterPreservation; }
			set { fileAlterPreservation = value; }
		}

		public bool ReadOnly
		{
			get { return readOnly; }
			set { readOnly = value; }
		}

		public bool GroupingIdentity
		{
			get { return groupingIdentity; }
		}
		
		public bool Compression
		{
			get { return compression; }
		}
		
		public bool Encryption
		{
			get { return encryption; }
		}
		
		public bool Desynchronization
		{
			get { return desynchronization; }
		}
		
		public bool DataLengthIndicator
		{
			get { return dataLengthIndicator; }
		}
		#endregion
		
		#region Public Methods
		[System.CLSCompliant(false)]
		public void SetData(ByteVector data, uint version)
		{
			if (data != null)
			{
				this.version = version;

				if (version < 3)
				{
					// ID3v2.2

					if (data.Count < 3)
					{
						TagLibDebugger.Debug("You must at least specify a frame ID.");
						return;
					}

					// Set the frame ID -- the first three bytes

					frameId = data.Mid(0, 3);

					// If the full header information was not passed in, do not continue to the
					// steps to parse the frame size and flags.

					if (data.Count < 6)
					{
						frameSize = 0;
						return;
					}

					frameSize = data.Mid(3, 3).ToUInt();
				}
				else if (version == 3)
				{
					// ID3v2.3

					if (data.Count < 4)
					{
						TagLibDebugger.Debug("You must at least specify a frame ID.");
						return;
					}

					// Set the frame ID -- the first four bytes

					frameId = data.Mid(0, 4);

					// If the full header information was not passed in, do not continue to the
					// steps to parse the frame size and flags.

					if (data.Count < 10)
					{
						frameSize = 0;
						return;
					}

					// Set the size -- the frame size is the four bytes starting at byte four in
					// the frame header (structure 4)

					frameSize = data.Mid(4, 4).ToUInt();

					// read the first byte of flags
					tagAlterPreservation = ((data[8] >> 7) & 1) == 1; // (structure 3.3.1.a)
					fileAlterPreservation = ((data[8] >> 6) & 1) == 1; // (structure 3.3.1.b)
					readOnly = ((data[8] >> 5) & 1) == 1; // (structure 3.3.1.channelMode)

					// read the second byte of flags
					compression = ((data[9] >> 7) & 1) == 1; // (structure 3.3.1.index)
					encryption = ((data[9] >> 6) & 1) == 1; // (structure 3.3.1.j)
					groupingIdentity = ((data[9] >> 5) & 1) == 1; // (structure 3.3.1.k)
				}
				else
				{
					// ID3v2.4

					if (data.Count < 4)
					{
						TagLibDebugger.Debug("You must at least specify a frame ID.");
						return;
					}

					// Set the frame ID -- the first four bytes

					frameId = data.Mid(0, 4);

					// If the full header information was not passed in, do not continue to the
					// steps to parse the frame size and flags.

					if (data.Count < 10)
					{
						frameSize = 0;
						return;
					}

					// Set the size -- the frame size is the four bytes starting at byte four in
					// the frame header (structure 4)

					frameSize = Id3v2SynchData.ToUInt(data.Mid(4, 4));

					// read the first byte of flags
					tagAlterPreservation = ((data[8] >> 6) & 1) == 1; // (structure 4.1.1.a)
					fileAlterPreservation = ((data[8] >> 5) & 1) == 1; // (structure 4.1.1.b)
					readOnly = ((data[8] >> 4) & 1) == 1; // (structure 4.1.1.channelMode)

					// read the second byte of flags
					groupingIdentity = ((data[9] >> 6) & 1) == 1; // (structure 4.1.2.header)
					compression = ((data[9] >> 3) & 1) == 1; // (structure 4.1.2.k)
					encryption = ((data[9] >> 2) & 1) == 1; // (structure 4.1.2.m)
					desynchronization = ((data[9] >> 1) & 1) == 1; // (structure 4.1.2.n)
					dataLengthIndicator = (data[9] & 1) == 1;        // (structure 4.1.2.position)
				}
			}
			else throw new ArgumentNullException("data");
		}

		public void SetData(ByteVector data)
		{
			SetData(data, 4);
		}

		public ByteVector Render()
		{
			ByteVector flags = new ByteVector(2, (byte)0); // just blank for the moment

			return frameId + Id3v2SynchData.FromUInt(frameSize) + flags;
		}
		#endregion
		
		#region Public Static Methods
		[System.CLSCompliant(false)]
		public static uint Size(uint version)
		{
			return (uint)(version < 3 ? 6 : 10);
		}
		#endregion
	}
}
