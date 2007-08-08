#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

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
		private IDictionary<Type, ToolTypeAttribute> toolTypes = new Dictionary<Type, ToolTypeAttribute>();
		private IDictionary<Type, List<ITool>> tools = new Dictionary<Type, List<ITool>>();
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
							foreach (Type type in assembly.GetTypes())
							{
								foreach (ToolTypeAttribute attribute in type.GetCustomAttributes(typeof(ToolTypeAttribute), false))
								{
									toolTypes.Add(type, attribute);
								}
								
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
								settings.ConfigurationMap = configMap;
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
		public ConfigurationMap GetConfigurationMap(string assemblyName)
		{
			foreach(Assembly assembly in ConfigurationMaps.Keys)
			{
				if (assembly.FullName.Contains(assemblyName))
					return ConfigurationMaps[assembly];
			}
			return null;
		}
		#endregion
	}
}
