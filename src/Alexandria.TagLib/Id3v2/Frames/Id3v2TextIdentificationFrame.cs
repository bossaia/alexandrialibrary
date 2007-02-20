/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : textidentificationframe.cpp from TagLib
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

namespace AlexandriaOrg.Alexandria.TagLib
{
	public class Id3v2TextIdentificationFrame : Id3v2Frame
	{
		#region Constructors
		public Id3v2TextIdentificationFrame(ByteVector type, StringType encoding) : base(type)
		{
			//fieldList = new StringCollection();
			textEncoding = encoding;
		}

		public Id3v2TextIdentificationFrame(ByteVector data) : base(data)
		{
			//fieldList = new StringCollection();
			textEncoding = StringType.UTF8;
			//SetData(data);
			ParseHeader(data);
			ParseTextIdentifierFields(data);
		}

		protected internal Id3v2TextIdentificationFrame(ByteVector data, Id3v2FrameHeader header) : base(header)
		{
			//fieldList = new StringCollection();
			textEncoding = StringType.UTF8;

			//ParseFields(FieldData(data));
			ParseTextIdentifierFields(FieldData(data));
		}
		#endregion
		
		#region Private Fields
		private StringType textEncoding;
		private StringCollection fieldList = new StringCollection();
		#endregion
		
		#region Private Methods
		private void ParseTextIdentifierFields(ByteVector data)
		{
			// read the string data type (the first byte of the field data)

			textEncoding = (StringType)data[0];

			// split the byte numbers into chunks based on the string type (two byte delimiter
			// for unicode encodings)

			int byteAlign = textEncoding == StringType.Latin1 || textEncoding == StringType.UTF8 ? 1 : 2;

			ByteVectorCollection list = ByteVectorCollection.Split(data.Mid(1), TextDelimiter(textEncoding), byteAlign);

			fieldList.Clear();

			// append those split values to the text and make sure that the new string'field
			// type is the same specified for this frame

			foreach (ByteVector vector in list)
				fieldList.Add(vector.ToString(textEncoding));
		}
		
		private ByteVector RenderTextIdentifierFields()
		{
			ByteVector vector = new ByteVector();

			if (fieldList.Count > 0)
			{
				vector.Add((byte)textEncoding);

				bool first = true;
				foreach (string field in fieldList)
				{
					// Since the field text is null delimited, if this is not the
					// first element in the text, append the appropriate delimiter
					// for this encoding.

					if (!first)
						vector.Add(TextDelimiter(textEncoding));
					first = false;

					vector.Add(ByteVector.FromString(field, textEncoding));
				}
			}

			return vector;
		}
		#endregion
		
		#region Protected Methods
		protected override void ParseFields(ByteVector data)
		{
			ParseTextIdentifierFields(data);
		}

		protected override ByteVector RenderFields()
		{
			return RenderTextIdentifierFields();
		}
		#endregion
		
		#region Public Properties
		public StringCollection FieldList
		{
			get { return new StringCollection(fieldList); }
		}

		public StringType TextEncoding
		{
			get { return textEncoding; }
			set { textEncoding = value; }
		}
		#endregion
		
		#region Public Methods
		public void SetText(StringCollection text)
		{
			fieldList.Clear();
			fieldList.Add(text);
		}

		public override void SetText(string text)
		{
			fieldList.Clear();
			fieldList.Add(text);
		}

		public override string ToString()
		{
			return fieldList.ToString();
		}
		#endregion
	}
}
