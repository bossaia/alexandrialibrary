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

namespace Alexandria.LastFM.LastRipper
{
	public class MetaInfo : System.EventArgs, IMetaMusic
	{
		protected System.String _Station;
		protected System.String _Artist;
		protected System.String _Track;
		protected System.String _Album;
		protected System.String _AlbumcoverSmall;
		protected System.String _AlbumcoverMedium;
		protected System.String _AlbumcoverLarge;
		protected System.String _Trackduration;
		protected System.String _Trackprogress;
		protected System.Boolean _Streaming = true;

		public MetaInfo(System.String Data)
		{
			System.String[] Lines = Data.Split(new System.Char[] { '\n' });
			foreach (System.String Line in Lines)
			{
				System.String[] Opts = Line.Split(new System.Char[] { '=' });
				switch (Opts[0].ToLower())
				{
					case "station":
						this._Station = Opts[1];
						break;
					case "artist":
						this._Artist = Opts[1];
						break;
					case "track":
						this._Track = Opts[1];
						break;
					case "album":
						this._Album = Opts[1];
						break;
					case "albumcover_small":
						this._AlbumcoverSmall = Opts[1];
						break;
					case "albumcover_medium":
						this._AlbumcoverMedium = Opts[1];
						break;
					case "albumcover_large":
						this._AlbumcoverLarge = Opts[1];
						break;
					case "trackduration":
						this._Trackduration = Opts[1];
						break;
					case "trackprogress":
						this._Trackprogress = Opts[1];
						break;
					case "streaming":
						if (Opts[1].ToLower() == "false")
						{

						}
						break;
				}
			}
		}

		public System.String Station
		{
			get
			{
				return this._Station;
			}
		}

		public System.String Artist
		{
			get
			{
				return this._Artist;
			}
		}

		public System.String Track
		{
			get
			{
				return this._Track;
			}
		}

		public System.String Album
		{
			get
			{
				return this._Album;
			}
		}

		public System.String AlbumcoverSmall
		{
			get
			{
				return this._AlbumcoverSmall;
			}
		}

		public System.String AlbumcoverMedium
		{
			get
			{
				return this._AlbumcoverMedium;
			}
		}

		public System.String AlbumcoverLarge
		{
			get
			{
				return this._AlbumcoverLarge;
			}
		}

		public System.String Trackduration
		{
			get
			{
				return this._Trackduration;
			}
		}

		public System.String Trackprogress
		{
			get
			{
				return this._Trackprogress;
			}
		}

		public System.Boolean Streaming
		{
			get
			{
				return this._Streaming;
			}
		}

		public override System.String ToString()
		{
			System.String OutStr = "";

			if (this._Streaming)
			{
				OutStr += "Track: " + this._Artist + " - " + this._Album + " - " + this._Track + "\n";
				OutStr += "From: " + this._Station + "\n";
				OutStr += "Duration: " + this._Trackduration;
			}
			else
			{
				OutStr = "Streaming: " + this._Streaming.ToString();
			}
			return OutStr;
		}

		public override System.Int32 GetHashCode()
		{
			return (this._Album + this._Artist + this._Track).GetHashCode();
		}
		new public static System.Boolean Equals(System.Object Obj1, System.Object Obj2)
		{
			if (Obj1.GetType() == typeof(MetaInfo) && Obj2.GetType() == typeof(MetaInfo))
			{
				if (((MetaInfo)Obj1).GetHashCode() == ((MetaInfo)Obj2).GetHashCode())
				{
					return true;
				}
			}
			return false;
		}
		public void AppendID3(System.IO.FileStream Stream)
		{
			//Blank string, used to prevent substring from running out of index
			System.String BlankString = "                                ";

			//Getting values
			System.String Title = LastManager.RemoveIllegalChars(this._Track) + BlankString;
			System.String Artist = LastManager.RemoveIllegalChars(this._Artist) + BlankString;
			System.String Album = LastManager.RemoveIllegalChars(this._Album) + BlankString;
			System.String Year = "0000" + BlankString;
			System.String Comment = "Last.FM by TheLastRipper." + BlankString;

			//Settings max length
			Title = Title.Substring(0, 30);
			Artist = Artist.Substring(0, 30).Trim();
			Album = Album.Substring(0, 30).Trim();
			Year = Year.Substring(0, 4).Trim();
			Comment = Comment.Substring(0, 28).Trim();

			System.Byte[] TagArray = new System.Byte[128];
			for (int i = 0; i < TagArray.Length; i++)
			{
				TagArray[i] = 0;
			}

			//Get encoder
			System.Text.Encoding Coder = new System.Text.ASCIIEncoding();

			//Get the bytes
			System.Byte[] Buffer = Coder.GetBytes("TAG");
			System.Array.Copy(Buffer, 0, TagArray, 0, Buffer.Length);
			Buffer = Coder.GetBytes(Title);
			System.Array.Copy(Buffer, 0, TagArray, 3, Buffer.Length);
			Buffer = Coder.GetBytes(Artist);
			System.Array.Copy(Buffer, 0, TagArray, 33, Buffer.Length);
			Buffer = Coder.GetBytes(Album);
			System.Array.Copy(Buffer, 0, TagArray, 63, Buffer.Length);
			Buffer = Coder.GetBytes(Year);
			System.Array.Copy(Buffer, 0, TagArray, 93, Buffer.Length);
			Buffer = Coder.GetBytes(Comment);
			System.Array.Copy(Buffer, 0, TagArray, 97, Buffer.Length);

			//Set Track number to 0, and genre to other
			TagArray[126] = System.Convert.ToByte(0);
			TagArray[127] = System.Convert.ToByte(12);

			//Write data to end of stream
			Stream.Seek(0, System.IO.SeekOrigin.End);
			Stream.Write(TagArray, 0, 128);
		}
	}
}
