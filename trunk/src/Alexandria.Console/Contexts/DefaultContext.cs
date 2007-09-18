using System;
using System.Collections.Generic;

using Alexandria.Console.Commands;

namespace Alexandria.Console.Contexts
{
	public class DefaultContext : Context
	{
		public DefaultContext() : base(ContextConstants.Default)
		{
		}
	
		private void HandleContext(string option)
		{
			if (!string.IsNullOrEmpty(option))
			{
				if (ContextFactory.IsContext(option))
				{
					ContextFactory.SetActiveContext(option);
				}
				else System.Console.WriteLine(string.Format("{0} is not a valid context", option));
			}
			else
			{
				System.Console.WriteLine(string.Format("Active Context: {0}", ContextFactory.ActiveContext.Name));
			}
		}
	
		public override void HandleCommand(Command command, string option)
		{
			switch(command.Name)
			{
				case CommandConstants.Close:
					ContextFactory.ActiveContext.Close();
					break;
				case CommandConstants.Context:
					HandleContext(option);
					break;
				case CommandConstants.Status:
					if (IsActive) WriteStatus();
					break;
				default:
					break;
			}
		}

		public override void WriteStatus()
		{
			System.Console.WriteLine("Default status message goes here");
		}
	}
}
