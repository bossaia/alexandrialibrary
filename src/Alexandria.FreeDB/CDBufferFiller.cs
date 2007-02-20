using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.FreeDB
{
	public class CDBufferFiller
	{
		#region Private fields
		private byte[] bufferArray;
		private int writePosition = 0;
		#endregion

		#region Constructors
		public CDBufferFiller(byte[] bufferArray)
		{
			this.bufferArray = bufferArray;
		}
		#endregion
		
		#region Public properties
		
		#region BufferArray
		public byte[] BufferArray
		{
			get {return bufferArray;}
		}
		#endregion
		
		#region WritePosition
		public int WritePosition
		{
			get {return writePosition;}
			set {writePosition = value;}
		}
		#endregion
		
		#endregion
		
		#region Public methods
		public void OnCDDataRead(object sender, DataReadEventArgs ea)
		{
			Buffer.BlockCopy(ea.Data, 0, BufferArray, WritePosition, (int)ea.Size);
			WritePosition += (int)ea.Size;
		}
		#endregion
	}
}
