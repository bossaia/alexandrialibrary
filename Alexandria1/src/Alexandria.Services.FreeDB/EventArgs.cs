using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.FreeDB
{
	#region DataReadEventArgs
	public class DataReadEventArgs : EventArgs
	{
		#region Private fields
		private byte[] data = null;
		private uint size = 0;
		#endregion
		
		#region Constructors
		public DataReadEventArgs(byte[] data, uint size)
		{
			this.data = data;
			this.size = size;
		}
		#endregion
		
		#region Public properties
		
		#region Data
		public byte[] Data
		{
			get {return data;}
		}
		#endregion
		
		#region Size
		public uint Size
		{
			get	{return size;}
		}
		#endregion
		
		#endregion
	}
	#endregion
	
	#region ReadProgressEventArgs
	public class ReadProgressEventArgs : EventArgs
	{
		#region Private fields
		private uint bytesToRead = 0;
		private uint bytesRead = 0;
		private bool cancelRead = false;
		#endregion
		
		#region Constructors
		public ReadProgressEventArgs(uint bytesToRead, uint bytesRead)
		{
			this.bytesToRead = bytesToRead;
			this.bytesRead = bytesRead;
		}
		#endregion
		
		#region Public properties
		
		#region BytesToRead
		public uint BytesToRead
		{
			get {return bytesToRead;}
		}
		#endregion
		
		#region BytesRead
		public uint BytesRead
		{
			get{return bytesRead;}
		}
		#endregion
		
		#region CancelRead
		public bool CancelRead
		{
			get {return cancelRead;}
			set {cancelRead = value;}
		}
		#endregion
		
		#endregion
	}
	#endregion
	
	#region DeviceChangeEventArgs
	internal class DeviceChangeEventArgs : EventArgs
	{
		#region Private fields
		private char driveLetter;
		private DeviceChangeEventType changeType;		
		#endregion
		
		#region Constructors
		public DeviceChangeEventArgs(char driveLetter, DeviceChangeEventType changeType)
		{
			this.driveLetter = driveLetter;
			this.changeType = changeType;
		}
		#endregion
		
		#region Public properties
		
		#region DriveLetter
		public char DriveLetter
		{
			get	{return driveLetter;}
		}
		#endregion
		
		#region ChangeType
		public DeviceChangeEventType ChangeType
		{
			get {return changeType;}
		}
		#endregion
		
		#endregion
	}
	#endregion
}
