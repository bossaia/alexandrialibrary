using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Imdb
{
	public class AsyncCommandFillMovie : AsyncCommand
	{
		public AsyncCommandFillMovie(int priority, long MovieCode, Movie movie, IAsyncCallback callback, Object parameters)
		{
			this.priority = priority;
			this.movie = movie;
			this.code = MovieCode;
			this.callback = callback;
			this.parameters = parameters;
			this.running = false;
			this.cmd = 1;//FillMovie
		}
		
		public long code;
		public Movie movie;
		public object parameters;
		public IAsyncCallback callback;
	}
}
