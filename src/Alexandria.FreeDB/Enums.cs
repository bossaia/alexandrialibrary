using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.FreeDB
{
	#region DeviceChangeEventType
	internal enum DeviceChangeEventType 
	{ 
		DeviceInserted, 
		DeviceRemoved 
	};
	#endregion

	#region DeviceType
	/// <summary>
	/// the general types of hardware devices
	/// </summary>
	internal enum DeviceType : uint
	{
		/// <summary>
		/// OEM-defined device type
		/// </summary>
		/// <remarks>DBT_DEVTYP_OEM</remarks>
		OemDefined = 0x00000000,
		
		/// <summary>
		/// Device node number
		/// </summary>
		/// <remarks>DBT_DEVTYP_DEVNODE</remarks>
		DevNode = 0x00000001,
		
		/// <summary>
		/// Logical volume
		/// </summary>
		/// <remarks>DBT_DEVTYP_VOLUME</remarks>
		LogicalVolume = 0x00000002,
		
		/// <summary>
		/// Serial or parallel port device
		/// </summary>
		/// <remarks>DBT_DEVTYP_PORT</remarks>
		SerialOrParallel = 0x00000003,
		
		/// <summary>
		/// Network resource
		/// </summary>
		/// <remarks>DBT_DEVTYP_NET</remarks>
		NetworkResource = 0x00000004
	}
	#endregion

	#region VolumeChangeFlags
	internal enum VolumeChangeFlags : ushort
	{
		/// <summary>
		/// Media reading/writing
		/// </summary>
		/// <remarks>DBTF_MEDIA</remarks>
		Media = 0x0001,
		
		/// <summary>
		/// Network volume
		/// </summary>
		/// <remarks>DBTF_NET</remarks>
		NetworkVolume = 0x0002
	}
	#endregion

	#region TrackModeType
	/// <summary>
	/// Track mode type
	/// </summary>
	/// <remarks>TRACK_MODE_TYPE</remarks>
	public enum TrackModeType
	{
		YellowMode2,
		XAForm2,
		CDDA
	}
	#endregion

	#region DriveType
	/// <summary>
	/// Drive type
	/// </summary>
	/// <remarks>DriveTypes</remarks>
    public enum DriveType :uint
    {
      DRIVE_UNKNOWN = 0,
      DRIVE_NO_ROOT_DIR,
      DRIVE_REMOVABLE,
      DRIVE_FIXED,
      DRIVE_REMOTE,
      DRIVE_CDROM,
      DRIVE_RAMDISK
    };	
	#endregion
}
