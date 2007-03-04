using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Imdb
{
	public class AsyncCommand
	{
		public AsyncCommand()
		{
			running = false;
		}
		
		public int priority;
		public int cmd;
		public bool running;
	}
}
