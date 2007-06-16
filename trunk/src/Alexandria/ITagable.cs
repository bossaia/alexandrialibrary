using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ITagable
	{
		IList<ITag> Tags { get; }		
		void LoadAllTags();
	}
}
