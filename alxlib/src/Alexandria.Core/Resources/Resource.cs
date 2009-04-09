using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Resources
{
	public abstract class Resource : IResource
	{
		protected Resource(Uri id)
		{
			this.id = id;
		}

		protected Resource(Uri id, string name)
			: this(id)
		{
			this.name = name;
		}

		#region Private Members

		private Uri id;
		private string name;
		private string hash;
		private Uri creator;
		private DateTime created;
		private Uri modifier;
		private DateTime modified;

		#endregion

		#region Protected Members

		protected virtual void SetName(string value)
		{
			name = value;

			//TODO: set hash here
		}

		#endregion

		#region IResource Members

		public Uri Id
		{
			get { return id; }
		}

		public string Name
		{
			get { return name; }
			set { SetName(value); }
		}

		public string Hash
		{
			get { return hash; }
			protected set { hash = value; }
		}

		public Uri Creator
		{
			get { return creator; }
			set { creator = value; }
		}

		public DateTime Created
		{
			get { return created; }
			set { created = value; }
		}

		public Uri Modifier
		{
			get { return modifier; }
			set { modifier = value; }
		}

		public DateTime Modified
		{
			get { return modified; }
			set { modified = value; }
		}

		#endregion
	}
}
