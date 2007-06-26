using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Alexandria.Media.IO
{
	public interface IMediaStream
	{
		//Stream Members
		bool CanRead { get; }
		bool CanSeek { get; }
		bool CanWrite { get; }
		bool CanTimeout { get; }
		void Flush();		
		long Length { get; }
		long Position { get; set; }
		int Read(byte[] buffer, int offset, int count);
		long Seek(long offset, SeekOrigin origin);
		void SetLength(long value);
		void Write(byte[] buffer, int offset, int count);
	
		string Path { get; }
		bool CanSetPosition { get; }
		bool CanSetElapsed { get; }
		bool CanPlay { get; }
		BufferState BufferState { get; }
		PlaybackState PlaybackState { get; }
		NetworkState NetworkState { get; }		
		TimeSpan Duration { get; }
		TimeSpan Elapsed { get; set; }
		float PercentBuffered { get; }
		void Play();
		void Pause();
		void Resume();
		void Stop();
	}
}
