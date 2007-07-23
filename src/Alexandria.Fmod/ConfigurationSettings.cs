using System;
using System.Collections.Generic;
using System.Text;
using Alexandria.Plugins;

namespace Alexandria.Fmod
{
	public class ConfigurationSettings : IPluginSettings
	{
		#region Constructors
		public ConfigurationSettings()
		{
		}
		#endregion
	
		#region Private Fields
		private bool enabled;
		private int numberOfChannels = 10;
		private uint streamBufferSize = 65536;
		private OutputType outputType = OutputType.AutoDetect;
		private SpeakerMode speakerMode = SpeakerMode.Stereo;
		#endregion
	
		#region IPluginSettings Members
		[PluginSetting(PluginSettingType.Boolean, "Indicates whether or not the FMOD Sound System plugin is enabled")]
		public bool Enabled
		{
			get { return enabled; }
			set { enabled = value; }
		}
		#endregion
		
		#region Public Properties
		[PluginSetting(PluginSettingType.Integer, "The maximum number of simultaneous sounds")]
		public int NumberOfChannels
		{
			get { return numberOfChannels; }
			set { numberOfChannels = value; }
		}
		
		[CLSCompliant(false)]
		[PluginSetting(PluginSettingType.Integer, "The size in bytes of the memory buffer used for streaming playback")]
		public uint StreamBufferSize
		{
			get { return streamBufferSize; }
			set { streamBufferSize = value; }
		}
		
		[PluginSetting(PluginSettingType.Enumeration, "The output system to use as a hardware abstraction layer")]
		public OutputType OutputType
		{
			get { return outputType; }
			set { outputType = value; }
		}
		
		[PluginSetting(PluginSettingType.Enumeration, "The speaker mode to use for outputting sound")]
		public SpeakerMode SpeakerMode
		{
			get { return speakerMode; }
			set { speakerMode = value; }
		}
		#endregion
	}
}
