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
	public class OggVorbisProperties : AudioProperties
	{
		#region Constructors
		public OggVorbisProperties(OggVorbisFile file, ReadStyle style) : base(style)
		{
			duration = TimeSpan.Zero;
			//bitrate         = 0;
			//sample_rate     = 0;
			//channels        = 0;
			//vorbis_version  = 0;
			//bitrate_maximum = 0;
			//bitrate_nominal = 0;
			//bitrate_minimum = 0;

			Read(file); // the old version of this had style as the second parameter
		}

		public OggVorbisProperties(OggVorbisFile file) : this(file, ReadStyle.Average)
		{
		}
		#endregion
		
		#region Private Fields
		private TimeSpan duration;
		private int bitrate;
		private int sampleRate;
		private int channels;
		private int vorbisVersion;
		private int bitrateMaximum;
		private int bitrateNominal;
		private int bitrateMinimum;
		#endregion
		
		#region Private Constant Fields
		private static readonly byte[] vorbisCommentHeaderId = { 0x01, (byte)'v', (byte)'o', (byte)'r', (byte)'b', (byte)'i', (byte)'s' };
		#endregion
		
		#region Private Methods
		//private void Read(OggVorbisFile file, ReadStyle style)
		private void Read(OggVorbisFile file)
		{
			// Get the identification header from the Ogg implementation.

			ByteVector data = file.GetPacket(0);

			int pos = 0;

			if (data.Mid(pos, 7) != vorbisCommentHeaderId)
			{
				TagLibDebugger.Debug("Vorbis.Properties.Read() -- invalid Vorbis identification header");
				return;
			}

			pos += 7;

			vorbisVersion = (int)data.Mid(pos, 4).ToUInt(false);
			pos += 4;

			channels = data[pos];
			pos += 1;

			sampleRate = (int)data.Mid(pos, 4).ToUInt(false);
			pos += 4;

			bitrateMaximum = (int)data.Mid(pos, 4).ToUInt(false);
			pos += 4;

			bitrateNominal = (int)data.Mid(pos, 4).ToUInt(false);
			pos += 4;

			bitrateMinimum = (int)data.Mid(pos, 4).ToUInt(false);

			// TODO: Later this should be only the "fast" mode.
			bitrate = bitrateNominal;

			// Find the length of the file.  See http://wiki.xiph.org/VorbisStreamLength/
			// for my notes on the topic.

			OggPageHeader first = file.FirstPageHeader;
			OggPageHeader last = file.LastPageHeader;

			if (first != null && last != null)
			{
				long start = first.AbsoluteGranularPosition;
				long end = last.AbsoluteGranularPosition;

				if (start >= 0 && end >= 0 && sampleRate > 0)
					duration = TimeSpan.FromSeconds(((double)(end - start) / (double)sampleRate));
				else
					TagLibDebugger.Debug("Vorbis.Properties.Read() -- Either the PCM " +
									"values for the start or end of this file was " +
									"incorrect or the sample rate is zero.");
			}
			else
				TagLibDebugger.Debug("Vorbis.Properties.Read() -- Could not find valid first and last Ogg pages.");
		}
		#endregion
		
		#region Public Properties
		public override TimeSpan Duration
		{
			get { return duration; }
		}
		
		public override int Bitrate
		{
			get { return (int)((float)bitrate / 1000f + 0.5); }
		}
		
		public override int SampleRate
		{
			get { return sampleRate; }
		}
		
		public override int Channels
		{
			get { return channels; }
		}
		
		public int VorbisVersion
		{
			get { return vorbisVersion; }
		}
		
		public int BitrateMaximum
		{
			get { return bitrateMaximum; }
		}
		
		public int BitrateNominal
		{
			get { return bitrateNominal; }
		}
		
		public int BitrateMinimum
		{
			get { return bitrateMinimum; }
		}
		#endregion
	}
}
