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

using Alexandria;
using Alexandria.Media;
using Alexandria.Media.IO;

namespace Alexandria.Console
{
	class Program
	{
		private static IAudioPlayer player;
	
		static void Main(string[] args)
		{
			player = new AudioPlayer();
			player.AudioStreamFactory = new Alexandria.Fmod.AudioStreamFactory();
			player.PlayToggles = true;
			player.MuteToggles = true;
		
			const string prompt = "[alx] ";
			string cmd = string.Empty;
			
			while(cmd != "close")
			{
				System.Console.Write(prompt);
				
				string[] input = System.Console.ReadLine().Split(new char[]{'~'});
				string option1 = string.Empty;
				cmd = input[0];
				if (input.Length > 1)
					option1 = input[1];
				
				if (cmd == "status")
				{
					WriteCurrentStreamStatus();
				}
				else if (cmd == "play")
				{
					if (player.CurrentAudioStream != null && player.CurrentAudioStream.PlaybackState == PlaybackState.Playing)
					{
						player.Play();
					}
					else if (!string.IsNullOrEmpty(option1))
					{
						Uri path = new Uri(option1);
						player.LoadAudioStream(path);
						player.Play();
					}
					WriteCurrentStreamStatus();
				}
				else if (player.CurrentAudioStream != null)
				{
					if (cmd == "pause")
					{
						player.Play();
						WriteCurrentStreamStatus();
					}
					else if (cmd == "stop")
					{
						player.Stop();
						WriteCurrentStreamStatus();
					}
					else if (cmd == "seek" && !string.IsNullOrEmpty(option1))
					{
						int hours = 0, minutes = 0, seconds = 0;
						string[] parts = option1.Split(new char[]{':'}, 3);
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
						else seconds = Convert.ToInt32(option1);
						
						TimeSpan position = new TimeSpan(hours, minutes, seconds);
						player.Seek((int)position.TotalMilliseconds);
						WriteCurrentStreamStatus("Stream Position", position.ToString());
					}
					else if (cmd == "volume" && !string.IsNullOrEmpty(option1))
					{
						float volume = 0f;
						if (float.TryParse(option1, out volume))
						{
							if (volume < 0) volume = 0;
							if (volume > 1) volume = 1;
							player.SetVolume(volume);
							WriteCurrentStreamStatus("Stream Volume", volume.ToString());
						}
					}
				}
				System.Console.WriteLine();
			}
		}
		
		private static void WriteCurrentStreamStatus()
		{
			WriteCurrentStreamStatus(string.Empty, string.Empty);
		}
		
		private static void WriteCurrentStreamStatus(string command, string option)
		{
			if (string.IsNullOrEmpty(command))
				command = "Stream";
		
			if (string.IsNullOrEmpty(option))
				option = player.CurrentAudioStream.PlaybackState.ToString();
		
			if (player != null && player.CurrentAudioStream != null)
				System.Console.WriteLine(string.Format("{0} {1}", command, option));
			else System.Console.WriteLine("NO STREAM LOADED");
		}
	}
}
