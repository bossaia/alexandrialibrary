using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.Imdb
{
	public interface IAsyncCallback
	{
		void OnFillMovieDetailsAsyncProgress(object parameters);
		void OnFillMovieDetailsAsyncDone(object parameters);
	}
}