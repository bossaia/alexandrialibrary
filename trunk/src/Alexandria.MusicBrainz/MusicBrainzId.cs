#region License (MIT)
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
using Alexandria.Metadata;

namespace Alexandria.MusicBrainz
{
	public class MusicBrainzId : IMetadataIdentifier
	{
		#region Public Enum
		public enum MusicBrainzIdType
		{
			MusicBrainzId = 0,
			MusicBrainzReleaseId,
			MusicBrainzArtistId,
			MusicBrainzTrackId,
			MusicBrainzTrmId,
		}
		#endregion
	
		#region Constructors
		public MusicBrainzId(Guid value, MusicBrainzIdType type, IMetadata parent)
		{
			this.value = value;
			this.type = type;
			this.parent = parent;
		}
		#endregion
	
		#region Private Fields
		private IMetadata parent;
		private Guid value;
		private MusicBrainzIdType type = MusicBrainzIdType.MusicBrainzId;
		private readonly Version version = new Version(1, 0, 0, 0);
		#endregion
	
		#region IIdentifier Members
		public IMetadata Parent
		{
			get { return parent; }
			set { parent = value; }
		}
		
		public string Value
		{
			get { return value.ToString(); }
		}

		public string Type
		{
			get { return type.ToString(); }
		}
		
		public Version Version
		{
			get { return version; }
		}

		public IdentificationResult CompareTo(IMetadataIdentifier other)
		{
			if (other != null)
			{
				if (other is MusicBrainzId)
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
		
		#region Public Methods
		public override string ToString()
		{			
			return this.value.ToString();
		}
		#endregion

		#region IRecord Members

		public Guid Id
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public Alexandria.Persistence.IPersistenceBroker PersistenceBroker
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

		public void Save()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Delete()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion

		#region IMetadataIdentifier Members

		IMetadata IMetadataIdentifier.Parent
		{
			get
			{
				return parent;
			}
			set
			{
				parent = value;
			}
		}

		string IMetadataIdentifier.Value
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		string IMetadataIdentifier.Type
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		Version IMetadataIdentifier.Version
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		IdentificationResult IMetadataIdentifier.CompareTo(IMetadataIdentifier other)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion

		#region IRecord Members

		Guid Alexandria.Persistence.IRecord.Id
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		Alexandria.Persistence.IPersistenceBroker Alexandria.Persistence.IRecord.PersistenceBroker
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

		void Alexandria.Persistence.IRecord.Save()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		void Alexandria.Persistence.IRecord.Delete()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion
	}
}
