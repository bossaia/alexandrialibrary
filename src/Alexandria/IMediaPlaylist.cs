using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMediaPlaylist
	{
		string Path{get;}
		string Name{get;}
		string Version{get;}
		IList<MediaFile> Files{get;}
	}
}
