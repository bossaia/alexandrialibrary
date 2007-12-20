#region License (LGPL)
/* --------------------------------------------------------------------------

Copyright (C) 2006 Dan Poage
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
#endregion

using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;

namespace Telesophy.Alexandria.MusicBrainz
{
	public class MusicBrainzClient : IDisposable
	{
		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public MusicBrainzClient()
		{
			handle = NativeMethods.mb_New();
			NativeMethods.mb_UseUTF8(handle, 1);
			BeginSession();
		}
		#endregion

		#region Finalizer
		~MusicBrainzClient()
		{
			Dispose(false);
		}
		#endregion

		#region IDisposable Members
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				EndSession();
				NativeMethods.mb_Delete(handle);
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	
		#region Private Fields
		private IntPtr handle;
		private int currentResult;
		private ClientVersion version = ClientVersion.Default;
		private ServerInfo server = ServerInfo.Default;
		private ServerInfo proxy = ServerInfo.Default;
		private string device;
		private int queryDepth;
		private int maximumItems;
		private string queryError;
		private Uri webSubmitUrl;
		private bool debug;	
		private WindowsNetworkControl networkControl;	
		#endregion		

		#region Private Static Properties
		
		#region IsWindows
		private static bool IsWindows
		{
			get
			{
				bool isWindows;
				PlatformID platform = System.Environment.OSVersion.Platform;
				PlatformID winPlatformMask = PlatformID.Win32S | PlatformID.Win32Windows | PlatformID.Win32NT;
				isWindows = (Convert.ToInt32(platform & winPlatformMask, System.Globalization.NumberFormatInfo.InvariantInfo) != 0);
				return isWindows;
			}
		}
		#endregion
		
		#endregion

		#region Private Methods
		
		#region Session Methods
		private void BeginSession()
		{
			if (IsWindows)
			{
				networkControl = new WindowsNetworkControl();
				networkControl.Init(handle);
			}
		}

		private void EndSession()
		{
			if (IsWindows)
			{
				if (networkControl != null)
				{
					networkControl.Stop(handle);
				}
			}
		}
		#endregion
		
		#endregion
		
		#region Public Properties
		
		#region CurrentResult
		public int CurrentResult
		{
			get {return currentResult;}
		}
		#endregion
		
		#region Version
		public ClientVersion Version
		{
			get
			{
				if (version == ClientVersion.Default)
				{
					int major, minor, revision;
					NativeMethods.mb_GetVersion(handle, out major, out minor, out revision);
					version.Major = major;
					version.Minor = minor;
					version.Revision = revision;
				}
				return version;
			}	
		}
		#endregion
		
		#region Server
		public ServerInfo Server
		{
			get {return server;}
			set
			{
				server = value;
				currentResult = NativeMethods.mb_SetServer(handle, Utility.ToUtf8(server.Address), server.Port); 
			}
		}
		#endregion
		
		#region Proxy
		public ServerInfo Proxy
		{
			get {return proxy;}
			set
			{
				proxy = value;
				currentResult = NativeMethods.mb_SetProxy(handle, Utility.ToUtf8(proxy.Address), proxy.Port);
			}
		}
		#endregion
		
		#region Device
		public string Device
		{
			get {return device;}
			set
			{
				device = value;
				currentResult = NativeMethods.mb_SetDevice(handle, Utility.ToUtf8(device));
			}
		}
		#endregion
		
		#region QueryDepth
		public int QueryDepth
		{
			get {return queryDepth;}
			set
			{
				queryDepth = value;
				NativeMethods.mb_SetDepth(handle, queryDepth);
			}
		}
		#endregion
		
		#region MaximumItems
		public int MaximumItems
		{
			get {return maximumItems;}
			set
			{
				maximumItems = value;
				NativeMethods.mb_SetMaxItems(handle, maximumItems);
			}
		}
		#endregion
		
		#region QueryError
		public string QueryError
		{
			get
			{
				byte[] errorBytes = new byte[MusicBrainzConstants.MAX_STRING_LEN];
				currentResult = NativeMethods.mb_GetQueryError(handle, errorBytes, MusicBrainzConstants.MAX_STRING_LEN);
				queryError = Utility.FromUtf8(errorBytes);
				return queryError;
			}
		}
		#endregion
		
		#region WebSubmitUrl
		public Uri WebSubmitUrl
		{
			get
			{
				if (webSubmitUrl == null)
				{
					byte[] urlBytes = new byte[MusicBrainzConstants.MAX_STRING_LEN];
					currentResult = NativeMethods.mb_GetWebSubmitURL(handle, urlBytes, MusicBrainzConstants.MAX_STRING_LEN);
					webSubmitUrl = new Uri(Utility.FromUtf8(urlBytes));
				}
				return webSubmitUrl;
			}
		}
		#endregion
		
		#region ResultRdf
		public string ResultRdf
		{
			get
			{
				string rdfObject;
				int length = Convert.ToInt32(NativeMethods.mb_GetResultRDFLen(handle));

				byte[] rdfObjectNative = new byte[length + 1];
				currentResult = NativeMethods.mb_GetResultRDF(handle, rdfObjectNative, length + 1);
				rdfObject = (currentResult == 0) ? null : Utility.FromUtf8(rdfObjectNative);

				return rdfObject;
			}
			set {currentResult = NativeMethods.mb_SetResultRDF(handle, Utility.ToUtf8(value));}
		}
		#endregion
		
		#region Debug
		public bool Debug
		{
			get {return debug;}
			set
			{
				debug = value;
				NativeMethods.mb_SetDebug(handle, Convert.ToInt32(debug));
			}
		}
		#endregion
		
		#endregion
		
		#region Public Methods
		
		#region GetVersion
		/*
		[Obsolete("Use the corresponding property instead", true)]
		public void GetVersion(out int major, out int minor, out int rev)
		{
			NativeMethods.mb_GetVersion(handle, out major, out minor, out rev);
		}
		*/
		#endregion

		#region SetServer
		/*
		[Obsolete("Use the corresponding property instead", true)]
		public bool SetServer(string serverAddress, short serverPort)
		{
			int result = NativeMethods.mb_SetServer(handle, DataConverter.ToUtf8(serverAddress), serverPort);

			return (result != 0);
		}
		*/
		#endregion

		#region SetProxy
		/*
		[Obsolete("Use the corresponding property instead", true)]
		public bool SetProxy(string serverAddress, short serverPort)
		{
			int result = NativeMethods.mb_SetProxy(handle, DataConverter.ToUtf8(serverAddress), serverPort);

			return (result != 0);
		}
		*/
		#endregion
		
		#region Authenticate
		public bool Authenticate(string userName, string password)
		{
			int result = NativeMethods.mb_Authenticate(handle, Utility.ToUtf8(userName), Utility.ToUtf8(password));

			return (result != 0);
		}
		#endregion
		
		#region SetDevice
		/*
		[Obsolete("Use the corresponding property instead", true)]
		public bool SetDevice(string device)
		{
			int result = NativeMethods.mb_SetDevice(handle, DataConverter.ToUtf8(device));

			return (result != 0);
		}
		*/
		#endregion
		
		#region SetDepth
		/*
		[Obsolete("Use the corresponding property instead", true)]
		public void SetDepth(int depth)
		{
			NativeMethods.mb_SetDepth(handle, depth);
		}
		*/
		#endregion
		
		#region SetMaxItems
		/*
		[Obsolete("Use the corresponding property instead", true)]
		public void SetMaxItems(int maxItems)
		{
			NativeMethods.mb_SetMaxItems(handle, maxItems);
		}
		*/
		#endregion

		#region Query
		public bool Query(string rdfObject)
		{
			currentResult = NativeMethods.mb_Query(handle, Utility.ToUtf8(rdfObject));

			return (currentResult != 0);
		}

		[EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public bool Query(string rdfObject, string[] args)
		{
			if (args != null)
			{
				IList argsList = new ArrayList();
				foreach (string arg in args)
				{
					argsList.Add(arg);
				}
				return Query(rdfObject, argsList);
			}
			else throw new ArgumentNullException("args");
		}

		[EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public bool Query(string rdfObject, IList args)
		{
			if (args == null)
			{
				return Query(rdfObject);
			}

			IntPtr[] argsNative = new IntPtr[args.Count + 1];
			for (int i = 0; i < args.Count; i++)
			{				
				argsNative[i] = Utf8NativeWrapper.GetHandle((string)args[i]);
			}
			argsNative[args.Count] = IntPtr.Zero; // This may need to be (IntPtr)0;

			currentResult = NativeMethods.mb_QueryWithArgs(handle, Utility.ToUtf8(rdfObject), argsNative);

			for (int i = 0; i < args.Count; i++)
			{
				Utf8NativeWrapper.FreeHandle(argsNative[i]);
			}

			return (currentResult != 0);
		}
		#endregion

		#region GetQueryError
		/*
		[Obsolete("Use the corresponding property instead", true)]
		public bool GetQueryError(out string error)
		{
			byte[] errorNative = new byte[MAX_STRING_LEN];

			currentResult = NativeMethods.mb_GetQueryError(handle, errorNative, MAX_STRING_LEN);
			error = (currentResult == 0) ? null : DataConverter.FromUtf8(errorNative);

			return (currentResult != 0);
		}
		*/
		#endregion

		#region GetWebSubmitUrl
		/*
		[Obsolete("Use the corresponding property instead", true)]
		public bool GetWebSubmitUrl(out string url)
		{
			byte[] urlNative = new byte[MAX_STRING_LEN];

			currentResult = NativeMethods.mb_GetWebSubmitURL(handle, urlNative, MAX_STRING_LEN);
			url = (currentResult == 0) ? null : DataConverter.FromUtf8(urlNative);

			return (currentResult != 0);
		}
		*/
		#endregion

		#region Select
		public bool Select(string selectQuery)
		{
			return Select(selectQuery, 0);
		}
		
		public bool Select(string selectQuery, int index)
		{
			currentResult = NativeMethods.mb_Select1(handle, Utility.ToUtf8(selectQuery), index);

			return (currentResult != 0);
		}

		public bool Select(string selectQuery, IList indexes)
		{
			if (indexes != null)
			{
				int[] indexArray = new int[indexes.Count];
				for (int i = 0; i < indexes.Count; i++)
				{
					indexArray[i] = (int)indexes[i];
				}
				return Select(selectQuery, indexArray);
			}
			else throw new ArgumentNullException("indexes");
		}

		public bool Select(string selectQuery, int[] indexes)
		{
			currentResult = NativeMethods.mb_SelectWithArgs(handle, Utility.ToUtf8(selectQuery), indexes);

			return (currentResult != 0);
		}
		#endregion
		
		#region DoesResultExist
		public bool DoesResultExist(string resultName)
		{
			return DoesResultExist(resultName, 0);
		}
		
		public bool DoesResultExist(string resultName, int index)
		{
			currentResult = NativeMethods.mb_DoesResultExist1(handle, Utility.ToUtf8(resultName), index);

			return (currentResult != 0);
		}
		#endregion

		#region Data
		public string Data(string resultName)
		{
			return Data(resultName, 0);
		}
		
		public string Data(string resultName, int index)
		{			
			return GetResultData(resultName, index);
		}
		#endregion
		
		#region GetResultData
		public string GetResultData(string resultName)
		{			
			return GetResultData(resultName, 0);			
		}

		public string GetResultData(string resultName, int index)
		{
			string data;
			byte[] dataNative = new byte[MusicBrainzConstants.MAX_STRING_LEN];
			currentResult = NativeMethods.mb_GetResultData1(handle, Utility.ToUtf8(resultName), dataNative, MusicBrainzConstants.MAX_STRING_LEN, index);
			data = (currentResult == 0) ? null : Utility.FromUtf8(dataNative);
			return data;
		}
		#endregion

		#region DataInt
		public int DataInt(string resultName)
		{
			return DataInt(resultName, 0);
		}
		
		public int DataInt(string resultName, int index)
		{
			return GetResultInt(resultName, index);
		}
		#endregion
		
		#region GetResultInt
		public int GetResultInt(string resultName)
		{
			return GetResultInt(resultName, 0);
		}
		
		public int GetResultInt(string resultName, int index)
		{
			currentResult = NativeMethods.mb_GetResultInt1(handle, Utility.ToUtf8(resultName), index);

			return currentResult;
		}
		#endregion

		#region GetResultRdf
		/*
		public bool GetResultRdf(out string rdfObject)
		{
			int length = Convert.ToInt32(NativeMethods.mb_GetResultRDFLen(handle));

			byte[] rdfObjectNative = new byte[length + 1];
			currentResult = NativeMethods.mb_GetResultRDF(handle, rdfObjectNative, length + 1);
			rdfObject = (currentResult == 0) ? null : DataConverter.FromUtf8(rdfObjectNative);

			return (currentResult != 0);
		}

		public bool SetResultRdf(string rdf)
		{
			currentResult = NativeMethods.mb_SetResultRDF(handle, DataConverter.ToUtf8(rdf));

			return (currentResult != 0);
		}
		*/
		#endregion

		#region GetIdFromUrl
		public string GetIdFromUrl(string url)
		{
			if (url != null)
			{
				Uri realUrl = new Uri(url);
				return GetIdFromUrl(realUrl);
			}
			else throw new ArgumentNullException("url");
		}

		public string GetIdFromUrl(Uri url)
		{
			if (url != null)
			{
				byte[] idNative = new byte[MusicBrainzConstants.MAX_STRING_LEN];
				NativeMethods.mb_GetIDFromURL(handle, Utility.ToUtf8(url.AbsoluteUri), idNative, MusicBrainzConstants.MAX_STRING_LEN);
				string id = Utility.FromUtf8(idNative);
				int offset = id.IndexOf('#') + 1;
				return (offset >= 0) ? id.Substring(offset) : id;
			}
			else throw new ArgumentNullException("url");
		}

		/*
		public void GetIdFromUrl(string url, out string id)
		{
			byte[] idNative = new byte[MAX_STRING_LEN];
			NativeMethods.mb_GetIDFromURL(handle, DataConverter.ToUtf8(url), idNative, MAX_STRING_LEN);
			id = DataConverter.FromUtf8(idNative);
		}
		*/
		#endregion

		#region GetFragmentFromUrl
		public string GetFragmentFromUrl(string url)
		{
			if (url != null)
			{
				Uri realUrl = new Uri(url);
				return GetFragmentFromUrl(realUrl);
			}
			else throw new ArgumentNullException("url");
		}
		
		public string GetFragmentFromUrl(Uri url)
		{
			if (url != null)
			{
				string fragment;
				byte[] fragmentNative = new byte[MusicBrainzConstants.MAX_STRING_LEN];

				NativeMethods.mb_GetFragmentFromURL(handle, Utility.ToUtf8(url.AbsoluteUri), fragmentNative, MusicBrainzConstants.MAX_STRING_LEN);
				fragment = Utility.FromUtf8(fragmentNative);

				return fragment;
			}
			else throw new ArgumentNullException("url");
		}
		#endregion

		#region GetOrdinalFromList
		public int GetOrdinalFromList(string resultList, string url)
		{
			if (url != null)
			{
				Uri realUrl = new Uri(url);
				return GetOrdinalFromList(resultList, realUrl);
			}
			else throw new ArgumentNullException("url");
		}
		
		public int GetOrdinalFromList(string resultList, Uri url)
		{
			if (url != null)
			{
				currentResult = NativeMethods.mb_GetOrdinalFromList(handle, Utility.ToUtf8(resultList), Utility.ToUtf8(url.AbsoluteUri));
				return currentResult;
			}
			else throw new ArgumentNullException("url");
		}
		#endregion

		#region GetMp3Info
		public Mp3Info GetMp3Info(string fileName)
		{			
			int duration;
			int bitRate;			
			int stereoNative;
			int sampleRate;
			bool stereo;
			currentResult = NativeMethods.mb_GetMP3Info(handle, Utility.ToUtf8(fileName), out duration, out bitRate, out stereoNative, out sampleRate);			
			stereo = stereoNative != 0;
			
			Mp3Info mp3Info = new Mp3Info(fileName, duration, bitRate, stereo, sampleRate);
			return mp3Info;
		}
		#endregion

		#region SetDebug
		[Obsolete("Use the corresponding property instead", true)]
		public void SetDebug(bool debug)
		{
			NativeMethods.mb_SetDebug(handle, debug ? 1 : 0);
		}
		#endregion

		#region GetId
		public string GetId(string id)
		{
			if (id != null)
			{
				if (Regex.IsMatch(id, @"^[A-Za-z0-9\-]+$"))				
					return id;				
				else
					return GetIdFromUrl(new Uri(id));
			}
			else throw new ArgumentNullException("id");
		}
		#endregion
		
		#endregion
	}
}
