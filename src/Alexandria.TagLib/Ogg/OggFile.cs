/***************************************************************************
    copyright            : (C) 2006 by Dan Poage      dan.poage@gmail.com
    copyright            : (C) 2005 by Brian Nickel   brian.nickel@gmail.com
    based on             : oggfile.cpp from TagLib
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
using System.Collections.Generic;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public abstract class OggFile : File
	{
		#region Constructors
		protected OggFile(string file) : base(file)
		{
			ClearPageData();
		}
		#endregion
	
		#region Private Fields
		private uint streamSerialNumber;
		private List<OggPage> pages; //ArrayList  pages;
		private OggPageHeader firstPageHeader;
		private OggPageHeader lastPageHeader;
		private List<IntCollection> packetToPageMap;
		private Dictionary<uint, ByteVector> dirtyPackets; //Hashtable  dirtyPackets;
		private IntCollection dirtyPages;
		private OggPage currentPage; // The current page for the reader -- used by nextPage()
		private OggPage currentPacketPage; //! The current page for the packet parser -- used by packet()
		private ByteVectorCollection currentPackets; //! The packets for the currentPacketPage -- used by packet()
		#endregion

		#region Private Methods
		private bool NextPage()
		{
			long next_page_offset;
			int current_packet;

			if (pages.Count == 0)
			{
				current_packet = 0;
				next_page_offset = Find("OggS");
				if (next_page_offset < 0)
					return false;
			}
			else
			{
				if (currentPage.Header.LastPageOfStream)
					return false;

				if (currentPage.Header.LastPacketCompleted)
					current_packet = (int)(currentPage.FirstPacketIndex + currentPage.PacketCount);
				else
					current_packet = (int)(currentPage.FirstPacketIndex + currentPage.PacketCount - 1);

				next_page_offset = currentPage.FileOffset + currentPage.Size;
			}

			// Read the next page and add it to the page text.

			currentPage = new OggPage(this, next_page_offset);

			if (!currentPage.Header.IsValid)
			{
				currentPage = null;
				return false;
			}

			currentPage.FirstPacketIndex = current_packet;

			if (pages.Count == 0)
				streamSerialNumber = currentPage.Header.StreamSerialNumber;

			pages.Add(currentPage);

			// Loop through the packets in the page that we just read appending the
			// current page number to the packet to page map for each packet.

			for (int i = 0; i < currentPage.PacketCount; i++)
			{
				current_packet = currentPage.FirstPacketIndex + i;
				if (packetToPageMap.Count <= current_packet)
					packetToPageMap.Add(new IntCollection());
				((IntCollection)packetToPageMap[current_packet]).Add(pages.Count - 1);
			}

			return true;

		}

		private void WritePageGroup(IntCollection page_group)
		{
			if (page_group.IsEmpty)
				return;

			ByteVectorCollection packets = new ByteVectorCollection();

			// If the first page of the group isn'type dirty, append its partial content here.

			if (!dirtyPages.Contains(((OggPage)this.pages[page_group[0]]).FirstPacketIndex))
				packets.Add(((OggPage)this.pages[page_group[0]]).Packets[0]);

			int previous_packet = -1;
			int original_size = 0;

			for (int i = 0; i < page_group.Count; i++)
			{
				int page = page_group[i];

				uint first_packet = (uint)((OggPage)this.pages[page]).FirstPacketIndex;
				uint last_packet = first_packet + ((OggPage)this.pages[page]).PacketCount - 1;

				for (uint j = first_packet; j <= last_packet; j++)
				{

					if (i == page_group.Count - 1 && j == last_packet && !dirtyPages.Contains((int)j))
						packets.Add(((OggPage)this.pages[page]).Packets[((OggPage)this.pages[page]).Packets.Count - 1]);
					else if ((int)j != previous_packet)
					{
						previous_packet = (int)j;
						packets.Add(GetPacket(j));
					}
				}
				original_size += ((OggPage)this.pages[page]).Size;
			}

			bool continued = ((OggPage)this.pages[page_group[0]]).Header.FirstPacketContinued;
			bool completed = ((OggPage)this.pages[page_group[page_group.Count - 1]]).Header.LastPacketCompleted;

			// TODO: This pagination method isn'type accurate for what'field being done here.
			// This should account for real possibilities like non-aligned packets and such.

			OggPage[] pages = OggPage.Paginate(packets, PaginationStrategy.SinglePagePerGroup,
										   streamSerialNumber, page_group[0],
										   continued, completed);

			ByteVector data = new ByteVector();

			foreach (OggPage p in pages)
				data.Add(p.Render());

			// The insertion algorithms could also be improve to queue and prioritize data
			// on the way out.  Currently it requires rewriting the file for every page
			// group rather than just once; however, for tagging applications there will
			// generally only be one page group, so it'field not worth the time for the
			// optimization at the moment.

			Insert(data, ((OggPage)this.pages[page_group[0]]).FileOffset, original_size);

			// Update the page index to include the pages we just created and to delete the
			// old pages.

			foreach (OggPage p in pages)
			{
				int index = p.Header.PageSequenceNumber;
				this.pages[index] = p;
			}
		}		
		#endregion

		#region Protected Methods
		protected void ClearPageData()
		{
			streamSerialNumber = 0;
			pages = new List<OggPage>(); //new ArrayList ();
			firstPageHeader = null;
			lastPageHeader = null;
			packetToPageMap = new List<IntCollection>(); //new ArrayList();
			dirtyPackets = new Dictionary<uint, ByteVector>(); //new Hashtable ();
			dirtyPages = new IntCollection();
			currentPage = null;
			currentPacketPage = null;
			currentPackets = null;
		}
		#endregion
      
		#region Public Properties
		public OggPageHeader FirstPageHeader
		{
			get
			{
				if (firstPageHeader == null)
				{
					long first_page_header_offset = Find ("OggS");

					if (first_page_header_offset < 0)
						return null;

					firstPageHeader = new OggPageHeader (this, first_page_header_offset);
				}
            
				return firstPageHeader.IsValid ? firstPageHeader : null;
			}
		}
      
		public OggPageHeader LastPageHeader
		{
			get
			{
				if (lastPageHeader == null)
				{
					long last_page_header_offset = RFind ("OggS");

					if(last_page_header_offset < 0)
						return null;

					lastPageHeader = new OggPageHeader (this, last_page_header_offset);
				}
				
				return lastPageHeader.IsValid ? lastPageHeader : null;
			}
		}
		#endregion

		#region Public Methods
		[System.CLSCompliant(false)]
		public ByteVector GetPacket(uint index)
		{
			// Check to see if we're called setPacket() for this packet since the last
			// save:

			if (dirtyPackets.ContainsKey(index))
				return dirtyPackets[index];

			// If we haven'type indexed the page where the packet we're interested in starts,
			// begin reading pages until we have.

			while (packetToPageMap.Count <= index)
				if (!NextPage())
				{
					TagLibDebugger.Debug("Ogg.File.Packet() -- Could not find the requested packet.");
					return null;
				}

			// Start reading at the first page that contains part (or all) of this packet.
			// If the last read stopped at the packet that we're interested in, don'type
			// reread its packet text.  (This should make sequential packet reads fast.)

			int pageIndex = ((IntCollection)packetToPageMap[(int)index])[0];
			if (currentPacketPage != pages[pageIndex])
			{
				currentPacketPage = pages[pageIndex];
				currentPackets = currentPacketPage.Packets;
			}

			// If the packet is completely contained in the first page that it'field in, then
			// just return it now.

			if ((currentPacketPage.ContainsPacket((int)index) & ContainsPacketSettings.CompletePacket) != 0)
				return currentPackets[(int)(index - currentPacketPage.FirstPacketIndex)];

			// If the packet is *not* completely contained in the first page that it'field a
			// part of then that packet trails off the end of the page.  Continue appending
			// the pages' packet data until we hit a page that either does not end with the
			// packet that we're fetching or where the last packet is complete.

			ByteVector packet = currentPackets[currentPackets.Count - 1];
			while ((currentPacketPage.ContainsPacket((int)index) & ContainsPacketSettings.EndsWithPacket) != 0
				&& !currentPacketPage.Header.LastPacketCompleted)
			{
				pageIndex++;
				if (pageIndex == pages.Count && !NextPage())
				{
					TagLibDebugger.Debug("Ogg.File.Packet() -- Could not find the requested packet.");
					return null;
				}

				currentPacketPage = (OggPage)pages[pageIndex];
				currentPackets = currentPacketPage.Packets;
				packet.Add(currentPackets[0]);
			}

			return packet;
		}

		[System.CLSCompliant(false)]
		public void SetPacket(uint index, ByteVector packet)
		{
			while (packetToPageMap.Count <= index)
				if (!NextPage())
				{
					TagLibDebugger.Debug("Ogg.File.SetPacket() -- Could not set the requested packet.");
					return;
				}

			foreach (int page in (IntCollection)packetToPageMap[(int)index])
				dirtyPages.SortedInsert(page, true);

			if (dirtyPackets.ContainsKey(index))
				dirtyPackets[index] = packet;
			else
				dirtyPackets.Add(index, packet);
		}

		public override void Save()
		{
			if (IsReadOnly)
			{
				throw new ReadOnlyException();
			}

			Mode = FileAccessMode.Write;

			IntCollection pageGroup = new IntCollection();

			foreach (int page in dirtyPages)
				if (!pageGroup.IsEmpty && pageGroup[pageGroup.Count - 1] + 1 != page)
				{
					WritePageGroup(pageGroup);
					pageGroup.Clear();
				}
				else
					pageGroup.Add(page);

			WritePageGroup(pageGroup);
			dirtyPages.Clear();
			dirtyPackets.Clear();

			Mode = FileAccessMode.Closed;
		}
		#endregion
	}
}
