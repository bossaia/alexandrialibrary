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

namespace Alexandria.Catalog
{
	[Class("User", LoadType.Constructor, "Id")]
	public class BaseUser : IUser, IPersistent
	{
		#region Constructors
		[Constructor]
		public BaseUser(Guid id, string name, string password)
		{
			this.id = id;
			this.name = name;
			this.password = password;
		}
		#endregion
	
		#region Private Fields
		private Guid id;
		private string name;
		private string password;
		private List<IUserCatalog> userCatalogs = new List<IUserCatalog>();
		private IDataStore dataStore;
		#endregion
	
		#region IUser Members
		[Property(FieldType.Basic, LoadType.Constructor, Ordinal=2)]
		public string Name
		{
			get { return name; }
		}

		[Property(FieldType.Basic, LoadType.Constructor, Ordinal=3)]
		public string Password
		{
			get { return password; }
		}

		public bool Authenticate(string name, string password)
		{
			return (this.name == name && this.password == password);
		}
		#endregion

		#region IPersistent Members
		[Property(FieldType.Basic, LoadType.Constructor, Ordinal=1, IsPrimaryKey=true, IsRequired=true)]
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
		
		#region Public Properties
		[Property(FieldType.OneToManyChildren, LoadType.Property, "UserID", typeof(BaseUserCatalog), CascadeSave=true, CascadeDelete=true)]
		public IList<IUserCatalog> UserCatalogs
		{
			get { return userCatalogs; }
		}
		#endregion
	}
}
