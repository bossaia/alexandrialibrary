using System;
using System.Collections.Generic;
using System.Text;

using Alexandria;
using Alexandria.Media;
using Alexandria.Media.IO;

namespace Alexandria.Console
{
	public abstract class Context
	{
		public Context(string name)
		{
			this.name = name;
		}

		private string name;
		private bool isActive;
		private bool isOpen = true;
		private string prompt = "alx> ";
		private string result;
		
		public string Name
		{
			get { return name; }
		}
		
		public bool IsActive
		{
			get { return isActive; }
			set { isActive = value; }
		}
		
		public bool IsOpen
		{
			get { return isOpen; }
		}
		
		public string Prompt
		{
			get { return prompt; }
			set { prompt = value; }
		}
	
		public string Result
		{
			get { return result; }
			set { result = value; }
		}
		
		public virtual void Close()
		{
			if (isActive)
				isOpen = false;
		}
		
		public virtual void WriteResult()
		{
			System.Console.WriteLine(Result);
		}
				
		public abstract void HandleCommand(Command command, string option);
	}
}
