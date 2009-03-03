using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Papyrus.Views
{
	public struct ViewAction
	{
		public ViewAction(ViewActionType type) 
			: this(type, true)
		{
			Type = type;
			IsValid = true;
		}

		public ViewAction(ViewActionType type, bool isValid)
		{
			Type = type;
			IsValid = isValid;
		}

		public ViewActionType Type;
		public bool IsValid;
	}
}
