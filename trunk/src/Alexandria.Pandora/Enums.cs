using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Alexandria.Pandora
{
	#region INTERNET_CACHE_ENTRY_INFOA
	// For PInvoke: Contains information about an entry in the Internet cache
	[StructLayout(LayoutKind.Explicit, Size = 80)]
	public struct INTERNET_CACHE_ENTRY_INFOA
	{
		[FieldOffset(0)]
		public uint dwStructSize;
		[FieldOffset(4)]
		public IntPtr lpszSourceUrlName;
		[FieldOffset(8)]
		public IntPtr lpszLocalFileName;
		[FieldOffset(12)]
		public uint CacheEntryType;
		[FieldOffset(16)]
		public uint dwUseCount;
		[FieldOffset(20)]
		public uint dwHitRate;
		[FieldOffset(24)]
		public uint dwSizeLow;
		[FieldOffset(28)]
		public uint dwSizeHigh;
		[FieldOffset(32)]
		public System.Runtime.InteropServices.ComTypes.FILETIME LastModifiedTime;
		[FieldOffset(40)]
		public System.Runtime.InteropServices.ComTypes.FILETIME ExpireTime;
		[FieldOffset(48)]
		public System.Runtime.InteropServices.ComTypes.FILETIME LastAccessTime;
		[FieldOffset(56)]
		public System.Runtime.InteropServices.ComTypes.FILETIME LastSyncTime;
		[FieldOffset(64)]
		public IntPtr lpHeaderInfo;
		[FieldOffset(68)]
		public uint dwHeaderInfoSize;
		[FieldOffset(72)]
		public IntPtr lpszFileExtension;
		[FieldOffset(76)]
		public uint dwReserved;
		[FieldOffset(76)]
		public uint dwExemptDelta;
	}
	#endregion
}
