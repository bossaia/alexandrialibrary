// MediaInfoDLL - All info about media files, for DLL
// Copyright (C) 2002-2006 Jerome Martinez, Zen@MediaArea.net
//
// Modifications (C) 2007 Dan Poage dan.poage@gmail.com
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// MediaInfoDLL - All info about media files, for DLL
// Copyright (C) 2002-2006 Jerome Martinez, Zen@MediaArea.net
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//
// Microsoft Visual C# wrapper for MediaInfo Library
// See MediaInfo.h for help
//
// To make it working, you must put MediaInfo.Dll
// in the executable folder
//
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

using System;
using System.Runtime.InteropServices;

namespace Alexandria.MediaInfo
{
	public class Info : IDisposable
	{
		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public Info()
		{
			handle = NativeMethods.MediaInfo_New();
		}
		#endregion
		
		#region Finalizer
		~Info()
		{
			Dispose(false);
		}
		#endregion
		
		#region IDisposable Members
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					// Dispose managed resources here
				}
				
				if (handle != IntPtr.Zero)
				{
					NativeMethods.MediaInfo_Delete(handle);
				}
			}
			disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
		
		#region Private Fields
		private IntPtr handle;
		private bool disposed;
		#endregion
		
		#region Public Methods
		
		#region Open
		public int Open(string FileName)
		{
			return NativeMethods.MediaInfo_Open(handle, FileName);
		}
		#endregion
		
		#region Close
		public void Close()
		{
			NativeMethods.MediaInfo_Close(handle);
		}
		#endregion
		
		#region Inform
		public string Inform()
		{
			return Marshal.PtrToStringUni(NativeMethods.MediaInfo_Inform(handle, 0));
		}
		#endregion
		
		#region Get
		public string Get(StreamType streamType, uint streamNumber, string parameter, InfoType infoType, InfoType searchType)
		{
			return Marshal.PtrToStringUni(NativeMethods.MediaInfo_Get(handle, streamType, streamNumber, parameter, infoType, searchType));
		}
		
		public string Get(StreamType streamType, uint streamNumber, uint parameter, InfoType infoType)
		{
			return Marshal.PtrToStringUni(NativeMethods.MediaInfo_GetI(handle, streamType, streamNumber, parameter, infoType));
		}

		public string Get(StreamType streamType, uint streamNumber, string parameter, InfoType infoType)
		{
			return Get(streamType, streamNumber, parameter, infoType, InfoType.Name);
		}
		
		public string Get(StreamType streamType, uint streamNumber, string parameter)
		{
			return Get(streamType, streamNumber, parameter, InfoType.Text, InfoType.Name);
		}
		
		public string Get(StreamType streamType, uint streamNumber, uint parameter)
		{
			return Get(streamType, streamNumber, parameter, InfoType.Text);
		}
		#endregion
		
		#region Option
		public string Option(string option, string value)
		{
			return Marshal.PtrToStringUni(NativeMethods.MediaInfo_Option(handle, option, value));
		}
		
		public string Option(string option)
		{
			return Option(option, string.Empty);
		}		
		#endregion
		
		#region GetState
		public int GetState()
		{
			return NativeMethods.MediaInfo_State_Get(handle);
		}
		#endregion
		
		#region GetCount
		public int GetCount(StreamType streamKind, int streamNumber)
		{
			return NativeMethods.MediaInfo_Count_Get(handle, streamKind, streamNumber);
		}
		
		public int GetCount(StreamType streamKind)
		{
			return GetCount(streamKind, -1);
		}
		#endregion
		
		#endregion		
	}
}

