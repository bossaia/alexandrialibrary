using System;
using System.Collections.Generic;
using System.Reflection;
using Alexandria.Plugins;

namespace Alexandria.Fmod
{
	public class PlaybackPlugin : IPlugin
	{
		#region Constructors
		public PlaybackPlugin()
		{
		}
		#endregion

		#region Private Fields
		private Assembly assembly;
		private string description;
		private Guid id;
		private string name;
		private Uri path;
		private IDictionary<string, ITool> tools = new Dictionary<string, ITool>();
		private Version version;
		
		private bool enabled;
		//private ConfigurationMap configurationMap;
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

		#region IPlugin Members
		public Assembly Assembly
		{
			get { return assembly; }
		}

		public string Description
		{
			get { return description; }
		}
		
		public bool Enabled
		{
			get { return enabled; }
			set { enabled = value; }
		}

		public Guid Id
		{
			get { return id; }
		}

		public void Initialize()
		{
		}

		public string Name
		{
			get { return name; }
		}

		public Uri Path
		{
			get { return path; }
		}

		public void SaveSettings()
		{
		}

		public IDictionary<string, ITool> Tools
		{
			get { return tools; }
		}

		public Version Version
		{
			get { return version; }
		}
		#endregion
	}
}
