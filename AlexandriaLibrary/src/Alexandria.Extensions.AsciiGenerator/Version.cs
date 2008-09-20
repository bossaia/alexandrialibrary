using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.AsciiGenerator
{
	/// <summary>
	/// Class Holding the current application version
	/// </summary>
	public abstract class Version
	{
		/// <summary>
		/// Build and return the current application version
		/// </summary>
		/// <returns>The current version as a string</returns>
		public static string GetVersion()
		{
			return Major.ToString() + "." + Minor.ToString() + "." + Patch.ToString() + Suffix;
		}

		/// <summary>Major version number</summary>
		public const int Major = 0;

		/// <summary>Minor version number</summary>
		public const int Minor = 7;

		/// <summary>Patch version number</summary>
		public const int Patch = 2;

		/// <summary>Version Suffix</summary>
		public const string Suffix = "";

		/// <summary>Version Suffix Number</summary>
		public const int SuffixNumber = 1;
	}
}