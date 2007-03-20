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
	public class ApeTag : TagLib.Tag
	{
		#region Constructors
		public ApeTag() : base()
		{
			tagOffset = -1;
			footer = new ApeFooter();
			items = new Dictionary<string, ApeItem>(StringComparer.CurrentCultureIgnoreCase); //Hashtable();
		}

		public ApeTag(File file, long tagOffset) : this()
		{
			this.tagOffset = tagOffset;
			Read(file);
		}
		#endregion
      
		#region Private Fields
		private long tagOffset;
		private ApeFooter footer;
		private Dictionary<string, ApeItem> items; //Hashtable items;
		#endregion
            
		#region Private Static Fields
		private static ByteVector fileIdentifier = ApeFooter.FileIdentifier;
		#endregion

		#region Protected Methods
		protected void Read(File file)
		{
			if (file == null)
				return;

			try
			{
				file.Mode = FileAccessMode.Read;
			}
			catch (TagLibException)
			{ return; }

			file.Seek(tagOffset);
			footer.SetData(file.ReadBlock((int)ApeFooter.Size));

			if (footer.TagSize == 0 || footer.TagSize > (uint)file.Length)
				return;

			file.Seek(tagOffset + ApeFooter.Size - footer.TagSize);
			Parse(file.ReadBlock((int)(footer.TagSize - ApeFooter.Size)));
		}

		protected void Parse(ByteVector data)
		{
			if (data != null)
			{
				int position = 0;

				// 11 buffer is the minimum size for an APE item
				for (uint i = 0; i < footer.ItemCount && position <= data.Count - 11; i++)
				{
					ApeItem item = new ApeItem();
					item.Parse(data.Mid(position));

					SetItem(item.Key, item);

					position += item.Size;
				}
			}
			else throw new ArgumentNullException("data");
		}
		
		protected void AddEnumerableValues(string key, IEnumerable values, bool replace)
		{
			if (replace)
				RemoveItem(key);

			if (values != null)
				foreach (string s in values)
					AddValue(key, s, false);
		}
		#endregion

		#region Public Properties
		public override string Title
		{
			get
			{
				ApeItem item = GetItem("TITLE");
				return item != null ? item.ToString() : null;
			}
			set
			{
				AddValue("TITLE", value, true);
			}
		}

		public override IList<string> Artists
		{
			get
			{
				ApeItem item = GetItem("ARTIST");
				return item != null ? item.ToStringList() : new List<string>();
			}
			set
			{
				AddValues("ARTIST", value, true);
			}
		}

		public override IList<string> Performers
		{
			get
			{
				ApeItem item = GetItem("PERFORMER");
				
				return item != null ? item.ToStringList() : new List<string>();
			}
			set
			{
				AddValues("PERFORMER", value, true);
			}
		}

		public override IList<string> Composers
		{
			get
			{
				ApeItem item = GetItem("COMPOSER");
				return item != null ? item.ToStringList() : new List<string>();
			}
			set
			{
				AddValues("COMPOSER", value, true);
			}
		}

		public override string Album
		{
			get
			{
				ApeItem item = GetItem("ALBUM");
				return item != null ? item.ToString() : null;
			}
			set
			{
				AddValue("ALBUM", value, true);
			}
		}

		public override string Comment
		{
			get
			{
				ApeItem item = GetItem("COMMENT");
				return item != null ? item.ToString() : null;
			}
			set
			{
				AddValue("COMMENT", value, true);
			}
		}

		public override IList<string> Genres
		{
			get
			{
				ApeItem item = GetItem("GENRE");
				return item != null ? item.ToStringList() : new List<string>();
			}
			set
			{
				AddValues("GENRE", value, true);
			}
		}

		[System.CLSCompliant(false)]
		public override uint Year
		{
			get
			{
				ApeItem yearItem = GetItem("YEAR");
				if (yearItem != null)
				{
					uint year = 0;
					if (uint.TryParse(yearItem.ToString().Substring(0, 4), out year))
						return year;
					else
						return 0;
				}
				else return 0;
			}
			set
			{
				AddValue("YEAR", value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo), true);
			}
		}

		[System.CLSCompliant(false)]
		public override uint Track
		{
			get
			{
				ApeItem trackItem = GetItem("TRACK");
				if (trackItem != null)
				{
					uint track = 0;
					string[] trackData = trackItem.ToString().Split('/');
					if (trackData != null && trackData.Length > 0)
					{
						if (uint.TryParse(trackData[0], out track))
							return track;
					}
				}
				return 0;
			}
			set
			{
				uint count = TrackCount;
				if (count != 0)
					AddValue("TRACK", value + "/" + count, true);
				else
					AddValue("TRACK", value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo), true);
			}
		}

		[System.CLSCompliant(false)]
		public override uint TrackCount
		{
			get
			{
				ApeItem trackItem = GetItem("TRACK");
				if (trackItem != null)
				{
					uint track = 0;
					string[] trackData = trackItem.ToString().Split('/');
					if (trackData != null && trackData.Length > 1)
					{
						if (uint.TryParse(trackData[1], out track))
							return track;
					}
				}
				return 0;
			}
			set
			{
				AddValue("TRACK", Track + "/" + value, true);
			}
		}

		[System.CLSCompliant(false)]
		public override uint Disc
		{
			get
			{
				ApeItem discItem = GetItem("DISC");
				if (discItem != null)
				{
					uint disc = 0;
					string[] discData = discItem.ToString().Split('/');
					if (discData != null && discData.Length > 0)
					{
						if (uint.TryParse(discData[0], out disc))
							return disc;
					}
				}
				return 0;
			}
			set
			{
				uint count = DiscCount;
				if (count != 0)
					AddValue("DISC", value + "/" + count, true);
				else
					AddValue("DISC", value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo), true);
			}
		}

		[System.CLSCompliant(false)]
		public override uint DiscCount
		{
			get
			{
				ApeItem discItem = GetItem("DISC");
				if (discItem != null)
				{
					uint disc = 0;
					string[] discData = discItem.ToString().Split('/');
					if (discData != null && discData.Length > 1)
					{
						if (uint.TryParse(discData[1], out disc))
							return disc;
					}
				}
				return 0;
			}
			set
			{
				AddValue("DISC", Disc + "/" + value, true);
			}
		}

		public override bool IsEmpty
		{
			get {return items.Count == 0;}
		}

		public ApeFooter Footer
		{
			get {return footer;}
		}
		#endregion
		
		#region Public Static Properties
		public static ByteVector FileIdentifier
		{
			get {return fileIdentifier;}
		}
		#endregion

		#region Public Methods
		public ByteVector Render()
		{
			ByteVector data = new ByteVector();
			uint item_count = 0;

			foreach (ApeItem item in items.Values)
			{
				data.Add(item.Render());
				item_count++;
			}

			footer.ItemCount = item_count;
			footer.TagSize = (uint)(data.Count + ApeFooter.Size);
			footer.HeaderPresent = true;

			return footer.RenderHeader() + data + footer.RenderFooter();
		}

		public void RemoveItem(string key)
		{
			items.Remove(key);
		}

		public ApeItem GetItem(string key)
		{
			return items.ContainsKey(key) ? items[key] : null;
		}

		public void AddValue(string key, string value, bool replace)
		{
			if (replace)
				RemoveItem(key);

			if (value != null && value.Length > 0)
			{
				StringCollection l = new StringCollection();

				if (GetItem(key) != null && !replace)
					l.Add(GetItem(key).ToStringArray());

				l.Add(value);

				SetItem(key, new ApeItem(key, l));
			}
		}

		public void AddValue(string key, string value)
		{
			AddValue(key, value, true);
		}

		public void AddValues(string key, IList<string> values, bool replace)
		{
			AddEnumerableValues(key, values, replace);
		}
		
		public void AddValues(string key, IList<string> values)
		{
			AddEnumerableValues(key, values, true);
		}

		public void AddValues(string key, string[] values, bool replace)
		{
			AddEnumerableValues(key, values, replace);
		
			//if (replace)
				//RemoveItem(key);

			//if (values != null)
				//foreach (string s in values)
					//AddValue(key, s, false);
		}

		public void AddValues(string key, string[] values)
		{
			AddEnumerableValues(key, values, true);
		
			//AddValues(key, values, true);
		}

		public void SetItem(string key, ApeItem item)
		{
			if (items.ContainsKey(key))
				items[key] = item;
			else
				items.Add(key, item);
		}
		#endregion
	}
}
