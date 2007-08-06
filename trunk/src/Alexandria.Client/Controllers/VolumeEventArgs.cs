using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Client.Controllers
{
	public class VolumeEventArgs : EventArgs
	{
		#region Constructors
		public VolumeEventArgs(float volume, bool isMuted)
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
