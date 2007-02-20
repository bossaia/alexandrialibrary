using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public class ChannelData
	{
		#region Constructors
		public ChannelData(Id3v2ChannelType type)
		{
			channelType = type;
			//volumeAdjustment = 0;
			//peakVolume = 0;
		}
		#endregion
	
		#region Private Fields
		private Id3v2ChannelType channelType;
		private short volumeAdjustment;
		private ulong peakVolume;
		#endregion
		
		#region Public Properties
		public Id3v2ChannelType ChannelType
		{
			get {return channelType;}
			set {channelType = value;}
		}
		
		public short VolumeAdjustment
		{
			get {return volumeAdjustment;}
			set {volumeAdjustment = value;}
		}
		
		[CLSCompliant(false)]
		public ulong PeakVolume
		{
			get {return peakVolume;}
			set {peakVolume = value;}
		}
		#endregion
	}
}
