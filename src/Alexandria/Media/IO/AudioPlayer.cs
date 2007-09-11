using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Media.IO
{
	public class AudioPlayer : IAudioPlayer
	{
		#region Constructors
		public AudioPlayer()
		{
		}
		#endregion

		#region IDisposable Members
		~AudioPlayer()
		{
			Dispose(false);
		}
		
		protected void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					if (currentAudioStream != null)
					{
						currentAudioStream.Dispose();
						currentAudioStream = null;
					}
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
		private bool disposed;
		private IAudioStreamFactory audioStreamFactory;
		private IAudioStream currentAudioStream;
		private EventHandler<EventArgs> currentAudioStreamChanged;
		private bool muteToggles;
		private bool playToggles;
		private bool seekIsPending;
		#endregion
		
		#region Private Event Methods
		private void OnBufferStateChanged(object sender, MediaStateChangedEventArgs e)
		{
			if (BufferStateChanged != null)
				BufferStateChanged(this, e);
		}
		
		private void OnNetworkStateChanged(object sender, MediaStateChangedEventArgs e)
		{
			if (NetworkStateChanged != null)
				NetworkStateChanged(this, e);
		}
		
		private void OnPlaybackStateChanged(object sender, MediaStateChangedEventArgs e)
		{
			if (PlaybackStateChanged != null)
				PlaybackStateChanged(this, e);
		}
		
		private void OnSeekStateChanged(object sender, MediaStateChangedEventArgs e)
		{
			if (SeekStateChanged != null)
				SeekStateChanged(this, e);
		}
		
		private void OnVolumeChanged(object sender, AudioStateChangedEventArgs e)
		{
			if (VolumeChanged != null)
				VolumeChanged(this, e);
		}
		#endregion
		
		#region IAudioPlayer Members
		public IAudioStreamFactory AudioStreamFactory
		{
			get { return audioStreamFactory; }
			set { audioStreamFactory = value; }
		}

		public IAudioStream CurrentAudioStream
		{
			get { return currentAudioStream; }
		}

		public TimeSpan Duration
		{
			get { return (currentAudioStream != null) ? currentAudioStream.Duration : TimeSpan.Zero; }
		}
		
		public TimeSpan Elapsed
		{
			get { return (currentAudioStream != null) ? currentAudioStream.Elapsed : TimeSpan.Zero; }
		}

		public bool IsMuted
		{
			get { return (currentAudioStream != null) ? currentAudioStream.IsMuted : false; }
		}

		public bool MuteToggles
		{
			get { return muteToggles; }
			set { muteToggles = value; }
		}

		public bool PlayToggles
		{
			get { return playToggles; }
			set { playToggles = value; }
		}

		public bool SeekIsPending
		{
			get { return seekIsPending; }
		}

		public float Volume
		{
			get { return (currentAudioStream != null) ? currentAudioStream.Volume : 0f; }
		}

		public EventHandler<MediaStateChangedEventArgs> BufferStateChanged
		{
			get { return currentAudioStream != null ? currentAudioStream.BufferStateChanged : null; }
			set {
				if (currentAudioStream != null)
					currentAudioStream.BufferStateChanged = value;
			}
		}

		public EventHandler<EventArgs> CurrentAudioStreamChanged
		{
			get { return currentAudioStreamChanged; }
			set { currentAudioStreamChanged = value; }
		}

		public EventHandler<MediaStateChangedEventArgs> NetworkStateChanged
		{
			get { return currentAudioStream != null ? currentAudioStream.NetworkStateChanged : null; }
			set
			{
				if (currentAudioStream != null)
					currentAudioStream.NetworkStateChanged = value;
			}
		}

		public EventHandler<MediaStateChangedEventArgs> PlaybackStateChanged
		{
			get { return currentAudioStream != null ? currentAudioStream.PlaybackStateChanged : null; }
			set
			{
				if (currentAudioStream != null)
					currentAudioStream.PlaybackStateChanged = value;
			}
		}

		public EventHandler<MediaStateChangedEventArgs> SeekStateChanged
		{
			get { return currentAudioStream != null ? currentAudioStream.SeekStateChanged : null; }
			set
			{
				if (currentAudioStream != null)
					currentAudioStream.SeekStateChanged = value;
			}
		}

		public EventHandler<AudioStateChangedEventArgs> VolumeChanged
		{
			get { return currentAudioStream != null ? currentAudioStream.VolumeChanged : null; }
			set
			{
				if (currentAudioStream != null)
					currentAudioStream.VolumeChanged = value;
			}
		}

		public void BeginSeek()
		{
			seekIsPending = true;
		}
		
		public void CancelSeek()
		{
			seekIsPending = false;
		}

		public void LoadAudioStream(IAudioStream audioStream)
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.Stop();
				currentAudioStream.Dispose();
			}
		
			currentAudioStream = audioStream;
		
			if (CurrentAudioStreamChanged != null)
				CurrentAudioStreamChanged(this, EventArgs.Empty);

			if (currentAudioStream != null)
			{
				currentAudioStream.BufferStateChanged += new EventHandler<MediaStateChangedEventArgs>(OnBufferStateChanged);
				currentAudioStream.NetworkStateChanged += new EventHandler<MediaStateChangedEventArgs>(OnNetworkStateChanged);
				currentAudioStream.PlaybackStateChanged += new EventHandler<MediaStateChangedEventArgs>(OnPlaybackStateChanged);
				currentAudioStream.SeekStateChanged += new EventHandler<MediaStateChangedEventArgs>(OnSeekStateChanged);
				currentAudioStream.VolumeChanged += new EventHandler<AudioStateChangedEventArgs>(OnVolumeChanged);
				RefreshPlayerStates();
			}
		}

		public void LoadAudioStream(Uri path)
		{
			if (path != null && audioStreamFactory != null)
			{
				IAudioStream audioStream = audioStreamFactory.CreateAudioStream(path);
				LoadAudioStream(audioStream);
			}
		}

		public void Mute()
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.IsMuted = (MuteToggles) ? !currentAudioStream.IsMuted : true;
				RefreshPlayerStates();
			}
		}

		public void Pause()
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.Pause();
				RefreshPlayerStates();
			}
		}

		public void Play()
		{
			if (currentAudioStream != null)
			{
				if (PlayToggles)
				{
					switch (currentAudioStream.PlaybackState)
					{
						case PlaybackState.Error:
							break;
						case PlaybackState.None:
						case PlaybackState.Stopped:
							currentAudioStream.Play();
							break;
						case PlaybackState.Paused:
							currentAudioStream.Resume();
							break;
						case PlaybackState.Playing:
							currentAudioStream.Pause();
							break;
						default:
							break;
					}
				}
				else
				{
					currentAudioStream.Play();
				}
				
				RefreshPlayerStates();
			}
		}

		public void RefreshPlayerStates()
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.RefreshBufferState();
				currentAudioStream.RefreshNetworkState();
				currentAudioStream.RefreshPlaybackState();
				currentAudioStream.RefreshSeekState();
			}
		}

		public void Resume()
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.Resume();
				RefreshPlayerStates();
			}
		}

		public void Seek(int position)
		{
			seekIsPending = false;
			
			if (currentAudioStream != null)
			{
				currentAudioStream.Elapsed = new TimeSpan(0, 0, 0, 0, position);
				RefreshPlayerStates();
			}
		}

		public void SetVolume(float volume)
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.Volume = volume;
				RefreshPlayerStates();
			}
		}

		public void Stop()
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.Stop();
				RefreshPlayerStates();
			}
		}

		public void Unmute()
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.IsMuted = false;
				RefreshPlayerStates();
			}
		}
		#endregion
	}
}
