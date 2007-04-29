using System;

namespace Alexandria.TagLib
{
	public class Mpeg4IsoSampleDescriptionBox : Mpeg4FullBox
	{
		#region Constructors
		public Mpeg4IsoSampleDescriptionBox(Mpeg4BoxHeader header, Mpeg4Box parent) : base(header, parent)
		{
			File.Seek(base.DataPosition);

			// This box just contains a number saying how many of the first boxes
			// will be SampleEntries, since they can be named whatever they want to
			// be.
			entryCount = File.ReadBlock(4).ToUInt();
		}
		#endregion
		
		#region Private Fields
		private uint entryCount;
		#endregion
		
		#region Protected Properties
		// Offset for those buffer.
		protected override long DataPosition
		{
			get { return base.DataPosition + 4; }
		}

		[System.CLSCompliant(false)]
		protected override ulong DataSize
		{
			get { return base.DataSize - 4; }
		}
		#endregion
		
		#region Public Properties
		[System.CLSCompliant(false)]
		public uint EntryCount
		{
			get { return entryCount; }
		}

		// This box contains no data and has children.
		public override bool HasChildren
		{
			get { return true; }
		}

		public override ByteVector Data
		{
			get { return null; }
			set { }
		}
		#endregion
	}
}
