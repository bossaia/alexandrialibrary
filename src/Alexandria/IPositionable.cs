using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IPositionable
	{
		void SetPosition(TimeSpan position);
		void SetPosition(TimeSpan position, PositionType type);
	}
}
