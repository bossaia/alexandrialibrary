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
// $Id: AsciiRampCreator.cs,v 1.1 2007/01/03 16:12:27 wardog_uk Exp $
//----------------------------------------------------------------------------
using System;
using System.Drawing;

namespace Alexandria.AsciiGenerator.AsciiConversion
{
	/// <summary>
	/// Class to handle creation of ASCII Ramps
	/// </summary>
	public abstract class AsciiRampCreator
	{
		/// <summary>
		/// Create an ASCII Ramp with from the given font and characters
		/// </summary>
		/// <param name="font">Font to be used</param>
		/// <param name="characters">The characters to be used for the ramp</param>
		/// <returns>A new ASCII ramp</returns>
		public static string CreateRamp(Font font, string characters)
		{
			if (characters == null || characters.Length < 1)
				return null;

			if (characters.Length == 1)
				return characters;

			string characterstring = "";

			foreach (char c in characters.ToCharArray())
			{
				if (characterstring.IndexOf(c) == -1)
				{
					characterstring += c.ToString();
				}
			}

			System.Collections.SortedList list = new System.Collections.SortedList();

			int min = 255;
			int max = 0;

			CharacterValue charval;

			for (int i = 0; i < characterstring.Length; i++)
			{
				charval = new CharacterValue(characterstring[i], font);

				if (list.ContainsKey(charval.Value))
				{
					if (charval.Score < ((CharacterValue)list[charval.Value]).Score)
					{
						list[charval.Value] = charval;
					}
				}
				else
				{
					if (charval.Value < min)
						min = charval.Value;

					if (charval.Value > max)
						max = charval.Value;

					list.Add(charval.Value, charval);
				}
			}

			list.TrimToSize();


			string result = "";

			System.Collections.IDictionaryEnumerator idenu = list.GetEnumerator();

			// move to the first object
			idenu.MoveNext();

			int current = (int)idenu.Key;
			int next, mid;

			// loop through and fill in the gaps
			while (idenu.MoveNext())
			{
				next = (int)idenu.Key;
				mid = ((next - current) / 2) + current;

				for (int i = current; i < mid; i++)
				{
					result += list[current];
				}

				for (int i = mid; i < next; i++)
				{
					result += list[next];
				}

				current = next;
			}

			return result;
		}
	}
}