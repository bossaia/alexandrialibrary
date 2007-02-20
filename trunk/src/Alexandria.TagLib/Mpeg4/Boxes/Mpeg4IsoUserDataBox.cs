using System;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public class Mpeg4IsoUserDataBox : Mpeg4Box
	{
		#region Constructors
		public Mpeg4IsoUserDataBox(Mpeg4BoxHeader header, Mpeg4Box parent) : base(header, parent)
		{
		}

		// This box can be created without loading it.
		public Mpeg4IsoUserDataBox(Mpeg4Box parent) : base("udta", parent)
		{
		}

		public Mpeg4IsoUserDataBox() : this(null)
		{
		}
		#endregion
		
		#region Public Properties
		// This box has children, and no readable data.
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