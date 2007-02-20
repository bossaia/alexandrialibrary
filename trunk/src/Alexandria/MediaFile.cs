using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AlexandriaOrg.Alexandria
{
	public class MediaFile : IMediaFile
	{
		#region Private Constants
		private const string PREFIX_FILE = "file:///";
		private const string PREFIX_HTTP = "http://";
		private const string MS = "ms";
		private const string COLON = ":";
		private const string PERIOD = ".";
		#endregion
	
		#region Private Fields
		private string path;
		private bool isLocal = true;
		private MediaFileFormat format;
		private uint length;
		#endregion
		
		#region Constructors
		/// <summary>
		/// A file containing media data
		/// </summary>
		/// <param name="path">The file path</param>
		/// <param name="isLocal">Indicates whether or not this file is local</param>
		public MediaFile(string path, bool isLocal) : this(path, isLocal, 0)
		{
		}
		
		/// <summary>
		/// A file containing media data
		/// </summary>
		/// <param name="path">The file path</param>
		/// <param name="isLocal">Indicates whether or not this file is local</param>
		/// <param name="length">The length of this file in milliseconds</param>
		[System.CLSCompliant(false)]
		public MediaFile(string path, bool isLocal, uint length)
		{
			this.path = path;
			this.isLocal = isLocal;
			this.length = length;
			
			foreach(string fileExtension in MediaFileFormat.Manifest.Keys)
			{
				if (path.ToLower(System.Globalization.CultureInfo.InvariantCulture).EndsWith(fileExtension.ToLower(System.Globalization.CultureInfo.InvariantCulture)))
				{
					this.format = MediaFileFormat.Manifest[fileExtension];
				}
			}
		}
		#endregion
		
		#region Public Properties
		/// <summary>
		/// Get the file path
		/// </summary>
		public string Path
		{
			get {return path;}
		}
		
		/// <summary>
		/// Get a value indicating whether or not this media file is local to the current application
		/// </summary>
		public bool IsLocal
		{
			get {return isLocal;}
		}
		
		/// <summary>
		/// Get the MediaFormat of this media file
		/// </summary>
		public MediaFileFormat Format
		{
			get {return format;}
		}
		
		/// <summary>
		/// Get the length of the media file in milliseconds
		/// </summary>
		[System.CLSCompliant(false)]
		public uint Length
		{
			get {return length;}
		}
		#endregion
		
		#region Public Methods
		public static MediaFile Load(string path)
		{
			return MediaFile.Load(path, null);
		}
		
		public static MediaFile Load(string path, string length)
		{
			MediaFile mediaFile = null;
		
			if (path != null)
			{
				string pathLower = path.ToLower(System.Globalization.CultureInfo.InvariantCulture);
				if (pathLower.StartsWith(PREFIX_HTTP))
				{
					mediaFile = new RemoteMediaFile(path, length);
				}
				else if (pathLower.StartsWith(PREFIX_FILE))
				{
					mediaFile = new LocalMediaFile(path.Substring(PREFIX_FILE.Length, path.Length - PREFIX_FILE.Length), length);
				}
				else
				{
					mediaFile = new LocalMediaFile(path, length);
				}								
			}
			
			return mediaFile;
		}

        public static MediaFile Load(FileInfo playlistInfo, string path)
        {
            MediaFile mediaFile = null;

			if (playlistInfo != null)
			{
				if (path != null)
				{
					string pathLower = path.ToLower(System.Globalization.CultureInfo.InvariantCulture);
					if (pathLower.StartsWith(PREFIX_HTTP))
					{
						mediaFile = new RemoteMediaFile(path);
					}
					else
					{
						if (Regex.IsMatch(path, @"^[a-zA-Z]{1}\:\\", RegexOptions.None))
						{
							//if this is a match we know this is a full path. eg: c:\somthing.mp3
							mediaFile = new LocalMediaFile(path);
						}
						else
						{
							string fullPath = playlistInfo.DirectoryName + "\\" + path;
							mediaFile = new LocalMediaFile(fullPath);
						}
					}
				}
				else throw new ArgumentNullException("path");
			}
			else throw new ArgumentNullException("playlistInfo");

            return mediaFile;            
        }

		[System.CLSCompliant(false)]
		public static uint ParseLength(string value)
		{
			uint length = 0;

			if (!String.IsNullOrEmpty(value))
			{
				if (value.EndsWith(MS, StringComparison.OrdinalIgnoreCase))
				{
					try
					{
						length = Convert.ToUInt32(value.Substring(0, value.IndexOf(MS)), System.Globalization.NumberFormatInfo.InvariantInfo);
					}
					catch (Exception ex)
					{
						throw new AlexandriaException(Subsystem.Core, ex);
					}
				}
				else if (value.Contains(COLON))
				{
					string part1 = value.Substring(0, value.IndexOf(COLON));
					string part2 = value.Substring(value.IndexOf(COLON) + 1, value.Length - value.IndexOf(COLON)-1);
					if (part2.Contains(COLON))
					{
						try
						{
							uint hours = Convert.ToUInt32(part1, System.Globalization.NumberFormatInfo.InvariantInfo);
							uint minutes = Convert.ToUInt32(part2.Substring(0, part2.IndexOf(COLON) - 1), System.Globalization.NumberFormatInfo.InvariantInfo);
							uint milliseconds = ParseMilliseconds(part2.Substring(part2.IndexOf(COLON) + 1, part2.Length - part2.IndexOf(COLON) - 1));
							length = (hours * 3600000) + (minutes * 60000) + milliseconds;
						}
						catch (Exception ex)
						{
							throw new AlexandriaException(Subsystem.Core, ex);
						}
					}
					else
					{
						try
						{
							uint minutes = Convert.ToUInt32(part1, System.Globalization.NumberFormatInfo.InvariantInfo);
							uint milliseconds = ParseMilliseconds(part2);
							length = (minutes * 60000) + milliseconds;
						}
						catch (Exception ex)
						{
							throw new AlexandriaException(Subsystem.Core, ex);
						}
					}
				}
				else
				{
					length = ParseMilliseconds(value);
				}
			}
			return length;
		}

		private static uint ParseMilliseconds(string value)
		{
			if (!String.IsNullOrEmpty(value))
			{
				try
				{
					if (value.Contains(PERIOD))
					{
						uint seconds = Convert.ToUInt32(value.Substring(0, value.IndexOf(PERIOD) - 1), System.Globalization.NumberFormatInfo.InvariantInfo);
						uint milliseconds = Convert.ToUInt32(value.Substring(value.IndexOf(PERIOD) + 1, value.Length - value.IndexOf(PERIOD) - 1), System.Globalization.NumberFormatInfo.InvariantInfo);
						return (seconds * 1000) + milliseconds;
					}
					else return Convert.ToUInt32(value, System.Globalization.NumberFormatInfo.InvariantInfo);
				}
				catch (Exception ex)
				{
					throw new AlexandriaException(Subsystem.Core, ex);
				}
			}
			return 0;
		}
		#endregion
	}
}
