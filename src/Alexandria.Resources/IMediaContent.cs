using System;
using System.Collections.Generic;
using System.Net.Mime;

namespace Telesophy.Alexandria.Resources
{
	public interface IMediaContent
	{
		IResourceFormat Format { get; set; }
	}
}
