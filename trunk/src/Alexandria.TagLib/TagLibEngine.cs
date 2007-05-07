using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.TagLib
{
	//[TagEngineClass]
	public class TagLibEngine
	{
		#region Constructors
		public TagLibEngine()
		{
		}
		#endregion
		
		#region Public Methods
		[System.CLSCompliant(false)]
		public Tag GetAudioTag(IMedia resource)
		{
			Tag tag = null;
			File tagFile = null;
					
			if (resource != null)
			{
				tagFile = File.Create(resource.Location.Path);
				if (tagFile != null)
				{
					System.Diagnostics.Debug.WriteLine("tagFile has data");
					switch(resource.Format.ContentTypes[0].Name.ToLowerInvariant()) //file.Format.Mime.ToString())
					{
						case "audio/ogg":
						case "audio/flac":
							tag = tagFile.FindTag(TagTypes.Xiph);
							break;
						case "audio/mp3":
							tag = tagFile.FindTag(TagTypes.Id3v2);
							if (tag == null)
								tag = tagFile.FindTag(TagTypes.Id3v1);
							break;
						case "audio/ape":
							tag = tagFile.FindTag(TagTypes.Ape);
							break;
						case "audio/wma":
							tag = tagFile.FindTag(TagTypes.Asf);
							break;
						case "audio/m4a":
						case "audio/aac":
							tag = tagFile.FindTag(TagTypes.Apple);
							break;
						default:
							break;
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
