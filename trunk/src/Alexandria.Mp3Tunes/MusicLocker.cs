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

using System;
using System.IO;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using System.Text;
using System.Collections.Generic;
using System.Globalization;

namespace Alexandria.Mp3Tunes
{
	public class MusicLocker
	{
		#region Constructors
		public MusicLocker()
		{
		}
		#endregion
	
		#region Private Constants
		private const string partner_token = "2036856440";
		private const string get_url = "http://content.mp3tunes.com";
		#endregion
	
		#region Private Fields
		private string session_id;		
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
		//public string PartnerToken
		//{
			//get { return partner_token; }			
		//}
		#endregion

		#region Public Methods
		
		#region Login
		public void Login(string username, string password)
		{
			try
			{				
				XmlNode status;
				XmlNode session_id;
				XmlNode errorMessage;
				XmlDocument doc = new XmlDocument();

				string safeUsername = HttpUtility.UrlEncode(username);			
				string safePassword = HttpUtility.UrlEncode(password);
				string parameters = String.Format("?output=xml&username={0}&password={1}", safeUsername, safePassword);

				string request = Request("https://shop.mp3tunes.com/api/v0/login", parameters);
				doc.LoadXml(request);

				status = doc.SelectSingleNode("mp3tunes/status");
				session_id = doc.SelectSingleNode("mp3tunes/session_id");
				errorMessage = doc.SelectSingleNode("mp3tunes/errorMessage");

				if (status != null && status.InnerXml == "1" && session_id != null && session_id.InnerXml != "null")			
					this.session_id = session_id.InnerXml;			
				else if (errorMessage != null && errorMessage.InnerXml != "null")			
					throw new AuthenticationException(errorMessage.InnerXml.ToString());			
				else
					throw new AuthenticationException("Wrong username or password.");
			}
			catch (Exception ex)
			{
				throw new ApplicationException("There was an error logging in to MP3tunes", ex);
			}
		}
		#endregion

		#region GetTracks
		public List<IAudioTrack> GetTracks(bool ignoreCache)
		{
			List<IAudioTrack> tracks = new List<IAudioTrack>();
			XmlDocument doc = new XmlDocument();
			
			string dirPath = string.Format("{0}Alexandria{1}MP3tunes{1}", Path.GetTempPath(), Path.DirectorySeparatorChar);
			if (!Directory.Exists(dirPath))
				Directory.CreateDirectory(dirPath);
				
			string xmlPath = string.Format("{0}tracks.xml", dirPath);
			
			if (ignoreCache || !File.Exists(xmlPath))
			{
				string request;
				string parameters = String.Format("?output=xml&sid={0}&type=track",
				HttpUtility.UrlEncode(this.session_id));

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
						
					trackId = TrackIdFactory.CreateTrackId(trackIdValue);

					string url = string.Format("{0}{1}", System.Web.HttpUtility.UrlDecode(downloadUrl.InnerXml), partner_token);
					
					//HACK: fix this later
					url = url.Replace("&amp;", "&");
					
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
					
					DateTime releaseDate = DateTime.MinValue;
					if (albumYear != null && !string.IsNullOrEmpty(albumYear.InnerXml))
					{
						try
						{
							releaseDate = Convert.ToDateTime("1/1/" + albumYear.InnerXml);
						}
						catch (FormatException)
						{
							releaseDate = DateTime.MinValue;
						}
					}						
						
					string format = string.Empty;
					if (!string.IsNullOrEmpty(trackFileName.InnerXml))					
					{
						FileInfo fileInfo = new FileInfo(trackFileName.InnerXml);
						format = fileInfo.Extension.Remove(0, 1);
					}

					//Album album = new Album(location, albumTitle.InnerXml, artistName.InnerXml, releaseDate);
					
					string id = Guid.NewGuid().ToString();
					Track track = new Track(id, url, trackTitle.InnerXml, albumTitle.InnerXml, artistName.InnerXml, duration, releaseDate, trackNumber, format);
					track.MetadataIdentifiers.Add(trackId);

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
		
		#endregion
	}
}

