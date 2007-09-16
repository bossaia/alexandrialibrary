using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Console.Commands
{
	public class StatusCommand : Command
	{
		public StatusCommand() : base("Status")
		{
		}
		
		public override Context Execute(Context context, string option)
		{
			context.WriteStatus();
			return context;
		}
	}
}
