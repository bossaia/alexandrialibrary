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
	public class ImdbMetadataProvider
	{
		public string FileCachePath
		{
			get { return imdb.FileCachePath; }
			set { imdb.FileCachePath = value; }
		}

		public AsyncProcessor processor;


		public bool Offline = false;
		public bool UseCache = false;
		public Int32 CacheExpireInterval = 60;
		public Int32 MovieExpireInterval = 120;
		public Int32 PersonExpireInterval = 60;
		private Cache cache = new Cache();
		private ImdbParser imdb = new ImdbParser();

		public ImdbMetadataProvider()
		{
			processor = new AsyncProcessor(10, imdb);
			cache.imdb = imdb;
		}
		
		public List<Movie> GetBasicMovieData()
		{
			return imdb.GetBasicMovieData();
		}

		public bool UpdateCache()
		{
			if (Offline || !UseCache)
			{
				return false;
			}
			return cache.Update();
		}

		public List<Movie> SearchMovie(String TitlePart)
		{
			if (UseCache)
			{
				return cache.SearchMovie(TitlePart);
			}
			if (Offline)
			{
				return null;
			}
			return imdb.SearchMovie(TitlePart);
		}

		public bool Expired(Movie md)
		{
			//return true if md expired

			return false;
		}
		public bool Expired(Person pd)
		{
			//return true if pd expired

			return false;
		}

		public Movie GetMovieDetails(Int64 MovieCode)
		{
			Movie md = null;
			if (UseCache)
			{
				md = cache.GetMovieDetails(MovieCode);
				if (md != null)
				{
					if ((Expired(md) && Offline) || !Expired(md))
					{
						return md;
					}
				}
			}
			if (Offline)
			{
				return null;
			}

			Movie result = imdb.GetMovieDetails(MovieCode);
			if (result == null)
			{
				return md;
			}

			return result;
		}

		public Person GetPersonDetails(Int64 PersonCode)
		{
			Person pd = null;
			if (UseCache)
			{
				pd = cache.GetPersonDetails(PersonCode);
				if (pd != null)
				{
					if ((Expired(pd) && Offline) || !Expired(pd))
					{
						return pd;
					}
				}
			}
			if (Offline)
			{
				return null;
			}

			Person result = imdb.GetPersonDetails(PersonCode);
			if (result == null)
			{
				return pd;
			}

			return result;

		}
	}


}
