using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class MediaController
	{
		#region Private Fields
		private List<MediaItem> items;
		private GetAudioInfo getAudioInfo;
		#endregion
		
		#region Constructors
		public MediaController(GetAudioInfo getAudioInfo)
		{
			this.getAudioInfo = getAudioInfo;
			this.items = new List<MediaItem>();
		}
		
		public MediaController(GetAudioInfo getAudioInfo, int capacity)
		{
			this.getAudioInfo = getAudioInfo;
			this.items = new List<MediaItem>(capacity);
		}
		
		public MediaController(GetAudioInfo getAudioInfo, IList<MediaFile> files)
		{
			this.getAudioInfo = getAudioInfo;
			this.items = new List<MediaItem>(files.Count);

			if (getAudioInfo != null)
			{
				foreach(MediaFile file in files)
				{
					AudioInfo info = getAudioInfo(file);
					Items.Add(new MediaItem(file, info));
				}
			}
		}
		#endregion
		
		#region Public Properties
		public IList<MediaItem> Items
		{
			get {return items;}
		}
		
		public GetAudioInfo GetAudioInfo
		{
			get {return getAudioInfo;}
		}
		#endregion
		
		#region Public Methods
		public void Load(IMediaPlaylist playlist, TagEngine tagEngine)
		{
			foreach(MediaFile file in playlist.Files)
			{
				if (file != null)
				{
					AudioInfo info = null;
					if (getAudioInfo != null)
						info = getAudioInfo(file);
				
					// If info is null then use tagEngine to get it
					if (info == null)
					{
						IAudioTag tag = tagEngine.GetAudioTag(file);
						info = new AudioInfo(tag, 0);
					}
					else if (info.Tag == null)
					{
						IAudioTag tag = tagEngine.GetAudioTag(file);
						info.Tag = tag;
					}

					// Check for an overrided length in remote media files
					if (info != null && !file.IsLocal)				
						info.Length = file.Length;
									
					Items.Add(new MediaItem(file, info));
				}
			}
		}
		#endregion
	}
}
