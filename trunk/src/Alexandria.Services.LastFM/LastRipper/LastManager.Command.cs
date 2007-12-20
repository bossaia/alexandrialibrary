// LibLastRip - A Last.FM ripping library for TheLastRipper
// Copyright (C) 2007  Jop... (Jonas F. Jensen).
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

using System;
using System.Net;
using System.IO;
using System.Text;

namespace Alexandria.LastFM.LastRipper
{
	/*
	This part of the LastManager class handles and exposes Last.FM commands.
	*/
	public partial class LastManager
	{
		///<summary>
		///Sends a command to the Last.FM server
		///</summary>
		protected System.Boolean SendCommand(System.String Command)
		{
			System.Boolean Status = false;
			try
			{
				HttpWebRequest hReq = (HttpWebRequest)WebRequest.Create(this.ServiceURL + "control.php?session=" + this.SessionID + "&command=" + Command + "&debug=0");
				hReq.Timeout = 3000;
				HttpWebResponse hRes = (HttpWebResponse)hReq.GetResponse();
				Stream ResponseStream = hRes.GetResponseStream();

				System.Byte[] Buffer = new System.Byte[LastManager.ProtocolBufferSize];

				System.Int32 Count = ResponseStream.Read(Buffer, 0, Buffer.Length);
				System.String[] Data = Encoding.UTF8.GetString(Buffer, 0, Count).Split(new System.Char[] { '\n' });

				Status = false;
				foreach (System.String Line in Data)
				{
					System.String[] Opts = Line.Split(new System.Char[] { '=' });

					if (Opts[0].ToLower() == "response")
					{
						if (Opts[1].ToLower() == "ok")
						{
							Status = true;
						}
					}
				}
			}
			catch
			{
				Status = false;
			}
			return Status;
		}

		///<summary>
		///Skips the current song an moves on
		///</summary>
		public System.Boolean SkipSong()
		{
			if (this.Status == ConnectionStatus.Recording)
			{
				System.Boolean Result = this.SendCommand("skip");
				if (Result)
				{
					//Making sure we don't save half a file
					this.SkipSave = true;
					this.UpdateMetaInfo();
					//new System.Threading.Thread(new System.Threading.ThreadStart(this.UpdateMetaInfo));
				}
				return Result;
			}
			else
			{
				return false;
			}
		}

		///<summary>
		///Loves the current song
		///</summary>
		public System.Boolean LoveSong()
		{
			if (this.Status == ConnectionStatus.Recording)
			{
				return this.SendCommand("love");
			}
			else
			{
				return false;
			}
		}

		///<summary>
		///Bans the current song
		///</summary>
		public System.Boolean BanSong()
		{
			if (this.Status == ConnectionStatus.Recording)
			{
				System.Boolean Result = this.SendCommand("ban");
				if (Result)
				{
					//Dont save a half song
					this.SkipSave = true;
					this.UpdateMetaInfo();
				}
				return Result;
			}
			else
			{
				return false;
			}
		}

		public System.Boolean ChangeStation(System.String LastFMStation)
		{
			if (this.Status == ConnectionStatus.Created)
			{
				//We can't perform this action
				return false;
			}
			else
			{
				HttpWebRequest hReq = (HttpWebRequest)WebRequest.Create(this.ServiceURL + "adjust.php?session=" + this.SessionID + "&url=" + LastFMStation + "&debug=0");
				hReq.Timeout = 10000;
				HttpWebResponse hRes = (HttpWebResponse)hReq.GetResponse();
				Stream ResponseStream = hRes.GetResponseStream();

				System.Byte[] Buffer = new System.Byte[LastManager.ProtocolBufferSize];

				System.Int32 Count = ResponseStream.Read(Buffer, 0, Buffer.Length);
				System.String[] Data = Encoding.UTF8.GetString(Buffer, 0, Count).Split(new System.Char[] { '\n' });

				System.Boolean Status = false;
				foreach (System.String Line in Data)
				{
					System.String[] Opts = Line.Split(new System.Char[] { '=' });

					if (Opts[0].ToLower() == "response")
					{
						if (Opts[1].ToLower() == "ok")
						{
							Status = true;
							if (this.Status == ConnectionStatus.Recording)
							{
								//Dont save the current song
								this.SkipSave = true;
							}
							else
							{
								this.Status = ConnectionStatus.Recording;
								this.StartRecording();
							}

							this.UpdateMetaInfo();
							//new System.Threading.Thread(new System.Threading.ThreadStart(this.UpdateMetaInfo));
						}
					}
				}

				return Status;
			}
		}
	}
}
