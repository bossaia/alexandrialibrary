using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Imdb
{
	public interface IAsyncProcessorDone
	{
		void Done(AsyncCommand cmd);
	}
}
