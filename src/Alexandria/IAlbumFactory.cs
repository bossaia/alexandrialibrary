using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAlbumFactory
	{
		IAlbumResource GetAlbum(IAudioCompactDisc disc);

		IAlbumResource GetAlbum(ISearch search);
	}
}
