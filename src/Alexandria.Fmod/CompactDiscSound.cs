using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.Fmod
{
	public class CompactDiscSound : Sound
	{
		#region Constructors
		public CompactDiscSound(SoundSystem soundSystem, IAudioCompactDisc disc) : base(soundSystem, disc)
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
