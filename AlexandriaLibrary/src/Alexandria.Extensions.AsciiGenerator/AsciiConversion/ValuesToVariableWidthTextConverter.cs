using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Alexandria.AsciiGenerator.AsciiConversion
{
	/// <summary>
	/// Class to convert a fixed size array into strings in a specific font using the characters set
	/// </summary>
	class ValuesToVariableWidthTextConverter : ValuesToTextConverter
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="validcharacters">Set initial string of characters used to create the output</param>
		/// <param name="font">Font to be used</param>
		public ValuesToVariableWidthTextConverter(string validcharacters, Font font)
		{
			_DiffArray = new int[256, 256];

			for (int y = 0; y < 256; y++)
			{
				for (int x = 0; x < 256; x++)
				{
					_DiffArray[x, y] = x > y ? x - y : y - x;
				}
			}

			_Font = font;

			_ValidCharacters = validcharacters;

			CreateArrays();
		}

		/// <summary>
		/// Convert 2d array of byte values into character strings
		/// </summary>
		/// <param name="values">2d array of values that represent the image</param>
		/// <returns>Array of strings containing the text image</returns>
		public override string[] Apply(byte[,] values)
		{
			if (values == null) return null;

			int ArrayWidth = values.GetLength(0);
			int ArrayHeight = values.GetLength(1);

			if (ArrayWidth < 1 || ArrayHeight < 1)
			{
				return null;
			}


			// the corresponding character position for every possible output pixel
			int[] PixelToCharPos = new int[(ArrayWidth * CharacterWidth) + _MaxCharWidth];

			for (int charpos = 0, pixelpos = 0; charpos < ArrayWidth; charpos++)
			{
				for (int z = 0; z < CharacterWidth; z++)
				{
					PixelToCharPos[pixelpos++] = charpos;
				}
			}

			for (int x = ArrayWidth * CharacterWidth; x < (ArrayWidth * CharacterWidth) + _MaxCharWidth; x++)
			{
				PixelToCharPos[x] = ArrayWidth - 1;
			}


			string[] Result = new string[ArrayHeight];
			int targetwidth = ArrayWidth * CharacterWidth;
			int charwidtharraysize = _MaxCharWidth + 1;
			int[] PostionValues = new int[charwidtharraysize];

			for (int row = 0; row < ArrayHeight; row++)
			{
				int XPosition = 0;
				StringBuilder builder = new StringBuilder();

				while (XPosition < targetwidth)
				{
					int Total = 0;

					for (int x = 1; x < charwidtharraysize; x++)
					{
						Total += values[PixelToCharPos[XPosition + x], row];

						if (_UsedWidths[x])
						{
							PostionValues[x] = _AverageArray[Total, x];
						}
					}

					CharacterValue bestfit = _BestCharacter[PostionValues[_MaxCharWidth], _MaxCharWidth];
					int bestdiff = _BestCharacterDiff[PostionValues[_MaxCharWidth], _MaxCharWidth];

					for (int x = 1; x < charwidtharraysize; x++)
					{
						if (_UsedWidths[x])
						{
							if (_BestCharacterDiff[PostionValues[x], x] < bestdiff)
							{
								bestfit = _BestCharacter[PostionValues[x], x];
								bestdiff = _BestCharacterDiff[PostionValues[x], x];
							}
						}
					}

					builder.Append(bestfit.Character);
					XPosition += bestfit.Width;
				}

				Result[row] = builder.ToString();
			}

			return Result;
		}

		/// <summary>Create and setup all the necessary arrays</summary>
		private void CreateArrays()
		{
			UpdateFontArrays();
			CreateCharacterArrays();
		}

		/// <summary>Create the arrays of best CharacterValues</summary>
		private void CreateCharacterArrays()
		{
			_BestCharacter = new CharacterValue[256, _MaxCharWidth + 1];
			_BestCharacterDiff = new int[256, _MaxCharWidth + 1];

			for (int currentwidth = 0; currentwidth < _MaxCharWidth + 1; currentwidth++)
			{
				if (_UsedWidths[currentwidth])
				{
					for (int currentvalue = 0; currentvalue < 256; currentvalue++)
					{
						CharacterValue bestcharacter = new CharacterValue();
						int bestdifference = 256;

						for (int currentcharpos = 0; currentcharpos < characters.Length; currentcharpos++)
						{
							if (characters[currentcharpos].Width == currentwidth)
							{
								int chardifference = _DiffArray[characters[currentcharpos].Value, currentvalue];

								if (chardifference < bestdifference)
								{
									bestcharacter = characters[currentcharpos];
									bestdifference = chardifference;
								}
							}
						}

						_BestCharacter[!_InvertOutput ? currentvalue : 255 - currentvalue, currentwidth] = bestcharacter;
						_BestCharacterDiff[!_InvertOutput ? currentvalue : 255 - currentvalue, currentwidth] = bestdifference;
					}
				}
			}
		}

		/// <summary>Update the arrays for the current Font</summary>
		private void UpdateFontArrays()
		{
			ArrayList newcharacters = new ArrayList();

			int min = 255;
			int max = 0;
			_MaxCharWidth = 0;

			for (int currentcharpos = 0; currentcharpos < ValidCharacters.Length; currentcharpos++)
			{
				CharacterValue current = new CharacterValue(ValidCharacters[currentcharpos], _Font);

				if (current.Value > -1)
				{
					newcharacters.Add(current);

					if (current.Value > max) max = current.Value;
					if (current.Value < min) min = current.Value;
					if (current.Width > _MaxCharWidth) _MaxCharWidth = current.Width;
				}
			}


			_AverageArray = new int[256 * (_MaxCharWidth + 1), _MaxCharWidth + 1];

			for (int x = 0; x < 256 * (_MaxCharWidth + 1); x++)
			{
				for (int y = 1; y < (_MaxCharWidth + 1); y++)
				{
					_AverageArray[x, y] = (int)(((float)x / (float)y) + 0.5f);
				}
			}


			float ratio = 255f / (float)(max - min);

			ArrayList list = new ArrayList();
			_UsedWidths = new bool[_MaxCharWidth + 1];

			foreach (CharacterValue charvalue in newcharacters)
			{
				int newvalue = (int)(((float)(charvalue.Value - min) * ratio) + 0.5f);
				list.Add(new CharacterValue(charvalue.Character, charvalue.Width, newvalue));

				_UsedWidths[charvalue.Width] = true;
			}

			newcharacters = new ArrayList();

			foreach (CharacterValue oldvalue in list)
			{
				bool match = false;

				foreach (CharacterValue charvalue in newcharacters)
				{
					if (charvalue.Value == oldvalue.Value && charvalue.Width == oldvalue.Width)
					{
						// TODO: check which character is better (covers more of the area?)

						match = true;
						break;
					}
				}

				if (!match)
				{
					newcharacters.Add(oldvalue);
				}
			}

			characters = (CharacterValue[])newcharacters.ToArray(typeof(CharacterValue));
		}

		private string _ValidCharacters;
		/// <summary>Get or set the string of characters to create the output (.Length > 1)</summary>
		public string ValidCharacters
		{
			get { return _ValidCharacters; }

			set
			{
				string uniquechars = string.Empty;

				for (int x = 0; x < value.Length; x++)
				{
					if (uniquechars.IndexOf(value[x]) == -1)
					{
						uniquechars += value[x];
					}
				}

				if (uniquechars.Length > 1)
				{
					_ValidCharacters = uniquechars;
					CreateArrays();
				}
			}
		}

		/// <summary>Font used for this object</summary>
		private Font _Font;

		/// <summary>The best fitting character for every value and width (0->255, 0->_MaxCharWidth)</summary>
		private CharacterValue[,] _BestCharacter;

		/// <summary>Difference between the matching charactervalue.value in CharacterValue[,] and the actual value</summary>
		private int[,] _BestCharacterDiff;

		/// <summary>The difference between 0-255 and 0-255</summary>
		private int[,] _DiffArray;

		/// <summary>Width of one character</summary>
		public static int CharacterWidth = 7;

		private CharacterValue[] characters;

		private bool _InvertOutput;
		/// <summary>Should the output be inverted (white text on black background)?</summary>
		public bool InvertOutput
		{
			get
			{
				return _InvertOutput;
			}

			set
			{
				_InvertOutput = value;

				CreateArrays();
			}
		}

		/// <summary>Largest size of character width</summary>
		private int _MaxCharWidth;

		/// <summary>Is this character width (0 to _MaxCharWidth) used?</summary>
		private bool[] _UsedWidths;

		/// <summary>Array of (0 to 256 * _MaxCharWidth + 1) divided by (0 to _MaxCharWidth + 1)</summary>
		private int[,] _AverageArray;

		/// <summary>Structure used to store characters and their values</summary>
		internal struct CharacterValue
		{
			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="character">The initial character for this object</param>
			/// <param name="font">Font to use</param>
			public CharacterValue(char character, Font font)
			{
				Character = character;
				Value = 0;
				Width = 0;
				CalculateValue(font);
			}

			public CharacterValue(char character, int width, int charvalue)
			{
				Character = character;
				Width = width;
				Value = charvalue;
			}

			/// <summary>
			/// Update Value for the passed font
			/// </summary>
			/// <param name="font">Font to be used</param>
			public void CalculateValue(Font font)
			{
				using (Bitmap bmp = FontFunctions.DrawText(Character.ToString(), font))
				{
					if (bmp == null)
					{
						Value = -1;
					}
					else
					{
						Width = bmp.Width;
						int total = 0;

						for (int y = 0; y < bmp.Height; y++)
						{
							for (int x = 0; x < Width; x++)
							{
								total += bmp.GetPixel(x, y).R;
							}
						}

						Value = (int)(((float)total / (float)(Width * bmp.Height)) + 0.5);
					}
				}
			}

			/// <summary>This objects character</summary>
			public char Character;

			/// <summary>The current value of the object</summary>
			public int Value;

			/// <summary>The width of the character (automatically set in CalculateValue)</summary>
			public int Width;

			/// <summary>
			/// Get the Character as a string
			/// </summary>
			/// <returns>The Character as a string</returns>
			public override string ToString()
			{
				return Character.ToString();
			}
		}
	}
}
