using System;
using System.Collections.Generic;
using System.Text;
using AlexandriaOrg.Alexandria;

namespace AlexandriaOrg.Alexandria.Fmod
{
	public class CompactDiscSound : Sound
	{
		#region Constructors
		public CompactDiscSound(SoundSystem soundSystem, OpticalDrive drive) : base(soundSystem, drive)
		{
		}
		#endregion
	
		#region ICompactDiscSound Members
		//[CLSCompliant(false)]
		//public SortedList<uint, ISound> Tracks
		//{
			//get { return SubSounds.Items; }
		//}
		#endregion
	}
}
