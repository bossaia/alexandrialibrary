using System;
using System.Collections.Generic;

using Alexandria.Media;
using Alexandria.Media.IO;

namespace Alexandria.Console.Contexts
{
	public class PlaybackContext : Context
	{
		public PlaybackContext() : base("Playback")
		{
			player = new AudioPlayer();
			player.AudioStreamFactory = new Fmod.AudioStreamFactory();
			player.PlayToggles = true;
			player.MuteToggles = true;
		}

		private IAudioPlayer player;

		public IAudioPlayer Player
		{
			get { return player; }
		}

		public override void WriteStatus()
		{
			WriteStatus(string.Empty, string.Empty);
		}

		public void WriteStatus(string command, string option)
		{
			if (player != null && player.CurrentAudioStream != null)
			{
				if (string.IsNullOrEmpty(command))
					command = "Stream";

				if (string.IsNullOrEmpty(option))
					option = player.CurrentAudioStream.PlaybackState.ToString();

				System.Console.WriteLine(string.Format("{0} {1}", command, option));
			}
			else System.Console.WriteLine("NO STREAM LOADED");
		}
	}
}
