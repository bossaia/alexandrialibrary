using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AlexandriaOrg.Alexandria
{
	public class Drive : Device
	{
		#region Private Static Fields
		private static List<Drive> drives;
		#endregion
	
		#region Constructors
		public Drive(string name) : base(name)
		{
		}
		#endregion
		
		#region Public Static Methods
		public static IList<Drive> Drives
		{
			get
			{
				if (drives == null)
				{
					drives = new List<Drive>();
			
					foreach(DriveInfo driveInfo in System.IO.DriveInfo.GetDrives())
					{
						switch (driveInfo.DriveType)
						{
							case DriveType.CDRom:
								drives.Add(new OpticalDrive(driveInfo.Name));
								break;
							case DriveType.Fixed:
								drives.Add(new LocalHardDrive(driveInfo.Name));
								break;
							case DriveType.Network:
								drives.Add(new NetworkDrive(driveInfo.Name));
								break;
							case DriveType.Ram:
								drives.Add(new RamDrive(driveInfo.Name));
								break;
							case DriveType.Removable:
								drives.Add(new RemovableDrive(driveInfo.Name));
								break;
							default:
								break;
						}
					}
				}
				
				return drives;
			}
		}
		#endregion
	}
}
