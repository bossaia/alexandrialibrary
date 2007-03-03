using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Text;

namespace Alexandria
{
	public static class PluginManager
	{
		#region Private Static Fields
		private static string defaultPluginPath = System.Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar;
		private static Assembly tagEngineAssembly;
		private static TagEngine tagEngine;
		private static string tagEnginePluginPath = PluginManager.DefaultPluginPath + System.Configuration.ConfigurationManager.AppSettings["TagEnginePluginName"];
		private static Assembly audioPlayerAssembly;
		private static AudioPlayer audioPlayer;
		private static string audioPlayerPluginPath = PluginManager.DefaultPluginPath + System.Configuration.ConfigurationManager.AppSettings["AudioPlayerPluginName"];
		//private static Assembly dataFactoryAssembly;
		//private static DataFactory dataFactory;
		//private static string dataFactoryPluginPath = PluginManager.DefaultPluginPath + System.Configuration.ConfigurationManager.AppSettings["DataFactoryPluginName"];
		private static Assembly metadataProviderAssembly;
		private static MetadataProvider metadataProvider;
		private static string metadataProviderPluginPath = PluginManager.DefaultPluginPath + System.Configuration.ConfigurationManager.AppSettings["MetadataProviderPluginName"];
		#endregion
		
		#region Private Static Methods
		private static object InstantiateType(Type type)
		{
			if (type != null)
			{
				System.Reflection.ConstructorInfo cinfo = type.GetConstructor(System.Type.EmptyTypes);
				if (cinfo != null)
				{
					return cinfo.Invoke(null);
				}
			}
			return null;		
		}
		#endregion
				
		#region Public Static Properties
		public static string DefaultPluginPath
		{
			get {return defaultPluginPath;}
		}

		public static Assembly AudioPlayerAssembly
		{
			get
			{
				if (audioPlayerAssembly == null) audioPlayerAssembly = PluginManager.GetAssembly(audioPlayerPluginPath);
				return audioPlayerAssembly;
			}
		}

		public static AudioPlayer AudioPlayer
		{
			get
			{
				if (audioPlayer == null) audioPlayer = PluginManager.GetObject(AudioPlayerAssembly, typeof(AudioPlayerClassAttribute)) as AudioPlayer;
				return audioPlayer;
			}
		}

		public static Assembly TagEngineAssembly
		{
			get
			{
				if (tagEngineAssembly == null) tagEngineAssembly = PluginManager.GetAssembly(tagEnginePluginPath);
				return tagEngineAssembly;
			}
		}

		public static TagEngine TagEngine
		{
			get
			{
				if (tagEngine == null) tagEngine = PluginManager.GetObject(TagEngineAssembly, typeof(TagEngineClassAttribute)) as TagEngine;
				return tagEngine;
			}
		}
		
		public static Assembly MetadataProviderAssembly
		{
			get
			{
				if (metadataProviderAssembly == null) metadataProviderAssembly = PluginManager.GetAssembly(metadataProviderPluginPath);
				return metadataProviderAssembly;
			}
		}
		
		public static MetadataProvider MetadataProvider
		{
			get
			{
				if (metadataProvider == null) metadataProvider = PluginManager.GetObject(MetadataProviderAssembly, typeof(MetadataProviderClassAttribute)) as MetadataProvider;
				return metadataProvider;
			}
		}
		#endregion
	
		#region Public Static Methods
		public static Assembly GetAssembly(string path)
		{
			if (path != null)
			{
				Assembly assembly = System.Reflection.Assembly.LoadFile(path);
				return assembly;
			}
			else throw new ArgumentNullException("path");
		}

		public static object GetObject(Assembly assembly, string className)
		{
			if (assembly != null)
			{
				if (className != null)
				{
					System.Type type = assembly.GetType(className, true);
					return InstantiateType(type);
				}
				else throw new ArgumentNullException("className");
			}
			else throw new ArgumentNullException("assembly");
		}
		
		public static object GetObject(Assembly assembly, Type attributeType)
		{
			if (assembly != null)
			{
				if (attributeType != null)
				{
					foreach(Type type in assembly.GetTypes())
					{				
						foreach(Attribute attribute in type.GetCustomAttributes(attributeType, false))
						{
							if (attribute.GetType().Name == attributeType.Name)
							{						
								return InstantiateType(type);
							}
						}
					}
					return null;
				}
				else throw new ArgumentNullException("attributeType");
			}
			else throw new ArgumentNullException("assembly");
		}
		#endregion
	}
}
