/***************************************************************************
 *  Queue.cs
 *
 *  Copyright (C) 2005-2006 Novell, Inc.
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
using Mono.Security.Cryptography;
using System.Collections;
using System.Web;
using System.Xml;

//using GLib;
//using Banshee.MediaEngine;
//using Banshee.Base;
//using Banshee;

namespace AlexandriaOrg.Alexandria.LastFM
{
	public class LastFMQueue
	{
		#region Constructors
		public LastFMQueue()
		{
			xml_path = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AudioscrobblerQueue.xml");
			//Paths.UserPluginDirectory, "AudioscrobblerQueue.xml");
			queue = new ArrayList();

			Load();
		}
		#endregion

		#region Private Fields
		private ArrayList queue;
		private string xml_path;
		private bool dirty;
		#endregion
		
		#region Private Event Methods
		private void RaiseTrackAdded(object o, EventArgs args)
		{
			EventHandler handler = TrackAdded;
			if (handler != null)
				handler(o, args);
		}
		#endregion
		
		#region Public Fields
		public event EventHandler TrackAdded;
		#endregion

		#region Public Properties
		
		public int Count
		{
			get { return queue.Count; }
		}
		
		#endregion

		#region Public Methods
		
		#region Save
		public void Save()
		{
			if (!dirty)
				return;

			XmlTextWriter writer = new XmlTextWriter(xml_path, System.Text.Encoding.Default);

			writer.Formatting = Formatting.Indented;
			writer.Indentation = 4;
			writer.IndentChar = ' ';

			writer.WriteStartDocument(true);

			writer.WriteStartElement("AudioscrobblerQueue");
			foreach (QueuedTrack track in queue)
			{
				writer.WriteStartElement("QueuedTrack");
				writer.WriteElementString("Artist", track.Artist);
				writer.WriteElementString("Album", track.Album);
				writer.WriteElementString("Title", track.Title);
				writer.WriteElementString("MusicBrainzId", track.MusicBrainzId);
				writer.WriteElementString("Duration", track.Duration.ToString());
				writer.WriteElementString("StartTime", DateTimeUtil.ToTimeT(track.StartTime).ToString());
				writer.WriteEndElement(); // Track
			}
			writer.WriteEndElement(); // AudioscrobblerQueue
			writer.WriteEndDocument();
			writer.Close();
		}
		#endregion

		#region Load
		public void Load()
		{
			queue.Clear();

			try
			{
				string query = "//AudioscrobblerQueue/QueuedTrack";
				XmlDocument doc = new XmlDocument();

				doc.Load(xml_path);
				XmlNodeList nodes = doc.SelectNodes(query);

				foreach (XmlNode node in nodes)
				{
					string artist = string.Empty;
					string album = string.Empty;
					string title = string.Empty;
					string musicBrainzId = string.Empty;
					int duration = 0;
					DateTime start_time = new DateTime(0);

					foreach (XmlNode child in node.ChildNodes)
					{
						if (child.Name == "Artist" && child.ChildNodes.Count != 0)
						{
							artist = child.ChildNodes[0].Value;
						}
						else if (child.Name == "Album" && child.ChildNodes.Count != 0)
						{
							album = child.ChildNodes[0].Value;
						}
						else if (child.Name == "Title" && child.ChildNodes.Count != 0)
						{
							title = child.ChildNodes[0].Value;
						}
						else if (child.Name == "Duration" && child.ChildNodes.Count != 0)
						{
							duration = Convert.ToInt32(child.ChildNodes[0].Value);
						}
						// THIS SECTION MAY NEED TO BE REMOVED
						else if (child.Name == "MusicBrainzId" && child.ChildNodes.Count != 0)
						{
							musicBrainzId = child.ChildNodes[0].Value;
						}
						else if (child.Name == "StartTime" && child.ChildNodes.Count != 0)
						{
							long time = Convert.ToInt64(child.ChildNodes[0].Value);
							start_time = DateTimeUtil.FromTimeT(time);
						}
					}

					queue.Add(new QueuedTrack(artist, album, title, musicBrainzId, duration, start_time));
				}
			}
			catch
			{
			}
		}
		#endregion

		#region GetTransmitInfo
		public string GetTransmitInfo(out int num_tracks)
		{
			StringBuilder sb = new StringBuilder();

			int i;
			for (i = 0; i < queue.Count; i++)
			{
				/* we queue a maximum of 10 tracks per request */
				// For testing purposes queue only 1 track
				// normal check here is: i == 9
				if (i > 0) break;

				QueuedTrack track = (QueuedTrack)queue[i];

				sb.AppendFormat(
						 "&a[{6}]={0}&t[{6}]={1}&b[{6}]={2}&m[{6}]={3}&l[{6}]={4}&i[{6}]={5}",
						 HttpUtility.UrlEncode(track.Artist),
						 HttpUtility.UrlEncode(track.Title),
						 HttpUtility.UrlEncode(track.Album),
						 string.Empty, //track.MusicBrainzId,
						 track.Duration.ToString(),
						 HttpUtility.UrlEncode(track.StartTime.ToString("yyyy-MM-dd HH:mm:ss")),
						 i);
			}

			num_tracks = i;
			return sb.ToString();
		}
		#endregion

		#region Add
		public void Add(string artist, string album, string title, string musicBrainzId,int duration, DateTime start_time)		
		{
			queue.Add(new QueuedTrack(artist, album, title, musicBrainzId, duration, start_time));
			dirty = true;
			RaiseTrackAdded(this, new EventArgs());
		}
		#endregion

		#region RemoveRange
		public void RemoveRange(int first, int count)
		{
			queue.RemoveRange(first, count);
			dirty = true;
		}
		#endregion
		
		#endregion
	}

}
