using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AlexandriaOrg.Alexandria.AsciiGenerator.AsciiConversion
{
	/// <summary>
	/// Class to convert a fixed size array into strings using the specified ASCII Ramp
	/// </summary>
	class ValuesToFixedWidthTextConverter : ValuesToTextConverter
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="ramp">Ramp used to create the output image</param>
		public ValuesToFixedWidthTextConverter(string ramp)
		{
			Ramp = ramp;
		}

		/// <summary>
		/// Convert 2d array of byte values into character strings
		/// </summary>
		/// <param name="values">2d array of values that represent the image</param>
		/// <returns>Array of strings containing the text image</returns>
		public override string[] Apply(byte[,] values)
		{
			if (values == null)
				return null;

			// store the dimensions of the array
			int ArrayWidth = values.GetLength(0);
			int ArrayHeight = values.GetLength(1);

			// array to hold the strings
			string[] Strings = new string[ArrayHeight];

			// used to build the string from the characters
			StringBuilder Builder;

			// loop for all rows
			for (int y = 0; y < ArrayHeight; y++)
			{
				Builder = new StringBuilder();

				// loop for all values in the row
				for (int x = 0; x < ArrayWidth; x++)
				{
					Builder.Append(_ValueCharacters[values[x, y]]);
				}

				// add the string to the list
				Strings[y] = Builder.ToString();
			}

			return Strings;
		}

		private string _Ramp;
		/// <summary>ASCII Ramp (Brightest to Darkest)</summary>
		public string Ramp
		{
			get
			{
				return _Ramp;
			}

			set
			{
				if (value != null && value.Length > 0)
				{
					_Ramp = value;

					_ValueCharacters = new char[256];

					float RampLength = (float)(Ramp.Length - 1);

					char[] ramparray = Ramp.ToCharArray();

					for (int x = 0; x < 256; x++)
					{
						_ValueCharacters[x] = ramparray[(int)((((float)x / 255f) * RampLength) + 0.5f)];
					}
				}
			}
		}

		/// <summary>
		/// Array mapping the value x onto the matching character from the ramp
		/// </summary>
		private char[] _ValueCharacters;
	}
}
