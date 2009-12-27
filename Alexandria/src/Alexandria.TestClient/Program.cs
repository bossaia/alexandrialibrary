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
			//IArtist pinkFloyd = null;

			/*
			var c1 =
				new Alexandria.Core.Criteria<Alexandria.Core.IAlbum>(x => x.Title.TRIM().UPPER().IsEqualTo("THE WALL"), "")
					.And<string>(x => x.Artist.Name.IsLike("Pink Floyd"), "")
					.And<DateTime>(x => x.Released.IsGreaterThanOrEqualTo("1970-12-31"), new DateTime(1970, 12, 31));
			*/
			
			var settings = new { Title = "Foiled"  };

			var c2 = new Criteria<IAlbum>(settings)
				.That(x => x.Title.IsEqualTo(settings.Title))
				.Or
				.That(x => x.Artist.Name.IsEqualTo("Snow Patrol"))
				.Or
				.That(x => x.Released.IsGreaterThanOrEqualTo(new DateTime(1999, 12, 31)));
					
						//.That(x => x.Title.

			Console.WriteLine(c2.ToString());
			Console.ReadLine();
		}
	}
}
