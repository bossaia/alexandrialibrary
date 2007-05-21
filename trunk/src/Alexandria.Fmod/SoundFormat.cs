using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Alexandria.Fmod
{
	public class SoundFormat : IMediaFormat
	{
		#region Constructors
		public SoundFormat(FmodSoundFormat fmodSoundFormat, SoundType soundType)
		{
			this.fmodSoundFormat = fmodSoundFormat;
			this.soundType = soundType;
			
			switch(soundType)
			{
				case SoundType.Aac:
					name = "Advanced Audio Codec";
					contentTypes.Add(new ContentType("audio/aac"));
					contentTypes.Add(new ContentType("audio/m4a"));
					fileExtensions.Add("aac");
					fileExtensions.Add("m4a");
					break;
				case SoundType.Aiff:
					break;
				case SoundType.Asf:
					break;
				case SoundType.At3:
					break;
				case SoundType.Cdda:
					name = "CDDA";
					break;
				case SoundType.Dls:
					break;
				case SoundType.Flac:
					break;
				case SoundType.Fsb:
					break;
				case SoundType.GameCubeAdpcm:
					break;
				case SoundType.IT:
					break;
				case SoundType.Midi:
					break;
				case SoundType.Mod:
					break;
				case SoundType.Mpeg:
					name = "MPEG III layer I";
					contentTypes.Add(new ContentType("audio/mp3"));
					fileExtensions.Add("mp3");
					break;
				case SoundType.OggVorbis:
					name = "Ogg Vorbis";
					contentTypes.Add(new ContentType("application/ogg"));
					fileExtensions.Add("ogg");
					break;
				case SoundType.Playlist:
					break;
				case SoundType.Raw:
					break;
				case SoundType.S3m:
					break;
				case SoundType.Sf2:
					break;
				case SoundType.Vag:
					break;
				case SoundType.Wav:
					break;
				case SoundType.XM:
					break;
				case SoundType.Xma:
					break;
				default:
					break;
			}
		}
		#endregion
		
		#region Private Fields
		private FmodSoundFormat fmodSoundFormat = FmodSoundFormat.None;
		private SoundType soundType = SoundType.Unknown;
		private string name;
		private List<System.Net.Mime.ContentType> contentTypes = new List<System.Net.Mime.ContentType>();
		private List<string> fileExtensions = new List<string>();
		#endregion

		#region IMediaFormat Members
		public IList<System.Net.Mime.ContentType> ContentTypes
		{
			get { return contentTypes; }
		}

		public IList<string> FileExtensions
		{
			get { return fileExtensions; }
		}

		public string Name
		{
			get { return name; }
		}
		#endregion
	}
}
