#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using Alexandria.Media;
using Alexandria.Metadata;
using Alexandria.Persistence;

namespace Alexandria.Catalog
{	
	[Record("Catalog")]
	[RecordType("A7612A3C-1A83-4b66-80AD-AB001CA67EA3")]
	public class BaseCatalog : ICatalog, IRecord
	{
		#region Constructors
		[Factory("A7612A3C-1A83-4b66-80AD-AB001CA67EA3")]
		public BaseCatalog(Guid id, string name)
		{
			this.id = id;
			this.name = name;
		}
		#endregion
	
		#region Private Fields
		private Guid id;
		private string name;
		private IUser user;
		private List<IOldAlbum> albums = new List<IOldAlbum>();
		private List<IArtist> artists = new List<IArtist>();
		private List<IAudioTrack> tracks = new List<IAudioTrack>();
		
		private IPersistenceBroker persistenceBroker;
		#endregion

		#region ICatalog Members
		public string Name
		{
			get { return name; }
		}
		
		public IUser User
		{
			get { return user; }
			set { user = value; }
		}

		public IList<IOldAlbum> Albums
		{
			get { return albums; }
		}

		public IList<IArtist> Artists
		{
			get { return artists; }
		}

		public IList<IAudioTrack> Tracks
		{
			get { return tracks; }
		}
		#endregion

		#region IRecord Members
		public Guid Id
		{
			get { return id; }
		}

		public IRecord Parent
		{
			get { return user; }
			set { user = (IUser)value; }
		}

		public IPersistenceBroker PersistenceBroker
		{
			get { return persistenceBroker; }
			set { persistenceBroker = value; }
		}

		public virtual bool IsProxy
		{
			get { return false; }
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
	}
}
