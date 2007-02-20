using System;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public class Mpeg4AppleItemListBox : Mpeg4Box
	{
		#region Constructors
		public Mpeg4AppleItemListBox(Mpeg4BoxHeader header, Mpeg4Box parent) : base(header, parent)
		{
		}

		// This box can be created without loading it.
		public Mpeg4AppleItemListBox(Mpeg4Box parent) : base("ilst", parent)
		{
		}

		public Mpeg4AppleItemListBox() : this(null)
		{
		}
		#endregion
		
		#region Public Properties
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