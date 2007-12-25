using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Telesophy.Alexandria.Extensions.CompactDisc
{
	public static class AspiScanner
	{		
		#region Private Constants
		private const string FILE_NAME = "aspi_you.exe";
		private const char LINE_BREAK = '\n';
		private const int MIN_LINE_LENGTH = 47;
		private const int POS_BUS = 0;		private const int LEN_BUS = 1;
		private const int POS_ID = 2;		private const int LEN_ID = 1;
		private const int POS_LUN = 4;		private const int LEN_LUN = 1;
		private const int POS_MANUFAC = 9;	private const int LEN_MANUFAC = 8;
		private const int POS_MODEL = 18;	private const int LEN_MODEL = 16;
		private const int POS_REV = 35;		private const int LEN_REV = 4;
		private const int POS_TYPE = 40;	private const int LEN_TYPE = 8;
		#endregion
		
		#region Private Static Methods
		private static void ParseOutput(string output, IList<AspiDeviceInfo> devices)
		{
			if (!string.IsNullOrEmpty(output))
			{
				string[] lines = output.Split(new char[]{LINE_BREAK}, StringSplitOptions.RemoveEmptyEntries);
				if (lines != null && lines.Length > 0)
				{
					for(int i=0;i<lines.Length;i++)
					{
						string line = lines[i];
						if (line.Length > MIN_LINE_LENGTH)
						{
							//Look for commas to be sure that this is a device info line
							if (line.Substring(POS_BUS+1, 1) == "," && line.Substring(POS_ID+1, 1) == ",")
							{
								AspiDeviceInfo device = new AspiDeviceInfo();
								
								int bus = 0;
								if (int.TryParse(line.Substring(POS_BUS, LEN_BUS), out bus))
									device.Bus = bus;
								
								int id = 0;
								if (int.TryParse(line.Substring(POS_ID, LEN_ID), out id))
									device.Id = id;
								
								int lun = 0;
								if (int.TryParse(line.Substring(POS_LUN, LEN_LUN), out lun))
									device.Lun = lun;
									
								device.Manufacturer = line.Substring(POS_MANUFAC, LEN_MANUFAC).TrimEnd(' ');
								device.Model = line.Substring(POS_MODEL, LEN_MODEL).TrimEnd(' ');
								device.Revision = line.Substring(POS_REV, LEN_REV).TrimEnd(' ');
								device.Type = line.Substring(POS_TYPE, LEN_TYPE).TrimEnd(' ','\r');
								
								devices.Add(device);
							}
						}
					}
				}
			}
		}
		#endregion
		
		#region Public Static Methods
		public static IList<AspiDeviceInfo> GetDevices()
		{
			IList<AspiDeviceInfo> devices = new List<AspiDeviceInfo>();
			
			try
			{
				Process scanProcess = new Process();
				scanProcess.StartInfo.CreateNoWindow = true;
				scanProcess.StartInfo.UseShellExecute = false;
				scanProcess.StartInfo.RedirectStandardOutput = true;
				scanProcess.StartInfo.FileName = FILE_NAME;
				scanProcess.Start();
				
				string output = scanProcess.StandardOutput.ReadToEnd();
				scanProcess.WaitForExit();
				
				ParseOutput(output, devices);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			
			return devices;
		}
		#endregion
	}
}
