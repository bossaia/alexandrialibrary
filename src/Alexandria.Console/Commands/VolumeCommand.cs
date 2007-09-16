using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Console.Commands
{
	public class VolumeCommand : Command
	{
		public VolumeCommand() : base("Volume")
		{
		}
		
		public override Context Execute(Context context, string option)
		{
			float volume = 0f;
			if (float.TryParse(option, out volume))
			{
				if (volume < 0) volume = 0;
				if (volume > 1) volume = 1;
			}
			else volume = 0.5f;

			ContextFactory.PlaybackContext.Player.SetVolume(volume);
			ContextFactory.PlaybackContext.WriteStatus("Stream Volume", volume.ToString());
			return context;
		}
	}
}
