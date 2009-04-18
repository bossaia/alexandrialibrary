using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
		private string hash;

		#endregion

		#region Protected Members

		protected virtual void SetHash(string value)
		{
			hash = value;
		}

		#endregion

		#region IResource Members

		public Uri Id
		{
			get { return id; }
		}

		public string Hash
		{
			get { return hash; }
		}

		#endregion
	}
}
