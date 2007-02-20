using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria
{
	public class PlaybackEventArgs : EventArgs
	{
		#region Private Fields
		private bool isValid = true;
		private string playbackStatus;
		private string streamingStatus;
		private string rippingStatus;
		#endregion
		
		#region Constructors
		public PlaybackEventArgs()
		{
		}
		
		public PlaybackEventArgs(bool isValid)
		{
			this.isValid = isValid;
		}
		
		public PlaybackEventArgs(bool isValid, string playbackStatus, string streamingStatus, string rippingStatus)
		{
			this.isValid = isValid;
			this.playbackStatus = playbackStatus;
			this.streamingStatus = streamingStatus;
			this.rippingStatus = rippingStatus;
		}
		#endregion
						
		#region Public Properties
		public bool IsValid
		{
			get {return isValid;}
			set {isValid = value;}
		}

		public string PlaybackStatus
		{
			get {return playbackStatus;}
			set {playbackStatus = value;}
		}
		
		public string StreamingStatus
		{
			get {return streamingStatus;}
			set {streamingStatus = value;}
		}
		
		public string RippingStatus
		{
			get {return rippingStatus;}
			set {rippingStatus = value;}
		}
		#endregion
	}
}
