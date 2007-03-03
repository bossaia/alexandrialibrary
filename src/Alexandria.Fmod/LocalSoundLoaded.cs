using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace AlexandriaOrg.Alexandria.Fmod
{
	public sealed class LocalSoundLoaded : Alexandria.SoundStatus
	{
		#region Constructors
		private LocalSoundLoaded() : base(false, false, true, true, true, true)
		{
		}
		#endregion
		
		#region Static Members
		private static LocalSoundLoaded example;
		
		public static LocalSoundLoaded Example
		{
			get
			{
				if (example == null) example = new LocalSoundLoaded();
				
				return example;
			}
		}
		#endregion
	}
}
