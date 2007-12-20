/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : wvproperties.cpp from libtunepimp
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
	public class WavPackProperties : AudioProperties
	{
		#region Constructors
		public WavPackProperties(ByteVector data, long stream_length, ReadStyle style) : base(style)
		{
			//version         = 0;
			duration = TimeSpan.Zero;
			//bitrate         = 0;
			//sample_rate     = 0;
			//channels        = 0;
			//bits_per_sample = 0;

			Read(data, stream_length); //old version had style as the third parameter
		}

		public WavPackProperties(ByteVector data, long stream_length) : this(data, stream_length, ReadStyle.Average)
		{
		}
		#endregion
		
		#region Private Fields
		private int version;
		private TimeSpan duration;
		private int bitrate;
		private int sampleRate;
		private int channels;
		private int bitsPerSample;
		#endregion
      
		#region Private Constant Fields
		private const uint headerSize = 32;
		#endregion
      
		#region Private Static Fields
		private static uint[] sampleRates = {6000, 8000, 9600, 11025, 12000, 16000, 22050, 24000, 32000, 44100, 48000, 64000, 88200, 96000, 192000};	  
		#endregion
	  
		#region Private Methods
		//private void Read(ByteVector data, long stream_length, ReadStyle style)
		private void Read(ByteVector data, long stream_length)
		{
			int BYTES_STORED = 3;
			int MONO_FLAG = 4;

			int SHIFT_LSB = 13;
			long SHIFT_MASK = (0x1fL << SHIFT_LSB);

			int SRATE_LSB = 23;
			long SRATE_MASK = (0xfL << SRATE_LSB);

			if (!data.StartsWith("wvpk"))
				return;

			version = data.Mid(8, 2).ToShort(false);

			uint flags = data.Mid(24, 4).ToUInt(false);
			bitsPerSample = (int)(((flags & BYTES_STORED) + 1) * 8 - ((flags & SHIFT_MASK) >> SHIFT_LSB));
			sampleRate = (int)(sampleRates[(flags & SRATE_MASK) >> SRATE_LSB]);
			channels = ((flags & MONO_FLAG) != 0) ? 1 : 2;

			uint samples = data.Mid(12, 4).ToUInt(false);
			duration = sampleRate > 0 ? TimeSpan.FromSeconds((double)samples / (double)sampleRate + 0.5) : TimeSpan.Zero;
			bitrate = (int)(duration > TimeSpan.Zero ? ((stream_length * 8L) / duration.TotalSeconds) / 1000 : 0);
		}		
		#endregion
	  
		#region Public Properties
		public override TimeSpan Duration { get { return duration; } }
		public override int Bitrate { get { return bitrate; } }
		public override int SampleRate { get { return sampleRate; } }
		public override int Channels { get { return channels; } }
		public int Version { get { return version; } }
		public int BitsPerSample { get { return bitsPerSample; } }		
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
