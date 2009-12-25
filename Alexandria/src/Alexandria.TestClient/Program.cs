using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Alexandria.Core;

namespace Alexandria.TestClient
{
	class Program
	{
		static void Main(string[] args)
		{
			//IArtist pinkFloyd = null;

			var criteria = 
				new Criteria<IAlbum>(x => x.Title.TRIM().UPPER().IsEqualTo(), "THE WALL")
					.And<string>(x => x.Artist.Name.IsLike(), "Pink Floyd")
					.And<DateTime>(x => x.Released.IsGreaterThanOrEqualTo(), new DateTime(1970, 12, 31));

			Console.WriteLine(criteria.ToString());
			Console.ReadLine();
		}
	}
}
