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

namespace AlexandriaOrg.Alexandria.Imdb
{
	public class Movie : IComparable<Movie>
	{
		public int CompareTo(Movie m)
		{
			return this.iVotes.CompareTo(m.iVotes);
		}

		public class MovieTitle
		{
			public MovieTitle(string psTitle, bool pbAKA)
			{
				sTitle = psTitle;
				bAKA = pbAKA;
			}
			public string sTitle;
			public bool bAKA;
		}

		public Movie()
		{
			Reset();
		}

		public Movie(long piCode, string psTitle)
		{
			Reset();
			iCode = piCode;
			lTitles.Add(new MovieTitle(psTitle, false));
		}

		public void Reset()
		{
			iCode = 0;
			iYear = 0;
			iRating = 0;
			iVotes = 0;
			sMPAA = "";
			lTitles = new List<MovieTitle>();
			lGenres = new List<string>();
			lDirectors = new List<Person>();
			lWriters = new List<Person>();
			lActors = new List<Person>();
			lPlots = new List<string>();
			bPoster = null;
			sPosterLink = "";
		}

		public long iCode;
		public int iYear;
		public int iRating;
		public int iVotes;
		public string sMPAA;
		public List<MovieTitle> lTitles;
		public List<string> lGenres;
		public List<Person> lDirectors;
		public List<Person> lWriters;
		public List<Person> lActors;
		public List<string> lPlots;
		public byte[] bPoster;
		public string sPosterLink;
	}
}