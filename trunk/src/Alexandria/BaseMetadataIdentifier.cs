using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class BaseMetadataIdentifier : BaseIdentifier, IMetadataIdentifier
	{
		#region Constructors
		[PersistanceOptions("MetadataID", PersistanceLoadType.ConstructorByName, PersistanceSaveType.PropertyGetToString)]
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
		
		public void Save(IStorageEngine engine)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Delete(IStorageEngine engine)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion
	}
}
