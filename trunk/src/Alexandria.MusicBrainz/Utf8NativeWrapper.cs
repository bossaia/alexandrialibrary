#region License (MIT)
/*
Copyright (c) 2007 Dan Poage

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace Alexandria.MusicBrainz
{
	internal static class Utf8NativeWrapper //: IDisposable
	{
		#region Constructors
		/*
		[EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted=true)]
		public Utf8NativeWrapper(string text)
		{
			if (text == null)
				handle = IntPtr.Zero;
			else
			{				
				byte[] bytes = encoding.GetBytes(text);
				IntPtr nativeBytes = Marshal.AllocHGlobal(text.Length + 1);
				Marshal.Copy(bytes, 0, nativeBytes, bytes.Length);
				Marshal.WriteByte(nativeBytes, text.Length, (byte)0);
				this.handle = nativeBytes;
			}
		}
		*/
		#endregion

		#region Finalizer
		/*
		[EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		~Utf8NativeWrapper()
		{
			Dispose(false);
		}
		*/
		#endregion

		#region IDisposable Members
		/*
		[EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (handle != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(handle);
				}
			}
		}
		
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		*/
		#endregion
	
		#region Private Fields
		//private IntPtr handle = IntPtr.Zero;
		#endregion
		
		#region Private Static Fields
		private static UTF8Encoding encoding = new UTF8Encoding();
		#endregion
				
		#region Public Properties
		/*
		public IntPtr Handle
		{
			get {return handle;}
		}
		*/
		#endregion
		
		#region Public Static Methods
		[EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public static IntPtr GetHandle(string text)
		{
			IntPtr handle = IntPtr.Zero;
		
			if (text != null)
			{				
				byte[] bytes = encoding.GetBytes(text);
				IntPtr nativeBytes = Marshal.AllocHGlobal(text.Length + 1);
				Marshal.Copy(bytes, 0, nativeBytes, bytes.Length);
				Marshal.WriteByte(nativeBytes, text.Length, (byte)0);
				handle = nativeBytes;
			}
			
			return handle;
		}

		[EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public static void FreeHandle(IntPtr handle)
		{
			if (handle != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(handle);
			}
		}
		#endregion
	}
}
