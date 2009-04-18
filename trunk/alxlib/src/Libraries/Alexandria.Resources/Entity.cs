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

		private IEntityType type;
		private string name;

		#endregion

		#region IEntity Members

		public IEntityType Type
		{
			get { return type; }
		}

		public string Name
		{
			get { return name; }
			set {
				//TODO: implement some hash-scheme here
				SetHash(value);
				name = value;
			}
		}

		#endregion
	}
}
