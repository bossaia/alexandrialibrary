using System;
using Alexandria.AudioToolsLibrary;

namespace Alexandria.AudioToolsLibrary.BinaryLogic
{
	/// <summary>
	/// Dummy physical data provider
	/// </summary>
	public class DummyReader : IAudioReader
	{
		public DummyReader()
		{
		}

		public double BitRate
		{
			get { return 0; }
		}		
		public double Duration
		{
			get { return 0; }
		}
		public bool IsVBR
		{
			get { return false; }
		}
		public CodecFamily CodecFamily
		{
			get { return CodecFamily.Lossy; }
		}
		public BinaryLogic.TID3v1 ID3v1
		{
			get { return new TID3v1(); }
		}
		public BinaryLogic.TID3v2 ID3v2
		{
			get { return new TID3v2(); }
		}
		public BinaryLogic.TAPEtag APEtag
		{
			get { return new TAPEtag(); }
		}

		public bool ReadFromFile(String fileName)
		{
			return true;
		}
	}
}
