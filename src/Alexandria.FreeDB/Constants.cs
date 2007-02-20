using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.FreeDB
{
	public abstract class Constants
	{
		//DesiredAccess values
		public const uint GENERIC_READ      = 0x80000000;
		public const uint GENERIC_WRITE     = 0x40000000;
		public const uint GENERIC_EXECUTE   = 0x20000000;
		public const uint GENERIC_ALL       = 0x10000000;

		//Share constants
		public const uint FILE_SHARE_READ   = 0x00000001;  
		public const uint FILE_SHARE_WRITE  = 0x00000002;  
		public const uint FILE_SHARE_DELETE = 0x00000004;  
	    
		//CreationDisposition constants
		public const uint CREATE_NEW        = 1;
		public const uint CREATE_ALWAYS     = 2;
		public const uint OPEN_EXISTING     = 3;
		public const uint OPEN_ALWAYS       = 4;
		public const uint TRUNCATE_EXISTING = 5;

		public const uint IOCTL_CDROM_READ_TOC = 0x00024000;
		public const uint IOCTL_STORAGE_CHECK_VERIFY = 0x002D4800;
		public const uint IOCTL_CDROM_RAW_READ = 0x0002403E;
		public const uint IOCTL_STORAGE_MEDIA_REMOVAL = 0x002D4804;
		public const uint IOCTL_STORAGE_EJECT_MEDIA = 0x002D4808;
		public const uint IOCTL_STORAGE_LOAD_MEDIA = 0x002D480C;

		public const int MAXIMUM_NUMBER_TRACKS = 100;
	}
}
