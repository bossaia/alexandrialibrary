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
	
		#region Private Fields
		private string session_id;
		private const string partner_token = "2036856440";
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
			string rs;
			XmlNode status;
			XmlNode session_id;
			XmlNode errorMessage;
			XmlDocument doc = new XmlDocument();

			string strParams = String.Format("?output=xml&username={0}" +
				"&password={1}", HttpUtility.UrlEncode(username),
				HttpUtility.UrlEncode(password));

			rs = Request("https://shop.mp3tunes.com/api/v0/login", strParams);
			doc.LoadXml(rs);

			status = doc.SelectSingleNode("mp3tunes/status");
			session_id = doc.SelectSingleNode("mp3tunes/session_id");
			errorMessage = doc.SelectSingleNode("mp3tunes/errorMessage");

			if (status != null && status.InnerXml == "1" &&
				session_id != null && session_id.InnerXml != "null")
			{
				this.session_id = session_id.InnerXml;
			}
			else if (errorMessage != null && errorMessage.InnerXml != "null")
			{
				throw new AuthenticationException((string)
												   errorMessage.InnerXml);
			}
			else
			{
				throw new AuthenticationException("Wrong username or " +
												   "password.");
			}
		}
		#endregion

		#region GetTracks
		public List<IAudioTrack> GetTracks()
		{
			string rs;
			List<IAudioTrack> tracks = new List<IAudioTrack>();
			
			XmlDocument doc = new XmlDocument();

			string strParams = String.Format("?output=xml&sid={0}&type=track",
				HttpUtility.UrlEncode(this.session_id));

			rs = Request("http://www.mp3tunes.com/api/v0/lockerData",
						  strParams);

			doc.LoadXml(rs);
			XPathNavigator nav = doc.CreateNavigator();
			XPathNodeIterator iter = nav.Select("/mp3tunes/trackList/item");

			while (iter.MoveNext())
			{
				XmlNode node = ((IHasXmlNode)iter.Current).GetNode();
				//XmlNode trackId = node.SelectSingleNode( "trackId" );
				XmlNode trackTitle = node.SelectSingleNode("trackTitle");
				XmlNode trackNumber = node.SelectSingleNode("trackNumber");
				XmlNode trackLength = node.SelectSingleNode("trackLength");
				//XmlNode trackFileName = node.SelectSingleNode( "trackFileName" );
				//XmlNode trackFileKey = node.SelectSingleNode( "trackFileKey" );
				XmlNode downloadURL = node.SelectSingleNode("downloadURL");
				XmlNode albumTitle = node.SelectSingleNode("albumTitle");
				  XmlNode albumYear = node.SelectSingleNode( "albumYear" );
				XmlNode artistName = node.SelectSingleNode("artistName");

				UriLocation location = new UriLocation(downloadURL.InnerXml);
				Artist artist = new Artist(null, null, artistName.InnerXml, false, DateTime.MinValue, DateTime.MinValue);

				int number = 0;
				if (trackNumber != null && !string.IsNullOrEmpty(trackNumber.InnerXml))
					number = Convert.ToInt32(trackNumber.InnerXml);

				TimeSpan length = new TimeSpan(Convert.ToInt64(Convert.ToDouble(trackLength.InnerXml)) * 10000);

				DateTime releaseDate = Convert.ToDateTime(albumYear); //NOTE: this may not always work...

				Album album = new Album(null, null, albumTitle.InnerXml, releaseDate, false);

				Track track = new Track(null, location, trackTitle.InnerXml, number, length, releaseDate, album, null);
				//tr.Uri = new Uri(downloadURL.InnerXml);
				//tr.Artist = artistName.InnerXml;
				//tr.Album = albumTitle.InnerXml;
				//tr.Title = trackTitle.InnerXml;
				//tr.Duration = new TimeSpan(Convert.ToInt64(Convert.ToDouble(trackLength.InnerXml)) * 10000);
				//if (trackNumber != null &&
					//trackNumber.InnerXml != "")
				//{
					//tr.TrackNumber = Convert.ToUInt32(trackNumber.InnerXml);
				//}
				tracks.Add(track);
			}

			return tracks;
		}
		#endregion
		
		#endregion
	}
}

