using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Alexandria.FreeDB
{
	/// <summary>
	/// Raw read info
	/// </summary>
	/// <remarks>RAW_READ_INFO</remarks>
	[StructLayout(LayoutKind.Sequential)]
	public class RawReadInfo
	{
		public long DiskOffset = 0;
		public uint SectorCount = 0;
		public TrackModeType TrackMode = TrackModeType.CDDA;
	}
}
