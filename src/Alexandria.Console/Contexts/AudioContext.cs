using System;
using System.Collections.Generic;

using Alexandria.Media;
using Alexandria.Media.IO;
using Alexandria.Console.Commands;

namespace Alexandria.Console.Contexts
{
	public class AudioContext : Context
	{
		public AudioContext() : base(ContextConstants.Audio)
		{
			player = new AudioPlayer();
			player.AudioStreamFactory = new Fmod.AudioStreamFactory();
			player.PlayToggles = true;
			player.MuteToggles = true;
			
			Prompt = "audio> ";
		}

		private IAudioPlayer player;

		private void Pause(string option)
		{
			player.Play();
			WriteStatus();
		}

		private void Play(string option)
		{
			if (!string.IsNullOrEmpty(option))
			{
				Uri path = new Uri(option);
				player.LoadAudioStream(path);
				player.Play();
			}
			else
			{
				player.Play();
			}

			WriteStatus();
		}
		
		private void Seek(string option)
		{
			int hours = 0, minutes = 0, seconds = 0;

			if (!string.IsNullOrEmpty(option))
			{
				string[] parts = option.Split(new char[] { ':' }, 3);
				if (parts.Length > 1)
				{
					if (parts.Length > 2)
					{
						hours = Convert.ToInt32(parts[0]);
						minutes = Convert.ToInt32(parts[1]);
						seconds = Convert.ToInt32(parts[2]);
					}
					else
					{
						minutes = Convert.ToInt32(parts[0]);
						seconds = Convert.ToInt32(parts[1]);
					}
				}
				else seconds = Convert.ToInt32(option);
			}

			TimeSpan position = new TimeSpan(hours, minutes, seconds);
			player.Seek((int)position.TotalMilliseconds);
			WriteStatus("Stream Position", position.ToString());
		}

		private void SetVolume(string option)
		{
			float volume = 0f;
			if (float.TryParse(option, out volume))
			{
				if (volume < 0) volume = 0;
				if (volume > 1) volume = 1;
			}
			else volume = 0.5f;

			player.SetVolume(volume);
			WriteStatus("Stream Volume", volume.ToString());
		}

		private void Stop(string option)
		{
			player.Stop();
			WriteStatus();
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

		public override void HandleCommand(Command command, string option)
		{
			switch(command.Name)
			{
				case CommandConstants.Pause:
					Pause(option);
					break;
				case CommandConstants.Play:
					Play(option);
					break;
				case CommandConstants.Seek:
					Seek(option);
					break;
				case CommandConstants.Status:
					if (IsActive) WriteStatus();
					break;
				case CommandConstants.Stop:
					Stop(option);
					break;
				case CommandConstants.Volume:
					SetVolume(option);
					break;
				default:
					break;
			}
		}
	}
}
