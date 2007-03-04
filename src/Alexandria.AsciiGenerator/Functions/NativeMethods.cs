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
// $Id: NativeMethods.cs,v 1.12 2006/06/03 23:29:11 wardog_uk Exp $
//----------------------------------------------------------------------------
using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Alexandria.AsciiGenerator
{
	/// <summary>
	/// Class encapsulating system specific calls (Windows Version)
	/// </summary>
	internal class NativeMethods
	{
		/// <summary>Private constructor</summary>
		private NativeMethods() { }

		#region Publically Accessible Code
		/// <summary>
		/// Check if the font has a fixed width
		/// </summary>
		/// <param name="font">Font to check</param>
		/// <returns>true if the font has a fixed width</returns>
		public static bool IsFixedPitch(Font font)
		{
			bool result;

			IntPtr fnt = font.ToHfont();

			using (Bitmap bmp = new Bitmap(1, 1))
			{
				using (Graphics g = Graphics.FromImage(bmp))
				{
					IntPtr hdc = g.GetHdc();

					// store the current font and set the new one
					IntPtr fntOld = SelectObject(hdc, fnt);

					TEXTMETRIC metric = new TEXTMETRIC();

					NativeMethods.GetTextMetrics(hdc, ref metric);

					result = (metric.tmPitchAndFamily & TMPF_FIXED_PITCH) == 0;

					// restore the old font
					SelectObject(hdc, fntOld);

					g.ReleaseHdc(hdc);
				}
			}

			DeleteObject(fnt);

			return result;
		}

		#endregion

		#region Windows Specific Code
		/// <summary>
		/// Select an object into the specified device context
		/// </summary>
		/// <param name="hdc">Handle to DC</param>
		/// <param name="hgdiobj">Handle to the object</param>
		/// <returns>Handle to the object being replaced (if applicable)</returns>
		[DllImport("gdi32.dll", EntryPoint = "SelectObject")]
		private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

		/// <summary>
		/// Free all system resources associated with an object
		/// </summary>
		/// <param name="hobject">Handle to a logical pen, brush, font, bitmap, region, or palette</param>
		/// <returns>bool - Did the function succeed?</returns>
		[DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool DeleteObject(IntPtr hobject);

		/// <summary>
		/// Fills the specified buffer with the metrics for the currently selected font
		/// </summary>
		/// <param name="hdc">[in] Handle to the device context</param>
		/// <param name="lptm">[out] Pointer to the TEXTMETRIC structure that receives the text metrics</param>
		/// <returns>did the function succeed?</returns>
		[DllImport("gdi32.dll", EntryPoint = "GetTextMetrics")]
		private static extern bool GetTextMetrics(IntPtr hdc, ref TEXTMETRIC lptm);

		[Serializable, StructLayout(LayoutKind.Sequential)]
		public struct TEXTMETRIC
		{
			/// <summary>Specifies the height (ascent + descent) of characters</summary>
			public int tmHeight;
			/// <summary>Specifies the ascent (units above the base line) of characters</summary>
			public int tmAscent;
			/// <summary>Specifies the descent (units below the base line) of characters</summary>
			public int tmDescent;
			/// <summary>Specifies the amount of leading (space) inside the bounds set by the tmHeight member</summary>
			public int tmInternalLeading;
			/// <summary>Specifies the amount of extra leading (space) that the application adds between rows</summary>
			public int tmExternalLeading;
			/// <summary>Specifies the average width of characters in the font</summary>
			public int tmAveCharWidth;
			/// <summary>Specifies the width of the widest character in the font</summary>
			public int tmMaxCharWidth;
			/// <summary>Specifies the weight of the font</summary>
			public int tmWeight;
			/// <summary>Specifies the extra width per string that may be added to some synthesized fonts</summary>
			public int tmOverhang;
			/// <summary>Specifies the horizontal aspect of the device for which the font was designed</summary>
			public int tmDigitizedAspectX;
			/// <summary>Specifies the vertical aspect of the device for which the font was designed</summary>
			public int tmDigitizedAspectY;
			/// <summary>Specifies the value of the first character defined in the font</summary>
			public char tmFirstChar;
			/// <summary>Specifies the value of the last character defined in the font</summary>
			public char tmLastChar;
			/// <summary>Specifies the value of the character to be substituted for characters not in the font</summary>
			public char tmDefaultChar;
			/// <summary>Specifies the value of the character that will be used to define word breaks for text justification</summary>
			public char tmBreakChar;
			/// <summary>Specifies an italic font if it is nonzero</summary>
			public byte tmItalic;
			/// <summary>Specifies an underlined font if it is nonzero</summary>
			public byte tmUnderlined;
			/// <summary>Specifies a strikeout font if it is nonzero</summary>
			public byte tmStruckOut;
			/// <summary>Specifies information about the pitch, the technology, and the family of a physical font</summary>
			public byte tmPitchAndFamily;
			/// <summary>Specifies the character set of the font</summary>
			public byte tmCharSet;
		}

		private const int TMPF_FIXED_PITCH = 1;

		#endregion
	}
}