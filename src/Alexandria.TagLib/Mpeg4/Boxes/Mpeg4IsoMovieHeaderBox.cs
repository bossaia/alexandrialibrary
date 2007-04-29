using System;

namespace Alexandria.TagLib
{
	public class Mpeg4IsoMovieHeaderBox : Mpeg4FullBox
	{
		#region Constructors
		public Mpeg4IsoMovieHeaderBox(Mpeg4BoxHeader header, Mpeg4Box parent) : base(header, parent)
		{
			// Size depends on version.
			boxSize = Version == 1 ? 108 : 96;

			// Get everything.
			File.Seek(base.DataPosition);
			ByteVector data = File.ReadBlock(boxSize);
			int pos = 0;

			// Read version one (large integers).
			if (Version == 1)
			{
				if (data.Count >= pos + 8)
					creationTime = (ulong)data.Mid(pos, 8).ToLong();
				pos += 8;

				if (data.Count >= pos + 8)
					modificationTime = (ulong)data.Mid(pos, 8).ToLong();
				pos += 8;

				if (data.Count >= pos + 4)
					timescale = data.Mid(pos, 4).ToUInt();
				pos += 4;

				if (data.Count >= pos + 8)
					duration = (ulong)data.Mid(pos, 8).ToLong();
				pos += 8;
			}
			// Read version zero (normal integers).
			else
			{
				if (data.Count >= pos + 4)
					creationTime = data.Mid(pos, 4).ToUInt();
				pos += 4;

				if (data.Count >= pos + 4)
					modificationTime = data.Mid(pos, 4).ToUInt();
				pos += 4;

				if (data.Count >= pos + 4)
					timescale = data.Mid(pos, 4).ToUInt();
				pos += 4;

				if (data.Count >= pos + 4)
					duration = (ulong)data.Mid(pos, 4).ToUInt();
				pos += 4;
			}

			// Get rate
			if (data.Count >= pos + 4)
				rate = data.Mid(pos, 4).ToUInt();
			pos += 4;

			// Get volume
			if (data.Count >= pos + 2)
				volume = (ushort)data.Mid(pos, 2).ToShort();
			pos += 2;

			// reserved
			pos += 2;

			// reserved
			pos += 8;

			// video transformation matrix
			pos += 36;

			// pre-defined
			pos += 24;

			// Get next track ID
			if (data.Count >= pos + 4)
				nextTrackId = (ushort)data.Mid(pos, 4).ToUInt();
		}
		#endregion
		
		#region Private Fields
		private ulong creationTime;
		private ulong modificationTime;
		private uint timescale;
		private ulong duration;
		private uint rate;
		private ushort volume;
		private uint nextTrackId;
		private int boxSize;
		#endregion
		
		#region Protected Properties
		protected override long DataPosition
		{
			get { return base.DataPosition + boxSize; }
		}
		
		[System.CLSCompliant(false)]
		protected override ulong DataSize
		{
			get { return base.DataSize - (ulong)boxSize; }
		}
		#endregion
		
		#region Public Properties
		public DateTime CreationTime
		{
			get { return (new System.DateTime(1904, 1, 1, 0, 0, 0)).AddTicks((long)(10000000 * creationTime)); }
		}
		
		public DateTime ModificationTime
		{
			get { return (new System.DateTime(1904, 1, 1, 0, 0, 0)).AddTicks((long)(10000000 * modificationTime)); }
		}
		
		[System.CLSCompliant(false)]
		public uint Timescale
		{
			get { return timescale; }
		}
		
		[System.CLSCompliant(false)]
		public ulong Duration
		{
			get { return duration; }
		}
		
		public double Rate
		{
			get { return ((double)rate) / ((double)0x10000); }
		}
		
		public double Volume
		{
			get { return ((double)volume) / ((double)0x100); }
		}
		
		[System.CLSCompliant(false)]
		public uint NextTrackId
		{
			get { return nextTrackId; }
		}

		public override bool HasChildren
		{
			get { return true; }
		}
		
		public override ByteVector Data
		{
			get { return null; } set { }
		}
		#endregion
	}
}
