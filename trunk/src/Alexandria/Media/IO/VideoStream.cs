using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Media.IO
{
	public abstract class VideoStream : MediaStream, IVideoStream
	{
		#region Constructors
		public VideoStream(string path) : base(path)
		{
			this.CanPlay = true;
		}
		#endregion
	
		#region IVideoStream Members
		public abstract int Width
		{
			get;
		}

		public abstract int Height
		{
			get;
		}
		#endregion
	}
}
