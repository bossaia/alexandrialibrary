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
	public class PlaybackController : IDisposable
	{
		#region Constructors
		public PlaybackController()
		{
			audioPlayer = new AudioPlayer();
			audioPlayer.CurrentAudioStreamChanged += new EventHandler<EventArgs>(OnCurrentAudioStreamChanged);
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
		private bool disposed;
		
		private TrackBar playbackTrackBar;
		private Button playPauseButton;
		private IAudioPlayer audioPlayer;
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
		
		public IAudioPlayer AudioPlayer
		{
			get { return audioPlayer; }
		}
		#endregion
				
		#region Public Methods
		public void LoadAudioStream(Uri path)
		{
			if (audioPlayer != null)
				audioPlayer.LoadAudioStream(path);
		}
		
		public void LoadAudioStream(IAudioStream audioStream)
		{
			if (audioPlayer != null)
				audioPlayer.LoadAudioStream(audioStream);
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
		#endregion
	}
}
