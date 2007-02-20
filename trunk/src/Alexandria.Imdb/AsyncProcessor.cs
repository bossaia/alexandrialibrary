using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.Imdb
{
	public class AsyncProcessor : IAsyncProcessorDone
	{
		public AsyncProcessor(int maximumSimultaneous, ImdbParser parser)
		{
			commands = new List<AsyncCommand>();
			running = 0;
			this.maximumSimultaneous = maximumSimultaneous;
			this.parser = parser;
		}

		List<AsyncCommand> commands;
		int running;
		int maximumSimultaneous;
		ImdbParser parser;

		public void Start()
		{
			//start the highest priority
			AsyncCommand toRunCmd = null;
			int maxPriority = 0;
			for (int i = 0; i < commands.Count; i++)
			{
				if (commands[i].running == false)
				{
					if (commands[i].priority > maxPriority)
					{
						toRunCmd = commands[i];
						maxPriority = commands[i].priority;
					}
				}
			}
			if (toRunCmd != null)
			{
				if (toRunCmd.cmd == 1)
				{
					parser.FillMovieDetailsAsync((AsyncCommandFillMovie)toRunCmd, (IAsyncProcessorDone)this);
				}
			}
		}

		public void Add(AsyncCommand cmd)
		{

			commands.Add(cmd);
			if (running < maximumSimultaneous)
			{
				Start();
			}
		}
		
		public void Done(AsyncCommand cmd)
		{
			commands.Remove(cmd);
			if (running < maximumSimultaneous)
			{
				Start();
			}
		}
	}
}