using System;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public class Mpeg4AppleAnnotationBox : Mpeg4Box
	{
		#region Constructors
		public Mpeg4AppleAnnotationBox(Mpeg4BoxHeader header, Mpeg4Box parent) : base(header, parent)
		{
		}

		// This box can be created without loading it.
		public Mpeg4AppleAnnotationBox(ByteVector type, Mpeg4Box parent) : base(type, parent)
		{
		}

		public Mpeg4AppleAnnotationBox(ByteVector type) : this(type, null)
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