﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface IAggregate : IEntity
	{
		IValidation Validate();
	}
}