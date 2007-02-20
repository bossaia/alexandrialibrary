using System;
using System.Collections;
using System.Net;
using System.Web.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;

namespace AlexandriaOrg.Alexandria.LastFM
{
	public class OldLastFMClient
	{
		#region Constructors
		public OldLastFMClient()
		{
		}
		#endregion
	
		#region Constants
		//		public const string GET_SESSION_URL = "http://wsdev.audioscrobbler.com/radio/getsession.php";
		//		public const string GET_SESSION_URL_USERNAME_VAR = "username";
		//		public const string GET_SESSION_URL_PASSWORD_VAR = "passwordmd5";
		//		public const string GET_SESSION_FAIL_CODE = "FAILED";
		//		public const string PLAYLIST_FILE_NAME = "lastfm.pls";
		//		public const string NOW_PLAYING_COVER_ART_SMALL_FILE_NAME = "nowplaying_small.jpg";
		//		public const string NOW_PLAYING_COVER_ART_MEDIUM_FILE_NAME = "nowplaying_medium.jpg";
		//		public const string NOW_PLAYING_COVER_ART_LARGE_FILE_NAME = "nowplaying_large.jpg";
		//		public const string NOW_PLAYING_URL = "http://wsdev.audioscrobbler.com/radio/np.php";
		//		public const string NOW_PLAYING_URL_SESSION_VAR = "session";
		//		public const string NOW_PLAYING_FAIL_CODE = "STREAMING=FALSE";
		//		public const string COMMANDS_URL = "http://wsdev.audioscrobbler.com/radio/control.php";
		//		public const string COMMANDS_URL_SESSION_VAR = "session";
		//		public const string COMMANDS_URL_COMMAND_VAR = "command";
		//		public const string COMMANDS_KEYWORD_LOVE = "love";
		//		public const string COMMANDS_KEYWORD_SKIP = "skip";
		//		public const string COMMANDS_KEYWORD_BAN = "ban";
		//		public const string COMMANDS_KEYWORD_RECORD_TO_PROFILE = "rtp";
		//		public const string COMMANDS_KEYWORD_NO_RECORD_TO_PROFILE = "nortp";
		//		public const string COMMANDS_FAIL_CODE = "RESPONSE=FAILED";
		//		public const string COMMANDS_SUCCESS_CODE = "RESPONSE=OK";
		//		public const string CHANGE_STATION_URL = "http://wsdev.audioscrobbler.com/radio/tune.php";
		//		public const string CHANGE_STATION_SESSION_VAR = "session";
		//		public const string CHANGE_STATION_MODE_VAR = "mode";
		//		public const string CHANGE_STATION_SUBJECT_VAR = "subject";
		//		public const string CHANGE_STATION_MODE_PERSONAL = "personal";
		//		public const string CHANGE_STATION_MODE_PROFILE = "profile";
		//		public const string CHANGE_STATION_MODE_Neighbor = "Neighbor";
		//		public const string CHANGE_STATION_SUCCESS_CODE = "RESPONSE=OK";
		//		public const string CHANGE_STATION_FAIL_CODE = "RESPONSE=FAILED";
		//
		//		public const string WEBSERVICE_FAIL_CODE = "<B>NOTICE</B>";
		//		public const string WEBSERVICE_ERROR_CODE = "WEBSERVICE_DOWN";
		//
		//		public const string SETTINGS_FILE_NAME = "mylastfmsettings.xml";
		#endregion

		#region Private Fields
		private Hashtable resources = new Hashtable();
		#endregion

		#region Private Methods
		
		#region ProcessLogin
		private bool ProcessLogin(string username, string password, ref OldLoginResponse lastFmLogin)
		{
			WebResponse response = null;
			StreamReader reader = null;
			bool success = false;

			try
			{
				WebRequest request = null;

				string hashedPassword = string.Empty;
				string requestUrl = string.Empty;
				string sessionId = string.Empty;
				string streamingUrl = string.Empty;
				string responseLine = string.Empty;

				if (lastFmLogin == null)
					hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5").ToLower();
				else
				{
					username = lastFmLogin.Username;
					hashedPassword = lastFmLogin.PasswordMd5;
				}

				requestUrl = this.resources["GET_SESSION_URL"].ToString() + "?" + this.resources["GET_SESSION_URL_USERNAME_VAR"].ToString() + "=" + username +
					"&" + this.resources["GET_SESSION_URL_PASSWORD_VAR"].ToString() + "=" + hashedPassword;

				request = WebRequest.Create(requestUrl);
				response = request.GetResponse();
				reader = new StreamReader(response.GetResponseStream());

				responseLine = reader.ReadLine();

				if (responseLine.ToUpper().IndexOf(this.resources["GET_SESSION_FAIL_CODE"].ToString()) == -1)
				{
					sessionId = responseLine.Substring(responseLine.IndexOf("=") + 1);

					responseLine = reader.ReadLine();
					streamingUrl = responseLine.Substring(responseLine.IndexOf("=") + 1);

					lastFmLogin = new OldLoginResponse(sessionId, streamingUrl, username, hashedPassword);

					responseLine = reader.ReadLine();
					if (responseLine.Substring(responseLine.IndexOf("=") + 1) == "1")
					{
						lastFmLogin.PaidAccount = true;
					}

					success = true;
				}
			}

			catch (Exception ex)
			{
				throw new Exception("An error occurred in LastFm.ProcessLogin().", ex);
			}

			finally
			{
				if (reader != null)
					reader.Close();

				if (response != null)
					response.Close();
			}

			return success;
		}
		#endregion

		#endregion

		#region Public Properties
		public Hashtable Resources
		{
			get { return resources; }
		}
		#endregion
		
		#region Public Methods
		
		#region ChangeStation
		public bool ChangeStation(string sessionId, StationModes mode)
		{
			return ChangeStation(sessionId, mode, string.Empty);
		}

		public bool ChangeStation(string sessionId, StationModes mode, string subject)
		{
			//lastfm://artist/Amon%2520Tobin/similarartists
			//lastfm://user/usernamehere/loved
			//lastfm://user/usernamehere/neighbors
			//lastfm://user/usernamehere/personal
			//lastfm://globaltags/electronica

			bool success = false;

			try
			{
				string postData = string.Empty;
				string response = string.Empty;
				string lastFmProtocol = string.Empty;

				switch (mode)
				{
					case StationModes.Personal:
						lastFmProtocol = string.Format(this.resources["CHANGE_STATION_PERSONAL"].ToString(), subject);
						break;

					case StationModes.Loved:
						lastFmProtocol = string.Format(this.resources["CHANGE_STATION_LOVED"].ToString(), subject);
						break;

					case StationModes.Neighbor:
						lastFmProtocol = string.Format(this.resources["CHANGE_STATION_NEIGHBOR"].ToString(), subject);
						break;
				}
				success = ChangeStation(sessionId, lastFmProtocol);
			}

			catch (Exception ex)
			{
				throw new Exception("An error occurred in LastFm.ChangeStation().", ex);
			}

			finally
			{
			}

			return success;
		}

		public bool ChangeStation(string sessionId, string lastFmProtocol)
		{
			//lastfm://artist/Amon%2520Tobin/similarartists
			//lastfm://user/jongalloway/loved
			//lastfm://user/jongalloway/neighbors
			//lastfm://user/jongalloway/personal
			//lastfm://globaltags/electronica
			bool success = false;

			try
			{
				string postData = string.Empty;
				string response = string.Empty;

				postData = HttpUtility.UrlEncode(this.resources["CHANGE_STATION_SESSION_VAR"].ToString()) + "=" +
					HttpUtility.UrlEncode(sessionId) +
					"&url=" +
					HttpUtility.UrlEncode(lastFmProtocol);

				//if (this._isDebuggingOn)
				//{
					//this._debugWindow.AddDebugEntry("ChangeStation", string.Empty);
				//}

				response = RequestData(this.resources["CHANGE_STATION_URL"].ToString(), postData);

				if (response.ToUpper().IndexOf(this.resources["CHANGE_STATION_SUCCESS_CODE"].ToString()) > -1)
				{
					success = true;
				}
			}

			catch (Exception ex)
			{
				throw new Exception("An error occurred in LastFm.ChangeStation().", ex);
			}

			finally
			{
			}

			return success;
		}
		#endregion

		#region RequestData
		public string RequestData(string url, string postData)
		{
			////HttpWebRequest request = null;
			//HttpWebResponse response = null;
			//Stream stream = null;
			//StreamWriter writer = null;
			//StreamReader reader = null;
			string result = string.Empty;

			try
			{
				//byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(postData);

				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url + "?" + postData);

				req.Method = "POST";
				req.ContentType = "application/x-www-form-urlencoded";

				req.ContentLength = postData.Length;

				StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
				stOut.Write(postData);
				stOut.Close();

				result = new StreamReader(req.GetResponse().GetResponseStream()).ReadToEnd();

				////request = (HttpWebRequest)HttpWebRequest.Create(url + "?" + postData);
				//request.Method = "POST";
				//request.KeepAlive = false;
				//request.Headers.Set("Pragma", "no-cache");
				//request.Headers.Set("Translate", "f");
				//request.Headers.Set("Accept-Language", "en-us");
				//request.ContentType = "application/x-www-form-urlencoded";

				//request.ContentLength = postData.Length; //data.Length;

				//stream = request.GetResponse().GetResponseStream();
				//stream.Write(data, 0, data.Length);
				//stream.Close();

				//writer = new StreamWriter(request.GetRequestStream());
				//writer.WriteLine(postData);
				//writer.Close();

				//response = (HttpWebResponse);
				////result = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();

				//if (this._isDebuggingOn)
				//{
					//this._debugWindow.AddDebugEntry("RequestData", url);
					//this._debugWindow.AddDebugEntry("RequestData", postData);
					//this._debugWindow.AddDebugEntry("RequestData", result);
				//}

				////return result;
			}

			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

			/*finally
			{
				//if(reader != null)
				//	reader.Close();

				//if(response != null)
				//	response.Close();
			}*/

			return result;
		}
		#endregion

		#region SendCommandPost
		public bool SendCommandPost(string sessionId, CommandKeywords keyword)
		{
			bool success = false;

			try
			{
				string requestUrl = string.Empty;
				string responseLine = string.Empty;
				string commandKeyword = string.Empty;

				switch (keyword)
				{
					case CommandKeywords.Love:
						commandKeyword = this.resources["COMMANDS_KEYWORD_LOVE"].ToString();
						break;

					case CommandKeywords.Skip:
						commandKeyword = this.resources["COMMANDS_KEYWORD_SKIP"].ToString();
						break;

					case CommandKeywords.Ban:
						commandKeyword = this.resources["COMMANDS_KEYWORD_BAN"].ToString();
						break;

					case CommandKeywords.RecordToProfile:
						commandKeyword = this.resources["COMMANDS_KEYWORD_RECORD_TO_PROFILE"].ToString();
						break;

					case CommandKeywords.NoRecordToProfile:
						commandKeyword = this.resources["COMMANDS_KEYWORD_NO_RECORD_TO_PROFILE"].ToString();
						break;
				}

				requestUrl = this.resources["COMMANDS_URL_SESSION_VAR"].ToString() + "=" + sessionId +
					"&" + this.resources["COMMANDS_URL_COMMAND_VAR"].ToString() + "=" + commandKeyword;

				responseLine = RequestData(this.resources["COMMANDS_URL"].ToString(), requestUrl);

				if (responseLine.ToUpper().IndexOf(this.resources["COMMANDS_SUCCESS_CODE"].ToString()) > -1)
				{
					success = true;
				}
			}

			catch (Exception ex)
			{
				throw new Exception("An error occurred in LastFm.SendCommandPost().", ex);
			}

			return success;
		}
		#endregion

		#region SendCommand
		public bool SendCommand(string sessionId, CommandKeywords keyword)
		{
			WebResponse response = null;
			StreamReader reader = null;
			bool success = false;

			try
			{
				WebRequest request = null;

				string requestUrl = string.Empty;
				string responseLine = string.Empty;
				string commandKeyword = string.Empty;

				switch (keyword)
				{
					case CommandKeywords.Love:
						commandKeyword = this.resources["COMMANDS_KEYWORD_LOVE"].ToString();
						break;

					case CommandKeywords.Skip:
						commandKeyword = this.resources["COMMANDS_KEYWORD_SKIP"].ToString();
						break;

					case CommandKeywords.Ban:
						commandKeyword = this.resources["COMMANDS_KEYWORD_BAN"].ToString();
						break;
				}

				requestUrl = this.resources["COMMANDS_URL"].ToString() + "?" + this.resources["COMMANDS_URL_SESSION_VAR"].ToString() + "=" + sessionId +
					"&" + this.resources["COMMANDS_URL_COMMAND_VAR"].ToString() + "=" + commandKeyword;

				request = WebRequest.Create(requestUrl);
				response = request.GetResponse();
				reader = new StreamReader(response.GetResponseStream());

				responseLine = reader.ReadLine();

				if (responseLine.ToUpper().IndexOf(this.resources["COMMANDS_SUCCESS_CODE"].ToString()) > -1)
				{
					success = true;
				}
			}

			catch (Exception ex)
			{
				throw new Exception("An error occurred in LastFm.SendCommand().", ex);
			}

			finally
			{
				if (reader != null)
					reader.Close();

				if (response != null)
					response.Close();
			}

			return success;
		}
		#endregion

		#region UpdateNowPlayingInformation
		public bool UpdateNowPlayingInformation(string sessionId, ref OldNowPlayingResponse nowPlaying)
		{
			//WebResponse response = null;
			//StreamReader reader = null;
			bool success = false;

			try
			{
				//WebRequest request = null;

				//string requestUrl = string.Empty;
				string postData = string.Empty;
				string response = string.Empty;
				//int counter = 0;
				System.Collections.ArrayList responses = new System.Collections.ArrayList();
				//string responseLine = string.Empty;

				//requestUrl = NOW_PLAYING_URL + "?" + NOW_PLAYING_URL_SESSION_VAR + "=" + sessionId;
				postData = this.resources["NOW_PLAYING_URL_SESSION_VAR"].ToString() + "=" + sessionId;

				//request = WebRequest.Create(requestUrl);
				//response = request.GetResponse();
				//reader = new StreamReader(response.GetResponseStream());

				if (nowPlaying == null)
					nowPlaying = new OldNowPlayingResponse();

				//responseLine = reader.ReadLine();

				//if (this._isDebuggingOn)
				//{
					//this._debugWindow.AddDebugEntry("GetNowPlayingInformation", string.Empty);
				//}

				Hashtable hashResponse = LookupResponse(RequestData(this.resources["NOW_PLAYING_URL"].ToString(), postData));

				responses.AddRange(response.Split('\n'));

				if (response.ToUpper().IndexOf(this.resources["WEBSERVICE_FAIL_CODE"].ToString()) > -1)
					throw new Exception(this.resources["WEBSERVICE_ERROR_CODE"].ToString());

				if (response.ToUpper().IndexOf(this.resources["NOW_PLAYING_FAIL_CODE"].ToString()) == -1)
				{
					//if(counter < responses.Count - 1)
					//	nowPlaying.Streaming = responses[counter].ToString().Substring(responses[counter].ToString().IndexOf("=") + 1);
					//counter++;

					nowPlaying.Streaming = hashResponse["streaming"].ToString();

					if (nowPlaying.Streaming.ToLower().Equals("false"))
					{
						throw new Exception("Streaming is not happening. Damn.");
					}

					//nowPlaying.Streaming = LookupResponse("streaming", ref responses);

					//if(counter < responses.Count - 1)
					//	nowPlaying.Station = responses[counter].ToString().Substring(responses[counter].ToString().ToString().IndexOf("=") + 1);
					//counter++;

					nowPlaying.Station = (hashResponse.ContainsKey("station") ? hashResponse["station"].ToString() : string.Empty);
					//nowPlaying.Station = LookupResponse("station", ref responses);

					//if(counter < responses.Count - 1)
					//	nowPlaying.StationUrl = responses[counter].ToString().Substring(responses[counter].ToString().IndexOf("=") + 1);
					//counter++;

					nowPlaying.StationUrl = (hashResponse.ContainsKey("station_url") ? hashResponse["station_url"].ToString() : string.Empty);
					//nowPlaying.StationUrl = LookupResponse("station_url", ref responses);

					//if(counter < responses.Count - 1)
					//	nowPlaying.StationFeed = responses[counter].ToString().Substring(responses[counter].ToString().IndexOf("=") + 1);
					//counter++;

					nowPlaying.StationFeed = (hashResponse.ContainsKey("stationfeed") ? hashResponse["stationfeed"].ToString() : string.Empty);
					//nowPlaying.StationFeed = LookupResponse("stationfeed", ref responses);

					//if(counter < responses.Count - 1)
					//	nowPlaying.StationFeedUrl = responses[counter].ToString().Substring(responses[counter].ToString().IndexOf("=") + 1);
					//counter++;
					nowPlaying.StationFeedUrl = (hashResponse.ContainsKey("stationfeed_url") ? hashResponse["stationfeed_url"].ToString() : string.Empty);
					//nowPlaying.StationFeedUrl = LookupResponse("stationfeed_url", ref responses);

					//if(counter < responses.Count - 1)
					//	nowPlaying.Artist = responses[counter].ToString().Substring(responses[counter].ToString().IndexOf("=") + 1);
					//counter++;

					nowPlaying.Artist = hashResponse["artist"].ToString();
					//nowPlaying.Artist = LookupResponse("artist", ref responses);

					//if(counter < responses.Count - 1)
					//	nowPlaying.ArtistUrl = responses[counter].ToString().Substring(responses[counter].ToString().IndexOf("=") + 1);
					//counter++;

					nowPlaying.ArtistUrl = hashResponse["artist_url"].ToString();
					//nowPlaying.ArtistUrl = LookupResponse("artist_url", ref responses);

					//if(counter < responses.Count - 1)
					//	nowPlaying.Track = responses[counter].ToString().Substring(responses[counter].ToString().IndexOf("=") + 1);
					//counter++;

					nowPlaying.Track = hashResponse["track"].ToString();
					//nowPlaying.Track = LookupResponse("track", ref responses);

					//if(counter < responses.Count - 1)
					//	nowPlaying.TrackUrl = responses[counter].ToString().Substring(responses[counter].ToString().IndexOf("=") + 1);
					//counter++;

					nowPlaying.TrackUrl = hashResponse["track_url"].ToString();
					//nowPlaying.TrackUrl = LookupResponse("track_url", ref responses);

					//if(counter < responses.Count - 1)
					//	nowPlaying.Album = responses[counter].ToString().Substring(responses[counter].ToString().IndexOf("=") + 1);
					//counter++;

					nowPlaying.Album = hashResponse["album"].ToString();
					//nowPlaying.Album = LookupResponse("album", ref responses);

					//if(counter < responses.Count - 1)
					//	nowPlaying.AlbumUrl = responses[counter].ToString().Substring(responses[counter].ToString().IndexOf("=") + 1);
					//counter++;

					nowPlaying.AlbumUrl = hashResponse["album_url"].ToString();
					//nowPlaying.AlbumUrl = LookupResponse("album_url", ref responses);

					//if(counter < responses.Count - 1)
					//	nowPlaying.AlbumCoverUrlSmall = responses[counter].ToString().Substring(responses[counter].ToString().IndexOf("=") + 1);
					//counter++;

					nowPlaying.AlbumCoverUrlSmall = hashResponse["albumcover_small"].ToString();
					//nowPlaying.AlbumCoverUrlSmall = LookupResponse("albumcover_small", ref responses);

					//if(counter < responses.Count - 1)
					//	nowPlaying.AlbumCoverUrlMedium = responses[counter].ToString().Substring(responses[counter].ToString().IndexOf("=") + 1);
					//counter++;

					nowPlaying.AlbumCoverUrlMedium = hashResponse["albumcover_medium"].ToString();
					//nowPlaying.AlbumCoverUrlMedium = LookupResponse("albumcover_medium", ref responses);

					//if(counter < responses.Count - 1)
					//	nowPlaying.AlbumCoverUrlLarge = responses[counter].ToString().Substring(responses[counter].ToString().IndexOf("=") + 1);
					//counter++;

					nowPlaying.AlbumCoverUrlLarge = hashResponse["albumcover_large"].ToString();
					//nowPlaying.AlbumCoverUrlLarge= LookupResponse("albumcover_large", ref responses);

					//if(counter < responses.Count - 1)
					//	nowPlaying.TrackDuration = responses[counter].ToString().Substring(responses[counter].ToString().IndexOf("=") + 1);
					//counter++;

					nowPlaying.TrackDuration = hashResponse["trackduration"].ToString();
					//nowPlaying.TrackDuration = LookupResponse("trackduration", ref responses);

					//if(counter < responses.Count - 1)
					//	nowPlaying.TrackProgress = responses[counter].ToString().Substring(responses[counter].ToString().IndexOf("=") + 1);
					//counter++;

					nowPlaying.TrackProgress = hashResponse["trackprogress"].ToString();
					//nowPlaying.TrackProgress = LookupResponse("trackprogress", ref responses);

					//if(counter < responses.Count - 1)
					//	nowPlaying.RadioMode = responses[counter].ToString().Substring(responses[counter].ToString().IndexOf("=") + 1);
					//counter++;

					nowPlaying.RadioMode = hashResponse["radiomode"].ToString();
					//nowPlaying.RadioMode = LookupResponse("radiomode", ref responses);

					//if(counter < responses.Count - 1)
					//	nowPlaying.RecordToProfile = responses[counter].ToString().Substring(responses[counter].ToString().IndexOf("=") + 1);
					//counter++;

					nowPlaying.RecordToProfile = hashResponse["recordtoprofile"].ToString();
					//nowPlaying.RecordToProfile = LookupResponse("recordtoprofile", ref responses);

					success = true;
				}
			}

			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + ":" + ex.StackTrace, ex.Source);
				throw ex;
			}

			finally
			{
				//if(reader != null)
				//	reader.Close();

				//if(response != null)
				//	response.Close();
			}

			return success;
		}
		#endregion

		#region LookupResponse
		public Hashtable LookupResponse(string response)
		{
			//not getting back all the key/value pairs expected
			Hashtable responses = new Hashtable(17);
			Regex regex = new Regex("(?xmi:(?<key>[^=]*)=(?<value>[^\\n\\r]*)\\s+)", RegexOptions.Compiled | RegexOptions.Multiline);
			MatchCollection matches = regex.Matches(response);

			foreach (Match match in matches)
				if (match.Success)
					responses.Add(match.Groups["key"].Value, match.Groups["value"].Value);

			return responses;
		}

		/*public string LookupResponse(string key, ref System.Collections.ArrayList responses)
		{
			try
			{
				foreach(string response in responses)
				{
					if(response.Substring(0, response.IndexOf("=")).ToLower().Equals(key))
					{
						if(response.IndexOf("=") < response.Length)
							return response.Substring(response.IndexOf("=") + 1);
						else
							return string.Empty;
					}
				}
			}

			catch(Exception)
			{
				return string.Empty;
			}

			return string.Empty;
		}*/
		#endregion

		#region DeleteCoverArt
		public void DeleteCoverArt(string path)
		{
			try
			{
				FileInfo coverArtImage;

				coverArtImage = new FileInfo(path);

				if (coverArtImage.Exists)
				{
					coverArtImage.Delete();
					coverArtImage = null;
				}
			}
			catch (Exception)
			{
			}
		}

		public bool DownloadCoverArt(OldNowPlayingResponse nowPlaying)
		{
			bool success = true;
			WebClient web = new WebClient();

			try
			{
				//if (this._isDebuggingOn)
				//{
					//this._debugWindow.AddDebugEntry("DownloadFile", nowPlaying.AlbumCoverUrlSmall);
				//}

				DeleteCoverArt(this.resources["NOW_PLAYING_COVER_ART_SMALL_FILE_NAME"].ToString());

				web.DownloadFile(nowPlaying.AlbumCoverUrlSmall, this.resources["NOW_PLAYING_COVER_ART_SMALL_FILE_NAME"].ToString());
			}

			catch (Exception)
			{
				//if (this._isDebuggingOn)
				//{
					//this._debugWindow.AddDebugEntry("DownloadFileFailure", ex.Message);
				//}

				success = false;
			}

			try
			{
				//if (this._isDebuggingOn)
				//{
					//this._debugWindow.AddDebugEntry("DownloadFile", nowPlaying.AlbumCoverUrlMedium);
				//}

				DeleteCoverArt(this.resources["NOW_PLAYING_COVER_ART_MEDIUM_FILE_NAME"].ToString());

				web.DownloadFile(nowPlaying.AlbumCoverUrlMedium, this.resources["NOW_PLAYING_COVER_ART_MEDIUM_FILE_NAME"].ToString());
			}

			catch (Exception)
			{
				//if (this._isDebuggingOn)
				//{
					//this._debugWindow.AddDebugEntry("DownloadFileFailure", ex.Message);
				//}

				success = false;
			}

			try
			{
				//if (this._isDebuggingOn)
				//{
					//this._debugWindow.AddDebugEntry("DownloadFile", nowPlaying.AlbumCoverUrlLarge);
				//}

				DeleteCoverArt(this.resources["NOW_PLAYING_COVER_ART_LARGE_FILE_NAME"].ToString());

				web.DownloadFile(nowPlaying.AlbumCoverUrlLarge, this.resources["NOW_PLAYING_COVER_ART_LARGE_FILE_NAME"].ToString());
			}

			catch (Exception)
			{
				//if (this._isDebuggingOn)
				//{
					//this._debugWindow.AddDebugEntry("DownloadFileFailure", ex.Message);
				//}

				success = false;
			}

			return success;
		}
		#endregion

		#region WritePlaylist
		public bool WritePlaylist(OldLoginResponse lastFmResponse)
		{
			bool success = false;
			StreamWriter writer;

			try
			{
				FileInfo playlistFile = new FileInfo(this.resources["PLAYLIST_FILE_NAME"].ToString());
				writer = playlistFile.CreateText();
				string plsFile = this.resources["WINDOWS_MEDIA_PLAYER_TEMPLATE"].ToString();
				plsFile = string.Format(plsFile, lastFmResponse.StreamUrl);
				writer.WriteLine(plsFile);
				writer.Close();

				FileInfo wmpFile = new FileInfo(this.resources["WINDOWS_MEDIA_PLAYER_FILE_NAME"].ToString());
				writer = wmpFile.CreateText();
				string asxFile = this.resources["WINDOWS_MEDIA_PLAYER_TEMPLATE"].ToString();
				asxFile = string.Format(asxFile, lastFmResponse.StreamUrl);
				writer.WriteLine(asxFile);
				writer.Close();

				success = true;
			}

			catch (Exception ex)
			{
				throw new Exception("An error occurred in LastFm.WritePlaylist().", ex);
			}

			return success;
		}
		#endregion
		
		#region Login
		public bool Login(string userName, string password)
		{
			bool success = false;
			OldLoginResponse response = null;

			/*
			if (this._settings.SaveUsername && !this._userChanged)
			{
				response = new LoginResponse(
					string.Empty,
					string.Empty,
					this._settings.Username,
					this._settings.PasswordMd5);
			}
			*/

			if (this.ProcessLogin(userName, password, ref response))
			{
				success = true;
				
				/*
				this._settings.Username = txtUsername.Text;
				this._settings.PasswordMd5 = response.PasswordMd5;
				
				if (!this._settings.SerializeSettings(this.resources["SETTINGS_FILE_NAME"].ToString()))
				{
					MessageBox.Show(this, "I could not save your settings. Please make sure you have write " +
						"access to the folder where myLastFM is installed. The application will run perfectly " +
						"fine without saving settings, however any changes to the configuration may not be saved.",
						"Save Settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}


				if (this.WritePlaylist(response))
				{
					frmMyLastFM lastFmWindow = new frmMyLastFM();

					lastFmWindow.LoginResponse = response;
					lastFmWindow.Settings = this._settings;
					lastFmWindow.Show();

					this.Hide();
				}
				else
				{
					MessageBox.Show(this, "A playlist could not be created. Please make sure that you have write permissions to the folder where myLastFM is installed.",
						"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				*/
			}
			return success;
		}
		#endregion
		
		#endregion
	}
}
