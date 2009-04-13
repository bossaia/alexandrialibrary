using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public class LinkType : Resource, ILinkType
	{
		public LinkType(Uri id, bool isSequential)
			: base(id)
		{
			this.isSequential = isSequential;
		}

		#region Private Members

		private bool isSequential;

		#endregion

		#region ILinkType Members

		public bool IsSequential
		{
			get { return isSequential; }
		}

		#endregion
	}
}
