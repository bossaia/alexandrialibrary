using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ITagPlugin : IPlugin
	{
		T GetTagFactory<T>() where T : ITag;
		T GetTagFactory<T>(string name);
		T GetTagFactory<T>(IMediaCapability<T> capability) where T : ITag;
	}
}
