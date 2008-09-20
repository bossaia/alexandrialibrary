using System;
using System.IO;

namespace Alexandria.LastFM.LastRipper
{
	public class MetaMusic : IMetaMusic
	{

		public MetaMusic(System.String FileURL)
		{
			FileInfo MusicInfo = new FileInfo(FileURL);

			this._TrackDuration = (MusicInfo.Length / 128).ToString();
			this._Artist = MusicInfo.Directory.Parent.Name;
			this._Album = MusicInfo.Directory.Name;
			this._Track = MusicInfo.Name.Replace(".mp3", "");
		}

		protected System.String _Track;
		protected System.String _Artist;
		protected System.String _TrackDuration;
		protected System.String _Album;

		public System.String Track
		{
			get
			{
				return this._Track;
			}
		}
		public System.String Artist
		{
			get
			{
				return this._Artist;
			}
		}
		public System.String Trackduration
		{
			get
			{
				return this._TrackDuration;
			}
		}
		public System.String Album
		{
			get
			{
				return this._Album;
			}
		}
		public override System.String ToString()
		{
			return this._Artist + " - " + this._Track;
		}
	}

	public class MetaTrack : IMetaTrack
	{
		public MetaTrack(System.String Track, System.String Artist)
		{
			this._Track = Track;
			this._Artist = Artist;
		}
		protected System.String _Track;
		protected System.String _Artist;
		public System.String Track
		{
			get
			{
				return this._Track;
			}
		}
		public System.String Artist
		{
			get
			{
				return this._Artist;
			}
		}
	}
}
