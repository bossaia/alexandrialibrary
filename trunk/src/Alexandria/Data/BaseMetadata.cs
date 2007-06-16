#region License
/*
Copyright (c) 2007 Dan Poage

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion

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
		[PersistanceProperty(PersistanceFieldType.Basic, IsPrimaryKey=true, IsRequired=true, Ordinal=1)]
		public Guid Id
		{
			get { return id; }
		}
		
		[PersistanceProperty(PersistanceFieldType.OneToManyChildren, PersistanceLoadType.Property, "ParentID", typeof(MetadataIdentifier), CascadeSave=true, CascadeDelete=true)]
		public IList<IMetadataIdentifier> MetadataIdentifiers
		{
			get { return metadataIdentifiers; }
		}

		[PersistanceProperty(PersistanceFieldType.Basic, Ordinal=2)]
		public ILocation Location
		{
			get { return location; }
		}

		[PersistanceProperty(PersistanceFieldType.Basic, IsRequired=true, Ordinal=3)]
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
