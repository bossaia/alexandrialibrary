using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.CompactDiscTools
{
	public abstract class Disc //: IMedia
	{
		#region IMedia Members
		public Guid Id
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}
		
		public Uri Path
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}
		
		//public IMediaFormat Format
		//{
			//get { throw new Exception("The method or operation is not implemented."); }
		//}
		#endregion

		#region IEntity Members


		public string Name
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		#endregion

		#region IProxy Members

		public void Load()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion
	}
}
