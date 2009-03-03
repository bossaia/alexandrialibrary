using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Model
{
	public struct Attribution
	{
		public static readonly Attribution Empty = default(Attribution);

		public Attribution(bool isLocation, Uri value)
		{
			this.isLocation = isLocation;
			this.value = value;
		}

		private bool isLocation;
		private Uri value;

		public bool IsLocation { get { return isLocation; } }
		public Uri Value { get { return value; } }
	}
}
