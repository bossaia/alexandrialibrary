/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : id3v2framefactory.cpp from TagLib
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

using System.Collections;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public static class Id3v2FrameFactory
	{
		#region Private Static Fields
		private static StringType defaultEncoding = StringType.UTF8;
		private static bool useDefaultEncoding;
		private static ArrayList frameCreators = new ArrayList();
		#endregion
	
		#region Private Static Methods
		private static bool UpdateFrame(Id3v2FrameHeader header)
		{
			ByteVector frameId = header.FrameId;

			switch (header.Version)
			{
				case 2: // ID3v2.2
					{
						if (frameId == "CRM" ||
						   frameId == "EQU" ||
						   frameId == "LNK" ||
						   frameId == "RVA" ||
						   frameId == "TIM" ||
						   frameId == "TSI")
						{
							TagLibDebugger.Debug("ID3v2.4 no longer supports the frame type "
							+ frameId.ToString() + ".  It will be discarded from the tag.");

							return false;
						}

						// ID3v2.2 only used 3 buffer for the frame ID, so we need to convert all of
						// the frames to their 4 byte ID3v2.4 equivalent.

						ConvertFrame("BUF", "RBUF", header);
						ConvertFrame("CNT", "PCNT", header);
						ConvertFrame("COM", "COMM", header);
						ConvertFrame("CRA", "AENC", header);
						ConvertFrame("ETC", "ETCO", header);
						ConvertFrame("GEO", "GEOB", header);
						ConvertFrame("IPL", "TIPL", header);
						ConvertFrame("MCI", "MCDI", header);
						ConvertFrame("MLL", "MLLT", header);
						ConvertFrame("PIC", "APIC", header);
						ConvertFrame("POP", "POPM", header);
						ConvertFrame("REV", "RVRB", header);
						ConvertFrame("SLT", "SYLT", header);
						ConvertFrame("STC", "SYTC", header);
						ConvertFrame("TAL", "TALB", header);
						ConvertFrame("TBP", "TBPM", header);
						ConvertFrame("TCM", "TCOM", header);
						ConvertFrame("TCO", "TCON", header);
						ConvertFrame("TCR", "TCOP", header);
						ConvertFrame("TDA", "TDRC", header);
						ConvertFrame("TDY", "TDLY", header);
						ConvertFrame("TEN", "TENC", header);
						ConvertFrame("TFT", "TFLT", header);
						ConvertFrame("TKE", "TKEY", header);
						ConvertFrame("TLA", "TLAN", header);
						ConvertFrame("TLE", "TLEN", header);
						ConvertFrame("TMT", "TMED", header);
						ConvertFrame("TOA", "TOAL", header);
						ConvertFrame("TOF", "TOFN", header);
						ConvertFrame("TOL", "TOLY", header);
						ConvertFrame("TOR", "TDOR", header);
						ConvertFrame("TOT", "TOAL", header);
						ConvertFrame("TP1", "TPE1", header);
						ConvertFrame("TP2", "TPE2", header);
						ConvertFrame("TP3", "TPE3", header);
						ConvertFrame("TP4", "TPE4", header);
						ConvertFrame("TPA", "TPOS", header);
						ConvertFrame("TPB", "TPUB", header);
						ConvertFrame("TRC", "TSRC", header);
						ConvertFrame("TRD", "TDRC", header);
						ConvertFrame("TRK", "TRCK", header);
						ConvertFrame("TSS", "TSSE", header);
						ConvertFrame("TT1", "TIT1", header);
						ConvertFrame("TT2", "TIT2", header);
						ConvertFrame("TT3", "TIT3", header);
						ConvertFrame("TXT", "TOLY", header);
						ConvertFrame("TXX", "TXXX", header);
						ConvertFrame("TYE", "TDRC", header);
						ConvertFrame("UFI", "UFID", header);
						ConvertFrame("ULT", "USLT", header);
						ConvertFrame("WAF", "WOAF", header);
						ConvertFrame("WAR", "WOAR", header);
						ConvertFrame("WAS", "WOAS", header);
						ConvertFrame("WCM", "WCOM", header);
						ConvertFrame("WCP", "WCOP", header);
						ConvertFrame("WPB", "WPUB", header);
						ConvertFrame("WXX", "WXXX", header);

					}
					break;

				case 3: // ID3v2.3
					{
						if (frameId == "EQUA" ||
						   frameId == "RVAD" ||
						   frameId == "TIME" ||
						   frameId == "TRDA" ||
						   frameId == "TSIZ" ||
						   frameId == "TDAT")
						{
							TagLibDebugger.Debug("ID3v2.4 no longer supports the frame type "
							+ frameId.ToString() + ".  It will be discarded from the tag.");

							return false;
						}

						ConvertFrame("TORY", "TDOR", header);
						ConvertFrame("TYER", "TDRC", header);

					}
					break;

				default:
					{
						// This should catch a typo that existed in TagLib up to and including
						// version 1.1 where TRDC was used for the year rather than TDRC.

						ConvertFrame("TRDC", "TDRC", header);
					}
					break;
			}

			return true;
		}

		private static void ConvertFrame(string from, string to, Id3v2FrameHeader header)
		{
			if (header.FrameId != from)
				return;

			header.FrameId = to;
		}
		#endregion
	
		#region Public Static Properties
		public static StringType DefaultTextEncoding
		{
			get
			{
				return defaultEncoding;
			}
			set
			{
				useDefaultEncoding = true;
				defaultEncoding = value;
			}
		}
		#endregion
		
		#region Public Static Methods
		
		#region CreateFrame
		[System.CLSCompliant(false)]
		public static Id3v2Frame CreateFrame(ByteVector data, uint version)
		{
			Id3v2FrameHeader header = new Id3v2FrameHeader(data, version);
			ByteVector frameId = header.FrameId;
			// A quick sanity check -- make sure that the frameId is 4 uppercase
			// Latin1 characters.  Also make sure that there is data in the frame.

			if (frameId == null || frameId.Count != (version < 3 ? 3 : 4) || header.FrameSize < 0)
				return null;

			foreach (byte b in frameId)
			{
				char c = (char)b;
				if ((c < 'A' || c > 'Z') && (c < '1' || c > '9'))
					return null;
			}

			// Windows Media Player may create zero byte frames. Just send them
			// off as unknown.
			if (header.FrameSize == 0)
				return new Id3v2UnknownFrame(data, header);

			// TagLib doesn'type mess with encrypted frames, so just treat them
			// as unknown frames.

			if (header.Compression)
			{
				TagLibDebugger.Debug("Compressed frames are currently not supported.");
				return new Id3v2UnknownFrame(data, header);
			}

			if (header.Encryption)
			{
				TagLibDebugger.Debug("Encrypted frames are currently not supported.");
				return new Id3v2UnknownFrame(data, header);
			}

			if (!UpdateFrame(header))
			{
				header.TagAlterPreservation = true;
				return new Id3v2UnknownFrame(data, header);
			}

			foreach (FrameCreator creator in frameCreators)
			{
				Id3v2Frame frame = creator(data, header);
				if (frame != null)
					return frame;
			}


			// UpdateFrame() might have updated the frame ID.

			frameId = header.FrameId;

			// This is where things get necissarily nasty.  Here we determine which
			// Frame subclass (or if none is found simply an Frame) based
			// on the frame ID.  Since there are a lot of possibilities, that means
			// a lot of if blocks.

			// Text Identification (frames 4.2)

			if (frameId.StartsWith("T"))
			{
				Id3v2TextIdentificationFrame frame = frameId != "TXXX"
				? new Id3v2TextIdentificationFrame(data, header)
				: new Id3v2UserTextIdentificationFrame(data, header);

				if (useDefaultEncoding)
					frame.TextEncoding = defaultEncoding;

				return frame;
			}

			// Comments (frames 4.10)

			if (frameId == "COMM")
			{
				Id3v2CommentsFrame frame = new Id3v2CommentsFrame(data, header);

				if (useDefaultEncoding)
					frame.TextEncoding = defaultEncoding;

				return frame;
			}

			// Attached Picture (frames 4.14)

			if (frameId == "APIC")
			{
				Id3v2AttachedPictureFrame f = new Id3v2AttachedPictureFrame(data, header);

				if (useDefaultEncoding)
					f.TextEncoding = defaultEncoding;

				return f;
			}

			// Relative Volume Adjustment (frames 4.11)

			if (frameId == "RVA2")
				return new Id3v2RelativeVolumeFrame(data, header);

			// Unique File Identifier (frames 4.1)

			if (frameId == "UFID")
				return new Id3v2UniqueFileIdentifierFrame(data, header);

			// Private (frames 4.27)

			if (frameId == "PRIV")
				return new Id3v2PrivateFrame(data, header);

			return new Id3v2UnknownFrame(data, header);
		}
		#endregion
		
		#region AddFrameCreator
		public static void AddFrameCreator(FrameCreator creator)
		{
			if (creator != null)
				frameCreators.Insert(0, creator);
		}
		#endregion
		
		#endregion
	}
}
