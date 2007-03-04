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
// $Id: BrightnessContrast.cs,v 1.1 2007/01/03 15:21:28 wardog_uk Exp $
//----------------------------------------------------------------------------
namespace Alexandria.AsciiGenerator.AsciiConversion.Filters
{
	/// <summary>
	/// Filter to apply Brightness/Contrast changes to the data
	/// </summary>
	public class BrightnessContrast : BaseFilter
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="brightness">Level of brightness to use (-255 to 255)</param>
		/// <param name="contrast">Level of contrast to use (-255 to 255)</param>
		public BrightnessContrast(int brightness, int contrast)
		{
			Brightness = brightness;
			Contrast = contrast;
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

			// store the dimensions of the array
			int ArrayWidth = values.GetLength(0);
			int ArrayHeight = values.GetLength(1);

			// create the new array to be returned
			byte[,] Result = new byte[ArrayWidth, ArrayHeight];

			byte[] BCArray = new byte[256];
			int iMin = 0, iMax = 255, iXOffset = 0;
			int iItemValue;

			float fContrast;

			if (Contrast < 0)
			{
				fContrast = 1f + ((float)Contrast / 255f);

				// calculate boundaries
				iMin = (int)(((255f - (255f * fContrast)) / 2.0) + 0.5);
				iMax = 255 - iMin;
			}
			else
			{
				fContrast = 256f / (256f - (float)Contrast);

				// calculate offset
				iXOffset = (int)(((255f - (255f * fContrast)) / 2.0) + 0.5);
			}

			int iItemOffset = Brightness + iMin + iXOffset;

			for (int i = 0; i < 256; i++)
			{
				// calculate value
				iItemValue = (int)(((float)i * fContrast) + 0.5) + iItemOffset;

				// limit to the min/max values
				if (iItemValue > iMax)
					iItemValue = iMax;
				else if (iItemValue < iMin)
					iItemValue = iMin;

				// store in the array
				BCArray[i] = (byte)iItemValue;
			}

			for (int y = 0; y < ArrayHeight; y++)
			{
				for (int x = 0; x < ArrayWidth; x++)
				{
					Result[x, y] = BCArray[values[x, y]];
				}
			}

			return Result;
		}

		private int _Brightness;
		/// <summary>Get or set Brightness</summary>
		public int Brightness
		{
			get { return _Brightness; }
			set { _Brightness = value; }
		}

		private int _Contrast;
		/// <summary>Get or set Contrast</summary>
		public int Contrast
		{
			get { return _Contrast; }
			set { _Contrast = value; }
		}
	}
}