using System;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public class Mpeg4AppleDataBox : Mpeg4FullBox
	{
		#region Constructors
		public Mpeg4AppleDataBox(Mpeg4BoxHeader header, Mpeg4Box parent) : base(header, parent)
		{
			// Ensure that the data is loaded before the file is closed.
			
			// Get the box data based on position and size offsets
			LoadBoxData(4, -4);			
		}

		// Make a data box from the given data and flags.
		[System.CLSCompliant(false)]
		public Mpeg4AppleDataBox(ByteVector data, uint flags, Mpeg4Box parent) : base("data", flags, parent)
		{
			InitializeBoxData(data);
			//Data = data;
		}

		// Make a data box from the given data and flags.
		[System.CLSCompliant(false)]
		public Mpeg4AppleDataBox(ByteVector data, uint flags) : this(data, flags, null)
		{
		}
		#endregion
		
		#region Protected Properties
		// Move the position and length to account for the reserved space.
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
		// If the flag type is ContainsText, then the data can be read as a UTF8
		// string. We can also store a UTF8 string as type if we set the flags to
		// show that.
		public string Text
		{
			get { return ((Flags & (int)Mpeg4ContentType.ContainsText) != 0) ? Data.ToString(StringType.UTF8) : null; }
			set
			{
				Flags = (int)Mpeg4ContentType.ContainsText;
				Data = ByteVector.FromString(value, StringType.UTF8);
			}
		}
		#endregion
		
		#region Public Methods
		public override ByteVector Render()
		{
			return Render(new ByteVector(4));
		}
		#endregion
	}
}
