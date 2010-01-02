using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.TagLib
{
	public class Id3v2UserTextIdentificationFrame : Id3v2TextIdentificationFrame
	{
		#region Constructors
		public Id3v2UserTextIdentificationFrame(StringType encoding) : base("TXXX", encoding)
		{
			StringCollection text = new StringCollection();
			text.Add((string)null);
			text.Add((string)null);

			base.SetText(text);
		}

		public Id3v2UserTextIdentificationFrame(ByteVector data) : base(data)
		{
		}

		protected internal Id3v2UserTextIdentificationFrame(ByteVector data, Id3v2FrameHeader header) : base(data, header)
		{
		}
		#endregion
		
		#region Public Properties
		public string Description
		{
			get { return !base.FieldList.IsEmpty ? base.FieldList[0] : null; }
			set
			{
				StringCollection fields = new StringCollection(base.FieldList);

				if (fields.IsEmpty)
					fields.Add(value);
				else
					fields[0] = value;

				base.SetText(fields);
			}
		}

		public new StringCollection FieldList
		{
			get
			{
				StringCollection fields = new StringCollection(base.FieldList);
				if (!fields.IsEmpty)
					fields.RemoveAt(0);
				return fields;
			}
		}
		#endregion
	
		#region Public Methods
		public override string ToString()
		{
			return "[" + Description + "] " + FieldList.ToString();
		}

		public override void SetText(string text)
		{
			StringCollection fields = new StringCollection(Description);
			fields.Add(text);

			base.SetText(fields);
		}

		public new void SetText(StringCollection fields)
		{
			StringCollection l = new StringCollection(Description);
			l.Add(fields);

			base.SetText(l);
		}
		#endregion
		
		#region Public Static Methods
		public static Id3v2UserTextIdentificationFrame Find(Id3v2Tag tag, string description)
		{
			if (tag != null)
			{
				foreach (Id3v2UserTextIdentificationFrame frame in tag.GetFrames("TXXX"))
					if (frame != null && frame.Description == description)
						return frame;
				return null;
			}
			else throw new ArgumentNullException("tag");
		}
		#endregion
	}
}
