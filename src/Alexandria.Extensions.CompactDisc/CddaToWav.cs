using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Telesophy.Alexandria.Extensions.CompactDisc
{
	public static class CddaToWave
	{
		#region Private Constants
		private const string FILE_NAME = "cdda2wav.exe";
		private const string FLAG_BATCH = "-B";
		private const string FLAG_NO_INFO = "-H";
		private const string FLAG_DEVICE = "dev=";
		private const string FLAG_TRACK = "track=";
		private const string FORMAT_TRACK_PATH = @"Rip\Track{0}.wav";
		#endregion
	
		#region Private Static Methods
		private static void Rip(AspiDeviceInfo deviceInfo, int trackNumber, bool batchAllTracks, bool createInfoFiles)
		{
			try
			{
				Process ripProcess = new Process();
				ripProcess.StartInfo.CreateNoWindow = true;
				ripProcess.StartInfo.UseShellExecute = false;
				//ripProcess.StartInfo.RedirectStandardOutput = true;
				ripProcess.StartInfo.FileName = FILE_NAME;
				ripProcess.StartInfo.Arguments = GetArguments(deviceInfo, batchAllTracks, createInfoFiles, trackNumber);
				ripProcess.Start();

				//string output = ripProcess.StandardOutput.ReadToEnd();
				//ripProcess.WaitForExit();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		} 

		
		private static string GetArguments(AspiDeviceInfo deviceInfo, bool batchAllTracks, bool createInfoFiles, int trackNumber)
		{
			StringBuilder arguments = new StringBuilder();
			
			if (batchAllTracks)
				arguments.AppendFormat("{0} ", FLAG_BATCH);
			else
				arguments.AppendFormat("{0}{1} ", FLAG_TRACK, trackNumber);
				
			if (!createInfoFiles)
				arguments.AppendFormat("{0} ", FLAG_NO_INFO);
			
			arguments.AppendFormat("{0}{1} ", FLAG_DEVICE, deviceInfo);
			
			if (!batchAllTracks)
				arguments.AppendFormat(FORMAT_TRACK_PATH, trackNumber);
			
			return arguments.ToString();
		}
		#endregion
	
		#region Public Static Methods
		public static void RipTrack(AspiDeviceInfo deviceInfo, int trackNumber)
		{
			Rip(deviceInfo, trackNumber, false, false);
		}
		#endregion
	}
}
