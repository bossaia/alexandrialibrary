using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Model
{
	public interface IMediaSetFactory
	{
		IMediaSet GetMediaSet(Uri path);
	}
}
