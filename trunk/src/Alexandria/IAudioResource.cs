using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	[CLSCompliant(false)]
	public interface IAudioResource : IResource
	{
		void Load();
		void Load(uint streamBufferSize);
		void Play();
		void Pause();
		void Stop();
		void Seek(uint position);
		uint Milliseconds {get;}
		uint Position {get;}
		IAudioStatus Status {get;}
	}
}
