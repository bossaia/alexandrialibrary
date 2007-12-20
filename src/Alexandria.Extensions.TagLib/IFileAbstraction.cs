using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.TagLib
{
	public interface IFileAbstraction
	{
		string Name {get;}
		System.IO.Stream ReadStream {get;}
		System.IO.Stream WriteStream {get;}
		bool IsReadable {get;}
		bool IsWritable {get;}
	}
}
