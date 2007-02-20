using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace AlexandriaOrg.Alexandria.FreeDB
{
	/// <summary>
	/// CD ROM Table of Contents
	/// </summary>
	/// CDROM_TOC
	[StructLayout(LayoutKind.Sequential)]
	public class CDRomToc
	{
		public ushort Length;
		public byte FirstTrack = 0;
		public byte LastTrack = 0;

		public TrackDataList TrackData;

		public CDRomToc()
		{
			TrackData = new TrackDataList();
			Length = (ushort)Marshal.SizeOf(this);
		}
	}
}
