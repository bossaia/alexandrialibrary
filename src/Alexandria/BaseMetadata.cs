using System;
using System.Collections.Generic;
using System.Text;
using Alexandria.Data;

namespace Alexandria
{
	public abstract class BaseMetadata : IMetadata, IPersistant
	{
		#region Constructors
		public BaseMetadata(string location, string name)
		{
			this.id = Guid.NewGuid();
			this.location = new Location(location);
			this.name = name;
		}
		
		public BaseMetadata(string id, string location, string name) : this(new Guid(id), new Location(location), name)
		{
		}
		
		public BaseMetadata(Guid id, ILocation location, string name)
		{
			this.id = id;
			this.location = location;
			this.name = name;
		}
		#endregion
	
		#region Private Fields
		private Guid id = default(Guid);		
		private IList<IMetadataIdentifier> metadataIdentifiers = new List<IMetadataIdentifier>();
		private ILocation location;
		private string name;

		private IDataStore dataStore;
		#endregion
	
		#region IMetadata Members
		public Guid Id
		{
			get { return id; }
		}
		
		public IList<IMetadataIdentifier> MetadataIdentifiers
		{
			get { return metadataIdentifiers; }
		}

		public ILocation Location
		{
			get { return location; }
		}

		public string Name
		{
			get { return name; }
		}
		#endregion

		#region IPersistant Members
		public IDataStore DataStore
		{
			get { return dataStore; }
			set { dataStore = value; }
		}
		
		public void Save()
		{
			dataStore.Save(this);
		}

		public void Delete()
		{
			dataStore.Delete(this);
		}
		#endregion
	}
}
