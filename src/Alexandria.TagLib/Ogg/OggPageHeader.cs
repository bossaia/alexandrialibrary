/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : mpegheader.cpp from TagLib
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

namespace AlexandriaOrg.Alexandria.TagLib
{
	public class OggPageHeader
	{
		#region Constructors
		public OggPageHeader(OggFile file, long pageOffset)
		{
			//isValid                   = false;
			//packetSizes = new ArrayList();
			//firstPacketContinued     = false;
			//lastPacketCompleted      = false;
			//first_page_of_stream       = false;
			//last_page_of_stream        = false;
			//absolute_granular_position = 0;
			//streamSerialNumber       = 0;
			//pageSequenceNumber = -1;
			//size                       = 0;
			//data_size                  = 0;

			if (file != null && pageOffset >= 0)
				Read(file, pageOffset);
		}

		public OggPageHeader() : this(null, -1)
		{
		}
		#endregion
		
		#region Private Fields
		private bool isValid;
		private List<int> packetSizes = new List<int>();
		private bool firstPacketContinued;
		private bool lastPacketCompleted;
		private bool firstPageOfStream;
		private bool lastPageOfStream;
		private long absoluteGranularPosition;
		private uint streamSerialNumber;
		private int pageSequenceNumber = -1;
		private int size;
		private int dataSize;
		#endregion
		
		#region Private Properties
		private ByteVector LacingValues
		{
			get
			{
				ByteVector data = new ByteVector();

				IList<int> sizes = PacketSizes;

				for (int i = 0; i < sizes.Count; i++)
				{
					// The size of a packet in an Ogg page is indicated by a series of "lacing
					// values" where the sum of the values is the packet size in bytes.  Each of
					// these values is a byte.  A value of less than 255 (0xff) indicates the end
					// of the packet.

					int quot = sizes[i] / 255;
					int rem = sizes[i] % 255;

					for (int j = 0; j < quot; j++)
						data.Add((byte)255);

					if (i < sizes.Count - 1 || lastPacketCompleted)
						data.Add((byte)rem);
				}

				return data;
			}
		}
		#endregion
		
		#region Private Methods
		private void Read(OggFile file, long file_offset)
		{
			file.Seek(file_offset);

			// An Ogg page header is at least 27 bytes, so we'll go ahead and read that
			// much and then get the rest when we're ready for it.

			ByteVector data = file.ReadBlock(27);

			// Sanity check -- make sure that we were in fact able to read as much data as
			// we asked for and that the page begins with "OggS".

			if (data.Count != 27 || !data.StartsWith("OggS"))
			{
				TagLibDebugger.Debug("Ogg.PageHeader.Read() -- error reading page header");
				return;
			}

			byte flags = data[5];

			firstPacketContinued = (flags & 1) != 0;
			firstPageOfStream = ((flags >> 1) & 1) != 0;
			lastPageOfStream = ((flags >> 2) & 1) != 0;

			absoluteGranularPosition = data.Mid(6, 8).ToLong(false);
			streamSerialNumber = data.Mid(14, 4).ToUInt(false);
			pageSequenceNumber = (int)data.Mid(18, 4).ToUInt(false);

			// Byte number 27 is the number of page segments, which is the only variable
			// length portion of the page header.  After reading the number of page
			// segments we'll then read in the coresponding data for this count.

			int pageSegmentCount = data[26];

			ByteVector page_segments = file.ReadBlock(pageSegmentCount);

			// Another sanity check.

			if (pageSegmentCount < 1 || page_segments.Count != pageSegmentCount)
				return;

			// The base size of an Ogg page 27 bytes plus the number of lacing values.

			size = 27 + pageSegmentCount;

			int packetSize = 0;

			for (int i = 0; i < pageSegmentCount; i++)
			{
				dataSize += page_segments[i];
				packetSize += page_segments[i];

				if (page_segments[i] < 255)
				{
					packetSizes.Add(packetSize);
					packetSize = 0;
				}
			}

			if (packetSize > 0)
			{
				packetSizes.Add(packetSize);
				lastPacketCompleted = false;
			}
			else
				lastPacketCompleted = true;

			isValid = true;
		}
		#endregion
		
		#region Public Properties
		public bool IsValid
		{
			get { return isValid; }
		}

		public IList<int> PacketSizes
		{
			get { return packetSizes; }
		}

		public bool FirstPacketContinued
		{
			get { return firstPacketContinued; }
			set { firstPacketContinued = value; }
		}

		public bool LastPacketCompleted
		{
			get { return lastPacketCompleted; }
			set { lastPacketCompleted = value; }
		}

		public bool FirstPageOfStream
		{
			get { return firstPageOfStream; }
			set { firstPageOfStream = value; }
		}

		public bool LastPageOfStream
		{
			get { return lastPageOfStream; }
			set { lastPageOfStream = value; }
		}

		public long AbsoluteGranularPosition
		{
			get { return absoluteGranularPosition; }
			set { absoluteGranularPosition = value; }
		}

		public int PageSequenceNumber
		{
			get { return pageSequenceNumber; }
			set { pageSequenceNumber = value; }
		}

		[System.CLSCompliant(false)]
		public uint StreamSerialNumber
		{
			get { return streamSerialNumber; }
			set { streamSerialNumber = value; }
		}

		public int Size
		{
			get { return size; }
		}
		
		public int DataSize
		{
			get { return dataSize; }
		}
		#endregion
		
		#region Public Methods
		public ByteVector Render()
		{
			ByteVector data = new ByteVector();

			// capture patern
			data.Add("OggS");

			// stream structure version
			data.Add(0);

			// header type flag
			byte flags = 0;
			if (firstPacketContinued) flags |= 1;
			if (pageSequenceNumber == 0) flags |= 2;
			if (lastPageOfStream) flags |= 4;

			data.Add(flags);

			// absolute granular position
			data.Add(ByteVector.FromLong(absoluteGranularPosition, false));

			// stream serial number
			data.Add(ByteVector.FromUInt(streamSerialNumber, false));

			// page sequence number
			data.Add(ByteVector.FromUInt((uint)pageSequenceNumber, false));

			// checksum -- this is left empty and should be filled in by the Ogg::Page
			// class
			data.Add(new ByteVector(4, 0));

			// page segment count and page segment table
			ByteVector page_segments = LacingValues;

			data.Add((byte)page_segments.Count);
			data.Add(page_segments);

			return data;
		}
		
		public void SetPacketSizes(int[] packetSizes)
		{
			this.packetSizes.Clear();
			foreach(int packetSize in packetSizes)
				this.packetSizes.Add(packetSize);
		}
		#endregion
	}
}
