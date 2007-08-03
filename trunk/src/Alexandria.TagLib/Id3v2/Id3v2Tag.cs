/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : id3v2tag.cpp from TagLib
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
using Alexandria.Media;

namespace Alexandria.TagLib
{
	public class Id3v2Tag : Tag
	{
		#region Constructors
		public Id3v2Tag() : base()
		{
			tagOffset = -1;
			header = new Id3v2Header();
			//extendedHeader = null;
			//footer = null;
			frameList = new ArrayList();
		}

		public Id3v2Tag(File file, long tagOffset) : this()
		{
			this.tagOffset = tagOffset;

			Read(file);
		}
		#endregion
	
		#region Private Fields
		private long tagOffset;
		private Id3v2Header header;
		private Id3v2ExtendedHeader extendedHeader;
		private Id3v2Footer footer;
		private ArrayList frameList;
		#endregion
		
		#region Private Static Fields
		private static ByteVector language = "eng";
		#endregion
		
		#region Protected Methods
		protected void Read(TagLib.File file)
		{
			if (file == null)
				return;

			try
			{
				file.Mode = FileAccessMode.Read;
			}
			catch (TagLibException)
			{
				return;
			}

			file.Seek(tagOffset);
			header.SetData(file.ReadBlock((int)Id3v2Header.Size));

			// if the tag size is 0, then this is an invalid tag (tags must contain
			// at least one frame)

			if (header.TagSize == 0)
				return;

			Parse(file.ReadBlock((int)header.TagSize));
		}

		protected void Parse(ByteVector data)
		{
			if (data != null)
			{
				try
				{
					int frameDataPosition = 0;
					int frameDataLength = data.Count;

					// check for extended header

					if (header.ExtendedHeader)
					{
						if (ExtendedHeader == null)
							extendedHeader = new Id3v2ExtendedHeader();

						ExtendedHeader.SetData(data);

						if (ExtendedHeader.Size <= data.Count)
						{
							frameDataPosition += (int)ExtendedHeader.Size;
							frameDataLength -= (int)ExtendedHeader.Size;
						}
					}

					// check for footer -- we don'type actually need to parse it, as it *must*
					// contain the same data as the header, but we do need to account for its
					// size.

					if (header.FooterPresent && Id3v2Footer.Size <= frameDataLength)
						frameDataLength -= (int)Id3v2Footer.Size;

					// parse frames

					// Make sure that there is at least enough room in the remaining frame data for
					// a frame header.

					while (frameDataPosition < frameDataLength - Id3v2FrameHeader.Size(header.MajorVersion))
					{

						// If the next data is position is 0, assume that we've hit the padding
						// portion of the frame data.
						if (data[frameDataPosition] == 0)
						{
							if (header.FooterPresent)
								TagLibDebugger.Debug("Padding *and* a footer found.  This is not allowed by the spec.");

							return;
						}

						Id3v2Frame frame = Id3v2FrameFactory.CreateFrame(data.Mid(frameDataPosition), header.MajorVersion);

						if (frame == null)
							return;

						// Checks to make sure that frame parsed correctly.
						if (frame.Size < 0)
							return;

						frameDataPosition += (int)(frame.Size + Id3v2FrameHeader.Size(header.MajorVersion));
						// Only add frames with content so we don'type send out just we got in.
						if (frame.Size > 0)
							AddFrame(frame);
					}
				}
				catch (Exception ex)
				{
					throw new ApplicationException("There was an error parsing this ID3 tag", ex);
				}
			}
			else throw new ArgumentNullException("data");
		}
		#endregion
		
		#region Public Properties
		public override string Title
		{
			get
			{
				foreach (Id3v2TextIdentificationFrame f in GetFrames("TIT2"))
					return f.ToString();
				return null;
			}
			set
			{
				SetTextFrame("TIT2", value);
			}
		}

		public override IList<string> Artists
		{
			get
			{
				foreach (Id3v2TextIdentificationFrame frame in GetFrames("TPE1"))
					return frame.FieldList.ToList();
				return new List<string>();
			}
			set
			{
				SetTextFrame("TPE1", new StringCollection(value));
			}
		}

		public override IList<string> Performers
		{
			get
			{
				foreach (Id3v2TextIdentificationFrame frame in GetFrames("TPE2"))
					return frame.FieldList.ToList();
				return new List<string>();
			}
			set
			{
				SetTextFrame("TPE2", new StringCollection(value));
			}
		}

		public override IList<string> Composers
		{
			get
			{
				foreach (Id3v2TextIdentificationFrame frame in GetFrames("TCOM"))
					return frame.FieldList.ToList();
				return new List<string>();
			}
			set
			{
				SetTextFrame("TCOM", new StringCollection(value));
			}
		}

		public override string Album
		{
			get
			{
				foreach (Id3v2TextIdentificationFrame f in GetFrames("TALB"))
					return f.ToString();
				return null;
			}
			set
			{
				SetTextFrame("TALB", value);
			}
		}

		public override string Comment
		{
			get
			{
				// This is weird, so bear with me. The best thing we can have is 
				// something straightforward and in our own language. If it has a 
				// fields, then it is probably used for something other than
				// an actual comment. If that doesn'type work, we'd still rather have 
				// something in our language than something in another. After that
				// all we have left are things in other languages, so we'd rather 
				// have one with actual content, so we try to get one with no 
				// fields first.
				Id3v2Frame[] frames = GetFrames("COMM");

				foreach (Id3v2CommentsFrame frame in frames)
					if (frame.Description.Length == 0 && frame.Language == Language)
						return frame.ToString();

				foreach (Id3v2CommentsFrame frame in frames)
					if (frame.Language == Language)
						return frame.ToString();

				foreach (Id3v2CommentsFrame frame in frames)
					if (frame.Description.Length == 0)
						return frame.ToString();

				foreach (Id3v2CommentsFrame frame in frames)
					return frame.ToString();

				return null;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					RemoveFrames("COMM");
					return;
				}

				// See above.
				Id3v2Frame[] frames = GetFrames("COMM");

				foreach (Id3v2CommentsFrame frame in frames)
					if (frame.Description.Length == 0 && frame.Language == Language)
					{
						frame.SetText(value);
						return;
					}
				
				foreach (Id3v2CommentsFrame frame in frames)
					if (frame.Language == Language)
					{
						frame.SetText(value);
						return;
					}

				foreach (Id3v2CommentsFrame frame in frames)
					if (frame.Description.Length == 0)
					{
						frame.SetText(value);
						return;
					}

				foreach (Id3v2CommentsFrame frame in frames)
				{
					frame.SetText(value);
					return;
				}

				// There were absolutely no comment frames. Let'field add one in our
				// language.
				Id3v2CommentsFrame addFrame = new Id3v2CommentsFrame(Id3v2FrameFactory.DefaultTextEncoding);
				addFrame.Language = Language;
				addFrame.SetText(value);
				AddFrame(addFrame);
			}
		}

		public override IList<string> Genres
		{
			get
			{
				//ArrayList l = new ArrayList();
				List<string> list = new List<string>();

				Id3v2Frame[] frames = GetFrames("TCON");
				Id3v2TextIdentificationFrame frame;
				if (frames.Length != 0 && (frame = (Id3v2TextIdentificationFrame)frames[0]) != null)
				{
					StringCollection fields = frame.FieldList;

					foreach (string field in fields)
					{
						if (field == null)
							continue;

						bool isNumber = true;
						foreach (char c in field)
							if (c < '0' || c > '9')
								isNumber = false;

						if (isNumber)
						{
							byte genre;
							if (byte.TryParse(field, out genre))
							{
								list.Add(Id3v1GenreList.GetGenre(genre));
								continue;
							}
						}

						int closing = field.IndexOf(')');
						if (closing > 0 && field.Substring(0, 1) == "(")
						{
							if (closing == field.Length - 1)
							{
								byte genre;
								if (byte.TryParse(field.Substring(1, closing - 1), out genre))
								{
									if (genre != (byte)255)
										list.Add(Id3v1GenreList.GetGenre(genre));
								}
								else
									list.Add(field);
							}
							else
								list.Add(field.Substring(closing + 1));
						}
						else
							list.Add(field);
					}
				}

				return list;
			}
			set
			{
				for (int i = 0; i < value.Count; i++)
				{
					int index = Id3v1GenreList.GetGenreIndex(value[i]);
					if (index != 255)
						value[i] = index.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
				}

				SetTextFrame("TCON", new StringCollection(value));
			}
		}

		[System.CLSCompliant(false)]
		public override uint Year
		{
			get
			{
				uint year = 0;

				foreach (Id3v2TextIdentificationFrame frame in GetFrames("TDRC"))
				{
					if (uint.TryParse(frame.ToString().Substring(0, 4), out year))
						return year;
				}
				return 0;
			}
			set
			{
				SetTextFrame("TDRC", value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
			}
		}

		[System.CLSCompliant(false)]
		public override uint Track
		{
			get
			{
				Id3v2Frame[] trackFrames = GetFrames("TRCK");
				if (trackFrames != null && trackFrames.Length > 0)
				{
					Id3v2Frame trackFrame = trackFrames[0];
					if (trackFrame != null)
					{
						string[] trackData = trackFrame.ToString().Split('/');
						if (trackData != null && trackData.Length > 0)
						{
							uint track = 0;
							if (uint.TryParse(trackData[0], out track))
								return track;
						}
					}
				}
				return 0;
			}
			set
			{
				uint count = TrackCount;
				if (count != 0)
					SetTextFrame("TRCK", value + "/" + count);
				else
					SetTextFrame("TRCK", value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
			}
		}

		[System.CLSCompliant(false)]
		public override uint TrackCount
		{
			get
			{
				Id3v2Frame[] trackFrames = GetFrames("TRCK");
				if (trackFrames != null && trackFrames.Length > 0)
				{
					Id3v2Frame trackFrame = trackFrames[0];
					if (trackFrame != null)
					{
						string[] trackData = trackFrame.ToString().Split('/');
						if (trackData != null && trackData.Length > 1)
						{
							uint track = 0;
							if (uint.TryParse(trackData[1], out track))
								return track;
						}
					}
				}
				return 0;
			}
			set
			{
				SetTextFrame("TRCK", Track + "/" + value);
			}
		}

		[System.CLSCompliant(false)]
		public override uint Disc
		{
			get
			{
				Id3v2Frame[] discFrames = GetFrames("TPOS");
				if (discFrames != null && discFrames.Length > 0)
				{
					Id3v2Frame discFrame = discFrames[0];
					if (discFrame != null)
					{
						string[] discData = discFrame.ToString().Split('/');
						if (discData != null && discData.Length > 0)
						{
							uint disc = 0;
							if (uint.TryParse(discData[0], out disc))
								return disc;
						}
					}
				}
				return 0;
			}
			set
			{
				uint count = DiscCount;
				if (count != 0)
					SetTextFrame("TPOS", value + "/" + count);
				else
					SetTextFrame("TPOS", value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
			}
		}

		[System.CLSCompliant(false)]
		public override uint DiscCount
		{
			get
			{
				Id3v2Frame[] discFrames = GetFrames("TPOS");
				if (discFrames != null && discFrames.Length > 0)
				{
					Id3v2Frame discFrame = discFrames[0];
					if (discFrame != null)
					{
						string[] discData = discFrame.ToString().Split('/');
						if (discData != null && discData.Length > 1)
						{
							uint disc = 0;
							if (uint.TryParse(discData[1], out disc))
								return disc;
						}
					}
				}
				return 0;
			}
			set
			{
				SetTextFrame("TPOS", Disc + "/" + value);
			}
		}

		public override IList<IImageContainer> Pictures
		{
			get
			{
				Id3v2Frame[] rawFrames = GetFrames("APIC");
				if (rawFrames == null || rawFrames.Length == 0)
				{
					return base.Pictures;
				}

				IList<IImageContainer> frames = new List<IImageContainer>(rawFrames.Length);
				//[rawFrames.Length];
				for (int i = 0; i < rawFrames.Length; i++)
				{
					Id3v2AttachedPictureFrame attachedPicture = rawFrames[i] as Id3v2AttachedPictureFrame;
					if (attachedPicture != null)
						frames.Add(attachedPicture);
				}

				return frames;
			}

			set
			{
				if (value == null || value.Count < 1)
					return;

				RemoveFrames("APIC");

				foreach (IPicture picture in value)
					AddFrame(new Id3v2AttachedPictureFrame(picture));
			}
		}

		public override bool IsEmpty
		{
			get { return frameList.Count == 0; }
		}

		public Id3v2Header Header
		{
			get { return header; }
		}
		
		public Id3v2ExtendedHeader ExtendedHeader
		{
			get { return extendedHeader; }
		}
		
		public Id3v2Footer Footer
		{
			get { return footer; }
		}
		#endregion
		
		#region Public Static Properties
		public static ByteVector Language
		{
			get { return language; }
			set { language = (value == null || value.Count < 3) ? "XXX" : value.Mid(0, 3); }
		}
		#endregion
		
		#region Public Methods
		public Id3v2Frame[] GetFrames()
		{
			return (Id3v2Frame[])frameList.ToArray(typeof(Id3v2Frame));
		}

		public Id3v2Frame[] GetFrames(ByteVector id)
		{
			ArrayList l = new ArrayList();
			foreach (Id3v2Frame f in frameList)
				if (f.FrameId == id)
					l.Add(f);

			return (Id3v2Frame[])l.ToArray(typeof(Id3v2Frame));
		}

		public void AddFrame(Id3v2Frame frame)
		{
			frameList.Add(frame);
		}

		public void RemoveFrame(Id3v2Frame frame)
		{
			if (frameList.Contains(frame))
				frameList.Remove(frame);
		}

		public void RemoveFrames(ByteVector id)
		{
			for (int i = frameList.Count - 1; i >= 0; i--)
			{
				Id3v2Frame f = (Id3v2Frame)frameList[i];
				if (f.FrameId == id)
					RemoveFrame(f);
			}
		}

		public ByteVector Render()
		{
			// We need to render the "tag data" first so that we have to correct size to
			// render in the tag'field header.  The "tag data" -- everything that is included
			// in ID3v2::Header::tagSize() -- includes the extended header, frames and
			// padding, but does not include the tag'field header or footer.

			ByteVector tagData = new ByteVector();

			// TODO: Render the extended header.
			header.ExtendedHeader = false;

			// Loop through the frames rendering them and adding them to the tagData.

			foreach (Id3v2Frame frame in frameList)
				if (!frame.Header.TagAlterPreservation)
					tagData.Add(frame.Render());

			// Compute the amount of padding, and append that to tagData.

			uint padding_size = 0;
			uint original_size = header.TagSize;

			if (!header.FooterPresent)
			{
				if (tagData.Count < original_size)
					padding_size = (uint)(original_size - tagData.Count);
				else
					padding_size = 1024;

				tagData.Add(new ByteVector((int)padding_size));
			}

			// Set the tag size.
			header.TagSize = (uint)tagData.Count;

			if (header.FooterPresent)
			{
				footer = new Id3v2Footer(header);
				return header.Render() + tagData + footer.Render();
			}

			return header.Render() + tagData;
		}

		public void SetTextFrame(ByteVector id, string value)
		{
			if (value == null || value.Length == 0)
			{
				RemoveFrames(id);
				return;
			}

			SetTextFrame(id, new StringCollection(value));
		}

		public void SetTextFrame(ByteVector id, StringCollection value)
		{
			if (value == null || value.Count == 0)
			{
				RemoveFrames(id);
				return;
			}

			Id3v2Frame[] frames = GetFrames(id);
			if (frames.Length != 0)
			{
				bool first = true;
				foreach (Id3v2TextIdentificationFrame frame in frames)
				{
					// There should only be one of each type frame, per the specification.
					if (first)
						frame.SetText(value);
					else
						RemoveFrame(frame);

					first = false;
				}
			}
			else
			{
				Id3v2TextIdentificationFrame f = new Id3v2TextIdentificationFrame(id, Id3v2FrameFactory.DefaultTextEncoding);
				AddFrame(f);
				f.SetText(value);
			}
		}
		#endregion
	}
}
