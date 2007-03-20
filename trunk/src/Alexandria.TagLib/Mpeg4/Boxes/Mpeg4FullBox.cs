using System;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public abstract class Mpeg4FullBox : Mpeg4Box
	{
		#region Constructors
		protected Mpeg4FullBox(Mpeg4BoxHeader header, Mpeg4Box parent) : base(header, parent)
		{
			File.Seek(base.DataPosition);

			// First 4 buffer contain version and flag data.
			version = File.ReadBlock(1)[0];
			flags = File.ReadBlock(3).ToUInt();
		}

		// We can create our own box.
		[System.CLSCompliant(false)]
		protected Mpeg4FullBox(ByteVector type, uint flags, Mpeg4Box parent) : base(new Mpeg4BoxHeader(type), parent)
		{
			//version = 0;
			this.flags = flags;
		}

		[System.CLSCompliant(false)]
		protected Mpeg4FullBox(ByteVector type, uint flags) : this(type, flags, null)
		{
		}

		protected Mpeg4FullBox(ByteVector type, Mpeg4Box parent) : this(type, 0, parent)
		{
		}

		protected Mpeg4FullBox(ByteVector type) : this(type, 0)
		{
		}
		#endregion
   
		#region Private Fields
		private byte version;
		private uint flags;
		#endregion                 
        
        #region Protected Properties
		// Offset for those four buffer.
		protected override long DataPosition
		{
			get { return base.DataPosition + 4; }
		}
        #endregion
           
		#region Protected Methods
		/// <summary>
		/// Render this box with the version and flags info at the beginning.
		/// </summary>
		/// <param name="data">The data to render</param>
		/// <returns>A ByteVector of the rendered data</returns>
		protected override ByteVector Render(ByteVector data)
		{
			ByteVector output = new ByteVector ();
         
			output += (byte)version;
			output += ByteVector.FromUInt(flags).Mid(1,3);
			output += data;
			         
			return base.Render(output);
		}
		#endregion
		
		#region Public Properties
		[System.CLSCompliant(false)]
		public uint Version
		{
			get {return version;}
			set {version = (byte)value;}
		}
		
		[System.CLSCompliant(false)]
		public uint Flags
		{
			get {return flags;}
			set {flags = value;}
		}      
		
		[System.CLSCompliant(false)]
		protected override ulong DataSize
		{
			get {return base.DataSize - 4;}
		}
		#endregion
	}
}
