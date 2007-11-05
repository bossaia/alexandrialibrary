using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Creation
{
	public interface IPiece
	{
		string Title { get; set; }
		string Type { get; set; }
		IArtist Creator { get; set; }
		DateTime DateCreated { get; set; }
		IList<ITheme> Themes { get; }
		IList<IStyle> Styles { get; }
	}
}
