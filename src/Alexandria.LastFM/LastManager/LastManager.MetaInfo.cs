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

namespace Alexandria.LastFM.LastManager
{
	/*
	This part of the LastManager class contains all MetaInfo related logic.
	*/
	public partial class LastManager
	{
		protected MetaInfo _CurrentSong;
		///<summary>
		///Gets the meta info about the current song
		///</summary>
		public MetaInfo CurrentSong
		{
			get
			{
				return this._CurrentSong;
			}
		}

		public event System.EventHandler OnNewSong;
		static System.Object UpdateLocker = new System.Object();

		///<summary>
		///Updates the metainfo about the current song
		///</summary>
		public void UpdateMetaInfo()
		{
			PerformUpdateMetaInfoDelegate SendDelegate = new PerformUpdateMetaInfoDelegate(this.PerformUpdateMetaInfo);
			System.AsyncCallback CallBack = new System.AsyncCallback(this.UpdateMetaInfoCallBack);
			SendDelegate.BeginInvoke(CallBack, null);
		}

		protected delegate MetaInfo PerformUpdateMetaInfoDelegate();

		protected MetaInfo PerformUpdateMetaInfo()
		{
			MetaInfo ConcurrentSong = this._CurrentSong;
			if (this.Status == ConnectionStatus.Recording && System.Threading.Monitor.TryEnter(LastManager.UpdateLocker))
			{
				try
				{
					System.IO.File.AppendAllText(PlatformSettings.TempPath + "\\" + "thelastripper.log",
										 "\n -Start update");

					HttpWebRequest hReq = (HttpWebRequest)WebRequest.Create(this.ServiceURL + "np.php?session=" + this.SessionID + "&debug=0");
					hReq.Timeout = 10000;
					System.IO.File.AppendAllText(PlatformSettings.TempPath + "\\" + "thelastripper.log",
											 "\n - URL :" + this.ServiceURL + "np.php?session=" + this.SessionID + "&debug=0");
					HttpWebResponse hRes = (HttpWebResponse)hReq.GetResponse();
					Stream ResponseStream = hRes.GetResponseStream();

					System.Byte[] Buffer = new System.Byte[LastManager.ProtocolBufferSize];

					System.Int32 Count = ResponseStream.Read(Buffer, 0, Buffer.Length);

					//WebClient Client = new WebClient();
					//System.String data = Client.DownloadString(this.ServiceURL + "np.php?session=" + this.SessionID + "&debug=0");


					ConcurrentSong = new MetaInfo(Encoding.UTF8.GetString(Buffer, 0, Count)); // data;
					//return ConcurrentSong;
				}
				catch (System.Exception e)
				{
					System.IO.File.AppendAllText(PlatformSettings.TempPath + "\\" + "thelastripper.log",
										 "\n - Update timeout ERROR: " + e.Message + e.StackTrace);
					//		System.Threading.Monitor.Exit(LastManager.UpdateLocker);
					//System.Threading.Thread.CurrentThread.Abort();
				}
				finally
				{
					System.IO.File.AppendAllText(PlatformSettings.TempPath + "\\" + "thelastripper.log",
										 "\n - Update finished");
					System.Threading.Monitor.Exit(LastManager.UpdateLocker);
				}
			}
			return ConcurrentSong;
		}

		protected void UpdateMetaInfoCallBack(System.IAsyncResult Res)
		{
			PerformUpdateMetaInfoDelegate SendDelegate = (PerformUpdateMetaInfoDelegate)((System.Runtime.Remoting.Messaging.AsyncResult)Res).AsyncDelegate;
			MetaInfo ConcurrentSong = SendDelegate.EndInvoke(Res);

			if (this._CurrentSong == null || !this._CurrentSong.Streaming)
			{
				this._CurrentSong = ConcurrentSong;
				if (this._CurrentSong.Streaming)
				{
					this.SetTimer();
					if (this.OnNewSong != null)
					{
						this.OnNewSong(this, ConcurrentSong);
					}
				}
				else
				{
					this.SetTimer(5000);
				}
			}
			else
			{
				if (!MetaInfo.Equals(ConcurrentSong, this._CurrentSong))
				{
					//Perform on new thread, perhaps not needed at all... 
					//Since all we are doing is IO work, not networking...
					this.SaveSong(this._CurrentSong);

					this._CurrentSong = ConcurrentSong;
					if (this.OnNewSong != null)
					{
						this.OnNewSong(this, ConcurrentSong);
					}
					this.SetTimer();
				}
			}

		}

		public void UpdateMetaInfo(System.Object Sender, System.EventArgs Args)
		{
			this.UpdateMetaInfo();
		}

		protected void SetTimer()
		{
			this.SetTimer(this.CurrentSong);
		}

		protected void SetTimer(MetaInfo SongInfo)
		{
			this.SetTimer(System.Convert.ToDouble(SongInfo.Trackduration) * 1000);
		}

		protected void SetTimer(System.Double Interval)
		{
			if ((Interval - 500) > 0)
			{
				//Set Timer 1
				this.Timer1.Enabled = false;
				this.Timer1.Interval = System.Convert.ToInt32(Interval - 500);
				this.Timer1.Tick += new EventHandler(this.UpdateMetaInfo);
				this.Timer1.Enabled = true;

				//Set Timer 2
				this.Timer2.Enabled = false;
				this.Timer2.Interval = System.Convert.ToInt32(Interval + 100);
				this.Timer2.Tick += new EventHandler(this.UpdateMetaInfo);
				this.Timer2.Enabled = true;

				//Set Timer 3
				this.Timer3.Enabled = false;
				this.Timer3.Interval = System.Convert.ToInt32(Interval + 700);
				this.Timer3.Tick += new EventHandler(this.UpdateMetaInfo);
				this.Timer3.Enabled = true;

				//Set timer 1
				/*this.Timer1.Stop();
				this.Timer1 = new System.Timers.Timer(Interval - 500);
				this.Timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.UpdateMetaInfo);
				this.Timer1.Start();
				
				//Set timer 2
				this.Timer2.Stop();
				this.Timer2 = new System.Timers.Timer(Interval + 100);
				this.Timer2.Elapsed += new System.Timers.ElapsedEventHandler(this.UpdateMetaInfo);
				this.Timer2.Start();
				
				//Set timer 3
				this.Timer3.Stop();
				this.Timer3 = new System.Timers.Timer(Interval + 700);
				this.Timer3.Elapsed += new System.Timers.ElapsedEventHandler(this.UpdateMetaInfo);
				this.Timer3.Start();*/
			}
			/*
			We're using 3 timers to make it easier to hit the right moment.
			*/
		}
	}
}
