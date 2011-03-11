#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.IO;

namespace Gnosis.Core
{
	public interface IMediaStream : IDisposable
	{
		//Stream Members
		bool CanRead { get; }
		bool CanSeek { get; }
		bool CanWrite { get; }
		bool CanTimeout { get; }
		void Flush();
		long Length { get; }
		long Position { get; set; }
		int StreamIndex { get; set; }
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
		SeekState SeekState { get; }
		TimeSpan Duration { get; }
		TimeSpan Elapsed { get; set; }
		float PercentBuffered { get; }
		void Play();
		void Pause();
		void Resume();
		void Stop();
		void RefreshBufferState();
		void RefreshNetworkState();
		void RefreshPlaybackState();
		void RefreshSeekState();
		EventHandler<MediaStateChangedEventArgs> BufferStateChanged { get; set; }
		EventHandler<MediaStateChangedEventArgs> NetworkStateChanged { get; set; }
		EventHandler<MediaStateChangedEventArgs> PlaybackStateChanged { get; set; }
		EventHandler<MediaStateChangedEventArgs> SeekStateChanged { get; set; }
	}
}