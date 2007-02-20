using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.Fmod
{
	public class SoundStreamCommand : FmodSoundCommand
	{
		#region Constructors
		[CLSCompliant(false)]
		public SoundStreamCommand(FmodAudioPlayer audioPlayer, Fmod.Sound sound, uint streamBufferSize) : base(audioPlayer, sound, SoundCommandType.Stream)
		{
			this.streamBufferSize = streamBufferSize;
		}
		#endregion
		
		#region Private Fields
		private uint streamBufferSize;
		#endregion
		
		#region Public Properties
		[CLSCompliant(false)]
		public uint StreamBufferSize
		{
			get {return streamBufferSize;}
		}
		#endregion
		
		#region Public Methods
		public override bool IsValid(SoundStatus status)
		{
			if (status != null)
				return status.AllowsStream;
			else
				return false;
		}

		public override void Execute(SoundStatus status)
		{
			if (IsValid(status))
			{
				Sound.Load(streamBufferSize);
				if (AudioPlayer.SoundSystem.CurrentResult == Fmod.Result.Ok)
				{
					switch(Sound.OpenState)
					{
						case OpenState.Error:
							Sound.Status.BufferState = BufferState.None;
							Sound.Status.PlaybackState = PlaybackState.None;
							Sound.Status.SeekState = SeekState.None;
							Result = SoundCommandResult.SoundStreamError;
							break;
						case OpenState.Buffering:
							Sound.Status.BufferState = BufferState.Buffering;
							break;
						case OpenState.Connecting:
							Sound.Status.BufferState = BufferState.Connecting;
							break;
						case OpenState.Loading:
							Sound.Status.BufferState = BufferState.Loading;
							break;
						case OpenState.Ready:
							Result = SoundCommandResult.SoundStreaming;
							Sound.Status = RemoteSoundReady.Example;
							break;
						default:
							Sound.Status.BufferState = BufferState.None;
							Sound.Status.PlaybackState = PlaybackState.None;
							Sound.Status.SeekState = SeekState.None;
							Result = SoundCommandResult.SoundStreamError;
							break;
					}
				}
				else Result = SoundCommandResult.SoundStreamError;
			}
			else Result = SoundCommandResult.InvalidCommand;
		}
		#endregion
	}
}
