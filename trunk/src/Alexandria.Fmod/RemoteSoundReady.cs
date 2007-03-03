using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace AlexandriaOrg.Alexandria.Fmod
{
	public sealed class RemoteSoundReady : Alexandria.SoundStatus
	{
		#region Constructors
		public RemoteSoundReady() : base(false, false, true, false, false, false)
		{
			this.BufferState = BufferState.Ready;
			this.PlaybackState = PlaybackState.None;
			this.SeekState = SeekState.None;
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
