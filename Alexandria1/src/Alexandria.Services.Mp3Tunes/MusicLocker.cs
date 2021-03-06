#region License (LGPL)
/*
 * Copyright (C) 2005-2006 MP3tunes, LLC
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/
#endregion

using System;
using System.IO;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using System.Text;
using System.Collections.Generic;
using System.Globalization;

using Telesophy.Alexandria.Model;

namespace Telesophy.Alexandria.Mp3Tunes
{
	public class MusicLocker
	{
		#region Constructors
		public MusicLocker()
		{
		}
		#endregion
	
		#region Private Constants
		private const string PARTNER_TOKEN = "2036856440";
		private const string GET_URL = "http://content.mp3tunes.com";
		#endregion
	
		#region Private Fields
		private bool isLoggedIn;
		private string sessionId;
		#endregion

		#region Private Methods
		private string Request(string strURL, string strParams)
		{
			HttpWebRequest req =
				(HttpWebRequest)WebRequest.Create(strURL + strParams);
			req.KeepAlive = false;

			req.UserAgent = "MP3tunes.NET 1.0";

			HttpWebResponse res = (HttpWebResponse)req.GetResponse();

			Stream rstr = res.GetResponseStream();
			StreamReader reader = new StreamReader(rstr);
			string content = reader.ReadToEnd();

			res.Close();

			return content;
		}		
		#endregion

		#region Public Properties
		public bool IsLoggedIn
		{
			get { return isLoggedIn; }
		}
		
		public string PartnerToken
		{
			get { return PARTNER_TOKEN; }
		}
		
		public string SessionId
		{
			get
			{
				//TODO: figure out a better way to automate this
				//if (!isLoggedIn)
					//Login("dan.poage@gmail.com", "automatic");
					
				return sessionId;
			}
		}
		#endregion

		#region Public Methods
		
		#region Login
		public void Login(string username, string password)
		{
			try
			{				
				XmlNode status;
				XmlNode sessionIdNode;
				XmlNode errorMessage;
				XmlDocument doc = new XmlDocument();

				string safeUsername = HttpUtility.UrlEncode(username);			
				string safePassword = HttpUtility.UrlEncode(password);
				string parameters = String.Format("?output=xml&username={0}&password={1}", safeUsername, safePassword);

				string request = Request("https://shop.mp3tunes.com/api/v0/login", parameters);
				doc.LoadXml(request);

				status = doc.SelectSingleNode("mp3tunes/status");
				sessionIdNode = doc.SelectSingleNode("mp3tunes/session_id");
				errorMessage = doc.SelectSingleNode("mp3tunes/errorMessage");

				if (status != null && status.InnerXml == "1" && sessionIdNode != null && sessionIdNode.InnerXml != "null")				
					this.sessionId = sessionIdNode.InnerXml;
				else if (errorMessage != null && errorMessage.InnerXml != "null")			
					throw new ApplicationException(errorMessage.InnerXml.ToString());
				else throw new ApplicationException("Wrong username or password.");
				
				isLoggedIn = true;
			}
			catch (Exception ex)
			{
				throw new ApplicationException("There was an error logging in to MP3tunes", ex);
			}
		}
		#endregion

		#region GetTracks
		public List<IMediaItem> GetTracks(bool ignoreCache)
		{
			List<IMediaItem> tracks = new List<IMediaItem>();
			XmlDocument doc = new XmlDocument();
			
			string dirPath = string.Format("{0}Alexandria{1}MP3tunes{1}", Path.GetTempPath(), Path.DirectorySeparatorChar);
			if (!Directory.Exists(dirPath))
				Directory.CreateDirectory(dirPath);
				
			string xmlPath = string.Format("{0}tracks.xml", dirPath);
			
			if (ignoreCache || !File.Exists(xmlPath))
			{
				string request;
				string parameters = String.Format("?output=xml&sid={0}&type=track",
				HttpUtility.UrlEncode(SessionId));

				request = Request("http://www.mp3tunes.com/api/v0/lockerData", parameters);
				
				doc.LoadXml(request);
				doc.Save(xmlPath);
			}
			else
			{
				doc.Load(xmlPath);
			}
			
			
			XPathNavigator nav = doc.CreateNavigator();
			XPathNodeIterator iter = nav.Select("/mp3tunes/trackList/item");

			while (iter.MoveNext())
			{
				try
				{
					XmlNode node = ((IHasXmlNode)iter.Current).GetNode();
					XmlNode trackIdNode = node.SelectSingleNode( "trackId" ); //This was originally commented out
					XmlNode trackTitle = node.SelectSingleNode("trackTitle");
					XmlNode trackNumberNode = node.SelectSingleNode("trackNumber");
					XmlNode trackLength = node.SelectSingleNode("trackLength");
					XmlNode trackFileName = node.SelectSingleNode( "trackFileName" );
					//XmlNode trackFileKey = node.SelectSingleNode( "trackFileKey" );
					XmlNode downloadUrl = node.SelectSingleNode("downloadURL");
					XmlNode albumTitle = node.SelectSingleNode("albumTitle");
					XmlNode albumYear = node.SelectSingleNode( "albumYear" ); //This was originally commented out
					XmlNode artistName = node.SelectSingleNode("artistName");				

					IMetadataIdentifier trackId;
					string trackIdValue = string.Empty;					
					if (trackIdNode != null && !string.IsNullOrEmpty(trackIdNode.InnerXml))
						trackIdValue = trackIdNode.InnerXml;

					string url = string.Format("{0}{1}", System.Web.HttpUtility.UrlDecode(downloadUrl.InnerXml), PARTNER_TOKEN);
					
					//HACK: fix this later
					url = url.Replace("&amp;", "&");
					Uri path = new Uri(url);
					
					//string.Format("{0}{1}?sid={2}&partner_token={3}", get_url, downloadUrl.InnerXml, session_id, partner_token);
					//Uri uri = new Uri(url);
					//ILocation location = new Location(uri);
					System.Diagnostics.Debug.WriteLine("xml=" + downloadUrl.InnerXml);
					System.Diagnostics.Debug.WriteLine("url=" + url);
					//System.Diagnostics.Debug.WriteLine("loc=" + location.Path);

					int trackNumber = 0;
					if (trackNumberNode != null && !string.IsNullOrEmpty(trackNumberNode.InnerXml))
					{
						try
						{
							trackNumber = Convert.ToInt32(trackNumberNode.InnerXml);
						}
						catch (FormatException)
						{
							trackNumber = -1;
						}
					}

					TimeSpan duration = TimeSpan.Zero;
					if (trackLength != null && !string.IsNullOrEmpty(trackLength.InnerXml))
					{
						try
						{
							duration = new TimeSpan(Convert.ToInt64(Convert.ToDouble(trackLength.InnerXml)) * 10000);
						}
						catch (FormatException)
						{
							duration = TimeSpan.Zero;
						}
					}
					
					//TODO: cleanup this mess
					//long releaseDateFileTime = 0;					
					DateTime releaseDate = new DateTime(1900, 1, 1);
					//long defaultReleaseDateTime = releaseDate.ToFileTime();
					if (albumYear != null && !string.IsNullOrEmpty(albumYear.InnerXml))
					{
						try
						{
							releaseDate = new DateTime(Convert.ToInt32(albumYear.InnerXml), 1, 1);
							//releaseDateFileTime = releaseDate.ToFileTime();
						}
						catch
						{
							releaseDate = DateTime.MinValue;
						}
						
						//catch (FormatException)
						//{
							//releaseDate = new DateTime(1900, 1, 1);
							//releaseDateFileTime = defaultReleaseDateTime;
						//}
						//catch(ArgumentOutOfRangeException)
						//{
							//releaseDateFileTime = defaultReleaseDateTime;
						//}
					}						
						
					string format = string.Empty;
					if (!string.IsNullOrEmpty(trackFileName.InnerXml))					
					{
						FileInfo fileInfo = new FileInfo(trackFileName.InnerXml);
						format = fileInfo.Extension.Remove(0, 1);
					}

					//Uri originalPath = new Uri(trackFileName.InnerXml);
					//Album album = new Album(location, albumTitle.InnerXml, artistName.InnerXml, releaseDate);
					
					Guid id = Guid.NewGuid();
					IMediaItem track = GetTrack(id, trackNumber, trackTitle.InnerXml, artistName.InnerXml, albumTitle.InnerXml, duration, releaseDate, format, path);
					//id, url, trackTitle.InnerXml, albumTitle.InnerXml, artistName.InnerXml, duration, releaseDateFileTime, trackNumber, format);
					//Track realTrack = (Track)track;
					//realTrack.AdditionalInfo = new TrackAdditionalInfo(Guid.NewGuid(), trackFileName.InnerXml);
					//realTrack.AdditionalInfo.Parent = track;

					//Track track = new Track(id, new Uri(url), trackTitle.InnerXml, albumTitle.InnerXml, artistName.InnerXml, duration, releaseDate, trackNumber, format, originalPath);
					trackId = TrackIdFactory.CreateTrackId(trackIdValue);
					//track.MetadataIdentifiers.Add(trackId);

					tracks.Add(track);
				}
				catch (Exception ex)
				{
					throw new ApplicationException("There was an error loading a track from this MP3tunes locker: " + ex.Message, ex);
				}
			}

			return tracks;
		}
		#endregion
		
		#region GetTrack
		public IMediaItem GetTrack(Guid id, int number, string title, string artist, string album, TimeSpan duration, DateTime date, string format, Uri path)
		{
			return new Mp3TunesTrack(id, number, title, artist, album, duration, date, format, path);
		}
		#endregion

		#region GetLockerPath
		public Uri GetLockerPath(string path)
		{
			if (!string.IsNullOrEmpty(path))
			{
				string root = path.Substring(0, path.IndexOf('?'));
				string fullPath = string.Format("{0}?sid={1}&partner_token={2}", root, SessionId, PARTNER_TOKEN);
				return new Uri(fullPath);
			}
			else return null;
		}
		#endregion
		
		#endregion
	}
}

