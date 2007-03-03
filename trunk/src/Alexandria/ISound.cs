using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace Alexandria
{
	[System.CLSCompliant(false)]
	public interface ISound
	{
		string Name {get;}
		MediaFile MediaFile {get;set;}
		uint Milliseconds {get;}
		uint Position {get;set;}
		string OpenStateName {get;}
		bool BufferIsStarving {get;}
		uint PercentBuffered {get;}
		SoundStatus Status {get;}
		[EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		void Save(string filePath);
		void Load();
		void Load(uint streamBufferSize);
		void Play();
		void Pause();
		void Stop();
	}
}
