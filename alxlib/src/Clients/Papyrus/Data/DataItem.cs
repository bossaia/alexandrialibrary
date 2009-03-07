using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Papyrus.Data
{
	public class DataItem<T>
	{
		public T Value { get; set; }
		public DataStatus Status { get; set; }
	}
}
