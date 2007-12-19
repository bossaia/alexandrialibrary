using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;
using Alexandria.Media;

using Telesophy.Alexandria.Model;

namespace Alexandria.TagLib
{
	public class TagLibEngine
	{
		#region Constructors
		public TagLibEngine()
		{
		}
		#endregion
		
		#region Public Methods
		[System.CLSCompliant(false)]
		public IMediaItem GetAudioTrack(Uri path)
		{
			Tag tag = null;
			File tagFile = null;
					
			if (path != null)
			{
				string localPath = path.LocalPath;
				tagFile = File.Create(localPath);
				
				if (tagFile != null && !string.IsNullOrEmpty(tagFile.MimeType))
				{
					TimeSpan duration = TimeSpan.Zero;
					if (tagFile.AudioProperties != null)
						duration = tagFile.AudioProperties.Duration;
				
					System.Diagnostics.Debug.WriteLine("tagFile has data");
					
					//switch(resource.Format.ContentTypes[0].Name.ToLowerInvariant()) //file.Format.Mime.ToString())
					switch(tagFile.MimeType.ToString().ToLowerInvariant())
					{
						case "taglib/ogg":
						case "audio/ogg":
							tag = tagFile.FindTag(TagTypes.Xiph);
							if (tag != null)
								tag.Format = "ogg";
							break;
						case "taglib/flac":
						case "audio/flac":
							tag = tagFile.FindTag(TagTypes.Xiph);
							if (tag != null)
								tag.Format = "flac";
							break;
						case "taglib/mp3":
						case "audio/mp3":
							tag = tagFile.FindTag(TagTypes.Id3v2);
							if (tag == null)
							{
								System.Diagnostics.Debug.WriteLine("Id3v1 tag");
								tag = tagFile.FindTag(TagTypes.Id3v1);
								if (tag != null)
								{
									tag.Format = "mp3";
								}
							}
							else
							{
								tag.Format = "mp3";
								System.Diagnostics.Debug.WriteLine("Id3v2 tag");
							}
							break;
						case "taglib/ape":
						case "audio/ape":
							System.Diagnostics.Debug.WriteLine("APE tag");
							tag = tagFile.FindTag(TagTypes.Ape);
							if (tag != null)
							{
								tag.Format = "ape";
							}
							break;
						case "taglib/wma":
						case "audio/wma":
							System.Diagnostics.Debug.WriteLine("ASF tag");
							tag = tagFile.FindTag(TagTypes.Asf);
							if (tag != null)
							{
								tag.Format = "wma";
							}
							break;
						case "taglib/m4a":
						case "audio/m4a":
						case "taglib/aac":
						case "audio/aac":
							tag = tagFile.FindTag(TagTypes.Apple);
							if (tag != null)
							{
								tag.Format = "aac";
							}
							break;
						default:
							break;
					}
					
					if (tag != null)
					{
						tag.Path = new Uri(localPath);
						tag.Duration = duration;
					}
				}
				else
				{
					System.Diagnostics.Debug.WriteLine("tagFile is NULL");
				}
			}
			
			return tag;
		}
		#endregion
	}
}
