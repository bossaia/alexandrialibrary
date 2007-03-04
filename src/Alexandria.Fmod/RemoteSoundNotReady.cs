using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace AlexandriaOrg.Alexandria.Fmod
{
	public sealed class RemoteSoundNotReady : AudioStatus
	{
		#region Constructors
		private RemoteSoundNotReady() : base(false, true, false, false, false, false)
		{
		}
		#endregion
		
		#region Static Members
		private static RemoteSoundNotReady example;
		
		public static RemoteSoundNotReady Example
		{
			get
			{
				if (example == null) example = new RemoteSoundNotReady();
				
				return example;
			}
		}
		#endregion
	}
}
