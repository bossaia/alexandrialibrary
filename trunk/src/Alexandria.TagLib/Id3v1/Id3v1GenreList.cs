/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : id3v1genres.cpp from TagLib
 ***************************************************************************/

/***************************************************************************
 *   This library is free software; you can redistribute it and/or modify  *
 *   it  under the terms of the GNU Lesser General Public License version  *
 *   2.1 as published by the Free Software Foundation.                     *
 *                                                                         *
 *   This library is distributed in the hope that it will be useful, but   *
 *   WITHOUT ANY WARRANTY; without even the implied warranty of            *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU     *
 *   Lesser General Public License for more details.                       *
 *                                                                         *
 *   You should have received a copy of the GNU Lesser General Public      *
 *   License along with this library; if not, write to the Free Software   *
 *   Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  *
 *   USA                                                                   *
 ***************************************************************************/

using System;
using System.Collections.Generic;

namespace Alexandria.TagLib
{
	public static class Id3v1GenreList
	{
		#region Private Static Fields
		private static readonly List<string> genres = new List<string>();
		#endregion
      
		#region Public Static Properties
		public static System.Collections.ObjectModel.ReadOnlyCollection<string> Genres
		{
			get
			{
				if (genres.Count == 0)
				{										
					genres.Add("Blues");
					genres.Add("Classic Rock");
					genres.Add("Country");
					genres.Add("Dance");
					genres.Add("Disco");
					genres.Add("Funk");
					genres.Add("Grunge");
					genres.Add("Hip-Hop");
					genres.Add("Jazz");
					genres.Add("Metal");
					genres.Add("New Age");
					genres.Add("Oldies");
					genres.Add("Other");
					genres.Add("Pop");
					genres.Add("R&B");
					genres.Add("Rap");
					genres.Add("Reggae");
					genres.Add("Rock");
					genres.Add("Techno");
					genres.Add("Industrial");
					genres.Add("Alternative");
					genres.Add("Ska");
					genres.Add("Death Metal");
					genres.Add("Pranks");
					genres.Add("Soundtrack");
					genres.Add("Euro-Techno");
					genres.Add("Ambient");
					genres.Add("Trip-Hop");
					genres.Add("Vocal");
					genres.Add("Jazz+Funk");
					genres.Add("Fusion");
					genres.Add("Trance");
					genres.Add("Classical");
					genres.Add("Instrumental");
					genres.Add("Acid");
					genres.Add("House");
					genres.Add("Game");
					genres.Add("Sound Clip");
					genres.Add("Gospel");
					genres.Add("Noise");
					genres.Add("Alternative Rock");
					genres.Add("Bass");
					genres.Add("Soul");
					genres.Add("Punk");
					genres.Add("Space");
					genres.Add("Meditative");
					genres.Add("Instrumental Pop");
					genres.Add("Instrumental Rock");
					genres.Add("Ethnic");
					genres.Add("Gothic");
					genres.Add("Darkwave");
					genres.Add("Techno-Industrial");
					genres.Add("Electronic");
					genres.Add("Pop-Folk");
					genres.Add("Eurodance");
					genres.Add("Dream");
					genres.Add("Southern Rock");
					genres.Add("Comedy");
					genres.Add("Cult");
					genres.Add("Gangsta");
					genres.Add("Top 40");
					genres.Add("Christian Rap");
					genres.Add("Pop/Funk");
					genres.Add("Jungle");
					genres.Add("Native American");
					genres.Add("Cabaret");
					genres.Add("New Wave");
					genres.Add("Psychedelic");
					genres.Add("Rave");
					genres.Add("Showtunes");
					genres.Add("Trailer");
					genres.Add("Lo-Fi");
					genres.Add("Tribal");
					genres.Add("Acid Punk");
					genres.Add("Acid Jazz");
					genres.Add("Polka");
					genres.Add("Retro");
					genres.Add("Musical");
					genres.Add("Rock & Roll");
					genres.Add("Hard Rock");
					genres.Add("Folk");
					genres.Add("Folk/Rock");
					genres.Add("National Folk");
					genres.Add("Swing");
					genres.Add("Fusion");
					genres.Add("Bebob");
					genres.Add("Latin");
					genres.Add("Revival");
					genres.Add("Celtic");
					genres.Add("Bluegrass");
					genres.Add("Avantgarde");
					genres.Add("Gothic Rock");
					genres.Add("Progressive Rock");
					genres.Add("Psychedelic Rock");
					genres.Add("Symphonic Rock");
					genres.Add("Slow Rock");
					genres.Add("Big Band");
					genres.Add("Chorus");
					genres.Add("Easy Listening");
					genres.Add("Acoustic");
					genres.Add("Humour");
					genres.Add("Speech");
					genres.Add("Chanson");
					genres.Add("Opera");
					genres.Add("Chamber Music");
					genres.Add("Sonata");
					genres.Add("Symphony");
					genres.Add("Booty Bass");
					genres.Add("Primus");
					genres.Add("Porn Groove");
					genres.Add("Satire");
					genres.Add("Slow Jam");
					genres.Add("Club");
					genres.Add("Tango");
					genres.Add("Samba");
					genres.Add("Folklore");
					genres.Add("Ballad");
					genres.Add("Power Ballad");
					genres.Add("Rhythmic Soul");
					genres.Add("Freestyle");
					genres.Add("Duet");
					genres.Add("Punk Rock");
					genres.Add("Drum Solo");
					genres.Add("A Cappella");
					genres.Add("Euro-House");
					genres.Add("Dance Hall");
					genres.Add("Goa");
					genres.Add("Drum & Bass");
					genres.Add("Club-House");
					genres.Add("Hardcore");
					genres.Add("Terror");
					genres.Add("Indie");
					genres.Add("BritPop");
					genres.Add("Negerpunk");
					genres.Add("Polsk Punk");
					genres.Add("Beat");
					genres.Add("Christian Gangsta Rap");
					genres.Add("Heavy Metal");
					genres.Add("Black Metal");
					genres.Add("Crossover");
					genres.Add("Contemporary Christian");
					genres.Add("Christian Rock");
					genres.Add("Merengue");
					genres.Add("Salsa");
					genres.Add("Thrash Metal");
					genres.Add("Anime");
					genres.Add("Jpop");
					genres.Add("Synthpop");
				}
				return genres.AsReadOnly();
			}
		}
		#endregion
      
		#region Public Static Methods
		public static string GetGenre(byte index)
		{
			if(index >= 0 && index < Genres.Count)
				return Genres[index];
			return null;
		}
      
		public static byte GetGenreIndex(string name)
		{
			for (byte i = 0; i < Genres.Count; i ++)
				if (name == Genres[i]) return i;
			return 255;
		}
		#endregion		
   }
}
