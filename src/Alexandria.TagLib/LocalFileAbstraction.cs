using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public class LocalFileAbstraction : IFileAbstraction
	{
		#region Constructors
		public LocalFileAbstraction(string file)
		{
			name = file;
			if (!IsReadable)
				throw new TagLibException(TagLibError.ReadAccessNotAvailable);
		}
		#endregion
		
		#region Private Fields
		private string name;
		#endregion
		
		#region Public Properties
		public string Name
		{
			get { return name; }
		}

		public System.IO.Stream ReadStream
		{
			get { return System.IO.File.Open(Name, System.IO.FileMode.Open, System.IO.FileAccess.Read); }
		}

		public System.IO.Stream WriteStream
		{
			get { return System.IO.File.Open(Name, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite); }
		}

		//#if (WINDOWS)
		public bool IsReadable
		{
			get
			{
				try
				{
					System.IO.Stream stream = ReadStream;
					stream.Close();
				}
				catch (System.IO.IOException)
				{
					return false;
				}

				return true;
			}
		}

		public bool IsWritable
		{
			get
			{
				try
				{
					System.IO.Stream s = WriteStream;
					s.Close();
				}
				catch (System.IO.IOException)
				{
					return false;
				}

				return true;
			}
		}
		
		//#else
		//public bool IsReadable
		//{
		//get {return Syscall.access (Name, AccessModes.R_OK) == 0;}
		//}

		//public bool IsWritable
		//{
		//get {return Syscall.access (Name, AccessModes.W_OK) == 0;}
		//}
		//#endif
		#endregion

		#region Public Static Methods
		public static IFileAbstraction CreateFile(string path)
		{
			return new LocalFileAbstraction(path);
		}
		#endregion		
	}
}
