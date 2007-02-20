using System;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public class Mpeg4FileBox : Mpeg4Box
	{
		#region Constructors
		// FileBox cannot be read from a file and is a seed box for a file.
		public Mpeg4FileBox(Mpeg4File file) : base(null)
		{
			this.File = file;
		}
		#endregion
		
		#region Protected Properties
		protected override long DataPosition
		{
			get { return 0; }
		}
		#endregion
		
		#region Public Properties
		public override bool IsValid
		{
			get { return File.IsValid; }
		}
		
		public override bool HasChildren
		{
			get { return true; }
		}
		
		public override ByteVector BoxType
		{
			get { return "FILE"; }
		}
		
		[System.CLSCompliant(false)]
		public override ulong BoxSize
		{
			get { return (ulong)File.Length; }
		}
		
		public override long NextBoxPosition
		{
			get { return File.Length; }
		}
		
		public override ByteVector Data
		{
			get { return null; }
			set { }
		}
		#endregion
	}
}
