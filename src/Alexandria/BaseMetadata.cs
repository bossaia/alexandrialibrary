using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class BaseMetadata : IMetadata, IPersistant
	{
		#region Constructors
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
		#endregion
	
		#region IMetadata Members
		[PersistanceOptions(PersistanceLoadType.ConstructorByName, PersistanceSaveType.PropertyGetToString, IsPrimaryKey=true)]
		public Guid Id
		{
			get { return id; }
		}
		
		[PersistanceOptions(PersistanceLoadType.Collection, PersistanceSaveType.Collection)]
		public IList<IMetadataIdentifier> MetadataIdentifiers
		{
			get { return metadataIdentifiers; }
		}

		[PersistanceOptions(PersistanceLoadType.ConstructorByName, PersistanceSaveType.PropertyGetToString)]
		public ILocation Location
		{
			get { return location; }
		}

		[PersistanceOptions(PersistanceLoadType.ConstructorByName, PersistanceSaveType.PropertyGet)]
		public string Name
		{
			get { return name; }
		}
		#endregion

		#region IPersistant Members
		public void Save(IStorageEngine engine)
		{
			if (engine != null)
			{
				engine.Save(this);
			}
			else throw new ArgumentNullException("engine");
		}

		public void Delete(IStorageEngine engine)
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
