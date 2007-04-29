using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.FreeDB
{
	public delegate void CdDataReadEventHandler(object sender, DataReadEventArgs ea);
	public delegate void CdReadProgressEventHandler(object sender, ReadProgressEventArgs ea);
	internal delegate void DeviceChangeEventHandler(object sender, DeviceChangeEventArgs ea);
}
