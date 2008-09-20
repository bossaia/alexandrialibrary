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
using System.Text;
using WMPLib;

using Alexandria.Media.IO;

namespace Telesophy.Alexandria.IO.WindowsMedia
{
	public class AudioStream : IAudioStream
	{
		#region Constructors
		public AudioStream(string path)
		{
			player = new WindowsMediaPlayer();
			player.PlayStateChange += new _WMPOCXEvents_PlayStateChangeEventHandler(player_PlayStateChange);
			player.MediaError += new _WMPOCXEvents_MediaErrorEventHandler(player_MediaError);
			player.URL = path;
		}
		#endregion

		#region IDisposable Members
		~AudioStream()
		{
			Dispose(false);
		}
		
		protected void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					//release unmanaged resources
				}
				
				if (player != null)
				{
					player.close();
					player = null;
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
		
		private WindowsMediaPlayer player;
		
		private EventHandler<AudioStateChangedEventArgs> volumeChanged;
		private int streamIndex;
		private BufferState bufferState = BufferState.None;
		private NetworkState networkState = NetworkState.None;
		private PlaybackState playbackState = PlaybackState.None;
		private SeekState seekState = SeekState.None;
		#endregion
	
		#region Private Methods
		private void player_PlayStateChange(int NewState)
		{
			WMPPlayState state = (WMPPlayState)NewState;
			switch(state)
			{
				case WMPPlayState.wmppsPlaying:
					break;
				case WMPPlayState.wmppsPaused:
					break;
				case WMPPlayState.wmppsStopped:
					break;
				default:
					break;
			}
		}

		private void player_MediaError(object pMediaObject)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion
	
		#region IAudioStream Members
		private float GetVolume(int value)
		{
			if (value >= 0 && value <= 100)
			{
				return Convert.ToSingle(value / 100);
			}
			else return 0.5f;
		}
		
		private int GetVolume(float value)
		{
			if (value >= 0f && value <= 1.0f)
			{
				return Convert.ToInt32(Math.Truncate(value * 100));
			}
			else return 50;
		}
		
		public float Volume
		{
			get { return GetVolume(player.settings.volume); }
			set { player.settings.volume = GetVolume(value); }
		}

		public bool IsMuted
		{
			get { return player.settings.mute; }
			set { player.settings.mute = value; }
		}

		public EventHandler<AudioStateChangedEventArgs> VolumeChanged
		{
			get { return volumeChanged; }
			set { volumeChanged = value; }
		}
		#endregion

		#region IMediaStream Members
		public bool CanRead
		{
			get { return false; }
		}

		public bool CanSeek
		{
			get { return true; }
		}

		public bool CanWrite
		{
			get { return false; }
		}

		public bool CanTimeout
		{
			get { return false; }
		}

		public void Flush()
		{
		}

		public long Length
		{
			get { return (long)player.controls.currentItem.duration; }
		}

		public long Position
		{
			get { return (long)player.controls.currentPosition; }
			set { player.controls.currentPosition = Convert.ToDouble(value); }
		}

		public int StreamIndex
		{
			get { return streamIndex; }
			set { streamIndex = value; }
		}

		public int Read(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException("Read() is not supported.");
		}

		public long Seek(long offset, System.IO.SeekOrigin origin)
		{
			//TODO: implement seek
			return 0L;
		}

		public void SetLength(long value)
		{
			throw new InvalidOperationException("SetLength() is not supported.");
		}

		public void Write(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException("Write() is not supported.");
		}

		public string Path
		{
			get { return player.URL; }
		}

		public bool CanSetPosition
		{
			get { return true; }
		}

		public bool CanSetElapsed
		{
			get { return false; }
		}

		public bool CanPlay
		{
			get { return true; }
		}

		public BufferState BufferState
		{
			get { return bufferState; }
		}

		public PlaybackState PlaybackState
		{
			get { return playbackState; }
		}

		public NetworkState NetworkState
		{
			get { return networkState; }
		}

		public SeekState SeekState
		{
			get { return seekState; }
		}

		public TimeSpan Duration
		{
			get { return new TimeSpan(0, 0, 0, 0, Convert.ToInt32(player.controls.currentItem.duration)); }
		}

		public TimeSpan Elapsed
		{
			get { return new TimeSpan(0, 0, 0, 0, Convert.ToInt32(player.controls.currentPosition)); }
			set { player.controls.currentPosition = value.TotalMilliseconds; }
		}

		public float PercentBuffered
		{
			get { return -1f; }
		}

		public void Play()
		{
			player.controls.play();
		}

		public void Pause()
		{
			player.controls.pause();
		}

		public void Resume()
		{
			player.controls.pause();
		}

		public void Stop()
		{
			player.controls.stop();
		}

		public void RefreshBufferState()
		{
		}

		public void RefreshNetworkState()
		{
		}

		public void RefreshPlaybackState()
		{
		}

		public void RefreshSeekState()
		{
		}

		public EventHandler<MediaStateChangedEventArgs> BufferStateChanged
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

		public EventHandler<MediaStateChangedEventArgs> NetworkStateChanged
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

		public EventHandler<MediaStateChangedEventArgs> PlaybackStateChanged
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

		public EventHandler<MediaStateChangedEventArgs> SeekStateChanged
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
	}
}
