/***************************************************************************
    copyright            : (C) 2006 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : audioproperties.cpp from TagLib
 ***************************************************************************/

/***************************************************************************
 *   This library is free software; you can redistribute it and/or modify  *
 *   it  under the terms of the GNU Lesser General Public License version  *
 *   2.1 as published by the Free Software Foundation.                     *
 *                                                                         *
 *   This library is distributed in the hope that it will be useful, but   *
 *   WITHOUT ANY WARRANTY; without even the implied warranty of            *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU     *
 *   Lesser General Public License for more details.                       *
 *                                                                         *
 *   You should have received a copy of the GNU Lesser General Public      *
 *   License along with this library; if not, write to the Free Software   *
 *   Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  *
 *   USA                                                                   *
 ***************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;

//#if (!WINDOWS)
//using Mono.Unix.Native;
//#endif

namespace Alexandria.TagLib
{
	public abstract class File
	{
		#region Constructors
		protected File(string file)
		{
			//fileStream = null;
			fileAbstraction = fileAbstractionCreator(file);
			readOnly = !fileAbstraction.IsWritable;
			valid = true;
		}
		#endregion
			
		#region Private Fields
		private System.IO.Stream fileStream;
		private IFileAbstraction fileAbstraction;
		private bool readOnly;
		private bool valid;
		private string mimeType;
		#endregion
	
		#region Private Static Fields
		private static Dictionary<string, System.Type> fileTypes;
		private static uint bufferSize = 1024;
		private static ArrayList fileTypeResolvers = new ArrayList();
		private static FileAbstractionCreator fileAbstractionCreator = new FileAbstractionCreator(LocalFileAbstraction.CreateFile);
		#endregion
				
		#region Private Methods
		private long RFind(ByteVector pattern, long fromOffset, ByteVector before)
		{
			try
			{
				Mode = FileAccessMode.Read;
			}
			catch (TagLibException)
			{
				return -1;
			}

			if (pattern.Count > bufferSize)
				return -1;

			// The position in the file that the current buffer starts at.

			ByteVector buffer;

			// These variables are used to keep track of a partial match that happens at
			// the end of a buffer.

			/*
			int previousPartialMatch = -1;
			int beforePreviousPartialMatch = -1;
			*/

			// Save the location of the current read pointer.  We will restore the
			// position using seek() before all returns.

			long originalPosition = fileStream.Position;

			// Start the search at the offset.

			long bufferOffset;
			if (fromOffset == 0)
				Seek(-1 * (int)bufferSize, System.IO.SeekOrigin.End);
			else
				Seek(fromOffset + -1 * (int)bufferSize, System.IO.SeekOrigin.Begin);

			bufferOffset = fileStream.Position;

			// See the notes in find() for an explanation of this algorithm.

			for (buffer = ReadBlock((int)bufferSize); buffer.Count > 0; buffer = ReadBlock((int)bufferSize))
			{
				// TODO: (1) previous partial match

				// (2) pattern contained in current buffer

				long location = buffer.RFind(pattern);
				if (location >= 0)
				{
					fileStream.Position = originalPosition;
					return bufferOffset + location;
				}

				if (before != null && buffer.Find(before) >= 0)
				{
					fileStream.Position = originalPosition;
					return -1;
				}

				// TODO: (3) partial match

				bufferOffset -= bufferSize;
				fileStream.Position = bufferOffset;
			}

			// Since we hit the end of the file, reset the status before continuing.

			fileStream.Position = originalPosition;
			return -1;
		}
		#endregion

		#region Private Static Methods
		private static void InitializeSupportedMimeTypes()
		{
			fileTypes = new Dictionary<string, System.Type>();
			
			System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

			foreach (Type type in assembly.GetTypes())
			{
				if (!type.IsSubclassOf(typeof(File)))
					continue;

				Attribute[] attributes = Attribute.GetCustomAttributes(type, typeof(SupportedMimeTypeAttribute));
				if (attributes == null || attributes.Length == 0)
					continue;


				foreach (SupportedMimeTypeAttribute attribute in attributes)
					fileTypes.Add(attribute.MimeType, type);
			}		
		}
		#endregion

		#region Protected Static Properties
		[CLSCompliant(false)]
		protected static uint BufferSize
		{
			get {return bufferSize;}
		}
		#endregion
		
		#region Protected Methods
		protected void SetValid(bool valid)
		{
			this.valid = valid;
		}

		protected void Truncate(long length)
		{
			try
			{
				Mode = FileAccessMode.Write;
			}
			catch (TagLibException)
			{
				return;
			}

			fileStream.SetLength(length);
		}
		#endregion

		#region Public Delegates
		public delegate IFileAbstraction FileAbstractionCreator(string path);
		public delegate File FileTypeResolver(string path, ReadStyle style);
		#endregion

		#region Public Properties
		public abstract Tag Tag {get;}

		public abstract AudioProperties AudioProperties {get;}

		public string Name
		{
			get {return fileAbstraction.Name;}
		}
		
		public string MimeType
		{
			get {return mimeType;}
			internal set {mimeType = value;}
		}

		public bool IsReadOnly
		{
			get {return readOnly;}
		}

		public bool IsValid
		{
			get {return valid;}
		}

		public long Tell
		{
			get {return (Mode == FileAccessMode.Closed) ? 0 : fileStream.Position;}
		}

		public long Length
		{
			get {return (Mode == FileAccessMode.Closed) ? 0 : fileStream.Length;}
		}

		public FileAccessMode Mode
		{
			get
			{
				return (fileStream == null) ? FileAccessMode.Closed : (fileStream.CanWrite) ? FileAccessMode.Write : FileAccessMode.Read;
			}
			set
			{
				if (Mode == value || (Mode == FileAccessMode.Write && value == FileAccessMode.Read))
					return;

				if (value == FileAccessMode.Write && IsReadOnly)
					throw new TagLibException(TagLibError.WriteAccessNotAvailable);
					//Exception("Write access is not available for this file.");

				if (fileStream != null)
					fileStream.Close();
				fileStream = null;

				if (value == FileAccessMode.Read)
					fileStream = fileAbstraction.ReadStream;
				else if (value == FileAccessMode.Write)
					fileStream = fileAbstraction.WriteStream;

				Mode = value;
			}
		}
		#endregion

		#region Public Static Properties
		public static Dictionary<string, System.Type> FileTypes
		{
			get
			{
				if (fileTypes == null)
				{
					InitializeSupportedMimeTypes();
				}

				return fileTypes;
			}
		}
		#endregion

		#region Public Methods
		public abstract void Save();

		public ByteVector ReadBlock(int length)
		{
			if (length == 0)
				return new ByteVector();

			try
			{
				Mode = FileAccessMode.Read;
			}
			catch (TagLibException)
			{
				TagLibDebugger.Debug(GetType().ToString() + ".ReadBlock() failed. Invalid File: " + Name);
				return null;
			}

			if (length > bufferSize && (long)length > Length)
				length = (int)Length;

			byte[] buffer = new byte[length];
			int count = fileStream.Read(buffer, 0, length);

			return new ByteVector(buffer, count);
		}

		public void WriteBlock(ByteVector data)
		{
			if (data != null)
			{
				try
				{
					Mode = FileAccessMode.Write;
				}
				catch (TagLibException)
				{
					TagLibDebugger.Debug(GetType().ToString() + ".WriteBlock () failed. Read-only File: " + Name);
					return;
				}

				fileStream.Write(data.GetDataBuffer(), 0, data.Count);
			}
			else throw new ArgumentNullException("data");
		}

		public long Find(ByteVector pattern, long fromOffset, ByteVector before)
		{
			if (pattern != null)
			{
				try
				{
					Mode = FileAccessMode.Read;
				}
				catch (TagLibException)
				{
					return -1;
				}

				if (pattern.Count > bufferSize)
					return -1;

				// The position in the file that the current buffer starts at.

				long bufferOffset = fromOffset;
				ByteVector buffer;

				// These variables are used to keep track of a partial match that happens at
				// the end of a buffer.

				int previousPartialMatch = -1;
				int beforePreviousPartialMatch = -1;

				// Save the location of the current read pointer.  We will restore the
				// position using seek() before all returns.

				long originalPosition = fileStream.Position;

				// Start the search at the offset.

				fileStream.Position = fromOffset;

				// This loop is the crux of the find method.  There are three cases that we
				// want to account for:
				//
				// (1) The previously searched buffer contained a partial match of the search
				// pattern and we want to see if the next one starts with the remainder of
				// that pattern.
				//
				// (2) The search pattern is wholly contained within the current buffer.
				//
				// (3) The current buffer ends with a partial match of the pattern.  We will
				// note this for use in the next iteration, where we will check for the rest
				// of the pattern.
				//
				// All three of these are done in two steps.  First we check for the pattern
				// and do things appropriately if a match (or partial match) is found.  We
				// then check for "before".  The order is important because it gives priority
				// to "real" matches.

				for (buffer = ReadBlock((int)bufferSize); buffer.Count > 0; buffer = ReadBlock((int)bufferSize))
				{

					// (1) previous partial match

					if (previousPartialMatch >= 0 && (int)bufferSize > previousPartialMatch)
					{
						int patternOffset = (int)bufferSize - previousPartialMatch;

						if (buffer.ContainsAt(pattern, 0, patternOffset))
						{
							fileStream.Position = originalPosition;
							return bufferOffset - bufferSize + previousPartialMatch;
						}
					}

					if (before != null && beforePreviousPartialMatch >= 0 && (int)bufferSize > beforePreviousPartialMatch)
					{
						int beforeOffset = (int)bufferSize - beforePreviousPartialMatch;
						if (buffer.ContainsAt(before, 0, beforeOffset))
						{
							fileStream.Position = originalPosition;
							return -1;
						}
					}

					// (2) pattern contained in current buffer

					long location = buffer.Find(pattern);
					if (location >= 0)
					{
						fileStream.Position = originalPosition;
						return bufferOffset + location;
					}

					if (before != null && buffer.Find(before) >= 0)
					{
						fileStream.Position = originalPosition;
						return -1;
					}

					// (3) partial match

					previousPartialMatch = buffer.EndsWithPartialMatch(pattern);

					if (before != null)
						beforePreviousPartialMatch = buffer.EndsWithPartialMatch(before);

					bufferOffset += bufferSize;
				}

				// Since we hit the end of the file, reset the status before continuing.

				fileStream.Position = originalPosition;
				return -1;
			}
			else throw new ArgumentNullException("pattern");
		}

		public long Find(ByteVector pattern, long fromOffset)
		{
			return Find(pattern, fromOffset, null);
		}

		public long Find(ByteVector pattern)
		{
			return Find(pattern, 0);
		}

		public long RFind(ByteVector pattern, long fromOffset)
		{
			return RFind(pattern, fromOffset, null);
		}

		public long RFind(ByteVector pattern)
		{
			return RFind(pattern, 0);
		}

		public void Insert(ByteVector data, long start, long replace)
		{
			if (data != null)
			{
				try
				{
					Mode = FileAccessMode.Write;
				}
				catch (TagLibException)
				{
					return;
				}

				if (data.Count == replace)
				{
					fileStream.Position = start;
					WriteBlock(data);
					return;
				}
				else if (data.Count < replace)
				{
					fileStream.Position = start;
					WriteBlock(data);
					RemoveBlock(start + data.Count, replace - data.Count);
					return;
				}

				// Woohoo!  Faster (about 20%) than id3lib at last.  I had to get hardcore
				// and avoid TagLib'field high level API for rendering just copying parts of
				// the file that don'type contain tag data.
				//
				// Now I'll explain the steps in this ugliness:

				// First, make sure that we're working with a buffer that is longer than
				// the *difference* in the tag sizes.  We want to avoid overwriting parts
				// that aren'type yet in memory, so this is necessary.

				int bufferLength = (int)BufferSize;
				while (data.Count - replace > bufferLength)
					bufferLength += (int)BufferSize;

				// Set where to start the reading and writing.

				long readPosition = start + replace;
				long writePosition = start;

				byte[] buffer;
				byte[] aboutToOverwrite;

				// This is basically a special case of the loop below.  Here we're just
				// doing the same steps as below, but since we aren'type using the same buffer
				// size -- instead we're using the tag size -- this has to be handled as a
				// special case.  We're also using File::writeBlock() just for the tag.
				// That'field a bit slower than using char *'field so, we're only doing it here.

				fileStream.Position = readPosition;
				aboutToOverwrite = ReadBlock(bufferLength).GetDataBuffer();
				readPosition += bufferLength;

				fileStream.Position = writePosition;
				WriteBlock(data);
				writePosition += data.Count;

				buffer = new byte[aboutToOverwrite.Length];
				System.Array.Copy(aboutToOverwrite, buffer, aboutToOverwrite.Length);

				// Ok, here'field the main loop.  We want to loop until the read fails, which
				// means that we hit the end of the file.

				while (bufferLength != 0)
				{
					// Seek to the current read position and read the data that we're about
					// to overwrite.  Appropriately increment the readPosition.

					fileStream.Position = readPosition;

					int bytesRead = fileStream.Read(aboutToOverwrite, 0, bufferLength < aboutToOverwrite.Length ? bufferLength : aboutToOverwrite.Length);
					readPosition += bufferLength;

					// Seek to the write position and write our buffer.  Increment the
					// writePosition.

					fileStream.Position = writePosition;
					fileStream.Write(buffer, 0, bufferLength < buffer.Length ? bufferLength : buffer.Length);
					writePosition += bufferLength;

					// Make the current buffer the data that we read in the beginning.
					System.Array.Copy(aboutToOverwrite, buffer, bytesRead);

					// Again, we need this for the last write.  We don'type want to write garbage
					// at the end of our file, so we need to set the buffer size to the amount
					// that we actually read.

					bufferLength = bytesRead;
				}
			}
			else throw new ArgumentNullException("data");
		}

		public void Insert(ByteVector data, long start)
		{
			Insert(data, start, 0);
		}

		public void Insert(ByteVector data)
		{
			Insert(data, 0);
		}

		public void RemoveBlock(long start, long length)
		{
			try
			{
				Mode = FileAccessMode.Write;
			}
			catch (TagLibException)
			{
				return;
			}

			int bufferLength = (int)BufferSize;

			long readPosition = start + length;
			long writePosition = start;

			ByteVector buffer = (byte)1;

			while (buffer.Count != 0)
			{
				fileStream.Position = readPosition;
				buffer = ReadBlock(bufferLength);
				readPosition += buffer.Count;

				fileStream.Position = writePosition;
				WriteBlock(buffer);
				writePosition += buffer.Count;
			}

			Truncate(writePosition);
		}

		public void RemoveBlock(long start)
		{
			RemoveBlock(start, 0);
		}

		public void RemoveBlock()
		{
			RemoveBlock(0);
		}

		public void Seek(long offset, System.IO.SeekOrigin position)
		{
			if (Mode != FileAccessMode.Closed)
				fileStream.Seek(offset, position);
		}

		public void Seek(long offset)
		{
			Seek(offset, System.IO.SeekOrigin.Begin);
		}

		public abstract Tag FindTag(TagTypes type, bool create);

		public Tag FindTag(TagTypes type)
		{
			return FindTag(type, false);
		}
		#endregion
		
		#region Internal Static Methods
		internal static FileAbstractionCreator GetFileAbstractionCreator()
		{
			return fileAbstractionCreator;
		}
		#endregion
		
		#region Public Static Methods
		public static File Create(string path)
		{
			return Create(path, null, ReadStyle.Average);
		}

		public static File Create(string path, ReadStyle style)
		{
			return Create(path, null, style);
		}

		public static File Create(string path, string mimeType, ReadStyle style)
		{
			foreach (FileTypeResolver resolver in fileTypeResolvers)
			{
				File file = resolver(path, style);
				if (file != null)
					return file;
			}

			if (mimeType == null)
			{
				/* ext = System.IO.Path.GetExtension(path).Substring(1) */
				string ext = String.Empty;

				//try
				//{
					int index = path.LastIndexOf(".") + 1;
					if (index >= 1 && index < path.Length)
						ext = path.Substring(index, path.Length - index);
				//}
				//catch
				//{
					/* Proper exception will be thrown later */
				//}

				mimeType = "taglib/" + ext.ToLower(System.Globalization.CultureInfo.InvariantCulture);
			}

			Type fileType = FileTypes[mimeType] as Type;
			if (fileType == null)
			{
				throw new UnsupportedFormatException(string.Format(System.Globalization.CultureInfo.InvariantCulture ,"{0} ({1})", path, mimeType));
			}

			try
			{
				File file = (File)Activator.CreateInstance(fileType, new object[]{path, style});
				file.MimeType = mimeType;
				return file;
			}
			catch (System.Reflection.TargetInvocationException e)
			{
				throw e.InnerException;
			}
		}

		public static void AddFileTypeResolver(FileTypeResolver resolver)
		{
			if (resolver != null)
				fileTypeResolvers.Insert(0, resolver);
		}

		public static void SetFileAbstractionCreator(FileAbstractionCreator creator)
		{
			if (creator != null)
				fileAbstractionCreator = creator;
		}
		#endregion
   }
}
