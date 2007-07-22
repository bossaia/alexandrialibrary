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
		public PluginRepository(IList<FileInfo> files)
		{
			LoadAssemblies(files);
		}
		#endregion

		#region Private Fields
		private IDictionary<Assembly, bool> assemblies = new Dictionary<Assembly, bool>();
		private IDictionary<Assembly, ConfigurationMap> configurationMaps = new Dictionary<Assembly, ConfigurationMap>();
		#endregion
		
		#region Private Methods
		private void LoadAssemblies(IList<FileInfo> files)
		{
			foreach(FileInfo file in files)
			{
				try
				{
					Assembly assembly = Assembly.LoadFrom(file.FullName);
					bool enabled = false;
					
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
							
							if (settings != null)
							{
								enabled = settings.Enabled;
								ConfigurationMap configMap = new ConfigurationMap(configFile, settings);
								configurationMaps.Add(assembly, configMap);
							}
						}
					}
					
					assemblies.Add(assembly, enabled);
				}
				catch (FileNotFoundException ex)
				{
					//file does not exist
					string x = ex.Message;
				}
				catch (FileLoadException ex)
				{
					//file is not a .NET assembly
					string x = ex.Message;
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
