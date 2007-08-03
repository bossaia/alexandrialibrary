using System;

namespace Alexandria.Media.IO
{
	public class MediaStateChangedEventArgs : MediaEventArgs
	{
		#region Constructors
		public MediaStateChangedEventArgs(BufferState bufferState, NetworkState networkState, PlaybackState playbackState)
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