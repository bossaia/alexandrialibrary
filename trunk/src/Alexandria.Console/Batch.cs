using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Console
{
	public abstract class Batch
	{
		private string name;
		private bool isOpen;
		private string prompt = "  batch> ";
		
		public string Name
		{
			get { return name; }
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
		
		public virtual void Open()
		{
			isOpen = true;
		}
		
		public virtual void Close()
		{
			isOpen = false;
		}
		
		public abstract bool InputIsValid(string input);
		
		public abstract void ProcessInput(string input);
	}
}
