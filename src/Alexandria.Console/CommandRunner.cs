using System;
using System.Collections.Generic;
using System.Text;

using Alexandria.Console.Contexts;

namespace Alexandria.Console
{
	public class CommandRunner
	{
		public CommandRunner()
		{
			ContextFactory.SetActiveContext(ContextConstants.Default);
		}
	
		public CommandRunner(string activeContext)
		{
			ContextFactory.SetActiveContext(activeContext);
		}
		
		private const string INVALID_INPUT = "Invalid Input";
		private List<Command> commands = new List<Command>();
		private List<string> options = new List<string>();
		private int currentCommandIndex = -1;

		private void AddCommand(string name, string option)
		{
			if (CommandFactory.IsCommand(name))
			{
				commands.Add(CommandFactory.Commands[name]);
				options.Add(option);
			}
		}
		
		private void ShowInvalidInput()
		{
			System.Console.WriteLine(INVALID_INPUT);
		}
		
		public int CurrentCommandIndex
		{
			get { return currentCommandIndex; }
		}
		
		public IList<Command> Commands
		{
			get { return commands.AsReadOnly(); }
		}
		
		public IList<string> Options
		{
			get { return options.AsReadOnly(); }
		}
		
		public void ShowPrompt()
		{
			if (ContextFactory.ActiveContext.HasOpenBatch)
				System.Console.Write(ContextFactory.ActiveContext.CurrentBatch.Prompt);
			else System.Console.Write(ContextFactory.ActiveContext.Prompt);
		}
		
		public void ParseInput()
		{
			string input = System.Console.ReadLine();
			ParseInput(input);
		}
		
		public void ParseInput(string input)
		{
			if (!string.IsNullOrEmpty(input))
			{
				if (ContextFactory.ActiveContext.HasOpenBatch)
				{
					Batch batch = ContextFactory.ActiveContext.CurrentBatch;
					if (batch.InputIsValid(input))
					{
						batch.ProcessInput(input);
					}
					else ShowInvalidInput();
				}
				else
				{
					string name = null;
					string option = null;
					string[] parts = input.Split(new char[] { ' ' }, 2);
					
					if (CommandFactory.IsCommand(parts[0]))
					{
						name = parts[0];

						if (parts.Length > 1)
							option = parts[1];

						AddCommand(name, option);
					}
					else ShowInvalidInput();
				}
			}
			else ShowInvalidInput();
		}
		
		public void ParseBatch()
		{
		}
		
		public void ParseBatch(string input)
		{
		}
		
		public void Reset()
		{
			commands.Clear();
			options.Clear();
		}
		
		public void Run()
		{
			if (commands.Count > 0)
			{
				for(int i=0; i<commands.Count; i++)
				{
					currentCommandIndex = i;
					foreach(Context context in ContextFactory.Contexts.Values)
					{
						commands[i].Execute(context, options[i]);
					}
				}
			}
		}
	}
}
