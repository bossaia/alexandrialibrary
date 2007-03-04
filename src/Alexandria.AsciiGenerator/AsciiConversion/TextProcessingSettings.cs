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
// $Id: TextProcessingSettings.cs,v 1.1 2007/01/03 16:12:27 wardog_uk Exp $
//----------------------------------------------------------------------------
using System;
using Alexandria.AsciiGenerator.AsciiConversion.Filters;

namespace Alexandria.AsciiGenerator.AsciiConversion
{
	/// <summary>
	/// Summary description for ConversionSettings.
	/// </summary>
	public class TextProcessingSettings
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public TextProcessingSettings()
		{
			Font = Variables.DefaultFont;
		}

		/// <summary>
		/// Apply the current text settings to the values
		/// </summary>
		/// <param name="values">values array to be used</param>
		/// <returns>Array of strings from the values using the current settings</returns>
		public string[] Apply(byte[,] values)
		{
			if (values == null)
			{
				return null;
			}

			// only apply brightness and contrast if necessary
			if (Brightness != 0 || Contrast != 0)
			{
				BrightnessContrast filter = new BrightnessContrast(IsBlackTextOnWhite ? Brightness : -Brightness,
					IsBlackTextOnWhite ? Contrast : -Contrast);

				values = filter.Apply(values);
			}

			if (Stretch)
			{
				Stretch filter = new Stretch();
				values = filter.Apply(values);
			}

			if (Levels.Minimum != 0 || Levels.Maximum != 255 || Levels.Median != 0.5f)
			{
				//values = AscgenConverter.ApplyLevels(values, Levels.Minimum, Levels.Maximum, Levels.Median);

				Levels filter = new Levels(Levels.Minimum, Levels.Maximum, Levels.Median);
				values = filter.Apply(values);
			}

			if (Sharpen)
			{
				values = AsciiConverter.SharpenValues(values);
			}

			if (Unsharp)
			{
				values = AsciiConverter.UnsharpMaskValues(values, 3);
			}

			if (FlipHorizontally || FlipVertically)
			{
				Flip filter = new Flip(FlipHorizontally, FlipVertically);
				values = filter.Apply(values);
			}

			return _ValuesToText.Apply(values);
		}

		private ValuesToTextConverter _ValuesToText;

		private int _Width = Variables.DefaultWidth;
		/// <summary>Width of the output image</summary>
		public int Width
		{
			get
			{
				return _Width;
			}

			set
			{
				_Width = value;
			}
		}

		private int _Height = Variables.DefaultHeight;
		/// <summary>Height of the output image</summary>
		public int Height
		{
			get
			{
				return _Height;
			}

			set
			{
				_Height = value;
			}
		}

		/// <summary>Size of the output image (accesses Width and Height values)</summary>
		public System.Drawing.Size Size
		{
			get
			{
				return new System.Drawing.Size(Width, Height);
			}

			set
			{
				Width = value.Width;
				Height = value.Height;
			}
		}

		private int _Brightness = Variables.DefaultTextBrightness;
		/// <summary>Brightness of the text image</summary>
		public int Brightness
		{
			get
			{
				return _Brightness;
			}

			set
			{
				_Brightness = value;
			}
		}

		private int _Contrast = Variables.DefaultTextContrast;
		/// <summary>Contrast of the text image</summary>
		public int Contrast
		{
			get
			{
				return _Contrast;
			}

			set
			{
				_Contrast = value;
			}
		}

		/// <summary>Class to hold values for the levels</summary>
		public class LevelsStruct
		{
			/// <summary>
			/// Constructor
			/// </summary>
			public LevelsStruct()
			{
			}

			/// <summary>
			/// Constructor with custom values
			/// </summary>
			/// <param name="min">Minimum Value</param>
			/// <param name="med">Median Value</param>
			/// <param name="max">Maximum Value</param>
			public LevelsStruct(int min, float med, int max)
			{
				Minimum = min;
				Median = med;
				Maximum = max;
			}

			private int _Minimum = Variables.DefaultMinLevel;
			/// <summary>Minimum Value</summary>
			public int Minimum
			{
				get { return _Minimum; }
				set { _Minimum = value; }
			}

			private float _Median = Variables.DefaultMedianLevel;
			/// <summary>Median Value</summary>
			public float Median
			{
				get { return _Median; }
				set { _Median = value; }
			}

			private int _Maximum = Variables.DefaultMaxLevel;
			/// <summary>Maximum Value</summary>
			public int Maximum
			{
				get { return _Maximum; }
				set { _Maximum = value; }
			}
		}

		private LevelsStruct _Levels = new LevelsStruct();
		/// <summary>Levels values for the text image</summary>
		public LevelsStruct Levels
		{
			get { return _Levels; }
			set { _Levels = value; }
		}

		private bool _Stretch = Variables.Stretch;
		/// <summary>Stretch the image?</summary>
		public bool Stretch
		{
			get
			{
				return _Stretch;
			}

			set
			{
				_Stretch = value;
			}
		}

		private bool _Sharpen = Variables.Sharpen;
		/// <summary>Sharpen the image?</summary>
		public bool Sharpen
		{
			get
			{
				return _Sharpen;
			}

			set
			{
				_Sharpen = value;
			}
		}

		private bool _Unsharp = Variables.UnsharpMask;
		/// <summary>Use an unsharpening mask on the image?</summary>
		public bool Unsharp
		{
			get
			{
				return _Unsharp;
			}

			set
			{
				_Unsharp = value;
			}
		}

		private System.Drawing.Font _Font;
		/// <summary>The current font</summary>
		public System.Drawing.Font Font
		{
			get
			{
				return _Font;
			}

			set
			{
				_Font = value;

				IsFixedWidth = FontFunctions.IsFixedWidth(_Font);

				if (CalculateCharacterSize)
				{
					CalculateCharacterSize = true;
				}
			}
		}

		private string _Ramp = "MMMMMMM@@@@@@@WWWWWWWWWBBBBBBBB000000008888888ZZZZZZZZZaZaaaaaa2222222" +
				"SSSSSSSXXXXXXXXXXX7777777rrrrrrr;;;;;;;;iiiiiiiii:::::::,:,,,,,,.........       ";
		/// <summary>The ASCII ramp used for the image (black->white)</summary>
		public string Ramp
		{
			get
			{
				return _Ramp;
			}

			set
			{
				if (value.Length > 0)
				{
					_Ramp = value;

					if (_ValuesToText.GetType() == typeof(ValuesToFixedWidthTextConverter))
					{
						((ValuesToFixedWidthTextConverter)_ValuesToText).Ramp =
							IsBlackTextOnWhite ? _Ramp : FontFunctions.Reverse(_Ramp);
					}
				}
			}
		}

		private bool _IsGeneratedRamp = Variables.UseGeneratedRamp;
		/// <summary>Has the ramp be automatically generated?</summary>
		public bool IsGeneratedRamp
		{
			get
			{
				return _IsGeneratedRamp;
			}

			set
			{
				_IsGeneratedRamp = value;
			}
		}

		private string _ValidCharacters = Variables.CurrentSelectedValidCharacters > -1 ?
			(string)Variables.DefaultValidCharacters[Variables.CurrentSelectedValidCharacters] :
			Variables.CurrentCharacters;
		/// <summary>The characters used to create the ASCII ramp</summary>
		public string ValidCharacters
		{
			get
			{
				return _ValidCharacters;
			}

			set
			{
				_ValidCharacters = value;

				if (_ValuesToText.GetType() == typeof(ValuesToVariableWidthTextConverter))
				{
					((ValuesToVariableWidthTextConverter)_ValuesToText).ValidCharacters = _ValidCharacters;
				}
			}
		}

		private bool _IsFixedWidth = true;
		/// <summary>Get or set if the current font is fixed width</summary>
		public bool IsFixedWidth
		{
			get { return _IsFixedWidth; }

			set
			{
				_IsFixedWidth = value;

				if (_IsFixedWidth)
				{
					_ValuesToText = new ValuesToFixedWidthTextConverter(Ramp);
				}
				else
				{
					_ValuesToText = new ValuesToVariableWidthTextConverter(ValidCharacters, Font);

					if (!IsBlackTextOnWhite)
					{
						((ValuesToVariableWidthTextConverter)_ValuesToText).InvertOutput = true;
					}
				}
			}
		}

		private bool _IsBlackTextOnWhite = Variables.BlackTextOnWhite;
		/// <summary>Is the image black text on a white background?</summary>
		public bool IsBlackTextOnWhite
		{
			get
			{
				return _IsBlackTextOnWhite;
			}

			set
			{
				if (_IsBlackTextOnWhite != value)
				{
					_IsBlackTextOnWhite = value;

					if (_ValuesToText.GetType() == typeof(ValuesToFixedWidthTextConverter))
					{
						((ValuesToFixedWidthTextConverter)_ValuesToText).Ramp =
							IsBlackTextOnWhite ? Ramp : FontFunctions.Reverse(Ramp);
					}
					else if (_ValuesToText.GetType() == typeof(ValuesToVariableWidthTextConverter))
					{
						((ValuesToVariableWidthTextConverter)_ValuesToText).InvertOutput = !_IsBlackTextOnWhite;
					}
				}
			}
		}

		private bool _FlipHorizontally = Variables.FlipHorizontally;
		/// <summary>Should the image be flipped horizontally?</summary>
		public bool FlipHorizontally
		{
			get
			{
				return _FlipHorizontally;
			}

			set
			{
				_FlipHorizontally = value;
			}
		}

		private bool _FlipVertically = Variables.FlipVertically;
		/// <summary>Should the image be flipped vertically?</summary>
		public bool FlipVertically
		{
			get
			{
				return _FlipVertically;
			}

			set
			{
				_FlipVertically = value;
			}
		}

		private bool _CalculateCharacterSize = true;
		/// <summary>Should the character size be automatically calculated?</summary>
		public bool CalculateCharacterSize
		{
			get
			{
				return _CalculateCharacterSize;
			}

			set
			{
				_CalculateCharacterSize = value;

				System.Drawing.Size size = FontFunctions.MeasureText("W", Font);

				if (_CalculateCharacterSize)
				{
					if (!IsFixedWidth)
					{
						size.Width = ValuesToVariableWidthTextConverter.CharacterWidth;
					}

					_CharacterSize = size;
				}
			}
		}

		private System.Drawing.Size _CharacterSize;
		/// <summary>Size of one character in the font</summary>
		public System.Drawing.Size CharacterSize
		{
			get
			{
				return _CharacterSize;
			}

			set
			{
				_CharacterSize = value;
				CalculateCharacterSize = false;
			}
		}
	}
}