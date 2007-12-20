using System;
using System.Collections.Generic;

namespace Alexandria.TagLib
{
	public class Mpeg4IsoChunkOffsetBox : Mpeg4FullBox
	{
		#region Constructors
		public Mpeg4IsoChunkOffsetBox(Mpeg4BoxHeader header, Mpeg4Box parent) : base(header, parent)
		{
			File.Seek(base.DataPosition);
			offsets = new List<uint>((int)File.ReadBlock(4).ToUInt());
			//new uint[(int)File.ReadBlock(4).ToUInt()];

			for (int i = 0; i < offsets.Count; i++)
				offsets[i] = File.ReadBlock(4).ToUInt();
		}
		#endregion
		
		#region Private Fields
		private List<uint> offsets; //uint[] offsets;
		#endregion
		
		#region Private Methods
		private ByteVector UpdateOffsetInternal(int sizeDifference)
		{
			ByteVector output = ByteVector.FromUInt((uint)offsets.Count);
			for (int i = 0; i < offsets.Count; i++)
			{
				offsets[i] = (uint)(offsets[i] + sizeDifference);
				output += ByteVector.FromUInt(offsets[i]);
			}

			return output;
		}
		#endregion
		
		#region Public Properties
		[System.CLSCompliant(false)]
		public IList<uint> Offsets
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
		public ByteVector Render(int sizeDifference)
		{

			return Render(UpdateOffsetInternal(sizeDifference));
		}

		public override ByteVector Render()
		{
			return Render(0);
		}

		public void UpdateOffset(int sizeDifference)
		{
			ByteVector newData = UpdateOffsetInternal(sizeDifference);
			File.Insert(newData, DataPosition, newData.Count);
		}
		#endregion
	}
}
