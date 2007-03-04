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
// $Id: ConvolutionMatrix.cs,v 1.3 2007/01/03 15:21:28 wardog_uk Exp $
//----------------------------------------------------------------------------
namespace Alexandria.AsciiGenerator
{
	/// <summary>
	/// Basic class for 3x3 convolution matricies
	/// </summary>
	public struct ConvolutionMatrix
	{
		/// <summary>Matrix values (= 0 if not specified)</summary>
		public int TopLeft, TopMiddle, TopRight,
				MiddleLeft, Pixel, MiddleRight,
				BottomLeft, BottomMiddle, BottomRight,
				Offset, Factor;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="topLeft">Top Left value</param>
		/// <param name="topMiddle">Top Middle value</param>
		/// <param name="topRight">Top Right value</param>
		/// <param name="middleLeft">Middle Left value</param>
		/// <param name="middleMiddle">Middle Middle value</param>
		/// <param name="middleRight">Middle Right value</param>
		/// <param name="bottomLeft">Bottom Left value</param>
		/// <param name="bottomMiddle">Bottom Middle value</param>
		/// <param name="bottomRight">Bottom Right value</param>
		public ConvolutionMatrix(int topLeft, int topMiddle, int topRight,
			int middleLeft, int middleMiddle, int middleRight,
			int bottomLeft, int bottomMiddle, int bottomRight)
		{
			TopLeft = topLeft;
			TopMiddle = topMiddle;
			TopRight = topRight;
			MiddleLeft = middleLeft;
			Pixel = middleMiddle;
			MiddleRight = middleRight;
			BottomLeft = bottomLeft;
			BottomMiddle = bottomMiddle;
			BottomRight = bottomRight;
			Factor = 1;
			Offset = 0;
		}

		/// <summary>
		/// Apply a ConvolutionMatrix to the array of values
		/// </summary>
		/// <param name="values">Values to be processed</param>
		/// <param name="matrix">ConvolutionMatrix to be applied</param>
		/// <returns>Array containing the altered values</returns>
		public static byte[,] Apply(byte[,] values, ConvolutionMatrix matrix)
		{
			if (values == null)
				return null;

			// store the dimensions of the array
			int ArrayWidth = values.GetLength(0);
			int ArrayHeight = values.GetLength(1);

			// create the new array to be returned
			byte[,] Result = new byte[ArrayWidth, ArrayHeight];

			int iPixel;

			for (int y = 0; y < ArrayHeight - 2; y++)
			{
				for (int x = 0; x < ArrayWidth - 2; x++)
				{
					iPixel =
						(values[x, y] * matrix.TopLeft) + (values[x + 1, y] * matrix.TopMiddle) + (values[x + 2, y] * matrix.TopRight) +
						(values[x, y + 1] * matrix.MiddleLeft) + (values[x + 1, y + 1] * matrix.Pixel) + (values[x + 2, y] * matrix.MiddleRight) +
						(values[x, y + 2] * matrix.BottomLeft) + (values[x + 1, y + 2] * matrix.BottomMiddle) + (values[x + 2, y] * matrix.BottomRight);

					iPixel = (int)(((float)iPixel / (float)matrix.Factor) + 0.5) + matrix.Offset;

					if (iPixel < 0) iPixel = 0;
					if (iPixel > 255) iPixel = 255;

					Result[x + 1, y + 1] = (byte)iPixel;
				}
			}

			int position = ArrayHeight - 1;

			// TODO: Apply the matrix to the edges
			for (int x = 0; x < ArrayWidth; x++)
			{
				Result[x, 0] = values[x, 0];
				Result[x, position] = values[x, position];
			}


			position = ArrayWidth - 1;

			for (int y = 1; y < ArrayHeight - 1; y++)
			{
				Result[0, y] = values[0, y];
				Result[position, y] = values[position, y];
			}

			return Result;
		}

	}

	//			// edge detect
	//			ConvolutionMatrix matrix = new ConvolutionMatrix(
	//				1, 1, 1,
	//				0, 0, 0,
	//				-1, -1, -1);
	//			
	//			matrix.Factor = 1;
	//			matrix.Offset = 127;

	//			// mean removal
	//			ConvolutionMatrix matrix = new ConvolutionMatrix(
	//				-1, -1, -1,
	//				-1, 9, -1,
	//				-1, -1, -1);
	//			
	//			matrix.Factor = 1;
}