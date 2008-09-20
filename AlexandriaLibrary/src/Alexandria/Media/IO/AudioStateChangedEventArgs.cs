using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Media.IO
{
	public class AudioStateChangedEventArgs : MediaEventArgs
	{
		#region Constructors
		public AudioStateChangedEventArgs(float volume, bool isMuted)
		{
			this.volume = volume;
			this.isMuted = isMuted;
		}
		#endregion
		
		#region Private Fields
		private float volume;
		private bool isMuted;
		#endregion
		
		#region Public Properties
		public float Volume
		{
			get { return volume; }
		}
		
		public bool IsMuted
		{
			get { return isMuted; }
		}
		#endregion
	}
}
