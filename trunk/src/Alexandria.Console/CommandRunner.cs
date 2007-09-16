using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Console
{
	public class CommandRunner
	{
		public CommandRunner(Context startingContext)
		{
			currentContext = startingContext;
		}
		
		private const string INVALID_INPUT = "INVALID INPUT";
		
		private Context currentContext;
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
		
		public Context CurrentContext
		{
			get { return currentContext; }
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
			System.Console.Write(currentContext.Prompt);
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
				else System.Console.WriteLine(INVALID_INPUT);
			}
			else System.Console.WriteLine(INVALID_INPUT);
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
					currentContext = commands[i].Execute(currentContext, options[i]);
				}
			}
		}
	}
}
