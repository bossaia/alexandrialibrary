/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : id3v2synchdata.cpp from TagLib
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
	public static class Id3v2SynchData
	{
		#region Public Static Methods
		[System.CLSCompliant(false)]
		public static uint ToUInt(ByteVector data)
		{
			if (data != null)
			{
				uint sum = 0;
				int last = data.Count > 4 ? 3 : data.Count - 1;

				for(int i = 0; i <= last; i++)
					sum |= (uint) (data [i] & 0x7f) << ((last - i) * 7);

				return sum;
			}
			else throw new ArgumentNullException("data");
		}

		[System.CLSCompliant(false)]
		public static ByteVector FromUInt(uint value)
		{
			ByteVector vector = new ByteVector (4, 0);

			for (int i = 0; i < 4; i++)
				vector [i] = (byte) (value >> ((3 - i) * 7) & 0x7f);

			return vector;
		}
		#endregion
	}
}
