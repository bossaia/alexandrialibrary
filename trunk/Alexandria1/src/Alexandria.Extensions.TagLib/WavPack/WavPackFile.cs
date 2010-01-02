/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : wvfile.cpp from libtunepimp
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

namespace Alexandria.TagLib
{
	[SupportedMimeTypeAttribute("taglib/wv")]
	[SupportedMimeTypeAttribute("audio/x-wavpack")]
	public class WavPackFile : File
	{
		#region Constructors
		public WavPackFile(string file, ReadStyle propertiesStyle) : base(file)
		{
			//apeTag        = null;
			//id3v1Tag      = null;
			tag = new CombinedTag();
			//properties     = null;

			try
			{
				Mode = FileAccessMode.Read;
			}
			catch (TagLibException)
			{
				return;
			}

			Read(propertiesStyle);

			Mode = FileAccessMode.Closed;
		}

		public WavPackFile(string file) : this(file, ReadStyle.Average)
		{
		}
		#endregion
		
		#region Private Fields
		private ApeTag apeTag;
		private Id3v1Tag id3v1Tag;
		private CombinedTag tag;
		private WavPackProperties properties;
		#endregion
	
		#region Private Methods
		private Tag FindWavPackTag(TagTypes type, bool create)
		{
			switch (type)
			{
				case TagTypes.Id3v1:
					{
						if (create && id3v1Tag == null)
						{
							id3v1Tag = new Id3v1Tag();

							if (tag != null)
								TagLib.Tag.Duplicate(tag, id3v1Tag, true);

							tag.SetTags(apeTag, id3v1Tag);
						}
						return id3v1Tag;
					}

				case TagTypes.Ape:
					{
						if (create && apeTag == null)
						{
							apeTag = new ApeTag();

							if (tag != null)
								TagLib.Tag.Duplicate(tag, apeTag, true);

							tag.SetTags(apeTag, id3v1Tag);
						}
						return apeTag;
					}

				default:
					return null;
			}
		}
		
		private void Read(ReadStyle propertiesStyle)
		{
			// Look for an ID3v1 tag

			long id3v1Location = FindId3v1();

			if (id3v1Location >= 0)
				id3v1Tag = new Id3v1Tag(this, id3v1Location);

			// Look for an APE tag

			long apeLocation = FindApe(id3v1Location != -1);

			if (apeLocation >= 0)
				apeTag = new ApeTag(this, apeLocation);

			tag.SetTags(apeTag, id3v1Tag);
			FindWavPackTag(TagTypes.Ape, true);

			// Look for MPC metadata

			if (propertiesStyle != ReadStyle.None)
			{
				Seek(0);
				properties = new WavPackProperties(ReadBlock((int)WavPackProperties.HeaderSize),
				   apeLocation + ApeFooter.Size - apeTag.Footer.CompleteTagSize);
			}
		}

		private long FindApe(bool hasId3v1)
		{
			if (!IsValid)
				return -1;

			if (hasId3v1)
				Seek(-160, System.IO.SeekOrigin.End);
			else
				Seek(-32, System.IO.SeekOrigin.End);

			long p = Tell;

			if (ReadBlock(8) == ApeTag.FileIdentifier)
				return p;

			return -1;
		}

		private long FindId3v1()
		{
			if (!IsValid)
				return -1;

			Seek(-128, System.IO.SeekOrigin.End);
			long p = Tell;

			if (ReadBlock(3) == Id3v1Tag.FileIdentifier)
				return p;

			return -1;
		}
		#endregion
	
		#region Public Properties
		public override Tag Tag
		{
			get {return tag;}
		}

		public override AudioProperties AudioProperties
		{
			get {return properties;}
		}
		#endregion
		
		#region Public Methods
		public override void Save()
		{
			if (IsReadOnly)
			{
				throw new ReadOnlyException();
			}

			Mode = FileAccessMode.Write;

			// Update ID3v1 tag
			long id3v1_location = FindId3v1();

			if (id3v1Tag != null)
			{
				if (id3v1_location >= 0)
					Insert(id3v1Tag.Render(), id3v1_location, 128);
				else
				{
					Seek(0, System.IO.SeekOrigin.End);
					id3v1_location = Tell;
					WriteBlock(id3v1Tag.Render());
				}
			}
			else if (id3v1_location >= 0)
			{
				RemoveBlock(id3v1_location, 128);
				id3v1_location = -1;
			}


			// Update APE tag
			long ape_location = FindApe(id3v1_location != -1);
			long ape_size = 0;

			if (ape_location != -1)
			{
				Seek(ape_location);
				ape_size = (new ApeFooter(ReadBlock((int)ApeFooter.Size))).CompleteTagSize;
				ape_location = ape_location + ApeFooter.Size - ape_size;
			}

			if (apeTag != null)
			{
				if (ape_location >= 0)
					Insert(apeTag.Render(), ape_location, ape_size);
				else
				{
					if (id3v1_location >= 0)
						Insert(apeTag.Render(), id3v1_location, 0);
					else
					{
						Seek(0, System.IO.SeekOrigin.End);
						WriteBlock(apeTag.Render());
					}
				}
			}
			else if (ape_location >= 0)
				RemoveBlock(ape_location, ape_size);

			Mode = FileAccessMode.Closed;
		}

		public override Tag FindTag(TagTypes type, bool create)
		{
			return FindWavPackTag(type, create);
		}

		public void Remove(TagTypes types)
		{
			if ((types & TagTypes.Id3v1) != 0)
				id3v1Tag = null;

			if ((types & TagTypes.Ape) != 0)
				apeTag = null;

			tag.SetTags(apeTag, id3v1Tag);
		}

		public void Remove()
		{
			Remove(TagTypes.AllTags);
		}
		#endregion
	}
}
