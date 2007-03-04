using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Imdb
{
	public interface IAsyncCallback
	{
		void OnFillMovieDetailsAsyncProgress(object parameters);
		void OnFillMovieDetailsAsyncDone(object parameters);
	}
}