using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Creation
{
	public interface ISet : IPiece
	{
		IList<IPiece> Pieces { get; }
	}
}
