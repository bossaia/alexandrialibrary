/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
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
using Alexandria;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public class AsfTag : Tag
	{
		#region Constructors
		public AsfTag(AsfHeaderObject header) : base()
		{
			description = new AsfContentDescriptionObject();
			extDescription = new AsfExtendedContentDescriptionObject();

			foreach (AsfObject child in header.Children)
			{
				if (child is AsfContentDescriptionObject)
					description = (AsfContentDescriptionObject)child;

				if (child is AsfExtendedContentDescriptionObject)
					extDescription = (AsfExtendedContentDescriptionObject)child;
			}
		}
		#endregion
		
		#region Private Fields
		private AsfContentDescriptionObject description;
		private AsfExtendedContentDescriptionObject extDescription;
		#endregion
		
		#region Private Methods
		private string GetDescriptorString(IEnumerable names)
		{
			foreach (string name in names)
			{
				foreach (AsfContentDescriptor descriptor in this.GetDescriptors(name))
					if (descriptor != null && descriptor.Type == AsfDataType.Unicode && descriptor.ToString() != null)
						return descriptor.ToString();
			}
			return null;
		}
		#endregion
		
		#region Private Static Methods
		private static IList<string> SplitAndClean(string value)
		{
			if (value == null || value.Trim().Length == 0)
				return new List<string>();
				//return new string[]{};

			StringCollection collection = StringCollection.Split(value, ";");
			for (int i = 0; i < collection.Count; i++)
				collection[i] = collection[i].Trim();
			return collection.ToList();
			//return collection.ToArray();
		}				
		
		private static string ToSemicolonString(IList<string> list)
		{
			string value = string.Empty;
		
			if (list != null)
			{
				for(int i=0; i < list.Count; i++)
				{
					value += list[i];
					if (i < (list.Count-1)) value += "; ";
				}
			}
			
			return value;
		}
		#endregion
   
		#region Public Properties
		public override string Title
		{
			get { return description.Title; }
			set { description.Title = value; }
		}

		// 
		/// <summary>
		/// Get or set the Artists
		/// </summary>
		/// <remarks>
		/// This may seem unintuitive, but the artists field is actually performers. 
		/// This makes sense as Artists should descibe the album artist and Performers
		/// should describe who is in the song. Because this may not be set, we'll 
		/// return performers if we can'type get an artist.
		/// </remarks>
		public override IList<string> Artists
		{
			get
			{
				string value = GetDescriptorString("WM/AlbumArtist", "AlbumArtist");
				return (value != null) ? SplitAndClean(value) : Performers;
			}
			set
			{
				//SetDescriptorString(String.Join("; ", ConvertToArray(value)), "WM/AlbumArtist", "AlbumArtist");
				SetDescriptorString(ToSemicolonString(value), "WM/AlbumArtist", "AlbumArtist");
			}
		}

		public override IList<string> Performers
		{
			get { return SplitAndClean(description.Author); }
			set { description.Author = ToSemicolonString(value); }
		}

		public override IList<string> Composers
		{
			get { return SplitAndClean(GetDescriptorString("WM/Composer", "Composer")); }
			set { SetDescriptorString(ToSemicolonString(value), "WM/Composer", "Composer"); }
		}

		public override string Album
		{
			get { return GetDescriptorString("WM/AlbumTitle", "Album"); }
			set { SetDescriptorString(value, "WM/AlbumTitle", "Album"); }
		}

		public override string Comment
		{
			get { return description.Description; }
			set { description.Description = value; }
		}

		public override IList<string> Genres
		{
			get
			{
				string value = GetDescriptorString("WM/Genre", "Genre");
				return (value != null) ? SplitAndClean(value) : new List<string>();
			}
			set
			{
				SetDescriptorString(ToSemicolonString(value), "WM/Genre", "Genre");
			}
		}

		[System.CLSCompliant(false)]
		public override uint Year
		{
			get
			{
				string value = GetDescriptorString("WM/Year");
				if (value != null)
				{
					uint year = 0;
					if (uint.TryParse(value.Substring(0, 4), out year))
						return year;
					else
						return 0;
				}
				else return 0;
			}
			set
			{
				SetDescriptorString(value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo), "WM/Year");
			}
		}

		[System.CLSCompliant(false)]
		public override uint Track
		{
			get
			{
				foreach (AsfContentDescriptor desc in GetDescriptors("WM/TrackNumber"))
					if (desc.ToDWord() != 0)
						return desc.ToDWord();

				return 0;
			}
			set
			{
				SetDescriptors("WM/TrackNumber", new AsfContentDescriptor("WM/TrackNumber", value));
			}
		}

		// This is not defined in the spec. If correct methods come along, correct.
		[System.CLSCompliant(false)]
		public override uint TrackCount
		{
			get
			{
				foreach (AsfContentDescriptor desc in GetDescriptors("TrackTotal"))
					if (desc.ToDWord() != 0)
						return desc.ToDWord();

				return 0;
			}
			set
			{
				SetDescriptors("TrackTotal", new AsfContentDescriptor("TrackTotal", value));
			}
		}

		[System.CLSCompliant(false)]
		public override uint Disc
		{
			get
			{
				string value = GetDescriptorString("WM/PartOfSet");
				if (value != null)
				{
					string[] discData = value.Split('/');
					if (discData != null && discData.Length > 0)
					{
						uint disc = 0;
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
					SetDescriptorString(value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo) + "/" + count.ToString(System.Globalization.NumberFormatInfo.InvariantInfo), "WM/PartOfSet");
				else
					SetDescriptorString(value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo), "WM/PartOfSet");
			}
		}

		[System.CLSCompliant(false)]
		public override uint DiscCount
		{
			get
			{
				string value = GetDescriptorString("WM/PartOfSet");
				if (value != null)
				{
					string[] discData = value.Split('/');
					if (discData != null && discData.Length > 1)
					{
						uint discCount = 0;
						if (uint.TryParse(discData[1], out discCount))
							return discCount;
					}
				}
				return 0;
			}
			set
			{
				SetDescriptorString(Disc.ToString(System.Globalization.NumberFormatInfo.InvariantInfo) + "/" + value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo), "WM/PartOfSet");
			}
		}

		public AsfContentDescriptionObject ContentDescriptionObject
		{
			get { return description; }
		}

		public AsfExtendedContentDescriptionObject ExtendedContentDescriptionObject
		{
			get { return extDescription; }
		}

		public override IList<IImage> Pictures
		{
			get
			{
				IList<IImage> list = new List<IImage>();

				foreach (AsfContentDescriptor descriptor in GetDescriptors("WM/Picture"))
				{
					ByteVector data = descriptor.ToByteVector();
					Picture picture = new Picture();

					if (data.Count < 9)
						continue;

					int offset = 0;
					picture.Type = (PictureType)data[0];
					offset += 1;
					int size = (int)data.Mid(offset, 4).ToUInt(false);
					offset += 4;

					int found = data.Find("\0\0", offset, 2);
					if (found == -1)
						continue;
					picture.MimeType = data.Mid(offset, found - offset).ToString(StringType.UTF16LE);
					offset = found + 2;

					found = data.Find("\0\0", offset, 2);
					if (found == -1)
						continue;
					picture.Description = data.Mid(offset, found - offset).ToString(StringType.UTF16LE);
					offset = found + 2;

					picture.Data = data.Mid(offset, size);

					list.Add(picture);
				}

				return list;
			}

			set
			{
				if (value == null || value.Count == 0)
				{
					RemoveDescriptors("WM/Picture");
					return;
				}

				IList<IPicture> pictures = value as IList<IPicture>;
				if (pictures != null)
				{
					AsfContentDescriptor[] descriptors = new AsfContentDescriptor[pictures.Count];
					for (int i = 0; i < pictures.Count; i++)
					{
						ByteVector vector = new ByteVector();
						vector.Add((byte)pictures[i].Type);
						vector.Add(AsfObject.RenderDWord((uint)pictures[i].Data.Count));
						vector.Add(AsfObject.RenderUnicode(pictures[i].MimeType));
						vector.Add(AsfObject.RenderUnicode(pictures[i].Description));
						vector.Add(pictures[i].Data);

						descriptors[i] = new AsfContentDescriptor("WM/Picture", vector);
					}

					SetDescriptors("WM/Picture", descriptors);
				}
			}
		}
		#endregion
		
		#region Public Methods
		public void RemoveDescriptors(string name)
		{
			extDescription.RemoveDescriptors(name);
		}

		public IList<AsfContentDescriptor> GetDescriptors(string name)
		{
			return extDescription.GetDescriptorsByName(name);
		}

		//public AsfContentDescriptor[] GetDescriptorsByName(string name)
		//{
			//return extDescription.GetDescriptorsByName(name);
		//}

		public void SetDescriptors(string name, params AsfContentDescriptor[] descriptors)
		{
			extDescription.SetDescriptorsByName(name, descriptors);
		}

		public void AddDescriptor(AsfContentDescriptor descriptor)
		{
			extDescription.Descriptors.Add(descriptor);
			//AddDescriptor(descriptor);
		}		

		public string GetDescriptionString(IList<string> names)
		{
			return GetDescriptorString(names);
		}

		public string GetDescriptorString(params string[] names)
		{
			return GetDescriptorString(names);
			//foreach (string name in names)
			//{
				//foreach (AsfContentDescriptor descriptor in GetDescriptorsByName(name))
					//if (descriptor != null && descriptor.Type == AsfDataType.Unicode && descriptor.ToString() != null)
						//return descriptor.ToString();
			//}
			//return null;
		}

		public void SetDescriptorString(string value, IList<string> names)
		{
			//if (value != null && value.Length > 0)
			//{
				if (names != null)
				{
					if (names.Count > 0)
					{
						SetDescriptors(names[0], new AsfContentDescriptor(names[0], value));
				
						for (int i = 1; i < names.Count; i++)
							RemoveDescriptors(names[i]);
					}
				}
				else throw new ArgumentNullException("names");
			//}
			//else throw new ArgumentNullException("value");
		}

		public void SetDescriptorString(string value, params string[] names)
		{
			//if (value != null && value.Length > 0)
			//{
				if (names != null)
				{
					if (names.Length > 0)
					{
						SetDescriptors(names[0], new AsfContentDescriptor(names[0], value));

						for (int i = 1; i < names.Length; i++)
							RemoveDescriptors(names[i]);
					}
				}
				else throw new ArgumentNullException("names");
			//}
			//else throw new ArgumentNullException("value");
		}
		#endregion
	}
}
