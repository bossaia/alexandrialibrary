/***************************************************************************
    copyright            : (C) 2006 by Dan Poage      dan.poage@gmail.com
    copyright            : (C) 2006 by Brian Nickel   brian.nickel@gmail.com
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
	public class Mpeg4Properties : AudioProperties
	{
		#region Constructors
		public Mpeg4Properties(Mpeg4IsoMovieHeaderBox mvhdBox, Mpeg4IsoAudioSampleEntry sampleEntry, ReadStyle style) : base(style)
		{
			this.mvhdBox = mvhdBox;
			this.sampleEntry = sampleEntry;
		}
		#endregion
		
		#region Private Fields
		private Mpeg4IsoMovieHeaderBox mvhdBox;
		private Mpeg4IsoAudioSampleEntry sampleEntry;
		#endregion
		
		#region Public Properties
		public override TimeSpan Duration
		{
			get
			{
				// The length is the number of ticks divided by ticks per second.
				return mvhdBox == null ? TimeSpan.Zero : TimeSpan.FromSeconds((double)mvhdBox.Duration / (double)mvhdBox.Timescale);
			}
		}

		public override int Bitrate
		{
			get
			{
				// If we don'type have an stream descriptor, we don'type know what'field what.
				if (sampleEntry == null || sampleEntry.FindChild("esds") == null)
					return 0;

				// Return from the elementary stream descriptor.
				return (int)((Mpeg4AppleElementaryStreamDescriptor)sampleEntry.FindChild("esds")).AverageBitrate;
			}
		}

		public override int SampleRate
		{
			get
			{
				// The sample entry has this info.
				return sampleEntry == null ? 0 : (int)sampleEntry.SampleRate;
			}
		}

		public override int Channels
		{
			get
			{
				// The sample entry has this info.
				return sampleEntry == null ? 0 : (int)sampleEntry.ChannelCount;
			}
		}

		// All additional special info from the Movie Header.
		public DateTime CreationTime
		{
			get { return mvhdBox == null ? new System.DateTime(1904, 1, 1, 0, 0, 0) : mvhdBox.CreationTime; }
		}
		
		public DateTime ModificationTime
		{
			get { return mvhdBox == null ? new System.DateTime(1904, 1, 1, 0, 0, 0) : mvhdBox.ModificationTime; }
		}
		
		public double Rate
		{
			get { return mvhdBox == null ? 1.0 : mvhdBox.Rate; }
		}
		
		public double Volume
		{
			get { return mvhdBox == null ? 1.0 : mvhdBox.Volume; }
		}
		#endregion
	}
}
