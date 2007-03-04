using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Alexandria.AsciiGenerator.AsciiConversion
{
	/// <summary>
	/// Class used to store values of characters for the AsciiRampCreator
	/// </summary>
	public class CharacterValue
	{
		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="character">Character to use</param>
		/// <param name="font">Font to use</param>
		public CharacterValue(char character, Font font)
		{
			Character = character;

			CalculateValues(font);
		}
		#endregion

		#region Private Fields
		private char character;
		private int value;
		private int score;
		#endregion

		#region Public Properties
		/// <summary>The character</summary>
		public char Character
		{
			get { return this.character; }
			set { this.character = value; }
		}

		/// <summary>The value of the character</summary>
		public int Value
		{
			get { return this.value; }
			set { this.value = value; }
		}

		/// <summary>The characters score (lower = better)</summary>
		public int Score
		{
			get { return this.score; }
			set { this.score = value; }
		}
		#endregion
		
		#region Public Methods
		/// <summary>
		/// calculate the values for the current character
		/// </summary>
		/// <param name="font">font to be used</param>
		public void CalculateValues(Font font)
		{
			int width = 4;
			int height = 4;

			using (Bitmap bmp = FontFunctions.DrawText(Character.ToString(), font))
			{
				using (Bitmap shrunk = new Bitmap(width, height))
				{
					using (Graphics g = Graphics.FromImage(shrunk))
					{
						g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

						g.Clear(Color.White);

						g.DrawImage(bmp, 0, 0, width, height);
					}

					bmp.Dispose();

					int total = 0;

					for (int y = 0; y < height; y++)
					{
						for (int x = 0; x < width; x++)
						{
							total += shrunk.GetPixel(x, y).R;
						}
					}

					Value = (int)(((float)total / (float)(width * height)) + 0.5);

					Score = 0;

					for (int y = 0; y < height; y++)
					{
						for (int x = 0; x < width; x++)
						{
							Score += Math.Max(Value, shrunk.GetPixel(x, y).R) - Math.Min(Value, shrunk.GetPixel(x, y).R);
						}
					}

					Score = (int)(((float)Score / (float)(width * height)) + 0.5);
				}
			}
		}

		/// <summary>
		/// Overridden ToString
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return Character.ToString();
		}
		#endregion		
	}
}