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
using System.Windows.Forms;

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
		private EventHandler<PlaybackEventArgs> onSeekStateChanged;
		private EventHandler<VolumeEventArgs> onVolumeChanged;
		private IAudioStreamFactory audioStreamFactory = new Fmod.AudioStreamFactory();
		
		private bool isSeekPending;
		private TrackBar playbackTrackBar;
		private Button playPauseButton;
		#endregion
		
		#region Private Methods
		private void CurrentAudioStreamBufferStateChanged(object sender, MediaStateChangedEventArgs args)
		{
			if (currentAudioStream != null && OnBufferStateChanged != null)
				OnBufferStateChanged(currentAudioStream, new PlaybackEventArgs(args.BufferState, args.NetworkState, args.PlaybackState, args.SeekState));
		}

		private void CurrentAudioStreamNetworkStateChanged(object sender, MediaStateChangedEventArgs args)
		{
			if (currentAudioStream != null && OnNetworkStateChanged != null)
				OnNetworkStateChanged(currentAudioStream, new PlaybackEventArgs(args.BufferState, args.NetworkState, args.PlaybackState, args.SeekState));
		}

		private void CurrentAudioStreamPlaybackStateChanged(object sender, MediaStateChangedEventArgs args)
		{
			if (currentAudioStream != null && OnPlaybackStateChanged != null)
				OnPlaybackStateChanged(currentAudioStream, new PlaybackEventArgs(args.BufferState, args.NetworkState, args.PlaybackState, args.SeekState));
		}
		
		private void CurrentAudioStreamSeekStateChanged(object sender, MediaStateChangedEventArgs args)
		{
			if (currentAudioStream != null && OnSeekStateChanged != null)
				OnSeekStateChanged(currentAudioStream, new PlaybackEventArgs(args.BufferState, args.NetworkState, args.PlaybackState, args.SeekState));
		}
		
		private void CurrentAudioStreamVolumeChanged(object sender, AudioStateChangedEventArgs args)
		{
			if (currentAudioStream != null && OnVolumeChanged != null)
				OnVolumeChanged(currentAudioStream, new VolumeEventArgs(args.Volume, args.IsMuted));
		}
		#endregion
		
		#region Public Properties
		public TrackBar PlaybackTrackBar
		{
			get { return playbackTrackBar; }
			set { playbackTrackBar = value; }
		}
		
		public Button PlayPauseButton
		{
			get { return playPauseButton; }
			set { playPauseButton = value; }
		}
		
		public bool IsSeekPending
		{
			get { return isSeekPending; }
			set { isSeekPending = value; }
		}
		
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
		
		public EventHandler<PlaybackEventArgs> OnSeekStateChanged
		{
			get { return onSeekStateChanged; }
			set { onSeekStateChanged = value; }
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
				currentAudioStream.RefreshSeekState();
				
				if (currentAudioStream.PlaybackState == PlaybackState.Playing && !IsSeekPending)
				{
					int value = (int)currentAudioStream.Elapsed.TotalMilliseconds;
					playbackTrackBar.Value = value;
				}
				
				if (currentAudioStream.PlaybackState == PlaybackState.Playing)
				{
					PlayPauseButton.BackgroundImage = Alexandria.Client.Properties.Resources.control_pause_blue;
				}
				else
				{
					PlayPauseButton.BackgroundImage = Alexandria.Client.Properties.Resources.control_play_blue;
				}	
			}
		}
		
		//public void LoadAudioFile(Uri path)
		//{
			//currentAudioStream = audioStreamFactory.CreateAudioStream(path);
		//}
		
		public void SetCurrentAudioStream(IAudioStream currentAudioStream)
		{
			this.currentAudioStream = currentAudioStream;
			if (currentAudioStream != null)
			{
				playbackTrackBar.Minimum = 0;
				playbackTrackBar.Maximum = Convert.ToInt32(currentAudioStream.Duration.TotalMilliseconds);
			}
		}
		
		public void Play()
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.Play();
				RefreshPlaybackStates();
			}
		}
		
		public void Pause()
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.Pause();
				RefreshPlaybackStates();
			}
		}
		
		public void TogglePlay()
		{
			if (currentAudioStream != null)
			{
				switch (currentAudioStream.PlaybackState)
				{
					case PlaybackState.Error:
						throw new AlexandriaException("Audio playback error");
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
				
				RefreshPlaybackStates();
			}
		}
		
		public void Stop()
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.Stop();
				RefreshPlaybackStates();
				if (currentAudioStream.PlaybackState == PlaybackState.Stopped)
				{
					playbackTrackBar.Value = 0;
				}
			}
		}
		
		public void Seek(int position)
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.Elapsed = new TimeSpan(0, 0, 0, 0, position);
				RefreshPlaybackStates();
			}
		}
		
		public void SetVolume(float volume)
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.Volume = volume;
				RefreshPlaybackStates();
			}
		}
		
		public void Mute()
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.IsMuted = true;
				RefreshPlaybackStates();
			}
		}
		
		public void Unmute()
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.IsMuted = false;
				RefreshPlaybackStates();
			}
		}
		
		public void ToggleMute()
		{
			if (currentAudioStream != null)
			{
				currentAudioStream.IsMuted = !currentAudioStream.IsMuted;
				RefreshPlaybackStates();
			}
		}
		
		public long GetPosition()
		{
			if (currentAudioStream != null)
				return currentAudioStream.Position;
			else return 0;
		}
		#endregion
	}
}
