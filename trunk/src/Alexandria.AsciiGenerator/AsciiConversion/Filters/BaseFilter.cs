//----------------------------------------------------------------------------
// ASCII Generator dotNET - Image to ASCII Art Conversion Program
// Copyright (C) 2007 Jonathan Mathews
//----------------------------------------------------------------------------
// This file is part of ASCII Generator dotNET.
//
// ASCII Generator dotNET is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
//----------------------------------------------------------------------------
// http://www.jmsoftware.co.uk/                http://ascgen2.sourceforge.net/
// <info@jmsoftware.co.uk>                              <jmsoftware@gmail.com>
//----------------------------------------------------------------------------
// $Id: AscgenFilter.cs,v 1.1 2007/01/03 15:21:28 wardog_uk Exp $
//----------------------------------------------------------------------------
namespace AlexandriaOrg.Alexandria.AsciiGenerator.AsciiConversion.Filters
{
	/// <summary>
	/// Base class for the ASCII Filters
	/// </summary>
	public abstract class BaseFilter
	{
		#region Public Methods
		/// <summary>
		/// Abstract function required for all filters
		/// </summary>
		/// <param name="values">Input values</param>
		/// <returns>Output values</returns>
		public abstract byte[,] Apply(byte[,] values);
		#endregion
	}
}