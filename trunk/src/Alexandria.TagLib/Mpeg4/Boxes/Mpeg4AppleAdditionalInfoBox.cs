using System;

namespace Alexandria.TagLib
{
	public class Mpeg4AppleAdditionalInfoBox : Mpeg4FullBox
	{
		#region Constructors
		public Mpeg4AppleAdditionalInfoBox(Mpeg4BoxHeader header, Mpeg4Box parent) : base(header, parent)
		{
			// The box content is a type string.
			text = LoadBoxData().ToString(StringType.Latin1);
			//BoxData.ToString(StringType.Latin1);
		}
		#endregion
		
		#region Private Fields
		private string text;
		#endregion
		
		#region Public Properties
		public string Text
		{
			// When we set the value, store it as a the data too.
			get { return text; }
			set { text = value; Data = ByteVector.FromString(text, StringType.Latin1); }
		}
		#endregion
	}
}
