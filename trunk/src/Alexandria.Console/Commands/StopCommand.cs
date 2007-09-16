using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Console.Commands
{
	public class StopCommand : Command
	{
		public StopCommand() : base("Stop")
		{
		}
		
		public override Context Execute(Context context, string option)
		{
			ContextFactory.PlaybackContext.Player.Stop();
			ContextFactory.PlaybackContext.WriteStatus();
			return context;
		}
	}
}
