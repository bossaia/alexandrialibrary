﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources.Media
{
	public class Catalog : Entity, IAggregate
	{
		public Catalog(Uri id)
			: base(id, Schema.Types.Entities.CatalogType)
		{
		}

		#region IAggregate Members

		public IEnumerable<ILinkType> GetSubjectLinkTypes()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<ILinkType> GetObjectLinkTypes()
		{
			throw new NotImplementedException();
		}

		public IEntityCollection GetSubjects(ILinkType type)
		{
			throw new NotImplementedException();
		}

		public IEntityCollection GetObjects(ILinkType type)
		{
			throw new NotImplementedException();
		}

		public void SetSubjects(IEntityCollection subjects)
		{
			throw new NotImplementedException();
		}

		public void SetObjects(IEntityCollection objects)
		{
			throw new NotImplementedException();
		}

		public IValidationResult Validate()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
