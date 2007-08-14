using System;
using System.Collections.Generic;
using Alexandria.Plugins;

namespace Alexandria.TagLib
{
	public class MetadataPlugin : BasePlugin
	{
		#region Constructors
		public MetadataPlugin() : base(
			new Guid("89282138-E7BA-4ada-86DC-0967C54F0481"), 
			"TagLib#",
			"Supports reading and editing the metadata of several popular audio formats (ID3, vorbiscomment, ape, asf)",
			new Version(1, 0, 0, 0),
			new Uri("Alexandria.TagLib.dll", UriKind.Relative)
		){}
		#endregion

		#region Public Methods
		public override void Load()
		{
			base.Load();
		}

		public override void Save()
		{
			base.Save();
		}
		#endregion
	}
}