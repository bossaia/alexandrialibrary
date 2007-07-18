using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using Alexandria.Persistence;

namespace Alexandria.Plugins
{
	public class PluginRepository : IPluginRepository
	{
		#region Constructors
		public PluginRepository(IDictionary<FileInfo, bool> files)
		{
			LoadAssemblies(files);
		}
		#endregion

		#region Private Fields
		private IDictionary<Assembly, bool> assemblies = new Dictionary<Assembly, bool>();
		private IDictionary<Assembly, Configuration> configurations = new Dictionary<Assembly, Configuration>();
		#endregion
		
		#region Private Methods
		private void LoadAssemblies(IDictionary<FileInfo, bool> files)
		{
			//DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory);
			//foreach(FileInfo file in dir.GetFiles(searchPattern))
			
			foreach(KeyValuePair<FileInfo,bool> pair in files)
			{
				try
				{
					FileInfo file = pair.Key;
					Assembly assembly = Assembly.LoadFrom(file.FullName);
					assemblies.Add(assembly, pair.Value);
					
					string configName = file.FullName + ".config";
					if (File.Exists(configName))
					{
						Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(file.FullName);
						if (config != null)
						{
							configurations.Add(assembly, config);
						}
					}
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
		public IDictionary<Assembly, bool> Assemblies
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
