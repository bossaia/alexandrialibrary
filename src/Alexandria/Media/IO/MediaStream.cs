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
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Alexandria.Media.IO
{
	public abstract class MediaStream : Stream, IMediaStream
	{
		#region Constructors
		public MediaStream(string path)
		{
			this.path = path;		
		}
		#endregion
		
		#region Private Fields
		//private bool canRead;
		//private bool canSeek;
		//private bool canWrite;
		private long length;		
		private long position;
		private int streamIndex;
		
		private string path;
		private bool canPlay;
		private bool canSetPosition;
		private bool canSetElapsed;
		private BufferState bufferState = BufferState.None;
		private PlaybackState playbackState = PlaybackState.None;
		private NetworkState networkState = NetworkState.None;
		private TimeSpan duration = TimeSpan.Zero;
		private TimeSpan elapsed = TimeSpan.Zero;
		private float percentBuffered;
		
		private EventHandler<MediaStateChangedEventArgs> onBufferStateChanged;
		private EventHandler<MediaStateChangedEventArgs> onNetworkStateChanged;
		private EventHandler<MediaStateChangedEventArgs> onPlaybackStateChanged;
		#endregion

		#region Stream Members
		public override bool CanRead
		{
			get { return false; }
		}

		public override bool CanSeek
		{
			get { return false; }
		}

		public override bool CanWrite
		{
			get { return false; }
		}		

		public override void Flush()
		{
		}

		public override long Length
		{
			get { return length; }
		}

		public override long Position
		{
			get { return position; }
			set { position = value; }
		}

		public int StreamIndex
		{
			get { return streamIndex; }
			set { streamIndex = value; }
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!CanRead)
				throw new InvalidOperationException("MediaStream read error: this stream is not readable");
			
			return 0;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			if (!CanSeek)
				throw new InvalidOperationException("MediaStream seek error: this stream is not seekable");
				
			return 0;
		}

		public override void SetLength(long value)
		{
			this.length = value;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (!CanWrite)
				throw new InvalidOperationException("MediaStream write error: this stream is read-only");
		}
		#endregion

		#region IMediaStream Members
		public string Path
		{
			get { return path; }
		}

		public bool CanSetPosition
		{
			get { return canSetPosition; }
			protected set { canSetPosition = value; }
		}

		public bool CanSetElapsed
		{
			get { return canSetElapsed; }
			protected set { canSetElapsed = value; }
		}

		public bool CanPlay
		{
			get { return canPlay; }
			protected set { canPlay = value; }
		}

		public BufferState BufferState
		{
			get { return bufferState; }
			protected set { bufferState = value; }
		}

		public PlaybackState PlaybackState
		{
			get { return playbackState; }
			protected set { playbackState = value; }
		}

		public NetworkState NetworkState
		{
			get { return networkState; }
			protected set { networkState = value; }
		}

		public TimeSpan Duration
		{
			get { return duration; }
			protected set { duration = value; }
		}

		public TimeSpan Elapsed
		{
			get { return elapsed; }
			set
			{
				if (!CanSetElapsed)
					throw new InvalidOperationException("MediaStream error setting elapsed: elapsed is read-only");
			}
		}


		public EventHandler<MediaStateChangedEventArgs> OnBufferStateChanged
		{
			get { return onBufferStateChanged; }
			set { onBufferStateChanged = value; }
		}

		public EventHandler<MediaStateChangedEventArgs> OnNetworkStateChanged
		{
			get { return onNetworkStateChanged; }
			set { onNetworkStateChanged = value; }
		}

		public EventHandler<MediaStateChangedEventArgs> OnPlaybackStateChanged
		{
			get { return onPlaybackStateChanged; }
			set { onPlaybackStateChanged = value; }
		}
		
		public float PercentBuffered
		{
			get { return percentBuffered; }
			protected set { percentBuffered = value; }
		}

		public virtual void Play()
		{
			if (!CanPlay)
				throw new InvalidOperationException("MediaStream playback error: this stream does not support playback");
		}

		public virtual void Pause()
		{
			if (!CanPlay)
				throw new InvalidOperationException("MediaStream playback error: this stream does not support playback");
		}

		public virtual void Resume()
		{
			if (!CanPlay)
				throw new InvalidOperationException("MediaStream playback error: this stream does not support playback");
		}

		public virtual void Stop()
		{
			if (!CanPlay)
				throw new InvalidOperationException("MediaStream playback error: this stream does not support playback");
		}

		public virtual void RefreshBufferState()
		{
		}
		
		public virtual void RefreshNetworkState()
		{
		}
		
		public virtual void RefreshPlaybackState()
		{
		}
		#endregion
	}
}
