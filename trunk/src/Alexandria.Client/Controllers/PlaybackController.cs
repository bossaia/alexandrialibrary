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

using Alexandria.LastFM;
using Alexandria.Media;
using Alexandria.Media.IO;
using Alexandria.Metadata;
using Alexandria.Fmod;

namespace Alexandria.Client.Controllers
{
	public class PlaybackController : IDisposable
	{
		#region Constructors
		public PlaybackController()
		{
			audioPlayer = new AudioPlayer();
			audioPlayer.CurrentAudioStreamChanged += new EventHandler<EventArgs>(OnCurrentAudioStreamChanged);
			audioPlayer.BufferStateChanged += new EventHandler<MediaStateChangedEventArgs>(OnBufferStateChanged);
			audioPlayer.NetworkStateChanged += new EventHandler<MediaStateChangedEventArgs>(OnNetworkStateChanged);
			audioPlayer.PlaybackStateChanged += new EventHandler<MediaStateChangedEventArgs>(OnPlaybackStateChanged);
			audioPlayer.SeekStateChanged += new EventHandler<MediaStateChangedEventArgs>(OnSeekStateChanged);
		}
		#endregion

		#region IDisposable Members
		~PlaybackController()
		{
			Dispose(false);
		}

		protected void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					if (audioPlayer != null)
					{
						audioPlayer.Dispose();
						audioPlayer = null;
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
		private const int SUBMIT_ELAPSED_TIME = 30000;
		
		private bool disposed;
		
		private QueueController queueController;
		private TrackBar playbackTrackBar;
		private Button playPauseButton;
		private IAudioPlayer audioPlayer;
		private bool currentTrackSubmitted;
		private EventHandler<PlaybackEventArgs> statusUpdated;
		private bool enableSubmitTracksToLastFM;

		private IAudioPlayer AudioPlayer
		{
			get { return audioPlayer; }
		}
		#endregion

		#region Private Methods
		private void CheckForChangedStatus()
		{
			if (statusUpdated != null)
				statusUpdated(this, GetCurrentStatus());
		}
		#endregion

		#region Private Event Methods
		private void OnCurrentAudioStreamChanged(object sender, EventArgs e)
		{
			if (audioPlayer != null && audioPlayer.CurrentAudioStream != null)
			{
				playbackTrackBar.Minimum = 0;
				playbackTrackBar.Maximum = Convert.ToInt32(audioPlayer.CurrentAudioStream.Duration.TotalMilliseconds);
			}
		}

		private void OnBufferStateChanged(object sender, MediaStateChangedEventArgs args)
		{
			if (statusUpdated != null)
				statusUpdated(this, GetCurrentStatus());
		}
		
		private void OnNetworkStateChanged(object sender, MediaStateChangedEventArgs args)
		{
			if (statusUpdated != null)
				statusUpdated(this, GetCurrentStatus());
		}

		private void OnPlaybackStateChanged(object sender, MediaStateChangedEventArgs args)
		{
			if (args.PlaybackState == PlaybackState.Stopped)
			{
				playbackTrackBar.Value = 0;
			}

			if (statusUpdated != null)
				statusUpdated(this, GetCurrentStatus());
		}

		private void OnSeekStateChanged(object sender, MediaStateChangedEventArgs args)
		{
			if (statusUpdated != null)
				statusUpdated(this, GetCurrentStatus());
		}
		#endregion
		
		#region Public Properties
		public QueueController QueueController
		{
			get { return queueController; }
			set { queueController = value; }
		}
		
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
				
		public bool EnableSubmitTracksToLastFM
		{
			get { return enableSubmitTracksToLastFM; }
			set { enableSubmitTracksToLastFM = value; }
		}
		
		public bool MuteToggles
		{
			get { return (audioPlayer != null) ? audioPlayer.MuteToggles : false; }
			set {
				if (audioPlayer != null)
					audioPlayer.MuteToggles = value;
			}
		}
		
		public bool PlayToggles
		{
			get { return (audioPlayer != null) ? audioPlayer.PlayToggles : false; }
			set {
				if (audioPlayer != null)
					audioPlayer.PlayToggles = value;
			}
		}
		#endregion
				
		#region Public Methods
		//TODO: move this method into its own controller
		public void SubmitTrackToLastFM(IAudioTrack track)
		{
			try
			{
				LastFM.AudioscrobblerRequest request = new Alexandria.LastFM.AudioscrobblerRequest();
				request.Username = "uberweasel";
				request.Password = "automatic";
				request.SubmitTrack(track);

				//if (StatusUpdated != null)
					//StatusUpdated(this, new UpdateStatusEventArgs("Track submitted to Last.fm", string.Format("{0} - {1} - {2}", track.Artist, track.Album, track.Name)));
			}
			catch (Exception)
			{
				//if (StatusUpdated != null)
					//StatusUpdated(this, new UpdateStatusEventArgs("Error submitting track to Last.fm", ex.Message));
			}
		}
		
		public void LoadAudioStream(Uri path)
		{
			if (audioPlayer != null)
			{
				currentTrackSubmitted = false;
				audioPlayer.LoadAudioStream(path);
			}
		}
		
		public void LoadAudioStream(IAudioStream audioStream)
		{
			if (audioPlayer != null)
			{
				currentTrackSubmitted = false;
				audioPlayer.LoadAudioStream(audioStream);
			}
		}
		
		public void Play()
		{
			if (audioPlayer != null && audioPlayer.CurrentAudioStream != null)
				audioPlayer.Play();
		}
		
		public bool IsPlaying()
		{
			if (audioPlayer != null && audioPlayer.CurrentAudioStream != null)
				return (audioPlayer.CurrentAudioStream.PlaybackState == PlaybackState.Playing);
			else return false;
		}
		
		public void Pause()
		{
			if (audioPlayer != null && audioPlayer.CurrentAudioStream != null)
				audioPlayer.Pause();
		}
		
		public void Stop()
		{
			if (audioPlayer != null && audioPlayer.CurrentAudioStream != null)
				audioPlayer.Stop();
		}
		
		public void BeginSeek()
		{
			if (audioPlayer != null && audioPlayer.CurrentAudioStream != null)
				audioPlayer.BeginSeek();
		}
		
		public void Seek(int position)
		{
			if (audioPlayer != null && audioPlayer.CurrentAudioStream != null)
				audioPlayer.Seek(position);
		}
		
		public void Mute()
		{
			if (audioPlayer != null && audioPlayer.CurrentAudioStream != null)
				audioPlayer.Mute();
		}
		
		public void SetVolume(float volume)
		{
			if (audioPlayer != null && audioPlayer.CurrentAudioStream != null)
				audioPlayer.SetVolume(volume);
		}
		
		public void RefreshPlaybackStates()
		{
			if (audioPlayer != null && audioPlayer.CurrentAudioStream != null)
			{
				audioPlayer.RefreshPlayerStates();
				if (audioPlayer.CurrentAudioStream.PlaybackState == PlaybackState.Playing && !audioPlayer.SeekIsPending)
				{
					int value = (int)audioPlayer.CurrentAudioStream.Elapsed.TotalMilliseconds;
					playbackTrackBar.Value = value;
					
					if (EnableSubmitTracksToLastFM && !currentTrackSubmitted)
					{
						if (value >= SUBMIT_ELAPSED_TIME || audioPlayer.CurrentAudioStream.Duration.TotalMilliseconds < SUBMIT_ELAPSED_TIME)
						{
							SubmitTrackToLastFM(QueueController.SelectedTrack);
							currentTrackSubmitted = true;
						}
					}
				}

				if (audioPlayer.CurrentAudioStream.PlaybackState == PlaybackState.Playing)
				{
					PlayPauseButton.BackgroundImage = Alexandria.Client.Properties.Resources.control_pause_blue;
				}
				else
				{
					PlayPauseButton.BackgroundImage = Alexandria.Client.Properties.Resources.control_play_blue;
				}
			}
		}

		public PlaybackEventArgs GetCurrentStatus()
		{
			if (audioPlayer != null && audioPlayer.CurrentAudioStream != null)
				return new PlaybackEventArgs(audioPlayer.CurrentAudioStream.BufferState, audioPlayer.CurrentAudioStream.NetworkState, audioPlayer.CurrentAudioStream.PlaybackState, audioPlayer.CurrentAudioStream.SeekState);
			else return new PlaybackEventArgs(BufferState.None, NetworkState.None, PlaybackState.None, SeekState.None);
		}
		
		public void WireCurrentAudioSteamEnded(EventHandler<EventArgs> handler)
		{
			if (audioPlayer != null && handler != null)
				audioPlayer.CurrentAudioStreamEnded += handler;
		}
		
		public void WireStatusUpdated(EventHandler<PlaybackEventArgs> handler)
		{
			if (handler != null)
				statusUpdated += handler;
		}		
		#endregion
	}
}
