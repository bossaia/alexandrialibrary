using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Alexandria.MusicDns
{
	internal static class NativeMethods
	{
		/// <summary>
		/// Get a PUID based on the raw audio data from a given track
		/// </summary>
		/// <param name="samples">At least the first 135 seconds worth of samples of the audio file as an array of bytes</param>
		/// <param name="byteOrder">0 for little-endian or 1 for big-endian</param>
		/// <param name="numberOfSamples">Number of samples (half the number of bytes if stereo = 1)</param>
		/// <param name="sampleRate">Sample rate (44100 is typical)</param>
		/// <param name="stereo">1 if stereo, 0 otherwise</param>
		/// <returns>The PUID as a string</returns>
		[DllImport("libofa.dll", EntryPoint = "ofa_create_print", ExactSpelling = false, CallingConvention = CallingConvention.Cdecl)]
		public static extern string ofa_create_print([MarshalAs(UnmanagedType.LPArray)] byte[] samples, int byteOrder, int size, int sRate, bool stereo);

		//[DllImport("libofa.dll")] //, EntryPoint="ofa_create_print", ExactSpelling=false, CallingConvention=CallingConvention.Cdecl)]
		//internal static extern string ofa_create_print([MarshalAs(UnmanagedType.LPArray)] byte[] samples, int byteOrder, long numberOfSamples, int sampleRate, int stereo);
		
		
		/// <summary>
		/// Get the version of the Open Fingerprint 
		/// </summary>
		/// <param name="major">Major number</param>
		/// <param name="minor">Minor number</param>
		/// <param name="revision">Revision number</param>
		[DllImport("libofa.dll")]
		internal static extern void ofa_get_version(ref int major, ref int minor, ref int revision);
	}
}
