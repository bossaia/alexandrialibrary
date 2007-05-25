using System;

namespace Alexandria.LastFM.LastRipper
{
	[SerializableAttribute]
	public class PlayListGenerator : System.Runtime.Serialization.ISerializable
	{
		protected System.String _MusicPath;
		protected System.String _UserName;

		protected PlayListGenerator() { }

		public PlayListGenerator(System.String MusicPath, System.String UserName)
		{
			this._MusicPath = MusicPath;
			this._UserName = UserName;
		}

		public System.Boolean TopTracks = true;
		public System.Boolean RecentLovedTracks = true;
		public System.Boolean WeeklyTrackChart = true;

		public System.Boolean TopTracksMixed = true;
		public System.Boolean RecentLovedTracksMixed = true;
		public System.Boolean WeeklyTrackChartMixed = true;

		public System.Boolean Mixed = true;

		public System.Boolean m3u = true;
		public System.Boolean pls = true;
		public System.Boolean smil = true;

		public virtual void Generate()
		{
			PlayList List1;
			PlayList List2;
			PlayList List3;

			List1 = this.GeneratePlayList("http://ws.audioscrobbler.com/1.0/user/" + this._UserName + "/toptracks.xml", this.TopTracks, this.TopTracksMixed, "TopTracks");
			List2 = this.GeneratePlayList("http://ws.audioscrobbler.com/1.0/user/" + this._UserName + "/recentlovedtracks.xml", this.RecentLovedTracks, this.RecentLovedTracksMixed, "RecentLovedTracks");
			List3 = this.GeneratePlayList("http://ws.audioscrobbler.com/1.0/user/" + this._UserName + "/weeklytrackchart.xml", this.WeeklyTrackChart, this.WeeklyTrackChartMixed, "WeeklyTrackChart");

			List1.AddRange(List2);
			List1.AddRange(List3);
			List1.Randomize();
			this.SavePlayList(List1, "Mixed");
		}

		protected virtual PlayList GeneratePlayList(System.String Feed, System.Boolean Clean, System.Boolean Mixed, System.String FileName)
		{
			PlayList List = new PlayList(this._MusicPath);

			List.DownloadXML(Feed);

			if (Clean)
			{
				this.SavePlayList(List, FileName);
			}
			if (Mixed)
			{
				List.Randomize();
				this.SavePlayList(List, FileName + "Mixed");
			}

			return List;
		}

		protected virtual void SavePlayList(PlayList List, System.String FileName)
		{
			if (this.m3u)
			{
				List.Save(FileName + ".m3u", PlayListType.m3u);
			}
			if (this.pls)
			{
				List.Save(FileName + ".pls", PlayListType.pls);
			}
			if (this.smil)
			{
				List.Save(FileName + ".smil", PlayListType.smil);
			}
		}

		protected PlayListGenerator(System.Runtime.Serialization.SerializationInfo Info, System.Runtime.Serialization.StreamingContext context)
		{
			if (Info == null)
			{
				throw new System.ArgumentNullException("Info");
			}
			this.TopTracks = (System.Boolean)Info.GetValue("TopTracks", typeof(System.Boolean));
			this.RecentLovedTracks = (System.Boolean)Info.GetValue("RecentLovedTracks", typeof(System.Boolean));
			this.WeeklyTrackChart = (System.Boolean)Info.GetValue("WeeklyTrackChart", typeof(System.Boolean));

			this.TopTracksMixed = (System.Boolean)Info.GetValue("TopTracksMixed", typeof(System.Boolean));
			this.RecentLovedTracksMixed = (System.Boolean)Info.GetValue("RecentLovedTracksMixed", typeof(System.Boolean));
			this.WeeklyTrackChartMixed = (System.Boolean)Info.GetValue("WeeklyTrackChartMixed", typeof(System.Boolean));

			this.Mixed = (System.Boolean)Info.GetValue("Mixed", typeof(System.Boolean));

			this.m3u = (System.Boolean)Info.GetValue("m3u", typeof(System.Boolean));
			this.pls = (System.Boolean)Info.GetValue("pls", typeof(System.Boolean));
			this.smil = (System.Boolean)Info.GetValue("smil", typeof(System.Boolean));

			this._MusicPath = (System.String)Info.GetValue("MusicPath", typeof(System.String));
			this._UserName = (System.String)Info.GetValue("UserName", typeof(System.String));
		}

		public virtual void GetObjectData(System.Runtime.Serialization.SerializationInfo Info, System.Runtime.Serialization.StreamingContext context)
		{
			if (Info == null)
			{
				throw new System.ArgumentNullException("Info");
			}
			Info.AddValue("TopTracks", this.TopTracks);
			Info.AddValue("RecentLovedTracks", this.RecentLovedTracks);
			Info.AddValue("WeeklyTrackChart", this.WeeklyTrackChart);

			Info.AddValue("TopTracksMixed", this.TopTracksMixed);
			Info.AddValue("RecentLovedTracksMixed", this.RecentLovedTracksMixed);
			Info.AddValue("WeeklyTrackChartMixed", this.WeeklyTrackChartMixed);

			Info.AddValue("Mixed", this.Mixed);

			Info.AddValue("m3u", this.m3u);
			Info.AddValue("pls", this.pls);
			Info.AddValue("smil", this.smil);

			Info.AddValue("MusicPath", this._MusicPath);
			Info.AddValue("UserName", this._UserName);
		}
	}
}