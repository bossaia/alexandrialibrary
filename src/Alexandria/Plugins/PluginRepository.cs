using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Alexandria.Persistence;

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
		private IDictionary<string, ConstructorMap> constructorsByRecordType = new Dictionary<string, ConstructorMap>();
		private IDictionary<Type, ConstructorMap> constructorsByType = new Dictionary<Type, ConstructorMap>();
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
					
					foreach(Type type in assembly.GetTypes())
					{
						Type persistent = type.GetInterface("IPersistent");
						if (persistent != null)
						{
							foreach(ConstructorInfo constructor in type.GetConstructors())
							{
								foreach (Attribute attribute in constructor.GetCustomAttributes(typeof(ConstructorAttribute), false))
								{
									ConstructorAttribute constructorAttribute = (ConstructorAttribute)attribute;
									ConstructorMap map = new ConstructorMap(constructorAttribute, constructor);
									constructorsByRecordType.Add(constructorAttribute.RecordType, map);
									constructorsByType.Add(type, map);
								}
							}
						}
					}
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
		
		public IDictionary<string, ConstructorMap> ConstructorsByRecordType
		{
			get { return constructorsByRecordType; }
		}
		
		public IDictionary<Type, ConstructorMap> ConstructorsByType
		{
			get { return constructorsByType; }
		}
		#endregion
		
		#region Public Methods
		#endregion
	}
}
