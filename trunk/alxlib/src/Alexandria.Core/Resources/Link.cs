using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Resources
{
	public class Link : Resource, ILink
	{
		public Link(Uri id, LinkType type, IEntity subject, IEntity value)
			: this(id, type, subject, value, 0)
		{
		}

		public Link(Uri id, LinkType type, IEntity subject, IEntity value, int sequence)
			: base(id)
		{
			this.type = type;
			this.subject = subject;
			this.value = value;
			this.sequence = sequence;
		}

		#region Private Members

		private LinkType type;
		private IEntity subject;
		private IEntity value;
		private int sequence;

		#endregion

		#region ILink Members

		public ILinkType Type
		{
			get { return type; }
		}

		public IEntity Subject
		{
			get { return subject; }
		}

		public IEntity Value
		{
			get { return value; }
			set { this.value = value; }
		}

		public int Sequence
		{
			get { return sequence; }
			set { sequence = value; }
		}

		#endregion
	}
}
