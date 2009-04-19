using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public abstract class Entity : Resource, IEntity
	{
		protected Entity(Uri id)
			: base(id)
		{
		}

		#region Private Members

		private string name;

		#endregion

		#region IEntity Members

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
