using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.Fmod
{
	public class LocalSound : ILocalAudio, IDisposable
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
				
					this.channel.Dispose();
					this.channel = null;
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
		private Channel channel = new Channel();
		
		private bool disposed;
		
		private IMediaFormat format;
		private IIdentifier id = new GuidIdentifier();
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
			if (this.channel != null)
			{
				if (this.channel.IsPlaying)
				{
					if (this.channel.Paused)
						playbackState = PlaybackState.Paused;
					else playbackState = PlaybackState.Playing;
				}
				else playbackState = PlaybackState.Stopped;
			}
			else playbackState = PlaybackState.None;
		}
		#endregion
	
		#region IMedia Members
		public IMediaFormat Format
		{
			get { return format; }
		}

		public IIdentifier Id
		{
			get { return id; }
		}

		public void Load()
		{
			if (sound == null)
			{
				if (location != null)
				{
					sound = new Sound(SoundSystemFactory.DefaultSoundSystem, location);
					format = new SoundFormat(sound.FmodSoundFormat, sound.SoundType);
					duration = sound.Duration;
				}
				else throw new InvalidOperationException("Could not load this sound: location undefined");
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
			get { return channel.Mute; }
		}

		public void Mute()
		{
			this.channel.Mute = true;
		}

		public void SetVolume(float volume)
		{
			this.channel.Volume = volume;
		}

		public void Unmute()
		{
			this.channel.Mute = false;
		}

		public float Volume
		{
			get { return channel.Volume; }
		}

		#endregion

		#region IPlayable Members
		public void Pause()
		{
			this.channel.Paused = true;
			RefreshPlaybackState();
		}

		public void Play()
		{
			SoundSystemFactory.DefaultSoundSystem.PlaySound(ChannelIndex.Free, sound, false, ref channel);
			RefreshPlaybackState();
		}

		public PlaybackState PlaybackState
		{
			get { return playbackState; }
		}

		public void Resume()
		{
			this.channel.Paused = false;
			RefreshPlaybackState();
		}

		public void Stop()
		{
			this.channel.Stop();
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
			return new TimeSpan(0, 0, 0, 0, (int)channel.Position);
		}
		#endregion

		#region IPositionable Members
		public void SetAbsolutePosition(TimeSpan position)
		{
			this.channel.Position = (uint)position.Milliseconds;
			throw new Exception("The method or operation is not implemented.");
		}

		public void SetRelativePosition(TimeSpan position)
		{
			SetAbsolutePosition(GetElapsed().Add(position));
		
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion
	}
}
