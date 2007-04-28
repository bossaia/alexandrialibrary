using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Alexandria.MusicDns
{
	internal static class NativeMethods
	{
		/// <summary>
		/// The key to use for submitting data
		/// </summary>
		internal const string key = "5d49e8b0deb040bc8cd38630b56be205";
	
		/*
		/// <summary>
		/// Get a PUID based on the raw audio data from a given track
		/// </summary>
		/// <param name="samples">The first 135 seconds of the audio file as an array of bytes</param>
		/// <param name="byteOrder">0 for little-endian or 1 for big-endian</param>
		/// <param name="size">Number of samples (half the number of bytes if stereo = 1)</param>
		/// <param name="sampleRate">Sample rate (44100 is typical)</param>
		/// <param name="stereo">1 if stereo, 0 otherwise</param>
		/// <returns>The PUID as a string</returns>
		[DllImport("libofa.dll")]
		internal static extern string ofa_create_print(byte[] samples, int byteOrder, long size, int sampleRate, int stereo);
		
		/// <summary>
		/// Get the version of the Open Fingerprint 
		/// </summary>
		/// <param name="major">Major version number</param>
		/// <param name="minor">Minor version number</param>
		/// <param name="revision">Revision number</param>
		[DllImport("libofa.dll")]
        internal static extern void ofa_get_version(ref int major, ref int minor, ref int revision);
        */
	}
}
