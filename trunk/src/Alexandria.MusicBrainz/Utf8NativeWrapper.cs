using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace AlexandriaOrg.Alexandria.MusicBrainz
{
	internal static class Utf8NativeWrapper //: IDisposable
	{
		#region Private Fields
		//private IntPtr handle = IntPtr.Zero;
		#endregion
		
		#region Private Static Fields
		private static UTF8Encoding encoding = new UTF8Encoding();
		#endregion
			
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
