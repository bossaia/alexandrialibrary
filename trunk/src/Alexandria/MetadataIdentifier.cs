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
	[PersistanceClass("MetadataID", PersistanceLoadType.Constructor)]
	public struct MetadataIdentifier : IMetadataIdentifier
	{
		#region Constructors
		public MetadataIdentifier(string parentId, string value, string type, string version) : this(Guid.NewGuid(), new Guid(parentId), value, type, new Version(version))
		{
		}
		
		[PersistanceConstructor]
		public MetadataIdentifier(string id, string parentId, string value, string type, string version) : this(new Guid(id), new Guid(parentId), value, type, new Version(version))
		{
		}
		
		public MetadataIdentifier(Guid id, Guid parentId, string value, string type, Version version)
		{
			this.id = id;
			this.parentId = parentId;
			this.value = value;
			this.type = type;
			this.version = version;
			this.dataStore = null;
		}
		#endregion
	
		#region Private Fields
		private Guid id;				
		private Guid parentId;
		private string value;
		private string type;
		private Version version;
		private IDataStore dataStore;
		#endregion

		#region IIdentifier Members
		[PersistanceProperty(PersistanceFieldType.Basic, IsRequired=true, Ordinal=3)]
		public string Value
		{
			get { return value; }
		}

		[PersistanceProperty(PersistanceFieldType.Basic, Ordinal=4)]
		public string Type
		{
			get { return type; }
		}

		[PersistanceProperty(PersistanceFieldType.Basic, Ordinal=5)]
		public IVersion Version
		{
			get { return version; }
		}

		public IdentificationResult CompareTo(IIdentifier other)
		{
			if (other != null)
			{
				if (other is BaseIdentifier)
				{
					if (this.Version.CompareTo(other.Version) == 0)
					{
						if ((string.Compare(this.Type, other.Type, true) == 0) && (string.Compare(this.Value, other.Value, true) == 0) && (this.Version.CompareTo(other.Version) == 0))
							return IdentificationResult.Match;
						else
							return IdentificationResult.IdMismatch;
					}
					else return IdentificationResult.VersionMismatch;
				}
				else return IdentificationResult.TypeMismatch;
			}
			else return IdentificationResult.None;
		}
		#endregion
	
		#region IMetadataIdentifier Members
		[PersistanceProperty(PersistanceFieldType.Basic, IsRequired=true, Ordinal=2)]
		public Guid ParentId
		{
			get { return parentId; }
			set { parentId = value; }
		}
		#endregion

		#region IPersistant Members
		[PersistanceProperty(PersistanceFieldType.Basic, IsPrimaryKey=true, IsRequired=true, Ordinal=1)]
		public Guid Id
		{
			get { return id; }
		}
				
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

		#region Public Methods
		public override string ToString()
		{
			return this.value;
		}
		#endregion
	}
}
