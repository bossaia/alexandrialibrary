using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Alexandria.Core.Model
{
	public class Playlist : Resource
	{
		#region Constructors

		public Playlist()
			: base(ResourceTypes.Playlist)
		{
		}

		#endregion
	}
}
