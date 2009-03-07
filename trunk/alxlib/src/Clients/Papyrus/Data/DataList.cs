using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Papyrus.Data
{
	public class DataList<T>
	{
		private IList<DataItem<T>> items = new List<DataItem<T>>();

		public DataStatus Status { get; set; }
		public IList<DataItem<T>> Items { get { return items; } }
	}
}
