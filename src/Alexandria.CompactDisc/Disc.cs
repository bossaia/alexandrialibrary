using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.CompactDisc
{
	public abstract class Disc : IResource
	{
		#region IResource Members
		public IIdentifier Id
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}
		
		public ILocation Location
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}
		
		public IFormat Format
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}
		#endregion
	}
}
