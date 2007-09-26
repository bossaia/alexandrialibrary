#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

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

		private void HandlePause(string option)
		{
			player.Play();
		}

		private void HandlePlay(string option)
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
		}
		
		private void HandleSeek(string option)
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
			Result = "Stream Position: " + position.ToString();
		}

		private void HandleStatus(string option)
		{
			if (player.CurrentAudioStream != null)
			{
				if (player.CurrentAudioStream.PlaybackState == PlaybackState.Playing || player.CurrentAudioStream.PlaybackState == PlaybackState.Paused)
				{
					System.Console.WriteLine(string.Format("Stream {0} is {1} at {2}", player.CurrentAudioStream.Path, player.CurrentAudioStream.PlaybackState.ToString(), player.CurrentAudioStream.Elapsed));
				}
				else
				{
					System.Console.WriteLine(string.Format("Stream {0} is {1}", player.CurrentAudioStream.Path, player.CurrentAudioStream.PlaybackState.ToString()));
				}
			}
			else System.Console.WriteLine("No audio stream loaded");
		}

		private void HandleVolume(string option)
		{
			float volume = 0f;
			if (float.TryParse(option, out volume))
			{
				if (volume < 0) volume = 0;
				if (volume > 1) volume = 1;
			}
			else volume = 0.5f;

			player.SetVolume(volume);
			Result = "Stream Volume: " + volume.ToString();
		}

		private void HandleStop(string option)
		{
			player.Stop();
		}

		public override void HandleCommand(Command command, string option)
		{
			switch(command.Name)
			{
				case CommandConstants.Pause:
					HandlePause(option);
					WriteResult();
					break;
				case CommandConstants.Play:
					HandlePlay(option);
					WriteResult();
					break;
				case CommandConstants.Seek:
					HandleSeek(option);
					WriteResult();
					break;
				case CommandConstants.Status:
					HandleStatus(option);
					break;
				case CommandConstants.Stop:
					HandleStop(option);
					WriteResult();
					break;
				case CommandConstants.Volume:
					HandleVolume(option);
					WriteResult();
					break;
				default:
					break;
			}
		}
	}
}
