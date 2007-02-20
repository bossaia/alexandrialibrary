using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace AlexandriaOrg.Alexandria.MusicBrainz
{
	internal class WindowsNetworkControl : IWindowsNetworkControl
	{
		#region Public IWindowsNetworkControl Methods
		/// <summary>
		/// Initialize the network control
		/// </summary>
		/// <param name="handle">The MusicBrainz object to use for initializing the network control</param>
		public void Init(IntPtr handle)
		{
			NativeMethods.mb_WSAInit(handle);
		}
		
		/// <summary>
		/// Stop the network control
		/// </summary>
		/// <param name="handle">The MusicBrainz object to use for stopping the network control</param>
		public void Stop(IntPtr handle)
		{
			NativeMethods.mb_WSAStop(handle);
		}
		#endregion
	}
}
