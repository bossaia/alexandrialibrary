using System;
using System.Collections.Generic;
using Alexandria.Media;
using Alexandria.Media.IO;

using Alexandria.Fmod;

namespace Alexandria.Client.Controllers
{
	public class PlaybackController
	{
		#region Constructors
		public PlaybackController()
		{
		}
		#endregion
		
		#region Private Fields
		private IAudioStream currentAudioStream;
		private EventHandler<PlaybackEventArgs> onBufferStateChanged;
		private EventHandler<PlaybackEventArgs> onNetworkStateChanged;
		private EventHandler<PlaybackEventArgs> onPlaybackStateChanged;
		private EventHandler<VolumeEventArgs> onVolumeChanged;
		private IAudioStreamFactory audioStreamFactory = new Fmod.AudioStreamFactory();
		#endregion
		
		#region Private Methods
		private void CurrentAudioStreamBufferStateChanged(object sender, MediaStateChangedEventArgs args)
		{
			if (currentAudioStream != null && OnBufferStateChanged != null)
				OnBufferStateChanged(currentAudioStream, new PlaybackEventArgs(args.BufferState, args.NetworkState, args.PlaybackState));
		}

		private void CurrentAudioStreamNetworkStateChanged(object sender, MediaStateChangedEventArgs args)
		{
			if (currentAudioStream != null && OnNetworkStateChanged != null)
				OnNetworkStateChanged(currentAudioStream, new PlaybackEventArgs(args.BufferState, args.NetworkState, args.PlaybackState));
		}

		private void CurrentAudioStreamPlaybackStateChanged(object sender, MediaStateChangedEventArgs args)
		{
			if (currentAudioStream != null && OnPlaybackStateChanged != null)
				OnPlaybackStateChanged(currentAudioStream, new PlaybackEventArgs(args.BufferState, args.NetworkState, args.PlaybackState));
		}
		
		private void CurrentAudioStreamVolumeChanged(object sender, AudioStateChangedEventArgs args)
		{
			if (currentAudioStream != null && OnVolumeChanged != null)
				OnVolumeChanged(currentAudioStream, new VolumeEventArgs(args.Volume, args.IsMuted));
		}
		#endregion
		
		#region Public Properties
		public EventHandler<PlaybackEventArgs> OnBufferStateChanged
		{
			get { return onBufferStateChanged; }
			set { onBufferStateChanged = value; }
		}
		
		public EventHandler<PlaybackEventArgs> OnNetworkStateChanged
		{
			get { return onNetworkStateChanged; }
			set { onNetworkStateChanged = value; }
		}
		
		public EventHandler<PlaybackEventArgs> OnPlaybackStateChanged
		{
			get { return onPlaybackStateChanged; }
			set { onPlaybackStateChanged = value; }
		}
		
		public EventHandler<VolumeEventArgs> OnVolumeChanged
		{
			get { return onVolumeChanged; }
			set { onVolumeChanged = value; }
		}
		#endregion
		
		#region Public Methods
		public void RefreshPlaybackStates()
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.RefreshBufferState();
				currentAudioStream.RefreshNetworkState();
				currentAudioStream.RefreshPlaybackState();
			}
		}
		
		public void LoadAudioFile(Uri path)
		{
			currentAudioStream = audioStreamFactory.CreateAudioStream(path);
		}
		#endregion
	}
}
