#region License (LGPL)
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
#endregion

using System;
using System.Collections;
using System.Collections.Generic;

namespace Alexandria.TagLib
{
	public class Mpeg4AppleTag : Tag, IEnumerable<Mpeg4Box>
	{
		#region Constructors
		public Mpeg4AppleTag(Mpeg4AppleItemListBox box, Mpeg4File file) : base()
		{
			// Hold onto the ilst_box and file. If the box doesn'type exist, create
			// one.
			listBox = (box == null) ? new Mpeg4AppleItemListBox() : box;
			this.file = file;
		}

		public Mpeg4AppleTag(Mpeg4File file) : this(null, file)
		{
		}
		#endregion
	
		#region Private Fields
      private Mpeg4AppleItemListBox listBox;
      private Mpeg4File file;
      #endregion

		#region Private Static Methods
		private static ByteVector FixId(ByteVector vector)
		{
			// IF we have a three byte type (like "wrt"), add the extra byte.
			if (vector.Count == 3)
				vector.Insert(0, 0xa9);
			return vector;
		}
		#endregion

		#region Public Properties
		public override string Title
		{
			get
			{
				string[] text = GetText(FixId("nam"));
				return text.Length == 0 ? null : text[0];
			}
			set { SetText(FixId("nam"), value); }
		}

		public override IList<string> Artists
		{
			get { return GetTextList(FixId("ART")); }
			set { SetText(FixId("ART"), value); }
		}

		// FIXME: If we can figure out the performers box, we'll migrate.
		public override IList<string> Performers
		{
			get { return GetTextList(FixId("prf")); }
			set { SetText(FixId("prf"), value); }
		}

		public override IList<string> Composers
		{
			get { return GetTextList(FixId("wrt")); }
			set { SetText(FixId("wrt"), value); }
		}

		public override string Album
		{
			get
			{
				string[] text = GetText(FixId("alb"));
				return text.Length == 0 ? null : text[0];
			}
			set
			{
				SetText(FixId("alb"), value);
			}
		}

		public override string Comment
		{
			get
			{
				IList<Mpeg4AppleDataBox> boxes = DataBoxes(FixId("cmt"));
				return boxes.Count == 0 ? null : boxes[0].Text;
			}
			set
			{
				SetText(FixId("cmt"), value);
			}
		}

		public override IList<string> Genres
		{
			get
			{
				StringCollection collection = new StringCollection();
				ByteVectorCollection names = new ByteVectorCollection();
				names.Add(FixId("gen"));
				names.Add(FixId("gnre"));
				foreach (Mpeg4AppleDataBox box in DataBoxes(names))
				{
					if (box.Text != null)
						collection.Add(box.Text);
					else if (box.Flags == (int)Mpeg4ContentType.ContainsData)
					{
						//string genre = Id3v1.GenreList.GetGenre(box.Data[0]);
						string genre = Id3v1GenreList.Genres[box.Data[0]];
						if (genre != null)
							collection.Add(genre);
					}
				}
				return collection.ToList();
			}
			set
			{
				ClearData(FixId("gnre"));
				SetText(FixId("gen"), value);
			}
		}

		[System.CLSCompliant(false)]
		public override uint Year
		{
			get
			{
				//Mpeg4AppleDataBox[] boxes = 
				IList<Mpeg4AppleDataBox> boxes = DataBoxes(FixId("day"));

				uint year = 0;
				if (boxes.Count > 0 && boxes[0].Text != null)
				{
					string boxData = boxes[0].Text.Substring(0, boxes[0].Text.Length < 4 ? boxes[0].Text.Length : 4);
					if (uint.TryParse(boxData, out year))
						return year;
				}
				return 0;
			}
			set
			{
				SetText(FixId("day"), value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
			}
		}

		[System.CLSCompliant(false)]
		public override uint Track
		{
			get
			{
				//Mpeg4AppleDataBox[] 
				IList<Mpeg4AppleDataBox> boxes = DataBoxes(FixId("trkn"));
				if (boxes.Count != 0 && boxes[0].Flags == (int)Mpeg4ContentType.ContainsData && boxes[0].Data.Count >= 4)
					return (uint)boxes[0].Data.Mid(2, 2).ToShort();
				return 0;
			}
			set
			{
				ByteVector v = new ByteVector();
				v += ByteVector.FromShort(0);
				v += ByteVector.FromShort((short)value);
				v += ByteVector.FromShort((short)TrackCount);
				v += ByteVector.FromShort(0);

				SetData(FixId("trkn"), v, (int)Mpeg4ContentType.ContainsData);
			}
		}

		[System.CLSCompliant(false)]
		public override uint TrackCount
		{
			get
			{
				//Mpeg4AppleDataBox[]
				IList<Mpeg4AppleDataBox> boxes = DataBoxes(FixId("trkn"));
				if (boxes.Count != 0 && boxes[0].Flags == (int)Mpeg4ContentType.ContainsData && boxes[0].Data.Count >= 6)
					return (uint)boxes[0].Data.Mid(4, 2).ToShort();
				return 0;
			}
			set
			{
				ByteVector v = new ByteVector();
				v += ByteVector.FromShort(0);
				v += ByteVector.FromShort((short)Track);
				v += ByteVector.FromShort((short)value);
				v += ByteVector.FromShort(0);

				SetData(FixId("trkn"), v, (int)Mpeg4ContentType.ContainsData);
			}
		}

		[System.CLSCompliant(false)]
		public override uint Disc
		{
			get
			{
				//Mpeg4AppleDataBox[]
				IList<Mpeg4AppleDataBox> boxes = DataBoxes(FixId("disk"));
				if (boxes.Count != 0 && boxes[0].Flags == (int)Mpeg4ContentType.ContainsData && boxes[0].Data.Count >= 4)
					return (uint)boxes[0].Data.Mid(2, 2).ToShort();
				return 0;
			}
			set
			{
				ByteVector v = new ByteVector();
				v += ByteVector.FromShort(0);
				v += ByteVector.FromShort((short)value);
				v += ByteVector.FromShort((short)DiscCount);
				v += ByteVector.FromShort(0);

				SetData(FixId("disk"), v, (int)Mpeg4ContentType.ContainsData);
			}
		}

		[System.CLSCompliant(false)]
		public override uint DiscCount
		{
			get
			{
				//Mpeg4AppleDataBox[]
				IList<Mpeg4AppleDataBox> boxes = DataBoxes(FixId("disk"));
				if (boxes.Count != 0 && boxes[0].Flags == (int)Mpeg4ContentType.ContainsData && boxes[0].Data.Count >= 6)
					return (uint)boxes[0].Data.Mid(4, 2).ToShort();
				return 0;
			}
			set
			{
				ByteVector v = new ByteVector();
				v += ByteVector.FromShort(0);
				v += ByteVector.FromShort((short)Disc);
				v += ByteVector.FromShort((short)value);
				v += ByteVector.FromShort(0);

				SetData(FixId("disk"), v, (int)Mpeg4ContentType.ContainsData);
			}
		}

		public override IList<IPicture> Pictures
		{
			get
			{
				//ArrayList l = new ArrayList();
				IList<IPicture> pictures = new List<IPicture>();

				foreach (Mpeg4AppleDataBox box in DataBoxes(FixId("covr")))
				{
					string type = null;
					string description = null;
					if (box.Flags == (int)Mpeg4ContentType.ContainsJpegData)
					{
						type = "image/jpeg";
						description = "cover.jpg";
					}
					else if (box.Flags == (int)Mpeg4ContentType.ContainsPngData)
					{
						type = "image/png";
						description = "cover.png";
					}
					else continue;

					Picture picture = new Picture();
					picture.Type = PictureType.FrontCover;
					picture.Data = box.Data;
					picture.MimeType = type;
					picture.Description = description;

					pictures.Add(picture);
				}

				return pictures;
				//(Picture[])list.ToArray(typeof(Picture));
			}

			set
			{
				if (value == null || value.Count == 0)
				{
					ClearData("covr");
					return;
				}

				IList<IPicture> pictures = value as IList<IPicture>;
				if (pictures != null)
				{
					Mpeg4AppleDataBox[] boxes = new Mpeg4AppleDataBox[pictures.Count];
					for (int i = 0; i < pictures.Count; i++)
					{
						uint type = (uint)Mpeg4ContentType.ContainsData;

						if (pictures[i].MimeType == "image/jpeg")
							type = (uint)Mpeg4ContentType.ContainsJpegData;
						else if (pictures[i].MimeType == "image/png")
							type = (uint)Mpeg4ContentType.ContainsPngData;

						boxes[i] = new Mpeg4AppleDataBox(pictures[i].Data, type);
					}

					SetData("covr", boxes);
				}
			}
		}
		#endregion
		
		#region Public Methods
		//public Mpeg4AppleDataBox[] DataBoxes(ByteVectorCollection list)
		public IList<Mpeg4AppleDataBox> DataBoxes(ByteVectorCollection list)
		{
			//ArrayList l = new ArrayList();
			List<Mpeg4AppleDataBox> boxes = new List<Mpeg4AppleDataBox>();

			// Check each box to see if the match any of the provided types.
			// If a match is found, loop through the children and add any data box.
			foreach (Mpeg4Box box in listBox.Children)
				foreach (ByteVector vector in list)
					if (FixId(vector) == box.BoxType)
						foreach (Mpeg4Box dataBox in box.Children)
						{
							//if (dataBox.GetType() == typeof(Mpeg4AppleDataBox))
							Mpeg4AppleDataBox appleDataBox = dataBox as Mpeg4AppleDataBox;
							if (appleDataBox != null) boxes.Add(appleDataBox);
						}

			// Return the results as an numbers.
			return boxes;
			//(Mpeg4AppleDataBox[])l.ToArray(typeof(Mpeg4AppleDataBox));
		}

		// Get all the data boxes with a given type.
		public IList<Mpeg4AppleDataBox> DataBoxes(ByteVector vector)
		{
			return DataBoxes(new ByteVectorCollection(FixId(vector)));
		}
		
		//public Mpeg4AppleDataBox[] DataBoxes(ByteVector vector)
		//{
		  //return DataBoxes(new ByteVectorCollection(FixId(vector)));
		//}

		// Find all the data boxes with a given mean and name.
		public Mpeg4AppleDataBox[] DataBoxes(string mean, string name)
		{
		  ArrayList l = new ArrayList();

		  // These children will have a box type of "----"
		  foreach (Mpeg4Box box in listBox.Children)
			  if (box.BoxType == "----")
			  {
				  // Get the mean and name boxes, make sure they're legit, and make
				  // sure that they match what we want. Then loop through and add
				  // all the data box children to our output.
				  Mpeg4AppleAdditionalInfoBox meanBox = (Mpeg4AppleAdditionalInfoBox)box.FindChild("meta");
				  Mpeg4AppleAdditionalInfoBox nameBox = (Mpeg4AppleAdditionalInfoBox)box.FindChild("name");
				  if (meanBox != null && nameBox != null && meanBox.Text == mean && nameBox.Text == name)
					  foreach (Mpeg4Box dataBox in box.Children)
						  if (dataBox.GetType() == typeof(Mpeg4AppleDataBox))
							  l.Add(dataBox);
			  }

		  // Return the results as an numbers.
		  return (Mpeg4AppleDataBox[])l.ToArray(typeof(Mpeg4AppleDataBox));
		}

		// Get a string collection of the data boxes for the given box type
		private StringCollection GetTextCollection(ByteVector type)
		{
			StringCollection collection = new StringCollection();
			foreach (Mpeg4AppleDataBox box in DataBoxes(type))
				if (box.Text != null)
					collection.Add(box.Text);
			return collection;
		}

		// Get an array of the data boxes for the given box type
		public string[] GetText(ByteVector type)
		{
			StringCollection collection = GetTextCollection(type);
			return collection.ToArray();
		}

		// Get a generic list of the data boxes for the given box type
		public IList<string> GetTextList(ByteVector type)
		{
			StringCollection collection = GetTextCollection(type);
			return collection.ToList();
		}		

		// Set the data with the given box type, data, and flags.
		public void SetData(ByteVector type, Mpeg4AppleDataBox[] boxes)
		{
		  // Fix the type.
		  type = FixId(type);

		  bool first = true;

		  // Loop through the children and find all with the type.
		  foreach (Mpeg4Box box in listBox.Children)
			  if (type == box.BoxType)
			  {
				  // If this is our first child...
				  if (first)
				  {
					  // clear its children and add our data boxes.
					  box.ClearChildren();
					  foreach (Mpeg4AppleDataBox b in boxes)
						  box.AddChild(b);
					  first = false;
				  }
				  // Otherwise, it is dead to us.
				  else
					  box.RemoveFromParent();
			  }

		  // If we didn'type find the box..
		  if (first)
		  {
			  // Add the box and try again.
			  Mpeg4Box box = new Mpeg4AppleAnnotationBox(type);
			  listBox.AddChild(box);
			  SetData(type, boxes);
		  }
		}

		[System.CLSCompliant(false)]
		public void SetData(ByteVector type, ByteVectorCollection data, uint flags)
		{
			if (data != null)
			{
				if (data.Count > 0)
				{
					Mpeg4AppleDataBox[] boxes = new Mpeg4AppleDataBox[data.Count];
					for (int i = 0; i < data.Count; i++)
						boxes[i] = new Mpeg4AppleDataBox(data[i], flags);

					SetData(type, boxes);
				}
				else ClearData(type);
			}
			else ClearData(type);
		}

		// Set the data with the given box type, data, and flags.
		[System.CLSCompliant(false)]
		public void SetData(ByteVector type, ByteVector data, uint flags)
		{
		  if (data == null || data.Count == 0)
			  ClearData(type);
		  else
			  SetData(type, new ByteVectorCollection(data), flags);
		}

		public void SetText(ByteVector type, IList<string> text)
		{
			// Remove empty data and return.
			if (text == null)
			{
				listBox.RemoveChildren(FixId(type));
				return;
			}

			// Create a text...
			ByteVectorCollection data = new ByteVectorCollection();

			// and populate it with the ByteVectorized strings.
			foreach (string value in text)
				data.Add(ByteVector.FromString(value, StringType.UTF8));

			// Send our final byte vectors to SetData
			SetData(type, data, (uint)Mpeg4ContentType.ContainsText);	
		}

		// Set the data with the given box type, strings, and flags.
		public void SetText(ByteVector type, string[] text)
		{
		  // Remove empty data and return.
		  if (text == null)
		  {
			  listBox.RemoveChildren(FixId(type));
			  return;
		  }

		  // Create a text...
		  ByteVectorCollection data = new ByteVectorCollection();

		  // and populate it with the ByteVectorized strings.
		  foreach (string value in text)
			  data.Add(ByteVector.FromString(value, StringType.UTF8));

		  // Send our final byte vectors to SetData
		  SetData(type, data, (uint)Mpeg4ContentType.ContainsText);
		}

		// Set the data with the given box type, string, and flags.
		public void SetText(ByteVector type, string text)
		{
		  // Remove empty data and return.
		  if (text == null || text.Length == 0)
		  {
			  listBox.RemoveChildren(FixId(type));
			  return;
		  }

		  SetText(type, new string[] { text });
		}

		// Clear all data associated with a box type.
		public void ClearData(ByteVector type)
		{
		  listBox.RemoveChildren(FixId(type));
		}

		// Save the file.
		public void Save()
		{
		  if (listBox == null)
			  throw new TagLibException(TagLibError.Mpeg4TagSaveFailed); 
			  //InvalidOperationException("Could not save AppleTag: listBox is not defined");

		  // Try to get into write mode.
			try
			{
				file.Mode = FileAccessMode.Write;
			}
			catch (TagLibException)
			{
				return;
			}

		  // Make a file box.
		  Mpeg4FileBox fileBox = new Mpeg4FileBox(file);

		  // Get the MovieBox.
		  Mpeg4IsoMovieBox moovBox = (Mpeg4IsoMovieBox)fileBox.FindChildDeep("moov");

		  // If we have a movie box...
		  if (moovBox != null)
		  {
			  // Set up how much, where, and what to replace, and who to tell
			  // about it.
			  ulong originalSize = 0;
			  long position = -1;
			  ByteVector data = null;
			  Mpeg4Box parent = null;

			  // Get the old ItemList (the one we're replacing.
			  Mpeg4AppleItemListBox old_ilst_box = (Mpeg4AppleItemListBox)moovBox.FindChildDeep("ilst");

			  // If it exists.
			  if (old_ilst_box != null)
			  {
				  // We stick ourself in the meta box and slate to overwrite it.
				  parent = old_ilst_box.Parent;
				  originalSize = parent.BoxSize;
				  position = parent.NextBoxPosition - (long)originalSize;

				  parent.ReplaceChild(old_ilst_box, listBox);
				  data = parent.Render();

				  parent = parent.Parent;
			  }
			  else
			  {
				  // There is not old ItemList. See if we can get a MetaBox.
				  Mpeg4IsoMetaBox metaBox = (Mpeg4IsoMetaBox)moovBox.FindChildDeep("meta");

				  //If we can...
				  if (metaBox != null)
				  {
					  // Stick the child in here and slate to overwrite...
					  metaBox.AddChild(listBox);

					  originalSize = metaBox.BoxSize;
					  position = metaBox.NextBoxPosition - (long)originalSize;
					  data = metaBox.Render();
					  parent = metaBox.Parent;
				  }
				  else
				  {
					  // There'field no MetaBox. Create one and add the ItemList.
					  metaBox = new Mpeg4IsoMetaBox("hdlr", null);
					  metaBox.AddChild(listBox);

					  // See if we can get a UserDataBox.
					  Mpeg4IsoUserDataBox udtaBox = (Mpeg4IsoUserDataBox)moovBox.FindChildDeep("udta");

					  // If we can...
					  if (udtaBox != null)
					  {
						  // We'll stick the MetaBox at the end and overwrite it.
						  originalSize = 0;
						  position = udtaBox.NextBoxPosition;
						  data = metaBox.Render();
						  parent = udtaBox;
					  }
					  else
					  {
						  // If not even the UserDataBox exists, create it and add
						  // our MetaBox.
						  udtaBox = new Mpeg4IsoUserDataBox();
						  udtaBox.AddChild(metaBox);

						  // Since UserDataBox is a child of MovieBox, we'll just
						  // insert it at the end.
						  originalSize = 0;
						  position = moovBox.NextBoxPosition;
						  data = udtaBox.Render();
						  parent = moovBox;
					  }
				  }
			  }

			  // If we have data and somewhere to put it..
			  if (data != null && position >= 0)
			  {
				  // Figure out the size difference.
				  long sizeDifference = (long)data.Count - (long)originalSize;

				  // Insert the new data.
				  file.Insert(data, position, (long)(originalSize));

				  // If there is a size difference, resize all parent headers.
				  if (sizeDifference != 0)
				  {
					  while (parent != null)
					  {
						  parent.OverwriteHeader(sizeDifference);
						  parent = parent.Parent;
					  }

					  // ALSO, VERY IMPORTANTLY, YOU MUST UPDATE EVERY 'stco'.

					  foreach (Mpeg4Box box in moovBox.Children)
						  if (box.BoxType == "trak")
						  {
							  Mpeg4IsoChunkLargeOffsetBox co64Box = (Mpeg4IsoChunkLargeOffsetBox)box.FindChildDeep("co64");
							  if (co64Box != null)
								  co64Box.UpdateOffset(sizeDifference);

							  Mpeg4IsoChunkOffsetBox stcoBox = (Mpeg4IsoChunkOffsetBox)box.FindChildDeep("stco");
							  if (stcoBox != null)
								  stcoBox.UpdateOffset((int)sizeDifference);
						  }
				  }

				  // Be nice and close the stream.
				  file.Mode = FileAccessMode.Closed;
				  return;
			  }
		  }
		  else
			  throw new TagLibException(TagLibError.Mpeg4StreamDoesNotHaveMoovTag);
			  //new ApplicationException("stream does not have MOOV tag");

		  // We're at the end. Close the stream and admit defeat.
		  file.Mode = FileAccessMode.Closed;
		  throw new TagLibException(TagLibError.Mpeg4CouldNotSaveAppleTag);
		  //ApplicationException("Could not complete AppleTag save");
		}
		#endregion
      
		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<Mpeg4Box> GetEnumerator()
		{
			foreach (Mpeg4Box box in listBox.Children)
				yield return box;
		}
		#endregion
	}
}
