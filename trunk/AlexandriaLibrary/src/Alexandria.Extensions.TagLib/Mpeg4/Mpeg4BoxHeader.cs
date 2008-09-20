using System;

namespace Alexandria.TagLib
{
	public class Mpeg4BoxHeader
	{
		#region Constructors
		public Mpeg4BoxHeader(Mpeg4File file, long position)
		{
			this.file = file;
			this.position = position;
			//boxType = null;
			//extendedType = null;
			//largeSize = 0;
			//size = 0;         
			Read();
		}

		// Create a new header with the provided box type.
		public Mpeg4BoxHeader(ByteVector type)
		{
			//this.file = null;
			this.position = -1;
			boxType = type;
			//extendedType = null;
			//largeSize = 0;
			//size = 0;
		}
		#endregion
		
		#region Private Fields
		private Mpeg4File file;
		private long position;
		private ByteVector boxType;
		private ByteVector extendedType;
		private ulong largeSize;
		private uint size;
		#endregion

		#region Private Methods
		private void Read()
		{
			// How much can we actually read?
			long spaceAvailable = file.Length - position;

			// The size has to be at least 8.
			size = 8;

			// If we can'type read that much, return.
			if (spaceAvailable < size)
				return;

			// Get into position.
			file.Seek(position);

			// Read the size and type of the block.
			largeSize = file.ReadBlock(4).ToUInt();
			boxType = file.ReadBlock(4);

			// If the size is zero, the block includes the rest of the file.
			if (largeSize == 0)
				largeSize = (ulong)spaceAvailable;
			// If the size is 1, that just tells us we have a massive ULONG size
			// waiting for us in the next 8 buffer.
			else if (largeSize == 1)
			{
				// The size is 8 bigger.
				size += 8;

				// If we don'type have room, we're lost. Abort.
				if (spaceAvailable < size)
				{
					largeSize = 0;
					return;
				}

				// This file is huge. 4GB+ I don'type think we can even read it.
				largeSize = (ulong)file.ReadBlock(8).ToLong();
			}

			// UUID has a special header with 16 extra buffer.
			if (boxType == "uuid")
			{
				// Size is 16 bigger.
				size += 16;

				// If we don'type have room, we're lost. Abort.
				if (spaceAvailable < size)
				{
					largeSize = 0;
					return;
				}

				// Load the extended type.
				extendedType = file.ReadBlock(16);
			}
		}
		#endregion

		#region Public Properties
		public Mpeg4File File
		{
			get {return file;}
		}

		// 
		/// <summary>
		/// Get a value indicating whether or not this header is valid 
		/// </summary>
		/// <remarks>
		/// If data was read, then the size is non-zero. If the size is non-zero,
		/// the header is valid. position-> q -> r.
		/// </remarks>
		public bool IsValid
		{
			get {return largeSize != 0;}
		}

		// the box'field type.
		public ByteVector BoxType
		{
			get {return boxType;}
		}

		// The extended type (for UUID only)
		public ByteVector ExtendedType
		{
			get {return extendedType;}
		}

		// The total size of the box.
		[System.CLSCompliant(false)]
		public ulong BoxSize
		{
			get {return largeSize;}
		}

		// The size of the header.
		[System.CLSCompliant(false)]
		public uint HeaderSize
		{
			get {return size;}
		}

		// The size of the data.
		[System.CLSCompliant(false)]
		public ulong DataSize
		{
			get {return largeSize - size;}
			set {largeSize = value + size;}
		}

		// The position of the box.
		public long Position
		{
			get {return position;}
		}

		// The position of the data.
		public long DataPosition
		{
			get {return position + size;}
		}

		// the position of the next box.
		public long NextBoxPosition
		{
			get {return (long)(position + (long)largeSize);}
		}
		#endregion
		
		#region Public Methods
		public ByteVector Render()
		{
			// The size is zero because the box header was created not read.
			// Increase the sizes to account for this.
			if (size == 0)
			{
				size = (uint)(extendedType != null ? 24 : 8);
				largeSize += size;
			}

			// Enlarge for large size if necessary. If large size is in use, the
			// header will be 16 or 32 big as opposed to 8 or 24.
			if ((size == 8 || size == 24) && largeSize > System.UInt32.MaxValue)
			{
				size += 8;
				largeSize += 8;
			}

			// Get ready to output.
			ByteVector output = new ByteVector();

			// Add the box size and type to the output.
			output += ByteVector.FromUInt((size == 8 || size == 24) ? (uint)largeSize : 1);
			output += boxType;

			// If the box size is 16 or 32, we must have more a large header to
			// append.
			if (size == 16 || size == 32)
				output += ByteVector.FromLong((long)largeSize);

			// The only reason for such a big size is an extended type. Extend!!!
			if (size >= 24)
				output += (extendedType != null) ? extendedType.Mid(0, 16) : new ByteVector(16);

			return output;
		}
		#endregion
	}
}