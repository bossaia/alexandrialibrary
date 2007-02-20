/* --------------------------------------------------------------------------

   Copyright (C) 2004 Sean Cier
   Copyright (C) 2000 Robert Kaye

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

using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace AlexandriaOrg.Alexandria.MusicBrainz
{
	#region Public Inner Classes
	
	#region ServerInfo
	/*
	public class ServerInfo
	{
		#region Private Fields
		private string address;
		private short port;
		#endregion

		#region Constructors
		public ServerInfo(string address, short port)
		{
			this.address = address;
			this.port = port;
		}
		#endregion

		#region Public Properties
		public string Address
		{
			get { return address; }
		}

		public short Port
		{
			get { return port; }
		}
		#endregion
	}
	*/
	#endregion
	
	#endregion
	
	#region Public Structs
	/*
	#region BitprintInfo_Native
	[StructLayout(LayoutKind.Sequential)]
	public struct BitprintInfo_Native
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
		public byte[] filename;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 89)]
		public byte[] bitprint;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 41)]
		public byte[] first20;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 41)]
		public byte[] audioSha1;
		public uint length;
		public uint duration;
		public uint samplerate;
		public uint bitrate;
		public byte stereo;
		public byte vbr;
	}
	#endregion
	*/
	#endregion
	
	/*
	public class OldClient : IDisposable
	{	
		#region Private Constants
		private const string dllName = "musicbrainz.dll";
		#endregion
			
		#region Private Fields
		private IntPtr handle;
		private int currentResult = 0;
		private ClientVersion version = ClientVersion.Default;
		private ServerInfo server = new ServerInfo();
		private ServerInfo proxyServer = new ServerInfo();
		private string userName = null;
		private string password = null;
		private string device = null;
		private int depth = 0;
		private int maximumItems = 0;
		private string queryError = null;
		private string webSubmitUrl = null;
		#endregion
		
		#region Private Static Fields
		private static readonly Encoding UTF8_ENCODING = new UTF8Encoding();
		private static int MAX_STRING_LEN = 8192;
		#endregion

		#region Private Static Properties
		/// <summary>
		/// Get a value indicating whether or not this session is running on a Microsoft Windows operating system
		/// </summary>
		private static bool IsWindows
		{
			get
			{
				PlatformID platform = System.Environment.OSVersion.Platform;
				PlatformID winPlatformMask = PlatformID.Win32S | PlatformID.Win32Windows | PlatformID.Win32NT;
				return (int)(platform & winPlatformMask) != 0;
			}
		}
		#endregion

		#region Private Methods
		private void BeginSession()
		{
			if (IsWindows)
			{
				try
				{
					WindowsNetworkControl networkControl = new WindowsNetworkControl();
					networkControl.Init(handle);
				
					//System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
					//System.Diagnostics.Debug.WriteLine("Executing assembly name: " + asm.FullName);
				
					//TODO: determine why we can load this type
					//Type type = Type.GetType("musicbrainz.MusicBrainz+WindowsNetworkControl");
					//if (type != null) System.Diagnostics.Debug.WriteLine("Found networkcontrol type");
					//else System.Diagnostics.Debug.WriteLine("Did not found networkcontrol type");
					//object networkControl = type.GetConstructor(new Type[0]).Invoke(new Object[0]);
					//((IWindowsNetworkControl)networkControl).Init(handle);
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine("Error stating session: " + ex.Message);
				}
			}
		}

		private void EndSession()
		{
			if (IsWindows)
			{
				WindowsNetworkControl networkControl = new WindowsNetworkControl();
				networkControl.Stop(handle);
				//Type type = Type.GetType("musicbrainz.MusicBrainz+WindowsNetworkControl");
				//object networkControl = type.GetConstructor(new Type[0]).Invoke(new Object[0]);
				//((IWindowsNetworkControl)networkControl).Stop(handle);
			}
		}
		#endregion
		
		#region Private Static Methods
		
		#region DllImport Methods
		[DllImport(dllName)]
		private static extern IntPtr mb_New();
		
		[DllImport(dllName)]
		private static extern void mb_Delete(IntPtr handle);
		
		[DllImport(dllName)]
		private static extern void mb_UseUTF8(IntPtr handle, int value);

		[DllImport(dllName)]
		private static extern void mb_GetVersion(IntPtr handle, out int majorVersion, out int minorVersion, out int buildVersion);

		[DllImport(dllName)]
		private static extern int mb_SetServer(IntPtr handle, byte[] serverAddress, short serverPort);

		[DllImport(dllName)]
		private static extern int mb_SetProxy(IntPtr handle, byte[] serverAddress, short serverPort);

		[DllImport(dllName)]
		private static extern int mb_Authenticate(IntPtr handle, byte[] userName, byte[] password);

		[DllImport(dllName)]
		private static extern int mb_SetDevice(IntPtr handle, byte[] device);

		[DllImport(dllName)]
		private static extern void mb_SetDepth(IntPtr handle, int depth);

		[DllImport(dllName)]
		private static extern void mb_SetMaxItems(IntPtr handle, int maxItems);

		[DllImport(dllName)]
		private static extern int mb_Query(IntPtr handle, byte[] rdfObject);

		[DllImport(dllName)]
		private static extern int mb_QueryWithArgs(IntPtr handle, byte[] rdfObject, IntPtr[] args);

		[DllImport(dllName)]
		private static extern int mb_GetQueryError(IntPtr handle, byte[] error, int errorLength);

		[DllImport(dllName)]
		private static extern int mb_GetWebSubmitURL(IntPtr handle, byte[] url, int urlLength);

		[DllImport(dllName)]
		private static extern int mb_Select1(IntPtr handle, byte[] selectQuery, int ordinal);

		[DllImport(dllName)]
		private static extern int mb_SelectWithArgs(IntPtr handle, byte[] selectQuery, int[] ordinals);

		[DllImport(dllName)]
		private static extern int mb_DoesResultExist1(IntPtr handle, byte[] resultName, int ordinal);

		[DllImport(dllName)]
		private static extern int mb_GetResultData1(IntPtr handle, byte[] resultName, byte[] data, int dataLength, int ordinal);

		[DllImport(dllName)]
		private static extern int mb_GetResultInt1(IntPtr handle, byte[] resultName, int ordinal);

		[DllImport(dllName)]
		private static extern int mb_GetResultRDF(IntPtr handle, byte[] rdf, int rdfLength);
		
		[DllImport(dllName)]
		private static extern int mb_GetResultRDFLen(IntPtr handle);

		[DllImport(dllName)]
		private static extern int mb_SetResultRDF(IntPtr handle, byte[] rdf);

		[DllImport(dllName)]
		private static extern void mb_GetIDFromURL(IntPtr handle, byte[] url, byte[] id, int idLength);

		[DllImport(dllName)]
		private static extern void mb_GetFragmentFromURL(IntPtr handle, byte[] url, byte[] fragment, int fragmentLength);

		[DllImport(dllName)]
		private static extern int mb_GetOrdinalFromList(IntPtr handle, byte[] resultList, byte[] uri);

		[DllImport(dllName)]
		private static extern int mb_GetMP3Info(IntPtr handle, byte[] fileName, out int duration, out int bitrate, out int stereo, out int samplerate);

		[DllImport(dllName)]
		private static extern void mb_SetDebug(IntPtr handle, int debug);
		#endregion

		#region UTF-8 Methods
		private static byte[] ToUtf8(string value)
		{
			if (value == null) return null;
			
			int length = UTF8_ENCODING.GetByteCount(value);
			byte[] result = new byte[length + 1];
			UTF8_ENCODING.GetBytes(value, 0, value.Length, result, 0);
			result[length] = 0;
			return result;
		}

		private static string FromUtf8(byte[] bytes)
		{
			if (bytes == null) return null;
			
			int length = 0;
			while ((length < bytes.Length) && (bytes[length] != 0))	length++;
						
			return UTF8_ENCODING.GetString(bytes, 0, length);
		}

		private static IntPtr ToUtf8Native(String value)
		{
			if (value == null) return (IntPtr)0;
			
			byte[] bytes = UTF8_ENCODING.GetBytes(value);
			IntPtr nativeBytes = Marshal.AllocHGlobal(bytes.Length + 1);
			Marshal.Copy(bytes, 0, nativeBytes, bytes.Length);
			Marshal.WriteByte(nativeBytes, bytes.Length, (byte)0);
			return nativeBytes;
		}

		private static void FreeUtf8Native(IntPtr nativeBytes)
		{
			if (nativeBytes == (IntPtr)0) return;
			
			Marshal.FreeHGlobal(nativeBytes);
		}

		private static string FromUtf8Native(IntPtr nativeBytes, int maximumLength)
		{
			if (nativeBytes == (IntPtr)0)
			{
				return null;
			}
			byte[] bytes = new byte[maximumLength];
			Marshal.Copy(nativeBytes, bytes, 0, maximumLength);
			return FromUtf8(bytes);
		}
		#endregion
		
		#endregion
		
		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public OldClient()
		{
			handle = mb_New();
			mb_UseUTF8(handle, 1);
			BeginSession();
		}
		#endregion

		#region IDisposable Members
		public void Dispose()
		{
			EndSession();
			mb_Delete(handle);
		}
		#endregion

		#region Public Static Fields
		public static readonly int CDINDEX_ID_LEN = 28;
		public static readonly int ID_LEN = 36;
		#endregion

		#region Public Properties
		public IntPtr Handle
		{
			get {return handle;}
		}
		
		public int CurrentResult
		{
			get {return currentResult;}
		}
		
		public ClientVersion Version
		{
			get
			{
				if (version.Major == 0 && version.Minor == 0 && version.Revision == 0)
				{
					int majorVersion, minorVersion, buildVersion = 0;
					mb_GetVersion(handle, out majorVersion, out minorVersion, out buildVersion);
					version = new ClientVersion(majorVersion, minorVersion, buildVersion);
				}
				return version;
			}
		}		
		
		public ServerInfo Server
		{
			get {return server;}
			set
			{
				server = value;				
				currentResult = mb_SetServer(handle, ToUtf8(server.Address), server.Port);
			}
		}
		
		public ServerInfo ProxyServer
		{
			get {return proxyServer;}
			set
			{
				proxyServer = value;
				currentResult = mb_SetProxy(handle, ToUtf8(proxyServer.Address), proxyServer.Port);
			}
		}
		
		public string Device
		{
			get {return device;}
			set
			{
				device = value;
				currentResult = mb_SetDevice(handle, ToUtf8(device));
			}
		}
		
		public int Depth
		{
			get {return depth;}
			set
			{
				depth = value;
				mb_SetDepth(handle, depth);
			}
		}
		
		public int MaximumItems
		{
			get {return maximumItems;}
			set
			{
				maximumItems = value;
				mb_SetMaxItems(handle, maximumItems);
			}
		}
		
		public string QueryError
		{
			get
			{
				byte[] errorNative = new byte[MAX_STRING_LEN];

				currentResult = mb_GetQueryError(handle, errorNative, MAX_STRING_LEN);
				queryError = (currentResult == 0) ? null : FromUtf8(errorNative);
				return queryError;
			}
		}
		
		public string WebSubmitUrl
		{
			get
			{
				byte[] webSubmitUrlNative = new byte[MAX_STRING_LEN];

				currentResult = mb_GetWebSubmitURL(handle, webSubmitUrlNative, MAX_STRING_LEN);
				webSubmitUrl = (currentResult == 0) ? null : FromUtf8(webSubmitUrlNative);
				return webSubmitUrl;
			}
		}
		#endregion
		
		#region Public Methods
		
		#region Authenticate
		public bool Authenticate(string userName, string password)
		{
			this.userName = userName;
			this.password = password;
			currentResult = mb_Authenticate(handle, ToUtf8(userName), ToUtf8(password));
			return (currentResult != 0);
		}
		#endregion

		#region Query
		public bool Query(string rdfObject)
		{
			int result = mb_Query(handle, ToUtf8(rdfObject));
			return (result != 0);
		}

		public bool Query(string rdfObject, String[] args)
		{
			IList argsList = new ArrayList();
			
			foreach (String arg in args) argsList.Add(arg);
						
			return Query(rdfObject, argsList);
		}
		
		public bool Query(string rdfObject, IList args)
		{
			if (args == null)
			{
				return Query(rdfObject);
			}

			IntPtr[] argsNative = new IntPtr[args.Count+1];
			for (int i = 0; i < args.Count; i++) argsNative[i] = ToUtf8Native((String)args[i]);
			
			argsNative[args.Count] = (IntPtr)0;

			int result = mb_QueryWithArgs(handle, ToUtf8(rdfObject), argsNative);

			for (int i = 0; i < args.Count; i++) FreeUtf8Native(argsNative[i]);		

			return (result != 0);
		}
		#endregion

		#region Select
		public bool Select(String selectQuery)
		{
			return Select(selectQuery, 0);
		}
		
		public bool Select(String selectQuery, int index)
		{
			int result = mb_Select1(handle, ToUtf8(selectQuery), index);
			return (result != 0);
		}

		public bool Select(String selectQuery, IList indices)
		{
			int[] indexArray = new int[indices.Count];
			for (int i = 0; i < indices.Count; i++)	indexArray[i] = (int)indices[i];		
			return Select(selectQuery, indexArray);
		}
		
		public bool Select(String selectQuery, int[] indices)
		{
			int result = mb_SelectWithArgs(handle, ToUtf8(selectQuery), indices);
			return (result != 0);
		}
		#endregion

		#region DoesResultExist
		public bool DoesResultExist(String resultName)
		{
			return DoesResultExist(resultName, 0);
		}
		
		public bool DoesResultExist(String resultName, int index)
		{
			int result = mb_DoesResultExist1(handle, ToUtf8(resultName), index);
			return (result != 0);
		}
		#endregion

		#region Data
		public string Data(String resultName)
		{
			return Data(resultName, 0);
		}
		
		public string Data(String resultName, int index)
		{
			string result;
			return GetResultData(resultName, index, out result) ? result : null;
		}		
		#endregion
		
		#region GetResultData
		public string GetResultData(string resultName)
		{
			string result = null;
			GetResultData(resultName, 0, out result);
			return result;
		}
		
		public string GetResultData(string resultName, int index)
		{
			string result = null;
			GetResultData(resultName, index, out result);
			return result;
		}

		public bool GetResultData(String resultName, out String data)
		{
			return GetResultData(resultName, 0, out data);
		}
		
		public bool GetResultData(String resultName, int index, out String data)
		{
			byte[] dataNative = new byte[MAX_STRING_LEN];
			int result = mb_GetResultData1(handle, ToUtf8(resultName), dataNative, MAX_STRING_LEN, index);
			data = (result == 0) ? null : FromUtf8(dataNative);
			return (result != 0);
		}
		#endregion

		#region DataInt
		public int DataInt(String resultName)
		{
			return DataInt(resultName, 0);
		}
		
		public int DataInt(String resultName, int index)
		{
			return GetResultInt(resultName, index);
		}
		#endregion
		
		#region GetResultInt
		public int GetResultInt(String resultName)
		{
			return GetResultInt(resultName, 0);
		}
		
		public int GetResultInt(String resultName, int index)
		{
			int result = mb_GetResultInt1(handle, ToUtf8(resultName), index);
			return result;
		}
		#endregion

		#region ResultRdf
		public bool GetResultRdf(out string rdfObject)
		{
			int len = (int)mb_GetResultRDFLen(handle);

			byte[] rdfObjectNative = new byte[len+1];
			int result = mb_GetResultRDF(handle, rdfObjectNative, len+1);
			rdfObject = (result == 0) ? null : FromUtf8(rdfObjectNative);
			return (result != 0);
		}

		public bool SetResultRdf(String rdf)
		{
			int result = mb_SetResultRDF(handle, ToUtf8(rdf));
			return (result != 0);
		}
		#endregion

		#region GetId
		public string GetId(string id)
		{
			if (id == null)
			{
				return String.Empty;
			}

			try
			{
				return System.Text.RegularExpressions.Regex.IsMatch(id, @"^[A-Za-z0-9\-]+$") ?
					id : GetIdFromUrl(id);
			}
			catch
			{
				return String.Empty;
			}
		}
		#endregion

		#region GetIdFromUrl
		public string GetIdFromUrl(string url)
		{
			byte[] buffer = new byte[64];
			mb_GetIDFromURL(handle, ToUtf8(url), buffer, buffer.Length);
			string id = FromUtf8(buffer);
			int offset = id.IndexOf('#') + 1;
			return (offset >= 0) ? id.Substring(offset) : id;
		}
		
		public void GetIdFromUrl(string url, out string id)
		{
			byte[] idNative = new byte[MAX_STRING_LEN];
			mb_GetIDFromURL(handle, ToUtf8(url), idNative, MAX_STRING_LEN);
			id = FromUtf8(idNative);
		}
		#endregion

		#region GetFragmentFromUrl
		public void GetFragmentFromUrl(string url, out string fragment)
		{
			byte[] fragmentNative = new byte[MAX_STRING_LEN];

			mb_GetFragmentFromURL(handle, ToUtf8(url), fragmentNative, MAX_STRING_LEN);
			fragment = FromUtf8(fragmentNative);
		}
		#endregion

		#region GetOrdinalFromList
		public int GetOrdinalFromList(string resultList, string uri)
		{
			int result = mb_GetOrdinalFromList(handle, ToUtf8(resultList), ToUtf8(uri));
			return result;
		}
		#endregion

		#region GetMp3Info
		public bool GetMp3Info(string fileName, out int duration, out int bitrate, out bool stereo, out int samplerate)
		{
			int stereoNative;
			int result = mb_GetMP3Info(handle, ToUtf8(fileName), out duration, out bitrate, out stereoNative, out samplerate);
			stereo = stereoNative != 0;
			return (result != 0);
		}
		#endregion

		#region SetDebug
		public void SetDebug(bool debug)
		{
			mb_SetDebug(handle, debug ? 1 : 0);
		}
		#endregion
	}
	#endregion
	*/
}
