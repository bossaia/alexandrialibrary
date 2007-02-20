using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace AlexandriaOrg.Alexandria.FreeDB
{
	/// <summary>
	/// Prevent media removal
	/// </summary>
	/// <remarks>PREVENT_MEDIA_REMOVAL</remarks>
	[StructLayout(LayoutKind.Sequential)]
	public class PreventMediaRemoval
	{
		public byte Lock = 0;
	}
}
