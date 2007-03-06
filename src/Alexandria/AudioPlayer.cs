using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class AudioPlayer : MediaPlayer,IAudioPlayer,IDisposable
	{
		#region Private Fields
		private PlaybackStatus status;
		private uint position;
		private IMediaFile currentMediaFile;
		private bool isMuted;
		private float volume;
		private double soundLoadTimeout = 3000;
		private event EventHandler<PlaybackEventArgs> onPlay;
		private event EventHandler<PlaybackEventArgs> onPause;
		private event EventHandler<PlaybackEventArgs> onStop;
		private event EventHandler<System.Timers.ElapsedEventArgs> onPlaybackTimerTick;
		private event EventHandler<PlaybackEventArgs> onSoundEnd;
		private event EventHandler<System.EventArgs> onSoundLoadTimout;
		private event EventHandler<PlaybackEventArgs> onPlaybackStatusChange;
		private event EventHandler<PlaybackEventArgs> onStreamingStatusChange;
		private event EventHandler<PlaybackEventArgs> onRippingStatusChange;
		private bool disposed;
		#endregion
		
		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public AudioPlayer()
		{
			status = PlaybackStatus.Stopped;
		}
		#endregion
		
		#region Finalizer
		~AudioPlayer()
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
		
		#region Public Properties
		public IMediaFile CurrentMediaFile
		{
			get {return currentMediaFile;}
		}
		
		/// <summary>
		/// Get the current audio resource loaded into this player
		/// </summary>
		[System.CLSCompliant(false)]
		public virtual IAudioResource CurrentAudio
		{
			get {return null;}
		}

		public virtual bool IsMuted
		{
			get {return isMuted;}
			set {isMuted = value;}
		}

		public string CurrentStatus
		{
			get {return status.Name;}
		}

		/// <summary>
		/// Get the current playback status
		/// </summary>
		public PlaybackStatus Status
		{
			get {return status;}
		}

		/// <summary>
		/// Get or set the current position in the active sound in milliseconds
		/// </summary>
		[System.CLSCompliant(false)]
		public virtual uint Position
		{
			get {return position;}
		}

		/// <summary>
		/// Get or set the current volume in the active sound
		/// </summary>
		public virtual float Volume
		{
			get {return volume;}
			set {volume = value;}
		}
		
		public virtual string CurrentResult
		{
			get {return null;}
		}
		
		public virtual double PlaybackInterval
		{
			get {return 0;}
			set {}
		}
		
		/// <summary>
		/// Get or set the number of milliseconds that the system will attempt to load a sound before timing out
		/// </summary>
		public double SoundLoadTimeout
		{
			get {return soundLoadTimeout;}
			set {soundLoadTimeout = value;}
		}
		
		public virtual System.ComponentModel.ISynchronizeInvoke TimerSynch
		{
			get {return null;}
			set {}
		}

		#region Delegates
		/*
		public virtual GetAudioInfo GetAudioInfoHandler
		{
			get {return null;}
		}
		*/
		#endregion

		#region Event Handlers
		public EventHandler<PlaybackEventArgs> OnPlay
		{
			get { return onPlay; }
			set { onPlay = value; }
		}

		public EventHandler<PlaybackEventArgs> OnPause
		{
			get { return onPause; }
			set { onPause = value; }
		}

		public EventHandler<PlaybackEventArgs> OnStop
		{
			get { return onStop; }
			set { onStop = value; }
		}
		
		public EventHandler<System.Timers.ElapsedEventArgs> OnPlaybackTimerTick
		{
			get {return onPlaybackTimerTick;}
			set {onPlaybackTimerTick = value;}
		}
		
		public EventHandler<PlaybackEventArgs> OnSoundEnd
		{
			get {return onSoundEnd;}
			set {onSoundEnd = value;}
		}
		
		public EventHandler<System.EventArgs> OnSoundLoadTimeout
		{
			get {return onSoundLoadTimout;}
			set {onSoundLoadTimout = value;}
		}
		
		public EventHandler<PlaybackEventArgs> OnPlaybackStatusChange
		{
			get {return onPlaybackStatusChange;}
			set {onPlaybackStatusChange = value;}
		}
		
		public EventHandler<PlaybackEventArgs> OnStreamingStatusChange
		{
			get {return onStreamingStatusChange;}
			set {onStreamingStatusChange = value;}
		}
		
		public EventHandler<PlaybackEventArgs> OnRippingStatusChange
		{
			get {return onRippingStatusChange;}
			set {onRippingStatusChange = value;}
		}
		#endregion

		#endregion		
		
		#region Protected Internal Methods
		protected internal void SetStatus(PlaybackStatus status)
		{
			this.status = status;
		}

		protected internal virtual void PlayCurrentSound()
		{
		}
		
		protected internal virtual void PauseCurrentSound()
		{
		}
		
		protected internal virtual void StopCurrentSound()
		{
		}
		#endregion
		
		#region Public Methods
		/// <summary>
		/// Set the current media file
		/// </summary>
		/// <param name="mediaFile">The current media file to use for this player</param>
		public virtual void SetCurrentMediaFile(MediaFile mediaFile)
		{
			currentMediaFile = mediaFile;						
		}

		public virtual void Play()
		{
			Status.Play(this);
		}

		public virtual void Pause()
		{
			Status.Pause(this);
		}

		public virtual void Stop()
		{			
			Status.Stop(this);
		}

		[System.CLSCompliant(false)]
		public virtual void SetPosition(uint position)
		{
			this.position = position;
		}

		[System.CLSCompliant(false)]
		public virtual void Seek(bool isForward, uint distance)
		{

		}

		public virtual void SaveStreamToLocalFile(string sourceFilePath, string destinationFilePath)
		{
		}
		
		internal virtual void LoadSound(IAudioResource audio, MediaFile file)
		{
		}
		#endregion
	}
}
