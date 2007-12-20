/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : tbytevector.cpp from TagLib
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

namespace Alexandria.TagLib
{   
	public class ByteVector : IList<byte>,ICollection<byte>
	{
		#region Constructors
		public ByteVector()
		{
		}

		public ByteVector(int size, byte value)
		{
			if (size > 0)
			{
				byte[] data = new byte[size];

				for (int i = 0; i < size; i++)
					data[i] = value;

				SetData(data);
			}
		}

		public ByteVector(int size) : this(size, 0)
		{
		}

		public ByteVector(ByteVector vector)
		{
			Add(vector);
		}

		public ByteVector(byte value) : this(1, value)
		{
		}

		public ByteVector(byte[] data, int length)
		{
			SetData(data, length);
		}

		public ByteVector(byte[] data)
		{
			SetData(data);
		}
		#endregion
	
		#region Private Fields
		private List<byte> data = new List<byte>();
		#endregion
		
		#region Private Static Fields
		private static uint[] crcTable = new uint[256]
		{
			0x00000000, 0x04c11db7, 0x09823b6e, 0x0d4326d9, 0x130476dc, 0x17c56b6b,
			0x1a864db2, 0x1e475005, 0x2608edb8, 0x22c9f00f, 0x2f8ad6d6, 0x2b4bcb61,
			0x350c9b64, 0x31cd86d3, 0x3c8ea00a, 0x384fbdbd, 0x4c11db70, 0x48d0c6c7,
			0x4593e01e, 0x4152fda9, 0x5f15adac, 0x5bd4b01b, 0x569796c2, 0x52568b75,
			0x6a1936c8, 0x6ed82b7f, 0x639b0da6, 0x675a1011, 0x791d4014, 0x7ddc5da3,
			0x709f7b7a, 0x745e66cd, 0x9823b6e0, 0x9ce2ab57, 0x91a18d8e, 0x95609039,
			0x8b27c03c, 0x8fe6dd8b, 0x82a5fb52, 0x8664e6e5, 0xbe2b5b58, 0xbaea46ef,
			0xb7a96036, 0xb3687d81, 0xad2f2d84, 0xa9ee3033, 0xa4ad16ea, 0xa06c0b5d,
			0xd4326d90, 0xd0f37027, 0xddb056fe, 0xd9714b49, 0xc7361b4c, 0xc3f706fb,
			0xceb42022, 0xca753d95, 0xf23a8028, 0xf6fb9d9f, 0xfbb8bb46, 0xff79a6f1,
			0xe13ef6f4, 0xe5ffeb43, 0xe8bccd9a, 0xec7dd02d, 0x34867077, 0x30476dc0,
			0x3d044b19, 0x39c556ae, 0x278206ab, 0x23431b1c, 0x2e003dc5, 0x2ac12072,
			0x128e9dcf, 0x164f8078, 0x1b0ca6a1, 0x1fcdbb16, 0x018aeb13, 0x054bf6a4,
			0x0808d07d, 0x0cc9cdca, 0x7897ab07, 0x7c56b6b0, 0x71159069, 0x75d48dde,
			0x6b93dddb, 0x6f52c06c, 0x6211e6b5, 0x66d0fb02, 0x5e9f46bf, 0x5a5e5b08,
			0x571d7dd1, 0x53dc6066, 0x4d9b3063, 0x495a2dd4, 0x44190b0d, 0x40d816ba,
			0xaca5c697, 0xa864db20, 0xa527fdf9, 0xa1e6e04e, 0xbfa1b04b, 0xbb60adfc,
			0xb6238b25, 0xb2e29692, 0x8aad2b2f, 0x8e6c3698, 0x832f1041, 0x87ee0df6,
			0x99a95df3, 0x9d684044, 0x902b669d, 0x94ea7b2a, 0xe0b41de7, 0xe4750050,
			0xe9362689, 0xedf73b3e, 0xf3b06b3b, 0xf771768c, 0xfa325055, 0xfef34de2,
			0xc6bcf05f, 0xc27dede8, 0xcf3ecb31, 0xcbffd686, 0xd5b88683, 0xd1799b34,
			0xdc3abded, 0xd8fba05a, 0x690ce0ee, 0x6dcdfd59, 0x608edb80, 0x644fc637,
			0x7a089632, 0x7ec98b85, 0x738aad5c, 0x774bb0eb, 0x4f040d56, 0x4bc510e1,
			0x46863638, 0x42472b8f, 0x5c007b8a, 0x58c1663d, 0x558240e4, 0x51435d53,
			0x251d3b9e, 0x21dc2629, 0x2c9f00f0, 0x285e1d47, 0x36194d42, 0x32d850f5,
			0x3f9b762c, 0x3b5a6b9b, 0x0315d626, 0x07d4cb91, 0x0a97ed48, 0x0e56f0ff,
			0x1011a0fa, 0x14d0bd4d, 0x19939b94, 0x1d528623, 0xf12f560e, 0xf5ee4bb9,
			0xf8ad6d60, 0xfc6c70d7, 0xe22b20d2, 0xe6ea3d65, 0xeba91bbc, 0xef68060b,
			0xd727bbb6, 0xd3e6a601, 0xdea580d8, 0xda649d6f, 0xc423cd6a, 0xc0e2d0dd,
			0xcda1f604, 0xc960ebb3, 0xbd3e8d7e, 0xb9ff90c9, 0xb4bcb610, 0xb07daba7,
			0xae3afba2, 0xaafbe615, 0xa7b8c0cc, 0xa379dd7b, 0x9b3660c6, 0x9ff77d71,
			0x92b45ba8, 0x9675461f, 0x8832161a, 0x8cf30bad, 0x81b02d74, 0x857130c3,
			0x5d8a9099, 0x594b8d2e, 0x5408abf7, 0x50c9b640, 0x4e8ee645, 0x4a4ffbf2,
			0x470cdd2b, 0x43cdc09c, 0x7b827d21, 0x7f436096, 0x7200464f, 0x76c15bf8,
			0x68860bfd, 0x6c47164a, 0x61043093, 0x65c52d24, 0x119b4be9, 0x155a565e,
			0x18197087, 0x1cd86d30, 0x029f3d35, 0x065e2082, 0x0b1d065b, 0x0fdc1bec,
			0x3793a651, 0x3352bbe6, 0x3e119d3f, 0x3ad08088, 0x2497d08d, 0x2056cd3a,
			0x2d15ebe3, 0x29d4f654, 0xc5a92679, 0xc1683bce, 0xcc2b1d17, 0xc8ea00a0,
			0xd6ad50a5, 0xd26c4d12, 0xdf2f6bcb, 0xdbee767c, 0xe3a1cbc1, 0xe760d676,
			0xea23f0af, 0xeee2ed18, 0xf0a5bd1d, 0xf464a0aa, 0xf9278673, 0xfde69bc4,
			0x89b8fd09, 0x8d79e0be, 0x803ac667, 0x84fbdbd0, 0x9abc8bd5, 0x9e7d9662,
			0x933eb0bb, 0x97ffad0c, 0xafb010b1, 0xab710d06, 0xa6322bdf, 0xa2f33668,
			0xbcb4666d, 0xb8757bda, 0xb5365d03, 0xb1f740b4
		};
		#endregion
 
		#region Private Methods
		private static System.Text.Encoding StringTypeToEncoding(StringType type, ByteVector bom)
		{
			switch (type)
			{
				case StringType.UTF16:
					return (bom == null || (bom[0] == 0xFF && bom[1] == 0xFE)) ?
					   System.Text.Encoding.Unicode : System.Text.Encoding.BigEndianUnicode;
				case StringType.UTF16BE:
					return System.Text.Encoding.BigEndianUnicode;
				case StringType.UTF8:
					return System.Text.Encoding.UTF8;
				case StringType.UTF16LE:
					return System.Text.Encoding.Unicode;
			}
			return System.Text.Encoding.ASCII;
		}

		/* FIXME: Figure out generics
		private static T ToNumber<T>(byte [] data, bool most_significant_byte_first) where T : IConvertible
		{
		   T sum = 0;
		         
		   if (data.Length <= 0)
			  return sum;
		         
		   int size = sizeof (T);
		   int last = data.Length > size ? size - 1 : data.Length - 1;
		         
		   for (int index = 0; index <= last; index ++)
			  sum |= (T) data [index] << ((most_significant_byte_first ? last - index : index) * 8);
		         
		   return sum;
		}
		      
		private static ByteVector FromNumber<T> (T value, bool most_significant_byte_first)
		{
		   int size = sizeof (T);

		   ByteVector vector = new ByteVector ();

		   for (int index = 0; index < size; index++)
			  vector.Add ((byte) (value >> ((most_significant_byte_first ? size - 1 - index : index) * 8) & 0xFF));

		   return vector;
		}
		*/
		#endregion
   
		#region Public Properties
		public IList<byte> Data
		{
			get { return data; }
		}
		
		public bool IsReadOnly
		{
			get {return false;}
		}

		//public bool IsFixedSize
		//{
			//get {return false;}
		//}

		byte IList<byte>.this[int index]
		{
			get {return this[index];}
			set {this[index] = value;}
		}

		public byte this[int index]
		{
			get { return data[index]; }
			set { data[index] = value; }
		}

		public int Count
		{
			get {return data.Count;}
		}

		//public bool IsSynchronized
		//{
			//get {return false;}
		//}

		public object SyncRoot
		{
			get {return this;}
		}

		public bool IsEmpty
		{
			get {return Count == 0;}
		}

		[System.CLSCompliant(false)]
		public uint Checksum
		{
			get
			{
				uint sum = 0;
				foreach (byte b in this)
					sum = (sum << 8) ^ crcTable[((sum >> 24) & 0xFF) ^ b];
				return sum;
			}
		}
		#endregion
   
		#region Public Methods
		void ICollection<byte>.Add(byte value)
		{
			Add(value);
		}

		public void Add(byte value)
		{
			data.Add(value);
		}

		public void Clear()
		{
			data.Clear();
		}

		bool ICollection<byte>.Contains(byte value)
		{
			return Contains(value);
		}

		public bool Contains(byte value)
		{
			return IndexOf(value) != -1;
		}

		int IList<byte>.IndexOf(byte value)
		{
			return IndexOf(value);
		}

		public int IndexOf(byte value)
		{
			return data.IndexOf(value);
		}

		void IList<byte>.Insert(int index, byte value)
		{
			Insert(index, value);
		}

		public void Insert(int index, byte value)
		{
			data.Insert(index, value);
		}

		bool ICollection<byte>.Remove(byte value)
		{
			return Remove(value);
		}

		public bool Remove(byte value)
		{
			return data.Remove(value);
		}

		public void RemoveAt(int index)
		{
			data.RemoveAt(index);
		}

		void ICollection<byte>.CopyTo(byte[] array, int index)
		{
			data.CopyTo(array, index);
		}

		public byte[] GetDataBuffer()
		{
			return data.ToArray();
		}

		public void SetData(byte[] value, int length)
		{
			if (value != null)
			{
				if (length >= value.Length)
					SetData(value);
				else
				{
					byte[] data = new byte[length];
					System.Array.Copy(value, data, length);
					SetData(data);
				}
			}
			else throw new ArgumentNullException("value");
		}

		public void SetData(byte[] value)
		{
			Clear();
			Add(value);
		}

		public ByteVector Mid(int index, int length)
		{
			if (length == Int32.MaxValue || index + length > Count)
				length = Count - index;

			ByteVector b = new ByteVector(length);
			for (int i = 0; i < length; i++)
				b[i] = this[i + index];
			return b;
		}

		public ByteVector Mid(int index)
		{
			return Mid(index, Int32.MaxValue);
		}

		public int Find(ByteVector pattern, int offset, int alignment)
		{
			if (pattern != null)
			{
				// FIXME: Implement searching as done in TagLib.
				int endByte = Count - pattern.Count;

				for (int i = offset; i <= endByte; i += alignment)
					if (ContainsAt(pattern, i)) return i;

				return -1;
			}
			else throw new ArgumentNullException("pattern");
		}

		public int Find(ByteVector pattern, int offset)
		{
			return Find(pattern, offset, 1);
		}

		public int Find(ByteVector pattern)
		{
			return Find(pattern, 0, 1);
		}

		public int RFind(ByteVector pattern, int offset, int alignment)
		{
			if (pattern != null)
			{
				// FIXME: Implement searching as done in TagLib.
				int endByte = Count - pattern.Count - offset;

				for (int i = endByte; i >= 0; i -= alignment)
					if (ContainsAt(pattern, i)) return i;

				return -1;
			}
			else throw new ArgumentNullException("pattern");
		}

		public int RFind(ByteVector pattern, int offset)
		{
			return RFind(pattern, offset, 1);
		}

		public int RFind(ByteVector pattern)
		{
			return RFind(pattern, 0, 1);
		}

		public bool ContainsAt(ByteVector pattern, int offset, int patternOffset, int patternLength)
		{
			if (pattern != null)
			{
				if (pattern.Count < patternLength)
					patternLength = pattern.Count;

				// do some sanity checking -- all of these things are needed for the search to be valid

				if (patternLength > Count || offset >= Count || patternOffset >= pattern.Count || patternLength == 0)
					return false;

				// loop through looking for a mismatch

				for (int i = 0; i < patternLength - patternOffset; i++)
				{
					if (this[i + offset] != pattern[i + patternOffset])
						return false;
				}

				return true;
			}
			else throw new ArgumentNullException("pattern");
		}

		public bool ContainsAt(ByteVector pattern, int offset, int patternOffset)
		{
			return ContainsAt(pattern, offset, patternOffset, Int32.MaxValue);
		}

		public bool ContainsAt(ByteVector pattern, int offset)
		{
			return ContainsAt(pattern, offset, 0);
		}

		public bool StartsWith(ByteVector pattern)
		{
			return ContainsAt(pattern, 0);
		}

		public bool EndsWith(ByteVector pattern)
		{
			if (pattern != null)
			{
				return ContainsAt(pattern, Count - pattern.Count);
			}
			else throw new ArgumentNullException("pattern");
		}

		public int EndsWithPartialMatch(ByteVector pattern)
		{
			if (pattern != null)
			{
				if (pattern.Count > Count)
					return -1;

				int start_index = Count - pattern.Count;

				// try to match the last n-1 buffer from the vector (where n is the pattern
				// size) -- continue trying to match n-2, n-3...1 buffer

				for (int i = 1; i < pattern.Count; i++)
				{
					if (ContainsAt(pattern, start_index + i, 0, pattern.Count - i))
						return start_index + i;
				}

				return -1;
			}
			else throw new ArgumentNullException("pattern");
		}

		public void Add(ByteVector vector)
		{
			if (vector != null)
				data.AddRange(vector);
		}

		public void Add(byte[] vector)
		{
			if (vector != null)
				data.AddRange(vector);
		}

		//public ByteVector Resize(int size, byte padding)
		//{
			//if (Count > size)
				//data.RemoveRange(size, Count - size);

			//while (Count < size)
				//Add(0);
			//return this;
		//}

		public ByteVector Resize(int size)
		{
			//return Resize(size, 0);
			
			if (Count > size)
				data.RemoveRange(size, Count - size);

			while (Count < size)
				Add(0);
			return this;						
		}

		[System.CLSCompliant(false)]
		public uint ToUInt(bool mostSignificantByteFirst)
		{
			// FIXME: Generics would be nice if I could figure them out.
			//return ToNumber<uint>(Data, mostSignificantByteFirst);
			uint sum = 0;

			if (Count <= 0)
				return sum;

			int last = Count > 4 ? 3 : Count - 1;

			for (int i = 0; i <= last; i++)
				sum |= (uint)this[i] << ((mostSignificantByteFirst ? last - i : i) * 8);

			return sum;
		}

		[System.CLSCompliant(false)]
		public uint ToUInt()
		{
			return ToUInt(true);
		}

		public short ToShort(bool mostSignificantByteFirst)
		{
			// FIXME: Generics would be nice if I could figure them out.
			//return ToNumber<short> (Data, most_significant_byte_first);
			short sum = 0;

			if (Count <= 0)
				return sum;

			int last = Count > 2 ? 1 : Count - 1;

			for (int i = 0; i <= last; i++)
				sum |= (short)(this[i] << ((mostSignificantByteFirst ? last - i : i) * 8));

			return sum;
		}

		public short ToShort()
		{
			return ToShort(true);
		}

		public long ToLong(bool mostSignificantByteFirst)
		{
			// FIXME: Generics would be nice if I could figure them out.
			//return ToNumber<long> (Data, most_significant_byte_first);
			long sum = 0;

			if (Count <= 0)
				return sum;

			int last = Count > 8 ? 7 : Count - 1;

			for (int i = 0; i <= last; i++)
				sum |= (long)this[i] << ((mostSignificantByteFirst ? last - i : i) * 8);

			return sum;
		}

		public long ToLong()
		{
			return ToLong(true);
		}

		public override string ToString()
		{
			return ToString(StringType.UTF8);
		}

		public string ToString(StringType type)
		{
			string s = StringTypeToEncoding(type, Mid(0, 2)).GetString(GetDataBuffer(), 0, Count);
			if (s.Length != 0 && (s[0] == 0xfffe || s[0] == 0xfeff)) // UTF16 BOM
				s = s.Substring(1);
			return s;
		}

		public override bool Equals(object obj)
		{
			ByteVector otherVector = obj as ByteVector;
			if (otherVector != null)
				return (this == otherVector);
			else return false;
		}

		public override int GetHashCode()
		{
			return Count;
		}
		#endregion
   
		#region Public and Internal Static Methods
		public static bool operator ==(ByteVector b1, ByteVector b2)
		{
			if ((object)b1 != null)
			{
				if ((object)b2 != null)
					return b1.Count == b2.Count && b1.StartsWith(b2);
				else
					return false;
			}
			else
			{
				if ((object)b2 == null)
					return true;
				else
					return false;
			}
		}

		public static bool operator !=(ByteVector b1, ByteVector b2)
		{
			return !(b1 == b2);
		}

		public static bool operator <(ByteVector b1, ByteVector b2)
		{
			if (b1 != null)
			{
				if (b2 != null)
				{
					for (int i = 0; i < b1.Count && i < b2.Count; i++)
						if (b1[i] < b2[i]) return true;
						
					return b1.Count < b2.Count;
				}
			}
			return false;
		}

		public static bool operator <=(ByteVector b1, ByteVector b2)
		{
			return b1 < b2 || b1 == b2;
		}

		public static bool operator >(ByteVector b1, ByteVector b2)
		{
			if (b1 != null)
			{
				if (b2 != null)
				{
					for (int i = 0; i < b1.Count && i < b2.Count; i++)
						if (b1[i] > b2[i]) return true;

					return b1.Count > b2.Count;
				}
			}
			return false;
		}

		public static bool operator >=(ByteVector b1, ByteVector b2)
		{
			return b1 > b2 || b1 == b2;
		}

		public static ByteVector operator +(ByteVector b1, ByteVector b2)
		{
			ByteVector sum = new ByteVector(b1);
			sum.Add(b2);
			return sum;
		}

		public static implicit operator ByteVector(byte value)
		{
			return new ByteVector(value);
		}

		public static implicit operator ByteVector(byte[] value)
		{
			return new ByteVector(value);
		}

		public static implicit operator ByteVector(string value)
		{
			return ByteVector.FromString(value);
		}
		
		public static int Compare(ByteVector b1, ByteVector b2)
		{
			if (b1 > b2)
				return 1;
			else if (b1 == b2)
				return 0;
			else
				return -1;
		}

		[System.CLSCompliant(false)]
		public static ByteVector FromUInt(uint value, bool mostSignificantByteFirst)
		{
			// FIXME: Generics would be nice if I could figure them out.
			//return FromNumber<uint> (value, most_significant_byte_first);
			ByteVector vector = new ByteVector();

			for (int i = 0; i < 4; i++)
				vector.Add((byte)(value >> ((mostSignificantByteFirst ? 3 - i : i) * 8) & 0xFF));

			return vector;
		}

		[System.CLSCompliant(false)]
		public static ByteVector FromUInt(uint value)
		{
			return FromUInt(value, true);
		}

		public static ByteVector FromShort(short value, bool mostSignificantByteFirst)
		{
			// FIXME: Generics would be nice if I could figure them out.
			//return FromNumber<short> (value, most_significant_byte_first);
			ByteVector vector = new ByteVector();

			for (int i = 0; i < 2; i++)
				vector.Add((byte)(value >> ((mostSignificantByteFirst ? 1 - i : i) * 8) & 0xFF));

			return vector;
		}

		public static ByteVector FromShort(short value)
		{
			return FromShort(value, true);
		}

		public static ByteVector FromLong(long value, bool mostSignificantByteFirst)
		{
			// FIXME: Generics would be nice if I could figure them out.
			//return FromNumber<long> (value, most_significant_byte_first);
			ByteVector vector = new ByteVector();

			for (int i = 0; i < 8; i++)
				vector.Add((byte)(value >> ((mostSignificantByteFirst ? 7 - i : i) * 8) & 0xFF));

			return vector;
		}

		public static ByteVector FromLong(long value)
		{
			return FromLong(value, true);
		}

		public static ByteVector FromString(string text, StringType type, int length)
		{
			if (text == null)
				return new ByteVector();

			if (text.Length > length)
				text = text.Substring(0, length);

			byte[] data = StringTypeToEncoding(type, null).GetBytes(text);

			if (type != StringType.UTF16)
				return new ByteVector(data);

			byte[] ordered_data = new byte[data.Length + 2];
			ordered_data[0] = 0xff;
			ordered_data[1] = 0xfe;
			data.CopyTo(ordered_data, 2);
			return new ByteVector(ordered_data);
		}

		public static ByteVector FromString(string text, StringType type)
		{
			return FromString(text, type, Int32.MaxValue);
		}

		public static ByteVector FromString(string text, int length)
		{
			return FromString(text, StringType.UTF8, length);
		}

		public static ByteVector FromString(string text)
		{
			return FromString(text, StringType.UTF8);
		}

		public static ByteVector FromUri(System.Uri uri)
		{
			byte[] tempOut;
			return FromUri(uri, out tempOut, false);
		}

		internal static ByteVector FromUri(System.Uri uri, out byte[] firstChunk, bool copyFirstChunk)
		{
			File.FileAbstractionCreator creator = File.GetFileAbstractionCreator();
			IFileAbstraction abstraction = creator(uri.LocalPath);

			using (System.IO.Stream stream = abstraction.ReadStream)
			{
				return FromStream(stream, out firstChunk, copyFirstChunk);
			}
		}

		public static ByteVector FromStream(System.IO.Stream stream)
		{
			byte[] buffer;
			return FromStream(stream, out buffer, false);
		}

		internal static ByteVector FromStream(System.IO.Stream stream, out byte[] firstChunk, bool copyFirstChunk)
		{
			ByteVector vector = new ByteVector();
			byte[] buffer = new byte[4096];
			int read_size = buffer.Length;
			int bytes_read = 0;
			bool set_first_chunk = false;

			firstChunk = null;

			while (true)
			{
				Array.Clear(buffer, 0, buffer.Length);
				int n = stream.Read(buffer, 0, read_size);
				vector.Add(buffer);
				bytes_read += n;

				if (!set_first_chunk)
				{
					if (copyFirstChunk)
					{
						if (firstChunk == null || firstChunk.Length != read_size)
						{
							firstChunk = new byte[read_size];
						}

						Array.Copy(buffer, firstChunk, n);
					}
					set_first_chunk = true;
				}

				if ((bytes_read == stream.Length && stream.Length > 0) ||
					(n < read_size && stream.Length <= 0))
				{
					break;
				}
			}

			if (stream.Length > 0 && vector.Count != stream.Length)
			{
				vector.Resize((int)stream.Length);
			}

			return vector;
		}
		#endregion
   
		#region IEnumerable Members
		public System.Collections.Generic.IEnumerator<byte> GetEnumerator()
		{
			return data.GetEnumerator();
		}

		System.Collections.IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		#endregion
	}
}
