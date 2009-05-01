using System;
using System.Collections.Generic;

namespace Alexandria.Resources
{
	public abstract class Resource : IResource
	{
		protected Resource(Uri id)
		{
			this.id = id;
		}

		#region Private Members

		private Uri id;
		private string name;
		private Dictionary<string, object> extensions = new Dictionary<string, object>();

		#endregion

		#region IResource Members

		public Uri Id
		{
			get { return id; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public virtual IDictionary<string, Type> GetSchema()
		{
			return null;
		}

		public virtual IEnumerable<T> GetValues<T>(string name)
		{
			return null;
		}

		public virtual void SetValues<T>(string name, IEnumerable<T> values)
		{
		}

		#endregion
	}
}
