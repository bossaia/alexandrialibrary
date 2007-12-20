using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Alexandria.FreeDB
{
	internal class DeviceChangeNotificationWindow : NativeWindow
	{
		public event DeviceChangeEventHandler DeviceChange;

		const int WS_EX_TOOLWINDOW = 0x80;
		const int WS_POPUP = unchecked((int)0x80000000);

		const int WM_DEVICECHANGE = 0x0219;

		const int DBT_APPYBEGIN = 0x0000;
		const int DBT_APPYEND = 0x0001;
		const int DBT_DEVNODES_CHANGED = 0x0007;
		const int DBT_QUERYCHANGECONFIG = 0x0017;
		const int DBT_CONFIGCHANGED = 0x0018;
		const int DBT_CONFIGCHANGECANCELED = 0x0019;
		const int DBT_MONITORCHANGE = 0x001B;
		const int DBT_SHELLLOGGEDON = 0x0020;
		const int DBT_CONFIGMGAPI32 = 0x0022;
		const int DBT_VXDINITCOMPLETE = 0x0023;
		const int DBT_VOLLOCKQUERYLOCK = 0x8041;
		const int DBT_VOLLOCKLOCKTAKEN = 0x8042;
		const int DBT_VOLLOCKLOCKFAILED = 0x8043;
		const int DBT_VOLLOCKQUERYUNLOCK = 0x8044;
		const int DBT_VOLLOCKLOCKRELEASED = 0x8045;
		const int DBT_VOLLOCKUNLOCKFAILED = 0x8046;
		const int DBT_DEVICEARRIVAL = 0x8000;
		const int DBT_DEVICEQUERYREMOVE = 0x8001;
		const int DBT_DEVICEQUERYREMOVEFAILED = 0x8002;
		const int DBT_DEVICEREMOVEPENDING = 0x8003;
		const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
		const int DBT_DEVICETYPESPECIFIC = 0x8005;

		public DeviceChangeNotificationWindow()
		{
			CreateParams Params = new CreateParams();
			Params.ExStyle = WS_EX_TOOLWINDOW;
			Params.Style = WS_POPUP;
			CreateHandle(Params);
		}

		private void OnCDChange(DeviceChangeEventArgs ea)
		{
			if (DeviceChange != null)
			{
				DeviceChange(this, ea);
			}
		}
		private void OnDeviceChange(DeviceBroadcastVolume DevDesc, DeviceChangeEventType EventType)
		{
			if (DeviceChange != null)
			{
				foreach (char ch in DevDesc.Drives)
				{
					DeviceChangeEventArgs a = new DeviceChangeEventArgs(ch, EventType);
					DeviceChange(this, a);
				}
			}
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == WM_DEVICECHANGE)
			{
				DeviceBroadcastHeader head;
				switch (m.WParam.ToInt32())
				{
					/*case DBT_DEVNODES_CHANGED :
					  break;
					case DBT_CONFIGCHANGED :
					  break;*/
					case DBT_DEVICEARRIVAL:
						head = (DeviceBroadcastHeader)Marshal.PtrToStructure(m.LParam, typeof(DeviceBroadcastHeader));
						if (head.DeviceType == DeviceType.LogicalVolume)
						{
							DeviceBroadcastVolume DevDesc = (DeviceBroadcastVolume)Marshal.PtrToStructure(m.LParam, typeof(DeviceBroadcastVolume));
							if (DevDesc.Flags == VolumeChangeFlags.Media)
							{
								OnDeviceChange(DevDesc, DeviceChangeEventType.DeviceInserted);
							}
						}
						break;
					/*case DBT_DEVICEQUERYREMOVE :
					  break;
					case DBT_DEVICEQUERYREMOVEFAILED :
					  break;
					case DBT_DEVICEREMOVEPENDING :
					  break;*/
					case DBT_DEVICEREMOVECOMPLETE:
						head = (DeviceBroadcastHeader)Marshal.PtrToStructure(m.LParam, typeof(DeviceBroadcastHeader));
						if (head.DeviceType == DeviceType.LogicalVolume)
						{
							DeviceBroadcastVolume DevDesc = (DeviceBroadcastVolume)Marshal.PtrToStructure(m.LParam, typeof(DeviceBroadcastVolume));
							if (DevDesc.Flags == VolumeChangeFlags.Media)
							{
								OnDeviceChange(DevDesc, DeviceChangeEventType.DeviceRemoved);
							}
						}
						break;
					/*case DBT_DEVICETYPESPECIFIC :
					  break;*/
				}
			}
			base.WndProc(ref m);
		}
	}
}
