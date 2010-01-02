using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Alexandria.Core;
using Babel;

namespace Alexandria.TestClient
{
	class Program
	{
		static void Main(string[] args)
		{
			var search = new { Album = "Foiled", Artist = "Snow Patrol", Date = "1999-12-31" };

			var query = new Query<IAlbum>(search)
				.Where(x => x.Title.IsEqualTo(search.Album))
				.Or
				.Where(x => x.Artist.Name.IsEqualTo(search.Artist))
				.Or
				.Where(x => x.Released.IsGreaterThanOrEqualTo(search.Date));

			Console.WriteLine(query.ToString());
			Console.ReadLine();
		}
	}
}
