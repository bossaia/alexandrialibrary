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
// $Id: Stretch.cs,v 1.1 2007/01/03 15:21:28 wardog_uk Exp $
//----------------------------------------------------------------------------
namespace Alexandria.AsciiGenerator.AsciiConversion.Filters
{
	/// <summary>
	/// Filter to stretch the values so min...max = 0...255
	/// </summary>
	public class Stretch : BaseFilter
	{
		/// <summary>
		/// Implementation of the apply function
		/// </summary>
		/// <param name="values">Input values</param>
		/// <returns>Output values</returns>
		public override byte[,] Apply(byte[,] values)
		{
			if (values == null)
				return null;

			// store the dimensions of the array
			int ArrayWidth = values.GetLength(0);
			int ArrayHeight = values.GetLength(1);

			// create the new array to be returned
			byte[,] Result = new byte[ArrayWidth, ArrayHeight];

			int iMin = 255, iMax = 0;

			// loop for all values
			for (int y = 0; y < ArrayHeight; y++)
			{
				for (int x = 0; x < ArrayWidth; x++)
				{
					if ((int)values[x, y] < iMin)
					{
						iMin = (int)values[x, y];
					}

					if ((int)values[x, y] > iMax)
					{
						iMax = (int)values[x, y];
					}
				}
			}

			// if the values need to be stretched
			if ((iMin > 0 || iMax < 255) && iMax > iMin)
			{
				float fRange = (float)(iMax - iMin);

				// loop for all values
				for (int y = 0; y < ArrayHeight; y++)
				{
					for (int x = 0; x < ArrayWidth; x++)
					{
						Result[x, y] = (byte)(((float)((values[x, y] - iMin) * 255) / fRange) + 0.5);
					}
				}

				// return the new array
				return Result;
			}
			else
			{
				// return the original array
				return values;
			}
		}
	}
}