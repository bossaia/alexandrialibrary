using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public abstract class Entity : Resource, IEntity
	{
		protected Entity(Uri id, IEntityType type)
			: base(id)
		{
			this.type = type;
		}

		#region Private Members

		protected IEntityType type;
		private ILinkCollection links = new LinkCollection();

		#endregion

		#region IEntity Members

		public IEntityType Type
		{
			get { return type; }
		}

		public ILinkCollection Links
		{
			get { return links; }
		}

		public void AddLink(ILinkType type, IEntity value)
		{
		}

		public void AddLink(ILinkType type, IEntity value, int sequence)
		{
		}

		public void RemoveLink(ILink link)
		{
		}

		#endregion
	}
}
