/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : id3v2frame.cpp from TagLib
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

namespace Alexandria.TagLib
{   
	public class AsfContentDescriptor
	{
		#region Constructors
		public AsfContentDescriptor(string name, string value) : this()
		{
			this.name = name;
			this.type = AsfDataType.Unicode;
			this.sValue = value;
		}

		public AsfContentDescriptor(string name, ByteVector value) : this()
		{
			this.name = name;
			this.type = AsfDataType.Bytes;
			this.bvValue = new ByteVector(value);
		}

		[System.CLSCompliant(false)]
		public AsfContentDescriptor(string name, uint value) : this()
		{
			this.name = name;
			this.type = AsfDataType.DWord;
			this.lValue = value;
		}

		public AsfContentDescriptor(string name, long value) : this()
		{
			this.name = name;
			this.type = AsfDataType.QWord;
			this.lValue = value;
		}

		public AsfContentDescriptor(string name, short value) : this()
		{
			this.name = name;
			this.type = AsfDataType.Word;
			this.lValue = value;
		}

		public AsfContentDescriptor(string name, bool value) : this()
		{
			this.name = name;
			this.type = AsfDataType.Bool;
			this.lValue = value ? 1 : 0;
		}

		protected AsfContentDescriptor()
		{
			type = AsfDataType.Unicode;
			//name    = null;
			//sValue  = null;
			//bvValue = null;
			//lValue  = 0;
		}

		protected internal AsfContentDescriptor(AsfFile file) : this()
		{
			Parse(file);
		}
		#endregion
	
		#region Private Fields
		private AsfDataType type;
		private string name;
		private string sValue;
		private ByteVector bvValue;
		private long lValue;
		#endregion
      
		#region Protected Methods
		protected bool Parse(AsfFile file)
		{
			if (file != null)
			{
				int size = file.ReadWord();
				name = file.ReadUnicode(size);

				type = (AsfDataType)file.ReadWord();
				size = file.ReadWord();

				switch (type)
				{
					case AsfDataType.Word:
						lValue = file.ReadWord();
						break;

					case AsfDataType.Bool:
						lValue = file.ReadDWord();
						break;

					case AsfDataType.DWord:
						lValue = file.ReadDWord();
						break;

					case AsfDataType.QWord:
						lValue = file.ReadQWord();
						break;

					case AsfDataType.Unicode:
						sValue = file.ReadUnicode(size);
						break;

					case AsfDataType.Bytes:
						bvValue = file.ReadBlock(size);
						break;

					default:
						return false;
				}

				return true;
			}
			else throw new ArgumentNullException("file");
		}
		#endregion
      
		#region Public Properties
		public string Name
		{
			get {return name;}
		}

		public AsfDataType Type
		{
			get {return type;}
		}
		#endregion
		
		#region Public Methods
		public override string ToString()
		{
			return sValue;
		}

		public ByteVector ToByteVector()
		{
			return bvValue;
		}

		public bool ToBool()
		{
			return lValue != 0;
		}

		[System.CLSCompliant(false)]
		public uint ToDWord()
		{
			if (type == AsfDataType.Unicode && sValue != null)
			{
				uint dWord = 0;
				if (System.UInt32.TryParse(sValue, out dWord))
					return dWord;
				else
					return (uint)lValue;
			}
			else return (uint)lValue;
		}

		public long ToQWord()
		{
			if (type == AsfDataType.Unicode && sValue != null)
			{
				long qWord = 0;
				if (long.TryParse(sValue, out qWord))
					return qWord;
				else
					return (long)lValue;
			}
			else return (long)lValue;
		}

		public short ToWord()
		{
			if (type == AsfDataType.Unicode && sValue != null)
			{
				short word = 0;
				if (short.TryParse(sValue, out word))
					return word;
				else
					return (short)lValue;
			}
			else return (short)lValue;
		}

		public ByteVector Render()
		{
			ByteVector v = AsfObject.RenderUnicode(name);

			ByteVector data = AsfObject.RenderWord((short)v.Count) + v + AsfObject.RenderWord((short)type);

			switch (type)
			{
				case AsfDataType.Unicode:
					v = AsfObject.RenderUnicode(sValue);
					data += AsfObject.RenderWord((short)v.Count) + v;
					break;
				case AsfDataType.Bytes:
					data.Add(AsfObject.RenderWord((short)bvValue.Count));
					data.Add(bvValue);
					break;
				case AsfDataType.Bool:
					data.Add(AsfObject.RenderWord(4));
					data.Add(AsfObject.RenderDWord((uint)lValue));
					break;
				case AsfDataType.DWord:
					data.Add(AsfObject.RenderWord(4));
					data.Add(AsfObject.RenderDWord((uint)lValue));
					break;
				case AsfDataType.QWord:
					data.Add(AsfObject.RenderWord(8));
					data.Add(AsfObject.RenderQWord(lValue));
					break;
				case AsfDataType.Word:
					data.Add(AsfObject.RenderWord(2));
					data.Add(AsfObject.RenderWord((short)lValue));
					break;
				default:
					return null;
			}

			return data;
		}
		#endregion
	}
}
