using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Plugins
{
	public enum PluginSettingType
	{
		None = 0,
		Text,
		MaskedText,
		Integer,
		Real,
		Boolean,
		Enumeration,
		DirectoryPath,
		FilePath,
		FileName
	}
}
