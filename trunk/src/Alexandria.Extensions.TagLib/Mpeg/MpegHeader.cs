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
using System.Collections;

namespace Alexandria.TagLib
{
	public class MpegHeader
	{
		#region Constructors
		public MpegHeader(ByteVector data)
		{
			//isValid           = false;
			version = MpegVersion.One;
			//layer              = 0;
			//protectionEnabled = false;
			//bitrate            = 0;
			//sampleRate        = 0;
			//isPadded          = false;
			channelMode = MpegChannelMode.Stereo;
			//isCopyrighted     = false;
			//isOriginal        = false;
			//frameLength       = 0;

			Parse(data);
		}

		public MpegHeader(MpegHeader header)
		{
			if (header != null)
			{
				isValid = header.IsValid;
				version = header.Version;
				layer = header.Layer;
				protectionEnabled = header.ProtectionEnabled;
				bitrate = header.Bitrate;
				sampleRate = header.SampleRate;
				isPadded = header.IsPadded;
				channelMode = header.ChannelMode;
				isCopyrighted = header.IsCopyrighted;
				isOriginal = header.IsOriginal;
				frameLength = header.FrameLength;
			}
		}
		#endregion
		
		#region Private Fields
		private bool isValid;
		private MpegVersion version;
		private int layer;
		private bool protectionEnabled;
		private int bitrate;
		private int sampleRate;
		private bool isPadded;
		private MpegChannelMode channelMode;
		private bool isCopyrighted;
		private bool isOriginal;
		private int frameLength;
		#endregion
		
		#region Private Methods
		private void Parse(ByteVector data)
		{
			if (data.Count < 4 || data[0] != 0xff)
			{
				TagLibDebugger.Debug("Mpeg.Header.Parse() -- First byte did not match MPEG synch.");
				return;
			}

			uint flags = data.ToUInt();

			// Check for the second byte'field part of the MPEG synch

			if ((flags & 0xFFE00000) != 0xFFE00000)
			{
				TagLibDebugger.Debug("Mpeg.Header.Parse() -- Second byte did not match MPEG synch.");
				return;
			}

			// Set the MPEG version
			switch ((flags >> 19) & 0x03)
			{
				case 0: version = MpegVersion.TwoPointFive; break;
				case 2: version = MpegVersion.Two; break;
				case 3: version = MpegVersion.One; break;
			}

			// Set the MPEG layer
			switch ((flags >> 17) & 0x03)
			{
				case 1: layer = 3; break;
				case 2: layer = 2; break;
				case 3: layer = 1; break;
			}

			protectionEnabled = ((flags >> 16) & 1) == 0;

			// Set the bitrate
			int[, ,] bitrates = new int[2, 3, 16] {
            { // Version 1
               { 0, 32, 64, 96, 128, 160, 192, 224, 256, 288, 320, 352, 384, 416, 448, 0 }, // layer 1
               { 0, 32, 48, 56, 64,  80,  96,  112, 128, 160, 192, 224, 256, 320, 384, 0 }, // layer 2
               { 0, 32, 40, 48, 56,  64,  80,  96,  112, 128, 160, 192, 224, 256, 320, 0 }  // layer 3
            },
            { // Version 2 or 2.5
               { 0, 32, 48, 56, 64, 80, 96, 112, 128, 144, 160, 176, 192, 224, 256, 0 }, // layer 1
               { 0, 8,  16, 24, 32, 40, 48, 56,  64,  80,  96,  112, 128, 144, 160, 0 }, // layer 2
               { 0, 8,  16, 24, 32, 40, 48, 56,  64,  80,  96,  112, 128, 144, 160, 0 }  // layer 3
            }
         };

			int versionIndex = version == MpegVersion.One ? 0 : 1;
			int layerIndex = layer > 0 ? layer - 1 : 0;

			// The bitrate index is encoded as the first 4 bits of the 3rd byte,
			// index.e. 1111xxxx

			int i = (int)(flags >> 12) & 0x0F;

			bitrate = bitrates[versionIndex, layerIndex, i];

			// Set the sample rate

			int[,] sampleRates = new int[3, 4] {
            { 44100, 48000, 32000, 0 }, // Version 1
            { 22050, 24000, 16000, 0 }, // Version 2
            { 11025, 12000, 8000,  0 }  // Version 2.5
         };

			// The sample rate index is encoded as two bits in the 3nd byte,
			// index.e. xxxx11xx
			i = (int)(flags >> 10) & 0x03;

			sampleRate = sampleRates[(int)version, i];

			if (sampleRate == 0)
			{
				TagLibDebugger.Debug("Mpeg.Header.Parse() -- Invalid sample rate.");
				return;
			}

			// The channel mode is encoded as a 2 bit value at the end of the 3nd
			// byte, index.e. xxxxxx11
			channelMode = (MpegChannelMode)((flags >> 16) & 0x3);

			// TODO: Add mode extension for completeness

			isCopyrighted = (flags & 1) == 1;
			isOriginal = ((flags >> 1) & 1) == 1;

			// Calculate the frame length
			if (layer == 1)
				frameLength = 24000 * 2 * bitrate / sampleRate + (IsPadded ? 1 : 0);
			else
				frameLength = 72000 * bitrate / sampleRate + (IsPadded ? 1 : 0);

			// Now that we're done parsing, set this to be a valid frame.
			isValid = true;
		}
		#endregion
		
		#region Public Properties
		public bool IsValid
		{
			get { return isValid; }
		}
		
		public MpegVersion Version
		{
			get { return version; }
		}
		
		public int Layer
		{
			get { return layer; }
		}
		
		public bool ProtectionEnabled
		{
			get { return protectionEnabled; }
		}
		
		public int Bitrate
		{
			get { return bitrate; }
		}
		
		public int SampleRate
		{
			get { return sampleRate; }
		}
		
		public bool IsPadded
		{
			get { return isPadded; }
		}
		
		public MpegChannelMode ChannelMode
		{
			get { return channelMode; }
		}
		
		public bool IsCopyrighted
		{
			get { return isCopyrighted; }
		}
		
		public bool IsOriginal
		{
			get { return isOriginal; }
		}
		
		public int FrameLength
		{
			get { return frameLength; }
		}
		#endregion
	}
}
