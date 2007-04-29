using System;

namespace Alexandria.TagLib
{
	public class Mpeg4IsoFreeSpaceBox : Mpeg4Box
	{
		#region Constructors
		public Mpeg4IsoFreeSpaceBox(Mpeg4BoxHeader header, Mpeg4Box parent) : base(header, parent)
		{
			// set padding equal to the size of the zero space.
			padding = HeaderDataSize;
		}

		[System.CLSCompliant(false)]
		public Mpeg4IsoFreeSpaceBox(ulong padding, Mpeg4Box parent) : base("free", parent)
		{
			PaddingSize = padding;
		}
		#endregion
		
		#region Private Fields
		private ulong padding;
		#endregion
		
		#region Public Properties
		public override ByteVector Data
		{
			//FIXME: DP 12/6/2006 - A property should not return new instances every time it is accessed
			
			// Get returns an numbers comprized entirely of zeros.
			get { return new ByteVector((int)padding); }
			set { }
		}

		[System.CLSCompliant(false)]
		public ulong PaddingSize
		{
			// PaddingSize equals the total rendered size of the box.
			get { return padding + 8; }
			set { padding = value - 8; }
		}
		#endregion
	}
}
