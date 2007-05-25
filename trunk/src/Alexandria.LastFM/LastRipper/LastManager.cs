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
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Alexandria.LastFM.LastRipper
{

	///<summary>
	///
	///</summary>
	public partial class LastManager
	{
		protected System.String UserID;
		protected System.String _Password;
		protected System.String StreamURL;
		protected System.String SessionID;
		protected System.String BaseURL;
		protected System.String BasePath;
		protected System.Boolean Subscripter;
		protected System.String _MusicPath;
		protected const System.String PathSeparator = "\\";
		protected const System.Int32 ProtocolBufferSize = 102400;//4096;
		protected System.Boolean SkipSave = false;
		//protected System.Timers.Timer Timer1 = new System.Timers.Timer();
		//protected System.Timers.Timer Timer2 = new System.Timers.Timer();
		//protected System.Timers.Timer Timer3 = new System.Timers.Timer();
		protected System.Windows.Forms.Timer Timer1 = new System.Windows.Forms.Timer();
		protected System.Windows.Forms.Timer Timer2 = new System.Windows.Forms.Timer();
		protected System.Windows.Forms.Timer Timer3 = new System.Windows.Forms.Timer();
		protected ConnectionStatus Status = ConnectionStatus.Created;

		///<summary>
		///Initializes an instance of LastManager
		///</summary>
		public LastManager(System.String UserID, System.String Password, System.String MusicPath, System.String LastFMStation)
		{
			this.MusicPath = MusicPath;

			if (this.Handshake(UserID, Password))
			{
				this.ChangeStation(LastFMStation);
			}
		}

		///<summary>
		///Initializes an instance of LastManager
		///</summary>
		public LastManager(System.String MusicPath)
		{
			this.MusicPath = MusicPath;
		}

		public System.Boolean Handshake()
		{
			if (this.UserID == null || this._Password == null)
			{
				throw new System.Exception("UserName and password needed.");
			}
			HttpWebRequest hReq = (HttpWebRequest)WebRequest.Create("http://ws.audioscrobbler.com/radio/handshake.php?version=" + "1.1.1" + "&platform=" + "windows" + "&username=" + this.UserID + "&passwordmd5=" + this.Password + "&debug=" + "0" + "&partner=");

			//TODO: Multithread this method to make it more responsive
			HttpWebResponse hRes = (HttpWebResponse)hReq.GetResponse();
			Stream ResponseStream = hRes.GetResponseStream();

			System.Byte[] Buffer = new System.Byte[LastManager.ProtocolBufferSize];

			System.Int32 Count = ResponseStream.Read(Buffer, 0, Buffer.Length);
			if (this.ParseHandshake(Encoding.UTF8.GetString(Buffer, 0, Count)))
			{
				this.Status = ConnectionStatus.Connected;
				return true;
			}
			else
			{
				this.Status = ConnectionStatus.Created;
				return false;
			}
		}

		///<summary>
		///Gives the Last.FM server a handshake
		///</summary>
		public System.Boolean Handshake(System.String UserID, System.String Password)
		{
			this.UserID = UserID;
			this.Password = Password;
			System.IO.File.AppendAllText(PlatformSettings.TempPath + "\\" + "thelastripper.log",
										 "\n -Performs handshake");
			return this.Handshake();
		}

		///<summary>
		///Parses the reponse data from a handshake
		///</summary>
		protected System.Boolean ParseHandshake(System.String Data)
		{
			/* This function is a rewrite from Last-Exit */
			System.String[] Lines = Data.Split(new System.Char[] { '\n' });
			System.Boolean Result = false;

			foreach (System.String Line in Lines)
			{
				System.String[] Opts = Line.Split(new System.Char[] { '=' }, 2);

				switch (Opts[0].ToLower())
				{
					case "session":
						if (Opts[1].ToLower() == "failed")
						{
							Result = false;
						}
						else
						{
							Result = true;
							this.SessionID = Opts[1];
						}
						break;
					case "stream_url":
						this.StreamURL = Opts[1];
						break;
					case "subscriber":
						if (Opts[1] == "1")
						{
							this.Subscripter = true;
						}
						else
						{
							this.Subscripter = false;
						}
						break;
					case "framehack":
						//Don't know what this is for
						break;
					case "base_url":
						this.BaseURL = Opts[1];
						break;
					case "base_path":
						this.BasePath = Opts[1];
						break;
					default:
						Console.WriteLine("LastManager.ParseHandshake(): Unknown key: " + Opts[0]);
						break;
				}
			}
			return Result;
		}

		///<summary>
		///Gets an md5 hash of the password, or sets the password from hash.
		///</summary>
		public System.String Password
		{
			set
			{
				this._Password = value;
			}
			get
			{
				return this._Password;
			}
		}

		public static System.String CalculateHash(System.String Pass)
		{
			/*
			Almost copy paste from Last-Exit-4 another GNU GPL licensed Last.FM client
			*/
			MD5 Hasher = MD5.Create();
			byte[] Hash = Hasher.ComputeHash(Encoding.Default.GetBytes(Pass));
			StringBuilder StrHash = new StringBuilder();

			for (int i = 0; i < Hash.Length; ++i)
			{
				StrHash.Append(Hash[i].ToString("x2"));
			}

			return StrHash.ToString();
		}

		///<summary>
		///Gets or set the UserName
		///</summary>
		public System.String UserName
		{
			get
			{
				return this.UserID;
			}
			set
			{
				this.UserID = value;
			}
		}

		///<summary>
		///Gets or set the UserName
		///</summary>
		public System.String MusicPath
		{
			get
			{
				return this._MusicPath;
			}
			set
			{
				this._MusicPath = value;
			}
		}

		///<summary>
		///Gets current connection status
		///</summary>
		public ConnectionStatus ConnectionStatus
		{
			get
			{
				return this.Status;
			}
		}

		///<summary>
		///Gets the URL for the Last.FM server
		///</summary>
		protected System.String ServiceURL
		{
			get
			{
				return "http://" + this.BaseURL + this.BasePath + "/";
			}
		}

		public static System.String RemoveIllegalChars(System.String Str)
		{
			if (Str == null)
			{
				Str = "Unknown";
			}
			Str = Str.Replace('\\', '-');
			Str = Str.Replace('/', '-');
			System.String[] AllowedCharsArray = new System.String[] { "q", "w", "e", "r", "t", "a", "s", "d", "f", "g", "z", "x", "c", "v", "b", "y", "u", "i", "o", "p", "h", "j", "k", "l", "n", "m", "1", "2", "3", "4", "5", "6", "7", "8", "9", "_", " ", "(", ")", "-" };
			System.Collections.ArrayList AllowedChars = new System.Collections.ArrayList(AllowedCharsArray);
			System.String OutStr = "";

			foreach (System.Char TestChar in Str.ToCharArray())
			{
				if (AllowedChars.Contains(TestChar.ToString().ToLower()))
				{
					OutStr += TestChar.ToString();
				}
			}

			if (OutStr == "")
			{
				OutStr = "Unknown";
			}

			return OutStr;
		}
	}

	public enum ConnectionStatus
	{
		Created,	//LastManager is created
		Connected,	//LastManager has connection and owns a SessionID
		Recording	//LastManager is connected to stream and is recording
	}
}
