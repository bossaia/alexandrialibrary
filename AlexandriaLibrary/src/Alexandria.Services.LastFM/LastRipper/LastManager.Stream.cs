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
using System.IO;
using System.Net;

namespace Alexandria.LastFM.LastRipper
{
	/*
	This part of the class handles all stream related matters.
	*/
	public partial class LastManager
	{
		protected FileStream TempFile;
		protected System.Byte[] Buffer = new System.Byte[4096];
		protected System.IAsyncResult ReadHandle;

		protected void StartRecording()
		{
			//Create or overwrite tempfile
			this.TempFile = File.Create(PathSettings.TempFilePath);

			//Getting stream
			WebRequest wReq = WebRequest.Create(this.StreamURL);
			HttpWebResponse hRes = (HttpWebResponse)wReq.GetResponse();
			System.IO.Stream RadioStream = hRes.GetResponseStream();

			//Start reading process
			this.ReadHandle = RadioStream.BeginRead(this.Buffer, 0, this.Buffer.Length, new System.AsyncCallback(this.TempSave), RadioStream);
		}
		//TODO: Deside whether this is good or bad!
		/*
		Number of times the stream has been dead, it happens
		but we might kickstart the stream with an SkipSong command if it happens
		too much...
		*/
		protected System.Int32 DeadStreamCount = 0;
		protected System.Boolean IsKickStarted = false;
		protected System.Boolean IsRestarted = false;
		static System.Object StreamLocker = new System.Object();

		protected void TempSave(System.IAsyncResult Res)
		{
			if (System.Threading.Monitor.TryEnter(LastManager.StreamLocker))
			{
				try
				{
					Stream RadioStream = (Stream)Res.AsyncState;
					System.Int32 Count = RadioStream.EndRead(Res);

					if (Count > 0)
					{
						this.DeadStreamCount = 0;
						this.IsKickStarted = false;
						this.IsRestarted = false;
						this.TempFile.Write(this.Buffer, 0, Count);
						this.ReadHandle = RadioStream.BeginRead(this.Buffer, 0, this.Buffer.Length, new System.AsyncCallback(this.TempSave), RadioStream);
					}
					else
					{
						if (this.DeadStreamCount < 5)
						{
							this.DeadStreamCount += 1;
							this.ReadHandle = RadioStream.BeginRead(this.Buffer, 0, this.Buffer.Length, new System.AsyncCallback(this.TempSave), RadioStream);
						}
						else
						{
							//TODO: handle a "Not enough content left..." error
							if (this.IsKickStarted)
							{
								if (this.IsRestarted)
								{
									//Declare connection dead
									this.Status = ConnectionStatus.Created;
									//Give it a handshake, just to try
									this.Handshake(this.UserID, this.Password);
									this.IsRestarted = false;
									this.IsKickStarted = false;
								}
								else
								{
									RadioStream.Close();
									this.TempFile.Close();
									this.StartRecording();
									this.IsKickStarted = false;
									this.IsRestarted = true;
								}
							}
							else
							{
								this.IsKickStarted = true;
								this.DeadStreamCount = 0;
								this.SkipSong();
								this.ReadHandle = RadioStream.BeginRead(this.Buffer, 0, this.Buffer.Length, new System.AsyncCallback(this.TempSave), RadioStream);
							}
						}
					}
				}
				finally
				{
					System.Threading.Monitor.Exit(LastManager.StreamLocker);
				}
			}
		}

		protected void SaveSong(MetaInfo SongInfo)
		{
			System.Boolean DownloadCovers = false;
			System.String AlbumPath = "";
			System.String NewFilePath = "";

			lock (LastManager.StreamLocker)
			{
				Stream RadioStream = (Stream)this.ReadHandle.AsyncState;
				System.Int32 Count = RadioStream.EndRead(this.ReadHandle);

				if (this.SkipSave || !SongInfo.Streaming)
				{
					//Close file
					this.TempFile.Close();

					//Create or overwrite tempfile
					this.TempFile = File.Create(PathSettings.TempFilePath);

					//Start recording agian
					this.ReadHandle = RadioStream.BeginRead(this.Buffer, 0, this.Buffer.Length, new System.AsyncCallback(this.TempSave), RadioStream);

					//Change SkipSave
					this.SkipSave = false;
				}
				else
				{

					//Write last data from stream
					this.TempFile.Write(this.Buffer, 0, Count);

					//Write metadata to stream as ID3v1
					SongInfo.AppendID3(this.TempFile);

					//Write the file, and close it
					this.TempFile.Flush();
					this.TempFile.Close();
					this.TempFile.Dispose();

					//Filesystem paths
					AlbumPath = this.MusicPath + System.IO.Path.DirectorySeparatorChar + LastManager.RemoveIllegalChars(SongInfo.Artist) + System.IO.Path.DirectorySeparatorChar + LastManager.RemoveIllegalChars(SongInfo.Album) + System.IO.Path.DirectorySeparatorChar;
					NewFilePath = AlbumPath + LastManager.RemoveIllegalChars(SongInfo.Track) + ".mp3";

					//Dont overwrite file if it already exist, new rip may be bad, and we should leave it to the user to sort them manually
					if (File.Exists(NewFilePath))
					{
						File.Delete(PathSettings.TempFilePath);
					}
					else
					{
						if (!Directory.Exists(AlbumPath))
						{
							Directory.CreateDirectory(AlbumPath);
						}
						File.Move(PathSettings.TempFilePath, NewFilePath);
					}

					//Create or overwrite tempfile
					this.TempFile = File.Create(PathSettings.TempFilePath);

					//Start recording agian
					this.ReadHandle = RadioStream.BeginRead(this.Buffer, 0, this.Buffer.Length, new System.AsyncCallback(this.TempSave), RadioStream);

					//Set download covers, do this outside the lock.
					DownloadCovers = true;
				}
			}
			if (DownloadCovers)
			{
				//Download covers
				WebClient Client = new WebClient();

				if ((!File.Exists(AlbumPath + "SmallCover.jpg")) && SongInfo.AlbumcoverSmall != null)
					Client.DownloadFile(SongInfo.AlbumcoverSmall, AlbumPath + "SmallCover.jpg");

				if ((!File.Exists(AlbumPath + "MediumCover.jpg")) && SongInfo.AlbumcoverMedium != null)
					Client.DownloadFile(SongInfo.AlbumcoverMedium, AlbumPath + "MediumCover.jpg");

				if ((!File.Exists(AlbumPath + "LargeCover.jpg")) && SongInfo.AlbumcoverLarge != null)
					Client.DownloadFile(SongInfo.AlbumcoverLarge, AlbumPath + "LargeCover.jpg");
			}
		}
	}
}
