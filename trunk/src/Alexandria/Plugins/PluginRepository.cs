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
		private IDictionary<Assembly, ConfigurationMap> configurationMaps = new Dictionary<Assembly, ConfigurationMap>();
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
						Configuration configFile = System.Configuration.ConfigurationManager.OpenExeConfiguration(file.FullName);
						if (configFile != null)
						{
							IPluginSettings settings = null;
							foreach(Type type in assembly.GetTypes())
							{
								if (type.GetInterface("IPluginSettings") != null)
								{
									ConstructorInfo ctor = type.GetConstructor(System.Type.EmptyTypes);
									if (ctor != null)
									settings = (IPluginSettings)ctor.Invoke(null);
								}
								if (settings != null)
									break;
							}
							ConfigurationMap configMap = new ConfigurationMap(configFile, settings);
							configurationMaps.Add(assembly, configMap);
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
		
		public IDictionary<Assembly, ConfigurationMap> ConfigurationMaps
		{
			get { return configurationMaps; }
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
