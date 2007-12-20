using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Alexandria.MediaInfo
{
	/// <summary>
	/// Import of native functions from MediaInfo.dll
	/// </summary>
	/// <remarks>Do NOT use CoTaskMemAlloc to allocate memory</remarks>
	internal static class NativeMethods
	{
		[DllImport("MediaInfo.dll")]
		internal static extern IntPtr MediaInfo_New();
		
		[DllImport("MediaInfo.dll")]
		internal static extern void MediaInfo_Delete(IntPtr handle);
		
		[DllImport("MediaInfo.dll")]
		internal static extern int MediaInfo_Open(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string fileName);
		
		[DllImport("MediaInfo.dll")]
		internal static extern void MediaInfo_Close(IntPtr handle);
		
		[DllImport("MediaInfo.dll")]
		internal static extern IntPtr MediaInfo_Inform(IntPtr handle, [MarshalAs(UnmanagedType.U4)] uint reserved);
		
		[DllImport("MediaInfo.dll")]
		internal static extern IntPtr MediaInfo_GetI(IntPtr handle, [MarshalAs(UnmanagedType.U4)] StreamType streamKind, uint streamNumber, uint parameter, [MarshalAs(UnmanagedType.U4)] InfoType KindOfInfo);
		
		[DllImport("MediaInfo.dll")]
		internal static extern IntPtr MediaInfo_Get(IntPtr handle, [MarshalAs(UnmanagedType.U4)] StreamType streamKind, uint StreamNumber, [MarshalAs(UnmanagedType.LPWStr)] string parameter, [MarshalAs(UnmanagedType.U4)] InfoType KindOfInfo, [MarshalAs(UnmanagedType.U4)] InfoType KindOfSearch);
		
		[DllImport("MediaInfo.dll")]
		internal static extern IntPtr MediaInfo_Option(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string option, [MarshalAs(UnmanagedType.LPWStr)] string value);
		
		[DllImport("MediaInfo.dll")]
		internal static extern int MediaInfo_State_Get(IntPtr handle);
		
		[DllImport("MediaInfo.dll")]
		internal static extern int MediaInfo_Count_Get(IntPtr handle, [MarshalAs(UnmanagedType.U4)] StreamType StreamKind, int streamNumber);
	}
}
