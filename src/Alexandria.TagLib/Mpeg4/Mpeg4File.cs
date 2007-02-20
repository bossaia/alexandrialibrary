using System;

namespace AlexandriaOrg.Alexandria.TagLib
{
	[SupportedMimeType("taglib/m4a")]
	[SupportedMimeType("taglib/m4p")]
	[SupportedMimeType("taglib/mp4")]
	[SupportedMimeType("audio/mp4")]
	[SupportedMimeType("audio/x-m4a")]
	public class Mpeg4File : File
	{
		#region Constructors
		public Mpeg4File(string file, ReadStyle propertiesStyle) : base(file)
		{
			// Nullify for safety.
			//tag = null;
			//properties = null;

			// Try to open read support.
			try
			{
				Mode = FileAccessMode.Read;
			}
			catch (TagLibException)
			{
				return;
			}

			// Read
			Read(propertiesStyle);

			// Be nice and close.
			Mode = FileAccessMode.Closed;
		}

		// Assume average speed.
		public Mpeg4File(string file) : this(file, ReadStyle.Average)
		{
		}
		#endregion
		
		#region Private Fields
		private Mpeg4AppleTag tag;
		private Mpeg4Properties properties;
		#endregion
		
		#region Private Methods
		/*
		private void ShowAllTags (Box box, string spa)
		{
			if (box.GetType () == typeof (AppleDataBox))
				System.Console.WriteLine (spa + box.BoxType.Mid (box.BoxType [0] == 0xa9 ? 1 : 0).ToString () + " - " + box.ToString () + " - " + ((AppleDataBox)box).Text);
			else if (box.GetType () == typeof (AppleAdditionalInfoBox))
				System.Console.WriteLine (spa + box.BoxType.Mid (box.BoxType [0] == 0xa9 ? 1 : 0).ToString () + " - " + box.ToString () + " - " + ((AppleAdditionalInfoBox)box).Text);
			else
				System.Console.WriteLine (spa + box.BoxType.Mid (box.BoxType [0] == 0xa9 ? 1 : 0).ToString () + " - " + box.ToString ());
		 
			foreach (Box child in box.Children)
			{
				ShowAllTags(child, spa + " ");
			}
		}
		*/
		#endregion
		
		#region Public Properties
		public override TagLib.Tag Tag
		{
			get {return FindTag(TagTypes.Apple, true);}
		}

		// Read the properties.
		public override TagLib.AudioProperties AudioProperties
		{
			get {return properties;}
		}
		#endregion
		
		#region Public Methods
		public override void Save()
		{
			tag.Save();
		}

		// Get the Apple Tag.
		public override TagLib.Tag FindTag(TagTypes type, bool create)
		{
			if (type == TagTypes.Apple)
			{
				if (tag == null && create)
					tag = new Mpeg4AppleTag(this);

				return tag;
			}

			return null;
		}

		// Read the file.
		private void Read(ReadStyle propertiesStyle)
		{
			// Create a dummie outer box, as perscribed by the specs.
			Mpeg4FileBox file_box = new Mpeg4FileBox(this);

			// Find the movie box and item text. If the movie box doen'type exist, an
			// exception will be thrown on the next call, but if there is no movie 
			// box, the file can'type possibly be valid.
			Mpeg4IsoMovieBox moov_box = (Mpeg4IsoMovieBox)file_box.FindChildDeep("moov");
			Mpeg4AppleItemListBox ilst_box = (Mpeg4AppleItemListBox)moov_box.FindChildDeep("ilst");

			// If we have a ItemListBox, deparent it.
			if (ilst_box != null)
				ilst_box.RemoveFromParent();

			// Create the tag.
			tag = new Mpeg4AppleTag(ilst_box, this);

			// If we're not reading properties, we're done.
			if (propertiesStyle == ReadStyle.None)
				return;

			// Get the movie header box.
			Mpeg4IsoMovieHeaderBox mvhd_box = (Mpeg4IsoMovieHeaderBox)moov_box.FindChildDeep("mvhd");
			Mpeg4IsoAudioSampleEntry sample_entry = null;

			// Find a TrackBox with a sound Handler.
			foreach (Mpeg4Box box in moov_box.Children)
				if (box.BoxType == "trak")
				{
					// If the handler isn'type sound, it could be metadata or video or
					// any number of other things.
					Mpeg4IsoHandlerBox hdlr_box = (Mpeg4IsoHandlerBox)box.FindChildDeep("hdlr");
					if (hdlr_box == null || hdlr_box.HandlerType != "soun")
						continue;

					// This track SHOULD contain at least one sample entry.
					sample_entry = (Mpeg4IsoAudioSampleEntry)box.FindChildDeep(typeof(Mpeg4IsoAudioSampleEntry));
					break;
				}

			// If we have a MovieHeaderBox, deparent it.
			if (mvhd_box != null)
				mvhd_box.RemoveFromParent();

			// If we have a SampleEntry, deparent it.
			if (sample_entry != null)
				sample_entry.RemoveFromParent();

			// Read the properties.
			properties = new Mpeg4Properties(mvhd_box, sample_entry, propertiesStyle);
		}
		#endregion
	}
}