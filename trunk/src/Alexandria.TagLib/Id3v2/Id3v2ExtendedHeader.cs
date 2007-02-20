/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : id3v2extendedheader.cpp from TagLib
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
	public class Id3v2ExtendedHeader
	{
		#region Constructors
		public Id3v2ExtendedHeader()
		{
			//size = 0;
		}
		#endregion
		
		#region Private Fields
		private uint size;
		#endregion
		
		#region Protected Methods
		protected void Parse(ByteVector data)
		{
			if (data != null)
			{
				size = Id3v2SynchData.ToUInt(data.Mid(0, 4));
			}
			else throw new ArgumentNullException("data");
		}
		#endregion
		
		#region Public Properties
		[System.CLSCompliant(false)]
		public uint Size
		{
			get { return size; }
		}
		#endregion
	
		#region Public Methods
		public void SetData(ByteVector data)
		{
			Parse(data);
		}
		#endregion
	}
}
