using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.MediaInfo
{
	#region StreamType
	public enum StreamType
	{
		General,
		Video,
		Audio,
		Text,
		Chapters,
		Image
	}
	#endregion

	#region InfoType
	public enum InfoType
	{
		Name,
		Text,
		Measure,
		Options,
		NameText,
		MeasureText,
		Info,
		HowTo
	}
	#endregion

	#region InfoOptions
	public enum InfoOptions
	{
		ShowInInform,
		Support,
		ShowInSupported,
		TypeOfValue
	}
	#endregion
}
