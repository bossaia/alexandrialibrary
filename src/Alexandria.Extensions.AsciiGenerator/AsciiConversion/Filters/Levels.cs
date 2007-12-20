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
// $Id: Levels.cs,v 1.1 2007/01/03 15:21:28 wardog_uk Exp $
//----------------------------------------------------------------------------
namespace Alexandria.AsciiGenerator.AsciiConversion.Filters
{
	/// <summary>
	/// Filter to adjust the levels of the data
	/// </summary>
	public class Levels : BaseFilter
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="minimum">minimum level (> 0)</param>
		/// <param name="maximum">maximum level (less then 256)</param>
		/// <param name="median">median value (0.0 to 1.0)</param>
		public Levels(int minimum, int maximum, float median)
		{
			Minimum = minimum;
			Maximum = maximum;
			Median = median;
		}

		/// <summary>
		/// Implementation of the apply function
		/// </summary>
		/// <param name="values">Input values</param>
		/// <returns>Output values</returns>
		public override byte[,] Apply(byte[,] values)
		{
			if (values == null || Minimum < 0 || Maximum < 0 ||
				Minimum > Maximum || Maximum > 255 || Minimum > 255) return null;

			// array to store the output value for each possible input value
			byte[] translated = new byte[256];

			for (int x = 0; x < Minimum; x++)
			{
				translated[x] = 0;
			}

			int mid = (int)(((float)(Maximum - Minimum) * Median) + 0.5f) + Minimum;

			float ratio = 128f / (float)(mid - Minimum);

			for (int x = Minimum; x < mid; x++)
			{
				translated[x] = (byte)(((float)(x - Minimum) * ratio) + 0.5f);
			}

			ratio = 128f / (float)(Maximum - mid);

			for (int x = mid; x < Maximum; x++)
			{
				translated[x] = (byte)(((float)(x - mid) * ratio) + 128.5f);
			}

			for (int x = Maximum; x < 256; x++)
			{
				translated[x] = 255;
			}

			int ArrayWidth = values.GetLength(0);
			int ArrayHeight = values.GetLength(1);

			byte[,] Result = new byte[ArrayWidth, ArrayHeight];

			for (int y = 0; y < ArrayHeight; y++)
			{
				for (int x = 0; x < ArrayWidth; x++)
				{
					Result[x, y] = translated[values[x, y]];
				}
			}

			return Result;
		}

		private int _Maximum;
		/// <summary>Get or set Maximum</summary>
		public int Maximum
		{
			get { return _Maximum; }
			set { _Maximum = value; }
		}

		private int _Minimum;
		/// <summary>Get or set Minimum</summary>
		public int Minimum
		{
			get { return _Minimum; }
			set { _Minimum = value; }
		}

		private float _Median;
		/// <summary>Get or set Median</summary>
		public float Median
		{
			get { return _Median; }
			set { _Median = value; }
		}
	}
}