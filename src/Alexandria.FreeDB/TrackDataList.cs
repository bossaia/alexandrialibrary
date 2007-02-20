using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace AlexandriaOrg.Alexandria.FreeDB
{
	[StructLayout(LayoutKind.Sequential)]
	public class TrackDataList
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.MAXIMUM_NUMBER_TRACKS * 8)]
		private byte[] Data;
		public TrackData this[int Index]
		{
			get
			{
				if ((Index < 0) | (Index >= Constants.MAXIMUM_NUMBER_TRACKS))
				{
					throw new IndexOutOfRangeException();
				}
				TrackData res;
				GCHandle handle = GCHandle.Alloc(Data, GCHandleType.Pinned);
				try
				{
					IntPtr buffer = handle.AddrOfPinnedObject();
					buffer = (IntPtr)(buffer.ToInt32() + (Index * Marshal.SizeOf(typeof(TrackData))));
					res = (TrackData)Marshal.PtrToStructure(buffer, typeof(TrackData));
				}
				finally
				{
					handle.Free();
				}
				return res;
			}
		}
		public TrackDataList()
		{
			Data = new byte[Constants.MAXIMUM_NUMBER_TRACKS * Marshal.SizeOf(typeof(TrackData))];
		}
	}
}
