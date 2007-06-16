using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IPositionable
	{
		void SetAbsolutePosition(TimeSpan position);
		void SetRelativePosition(TimeSpan position);
	}
}
