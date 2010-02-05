using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria;
using StructureMap.Configuration.DSL;

namespace Gnosis.Sphinx
{
	public class TypeRegistry
		: Registry
	{
		public TypeRegistry()
		{
			For<IAlbum>().Use<Album>();
			For<ITrackRepository>().Use<TrackRepository>();
			For<IMediaRepository>().Use<MediaRepository>();
		}
	}
}
