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
// $Id: Variables.cs,v 1.57 2007/01/26 20:01:08 wardog_uk Exp $
//----------------------------------------------------------------------------

using System;
using System.Collections;
using System.Globalization;

namespace Alexandria.AsciiGenerator
{
	public abstract class Variables
	{
		/// <summary>The program name</summary>
		public const string ProgramName = "ASCII Generator dotNET";

		/// <summary>The current culture of the program</summary>
		public static CultureInfo Culture = CultureInfo.InvariantCulture;

		/// <summary>Default output width (-1 to autocalculate from height, but only if DefaultHeight > 0)</summary>
		public static int DefaultWidth = 150;

		/// <summary>Default output height (-1 to autocalculate from width, but only if DefaultWidth > 0)</summary>
		public static int DefaultHeight = -1;

		/// <summary>Default value for whether the output size should be loaded from the settings</summary>
		public static bool LoadOutputSize = true;

		/// <summary>The default font to be used for conversions</summary>
		public static System.Drawing.Font DefaultFont = new System.Drawing.Font("Lucida Console", 9f);

		/// <summary>Default settings used for all ASCII ramp valid character strings</summary>
		public static ArrayList DefaultValidCharacters = new ArrayList(
			new string[] {
                " #,.0123456789:;@ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz$",
                " ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz",
                " 1234567890",
                "M@WB08Za2SX7r;i:;. ",
                "@#MBHAGh93X25Sisr;:, ",
                "█▓▒░ "}
			);

		/// <summary>The default selected string from DefaultValidCharacters (0 to DefaultValidCharacters.Length - 1)</summary>
		public static int CurrentSelectedValidCharacters = 0;

		/// <summary>The ramp used if CurrentSelectedValidCharacters is -1 (not in the list)</summary>
		public static string CurrentCharacters = String.Empty;

		/// <summary>Default brightness used for the input image (-200 to 200)</summary>
		public static int DefaultImageBrightness = 0;

		/// <summary>Default contrast used for the input image (-100 to 100)</summary>
		public static int DefaultImageContrast = 0;

		/// <summary>Default value for whether brightness and contrast should be loaded from the settings</summary>
		public static bool LoadImageBrightnessContrast = false;

		/// <summary>Default brightness used for the output text (-200 to 200)</summary>
		public static int DefaultTextBrightness = 0;

		/// <summary>Default contrast used for the output text (-100 to 100)</summary>
		public static int DefaultTextContrast = 0;

		/// <summary>Default value for whether the text brightness and contrast should be loaded from the settings</summary>
		public static bool LoadTextBrightnessContrast = false;

		/// <summary>Default minimum level value for the output text (0 to 253)</summary>
		public static int DefaultMinLevel = 0;

		/// <summary>Default median level value for the output text (0.0 to 1.0)</summary>
		public static float DefaultMedianLevel = 0.5f;

		/// <summary>Default maximum level value for the output text (2 to 255)</summary>
		public static int DefaultMaxLevel = 255;

		/// <summary>Default value for whether the levels should be loaded from the settings</summary>
		public static bool LoadLevels = false;

		/// <summary>Default value for whether the output should be stretched</summary>
		public static bool Stretch = true;

		/// <summary>Default value for whether the output should be sharpened</summary>
		public static bool Sharpen = false;

		/// <summary>Default value for whether the output should be sharpened with an unsharp mask</summary>
		public static bool UnsharpMask = true;

		/// <summary>Default value for whether the output should be flipped horizontally</summary>
		public static bool FlipHorizontally = false;

		/// <summary>Default value for whether the output should be flipped vertically</summary>
		public static bool FlipVertically = false;

		/// <summary>Default value for whether the output is black text on a white background</summary>
		public static bool BlackTextOnWhite = true;

		/// <summary>Default value for whether we should automatically generate a ramp</summary>
		public static bool UseGeneratedRamp = true;

		/// <summary>Default list of ramps</summary>
		public static ArrayList DefaultRamps = new ArrayList(
			new string[] {
                "MMMMMMM@@@@@@@WWWWWWWWWBBBBBBBB000000008888888ZZZZZZZZZaZaaaaaa2222222SSSSSSSXXXXXXXXXXX7777777rrrrrrr;;;;;;;;iiiiiiiii:::::::,:,,,,,,.........       ",
				"@@@@@@@######MMMBBHHHAAAA&&GGhh9933XXX222255SSSiiiissssrrrrrrr;;;;;;;;:::::::,,,,,,,........        ",
				"#WMBRXVYIti+=;:,. ",
				"##XXxxx+++===---;;,,...    ",
				"@%#*+=-:. ",
				"#¥¥®®ØØ$$ø0oo°++=-,.    ",
				"# ",
				"01 ",
                "█▓▒░ "}
			);

		/// <summary>The default selected string from DefaultRamps (0 to DefaultRamps.Length - 1)</summary>
		public static int CurrentSelectedRamp = 0;

		/// <summary>The ramp used if CurrentSelectedRamp is -1 (not in the list)</summary>
		public static string CurrentRamp = String.Empty;

		/// <summary>Default directory to use when loading an image (empty string = use current directory)</summary>
		public static string InitialInputDirectory = String.Empty;

		/// <summary>Default directory to use when saving (empty string = use current directory)</summary>
		public static string InitialOutputDirectory = String.Empty;

		/// <summary>Text added to the beginning of the output filenames</summary>
		public static string Prefix = "ASCII-";

		/// <summary>Default value for if the user should be asked to confirm when closing an unsaved image</summary>
		public static bool ConfirmOnClose = true;

		/// <summary>Default value for if the program will check the website for a new version</summary>
		public static bool CheckForNewVersions = true;
	}
}
