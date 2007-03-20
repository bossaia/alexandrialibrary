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
using System.Collections;
using System.Collections.Generic;

namespace AlexandriaOrg.Alexandria.TagLib
{   
	public class ApeItem
	{
		#region Constructors
		public ApeItem()
		{
			type = ApeItemType.Text;
			//key = null;
			//value = null;
			text = new StringCollection();
			//readOnly = false;
		}

		public ApeItem(string key, string value) : this()
		{
			this.key = key;
			this.text.Add(value);
		}

		public ApeItem(string key, StringCollection value) : this()
		{
			this.key = key;
			text.Add(value);
		}

		public ApeItem(string key, ByteVector value) : this()
		{
			this.key = key;
			this.type = ApeItemType.Binary;
			this.value = value;
		}

		public ApeItem(ApeItem item) : this()
		{
			if (item != null)
			{
				type = item.Type;
				key = item.Key;
				value = item.Value;
				text = new StringCollection(item.ToStringArray());
				//readOnly = false;
			}
		}
		#endregion
		
		#region Private Fields
		private ApeItemType type;
		private string key;
		private ByteVector value;
		private StringCollection text;
		private bool readOnly;
		#endregion
		
		#region Public Properties
		public string Key
		{
			get { return key; }
		}
		
		public ByteVector Value
		{
			get { return value; }
		}
		
		public int Size
		{
			get { return 8 + key.Length + 1 + value.Count; }
		}

		public ApeItemType Type
		{
			get { return type; }
			set { type = value; }
		}

		public bool ReadOnly
		{
			get { return readOnly; }
			set { readOnly = value; }
		}

		public bool IsEmpty
		{
			get
			{
				if (type != ApeItemType.Binary)
					return text.IsEmpty;
				else
					return value.IsEmpty;
			}
		}
		#endregion
		
		#region Public Methods
		public override string ToString()
		{
			return text.ToString();
		}

		public string[] ToStringArray()
		{
			return text.ToArray();
		}
		
		public IList<string> ToStringList()
		{
			return text.ToList();
		}
		
		public void Parse(ByteVector data)
		{
			if (data != null)
			{
				// 11 buffer is the minimum size for an APE item
				if (data.Count < 11)
				{
					TagLibDebugger.Debug("APE.Item.Parse() -- no data in item");
					return;
				}

				uint value_length = data.Mid(0, 4).ToUInt(false);
				uint flags = data.Mid(4, 4).ToUInt(false);

				int pos = data.Find(new ByteVector(1), 8);

				key = data.Mid(8, pos - 8).ToString(StringType.UTF8);
				value = data.Mid(pos + 1, (int)value_length);

				ReadOnly = (flags & 1) == 1;
				Type = (ApeItemType)((flags >> 1) & 3);

				if (Type != ApeItemType.Binary)
				{
					text.Clear();
					text = new StringCollection(ByteVectorCollection.Split(value, (byte)0), StringType.UTF8);
				}
			}
			else throw new ArgumentNullException("data");
		}

		public ByteVector Render()
		{
			ByteVector data = new ByteVector();
			uint flags = (uint)((ReadOnly) ? 1 : 0) | ((uint)Type << 1);

			if (IsEmpty)
				return data;

			if (type != ApeItemType.Binary)
			{
				value = new ByteVector();
				for (int i = 0; i < text.Count; i++)
				{
					if (i != 0)
						value.Add((byte)0);

					value.Add(ByteVector.FromString(text[i], StringType.UTF8));
				}
			}

			data.Add(ByteVector.FromUInt((uint)value.Count, false));
			data.Add(ByteVector.FromUInt(flags, false));
			data.Add(ByteVector.FromString(key, StringType.UTF8));
			data.Add((byte)0);
			data.Add(value);

			return data;
		}
		#endregion
	}
}