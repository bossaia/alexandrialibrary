using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Alexandria;

namespace Alexandria.MusicBrainz
{
	#region BitPrintInfo
	/*
	[StructLayout(LayoutKind.Sequential)]
	public struct BitPrintInfo
	{
		#region Public Fields
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
		public byte[] FileName;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 89)]
		public byte[] BitPrint;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 41)]
		public byte[] First20;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 41)]
		public byte[] AudioSha1;
		[CLSCompliant(false)]
		public uint Length;
		[CLSCompliant(false)]
		public uint Duration;
		[CLSCompliant(false)]
		public uint SampleRate;
		[CLSCompliant(false)]
		public uint BitRate;
		public byte Stereo;
		public byte VariableBitRate;
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj is BitPrintInfoNative)
			{
				BitPrintInfoNative otherBitPrintInfo = (BitPrintInfoNative)obj;
				return (this.FileName == otherBitPrintInfo.FileName);
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return DataConverter.GetHashCode(this.FileName);
		}
		#endregion
		
		#region Public Static Methods
		public static bool operator ==(BitPrintInfoNative b1, BitPrintInfoNative b2)
		{
			return b1.Equals(b2);
		}
		
		public static bool operator !=(BitPrintInfoNative b1, BitPrintInfoNative b2)
		{
			return !b1.Equals(b2);
		}
		#endregion
	}
	*/
	#endregion

	#region ClientVersion
	public struct ClientVersion
	{
		#region Private Fields
		private int major;
		private int minor;
		private int revision;
		#endregion

		#region Private Static Fields
		private static ClientVersion defaultVersion = new ClientVersion(0, 0, 0);
		#endregion

		#region Constructors
		public ClientVersion(int major, int minor, int revision)
		{
			this.major = major;
			this.minor = minor;
			this.revision = revision;
		}
		#endregion

		#region Public Properties
		public int Major
		{
			get { return major; }
			set { major = value; }
		}

		public int Minor
		{
			get { return minor; }
			set { minor = value; }
		}

		public int Revision
		{
			get { return revision; }
			set { revision = value; }
		}
		#endregion

		#region Public Static Properties
		public static ClientVersion Default
		{
			get {return defaultVersion;}
		}
		#endregion

		#region Public Methods
		public override string ToString()
		{
			return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}.{1}.{2}", Major, Minor, Revision);
		}

		public override bool Equals(object obj)
		{
			if (obj is ClientVersion)
			{
				ClientVersion otherVersion = (ClientVersion)obj;
				if (otherVersion.Major == this.Major && otherVersion.Minor == this.Minor && otherVersion.Revision == this.Revision)
					return true;
				else
					return false;
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return (Major * 100) + (Minor * 10) + Revision;
		}
		#endregion		
		
		#region Public Static Methods
		public static bool operator ==(ClientVersion c1, ClientVersion c2)
		{
			return c1.Equals(c2);
		}

		public static bool operator !=(ClientVersion c1, ClientVersion c2)
		{
			return !c1.Equals(c2);
		}
		
		public static bool operator >(ClientVersion c1, ClientVersion c2)
		{
			return c1.GetHashCode() > c2.GetHashCode();
		}
		
		public static bool operator >=(ClientVersion c1, ClientVersion c2)
		{
			return c1.GetHashCode() >= c2.GetHashCode();
		}
		
		public static bool operator <=(ClientVersion c1, ClientVersion c2)
		{
			return c1.GetHashCode() <= c2.GetHashCode();
		}
		
		public static bool operator <(ClientVersion c1, ClientVersion c2)
		{
			return c1.GetHashCode() < c2.GetHashCode();
		}
		
		public static int Compare(ClientVersion c1, ClientVersion c2)
		{
			if (c1 < c2)
				return -1;
			else if (c1 == c2)
				return 0;
			else if (c1 > c2)
				return 1;
			else
				return 0;
		}
		#endregion
	}
	#endregion

	#region ServerInfo
	public struct ServerInfo
	{
		#region Private Fields
		private string address;
		private short port;
		#endregion

		#region Private Static Fields
		private static ServerInfo defaultServer = new ServerInfo("mm.musicbrainz.org", 80);
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
		
		#region Public Static Properties
		/// <summary>
		/// The default MusicBrainz server
		/// </summary>
		public static ServerInfo Default
		{
			get {return defaultServer;}
		}
		#endregion
		
		#region Public Methods
		public override string ToString()
		{
			return this.Address + ":" + this.Port.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
		}
		
		public override bool Equals(object obj)
		{
			if (obj is ServerInfo)
			{
				ServerInfo otherServer = (ServerInfo)obj;
				if (otherServer.Address == this.Address && otherServer.Port == this.Port)
					return true;
				else
					return false;
			}
			else return false;
		}

		public override int GetHashCode()
		{	
			return (this.Address.GetHashCode() ^ this.Port.GetHashCode());
		}
		#endregion
		
		#region Public Static Methods
		public static bool operator ==(ServerInfo s1, ServerInfo s2)
		{
			return s1.Equals(s2);
		}
		
		public static bool operator !=(ServerInfo s1, ServerInfo s2)
		{
			return !s1.Equals(s2);
		}
		#endregion
	}
	#endregion
	
	#region Mp3Info
	public struct Mp3Info
	{
		#region Private Fields
		private string fileName;
		private int duration;
		private int bitRate;
		private bool stereo;
		private int sampleRate;
		#endregion
		
		#region Constructors
		public Mp3Info(string fileName, int duration, int bitRate, bool stereo, int sampleRate)
		{
			this.fileName = fileName;
			this.duration = duration;
			this.bitRate = bitRate;
			this.stereo = stereo;
			this.sampleRate = sampleRate;
		}
		#endregion
		
		#region Public Properties
		public string FileName
		{
			get {return fileName;}
		}
		
		public int Duration
		{
			get {return duration;}
		}
		
		public int BitRate
		{
			get {return bitRate;}
		}
		
		public bool Stereo
		{
			get {return stereo;}
		}
		
		public int SampleRate
		{
			get {return sampleRate;}
		}
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj is Mp3Info)
			{
				Mp3Info otherInfo = (Mp3Info)obj;
				return (this.FileName == otherInfo.FileName);
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return fileName.GetHashCode();
		}
		#endregion
		
		#region Public Static Methods
		public static bool operator ==(Mp3Info m1, Mp3Info m2)
		{
			return m1.Equals(m2);
		}
		
		public static bool operator !=(Mp3Info m1, Mp3Info m2)
		{
			return !m1.Equals(m2);
		}
		#endregion
	}
	#endregion
}
