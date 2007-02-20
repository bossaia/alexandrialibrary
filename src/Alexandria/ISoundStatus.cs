using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria
{
	public interface ISoundStatus
	{
		BufferState BufferState{get;}
		PlaybackState PlaybackState{get;}
		SeekState SeekState{get;}
		float BufferLevel{get;}
		bool AllowsLoad{get;}
		bool AllowsStream{get;}
		bool AllowsPlay{get;}
		bool AllowsPause{get;}
		bool AllowsStop{get;}
		bool AllowsSeek{get;}
		void Update(ISoundCommand command);
	}
}
