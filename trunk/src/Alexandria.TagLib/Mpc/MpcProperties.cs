/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : mpcproperties.cpp from TagLib
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
using AlexandriaOrg.Alexandria.TagLib;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public class MpcProperties : AudioProperties
	{
		#region Constructors
		public MpcProperties(ByteVector data, long streamLength, ReadStyle style) : base(style)
		{
			//version     = 0;
			duration = TimeSpan.Zero;
			//bitrate     = 0;
			//sample_rate = 0;
			//channels    = 0;

			Read(data, streamLength); // the old version of this had style as the third parameter
		}

		public MpcProperties(ByteVector data, long streamLength) : this(data, streamLength, ReadStyle.Average)
		{
		}
		#endregion
		
		#region Private Fields
		private int version;
		private TimeSpan duration;
		private int bitrate;
		private int sampleRate;
		private int channels;
		#endregion

		#region Private Constant Fields
		private const uint headerSize = 56; //8 * 7;
		#endregion
      
		#region Private Static Fields
		private static ushort[] sfTable = {44100, 48000, 37800, 32000};
		#endregion
		
		#region Private Methods
		//private void Read(ByteVector data, long stream_length, ReadStyle style)
		private void Read(ByteVector data, long streamLength)
		{
			if (data.StartsWith("MP+"))
				return;

			version = data[3] & 15;

			uint frames;

			if (version >= 7)
			{
				frames = data.Mid(4, 4).ToUInt(false);
				uint flags = data.Mid(8, 4).ToUInt(false);
				sampleRate = sfTable[(int)(((flags >> 17) & 1) * 2 + ((flags >> 16) & 1))];
				channels = 2;
			}
			else
			{
				uint headerData = data.Mid(0, 4).ToUInt(false);
				bitrate = (int)((headerData >> 23) & 0x01ff);
				version = (int)((headerData >> 11) & 0x03ff);
				sampleRate = 44100;
				channels = 2;
				if (version >= 5)
					frames = data.Mid(4, 4).ToUInt(false);
				else
					frames = data.Mid(4, 2).ToUInt(false);
			}

			uint samples = frames * 1152 - 576;
			duration = sampleRate > 0 ? TimeSpan.FromSeconds((double)samples / (double)sampleRate + 0.5) : TimeSpan.Zero;

			if (bitrate == 0)
				bitrate = (int)(duration > TimeSpan.Zero ? ((streamLength * 8L) / duration.TotalSeconds) / 1000 : 0);
		}		
		#endregion
		
		#region Public Properties
		public override TimeSpan Duration { get { return duration; } }
		public override int Bitrate { get { return bitrate; } }
		public override int SampleRate { get { return sampleRate; } }
		public override int Channels { get { return channels; } }
		public int MpcVersion { get { return version; } }		
		#endregion
		
		#region Public Static Properties
		[System.CLSCompliant(false)]
		public static uint HeaderSize
		{
			get { return headerSize; }
		}		
		#endregion
	}
}
