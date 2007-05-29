using System;

namespace Alexandria.LastFM.LastRipper
{

	public class PlayListX : System.Collections.ArrayList
	{
		protected System.String MusicPath;

		///<summary>
		///
		///</summary>
		public PlayListX(System.String MusicPath)
		{
			this.MusicPath = MusicPath;
		}

		///<summary>
		///Adds an object to this collection, returns -1 if the value already exist.
		///</summary>
		public override System.Int32 Add(System.Object Value)
		{
			if (!this.Contains(Value))
			{
				return base.Add(Value);
			}
			else
			{
				return -1;
			}
		}

		public override void AddRange(System.Collections.ICollection C)
		{
			foreach (System.Object Value in C)
			{
				this.Add(Value);
			}
		}

		///<summary>
		///
		///</summary>
		public virtual void Randomize()
		{
			System.Random Rnd = new System.Random();
			System.Collections.SortedList List = new System.Collections.SortedList();
			foreach (System.Object Value in this)
			{
				List.Add(Rnd.Next(), Value);
			}
			this.Clear();
			this.AddRange(List.Values);
		}

		public void DownloadXML(System.String XmlURL)
		{
			System.String TempFile = PathSettings.TempPath + "TheLastRipperData.xml";
			if (System.IO.File.Exists(TempFile))
			{
				System.IO.File.Delete(TempFile);
			}
			System.Net.WebClient Client = new System.Net.WebClient();
			Client.DownloadFile(XmlURL, TempFile);
			this.FetchXML(TempFile);
		}

		///<summary>
		///
		///</summary>
		protected virtual void FetchXML(System.String XmlFile)
		{
			System.Xml.XmlDocument XmlDoc = new System.Xml.XmlDocument();
			XmlDoc.Load(XmlFile);
			this.FetchXML(XmlDoc);
		}
		protected virtual void FetchXML(System.Xml.XmlDocument XmlDoc)
		{
			foreach (System.Xml.XmlNode Node in XmlDoc.DocumentElement.ChildNodes)
			{
				IMetaTrack Track = new MetaTrack(Node.SelectSingleNode("name").InnerText, Node.SelectSingleNode("artist").InnerText);

				//If the track exist, cast it to MetaMusic and add it to the playlist
				if (this.IsAvailable(ref Track))
				{
					this.Add((MetaMusic)Track);
				}
			}
		}

		///<summary>
		///Returns true the track is available, and the IMetaTrack would be castable to IMetaMusic
		///</summary>
		protected virtual System.Boolean IsAvailable(ref IMetaTrack Track)
		{
			System.String TrackPath = "";
			System.String ArtistDir = this.MusicPath + System.IO.Path.DirectorySeparatorChar + LastManager.RemoveIllegalChars(Track.Artist);
			if (System.IO.Directory.Exists(ArtistDir))
			{
				foreach (System.String Directory in System.IO.Directory.GetDirectories(ArtistDir))
				{
					TrackPath = Directory + System.IO.Path.DirectorySeparatorChar + LastManager.RemoveIllegalChars(Track.Track) + ".mp3";
					if (System.IO.File.Exists(TrackPath))
					{
						Track = new MetaMusic(TrackPath);
						return true;
					}
				}
			}
			return false;
		}

		public virtual void Save(System.String FileName, PlayListType FileType)
		{
			System.IO.FileStream File = System.IO.File.Create(this.MusicPath + System.IO.Path.DirectorySeparatorChar + FileName);
			System.Text.ASCIIEncoding Encoder = new System.Text.ASCIIEncoding();
			System.String Content;
			switch (FileType)
			{
				case PlayListType.pls:
					Content = this.GetPLS();
					break;
				case PlayListType.m3u:
					Content = this.GetM3U();
					break;
				default:
					Content = this.GetSMIL();
					break;
			}
			File.Write(Encoder.GetBytes(Content), 0, Encoder.GetByteCount(Content));
			File.Flush();
			File.Close();
		}

		protected virtual System.String GetM3U()
		{
			System.String PlayList = "";
			foreach (IMetaMusic Number in this)
			{
				PlayList += Number.Artist + System.IO.Path.DirectorySeparatorChar + Number.Album + System.IO.Path.DirectorySeparatorChar + Number.Track + ".mp3\n";
			}
			return PlayList;
		}

		protected virtual System.String GetPLS()
		{
			System.String PlayList = "[playlist]\nnumberofentries=" + this.Count + "\n";
			System.Int32 i = 1;
			foreach (IMetaMusic Number in this)
			{
				PlayList += "File" + i + "=" + Number.Artist + System.IO.Path.DirectorySeparatorChar + Number.Album + System.IO.Path.DirectorySeparatorChar + Number.Track + ".mp3\n";
				PlayList += "Title" + i + "=" + Number.ToString() + "\n";
				PlayList += "Length" + i + "=" + Number.Trackduration + "\n";
				i += 1;
			}
			PlayList += "Version=2";

			return PlayList;
		}

		protected virtual System.String GetSMIL()
		{
			System.String PlayList = "<smil>\n<body>\n<seq>\n";
			foreach (IMetaMusic Number in this)
			{
				PlayList += "<audio src=\"" + Number.Artist + System.IO.Path.DirectorySeparatorChar + Number.Album + System.IO.Path.DirectorySeparatorChar + Number.Track + ".mp3\"/>\n";
			}
			return PlayList + "</seq>\n</body>\n</smil>";
		}
	}

	public enum PlayListType
	{
		pls,
		m3u,
		smil
	}
}
