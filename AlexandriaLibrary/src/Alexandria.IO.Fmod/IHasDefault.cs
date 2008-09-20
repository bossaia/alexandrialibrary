using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Fmod
{
	public interface IHasDefault
	{
		SoundSettings DefaultSettings{get;set;}
	}
}
