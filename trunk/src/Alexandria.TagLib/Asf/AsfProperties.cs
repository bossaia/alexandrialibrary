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
using AlexandriaOrg.Alexandria.TagLib;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public class AsfProperties : AudioProperties
	{
		#region Constructors
		public AsfProperties(AsfHeaderObject header, ReadStyle style) : base(style)
		{
			duration = TimeSpan.Zero;
			//codecId = 0;
			//channels = 0;
			//sampleRate = 0;
			//bytesPerSecond = 0;

			foreach (AsfObject obj in header.Children)
			{
				if (obj is AsfFilePropertiesObject)
					duration = ((AsfFilePropertiesObject)obj).PlayDuration;

				if (obj is AsfStreamPropertiesObject && bytesPerSecond == 0)
				{
					AsfStreamPropertiesObject stream = (AsfStreamPropertiesObject)obj;

					if (!stream.StreamType.Equals(AsfGuid.AsfAudioMedia))
						continue;

					ByteVector data = stream.TypeSpecificData;

					codecId = data.Mid(0, 2).ToShort(false);
					channels = data.Mid(2, 2).ToShort(false);
					sampleRate = data.Mid(4, 4).ToUInt(false);
					bytesPerSecond = data.Mid(8, 4).ToUInt(false);
				}
			}
		}

		public AsfProperties(AsfHeaderObject header) : this(header, ReadStyle.Average)
		{
		}
		#endregion
	
		#region Private Fields
		private TimeSpan duration;
		private short codecId;
		private short channels;
		private uint sampleRate;
		private uint bytesPerSecond;
		#endregion
	
		#region Public Properties
		public override TimeSpan Duration
		{
			get {return duration;}
		}

		public short CodecId
		{
			get { return codecId; }
		}
		
		public override int Bitrate
		{
			get {return (int)(bytesPerSecond * 8 / 1000);}
		}
		
		public override int SampleRate
		{
			get {return (int) sampleRate;}
		}
		
		public override int Channels
		{
			get {return channels;}
		}
		#endregion
	}
}
