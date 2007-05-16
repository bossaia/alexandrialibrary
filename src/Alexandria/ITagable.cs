using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ITagable
	{
		IList<ITag> Tags { get; }
		void LoadTag(int index);
		void LoadAllTags();
		void SaveTag(int index);
		void SaveAllTags();
	}
}
