
/***************************************************************************
 *  Engine.cs
 *
 *  Copyright (C) 2005 Novell
 *  Written by Chris Toshok (toshok@ximian.com)
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

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Security.Cryptography;
//using Mono.Security.Cryptography;
using System.Collections;
using System.Web;
using System.Timers;

//using GLib;
//using Banshee.MediaEngine;
//using Banshee.Base;
//using Banshee;

namespace AlexandriaOrg.Alexandria.LastFM //Banshee.Plugins.Audioscrobbler
{
	public class LastFMPlugin
	{
		#region Constructors
		public LastFMPlugin()
		{
			timeout_id = 0;
			state = State.IDLE;
			queue = new LastFMQueue();

			timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);			
		}
		#endregion
	
		#region Private Constant Fields
		private const int TICK_INTERVAL = 2000; /* 2 seconds */
		private const int FAILURE_LOG_MINUTES = 5; /* 5 minute delay on logging failure to upload information */
		private const int RETRY_SECONDS = 60; /* 60 second delay for transmission retries */
		private const string CLIENT_ID = "bsh"; //TODO: get my own three letter plugin code
		private const string CLIENT_VERSION = "0.1";
		private const string SCROBBLER_URL = "http://post.audioscrobbler.com/";
		private const string SCROBBLER_VERSION = "1.1";
		#endregion

		#region Private Fields
		private string username;
		private string md5_pass;
		private string post_url;
		private string security_token;

		private uint timeout_id;
		private DateTime next_interval;
		private DateTime last_upload_failed_logged;

		private LastFMQueue queue;

		//private bool song_started; /* if we were watching the current song from the beginning */
		//private bool queued; /* if current_track has been queued */
		//private bool sought; /* if the user has sought in the current playing song */

		private WebRequest current_web_req;
		private IAsyncResult current_async_result;
		private State state;
		private Timer timer = new Timer((double)TICK_INTERVAL);
		#endregion

		#region Private Methods
		
		#region StartTransitionHandler
		private void StartTransitionHandler()
		{
			if (timeout_id == 0)
			{
				//timeout_id = Timeout.Add(TICK_INTERVAL, StateTransitionHandler);
			}
		}
		#endregion

		#region StopTransitionHandler
		private void StopTransitionHandler()
		{
			timer.Stop();
		
			//if (timeout_id != 0)
			//{
				//GLib.Source.Remove(timeout_id);
				//timeout_id = 0;
			//}
		}
		#endregion

		#region MD5Encode
		private string MD5Encode(string pass)
		{
			if (pass == null || pass == String.Empty)
				return String.Empty;

			MD5 md5 = MD5.Create();

			byte[] hash = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(pass));
			
			string hexName = string.Empty;
			for(int i =0; i < hash.Length; i++)
			{
				hexName += hash[i].ToString("X").ToLowerInvariant();
			}
			//string.Format("{0:x2}", hash);  
			//Convert.ToString(hash); 
			//CryptoConvert.ToHex(hash).ToLower(); <-- this was the original call
			
			return hexName;
		}
		#endregion

		#region OnPlayerEngineEventChanged
		/*
		void OnPlayerEngineEventChanged(object o, PlayerEngineEventArgs args)
		{
			switch (args.Event)
			{
				// Queue if we're watching this song from the beginning,
				// it isn't queued yet and the user didn't seek until now,
				// we're actually playing, song position and length are greater than 0
				// and we already played half of the song or 240 seconds
				case PlayerEngineEvent.Iterate:
					if (song_started && !queued && !sought && PlayerEngineCore.CurrentState == PlayerEngineState.Playing &&
						PlayerEngineCore.Length > 0 && PlayerEngineCore.Position > 0 &&
						(PlayerEngineCore.Position > PlayerEngineCore.Length / 2 || PlayerEngineCore.Position > 240))
					{
						TrackInfo track = PlayerEngineCore.CurrentTrack;
						if (track == null)
						{
							queued = sought = false;
						}
						else
						{
							queue.Add(track, DateTime.Now - TimeSpan.FromSeconds(PlayerEngineCore.Position));
							queued = true;
						}
					}
					break;
				// Start of Stream: new song started
				case PlayerEngineEvent.StartOfStream:
					queued = sought = false;
					song_started = true;
					break;
				// End of Stream: song finished
				case PlayerEngineEvent.EndOfStream:
					song_started = queued = sought = false;
					break;
				// Did the user seek?
				case PlayerEngineEvent.Seek:
					sought = true;
					break;
			}
		}
		*/
		#endregion
		
		#region timer_Elapsed
		void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			StateTransitionHandler();
		}
		#endregion

		#region StateTransitionHandler
		private bool StateTransitionHandler()
		{
			/* if we're not connected, don't bother doing anything
			 * involving the network. */
			//if (!Globals.Network.Connected)
				//return true;

			/* and address changes in our engine state */
			switch (state)
			{
				case State.IDLE:
					if (queue.Count > 0)
					{
						if (username != null && md5_pass != null && security_token == null)
							state = State.NEED_HANDSHAKE;
						else
							state = State.NEED_TRANSMIT;
					}
					else
					{
						StopTransitionHandler();
					}
					break;
				case State.NEED_HANDSHAKE:
					if (DateTime.Now > next_interval)
					{
						Handshake();
					}
					break;
				case State.NEED_TRANSMIT:
					if (DateTime.Now > next_interval)
					{
						TransmitQueue();
					}
					break;
				case State.WAITING_FOR_RESP:
					System.Diagnostics.Debug.WriteLine("Waiting for response...");
					if (current_async_result != null)
					{
						System.Diagnostics.Debug.WriteLine("  Completed Asynchronously: " + (current_async_result.IsCompleted).ToString());
						System.Diagnostics.Debug.WriteLine("  Completed Synchronously : " + (current_async_result.CompletedSynchronously));
					}
					break;
				case State.WAITING_FOR_REQ_STREAM:
				case State.WAITING_FOR_HANDSHAKE_RESP:
					/* nothing here */
					break;
			}

			return true;
		}
		#endregion

		#region TransmitQueue
		private void TransmitQueue()
		{
			int num_tracks_transmitted;

			/* save here in case we're interrupted before we complete
			 * the request.  we save it again when we get an OK back
			 * from the server */
			queue.Save();

			next_interval = DateTime.MinValue;

			if (post_url == null)
			{
				return;
			}

			StringBuilder sb = new StringBuilder();

			sb.AppendFormat("u={0}&s={1}", HttpUtility.UrlEncode(username), security_token);

			sb.Append(queue.GetTransmitInfo(out num_tracks_transmitted));

			current_web_req = WebRequest.Create(post_url);
			current_web_req.Method = "POST";
			current_web_req.ContentType = "application/x-www-form-urlencoded";
			current_web_req.ContentLength = sb.Length;

			TransmitState ts = new TransmitState();
			ts.Count = num_tracks_transmitted;
			ts.StringBuilder = sb;

			state = State.WAITING_FOR_REQ_STREAM;
			current_async_result = current_web_req.BeginGetRequestStream(TransmitGetRequestStream, ts);
			if (current_async_result == null)
			{
				next_interval = DateTime.Now + new TimeSpan(0, 0, RETRY_SECONDS);
				state = State.IDLE;
			}
		}
		#endregion

		#region TransmitGetRequestStream
		void TransmitGetRequestStream(IAsyncResult ar)
		{
			Stream stream;

			try
			{
				stream = current_web_req.EndGetRequestStream(ar);
			}
			catch (Exception e)
			{
				Console.WriteLine("Failed to get the request stream: {0}", e);

				state = State.IDLE;
				next_interval = DateTime.Now + new TimeSpan(0, 0, RETRY_SECONDS);
				return;
			}

			TransmitState ts = (TransmitState)ar.AsyncState;
			StringBuilder sb = ts.StringBuilder;

			StreamWriter writer = new StreamWriter(@"C:\last.txt");
			//stream);
			writer.Write(sb.ToString());
			writer.Close();

			state = State.WAITING_FOR_RESP;
			//NOTE: for some reason TransmitGetResponse is never actually called
			current_async_result = current_web_req.BeginGetResponse(TransmitGetResponse, ts);
			if (current_async_result == null)
			{
				next_interval = DateTime.Now + new TimeSpan(0, 0, RETRY_SECONDS);
				state = State.IDLE;
			}
		}
		#endregion

		#region TransmitGetResponse
		void TransmitGetResponse(IAsyncResult ar)
		{
			WebResponse resp;

			try
			{
				resp = current_web_req.EndGetResponse(ar);
			}
			catch (Exception e)
			{
				Console.WriteLine("Failed to get the response: {0}", e);

				state = State.IDLE;
				next_interval = DateTime.Now + new TimeSpan(0, 0, RETRY_SECONDS);
				return;
			}

			TransmitState ts = (TransmitState)ar.AsyncState;

			Stream s = resp.GetResponseStream();

			StreamReader sr = new StreamReader(s, System.Text.Encoding.UTF8);

			string line;
			line = sr.ReadLine();

			System.Diagnostics.Debug.WriteLine("Response:\n" + line);

			DateTime now = DateTime.Now;
			if (line.StartsWith("FAILED"))
			{
				if (now - last_upload_failed_logged > TimeSpan.FromMinutes(FAILURE_LOG_MINUTES))
				{
					//LogCore.Instance.PushWarning("Audioscrobbler upload failed", line.Substring("FAILED".Length).Trim(), false);
					System.Diagnostics.Debug.WriteLine("Audioscrobbler upload failed");
					last_upload_failed_logged = now;
				}
				/* retransmit the queue on the next interval */
				state = State.NEED_TRANSMIT;
			}
			else if (line.StartsWith("BADUSER")
					 || line.StartsWith("BADAUTH"))
			{
				if (now - last_upload_failed_logged > TimeSpan.FromMinutes(FAILURE_LOG_MINUTES))
				{
					//LogCore.Instance.PushWarning("Audioscrobbler upload failed", "invalid authentication", false);
					System.Diagnostics.Debug.WriteLine("Audioscrobbler upload failed: invalid authentication");
					last_upload_failed_logged = now;
				}
				/* attempt to re-handshake (and retransmit) on the next interval */
				security_token = null;
				next_interval = DateTime.Now + new TimeSpan(0, 0, RETRY_SECONDS);
				state = State.IDLE;
				return;
			}
			else if (line.StartsWith("OK"))
			{
				/* if we've previously logged failures, be nice and log the successful upload. */
				if (last_upload_failed_logged != DateTime.MinValue)
				{
					//LogCore.Instance.PushInformation("Audioscrobbler upload succeeded", "", false);
					System.Diagnostics.Debug.WriteLine("Audioscrobbler upload succeeded");
					last_upload_failed_logged = DateTime.MinValue;
				}
				/* we succeeded, pop the elements off our queue */
				queue.RemoveRange(0, ts.Count);
				queue.Save();
				state = State.IDLE;
			}
			else
			{
				if (now - last_upload_failed_logged > TimeSpan.FromMinutes(FAILURE_LOG_MINUTES))
				{
					//LogCore.Instance.PushDebug("Audioscrobbler upload failed", String.Format("Unrecognized response: {0}", line), false);
					System.Diagnostics.Debug.WriteLine(string.Format("Audioscrobbler upload failed: unrecognized response '{0}'", line));
					last_upload_failed_logged = now;
				}
				state = State.IDLE;
			}

			/* now get the next interval */
			line = sr.ReadLine();
			if (line.StartsWith("INTERVAL"))
			{
				int interval_seconds = Int32.Parse(line.Substring("INTERVAL".Length));
				next_interval = DateTime.Now + new TimeSpan(0, 0, interval_seconds);
			}
			else
			{
				Console.WriteLine("expected INTERVAL..");
			}
		}
		#endregion

		#region Handshake
		//
		// Async code for handshaking
		//
		private void Handshake()
		{
			string uri = String.Format("{0}?hs=true&p={1}&c={2}&v={3}&u={4}",
										SCROBBLER_URL,
										SCROBBLER_VERSION,
										CLIENT_ID, CLIENT_VERSION,
										HttpUtility.UrlEncode(username));

			current_web_req = WebRequest.Create(uri);

			state = State.WAITING_FOR_HANDSHAKE_RESP;
			current_async_result = current_web_req.BeginGetResponse(HandshakeGetResponse, null);
			if (current_async_result == null)
			{
				next_interval = DateTime.Now + new TimeSpan(0, 0, RETRY_SECONDS);
				state = State.IDLE;
			}
		}
		#endregion

		#region HandshakeGetResponse
		void HandshakeGetResponse(IAsyncResult ar)
		{
			bool success = false;
			WebResponse resp;

			try
			{
				resp = current_web_req.EndGetResponse(ar);
			}
			catch (Exception e)
			{
				Console.WriteLine("failed to handshake: {0}", e);

				/* back off for a time before trying again */
				state = State.IDLE;
				next_interval = DateTime.Now + new TimeSpan(0, 0, RETRY_SECONDS);
				return;
			}

			Stream s = resp.GetResponseStream();

			StreamReader sr = new StreamReader(s, System.Text.Encoding.UTF8);

			string line;

			line = sr.ReadLine();
			if (line.StartsWith("FAILED"))
			{
				//LogCore.Instance.PushWarning("Audioscrobbler sign-on failed", line.Substring("FAILED".Length).Trim(), false);
				System.Diagnostics.Debug.WriteLine("Audioscrobbler sign-on failed");
			}
			else if (line.StartsWith("BADUSER"))
			{
				//LogCore.Instance.PushWarning("Audioscrobbler sign-on failed", "unrecognized user/password", false);
				System.Diagnostics.Debug.WriteLine("Audioscrobbler sign-on failed: unrecognized user/password");
			}
			else if (line.StartsWith("UPDATE"))
			{
				//LogCore.Instance.PushInformation("Audioscrobbler plugin needs updating",
											//String.Format("Fetch a newer version at {0}\nor update to a newer version of Banshee",
														   //line.Substring("UPDATE".Length).Trim()), false);				
				System.Diagnostics.Debug.WriteLine("Audioscrobbler plugin needs updating");
				
				success = true;
			}
			else if (line.StartsWith("UPTODATE"))
			{
				success = true;
			}

			/* read the challenge string and post url, if
			 * this was a successful handshake */
			if (success == true)
			{
				string challenge = sr.ReadLine().Trim();
				post_url = sr.ReadLine().Trim();

				security_token = MD5Encode(md5_pass + challenge);
				//Console.WriteLine ("security token = {0}", security_token);
			}

			/* read the trailing interval */
			line = sr.ReadLine();
			if (line.StartsWith("INTERVAL"))
			{
				int interval_seconds = Int32.Parse(line.Substring("INTERVAL".Length));
				next_interval = DateTime.Now + new TimeSpan(0, 0, interval_seconds);
			}
			else
			{
				Console.WriteLine("expected INTERVAL..");
			}

			/* XXX we shouldn't just try to handshake again for BADUSER */
			state = success ? State.IDLE : State.NEED_HANDSHAKE;
		}
		#endregion

		#endregion
		
		#region Public Properties
		/// <summary>
		/// Get the queue of tracks to submit to Last.fm
		/// </summary>
		public LastFMQueue Queue
		{
			get { return queue; }
		}
		#endregion
		
		#region Public Methods
		
		#region Start
		public void Start()
		{
			timer.Start();
			//song_started = false;
			//PlayerEngineCore.EventChanged += OnPlayerEngineEventChanged;
			queue.TrackAdded += delegate(object o, EventArgs args)
			{
				StartTransitionHandler();
			};
			queue.Load();
		}
		#endregion
		
		#region Stop
		public void Stop()
		{
			//PlayerEngineCore.EventChanged -= OnPlayerEngineEventChanged;

			StopTransitionHandler();

			if (current_web_req != null)
			{
				current_web_req.Abort();
			}

			queue.Save();
		}
		#endregion
		
		#region SetUserPassword
		public void SetUserPassword(string username, string pass)
		{
			if (username == "" || pass == "")
				return;

			this.username = username;
			this.md5_pass = MD5Encode(pass);

			if (security_token != null)
			{
				security_token = null;
				state = State.NEED_HANDSHAKE;
			}
		}
		#endregion
		
		#endregion
	}
}
