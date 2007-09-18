using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Console
{
	public abstract class Command
	{
		public Command(string name)
		{
			this.name = name;
		}
		
		private string name;
		private string option;
		
		public string Name
		{
			get { return name; }
		}
				
		public virtual void Execute(Context context, string option)
		{
			context.HandleCommand(this, option);
		}
	}
}
