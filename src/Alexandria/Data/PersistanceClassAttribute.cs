using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Data
{
	[AttributeUsage(AttributeTargets.Class)]
	public class PersistanceClassAttribute : Attribute
	{
		#region Constructors
		public PersistanceClassAttribute()
		{
		}
		#endregion
		
		#region Private Fields
		private PersistanceLoadType loadType = PersistanceLoadType.None;
		private Type factoryType;
		private bool manuallySetProperties;
		#endregion
		
		#region Public Properties
		public PersistanceLoadType LoadType
		{
			get { return loadType; }
			set { loadType = value; }
		}
		
		public Type FactoryType
		{
			get { return factoryType; }
			set { factoryType = value; }
		}
		
		public bool ManuallySetProperties
		{
			get { return manuallySetProperties; }
			set { manuallySetProperties = value; }
		}
		#endregion
	}
}
