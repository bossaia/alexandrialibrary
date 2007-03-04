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
// $Id: FontFunctions.cs,v 1.13 2006/06/12 13:16:22 wardog_uk Exp $
//----------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Alexandria.AsciiGenerator
{
	/// <summary>
	/// Class for all general font related functions
	/// </summary>
	public abstract class FontFunctions
	{
		/// <summary>
		/// Draw the text onto a new bitmap, with the specified font
		/// </summary>
		/// <param name="text">The text to be drawn</param>
		/// <param name="font">The font to use</param>
		/// <returns>A new bitmap containing the text</returns>
		public static Bitmap DrawText(string text, Font font)
		{
			return DrawText(text, font, Color.Black, Color.White);
		}

		/// <summary>
		/// Draw the text onto a new bitmap, with the specified font and text colours
		/// </summary>
		/// <param name="text">The text to be drawn</param>
		/// <param name="font">The font to use</param>
		/// <param name="textcolor">The colour of the text</param>
		/// <param name="backgroundcolor">The colour of the background</param>
		/// <returns>A new bitmap containing the text</returns>
		public static Bitmap DrawText(string text, Font font, Color textcolor, Color backgroundcolor)
		{
			Size size = MeasureText(text, font);

			Bitmap result = new Bitmap(size.Width, size.Height);

			using (Graphics g = Graphics.FromImage(result))
			{
				g.Clear(backgroundcolor);
				TextRenderer.DrawText(g, text, font, new Point(0, 0), textcolor, backgroundcolor, TextFormatFlags.NoPadding);
			}

			return result;
		}

		/// <summary>
		/// Measure the given text when it is drawn with the specified font
		/// </summary>
		/// <param name="text">The text to measure</param>
		/// <param name="font">The font to be used</param>
		/// <returns>The size of the text</returns>
		public static Size MeasureText(string text, Font font)
		{
			Bitmap bmp = new Bitmap(1, 1);

			return TextRenderer.MeasureText(Graphics.FromImage(bmp), text, font, new Size(1, 1), TextFormatFlags.NoPadding);
		}

		/// <summary>
		/// Get the size of one character in a fixed pitch font
		/// </summary>
		/// <param name="font">Font to use</param>
		/// <returns>Size of one character, or empty size if font is not fixed pitch</returns>
		public static Size GetFixedPitchFontSize(Font font)
		{
			return IsFixedWidth(font) ? MeasureText("W", font) : Size.Empty;
		}

		/// <summary>
		/// Check if the font has a fixed width
		/// </summary>
		/// <param name="font"></param>
		/// <returns></returns>
		public static bool IsFixedWidth(Font font)
		{
			return NativeMethods.IsFixedPitch(font);
		}

		/// <summary>
		/// Reverse and return a string
		/// </summary>
		/// <param name="input">The string to be reversed</param>
		/// <returns>The reversed string</returns>
		public static string Reverse(string input)
		{
			if (input == null)
				return null;

			System.Text.StringBuilder builder = new System.Text.StringBuilder();

			for (int i = input.Length - 1; i > -1; i--)
			{
				builder.Append(input[i]);
			}

			return builder.ToString();
		}
	}
}