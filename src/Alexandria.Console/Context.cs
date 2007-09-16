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
		private bool isOpen = true;
		private string prompt = "alx> ";
		
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
		
		public virtual void Close()
		{
			isOpen = false;
		}
		
		public virtual void WriteStatus()
		{
		}
	}
}
