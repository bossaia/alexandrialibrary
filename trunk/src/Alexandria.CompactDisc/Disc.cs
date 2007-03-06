using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.CompactDisc
{
	public abstract class Disc : IResource
	{
		#region IResource Members

		public ResourceFormat Format
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public Uri Uri
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public IDictionary<object, IResource> Resources
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public bool IsLocal
		{
			get { return true; }
		}

		#endregion
	}
}
