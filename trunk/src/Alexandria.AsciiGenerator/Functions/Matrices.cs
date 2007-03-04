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
// $Id: Matricies.cs,v 1.2 2005/08/15 23:26:20 wardog_uk Exp $
//----------------------------------------------------------------------------
using System;
using System.Drawing.Imaging;

namespace Alexandria.AsciiGenerator
{
	/// <summary>
	/// Class of static functions to create or process ColorMatrix objects
	/// </summary>
	public abstract class Matrices
	{
		/// <summary>
		/// Returns a ColorMatrix to alter an image by the desired Brightness and Contrast
		/// </summary>
		/// <param name="brightness">Level of brightness (0.0 = none)</param>
		/// <param name="contrast">Level of contrast (1.0 = none)</param>
		/// <returns>ColorMatrix to modify an images brightness and contrast</returns>
		public static ColorMatrix BrightnessContrast(float brightness, float contrast)
		{
			float W = (0.5f * (1f - contrast)) + brightness;

			return new ColorMatrix(new float[][]{
													new float[]{contrast, 0f, 0f, 0f, 0f},
													new float[]{0f, contrast, 0f, 0f, 0f},
													new float[]{0f, 0f, contrast, 0f, 0f},
													new float[]{0f, 0f, 0f, 1, 0f},
													new float[]{W, W, W, 2f, 1f}});
		}

		/// <summary>
		/// Returns a ColorMatrix to convert an image into grayscale
		/// </summary>
		/// <returns>ColorMatrix to convert an image into grayscale</returns>
		public static ColorMatrix Grayscale()
		{
			return new ColorMatrix(new float[][]{
													new float[]{0.299f,  0.299f,  0.299f,  0, 0},
													new float[]{0.587f, 0.587f, 0.587f, 0, 0},
													new float[]{0.114f, 0.114f, 0.114f, 0, 0},
													new float[]{0, 0, 0, 1, 0},
													new float[]{0, 0, 0, 0, 1}});
		}

		/// <summary>
		/// Returns the identity ColorMatrix, which has no effect on an image
		/// </summary>
		/// <returns>The Identity ColorMatrix</returns>
		public static ColorMatrix Identity()
		{
			return new ColorMatrix(new float[][]{
													new float[]{1f, 0f, 0f, 0f, 0f},
													new float[]{0f, 1f, 0f, 0f, 0f},
													new float[]{0f, 0f, 1f, 0f, 0f},
													new float[]{0f, 0f, 0f, 1f, 0f},
													new float[]{0f, 0f, 0f, 0f, 1f}});
		}

		/// <summary>
		/// Returns a ColorMatrix for the product of Matrix1 and Matrix2
		/// </summary>
		/// <param name="matrix1">A ColorMatrix to be multiplied</param>
		/// <param name="matrix2">A ColorMatrix to be multiplied</param>
		/// <returns>A new ColorMatrix containing the result of (Matrix1)(Matrix2)</returns>
		public static ColorMatrix Multiply(ColorMatrix matrix1, ColorMatrix matrix2)
		{
			if (matrix1 == null || matrix2 == null)
				return null;

			ColorMatrix Result = new ColorMatrix();

			for (int x = 0; x < 5; x++)
			{
				for (int y = 0; y < 5; y++)
				{
					Result[x, y] = 0;

					for (int z = 0; z < 5; z++)
					{
						Result[x, y] += matrix1[x, z] * matrix2[z, y];
					}
				}
			}

			return Result;
		}
	}
}