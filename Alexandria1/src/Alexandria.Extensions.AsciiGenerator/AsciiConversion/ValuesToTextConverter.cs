//----------------------------------------------------------------------------
// ASCII Generator dotNET - Image to ASCII Art Conversion Program
// Copyright (C) 2005 Jonathan Mathews
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
// $Id: ValuesToTextConverter.cs,v 1.1 2007/01/03 16:12:27 wardog_uk Exp $
//----------------------------------------------------------------------------
using System;
using System.Text;
using System.Drawing;
using System.Collections;

namespace Alexandria.AsciiGenerator.AsciiConversion
{
	/// <summary>
	/// Abstract class for all values to text conversions
	/// </summary>
	abstract class ValuesToTextConverter
	{
		#region Constructor
		/// <summary>Constructor</summary>
		public ValuesToTextConverter()
		{
		}
		#endregion

		#region Public Fields
		/// <summary>
		/// Convert 2d array of byte values into character strings
		/// </summary>
		/// <param name="values">2d array of values that represent the image</param>
		/// <returns>Array of strings containing the text image</returns>
		abstract public string[] Apply(byte[,] values);
		#endregion
	}
}