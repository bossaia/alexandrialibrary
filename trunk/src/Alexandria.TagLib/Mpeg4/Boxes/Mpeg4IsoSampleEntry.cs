using System;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public class Mpeg4IsoSampleEntry : Mpeg4Box
	{
		#region Constructors
		public Mpeg4IsoSampleEntry(Mpeg4BoxHeader header, Mpeg4Box parent) : base(header, parent)
		{
			File.Seek(base.DataPosition + 6);
			dataReferenceIndex = (ushort)File.ReadBlock(2).ToShort();
		}
		#endregion
		
		#region Private Fields
		private ushort dataReferenceIndex;
		#endregion
		
		#region Protected Properties
		[System.CLSCompliant(false)]
		protected override ulong DataSize
		{
			get { return base.DataSize - 8; }
		}
		
		protected override long DataPosition
		{
			get { return base.DataPosition + 8; }
		}
		#endregion
		
		#region Public Properties
		[System.CLSCompliant(false)]
		public ushort DataReferenceIndex
		{
			get { return dataReferenceIndex; }
		}		
		#endregion
	}   
}
