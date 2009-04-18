﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface IResource
	{
		Uri Id { get; }
		string Hash { get; }
	}
}
