using System;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public class Mpeg4IsoHandlerBox : Mpeg4FullBox
	{
		#region Constructors
		public Mpeg4IsoHandlerBox(Mpeg4BoxHeader header, Mpeg4Box parent) : base(header, parent)
		{
			// Reserved
			File.Seek(base.DataPosition + 4);

			// Read the handler type.
			handlerType = File.ReadBlock(4);

			// Reserved
			File.Seek(base.DataPosition + 20);

			// Find the terminating byte and read a string from the data before it.
			long end = File.Find((byte)0, File.Tell);
			name = File.ReadBlock((int)(end - File.Tell)).ToString();
		}

		// We can make our own handler.
		public Mpeg4IsoHandlerBox(ByteVector handlerType, string name, Mpeg4Box parent) : base("hdlr", 0, parent)
		{
			if (handlerType != null)
				this.handlerType = handlerType.Mid(0, 4);
			
			this.name = name;
		}
		#endregion
		
		#region Private Fields
		private ByteVector handlerType;
		private string name;
		#endregion
		
		#region Public Properties
		public ByteVector HandlerType
		{
			get { return handlerType; }
		}

		public string Name
		{
			get { return name; }
		}

		// This box has no readable data.
		public override ByteVector Data
		{
			get { return null; }
			set { }
		}
		#endregion
		
		#region Public Methods
		public override ByteVector Render()
		{
			ByteVector output = new ByteVector(4);
			output += handlerType;
			output += new ByteVector(12);
			output += ByteVector.FromString(name);
			output += new ByteVector(2);
			return Render(output);
		}
		#endregion
	}
}
