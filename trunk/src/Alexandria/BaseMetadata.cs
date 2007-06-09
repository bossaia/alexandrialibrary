using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class BaseMetadata : IMetadata, Data.IPersistant
	{
		#region Constructors
		public BaseMetadata(string location, string name)
		{
			this.isNew = true;
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

		private bool isNew;
		private IStorageEngine engine;
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
		public bool IsNew
		{
			get { return isNew; }
		}

		public IStorageEngine Engine
		{
			get { return engine; }
			set { engine = value; }
		}
		
		public void Save()
		{
			if (engine != null)
			{
				engine.Save(this);
				this.isNew = false;
			}
			else throw new ArgumentNullException("engine");
		}

		public void Delete()
		{
			if (engine != null)
			{
				engine.Delete(this);
			}
			else throw new ArgumentNullException("engine");
		}
		#endregion
	}
}
