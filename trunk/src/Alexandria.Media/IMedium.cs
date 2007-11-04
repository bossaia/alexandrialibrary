using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Media
{
	public interface IMedium
	{
		string Name { get; set; }
		MediaTypes Type { get; set; }
		//Dimensions
	}
}
