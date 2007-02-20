using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria
{
	public delegate void SimpleEvent();
	public delegate void PlaybackFunction();
	public delegate AudioInfo GetAudioInfo(MediaFile file);
}
