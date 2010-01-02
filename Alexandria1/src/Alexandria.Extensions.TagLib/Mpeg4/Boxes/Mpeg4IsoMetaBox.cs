using System;

namespace Alexandria.TagLib
{
	public class Mpeg4IsoMetaBox : Mpeg4FullBox
	{
		#region Constructors
		public Mpeg4IsoMetaBox(Mpeg4BoxHeader header, Mpeg4Box parent) : base(header, parent)
		{
		}

		public Mpeg4IsoMetaBox(ByteVector handler_type, string handler_name, Mpeg4Box parent) : base("meta", 0, parent)
		{
			AddChild(new Mpeg4IsoHandlerBox(handler_type, handler_name, this));
		}

		public Mpeg4IsoMetaBox(ByteVector handler_type, string handler_name) : this(handler_type, handler_name, null)
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