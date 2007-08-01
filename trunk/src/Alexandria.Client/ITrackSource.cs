using System;
using System.Collections.Generic;
using System.Text;
using Alexandria.Metadata;

namespace Alexandria.Client
{
	public interface ITrackSource
	{
		IList<IAudioTrack> GetAudioTracks();
	}
}
