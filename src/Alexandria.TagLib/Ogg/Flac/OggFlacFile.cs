/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
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

namespace AlexandriaOrg.Alexandria.TagLib
{
	public abstract class OggFlacFile : OggFile
	{
		#region Constructors
		protected OggFlacFile(string file, ReadStyle propertiesStyle) : base(file)
		{
			//comment = null;
			//properties = null;
			//stream_info_data = null;
			//xiph_comment_data = null;
			//stream_start = 0;
			//stream_length = 0;
			//scanned = false;
			//has_xiph_comment = false;
			//comment_packet = 0;

			try
			{
				Mode = FileAccessMode.Read;
			}
			catch (TagLibException)
			{
				scanned = true;
				return;
			}

			Read(propertiesStyle);

			Mode = FileAccessMode.Closed;
		}

		protected OggFlacFile(string file) : this(file, ReadStyle.Average)
		{
		}		
		#endregion
		
		#region Private Fields
		private OggXiphComment comment;
		private AudioProperties properties;
		private ByteVector stream_info_data;
		private ByteVector xiph_comment_data;
		private long stream_start;
		private long stream_length;
		private bool scanned;
		private bool has_xiph_comment;
		private uint comment_packet;
		#endregion
      
		#region Private Properties
		private ByteVector StreamInfoData
		{
			get
			{
				Scan();
				return stream_info_data;
			}
		}

		private ByteVector XiphCommentData
		{
			get
			{
				Scan();
				return xiph_comment_data;
			}
		}
		#endregion
		
		#region Private Methods
		private void Read(ReadStyle properties_style)
		{
			// Look for FLAC metadata, including vorbis comments

			Scan();

			if (!scanned)
			{
				SetValid(false);
				return;
			}


			if (has_xiph_comment)
				comment = new OggXiphComment(XiphCommentData);
			else
				comment = new OggXiphComment();


			if (properties_style != ReadStyle.None)
				properties = new TagLib.FlacProperties(StreamInfoData, StreamLength, properties_style);
		}


		private void Scan()
		{
			// Scan the metadata pages

			if (scanned || !IsValid)
				return;

			uint ipacket = 0;
			long overhead = 0;

			ByteVector metadata_header = GetPacket(ipacket++);
			if (metadata_header == null)
				return;

			ByteVector header;

			if (!metadata_header.StartsWith("fLaC"))
			{
				// FLAC 1.1.2+
				if (metadata_header.Mid(1, 4) != "FLAC")
					return;

				if (metadata_header[5] != 1)
					return; // not version 1

				metadata_header = metadata_header.Mid(13);
			}
			else
			{
				// FLAC 1.1.0 & 1.1.1
				metadata_header = GetPacket(ipacket++);

				if (metadata_header == null)
					return;

			}

			header = metadata_header.Mid(0, 4);
			// Header format (from spec):
			// <1> Last-metadata-block flag
			// <7> BLOCK_TYPE
			//    0 : STREAMINFO
			//    1 : PADDING
			//    ..
			//    4 : VORBIS_COMMENT
			//    ..
			// <24> Length of metadata to follow

			byte block_type = (byte)(header[0] & 0x7f);
			bool last_block = (header[0] & 0x80) != 0;
			uint length = header.Mid(1, 3).ToUInt();
			overhead += length;

			// Sanity: First block should be the stream_info metadata

			if (block_type != 0)
			{
				TagLibDebugger.Debug("Ogg.Flac.File.Scan() -- Invalid Ogg/FLAC stream");
				return;
			}

			stream_info_data = metadata_header.Mid(4, (int)length);

			// Search through the remaining metadata

			while (!last_block)
			{
				metadata_header = GetPacket(ipacket++);

				if (metadata_header == null)
					return;

				header = metadata_header.Mid(0, 4);
				block_type = (byte)(header[0] & 0x7f);
				last_block = (header[0] & 0x80) != 0;
				length = header.Mid(1, 3).ToUInt();
				overhead += length;

				if (block_type == 4)
				{
					xiph_comment_data = metadata_header.Mid(4, (int)length);
					has_xiph_comment = true;
					comment_packet = ipacket;
				}
				else if (block_type > 5)
					TagLibDebugger.Debug("Ogg.Flac.File.Scan() -- Unknown metadata block");

			}

			// End of metadata, now comes the datastream
			stream_start = overhead;
			stream_length = Length - stream_start;

			scanned = true;
		}
		#endregion
		
		#region Public Properties
		public override TagLib.Tag Tag
		{
			get {return comment;}
		}

		public override AudioProperties AudioProperties
		{
			get {return properties;}
		}

		public long StreamLength
		{
			get
			{
				Scan();
				return stream_length;
			}
		}
		#endregion
		
		#region Public Methods
		public override void Save()
		{
			ClearPageData(); // Force re-reading of the file.

			xiph_comment_data = comment.Render();

			// Create FLAC metadata-block:

			// Put the size in the first 32 bit (I assume no more than 24 bit are used)

			ByteVector v = ByteVector.FromUInt((uint)xiph_comment_data.Count);

			// Set the type of the metadata-block to be a Xiph / Vorbis comment

			v[0] = 4;

			// Append the comment-data after the 32 bit header

			v.Add(xiph_comment_data);

			// Save the packet at the old spot
			// FIXME: Use padding if size is increasing

			SetPacket(comment_packet, v);

			base.Save();
		}
		#endregion
	}
}
