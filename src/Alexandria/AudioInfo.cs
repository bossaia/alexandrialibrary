using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class AudioInfo
	{
		#region Private Fields
		private string title;
		private string artist;
		private string album;
		private string comment;
		private uint number;
		private uint length;
		private IAudioTag tag;
		#endregion
		
		#region Private Methods
		private void SetAudioInfo()
		{
			if (tag != null)
			{
				System.Diagnostics.Debug.WriteLine("tag has data");
				this.title = tag.Title;
				this.artist = tag.FirstArtist;
				this.album = tag.Album;
				this.comment = tag.Comment;
				this.number = tag.Track;
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("tag is NULL");
				this.title = null;
				this.artist = null;
				this.album = null;
				this.comment = null;
				this.number = 0;
			}
		}
		#endregion
		
		#region Constructors
		public AudioInfo()
		{
		}

		[System.CLSCompliant(false)]
		public AudioInfo(string title, string artist, string album, string comment, uint number, uint length) : this()
		{
			this.title = title;
			this.artist = artist;
			this.album = album;
			this.comment = comment;
			this.number = number;
			this.length = length;
		}

		[System.CLSCompliant(false)]
		public AudioInfo(IAudioTag tag, uint length)
		{
			this.tag = tag;
			this.length = length;
			SetAudioInfo();
		}
		#endregion
		
		#region Public Properties
		public string Title
		{
			get {return title;}
			set {title = value;}
		}
		
		public string Artist
		{
			get {return artist;}
			set {artist = value;}
		}
		
		public string Album
		{
			get {return album;}
			set {album = value;}
		}
		
		public string Comment
		{
			get {return comment;}
			set {comment = value;}
		}

		[System.CLSCompliant(false)]
		public uint Number
		{
			get {return number;}
			set {number = value;}
		}

		[System.CLSCompliant(false)]
		public uint Length
		{
			get {return length;}
			set {length = value;}
		}
		
		public string FormattedLength
		{
			get
			{
				if (length > 0)
				{
					int minutes = Convert.ToInt32(length / 60000);
					int seconds = Convert.ToInt32(length / 1000);
					
					if (seconds > 60) seconds -= 60;
					
					string formattedMinutes, formattedSeconds;
					
					formattedMinutes = minutes.ToString("G", System.Globalization.CultureInfo.InvariantCulture);
					formattedSeconds = seconds.ToString("G", System.Globalization.CultureInfo.InvariantCulture);
					
					if (formattedSeconds.Length > 2)
					{
						formattedSeconds = formattedSeconds.Substring(0, 2);
					}

					return formattedMinutes + ":" + formattedSeconds;
				}
				else return string.Empty;
			}
		}

		[System.CLSCompliant(false)]
		public IAudioTag Tag
		{
			get {return tag;}
			set
			{
				tag = value;
				SetAudioInfo();
			}
		}
		#endregion
	}
}
