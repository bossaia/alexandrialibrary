using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria
{
	public class CompactDisc : IResource
	{
		#region IMediaResource Members

		public IDictionary<string, Uri> Paths
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public ResourceFormat Format
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public Uri Uri
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public IDictionary<object, IResource> Resources
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public void SaveAs(ResourceFormat format, Uri uri)
		{
			//TODO: implement ripping logic here
		}
		#endregion
	}
}
