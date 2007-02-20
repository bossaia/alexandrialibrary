/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : id3v2frame.cpp from TagLib
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
using System.Collections.Generic;

namespace AlexandriaOrg.Alexandria.TagLib
{   
	public class Id3v2RelativeVolumeFrame : Id3v2Frame
	{
		#region Constructors
		public Id3v2RelativeVolumeFrame(ByteVector data) : base(data)
		{
			//SetData(data);
			ParseHeader(data);
			ParseRelativeVolumeFields(data);
		}

		public Id3v2RelativeVolumeFrame(string identification) : base("RVA2")
		{
			this.identification = identification;
		}

		protected internal Id3v2RelativeVolumeFrame(ByteVector data, Id3v2FrameHeader header) : base(header)
		{
			//ParseFields(FieldData(data));
			ParseRelativeVolumeFields(FieldData(data));
		}
		#endregion
		
		#region Private Fields
		private string identification;
		private Dictionary<Id3v2ChannelType, ChannelData> channels = new Dictionary<Id3v2ChannelType, ChannelData>();
		#endregion
   
		#region Private Methods
		private void ParseRelativeVolumeFields(ByteVector data)
		{
			int pos = data.Find(TextDelimiter(StringType.Latin1));
			if (pos < 0)
				return;

			identification = data.Mid(0, pos).ToString(StringType.Latin1);
			pos += 1;

			// Each channel is at least 4 bytes.

			while (pos <= data.Count - 4)
			{
				Id3v2ChannelType type = (Id3v2ChannelType)data[pos];
				pos += 1;

				SetVolumeAdjustmentIndex(data.Mid(pos, 2).ToShort(), type);
				pos += 2;

				int bytes = BitsToBytes(data[pos]);
				pos += 1;

				SetPeakVolumeIndex(ParsePeakVolume(data.Mid(pos, bytes)), type);
				pos += bytes;
			}
		}
		
		private ByteVector RenderRelativeVolumeFields()
		{
			ByteVector data = new ByteVector();

			data.Add(ByteVector.FromString(identification, StringType.Latin1));
			data.Add(TextDelimiter(StringType.Latin1));

			foreach (ChannelData channel in channels.Values)
			{
				data.Add((byte)channel.ChannelType);
				data.Add(ByteVector.FromShort(channel.VolumeAdjustment));
				data.Add(RenderPeakVolume(channel.PeakVolume));
			}

			return data;
		}
		#endregion
   
		#region Private Static Methods
		private static int BitsToBytes(int numberOfBits)
		{
			return numberOfBits % 8 == 0 ? numberOfBits / 8 : ( numberOfBits - numberOfBits % 8) / 8 + 1;
		}
		#endregion
   
		#region Protected Methods
		protected override void ParseFields(ByteVector data)
		{
			ParseRelativeVolumeFields(data);
		}

		protected override ByteVector RenderFields()
		{
			return RenderRelativeVolumeFields();
		}
		#endregion

		#region Protected Static Methods
		[System.CLSCompliant(false)]
		protected static ulong ParsePeakVolume(ByteVector data)
		{
			if (data != null)
			{
				if (data.Count == 0)
					return 0;

				// If common, pound it out.
				if (data.Count == 2)
					return (ulong)(data[0] + data[1] * 0xFF);

				ulong peak = 0;
				for (int i = data.Count - 1; i >= 0; i--)
					peak = peak * 256 + data[i];

				return peak;
			}
			else throw new ArgumentNullException("data");
		}

		[System.CLSCompliant(false)]
		protected static ByteVector RenderPeakVolume(ulong peak)
		{
			ByteVector v = new ByteVector(1);

			if (peak == 0)
				return v;

			ulong bigger = 1;
			byte bits = 1;
			while (bigger < peak)
			{
				bigger *= 2;
				bits += 1;
			}

			v[0] = bits;

			for (uint j = 0; j < BitsToBytes(bits); j++)
			{
				byte o = (byte)(peak % 0xFF);
				peak -= o;
				peak /= 0xFF;
				v.Add(o);
			}
			return v;
		}
		#endregion
   
		#region Public Properties
		public string Identification
		{
			get {return identification;}
		}

		public IDictionary<Id3v2ChannelType, ChannelData> Channels //Id3v2ChannelType[] Channels
		{
			get {return channels;}
		}
		#endregion
   
		#region Public Methods
		public short VolumeAdjustmentIndex(Id3v2ChannelType type)
		{
			return channels.ContainsKey(type) ? channels[type].VolumeAdjustment : (short)0;
		}

		public void SetVolumeAdjustmentIndex(short index, Id3v2ChannelType type)
		{
			if (!channels.ContainsKey(type))
				channels.Add(type, new ChannelData(type));

			channels[type].VolumeAdjustment = index;
		}

		public float VolumeAdjustment(Id3v2ChannelType type)
		{
			return ((float)VolumeAdjustmentIndex(type)) / 512f;
		}

		public void SetVolumeAdjustment(float adjustment, Id3v2ChannelType type)
		{
			SetVolumeAdjustmentIndex((short)(adjustment * 512f), type);
		}

		[System.CLSCompliant(false)]
		public ulong PeakVolumeIndex(Id3v2ChannelType type)
		{
			return channels.ContainsKey(type) ? channels[type].PeakVolume : 0;
		}

		[System.CLSCompliant(false)]
		public void SetPeakVolumeIndex(ulong index, Id3v2ChannelType type)
		{
			if (!channels.ContainsKey(type))
				channels.Add(type, new ChannelData(type));

			channels[type].PeakVolume = index;
		}

		public double PeakVolume(Id3v2ChannelType type)
		{
			return ((double)PeakVolumeIndex(type)) / 512.0;
		}

		public void SetPeakVolume(double adjustment, Id3v2ChannelType type)
		{
			SetPeakVolumeIndex((ulong)(adjustment * 512.0), type);
		}

		public override string ToString()
		{
			return identification;
		}
		#endregion
   
		#region Public Static Methods
		public static Id3v2RelativeVolumeFrame Find(Id3v2Tag tag, string identification)
		{
			if (tag != null)
			{
				foreach (Id3v2RelativeVolumeFrame frame in tag.GetFrames("RVA2"))
				{
					if (frame != null && frame.Identification == identification)
						return frame;
				}
				return null;
			}
			else throw new ArgumentNullException("tag");
		}
		#endregion		
	}
}
