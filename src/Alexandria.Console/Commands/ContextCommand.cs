using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Console.Commands
{
	public class ContextCommand : Command
	{
		public ContextCommand() : base("Context")
		{
		}

		public override Context Execute(Context context, string option)
		{
			if (!string.IsNullOrEmpty(option) && ContextFactory.IsContext(option))
				context = ContextFactory.Contexts[option];
				
			System.Console.WriteLine(string.Format("Current Context: {0}", context.Name));
			return context;
		}
	}
}
