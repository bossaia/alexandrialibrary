using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public abstract class EntityBase
		: IEntity
	{
		protected EntityBase()
			: this(0)
		{
		}

		protected EntityBase(long id)
		{
			_id = id;
		}

		private long _id;

		#region IEntity Members

		public long Id
		{
			get { return _id; }
		}

		#endregion
	}
}
