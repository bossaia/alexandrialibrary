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
using Alexandria;
using Alexandria.Metadata;
using Alexandria.Persistence;

namespace Alexandria.Mp3Tunes
{
	[Record("AudioTrack_Mp3tunes")]
	[RecordType("4D5F5337-A34B-4f38-A175-18AF9F1A8A8D")]
	public class TrackAdditionalInfo : IRecord
	{
		#region Constructors
		[Factory("4D5F5337-A34B-4f38-A175-18AF9F1A8A8D")]
		public TrackAdditionalInfo(Guid id, string originalPath)
		{
			this.id = id;
			this.originalPath = new Uri(originalPath);
		}
		#endregion
		
		#region Private Fields
		private Guid id;
		private IPersistenceBroker persistenceBroker;
		private IAudioTrack track;
		private Uri originalPath;
		#endregion

		#region IRecord Members
		public Guid Id
		{
			get { return id; }
		}

		public IRecord Parent
		{
			get { return track; }
			set { track = (IAudioTrack)value; }
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
		
		#region Public Properties
		[Field(FieldType.Child, FieldRelationship.OneToOne, 2, "AudioTrackId", FieldConstraints.Required, FieldCascades.All)]
		public IAudioTrack Track
		{
			get { return track; }
			set { track = value; }
		}
		
		[Field(3, FieldConstraints.Required)]
		public Uri OriginalPath
		{
			get { return originalPath; }
		}
		#endregion
	}
}
