//---------- LICENSED UNDER LGPL PLEASE READ CAREFULLY -------------
// IMDBServices - a library that offers acces to IMDB (Home Page:  htpp://sourceforge.net/projects/imdb-services)
//    Copyright (C) 2005 Sebastian Bota (eMail:  sebutzu@gmail.com)
//    This library is free software; you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation; either version 2.1 of the License, or (at your option) any later version.
//    This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.
//    You should have received a copy of the GNU Lesser General Public License along with this library; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA 

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Alexandria.Imdb
{
	public class Person
	{
		public Person(long piCode, string psName, string psRole)
		{
			Reset();
			sName = psName;
			iCode = piCode;
			sRole = psRole;
		}

		public Person(long piCode, string psName)
		{
			Reset();
			sName = psName;
			iCode = piCode;
		}

		public Person()
		{
			Reset();
		}

		public void Reset()
		{
			sName = "";
			sBio = "";
			iCode = 0;
			hasDateOfBirth = false;
			lDirected = new List<Movie>();
			lWritten = new List<Movie>();
			lCasted = new List<Movie>();
			sRole = "";
			bHeadshot = null;
		}

		public string sName;
		public string sBio;
		public long iCode;
		public bool hasDateOfBirth;
		public DateTime dDateOfBirth;
		public List<Movie> lDirected;
		public List<Movie> lWritten;
		public List<Movie> lCasted;
		public string sRole;//only used with actors...linked to a movie...
		public byte[] bHeadshot;
	}
}