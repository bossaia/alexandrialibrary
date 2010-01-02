#region License (LGPL)
/* --------------------------------------------------------------------------

   Copyright (C) 2004 Sean Cier
   Copyright (C) 2000 Robert Kaye
   Copyright (C) Relatable

   This library is free software; you can redistribute it and/or
   modify it under the terms of the GNU Lesser General Public
   License as published by the Free Software Foundation; either
   version 2.1 of the License, or (at your option) any later version.

   This library is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
   Lesser General Public License for more details.

   You should have received a copy of the GNU Lesser General Public
   License along with this library; if not, write to the Free Software
   Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

----------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace Telesophy.Alexandria.MusicBrainz
{
	public class Trm : IDisposable
	{
		#region Private Fields
		private IntPtr handle;
		#endregion

		#region Private Static Fields
		private static readonly Encoding asciiEncoding = new ASCIIEncoding();
		private static readonly Encoding utf8Encoding = new UTF8Encoding();
		#endregion
	
		#region Constructors
		public Trm()
		{
			handle = NativeMethods.trm_New();
		}
		#endregion

		#region IDisposable Members
		~Trm()
		{
			Dispose(false);
		}

		protected virtual void Dispose(bool disposing)
		{
			NativeMethods.trm_Delete(handle);
		}
		
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
		
		#region Public Methods
		public void SetProxy(String proxyAddress, short proxyPort)
		{
			NativeMethods.trm_SetProxy(handle, ToUtf8(proxyAddress), proxyPort);
		}

		public bool SetPcmDataInfo(int samplesPerSecond, int numberOfChannels, int bitsPerSample)
		{
			int result = NativeMethods.trm_SetPCMDataInfo(handle, samplesPerSecond, numberOfChannels, bitsPerSample);
			return (result != 0);
		}

		public void SetSongLength(long seconds)
		{
			NativeMethods.trm_SetSongLength(handle, seconds);
		}

		public bool GenerateSignature(byte[] data)
		{
			if (data != null)
				return GenerateSignature(data, data.Length);
			else
				return false;
		}

		public bool GenerateSignature(byte[] data, int size)
		{
			int result = NativeMethods.trm_GenerateSignature(handle, data, size);
			return (result != 0);
		}

		public byte[] FinalizeSignature()
		{
			return FinalizeSignature(null);
		}

		public byte[] FinalizeSignature(string collectionId)
		{
			byte[] signature;
		
			// Create a MusicBrainz instance for the sole purpose of ensuring
			// a session is created and WSA_Init is called if neccessary.
			// Calls to WSA_Init are nestable, and cleanup will occur automatically
			// after this instance goes out-of-scope.
			MusicBrainzClient mb = new MusicBrainzClient();

			signature = new byte[17];
			int result = NativeMethods.trm_FinalizeSignature(handle, signature, (collectionId == null) ? null : ToUtf8(collectionId));

			// Unlike most mb_* methods, trm_FinalizeSignature returns 0 on success
			if (result == 0)
				return signature;
			else
				return null;
		}

		public string ConvertSignatureToAscii(byte[] signature)
		{
			byte[] asciiNativeSignature = new byte[37];
			NativeMethods.trm_ConvertSigToASCII(handle, signature, asciiNativeSignature);
			return FromAscii(asciiNativeSignature);
		}
		#endregion
		
		#region Public Static Methods
		public static byte[] ToAscii(string value)
		{
			if (value == null)
			{
				return null;
			}
		
			int length = asciiEncoding.GetByteCount(value);
			byte[] result = new byte[length];
			asciiEncoding.GetBytes(value, 0, value.Length, result, 0);
			result[length-1] = 0;
			return result;
		}

		public static string FromAscii(byte[] bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			int length = 0;
			while ((length < bytes.Length) && (bytes[length] != 0)) length++;
		    
			return asciiEncoding.GetString(bytes, 0, length);
		}

		public static byte[] ToUtf8(string value)
		{
			if (value == null)
			{
				return null;
			}
			
			int length = utf8Encoding.GetByteCount(value);
			byte[] result = new byte[length];
			utf8Encoding.GetBytes(value, 0, value.Length, result, 0);
			result[length-1] = 0;
			return result;
		}

		public static string FromUtf8(byte[] bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			
			int length = 0;
			while ((length < bytes.Length) && (bytes[length] != 0)) length++;    
			return utf8Encoding.GetString(bytes, 0, length);
		}
		#endregion				
	}
}