using System;
using System.Collections.Generic;
using System.Text;
using Alexandria.Metadata;
using Alexandria.MusicBrainz;

namespace Alexandria.Control
{
	public class CompactDiscTrackSource : ITrackSource
	{
		#region Constructors
		public CompactDiscTrackSource(IAlbumFactory factory, Uri path)
		{
			this.factory = factory;
			this.path = path;
		}
		#endregion
	
		#region Private Fields
		private IAlbumFactory factory;
		private Uri path;
		#endregion
	
		#region ITrackSource Members
		public IList<Alexandria.Metadata.IAudioTrack> GetAudioTracks()
		{
			try
			{
				IAlbum album = factory.CreateAlbum(path);
				return album.Tracks;
			}
			catch (Exception ex)
			{
				string x = ex.Message;
			}
			return null;
		}
		#endregion
	}
}
