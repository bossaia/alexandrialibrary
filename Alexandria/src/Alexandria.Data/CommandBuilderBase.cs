using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Data
{
	public abstract class CommandBuilderBase
		//: ICommandBuilder
	{
		protected CommandBuilderBase(IConnectionSource source)
		{
		}
	}
}
