using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace AlexandriaOrg.Alexandria.MediaInfo
{
	internal static class NativeMethods
	{
		//Import of DLL functions. DO NOT USE until you know what you do (MediaInfo DLL do NOT use CoTaskMemAlloc to allocate memory)  
		[DllImport("MediaInfo.dll")]
		public static extern IntPtr MediaInfo_New();
		
		[DllImport("MediaInfo.dll")]
		public static extern void MediaInfo_Delete(IntPtr handle);
		
		[DllImport("MediaInfo.dll")]
		public static extern int MediaInfo_Open(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string fileName);
		
		[DllImport("MediaInfo.dll")]
		public static extern void MediaInfo_Close(IntPtr handle);
		
		[DllImport("MediaInfo.dll")]
		public static extern IntPtr MediaInfo_Inform(IntPtr handle, [MarshalAs(UnmanagedType.U4)] uint reserved);
		
		[DllImport("MediaInfo.dll")]
		public static extern IntPtr MediaInfo_GetI(IntPtr handle, [MarshalAs(UnmanagedType.U4)] StreamType streamKind, uint streamNumber, uint parameter, [MarshalAs(UnmanagedType.U4)] InfoType KindOfInfo);
		
		[DllImport("MediaInfo.dll")]
		public static extern IntPtr MediaInfo_Get(IntPtr handle, [MarshalAs(UnmanagedType.U4)] StreamType streamKind, uint StreamNumber, [MarshalAs(UnmanagedType.LPWStr)] string parameter, [MarshalAs(UnmanagedType.U4)] InfoType KindOfInfo, [MarshalAs(UnmanagedType.U4)] InfoType KindOfSearch);
		
		[DllImport("MediaInfo.dll")]
		public static extern IntPtr MediaInfo_Option(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] string option, [MarshalAs(UnmanagedType.LPWStr)] string value);
		
		[DllImport("MediaInfo.dll")]
		public static extern int MediaInfo_State_Get(IntPtr handle);
		
		[DllImport("MediaInfo.dll")]
		public static extern int MediaInfo_Count_Get(IntPtr handle, [MarshalAs(UnmanagedType.U4)] StreamType StreamKind, int streamNumber);
	}
}
