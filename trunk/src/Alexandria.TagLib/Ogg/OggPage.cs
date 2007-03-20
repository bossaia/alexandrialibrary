/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : oggpage.cpp from TagLib
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
   public class OggPage
   {

      
      
      //////////////////////////////////////////////////////////////////////////
      // private properties
      //////////////////////////////////////////////////////////////////////////
      private OggFile           file;
      private long           fileOffset;
      private long           packetOffset;
      private int            dataSize;
      private OggPageHeader     header;
      private int            firstPacketIndex;
      private ByteVectorCollection packets;
      
      
      //////////////////////////////////////////////////////////////////////////
      // public methods
      //////////////////////////////////////////////////////////////////////////
		public OggPage(OggFile file, long pageOffset)
		{
			this.file = file;
			this.fileOffset = pageOffset;
			//packetOffset = 0;
			//dataSize = 0;
			header = new OggPageHeader(file, pageOffset);
			firstPacketIndex = -1;
			packets = new ByteVectorCollection();
         
			if (file != null)
			{
				packetOffset = fileOffset + header.Size;
				dataSize = header.DataSize;
			}
		}
      
		public ContainsPacketSettings ContainsPacket(int index)
		{
			ContainsPacketSettings settings = ContainsPacketSettings.None;

			int last_packet_index = (int) (firstPacketIndex + PacketCount - 1);
			if (index < firstPacketIndex || index > last_packet_index)
			return settings;

			if (index == firstPacketIndex)
			settings |= ContainsPacketSettings.BeginsWithPacket;

			if (index == last_packet_index)
			settings |= ContainsPacketSettings.EndsWithPacket;

			// If there'field only one page and it'field complete:

			if (PacketCount == 1 && !header.FirstPacketContinued && header.LastPacketCompleted)
			settings |= ContainsPacketSettings.CompletePacket;

			// Or if the page is (a) the first page and it'field complete or (b) the last page
			// and it'field complete or (channelMode) a page in the middle.

			else if (((settings & ContainsPacketSettings.BeginsWithPacket) != 0 && !header.FirstPacketContinued) ||
				((settings & ContainsPacketSettings.EndsWithPacket) != 0 && header.LastPacketCompleted) ||
				((settings & ContainsPacketSettings.BeginsWithPacket) == 0 && (settings & ContainsPacketSettings.EndsWithPacket) == 0))
				settings |= ContainsPacketSettings.CompletePacket;
         
			return settings;
		}
      
		public ByteVector Render()
		{
			ByteVector data = header.Render();

			if (packets.IsEmpty)
			{
				if (file != null)
				{
					file.Seek(packetOffset);
					data.Add(file.ReadBlock (dataSize));
				}
				else
					TagLibDebugger.Debug ("Ogg.Page.Render() -- this page is empty!");
			}
			else
				foreach (ByteVector v in packets)
					data.Add(v);

			// Compute and set the checksum for the Ogg page.  The checksum is taken over
			// the entire page with the 4 buffer reserved for the checksum zeroed and then
			// inserted in buffer 22-25 of the page header.

			ByteVector checksum = ByteVector.FromUInt (data.Checksum, false);
			for (int i = 0; i < 4; i++)
				data [i + 22] = checksum [i];

			return data;
		}

		[System.CLSCompliant(false)]
		public static OggPage[] Paginate(ByteVectorCollection packets, PaginationStrategy strategy, uint streamSerialNumber,
			int firstPage, bool firstPacketContinued, bool lastPacketCompleted, bool containsLastPacket)
		{
			ArrayList l = new ArrayList();

			int totalSize = 0;
         
			foreach (ByteVector b in packets)
				totalSize += b.Count;

			if (strategy == PaginationStrategy.Repaginate || totalSize + packets.Count > 255 * 256)
			{
				TagLibDebugger.Debug ("Ogg.Page.Paginate() -- Sorry!  Repagination is not yet implemented.");
				return (OggPage[]) l.ToArray(typeof(OggPage));
			}

			// TODO: Handle creation of multiple pages here with appropriate pagination.

			OggPage p = new OggPage(packets, streamSerialNumber, firstPage, firstPacketContinued,
                            lastPacketCompleted, containsLastPacket);
			l.Add (p);

			return (OggPage[]) l.ToArray(typeof(OggPage));
		}

		[System.CLSCompliant(false)]
		public static OggPage[] Paginate(ByteVectorCollection packets, PaginationStrategy strategy,
			uint streamSerialNumber, int firstPage, bool firstPacketContinued, bool lastPacketCompleted)
		{
			return Paginate(packets, strategy, streamSerialNumber, firstPage,
				firstPacketContinued, lastPacketCompleted, false);
		}

		[System.CLSCompliant(false)]      
		public static OggPage[] Paginate(ByteVectorCollection packets, PaginationStrategy strategy, uint streamSerialNumber,
			int firstPage, bool firstPacketContinued)
		{
			return Paginate(packets, strategy, streamSerialNumber, firstPage,
				firstPacketContinued, true);
		}

		[System.CLSCompliant(false)]
		public static OggPage[] Paginate(ByteVectorCollection packets, PaginationStrategy strategy, uint streamSerialNumber,
			int firstPage)
		{
			return Paginate(packets, strategy, streamSerialNumber, firstPage, false);
		}
      
      
      //////////////////////////////////////////////////////////////////////////
      // public properties
      //////////////////////////////////////////////////////////////////////////
      public long FileOffset {get {return fileOffset;}}
      
      public OggPageHeader Header {get {return header;}}
      
      public int FirstPacketIndex
      {
         get {return firstPacketIndex;}
         set {firstPacketIndex = value;}
      }

	   [System.CLSCompliant(false)]
      public uint PacketCount {get {return (uint) header.PacketSizes.Count;}}
      
      public ByteVectorCollection Packets
      {
         get
         {
            if (!packets.IsEmpty)
               return packets;

            ByteVectorCollection l = new ByteVectorCollection ();

            if (file != null && header.IsValid)
            {
               file.Seek (packetOffset);

               foreach (int packetSize in header.PacketSizes)
                  l.Add (file.ReadBlock (packetSize));
            }
            else
               TagLibDebugger.Debug ("Ogg.Page.Packets -- attempting to read packets from an invalid page.");

            return l;
         }
      }
      
      public int Size {get {return header.Size + header.DataSize;}}
      
      
      //////////////////////////////////////////////////////////////////////////
      // private methods
      //////////////////////////////////////////////////////////////////////////
      
      
      //////////////////////////////////////////////////////////////////////////
      // private properties
      //////////////////////////////////////////////////////////////////////////

	   [System.CLSCompliant(false)]
      protected OggPage (ByteVectorCollection packets,     uint stream_serial_number,
                      int page_number,            bool first_packet_continued,
                      bool last_packet_completed, bool contains_last_packet)
      {
         //file = null;
         fileOffset = -1;
         //packetOffset = 0;
         //dataSize = 0;
         header = new OggPageHeader();
         firstPacketIndex = -1;
         this.packets = packets;

         ByteVector data = new ByteVector();
         ArrayList packet_sizes = new ArrayList();

         header.FirstPageOfStream    = (page_number == 0 && !first_packet_continued);
         header.LastPageOfStream     = contains_last_packet;
         header.FirstPacketContinued = first_packet_continued;
         header.LastPacketCompleted  = last_packet_completed;
         header.StreamSerialNumber   = stream_serial_number;
         header.PageSequenceNumber   = page_number;

         // Build a page from the text of packets.
         foreach (ByteVector v in packets)
         {
            packet_sizes.Add (v.Count);
            data.Add (v);
         }
         
         header.SetPacketSizes((int[]) packet_sizes.ToArray(typeof(int)));
      }

	   [System.CLSCompliant(false)]
      protected OggPage (ByteVectorCollection packets,     uint stream_serial_number,
                        int page_number,            bool first_packet_continued,
                        bool last_packet_completed)
               : this (packets, stream_serial_number, page_number,
                        first_packet_continued, last_packet_completed, false)
      {
      }

	   [System.CLSCompliant(false)]
      protected OggPage (ByteVectorCollection packets,     uint stream_serial_number,
                        int page_number,            bool first_packet_continued)
               : this (packets, stream_serial_number, page_number,
                        first_packet_continued, true)
      {
      }

	   [System.CLSCompliant(false)]
      protected OggPage (ByteVectorCollection packets,     uint stream_serial_number,
                        int page_number)
               : this (packets, stream_serial_number, page_number, false)
      {
      }
   }
}
