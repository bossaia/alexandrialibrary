using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Console.Commands
{
	public class CloseCommand : Command
	{
		public CloseCommand() : base("Close")
		{
		}
		
		public override Context Execute(Context context, string option)
		{
			context.Close();
			return context;
		}
	}
}
