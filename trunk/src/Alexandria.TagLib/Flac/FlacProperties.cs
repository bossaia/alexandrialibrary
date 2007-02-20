/***************************************************************************
    copyright            : (C) 2006 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : audioproperties.cpp from TagLib
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
	public class FlacProperties : AudioProperties
	{
		#region Constructors
		public FlacProperties(ByteVector data, long streamLength, ReadStyle style) : base(style)
		{
			duration = TimeSpan.Zero;
			//bitrate      = 0;
			//sample_rate  = 0;
			//sample_width = 0;
			//channels     = 0;

			Read(data, streamLength); // the old version of this had style as the third parameter
		}

		public FlacProperties(ByteVector data, long streamLength) : this(data, streamLength, ReadStyle.Average)
		{
		}
		#endregion
		
		#region Private Fields
		private TimeSpan duration;
		private int bitrate;
		private int sampleRate;
		private int sampleWidth;
		private int channels;
		#endregion
		
		#region Private Methods
		//private void Read(ByteVector data, long streamLength, ReadStyle style)
		private void Read(ByteVector data, long streamLength)
		{
			if (data.Count < 18)
			{
				TagLibDebugger.Debug("FLAC.Properties.Read() - FLAC properties must contain at least 18 bytes.");
				return;
			}

			int pos = 0;

			// Minimum block size (in samples)
			pos += 2;

			// Maximum block size (in samples)
			pos += 2;

			// Minimum frame size (in bytes)
			pos += 3;

			// Maximum frame size (in bytes)
			pos += 3;

			uint flags = data.Mid(pos, 4).ToUInt(true);
			sampleRate = (int)(flags >> 12);
			channels = (int)(((flags >> 9) & 7) + 1);
			sampleWidth = (int)(((flags >> 4) & 31) + 1);

			// The last 4 bits are the most significant 4 bits for the 36 bit
			// stream length in samples. (Audio files measured in days)

			double high_length = (double)(sampleRate > 0 ? (((flags & 0xf) << 28) / sampleRate) << 4 : 0);
			pos += 4;

			duration = sampleRate > 0 ? TimeSpan.FromSeconds((double)data.Mid(pos, 4).ToUInt(true) / (double)sampleRate + high_length) : TimeSpan.Zero;
			pos += 4;

			// Uncompressed bitrate:

			//bitrate = ((sampleRate * channels) / 1000) * sample_width;

			// Real bitrate:
			bitrate = (int)(duration > TimeSpan.Zero ? ((streamLength * 8L) / duration.TotalSeconds) / 1000 : 0);
		}
		#endregion
		
		#region Public Properties
		[Obsolete("This property is obsolete; use the Duration property instead.")]
		public override int Length
		{
			get { return (int)duration.TotalSeconds; }
		}
		
		public override TimeSpan Duration
		{
			get { return duration; }
		}
		
		public override int Bitrate
		{
			get { return bitrate; }
		}
		
		public override int SampleRate
		{
			get { return sampleRate; }
		}
		
		public override int Channels
		{
			get { return channels; }
		}
		
		public int SampleWidth
		{
			get { return sampleWidth; }
		}
		#endregion
	}
}
