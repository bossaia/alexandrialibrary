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
using Alexandria;
using Alexandria.Persistence;

namespace Alexandria.Metadata
{
	public abstract class BaseMetadata : IMetadata, IPersistent
	{
		#region Constructors
		public BaseMetadata(string path, string name)
		{
			this.id = Guid.NewGuid();
			this.path = new Uri(path);
			this.name = name;
		}
		
		public BaseMetadata(string id, string path, string name) : this(new Guid(id), new Uri(path), name)
		{
		}
		
		public BaseMetadata(Guid id, Uri path, string name)
		{
			this.id = id;
			this.path = path;
			this.name = name;
		}
		#endregion
	
		#region Private Fields
		private Guid id = default(Guid);		
		private IList<IMetadataIdentifier> metadataIdentifiers = new List<IMetadataIdentifier>();
		private Uri path;
		private string name;

		private IDataStore dataStore;
		#endregion
	
		#region IMetadata Members
		[Property(1, IsPrimaryKey=true, IsRequired=true)]
		public Guid Id
		{
			get { return id; }
		}
		
		[Property(FieldType.OneToManyChildren, LoadType.Property, StoreType.ForeignKey, "ParentID", CascadeSave=true, CascadeDelete=true)]
		public IList<IMetadataIdentifier> MetadataIdentifiers
		{
			get { return metadataIdentifiers; }
		}

		[Property(2)]
		public Uri Path
		{
			get { return path; }
		}

		[Property(3, IsRequired=true)]
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
