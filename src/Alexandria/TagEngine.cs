using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class TagEngine : ITagEngine
	{
		#region Constructors
		public TagEngine()
		{
		}
		#endregion

		#region ITagEngine Members
		[System.CLSCompliant(false)]
		public virtual IAudioTag GetAudioTag(MediaFile file)
		{
			//throw new Exception("The method or operation is not implemented.");
			return null;
		}
		#endregion
	}
}
