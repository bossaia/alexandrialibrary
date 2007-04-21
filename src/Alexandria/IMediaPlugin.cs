using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMediaPlugin : IPlugin
	{
		//IMediaCapability<T> GetCapability<T>();
		//IMediaFactory GetMedia<T>() where T: IMedia;
		T GetMedia<T>(ISearch search) where T: IMedia;
	}
}
