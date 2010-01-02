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
// $Id: Flip.cs,v 1.1 2007/01/03 15:21:28 wardog_uk Exp $
//----------------------------------------------------------------------------
namespace Alexandria.AsciiGenerator.AsciiConversion.Filters
{
	/// <summary>
	/// Filter to flip the values Horizontally and/or Vertically
	/// </summary>
	public class Flip : BaseFilter
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="horizontal">Flip horizontally?</param>
		/// <param name="vertical">Flip vertically?</param>
		public Flip(bool horizontal, bool vertical)
		{
			Horizontal = horizontal;
			Vertical = vertical;
		}

		/// <summary>
		/// Implementation of the apply function
		/// </summary>
		/// <param name="values">Input values</param>
		/// <returns>Output values</returns>
		public override byte[,] Apply(byte[,] values)
		{
			if (values == null)
				return null;

			if (!Horizontal && !Vertical)
				return values;

			int ArrayWidth = values.GetLength(0);
			int ArrayHeight = values.GetLength(1);

			byte[,] Result = new byte[ArrayWidth, ArrayHeight];

			for (int y = 0, ypos = Vertical ? ArrayHeight - 1 : 0; y < ArrayHeight; y++, ypos += Vertical ? -1 : 1)
			{
				for (int x = 0, xpos = Horizontal ? ArrayWidth - 1 : 0; x < ArrayWidth; x++, xpos += Horizontal ? -1 : 1)
				{
					Result[x, y] = values[xpos, ypos];
				}
			}

			return Result;
		}

		private bool _Horizontal;
		/// <summary>Get or set Horizontal</summary>
		public bool Horizontal
		{
			get { return _Horizontal; }
			set { _Horizontal = value; }
		}

		private bool _Vertical;
		/// <summary>Get or set Vertical</summary>
		public bool Vertical
		{
			get { return _Vertical; }
			set { _Vertical = value; }
		}
	}
}