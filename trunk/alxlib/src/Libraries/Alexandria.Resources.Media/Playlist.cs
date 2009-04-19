﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources.Media
{
	public class Playlist : Entity, IAggregate
	{
		public Playlist(Uri id)
			: base(id)
		{
		}

		#region IAggregate Members

		public IValidation Validate()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}