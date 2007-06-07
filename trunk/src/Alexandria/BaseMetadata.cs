using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class BaseMetadata : IMetadata
	{
		#region Constructors
		public BaseMetadata(string alexandriaId, string path, string name) : this(new Guid(alexandriaId), new Location(path), name)
		{
		}
		
		public BaseMetadata(Guid alexandriaId, ILocation location, string name)
		{
			this.alexandriaId = alexandriaId;
			this.location = location;
			this.name = name;
		}
		#endregion
	
		#region Private Fields
		private Guid alexandriaId = default(Guid);
		private IList<IIdentifier> otherIdentifiers = new List<IIdentifier>();
		private ILocation location;
		private string name;
		#endregion
	
		#region IMetadata Members
		public Guid AlexandriaId
		{
			get { return alexandriaId; }
		}
		
		public IList<IIdentifier> OtherIdentifiers
		{
			get { return otherIdentifiers; }
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
	}
}
