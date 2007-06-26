using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;
using Alexandria.Media;
using Alexandria.Media.IO;

namespace Alexandria.Fmod
{
	public class RemoteSound : IAudioStream
	{
		#region Constructors
		public RemoteSound(string path)
		{
		}
		#endregion

		#region IAudioStream Members
		public bool IsMuted
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public float Volume
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}
		#endregion

		#region IMediaStream Members
		public BufferState BufferState
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public bool CanPlay
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public bool CanRead
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public bool CanSeek
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public bool CanSetElapsed
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public bool CanSetPosition
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public bool CanTimeout
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public bool CanWrite
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public TimeSpan Duration
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public TimeSpan Elapsed
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public void Flush()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public long Length
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public NetworkState NetworkState
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public string Path
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public void Pause()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public float PercentBuffered
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public void Play()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public PlaybackState PlaybackState
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public long Position
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public int Read(byte[] buffer, int offset, int count)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Resume()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public long Seek(long offset, System.IO.SeekOrigin origin)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void SetLength(long value)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Stop()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Write(byte[] buffer, int offset, int count)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion
	}
}
