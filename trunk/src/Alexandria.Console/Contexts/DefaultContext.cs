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
					Result = string.Format("Context changed to: {0}", option);
				}
				else Result = string.Format("{0} is not a valid context", option);
			}
			else
			{
				Result = string.Format("Active Context: {0}", ContextFactory.ActiveContext.Name);
			}
		}

		private void HandleStatus(string option)
		{
			System.Console.WriteLine("Alexandria Client Ready");
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
					WriteResult();
					break;
				case CommandConstants.Status:
					if (IsActive) HandleStatus(option);
					break;
				default:
					break;
			}
		}
	}
}
