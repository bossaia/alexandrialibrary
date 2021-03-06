using System;

namespace Alexandria.Media.IO
{
	public class MediaStateChangedEventArgs : MediaEventArgs
	{
		#region Constructors
		public MediaStateChangedEventArgs(BufferState bufferState, NetworkState networkState, PlaybackState playbackState, SeekState seekState)
		{
			this.bufferState = bufferState;
			this.networkState = networkState;
			this.playbackState = playbackState;
			this.seekState = seekState;
		}
		#endregion

		#region Private Fields
		private BufferState bufferState = BufferState.None;
		private NetworkState networkState = NetworkState.None;
		private PlaybackState playbackState = PlaybackState.None;
		private SeekState seekState = SeekState.None;
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
		
		public SeekState SeekState
		{
			get { return seekState; }
		}
		#endregion
	}
}