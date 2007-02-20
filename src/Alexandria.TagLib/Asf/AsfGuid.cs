/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : wmafile.cpp from libtunepimp
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
 *   Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111, 0x1307  *
 *   USA                                                                   *
 ***************************************************************************/

using System;
using System.Collections;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public struct AsfGuid
	{
		#region Constructors
		public AsfGuid(ByteVector raw)
		{
			if (raw != null)
			{
				this.part1 = raw.Mid(0, 4).ToUInt(false);
				this.part2 = raw.Mid(4, 2).ToShort(false);
				this.part3 = raw.Mid(6, 2).ToShort(false);
				this.part4 = raw.Mid(8, 2).ToShort(true);
				this.part5 = raw.Mid(10, 6).ToLong(true);
			}
			else throw new ArgumentNullException("raw");
		}

		[System.CLSCompliant(false)]
		public AsfGuid(uint part1, uint part2, uint part3, uint part4, long part5)
		{
			this.part1 = part1;
			this.part2 = (short)part2;
			this.part3 = (short)part3;
			this.part4 = (short)part4;
			this.part5 = part5;
		}
		#endregion
		
		#region Private Fields
		private uint part1;
		private short part2;
		private short part3;
		private short part4;
		private long part5;
		#endregion
		
		#region Private Static Fields
		private static AsfGuid asfHeaderObject = new AsfGuid(0x75B22630, 0x668E, 0x11CF, 0xA6D9, 0x00AA0062CE6C);
		private static AsfGuid asfFilePropertiesObject = new AsfGuid(0x8CABDCA1, 0xA947, 0x11CF, 0x8EE4, 0x00C00C205365);
		private static AsfGuid asfStreamPropertiesObject = new AsfGuid(0xB7DC0791, 0xA9B7, 0x11CF, 0x8EE6, 0x00C00C205365);
		private static AsfGuid asfContentDescriptionObject = new AsfGuid(0x75B22633, 0x668E, 0x11CF, 0xA6D9, 0x00AA0062CE6C);
		private static AsfGuid asfExtendedContentDescriptionObject = new AsfGuid(0xD2D0A440, 0xE307, 0x11D2, 0x97F0, 0x00A0C95EA850);
		private static AsfGuid asfPaddingObject = new AsfGuid(0x1806D474, 0xCADF, 0x4509, 0xA4BA, 0x9AABCB96AAE8);
		private static AsfGuid asfAudioMedia = new AsfGuid(0xF8699E40, 0x5B4D, 0x11CF, 0xA8FD, 0x00805F5C442B);		
		#endregion
		
		#region Private Static Methods
		private static string RenderNumber(long value, int length)
		{
			System.Text.StringBuilder numberBuilder = new System.Text.StringBuilder(value.ToString("x", System.Globalization.NumberFormatInfo.InvariantInfo));
			//string field = value.ToString("x");

			if (numberBuilder.Length > length)
				return numberBuilder.ToString().Substring(numberBuilder.Length - length);

			while (numberBuilder.Length < length)
				numberBuilder.Insert(0, "0");
			//field = "0" + field;

			return numberBuilder.ToString();
		}
		#endregion
		
		#region Public Properties
		[CLSCompliant(false)]
		public uint Part1
		{
			get {return part1;}
		}
		
		public short Part2
		{
			get {return part2;}
		}

		public short Part3
		{
			get {return part3;}
		}

		public short Part4
		{
			get {return part4;}
		}

		public long Part5
		{
			get {return part5;}
		}
		#endregion
		
		#region Public Static Properties
		public static AsfGuid AsfHeaderObject
		{
			get {return asfHeaderObject;}
		}
		
		public static AsfGuid AsfFilePropertiesObject
		{
			get {return asfFilePropertiesObject;}
		}
		
		public static AsfGuid AsfStreamPropertiesObject
		{
			get {return asfStreamPropertiesObject;}
		}
		
		public static AsfGuid AsfContentDescriptionObject
		{
			get { return asfContentDescriptionObject; }
		}
		
		public static AsfGuid AsfExtendedContentDescriptionObject
		{
			get { return asfExtendedContentDescriptionObject; }
		}
		
		public static AsfGuid AsfPaddingObject
		{
			get { return asfPaddingObject; }
		}
		
		public static AsfGuid AsfAudioMedia
		{
			get { return asfAudioMedia; }
		}
		#endregion
		
		#region Public Methods
		public ByteVector Render()
		{
			return ByteVector.FromUInt(part1, false).Mid(0, 4)
				 + ByteVector.FromShort(part2, false).Mid(0, 2)
				 + ByteVector.FromShort(part3, false).Mid(0, 2)
				 + ByteVector.FromShort(part4, true).Mid(0, 2)
				 + ByteVector.FromLong(part5, true).Mid(2, 6);
		}

		public override bool Equals(object obj)
		{
			if (obj is AsfGuid)
			{
				AsfGuid otherGuid = (AsfGuid)obj;
				return (this == otherGuid);
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return (int)(part1 * part2 * part3 * part4 * part5);
		}

		public new string ToString()
		{
			return RenderNumber(part1, 8) + "-"
				 + RenderNumber(part2, 4) + "-"
				 + RenderNumber(part3, 4) + "-"
				 + RenderNumber(part4, 4) + "-"
				 + RenderNumber(part5, 12);
		}
		#endregion

		#region Public Static Methods
		public static bool operator ==(AsfGuid g1, AsfGuid g2)
		{
			return g1.Part1 == g2.Part1 &&
				   g1.Part2 == g2.Part2 &&
				   g1.Part3 == g2.Part3 &&
				   g1.Part4 == g2.Part4 &&
				   g1.Part5 == g2.Part5;
		}

		public static bool operator !=(AsfGuid g1, AsfGuid g2)
		{
			return !(g1 == g2);
		}
		#endregion 
	}
}
