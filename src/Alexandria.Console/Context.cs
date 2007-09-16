using System;
using System.Collections.Generic;
using System.Text;

using Alexandria;
using Alexandria.Media;
using Alexandria.Media.IO;

namespace Alexandria.Console
{
	public class Context
	{
		public Context()
		{
			player = new AudioPlayer();
			player.AudioStreamFactory = new Alexandria.Fmod.AudioStreamFactory();
			player.PlayToggles = true;
			player.MuteToggles = true;
		}

		private bool isOpen = true;
		private string prompt = "alx> ";
		private IAudioPlayer player;
		
		public bool IsOpen
		{
			get { return isOpen; }
		}
		
		public string Prompt
		{
			get { return prompt; }
			set { prompt = value; }
		}
		
		public IAudioPlayer Player
		{
			get { return player; }
		}

		public void Close()
		{
			isOpen = false;
		}

		public void WriteCurrentStreamStatus()
		{
			WriteCurrentStreamStatus(string.Empty, string.Empty);
		}

		public void WriteCurrentStreamStatus(string command, string option)
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
