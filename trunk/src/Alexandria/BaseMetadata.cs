using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class BaseMetadata : IMetadata
	{
		#region Constructors
		public BaseMetadata(IIdentifier id, ILocation location, string name)
		{
			this.id = id;
			this.location = location;
			this.name = name;
		}
		#endregion
	
		#region Private Fields
		private IIdentifier id;
		private ILocation location;
		private string name;
		#endregion
	
		#region IMetadata Members
		public IIdentifier Id
		{
			get { return id; }
		}

		public ILocation Location
		{
			get { return location; }
		}

		public string Name
		{
			get { return name; }
		}

		public virtual IDataMatrix CreateMap()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public virtual void LoadMap(IDataMatrix map)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion
	}
}
