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

namespace Alexandria.TagLib
{
	public class Id3v2CommentsFrame : Id3v2Frame
	{
		#region Constructors
		public Id3v2CommentsFrame(StringType encoding) : base("COMM")
		{
			textEncoding = encoding;
			//language = null;
			//fields = null;
			//type = null;
		}

		public Id3v2CommentsFrame(ByteVector data) : base(data)
		{
			textEncoding = StringType.UTF8;
			//language = null;
			//fields = null;
			//type = null;
			//SetData(data);
			ParseHeader(data);
			ParseCommentsFields(data);
		}

		protected internal Id3v2CommentsFrame(ByteVector data, Id3v2FrameHeader header) : base(header)
		{
			textEncoding = StringType.UTF8;
			//language = null;
			//fields = null;
			//type = null;
			//ParseFields(FieldData(data));
			ParseCommentsFields(FieldData(data));
		}
		#endregion
		
		#region Private Fields
		StringType textEncoding;
		ByteVector language;
		string description;
		string text;
		#endregion

		#region Private Methods
		private void ParseCommentsFields(ByteVector data)
		{
			if (data.Count < 5)
			{
				TagLibDebugger.Debug("A comment frame must contain at least 5 bytes.");
				return;
			}

			textEncoding = (StringType)data[0];
			language = data.Mid(1, 3);

			int byte_align = textEncoding == StringType.Latin1 || textEncoding == StringType.UTF8 ? 1 : 2;

			ByteVectorCollection l = ByteVectorCollection.Split(data.Mid(4), TextDelimiter(textEncoding), byte_align, 2);

			if (l.Count == 2)
			{
				description = l[0].ToString(textEncoding);
				text = l[1].ToString(textEncoding);
			}
		}
		
		private ByteVector RenderCommentsFields()
		{
			ByteVector vector = new ByteVector();

			vector.Add((byte)TextEncoding);
			vector.Add(Language);
			vector.Add(ByteVector.FromString(description, TextEncoding));
			vector.Add(TextDelimiter(TextEncoding));
			vector.Add(ByteVector.FromString(text, TextEncoding));

			return vector;
		}
		#endregion

		#region Protected Methods
		protected override void ParseFields(ByteVector data)
		{
			ParseCommentsFields(data);
		}

		protected override ByteVector RenderFields()
		{
			return RenderCommentsFields();
		}
		#endregion

		#region Public Properties
		public StringType TextEncoding
		{
			get {return textEncoding;}
			set {textEncoding = value;}
		}

		public ByteVector Language
		{
			get {return language != null ? language : "XXX";}
			set {language = value != null ? value.Mid(0, 3) : "XXX";}
		}

		public string Description
		{
			get {return description;}
			set {description = value;}
		}

		public string Text
		{
			get {return text;}
			set {text = value;}
		}
		#endregion
		
		#region Public Methods
		public override string ToString()
		{
			return text;
		}

		public override void SetText(string text)
		{
			this.text = text;
		}
		#endregion
		
		#region Public Static Methods
		public static Id3v2CommentsFrame Find(Id3v2Tag tag, string description)
		{
			if (tag != null)
			{
				foreach (Id3v2CommentsFrame frame in tag.GetFrames("COMM"))
					if (frame != null && frame.Description == description)
						return frame;
				return null;
			}
			else throw new ArgumentNullException("tag");
		}
		#endregion
	}
}
