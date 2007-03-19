using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace AlexandriaOrg.Alexandria.Fmod
{
	public abstract class SoundStatus : IAudioStatus
	{
		#region Constructors
		protected SoundStatus(bool allowsLoad, bool allowsStream, bool allowsPlay, bool allowsPause, bool allowsStop, bool allowsSeek)
		{
			this.allowsLoad = allowsLoad;
			this.allowsStream = allowsStream;
			this.allowsPlay = allowsPlay;
			this.allowsPause = allowsPause;
			this.allowsStop = allowsStop;
			this.allowsSeek = allowsSeek;
		}
		#endregion

		#region Private Fields
		private MediaBufferState bufferState = MediaBufferState.None;
		private MediaStreamingState streamingState = MediaStreamingState.None;
		private MediaPlaybackState playbackState = MediaPlaybackState.None;
		private MediaSeekingState seekingState = MediaSeekingState.None;
		private bool isSeeking = false;
		private float bufferLevel;
		private bool allowsLoad;
		private bool allowsStream;
		private bool allowsPlay;
		private bool allowsPause;
		private bool allowsStop;
		private bool allowsSeek;
		#endregion

		#region Public Properties
		public MediaBufferState BufferState
		{
			get { return bufferState; }
			set { bufferState = value; }
		}

		public MediaStreamingState StreamingState
		{
			get { return streamingState; }
			set { streamingState = value; }
		}

		public MediaPlaybackState PlaybackState
		{
			get { return playbackState; }
			set { playbackState = value; }
		}

		public MediaSeekingState SeekingState
		{
			get { return seekingState; }
			set { seekingState = value; }
		}

		public bool IsSeeking
		{
			get { return isSeeking; }
			set { isSeeking = value; }
		}

		public float BufferLevel
		{
			get { return bufferLevel; }
			set { bufferLevel = value; }
		}

		public bool AllowsLoad
		{
			get { return allowsLoad; }
			protected set { allowsLoad = value; }
		}

		public bool AllowsStream
		{
			get { return allowsStream; }
			protected set { allowsStream = value; }
		}

		public bool AllowsPlay
		{
			get { return allowsPlay; }
			protected set { allowsPlay = value; }
		}

		public bool AllowsPause
		{
			get { return allowsPause; }
			protected set { allowsPause = value; }
		}

		public bool AllowsStop
		{
			get { return allowsStop; }
			protected set { allowsStop = value; }
		}

		public bool AllowsSeek
		{
			get { return allowsSeek; }
			protected set { allowsSeek = value; }
		}
		#endregion

		#region Public Methods
		public virtual void Update(IAudioCommand command)
		{
			if (command != null)
				command.Execute(this);
		}
		#endregion
	}
}
