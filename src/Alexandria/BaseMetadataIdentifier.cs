using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class BaseMetadataIdentifier : BaseIdentifier, IMetadataIdentifier
	{
		#region Constructors
		public BaseMetadataIdentifier(string parentId, string idValue, string idType, string idVersion) : base(idValue, idType, idVersion)
		{
			this.isNew = true;
			this.id = Guid.NewGuid();
			this.parentId = new Guid(parentId);
		}
		
		public BaseMetadataIdentifier(string id, string parentId, string idValue, string idType, string idVersion) : this(new Guid(id), new Guid(parentId), idValue, idType, new Version(idVersion))
		{
		}
		
		public BaseMetadataIdentifier(Guid id, Guid parentId, string idValue, string idType, Version idVersion) : base(idValue, idType, idVersion)
		{
			this.id = id;
			this.parentId = parentId;
		}
		#endregion
	
		#region Private Fields
		private Guid id;		
		private Guid parentId;
		private bool isNew;
		private IStorageEngine engine;
		#endregion
	
		#region IMetadataIdentifier Members
		public Guid MetadataId
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}
		#endregion

		#region IPersistant Members
		public Guid Id
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}
		
		public IStorageEngine Engine
		{
			get { return engine; }
			set { engine = value; }
		}
		
		public bool IsNew
		{
			get { return isNew; }
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
