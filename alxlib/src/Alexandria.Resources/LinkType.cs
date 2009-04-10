using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public class LinkType : Resource, ILinkType
	{
		public LinkType(Uri id)
			: base(id)
		{
		}

		#region Private Members

		private string subjectMask;
		private string valueMask;
		private string sequenceMask;

		#endregion

		#region ILinkType Members

		public string SubjectMask
		{
			get { return subjectMask; }
			set { subjectMask = value; }
		}

		public string ValueMask
		{
			get { return valueMask; }
			set { valueMask = value; }
		}

		public string SequenceMask
		{
			get { return sequenceMask; }
			set { sequenceMask = value; }
		}

		#endregion
	}
}
