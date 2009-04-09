using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Resources
{
	public class EntityType : Resource, IEntityType
	{
		public EntityType(Uri id, string idMask, string nameMask)
			: base(id)
		{
			this.idMask = idMask;
			this.nameMask = nameMask;
		}

		#region Private Members

		private string idMask;
		private string nameMask;

		#endregion

		#region IEntityType Members

		public string IdMask
		{
			get { return idMask; }
		}

		public string NameMask
		{
			get { return nameMask; }
		}

		#endregion
	}
}
