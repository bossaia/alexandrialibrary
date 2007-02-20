//---------- LICENSED UNDER LGPL PLEASE READ CAREFULLY -------------
// IMDBServices - a library that offers acces to IMDB (Home Page:  htpp://sourceforge.net/projects/imdb-services)
//    Copyright (C) 2005 Sebastian Bota (eMail:  sebutzu@gmail.com)
//    This library is free software; you can redistribute it and/or modify it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation; either version 2.1 of the License, or (at your option) any later version.
//    This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.
//    You should have received a copy of the GNU Lesser General Public License along with this library; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA 

using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.Imdb
{
	public class Cache
	{
		Movie bmd;
		public ImdbParser imdb;

		public bool Update()
		{
			Movie tmpbmd = null;// imdb.GetBasicMovieData();
			if (tmpbmd == null)
			{
				return false;
			}
			//SaveCache(tmpbmd)

			bmd = tmpbmd;
			return true;
		}

		public bool Load()
		{
			//bmd = LoadCache();
			if (bmd == null)
			{
				return false;
			}
			return true;
		}

		public List<Movie> SearchMovie(String TitlePart)
		{
			//BasicMovie result = new BasicMovie();

			//Search the damn TitlePart inside bmd;
			//fill results in result

			return null;
		}

		public Movie GetMovieDetails(Int64 MovieCode)
		{
			return null;
		}
		
		public Person GetPersonDetails(Int64 PersonCode)
		{
			return null;
		}
	}
}