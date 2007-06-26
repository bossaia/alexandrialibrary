using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Media.IO
{
	public abstract class AudioStream : MediaStream, IAudioStream
	{
		#region Constructors
		public AudioStream(string path) : base(path)
		{
			this.CanPlay = true;
		}
		#endregion
	
		#region IAudioStream Members
		public abstract float Volume
		{
			get;
			set;
		}

		public abstract bool IsMuted
		{
			get;
			set;
		}
		#endregion
	}
}
