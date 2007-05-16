using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ITemporal : IMedia
	{
		TimeSpan Length { get; }
		TimeSpan Position { get; }
		void SetPosition(TimeSpan position);
	}
}
