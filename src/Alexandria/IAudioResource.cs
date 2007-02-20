using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria
{
	[CLSCompliant(false)]
	public interface IAudioResource : IMediaResource
	{
		void Load();
		void Load(uint streamBufferSize);
		void Play();
		void Pause();
		void Stop();
		void Seek(uint position);
		uint Milliseconds {get;}
		uint Position {get;}
		SoundStatus Status {get;} //TODO: rename this to AudioStatus
	}
}
