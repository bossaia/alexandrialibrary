using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Web
{
	/// <summary>
	/// Describes what kind of media the resource is entended to be consumed with
	/// </summary>
	/// <remarks>
	/// Future versions of HTML may introduce new values and may allow parameterized values. To facilitate the introduction of these extensions, conforming user agents must be able to parse the media attribute value as follows:
	/// The value is a comma-separated list of entries. For example,
	/// media="screen, 3d-glasses, print and resolution > 90dpi"
	/// is mapped to:
	///     "screen"
	///     "3d-glasses"
	///     "print and resolution > 90dpi"
	/// Each entry is truncated just before the first character that isn't a US ASCII letter [a-zA-Z] (ISO 10646 hex 41-5a, 61-7a), digit [0-9] (hex 30-39), or hyphen (hex 2d). In the example, this gives:
	///     "screen"
	///     "3d-glasses"
	///     "print"
	/// A case-sensitive match is then made with the set of media types defined above. User agents may ignore entries that don't match. In the example we are left with screen and print.
	/// </remarks>
	public enum MediaDescriptor
	{
		None = 0,
		screen,
		tty,
		tv,
		projection,
		handheld,
		print,
		braille,
		aural,
		all
	}
}
