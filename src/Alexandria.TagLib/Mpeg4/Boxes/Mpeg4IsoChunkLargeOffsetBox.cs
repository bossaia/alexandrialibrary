using System;
using System.Collections.Generic;

namespace Alexandria.TagLib
{
	public class Mpeg4IsoChunkLargeOffsetBox : Mpeg4FullBox
	{
		#region Constructors
		public Mpeg4IsoChunkLargeOffsetBox(Mpeg4BoxHeader header, Mpeg4Box parent) : base(header, parent)
		{
			File.Seek(base.DataPosition);
			offsets = new List<ulong>((int)File.ReadBlock(4).ToUInt());

			for (int i = 0; i < offsets.Count; i++)
				offsets[i] = (ulong)File.ReadBlock(4).ToLong();
		}
		#endregion
		
		#region Private Fields
		private List<ulong> offsets; //ulong[] offsets;
		#endregion
		
		#region Private Methods
		private ByteVector UpdateOffsetInternal(long sizeDifference)
		{
			ByteVector output = ByteVector.FromUInt((uint)offsets.Count);
			for (int i = 0; i < offsets.Count; i++)
			{
				offsets[i] = (ulong)((long)offsets[i] + sizeDifference);
				output += ByteVector.FromLong((long)offsets[i]);
			}

			return output;
		}
		#endregion
		
		#region Public Properties
		[System.CLSCompliant(false)]
		public IList<ulong> Offsets
		{
			get { return offsets; }
		}
		
		public override ByteVector Data
		{
			get { return null; }
			set { }
		}
		#endregion
		
		#region Public Methods
		public ByteVector Render(long sizeDifference)
		{
			return Render(UpdateOffsetInternal(sizeDifference));
		}

		public override ByteVector Render()
		{
			return Render(0);
		}

		public void UpdateOffset(long sizeDifference)
		{
			ByteVector newData = UpdateOffsetInternal(sizeDifference);
			File.Insert(newData, DataPosition, newData.Count);
		}
		#endregion
	}
}
