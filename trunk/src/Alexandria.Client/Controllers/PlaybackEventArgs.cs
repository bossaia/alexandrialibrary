using System;
using System.Collections.Generic;
using Alexandria.Media;
using Alexandria.Media.IO;

namespace Alexandria.Client.Controllers
{
	public class PlaybackEventArgs : EventArgs
	{
		#region Constructors
		public PlaybackEventArgs(BufferState bufferState, NetworkState networkState, PlaybackState playbackState)
		{
			this.bufferState = bufferState;
			this.networkState = networkState;
			this.playbackState = playbackState;
		}
		#endregion
		
		#region Private Fields
		private BufferState bufferState = BufferState.None;
		private NetworkState networkState = NetworkState.None;
		private PlaybackState playbackState = PlaybackState.None;
		#endregion
		
		#region Public Properties
		public BufferState BufferState
		{
			get { return bufferState; }
		}
		
		public NetworkState NetworkState
		{
			get { return networkState; }
		}
		
		public PlaybackState PlaybackState
		{
			get { return playbackState; }
		}
		#endregion
	}
}
