using System;

namespace Alexandria.LastFM.LastRipper
{

	public class PlatformSettings
	{
		public const System.String PathSeparator = "\\";
		public static System.String TempFilePath
		{
			get
			{
				return PlatformSettings.TempPath + "TheLastRipper.mp3";
			}
		}

		public static System.String TempPath
		{
			get
			{
				System.String Path = "c:\\temp\\";
				if (!System.IO.Directory.Exists(Path))
				{
					System.IO.Directory.CreateDirectory(Path);
				}
				return Path;
			}
		}
		/*public const System.String PathSeparator = "/";
		public static System.String TempFilePath
		{
			get
			{
				return PlatformSettings.TempPath + "TheLastRipper.mp3";
			}
		}
		
		public static System.String TempPath
		{
			get
			{
				System.String Path = "/tmp/";
				if(!System.IO.Directory.Exists(Path))
				{
					System.IO.Directory.CreateDirectory(Path);
				}
				return Path;
			}
		}*/

	}
}
