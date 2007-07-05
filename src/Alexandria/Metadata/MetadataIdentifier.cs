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
using Alexandria.Persistence;

namespace Alexandria.Metadata
{
	public enum IdentificationResult
	{
		None = 0,
		TypeMismatch,
		VersionMismatch,
		IdMismatch,
		Match
	}

	public struct MetadataIdentifier : IMetadataIdentifier
	{
		#region Constructors
		[Constructor("585F6263-29FA-41ae-93A0-9250348CEB4D")]
		public MetadataIdentifier(Guid id, IMetadata parent, string value, string type, Version version)
		{
			this.id = id;
			this.parent = parent;
			this.value = value;
			this.type = type;
			this.version = version;
			this.persistenceBroker = null;
		}
		#endregion
	
		#region Private Fields
		private Guid id;
		private IMetadata parent;
		private string value;
		private string type;
		private Version version;
		private IPersistenceBroker persistenceBroker;
		#endregion

		#region IMetadataIdentifier Members
		public IMetadata Parent
		{
			get { return parent; }
			set { parent = value; }
		}
		
		public string Value
		{
			get { return value; }
		}

		public string Type
		{
			get { return type; }
		}

		public Version Version
		{
			get { return version; }
		}

		public IdentificationResult CompareTo(IMetadataIdentifier other)
		{
			if (other != null)
			{
				if (other is MetadataIdentifier)
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

		#region IPersistant Members
		[Property(1, IsPrimaryKey=true, IsRequired=true)]
		public Guid Id
		{
			get { return id; }
		}
				
		public IPersistenceBroker PersistenceBroker
		{
			get { return persistenceBroker; }
			set { persistenceBroker = value; }
		}

		public void Save()
		{
			persistenceBroker.SaveRecord(this);
		}

		public void Delete()
		{
			persistenceBroker.DeleteRecord(this);
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
