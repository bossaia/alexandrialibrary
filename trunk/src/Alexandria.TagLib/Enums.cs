using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.TagLib
{
	#region ApeItemType
	public enum ApeItemType
	{
		Text = 0,   // Item contains type information coded in UTF-8
		Binary = 1, // Item contains binary information
		Locator = 2 // Item is a locator of external stored information
	}
	#endregion

	#region AsfDataType
	public enum AsfDataType
	{
		Unicode = 0,
		Bytes = 1,
		Bool = 2,
		DWord = 3,
		QWord = 4,
		Word = 5
	}
	#endregion

	#region ContainsPacketSettings
	[Flags]
	public enum ContainsPacketSettings
	{
		None = 0,  //DoesNotContainPacket = 0, //0x0000, // No part of the packet is contained in the page
		CompletePacket = 0x0001, // The packet is wholly contained in the page
		BeginsWithPacket = 0x0002, // The page starts with the given packet
		EndsWithPacket = 0x0004  // The page ends with the given packet
	}
	#endregion

	#region FileAccessMode
	public enum FileAccessMode
	{
		Read = 0,
		Write,
		Closed
	}
	#endregion

	#region Id3v2ChannelType
	public enum Id3v2ChannelType
	{
		//Other = 0x00,
		None = 0,
		MasterVolume = 0x01,
		FrontRight = 0x02,
		FrontLeft = 0x03,
		BackRight = 0x04,
		BackLeft = 0x05,
		FrontCenter = 0x06,
		BackCenter = 0x07,
		Subwoofer = 0x08
	}
	#endregion

	#region Mpeg4ContentType
	public enum Mpeg4ContentType
	{
		ContainsText = 0x01,
		ContainsData = 0x00,
		ForTempo = 0x15,
		ContainsJpegData = 0x0D,
		ContainsPngData = 0x0E
	}
	#endregion

	#region MpegVersion
	public enum MpegVersion
	{
		One = 0, // MPEG Version 1
		Two = 1, // MPEG Version 2
		TwoPointFive = 2  // MPEG Version 2.5
	}
	#endregion

	#region MpegChannelMode
	public enum MpegChannelMode
	{
		Stereo = 0, // Stereo
		JointStereo = 1, // Stereo
		DualChannel = 2, // Dual Mono
		SingleChannel = 3  // Mono
	}
	#endregion
	
	#region PaginationStrategy
	public enum PaginationStrategy
	{
		SinglePagePerGroup = 0,
		Repaginate
	}
	#endregion

	#region PictureType
	public enum PictureType
	{
		None = 0, // A type not enumerated below
		FileIcon = 0x01, // 32x32 PNG image that should be used as the file icon
		OtherFileIcon = 0x02, // File icon of a different size or format
		FrontCover = 0x03, // Front cover image of the album
		BackCover = 0x04, // Back cover image of the album
		LeafletPage = 0x05, // Inside leaflet page of the album
		Media = 0x06, // Image from the album itself
		LeadArtist = 0x07, // Picture of the lead artist or soloist
		Artist = 0x08, // Picture of the artist or performer
		Conductor = 0x09, // Picture of the conductor
		Band = 0x0A, // Picture of the band or orchestra
		Composer = 0x0B, // Picture of the composer
		Lyricist = 0x0C, // Picture of the lyricist or type writer
		RecordingLocation = 0x0D, // Picture of the recording location or studio
		DuringRecording = 0x0E, // Picture of the artists during recording
		DuringPerformance = 0x0F, // Picture of the artists during performance
		MovieScreenCapture = 0x10, // Picture from a movie or video related to the track
		ColoredFish = 0x11, // Picture of a large, coloured fish
		Illustration = 0x12, // Illustration related to the track
		BandLogo = 0x13, // Logo of the band or performer
		PublisherLogo = 0x14  // Logo of the publisher (record company)
	}
	#endregion

	#region ReadStyle
	public enum ReadStyle
	{
		None,
		Fast,
		Average,
		Accurate
	}
	#endregion

	#region StringType
	public enum StringType
	{
		Latin1 = 0,
		UTF16 = 1,
		UTF16BE = 2,
		UTF8 = 3,
		UTF16LE = 4
	}
	#endregion

	#region TagLibError
	public enum TagLibError
	{
		None = 0,
		ReadAccessNotAvailable,
		WriteAccessNotAvailable,
		AsfObjectGuidIncorrect,
		AsfObjectSizeTooSmall,
		MpegCouldNotStripTags,
		MpegCouldNotWriteTags,
		Mpeg4TagSaveFailed,
		Mpeg4StreamDoesNotHaveMoovTag,
		Mpeg4CouldNotSaveAppleTag
	}
	#endregion

	#region TagTypes
	[Flags]
	public enum TagTypes
	{
		None = 0, //was 0x0000,
		Xiph = 0x0001,
		Id3v1 = 0x0002,
		Id3v2 = 0x0004,
		Ape = 0x0008,
		Apple = 0x0010,
		Asf = 0x0020,
		AllTags = (Xiph | Id3v1 | Id3v2 | Ape | Apple | Asf) //0xFFFF
	}
	#endregion
}
