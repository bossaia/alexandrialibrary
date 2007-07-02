using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Alexandria.Plugins
{
	public class PluginRepository
	{
		#region Constructors
		public PluginRepository(string searchPattern)
		{
			LoadAssmblies(searchPattern);
		}
		#endregion

		#region Private Fields
		private IList<Assembly> assemblies = new List<Assembly>();
		private IDictionary<string, ConstructorInfo> constructors = new Dictionary<string,ConstructorInfo>();
		#endregion
		
		#region Private Methods
		private void LoadAssmblies(string searchPattern)
		{
			DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory);
			foreach(FileInfo file in dir.GetFiles(searchPattern))
			{
				try
				{
					Assembly assembly = Assembly.LoadFrom(file.FullName);
					assemblies.Add(assembly);
				}
				catch (FileLoadException)
				{
					//file is not a .NET assembly
				}
			}
		}
		#endregion
		
		#region Public Properties
		public IList<Assembly> Assemblies
		{
			get { return assemblies; }
		}
		
		public IDictionary<string, ConstructorInfo> Constructors
		{
			get { return constructors; }
		}
		#endregion
	}
}
