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
		private bool canRead;
		private bool canSeek;
		private bool canWrite;
		private long length;		
		private long position;
		
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
		#endregion

		#region Stream Members
		public override bool CanRead
		{
			get { return canRead; }
		}

		public override bool CanSeek
		{
			get { return canSeek; }
		}

		public override bool CanWrite
		{
			get { return canWrite; }
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
		}

		public PlaybackState PlaybackState
		{
			get { return playbackState; }
		}

		public NetworkState NetworkState
		{
			get { return networkState; }
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
		#endregion
	}
}
