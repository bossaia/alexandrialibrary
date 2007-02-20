using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.MusicBrainz
{
	internal interface IWindowsNetworkControl
	{
		void Init(IntPtr musicBrainzObject);
		void Stop(IntPtr musicBrainzObject);
	}
}
