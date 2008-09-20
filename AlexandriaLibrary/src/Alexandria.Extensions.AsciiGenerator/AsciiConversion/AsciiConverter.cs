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
// $Id: AscgenConverter.cs,v 1.1 2007/01/03 16:12:27 wardog_uk Exp $
//----------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using Alexandria.AsciiGenerator;

namespace Alexandria.AsciiGenerator.AsciiConversion
{
	/// <summary>
	/// Static class with methods for Image to Text conversion
	/// </summary>
	public sealed class AsciiConverter
	{
		/// <summary>
		/// Empty private constructor
		/// </summary>
		private AsciiConverter()
		{
		}

		/// <summary>
		/// Convert the image into an array of strings using the passed size
		/// </summary>
		/// <remarks>Uses the default TextProcessingSettings</remarks>
		/// <param name="image">image to convert</param>
		/// <param name="size">size of the output text image</param>
		/// <returns>the converted text image</returns>
		public static string[] Convert(Image image, Size size)
		{
			if (image == null || size.Width < 1 || size.Height < 1)
				return null;

			TextProcessingSettings settings = new TextProcessingSettings();
			settings.Size = size;

			return Convert(image, settings);
		}

		/// <summary>
		/// Convert the image into an array of strings using the passed settings
		/// </summary>
		/// <param name="image">image to convert</param>
		/// <param name="settings">the text settings to use for the output image</param>
		/// <returns>the converted text image</returns>
		public static string[] Convert(Image image, TextProcessingSettings settings)
		{
			if (image == null || settings.Width < 1 || settings.Height < 1)
				return null;

			byte[,] values = ImageToTextValues(image, settings.Size);

			return settings.Apply(values);
		}

		/// <summary>
		/// Convert an Image to an OutputSize array of Byte values
		/// </summary>
		/// <param name="image">Image to convert</param>
		/// <param name="outputSize">Size of the array to be returned</param>
		/// <returns>2d array of bytes of size [OutputSize.Width, OutputSize.Height]</returns>
		public static byte[,] ImageToTextValues(Image image, Size outputSize)
		{
			if (image == null)
				return null;
			else
				return ImageToTextValues(image, outputSize, Matrices.Identity());
		}

		/// <summary>
		/// Modify and convert an Image to an OutputSize array of Byte values
		/// </summary>
		/// <param name="image">Image to convert</param>
		/// <param name="outputSize">Size of the array to be returned</param>
		/// <param name="matrix">ColorMatrix to be applied to the image</param>
		/// <returns>2d array of bytes of size [OutputSize.Width, OutputSize.Height]</returns>
		public static byte[,] ImageToTextValues(Image image, Size outputSize, ColorMatrix matrix)
		{
			if (image == null || matrix == null)
				return null;
			else
				return ImageToTextValues(image, outputSize, matrix, new Rectangle(0, 0, image.Width, image.Height));
		}

		/// <summary>
		/// Modify and convert an Image to an OutputSize array of Byte values
		/// </summary>
		/// <param name="image">Image to convert</param>
		/// <param name="outputSize">Size of the array to be returned</param>
		/// <param name="matrix">ColorMatrix to be applied to the image</param>
		/// <param name="section">The part of the image to be used</param>
		/// <returns>2d array of bytes of size [OutputSize.Width, OutputSize.Height]</returns>
		public static byte[,] ImageToTextValues(Image image, Size outputSize, ColorMatrix matrix, Rectangle section)
		{
			if (image == null || matrix == null)
				return null;

			byte[,] OutputArray;

			try
			{
				OutputArray = new byte[outputSize.Width, outputSize.Height];
			}
			catch (System.OutOfMemoryException)
			{
				return null;
			}

			using (Bitmap Resized = new Bitmap(outputSize.Width, outputSize.Height))
			{
				// draw a resized version onto the new bitmap
				using (Graphics g = Graphics.FromImage(Resized))
				{
					g.InterpolationMode = InterpolationMode.HighQualityBicubic;

					g.DrawImage(image, new Rectangle(0, 0, outputSize.Width, outputSize.Height),
						section.X, section.Y, section.Width, section.Height,
						GraphicsUnit.Pixel);
				}

				// copy the resized version onto a new bitmap, applying the matrix
				using (Bitmap Target = new Bitmap(outputSize.Width, outputSize.Height))
				{
					// create the image attributes and pass it the matrix
					using (ImageAttributes ia = new ImageAttributes())
					{
						// merge the passed matrix with the matrix for converting to greyscale
						ColorMatrix cm = Matrices.Multiply(Matrices.Grayscale(), matrix);

						ia.SetColorMatrix(cm);

						using (Graphics grap = Graphics.FromImage(Target))
						{
							grap.DrawImage(Resized, new Rectangle(0, 0, outputSize.Width, outputSize.Height),
								0, 0, outputSize.Width, outputSize.Height,
								GraphicsUnit.Pixel, ia);
						}
					}

					// loop for all rows
					for (int y = 0; y < outputSize.Height; y++)
					{
						// loop for all pixels in the row
						for (int x = 0; x < outputSize.Width; x++)
						{
							// TODO: Check overhead, use unsafe code?
							// get and store the R component of the pixel (R=G=B)
							OutputArray[x, y] = Target.GetPixel(x, y).R;
						}
					}
				}
			}

			// return the array
			return OutputArray;
		}

		/// <summary>
		/// Convert 2d array of byte values into character strings using the supplied ASCII Ramp
		/// </summary>
		/// <param name="values">2d array of values that represent the image</param>
		/// <param name="ramp">The characters used to represent the numbers (Darkest->Lightest)</param>
		/// <returns>Array of strings containing the text image</returns>
		public static string[] TextValuesToStrings(byte[,] values, string ramp)
		{
			if (ramp == null || values == null || ramp.Length == 0)
				return null;

			// store the dimensions of the array
			int ArrayWidth = values.GetLength(0);
			int ArrayHeight = values.GetLength(1);

			// array to hold the strings
			string[] Strings = new string[ArrayHeight];

			// used to build the string from the characters
			System.Text.StringBuilder Builder;

			float RampLength = (float)(ramp.Length - 1);

			char[] ramparray = ramp.ToCharArray();

			// loop for all rows
			for (int y = 0; y < ArrayHeight; y++)
			{
				Builder = new System.Text.StringBuilder();

				// loop for all values in the row
				for (int x = 0; x < ArrayWidth; x++)
				{
					// calculate the character value
					Builder.Append(ramparray[(int)((((float)values[x, y] / 255f) * RampLength) + 0.5)]);
				}

				// add the string to the list
				Strings[y] = Builder.ToString();
			}

			return Strings;
		}

		/// <summary>
		/// Run a sharpening matrix over the values
		/// </summary>
		/// <param name="values">Values to be sharpened</param>
		/// <returns>Array containing the sharpened values</returns>
		public static byte[,] SharpenValues(byte[,] values)
		{
			// sharpen
			ConvolutionMatrix matrix = new ConvolutionMatrix(
				0, -2, 0,
				-2, 11, -2,
				0, -2, 0);

			matrix.Factor = 3;

			return ConvolutionMatrix.Apply(values, matrix);
		}

		/// <summary>
		/// Run a blur matrix over the values
		/// </summary>
		/// <param name="values">Values to be blurred</param>
		/// <returns>Array containing the blurred values</returns>
		public static byte[,] BlurValues(byte[,] values)
		{
			ConvolutionMatrix matrix = new ConvolutionMatrix(
				1, 2, 1,
				2, 4, 2,
				1, 2, 1);

			matrix.Factor = 16;

			return ConvolutionMatrix.Apply(values, matrix);
		}

		/// <summary>
		/// Run a unsharp mask over the values
		/// </summary>
		/// <param name="values">Values to be processed</param>
		/// <returns>Array containing the new values</returns>
		public static byte[,] UnsharpMaskValues(byte[,] values)
		{
			return UnsharpMaskValues(values, 2);
		}

		/// <summary>
		/// Run a unsharp mask over the values
		/// </summary>
		/// <param name="values">Values to be processed</param>
		/// <param name="numberOfBlurs">Number of times to blur for the mask</param>
		/// <returns>Array containing the new values</returns>
		public static byte[,] UnsharpMaskValues(byte[,] values, int numberOfBlurs)
		{
			if (values == null)
				return null;

			byte[,] Result = (byte[,])values.Clone();

			for (int i = 0; i < numberOfBlurs; i++)
			{
				Result = BlurValues(Result);
			}

			int ArrayWidth = values.GetLength(0);
			int ArrayHeight = values.GetLength(1);

			int iNewValue;

			for (int y = 0; y < ArrayHeight; y++)
			{
				for (int x = 0; x < ArrayWidth; x++)
				{
					iNewValue = (values[x, y] * 2) - Result[x, y];

					if (iNewValue > 255) iNewValue = 255;
					if (iNewValue < 0) iNewValue = 0;

					Result[x, y] = (byte)iNewValue;
				}
			}

			return Result;
		}

		/// <summary>
		/// Calculate the other dimension (b) from the known dimension (a)
		/// </summary>
		/// <remarks>i.e. if width is known pass widths for all (a) values, and heights for all (b) values</remarks>
		/// <param name="dimension">the known dimension (a)</param>
		/// <param name="imageDimension">image dimension (a)</param>
		/// <param name="otherImageDimension">image dimension (b)</param>
		/// <param name="characterDimension">character dimension (a) - dimension of one character in the font</param>
		/// <param name="otherCharacterDimension">character dimension (b) - dimension of one character in the font</param>
		/// <returns>the other dimension (b)</returns>
		public static int CalculateOtherDimension(int dimension, int imageDimension, int otherImageDimension,
				int characterDimension, int otherCharacterDimension)
		{
			if (otherImageDimension == 0 || otherCharacterDimension == 0) return 0;

			int result = 0;

			// Catch strange 150 / 7 = DivideByZeroException that was reported
			try
			{
				float fRatio = (float)imageDimension / (float)otherImageDimension;
				float PixelDimension = (float)(dimension * characterDimension) / fRatio;
				result = (int)((PixelDimension / (float)otherCharacterDimension) + 0.5);
			}
			catch (System.DivideByZeroException)
			{
			}

			return result;
		}

		/// <summary>
		/// Save the text as an image file, will overwrite it if the file already exists
		/// </summary>
		/// <param name="text">The text to save</param>
		/// <param name="filename">Filename to save</param>
		/// <param name="font">Font to use for the text</param>
		/// <param name="textcolor">Color of the text</param>
		/// <param name="backgroundcolor">Color of the background</param>
		/// <param name="scale">Percentage scale of the image, 1.0-100.0</param>
		/// <param name="greyscale">Save the image as greyscale?</param>
		/// <returns>did the file save without errors?</returns>
		public static bool SaveTextAsImage(string text, string filename, Font font, Color textcolor, Color backgroundcolor, float scale, bool greyscale)
		{
			System.Guid format;

			switch (System.IO.Path.GetExtension(filename).ToLower())
			{
				case ".png":
					format = ImageFormat.Png.Guid;
					break;
				case ".jpg":
				case ".jpeg":
				case ".jpe":
					format = ImageFormat.Jpeg.Guid;
					break;
				case ".gif":
					format = ImageFormat.Gif.Guid;
					break;
				case ".tif":
				case ".tiff":
					format = ImageFormat.Tiff.Guid;
					break;
				case ".bmp":
				case ".rle":
				case ".dib":
				default:
					format = ImageFormat.Bmp.Guid;
					break;
			}

			using (Bitmap bmpFullSize = FontFunctions.DrawText(text, font, textcolor, backgroundcolor))
			{
				if (scale < 100f)
				{
					float fMagnification = scale / 100f;

					Size newSize = new Size((int)((bmpFullSize.Width * fMagnification) + 0.5),
						(int)((bmpFullSize.Height * fMagnification) + 0.5));

					if (newSize.Width < 1) newSize.Width = 1;
					if (newSize.Height < 1) newSize.Height = 1;

					using (Bitmap bmpOutput = new Bitmap(newSize.Width, newSize.Height))
					{
						using (ImageAttributes ia = new ImageAttributes())
						{
							ia.SetColorMatrix(greyscale ? Matrices.Grayscale() : Matrices.Identity());

							using (Graphics g = Graphics.FromImage(bmpOutput))
							{
								g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

								g.DrawImage(bmpFullSize, new Rectangle(0, 0, newSize.Width, newSize.Height),
									0, 0, bmpFullSize.Width, bmpFullSize.Height,
									GraphicsUnit.Pixel, ia);
							}
						}

						bmpOutput.Save(filename, new ImageFormat(format));
					}
				}
				else
				{
					bmpFullSize.Save(filename, new ImageFormat(format));
				}
			}

			return true;
		}

		/// <summary>
		/// Process the specified section of the passed image into an outputSize array of Colors
		/// </summary>
		/// <param name="image">Source image</param>
		/// <param name="outputSize">Size of the output array</param>
		/// <param name="section">Section of the image to use</param>
		/// <param name="reducecolors">Reduce the number of colors to no more then 256?</param>
		/// <returns>An outputSize array of Colors</returns>
		public static Color[,] ImageToColors(Image image, Size outputSize, Rectangle section, bool reducecolors)
		{
			Color[,] OutputArray;

			try
			{
				OutputArray = new Color[outputSize.Width, outputSize.Height];
			}
			catch (System.OutOfMemoryException)
			{
				return null;
			}

			string tempfilename = System.IO.Path.GetTempFileName();

			// create the resized and cropped image
			using (Bitmap Resized = new Bitmap(outputSize.Width, outputSize.Height))
			{
				using (Graphics g = Graphics.FromImage(Resized))
				{
					g.InterpolationMode = InterpolationMode.HighQualityBicubic;

					g.DrawImage(image, new Rectangle(0, 0, outputSize.Width, outputSize.Height),
						section.X, section.Y, section.Width, section.Height,
						GraphicsUnit.Pixel);
				}

				if (reducecolors)
				{
					// save as gif
					Resized.Save(tempfilename, System.Drawing.Imaging.ImageFormat.Gif);
				}
				else
				{
					for (int y = 0; y < outputSize.Height; y++)
					{
						for (int x = 0; x < outputSize.Width; x++)
						{
							OutputArray[x, y] = Resized.GetPixel(x, y);
						}
					}
				}
			}

			if (reducecolors)
			{
				// load into an image
				using (Image gif = Image.FromFile(tempfilename))
				{
					// fill the output array with the Colors
					for (int y = 0; y < outputSize.Height; y++)
					{
						for (int x = 0; x < outputSize.Width; x++)
						{
							OutputArray[x, y] = ((Bitmap)gif).GetPixel(x, y);
						}
					}
				}

				try
				{
					// cleanup the temp file
					System.IO.File.Delete(tempfilename);
				}
				catch
				{
				}
			}

			return OutputArray;
		}
	}
}