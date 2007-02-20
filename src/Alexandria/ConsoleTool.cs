using System;
using System.Collections.Generic;

namespace AlexandriaOrg.Alexandria
{
	/// <summary>
	/// A generic OO wrapper around a command line tool
	/// </summary>
	public abstract class ConsoleTool
	{
		#region Constructors
		protected ConsoleTool(string path)
		{
			this.path = path;
		}
		
		protected ConsoleTool(string path, IList<string> parameters) : this(path)
		{
			foreach (string parameter in parameters)
			{
				this.parameters.Add(parameter);
			}
		}
		#endregion

		#region Private Fields
		private string path;
		private List<string> parameters = new List<string>();
		private string currentResult;
		#endregion

		#region Protected Methods
		protected virtual void Execute()
		{
		}
		#endregion

		#region Public Properties
		public string CurrentResult
		{
			get { return currentResult; }
			protected set { currentResult = value; }
		}
		
		public IList<string> Parameters
		{
			get { return parameters; }			
		}
		#endregion

		#region Public Methods
		public void Execute(IDictionary<int, string> parameters)
		{
			Execute();
		}		
		
		public void Execute(IDictionary<string, string> parameters)
		{
			Execute();
		}
		#endregion
	}
}

