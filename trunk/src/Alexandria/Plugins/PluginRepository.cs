using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Alexandria.Persistence;

namespace Alexandria.Plugins
{
	public class PluginRepository : IPluginRepository
	{
		#region Constructors
		public PluginRepository(IList<FileInfo> files)
		{
			LoadAssemblies(files);
		}
		#endregion

		#region Private Fields
		private IList<Assembly> assemblies = new List<Assembly>();
		#endregion
		
		#region Private Methods
		private void LoadAssemblies(IList<FileInfo> files)
		{
			//DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory);
			//foreach(FileInfo file in dir.GetFiles(searchPattern))
			
			foreach(FileInfo file in files)
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
				//TODO: figure out how I want to handle exceptions
			}
		}
		#endregion
		
		#region Public Properties
		public IList<Assembly> Assemblies
		{
			get { return assemblies; }
		}		
		#endregion
		
		#region Public Methods
		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			//throw new Exception("The method or operation is not implemented.");
		}

		#endregion
	}
}
