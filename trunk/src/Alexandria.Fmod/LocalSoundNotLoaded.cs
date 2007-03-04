using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace AlexandriaOrg.Alexandria.Fmod
{
	public sealed class LocalSoundNotLoaded : AudioStatus
	{
		#region Constructors
		private LocalSoundNotLoaded() : base(true, false, false, false, false, false)
		{
		}
		#endregion
		
		#region Static Members
		private static LocalSoundNotLoaded example;
		
		public static LocalSoundNotLoaded Example
		{
			get
			{
				if (example == null) example = new LocalSoundNotLoaded();
				
				return example;
			}
		}
		#endregion
	}
}
