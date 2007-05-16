using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IImage : IMedia
	{
		//TODO: determine how to abstract this further
		IList<byte> ImageData { get; }
	}
}
