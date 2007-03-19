using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace AlexandriaOrg.Alexandria.Fmod
{
	public sealed class RemoteSoundReady : SoundStatus
	{
		#region Constructors
		public RemoteSoundReady() : base(false, false, true, false, false, false)
		{
			this.BufferState = MediaBufferState.Ready;
			this.PlaybackState = MediaPlaybackState.None;
			this.IsSeeking = false;
		}
		#endregion
		
		#region Static Members
		private static RemoteSoundReady example;
		
		public static RemoteSoundReady Example
		{
			get
			{
				if (example == null) example = new RemoteSoundReady();
				
				return example;
			}
		}
		#endregion
	}
}
