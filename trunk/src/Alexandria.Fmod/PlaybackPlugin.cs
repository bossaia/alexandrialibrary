using System;
using System.Collections.Generic;
using System.Reflection;
using Alexandria.Plugins;

namespace Alexandria.Fmod
{
	public class PlaybackPlugin : BasePlugin
	{
		#region Constructors
		public PlaybackPlugin() : base(
			new Guid("5F1EAB96-2FFC-4cd6-9374-407F77A75FDF"), 
			"FMOD Sound System",
			"Supports playback of common digital audio formats (mp3, aac, wma, vorbis, flac, wav)",
			new Version(1, 0, 0, 0),
			new Uri("Alexandria.Fmod.dll", UriKind.Relative)			
		) {}
		#endregion

		#region Private Fields
		private int numberOfChannels = 10;
		private uint streamBufferSize = 65536;
		private OutputType outputType = OutputType.AutoDetect;
		private SpeakerMode speakerMode = SpeakerMode.Stereo;
		#endregion

		#region Public Properties
		[PluginSetting("The maximum number of simultaneous sounds")]
		public int NumberOfChannels
		{
			get { return numberOfChannels; }
			set { numberOfChannels = value; }
		}

		[CLSCompliant(false)]
		[PluginSetting("The size in bytes of the memory buffer used for streaming playback")]
		public uint StreamBufferSize
		{
			get { return streamBufferSize; }
			set { streamBufferSize = value; }
		}

		[PluginSetting("The output system to use as a hardware abstraction layer")]
		public OutputType OutputType
		{
			get { return outputType; }
			set { outputType = value; }
		}

		[PluginSetting("The speaker mode to use for outputting sound")]
		public SpeakerMode SpeakerMode
		{
			get { return speakerMode; }
			set { speakerMode = value; }
		}
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
