using System;
using System.IO;
using System.Runtime.InteropServices;
using Alexandria.Media.IO;

namespace Alexandria.Fmod
{
	public class LocalSound : IDisposable, IAudioStream
	{
		#region Constructors
		public LocalSound(string path)
		{
			this.path = path;
			sound = SoundSystemFactory.DefaultSoundSystem.CreateStream(path, Modes.None);
		}
		#endregion

		#region Finalizer
		~LocalSound()
		{
			Dispose(false);
		}
		#endregion

		#region IDisposable Members
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					this.sound.Dispose();
					this.sound = null;
				}
			}
			disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion	
		
		#region Private Fields
		private string path;
		private Sound sound;
		private bool disposed;
		private BufferState bufferState = BufferState.None;
		private PlaybackState playbackState = PlaybackState.None;
		#endregion
		
		#region Private Methods
		
		#region RefreshBufferState
		private BufferState RefreshBufferState()
		{
			if (sound != null)
			{
				if (sound.BufferIsStarving)
				{
					bufferState = BufferState.Starving;
				}
				else
				{
					if (sound.OpenState == OpenState.Loading)
						bufferState = BufferState.Loading;
					else if (sound.OpenState == OpenState.Buffering)
					{
						if (sound.PercentBuffered < 100)
							bufferState = BufferState.Buffering;
						else bufferState = BufferState.Full;
					}
					else bufferState = BufferState.None;
				}
			}
			else bufferState = BufferState.None;
			
			return bufferState;
		}
		#endregion
		
		#region RefreshPlaybackState
		private PlaybackState RefreshPlaybackState()
		{
			if (sound != null && sound.Channel != null)
			{
				if (sound.Channel.IsPlaying)
				{
					if (sound.Channel.Paused)
						playbackState = PlaybackState.Paused;
					else playbackState = PlaybackState.Playing;
				}
				else playbackState = PlaybackState.Stopped;
			}
			else playbackState = PlaybackState.None;
			
			return playbackState;
		}
		#endregion
		
		#endregion

		#region IAudioStream Members
		public bool IsMuted
		{
			get { return sound.Channel.Mute; }			
			set { sound.Channel.Mute = value; }
		}

		public float Volume
		{
			get { return sound.Channel.Volume; }
			set { sound.Channel.Volume = value; }
		}
		#endregion

		#region IMediaStream Members
		public BufferState BufferState
		{
			get { return RefreshBufferState(); }
		}

		public bool CanPlay
		{
			get { return true; }
		}

		public bool CanRead
		{
			get { return true; }
		}

		public bool CanSeek
		{
			get { return true; }
		}

		public bool CanSetElapsed
		{
			get { return true; }
		}

		public bool CanSetPosition
		{
			get { return true; }
		}

		public bool CanTimeout
		{
			get { return false; }
		}

		public bool CanWrite
		{
			get { return false; }
		}

		public TimeSpan Duration
		{
			get { return sound.Duration; }
		}

		public TimeSpan Elapsed
		{
			get { return new TimeSpan(0, 0, 0, 0, (int)sound.Channel.Position); }
			set
			{
				lock(sound)
				{
					sound.Channel.Position = (uint)value.TotalMilliseconds;
				}
			}
		}

		public void Flush()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public long Length
		{
			get
			{
				sound.LengthUnit = TimeUnits.RawByte;
				return (long)sound.FmodLength;
			}
		}

		public NetworkState NetworkState
		{
			get { return NetworkState.None; }
		}

		public string Path
		{
			get { return path; }
		}

		public void Pause()
		{
			lock(sound)
			{
				sound.Pause();
				RefreshPlaybackState();
			}
		}

		public float PercentBuffered
		{
			get { return (float)sound.PercentBuffered; }
		}

		public void Play()
		{
			lock(sound)
			{
				sound.Play();
				RefreshPlaybackState();
			}
		}

		public PlaybackState PlaybackState
		{
			get { return RefreshPlaybackState(); }
		}

		public long Position
		{
			get { return (long)sound.Channel.PositionInBytes; }
			set
			{
				lock(sound)
				{
					sound.Channel.PositionInBytes = (uint)value;
				}
			}
		}

		public int StreamIndex
		{
			get { return 0; }
			set { }
		}

		/// <summary>
		/// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read
		/// </summary>
		/// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and (offset + count - 1) replaced by the bytes read from the current source.</param>
		/// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream.</param>
		/// <param name="count">The maximum number of bytes to be read from the current stream.</param>
		/// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>		
		/// <exception cref="System.ArgumentException"/>
		/// <exception cref="System.ArgumentNullException"/>
		/// <exception cref="System.OutOfMemoryException"/>
		public int Read(byte[] buffer, int offset, int count)
		{
			if (buffer != null)
			{
				if (offset + count <= buffer.Length)
				{
					lock(sound)
					{
						Stop();

						IntPtr bufferHandle = IntPtr.Zero;
						bufferHandle = Marshal.AllocHGlobal(count);				

						int bytesRead = Convert.ToInt32(sound.Read(bufferHandle, (uint)count));
									
						if (bufferHandle != IntPtr.Zero && bytesRead > 0)
						{
							byte[] data = new byte[bytesRead];
							Marshal.PtrToStructure(bufferHandle, data);
							Array.Copy(data, 0, buffer, offset, bytesRead);
							Marshal.FreeHGlobal(bufferHandle);
							return bytesRead;
						}
						else return 0;
					}
				}
				else throw new ArgumentException("The sum of offset and count is larger than the buffer length.");
			}
			else throw new ArgumentNullException("buffer");
		}

		public void Resume()
		{
			lock(sound)
			{
				sound.Resume();
				RefreshPlaybackState();
			}
		}

		public long Seek(long offset, SeekOrigin origin)
		{
			uint trueOffset = (offset <= uint.MaxValue) ? (uint)offset : uint.MaxValue;
			
			lock(sound)
			{
				switch (origin)
				{
					case SeekOrigin.Begin:
						if (trueOffset < 0) trueOffset = 0;
						else if (trueOffset > Length) trueOffset = (uint)Length;
						sound.Channel.PositionInBytes = trueOffset;
						break;
					case SeekOrigin.Current:
						uint currentPosition = sound.Channel.PositionInBytes;
						if (currentPosition + trueOffset > uint.MaxValue)
						{
							if (Length < uint.MaxValue)
								sound.Channel.PositionInBytes = (uint)Length;
							else sound.Channel.PositionInBytes = uint.MaxValue;
						}
						else
						{
							if (currentPosition + trueOffset > Length)
								sound.Channel.PositionInBytes = (uint)Length;
							else sound.Channel.PositionInBytes = (currentPosition + trueOffset);
						}
						break;
					case SeekOrigin.End:
						if (Length + trueOffset > uint.MaxValue)
						{
							if (Length < uint.MaxValue)
								sound.Channel.PositionInBytes = (uint)Length;
							else sound.Channel.PositionInBytes = uint.MaxValue;
						}
						else
						{
							if (Length + trueOffset > Length)
								sound.Channel.PositionInBytes = (uint)Length;
							else sound.Channel.PositionInBytes = Convert.ToUInt32(Length + trueOffset);
						}
						break;
					default:
						break;
				}
				
				return Position;
			}
		}

		public void SetLength(long value)
		{
		}

		public void Stop()
		{
			lock(sound)
			{
				sound.Stop();
				RefreshPlaybackState();
			}
		}

		public void Write(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException("This stream is read-only");
		}
		#endregion
	}
	
	#region Old Code
	/*
	public class LocalSound : IDisposable, ILocalAudioOutput
	{
		#region Constructors
		public LocalSound(string path)
		{
			location = new Location(path);
		}
		#endregion
	
		#region Finalizer
		~LocalSound()
		{
			Dispose(false);
		}
		#endregion
		
		#region IDisposable Members
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					this.sound.Dispose();
					this.sound = null;
				}				
			}
			disposed = true;
		}
		
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion	
	
		#region Constants
		private const int DEFAULT_SEEK_SPEED = 1;
		#endregion
	
		#region Private Fields
		private Sound sound;
		
		private bool disposed;
		
		private IMediaFormat format;
		private Guid id = Guid.NewGuid();
		private ILocation location;
		private PlaybackState playbackState = PlaybackState.None;
		private bool isSeeking;
		private int seekSpeed;
		private SeekDirection seekDirection = SeekDirection.None;
		private TimeSpan duration = TimeSpan.Zero;
		#endregion
	
		#region Private Methods
		private void RefreshPlaybackState()
		{
			if (this.sound.Channel != null)
			{
				if (this.sound.Channel.IsPlaying)
				{
					if (this.sound.Channel.Paused)
						playbackState = PlaybackState.Paused;
					else playbackState = PlaybackState.Playing;
				}
				else playbackState = PlaybackState.Stopped;
			}
			else playbackState = PlaybackState.None;
		}

		private IntPtr GetReadBuffer(int length)
		{
			//sound.LengthUnit = TimeUnits.PcmByte; //RawByte;
			//uint numberOfBytes = sound.FmodLength;
			uint bytesRead;
			
			IntPtr buffer = IntPtr.Zero;

			try
			{
				buffer = Marshal.AllocHGlobal(length); // AllocCoTaskMem((int)numberOfBytes);
			}
			catch (OutOfMemoryException ex)
			{
				throw new ApplicationException("There was an error reading the sound data: ran out of memory trying to allocate the buffer", ex);
			}

			bytesRead = sound.Read(buffer, (uint)length);
			if (bytesRead == length)
			{
				return buffer;
			}
			else throw new ApplicationException("There was an error reading the sound data: could not read to end of file (unexpected eof?)");
		}

		private void CleanupReadBuffer(IntPtr buffer)
		{
			try
			{
				Marshal.FreeHGlobal(buffer); //FreeCoTaskMem(buffer);
			}
			catch (Exception ex)
			{
				throw new ApplicationException("There was an error freeing the memory used for this buffer", ex);
			}
		}
		#endregion
	
		#region IMedia Members
		public IMediaFormat Format
		{
			get { return format; }
		}

		public Guid Id
		{
			get { return id; }
		}

		public void Load()
		{
			if (sound == null)
			{
				sound = SoundSystemFactory.DefaultSoundSystem.CreateStream(location.Path, Modes.None);
				format = new SoundFormat(sound.FmodSoundFormat, sound.FmodSoundType);
				duration = sound.Duration;
			}
		}

		public ILocation Location
		{
			get { return location; }
		}
		#endregion

		#region IAudible Members
		public bool IsMuted
		{
			get { return this.sound.Channel.Mute; }
		}

		public void Mute()
		{
			this.sound.Channel.Mute = true;
		}

		public void SetVolume(float volume)
		{
			this.sound.Channel.Volume = volume;
		}

		public void Unmute()
		{
			this.sound.Channel.Mute = false;
		}

		public float Volume
		{
			get { return this.sound.Channel.Volume; }
		}
		#endregion

		#region IPlayable Members
		public void Pause()
		{
			this.sound.Pause();
			RefreshPlaybackState();
		}

		public void Play()
		{
			this.sound.Play();
			RefreshPlaybackState();
		}

		public PlaybackState PlaybackState
		{
			get { return playbackState; }
		}

		public void Resume()
		{
			this.sound.Resume();
			RefreshPlaybackState();
		}

		public void Stop()
		{
			this.sound.Stop();
			RefreshPlaybackState();
		}
		#endregion

		#region ISeekable Members
		public bool IsSeeking
		{
			get { return isSeeking; }
		}

		public void SeekBackward(int seekSpeed)
		{
			this.isSeeking = true;
			this.seekDirection = SeekDirection.Backward;
			this.seekSpeed = seekSpeed;
			throw new Exception("The method or operation is not implemented.");
		}

		public void SeekBackward()
		{
			SeekBackward(DEFAULT_SEEK_SPEED);
		}

		public void SeekForward(int seekSpeed)
		{
			this.isSeeking = true;
			this.seekDirection = SeekDirection.Forward;
			this.seekSpeed = seekSpeed;
			throw new Exception("The method or operation is not implemented.");
		}

		public void SeekForward()
		{
			SeekForward(DEFAULT_SEEK_SPEED);
		}

		public SeekDirection SeekDirection
		{
			get { return seekDirection; }
		}

		public int SeekSpeed
		{
			get { return seekSpeed; }
		}
		
		public void StopSeeking()
		{
			isSeeking = false;
			seekDirection = SeekDirection.None;
			seekSpeed = 0;
		}
		#endregion

		#region IHasDuration Members
		public TimeSpan Duration
		{
			get { return duration; }
		}
		#endregion

		#region IHasElapsed Members
		public TimeSpan GetElapsed()
		{			
			return new TimeSpan(0, 0, 0, 0, (int)this.sound.Channel.Position);
		}
		#endregion

		#region IPositionable Members
		public void SetAbsolutePosition(TimeSpan position)
		{
			this.sound.Channel.Position = (uint)position.Milliseconds;
			throw new Exception("The method or operation is not implemented.");
		}

		public void SetRelativePosition(TimeSpan position)
		{
			SetAbsolutePosition(GetElapsed().Add(position));
		
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion
		
		#region IHasRawAudioData Members
		public int NumberOfBytes
		{
			get
			{
				sound.LengthUnit = TimeUnits.PcmByte; //RawByte;
				return (int)sound.FmodLength;
			}
		}
		
		public int NumberOfSamples
		{
			get
			{
				sound.LengthUnit = TimeUnits.PcmSample;
				return (int)sound.FmodLength;
			}
		}
		
		public int SampleRate
		{
			get { return sound.NumberOfBitsPerSample; }
		}
		
		public bool IsStereo
		{
			get { return (sound.NumberOfChannels == 2); }
		}
		
		public byte[] ReadData(int length)
		{
			IntPtr buffer = GetReadBuffer(length);
			if (buffer != IntPtr.Zero)
			{
				byte[] data = null;
				Marshal.PtrToStructure(buffer, data);
				CleanupReadBuffer(buffer);
				return data;
			}
			else return null;
		}
		#endregion
	}
	*/
	#endregion
}
