using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.Imdb
{
	public interface IAsyncProcessorDone
	{
		void Done(AsyncCommand cmd);
	}
}
