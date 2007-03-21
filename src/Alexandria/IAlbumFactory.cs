using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAlbumFactory
	{
		IAlbum GetAlbum(IAudioCompactDisc disc);

		IAlbum GetAlbum(ISearch search);
	}
}
