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
// $Id: Resource.cs,v 1.1 2005/06/02 13:06:38 wardog_uk Exp $
//----------------------------------------------------------------------------
using System;
using System.Resources;

namespace AlexandriaOrg.Alexandria.AsciiGenerator
{
	/// <summary>
	/// Class to handle accessing of the programs resources
	/// </summary>
	public abstract class Resource
	{
		/// <summary>
		/// The root location of the localization resources
		/// </summary>
		/*
		public static string Location = "AscGenDotNet.Resources.Localization.Localization";
		*/

		/// <summary>
		/// Get the string named 'key' from the resource file
		/// </summary>
		/// <param name="key">Name of the string to return</param>
		/// <returns>Specified string value from the resource file</returns>
		/*
		public static string GetString(string key)
		{
			ResourceManager _ResourceManager =
				new ResourceManager(Location,
				System.Reflection.Assembly.GetExecutingAssembly());

			return _ResourceManager.GetString(key, Variables.Culture);
		}
		*/
	}
}