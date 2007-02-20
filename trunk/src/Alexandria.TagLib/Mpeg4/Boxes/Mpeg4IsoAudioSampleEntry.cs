using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.TagLib
{
	/// <summary>
	/// Audio Sequences
	/// </summary>
	public class Mpeg4IsoAudioSampleEntry : Mpeg4IsoSampleEntry
	{
		#region Constructors
		public Mpeg4IsoAudioSampleEntry(Mpeg4BoxHeader header, Mpeg4Box parent) : base(header, parent)
		{
			File.Seek(base.DataPosition + 8);
			channelCount = (ushort)File.ReadBlock(2).ToShort();
			sampleSize = (ushort)File.ReadBlock(2).ToShort();

			File.Seek(base.DataPosition + 16);
			sampleRate = (uint)File.ReadBlock(4).ToUInt();
		}
		#endregion
	
		#region Private Fields
		private ushort channelCount;
		private ushort sampleSize;
		private uint sampleRate;
		#endregion
		
		#region Protected Properties
		protected override long DataPosition
		{
			get { return base.DataPosition + 20; }
		}
		
		[System.CLSCompliant(false)]
		protected override ulong DataSize
		{
			get { return base.DataSize - 20; }
		}
		#endregion
		
		#region Public Properties
		[System.CLSCompliant(false)]
		public ushort ChannelCount
		{
			get { return channelCount; }
		}
		
		[System.CLSCompliant(false)]
		public ushort SampleSize
		{
			get { return sampleSize; }
		}
		
		[System.CLSCompliant(false)]
		public uint SampleRate
		{
			get { return sampleRate >> 16; }
		}

		public override bool HasChildren
		{
			get { return true; }
		}
		#endregion
	}
}
