using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace AlexandriaOrg.Alexandria.FreeDB
{
	#region DeviceBroadcastHeader
	/// <summary>
	/// Device Broadcast Header
	/// </summary>
	/// <remarks>DEV_BROADCAST_HDR</remarks>
	[StructLayout(LayoutKind.Sequential)]
	internal struct DeviceBroadcastHeader
	{
		/// <summary>
		/// Size
		/// </summary>
		/// <remarks>dbch_size</remarks>
		public uint Size;
				
		/// <summary>
		/// DeviceType
		/// </summary>
		/// <remarks>dbch_devicetype</remarks>
		public DeviceType DeviceType;
		
		/// <summary>
		/// Reserved
		/// </summary>
		/// <remarks>dbch_reserved</remarks>
		uint Reserved;
	}
	#endregion

	#region DeviceBroadcastVolume
	/// <summary>
	/// Device Broadcast Volume
	/// </summary>
	/// <remarks>DEV_BROADCAST_VOLUME</remarks>
	[StructLayout(LayoutKind.Sequential)]
	internal struct DeviceBroadcastVolume
	{
		/// <summary>
		/// Size
		/// </summary>
		/// <remarks>dbcv_size</remarks>
		public uint Size;
		
		/// <summary>
		/// DeviceType
		/// </summary>
		/// <remarks>dbcv_devicetype</remarks>
		public DeviceType DeviceType;
		
		/// <summary>
		/// Reserved
		/// </summary>
		/// <remarks>dbcv_reserved</remarks>
		uint dbcv_reserved;
		
		/// <summary>
		/// UnitMask
		/// </summary>
		/// <remarks>dbcv_unitmask</remarks>
		uint dbcv_unitmask;
		
		/// <summary>
		/// Drives
		/// </summary>
		public char[] Drives
		{
			get
			{
				string drvs = "";
				for (char c = 'A'; c <= 'Z'; c++)
				{
					if ((dbcv_unitmask & (1 << (c - 'A'))) != 0)
					{
						drvs += c;
					}
				}
				return drvs.ToCharArray();
			}
		}
		
		/// <summary>
		/// Flags
		/// </summary>
		/// <remarks>dbcv_flags</remarks>
		public VolumeChangeFlags Flags;
	}
	#endregion
	
	#region TrackData
	/// <summary>
	/// Track data
	/// </summary>
	/// <remarks>TRACK_DATA</remarks>
	[StructLayout(LayoutKind.Sequential)]
	public struct TrackData
	{
		public byte Reserved;
		
		private byte BitMapped;
		
		public byte Control
		{
			get
			{
				return (byte)(BitMapped & 0x0F);
			}
			set
			{
				BitMapped = (byte)((BitMapped & 0xF0) | (value & (byte)0x0F));
			}
		}
		
		public byte Adr
		{
			get
			{
				return (byte)((BitMapped & (byte)0xF0) >> 4);
			}
			set
			{
				BitMapped = (byte)((BitMapped & (byte)0x0F) | (value << 4));
			}
		}
		
		public byte TrackNumber;
		
		public byte Reserved1;
		
		/// <summary>
		/// Don't use array to avoid array creation
		/// </summary>
		public byte Address_0;
		public byte Address_1;
		public byte Address_2;
		public byte Address_3;
	};	
	#endregion
}