using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria
{
	[System.CLSCompliant(false)]
	public interface ITagEngine
	{
		IAudioTag GetAudioTag(MediaFile file);
	}
}
