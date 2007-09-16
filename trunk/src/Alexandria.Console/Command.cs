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
				
		public abstract Context Execute(Context context, string option);
	}
}
