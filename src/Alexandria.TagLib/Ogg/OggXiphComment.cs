/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : tag.cpp from TagLib
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
	public class OggXiphComment : Tag, IEnumerable<StringCollection>
	{
		#region Constructors
		public OggXiphComment() : base()
		{
			fieldList = new Dictionary<string, StringCollection>(StringComparer.CurrentCultureIgnoreCase);
			//vendorId = null;
			//commentField = null;
		}

		public OggXiphComment(ByteVector data) : this()
		{
			Parse(data);
		}
		#endregion
	
		#region Private Fields
		//private Hashtable fieldList;
		private System.Collections.Generic.Dictionary<string, StringCollection> fieldList;
		private string vendorId;
		private string commentField;
		#endregion

		#region Protected Methods
		protected void Parse(ByteVector data)
		{
			if (data != null)
			{
				// The first thing in the comment data is the vendor ID length, followed by a
				// UTF8 string with the vendor ID.

				int pos = 0;

				int vendorLength = (int)data.Mid(0, 4).ToUInt(false);
				pos += 4;

				vendorId = data.Mid(pos, vendorLength).ToString(StringType.UTF8);
				pos += vendorLength;

				// Next the number of fields in the comment vector.

				int commentFields = (int)data.Mid(pos, 4).ToUInt(false);
				pos += 4;

				for (int i = 0; i < commentFields; i++)
				{
					// Each comment field is in the format "KEY=value" in a UTF8 string and has
					// 4 buffer before the type starts that gives the length.

					int commentLength = (int)data.Mid(pos, 4).ToUInt(false);
					pos += 4;

					string comment = data.Mid(pos, commentLength).ToString(StringType.UTF8);
					pos += commentLength;

					int commentSeparatorPosition = comment.IndexOf('=');

					string key = comment.Substring(0, commentSeparatorPosition);
					string value = comment.Substring(commentSeparatorPosition + 1);

					AddField(key, value, false);
				}
			}
			else throw new ArgumentNullException("data");
		}

		protected void AddEnumerableFields(string key, IEnumerable values, bool replace)
		{
			if (values != null)
			{
				if (replace)
					//RemoveField(key.ToUpper());
					RemoveField(key);

				foreach (string value in values)
					AddField(key, value, false);
			}
			else throw new ArgumentNullException("values");
		}
		#endregion

		#region Public Properties
		public override bool IsEmpty
		{
			get
			{
				foreach (StringCollection list in fieldList.Values)
					if (!list.IsEmpty) return false;

				return true;
			}
		}

		[System.CLSCompliant(false)]
		public uint FieldCount
		{
			get
			{
				uint count = 0;
				foreach (StringCollection list in fieldList.Values)
					count += (uint)list.Count;

				return count;
			}
		}

		public string VendorId
		{
			get
			{
				return vendorId;
			}
		}

		public override string Title
		{
			get
			{
				StringCollection list = GetField("TITLE");
				return (list != null && list.Count != 0) ? list[0] : null;
			}
			set
			{
				AddField("TITLE", value);
			}
		}

		public override IList<string> Artists
		{
			get
			{
				StringCollection collection = GetField("ARTIST");
				return (collection != null && collection.Count > 0) ? collection.ToList() : new List<string>();
			}
			set
			{
				AddFields("ARTIST", value);
			}
		}

		public override IList<string> Performers
		{
			get
			{
				StringCollection collection = GetField("PERFORMER");
				return (collection != null && collection.Count != 0) ? collection.ToList() : new List<string>();
			}
			set
			{
				AddFields("PERFORMER", value);
			}
		}

		public override IList<string> Composers
		{
			get
			{
				StringCollection collection = GetField("COMPOSER");
				return (collection != null && collection.Count != 0) ? collection.ToList() : new List<string>();
			}
			set
			{
				AddFields("COMPOSER", value);
			}
		}

		public override string Album
		{
			get
			{
				StringCollection list = GetField("ALBUM");
				return (list != null && list.Count != 0) ? list[0] : null;
			}
			set
			{
				AddField("ALBUM", value);
			}
		}

		public override string Comment
		{
			get
			{
				StringCollection list = GetField("DESCRIPTION");
				commentField = "DESCRIPTION";

				if (list == null || list.Count == 0)
				{
					list = GetField("COMMENT");
					commentField = "COMMENT";
				}

				return (list != null && list.Count != 0) ? list[0] : null;
			}
			set
			{
				AddField(commentField == null ? "DESCRIPTION" : commentField, value);
			}
		}

		public override IList<string> Genres
		{
			get
			{
				StringCollection collection = GetField("GENRE");
				return (collection != null && collection.Count != 0) ? collection.ToList() : new List<string>();
			}
			set
			{
				AddFields("GENRE", value);
			}
		}

		[System.CLSCompliant(false)]
		public override uint Year
		{
			get
			{
				uint year = 0;
				StringCollection list = GetField("DATE");
				if (list != null && list.Count > 0)
				{
					string yearData = list[0].Substring(0, 4);
					if (uint.TryParse(yearData, out year))
						return year;
				}
				return 0;
			}
			set
			{
				AddField("DATE", value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
			}
		}

		[System.CLSCompliant(false)]
		public override uint Track
		{
			get
			{
				uint track = 0;
				StringCollection list = GetField("TRACKNUMBER");
				if (list != null && list.Count > 0)
				{					
					if (uint.TryParse(list[0], out track))
						return track;
				}
				return 0;
			}
			set
			{
				AddField("TRACKNUMBER", value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
			}
		}

		[System.CLSCompliant(false)]
		public override uint TrackCount
		{
			get
			{
				uint trackCount = 0;
				StringCollection list = GetField("TRACKTOTAL");
				if (list != null && list.Count > 0)
				{
					if (uint.TryParse(list[0], out trackCount))
						return trackCount;
				}
				return 0;
			}
			set
			{
				AddField("TRACKTOTAL", value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
			}
		}

		[System.CLSCompliant(false)]
		public override uint Disc
		{
			get
			{
				uint disc = 0;
				StringCollection list = GetField("DISCNUMBER");
				if (list != null && list.Count > 0)
				{
					if (uint.TryParse(list[0], out disc))
						return disc;
				}
				return 0;
			}
			set
			{
				AddField("DISCNUMBER", value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
			}
		}

		[System.CLSCompliant(false)]
		public override uint DiscCount
		{
			get
			{
				uint discCount = 0;
				StringCollection list = GetField("DISCTOTAL");
				if (list != null && list.Count > 0)
				{
					if (uint.TryParse(list[0], out discCount))
						return discCount;
				}
				return 0;
			}
			set
			{
				AddField("DISCTOTAL", value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
			}
		}
		#endregion

		#region Public Methods
		public StringCollection GetField(string key)
		{
			// was key.ToUpper()
			return (fieldList.ContainsKey(key)) ? (StringCollection)fieldList[key] : null;
		}

		public void AddField(string key, string value, bool replace)
		{
			if (replace)
				// was key.ToUpper()
				RemoveField(key);
		 
			if (value != null && value.Trim().Length > 0)
			{
				// was key.ToUpper()
				if (!fieldList.ContainsKey(key))
					fieldList.Add(key, new StringCollection());
		 
				// was key.ToUpper()
				((StringCollection) fieldList[key]).Add(value);
			}
		}

		public void AddField(string key, string value)
		{
			AddField(key, value, true);
		}

		public void AddFields(string key, IList<string> values, bool replace)
		{
			AddEnumerableFields(key, values, replace);
		}

		public void AddFields(string key, IList<string> values)
		{
			AddEnumerableFields(key, values, true);
		}

		public void AddFields(string key, string[] values, bool replace)
		{
			AddEnumerableFields(key, values, replace);
		
			//if (replace)
				//RemoveField(key.ToUpper());
		 
			//foreach (string s in values)
				//AddField(key, s, false);
		}

		public void AddFields(string key, string[] values)
		{
			AddEnumerableFields(key, values, true);
		
			//AddFields(key, values, true);
		}

		public void RemoveField(string key, string value)
		{
			// was key.ToUpper()
			if (!fieldList.ContainsKey(key))
				return;
		 
			// was key.ToUpper()			
			StringCollection list = (StringCollection) fieldList[key];
		 
			if (value == null)
				list.Clear();
			else
			{
				int index;         
				while ((index = list.IndexOf(value)) >=0)
					list.RemoveAt(index);
			}
		}

		public void RemoveField(string key)
		{
			RemoveField(key, null);
		}

		public ByteVector Render(bool addFramingBit)
		{
			ByteVector data = new ByteVector ();

			// Add the vendor ID length and the vendor ID.  It'field important to use the
			// lenght of the data(String::UTF8) rather than the lenght of the the string
			// since this is UTF8 type and there may be more characters in the data than
			// in the UTF16 string.

			ByteVector vendorData = ByteVector.FromString(vendorId, StringType.UTF8);

			data.Add(ByteVector.FromUInt((uint) vendorData.Count, false));
			data.Add(vendorData);

			// Add the number of fields.

			data.Add(ByteVector.FromUInt(FieldCount, false));

			// Iterate over the the field lists.  Our iterator returns a
			// std::pair<String, StringList> where the first String is the field name and
			// the StringList is the values associated with that field.

			foreach(System.Collections.Generic.KeyValuePair<string, StringCollection> entry in fieldList)   //(DictionaryEntry entry in fieldList)
			{
				// And now iterate over the values of the current text.

				string fieldName = entry.Key; //(string)entry.Key;
				StringCollection values = entry.Value; //(StringList)entry.Value;

				foreach (string value in values)
				{
					ByteVector fieldData = ByteVector.FromString(fieldName, StringType.UTF8);
					fieldData.Add((byte) '=');
					fieldData.Add(ByteVector.FromString(value, StringType.UTF8));

					data.Add(ByteVector.FromUInt((uint) fieldData.Count, false));
					data.Add(fieldData);
				}
			}

			// Append the "framing bit".
			if (addFramingBit)
				data.Add((byte)1);

			return data;
		}

		public ByteVector Render()
		{
			return Render(true);
		}
		#endregion

		#region IEnumerable<StringList> Members
		public IEnumerator<StringCollection> GetEnumerator()
		{
			foreach(StringCollection list in fieldList.Values)
				yield return list;
		}
		#endregion

		#region IEnumerable Members
		IEnumerator IEnumerable.GetEnumerator()
		{
			return fieldList.Keys.GetEnumerator();
		}
		#endregion
	}
}
