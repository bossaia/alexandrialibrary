using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace AlexandriaOrg.Alexandria
{
	public class Sound : ISound
	{
		#region Private Fields
		private string name;		
		private MediaFile mediaFile;
		private SoundStatus status;
		#endregion
		
		#region Constructors
		/// <summary>
		/// Default Constructor
		/// </summary>
		public Sound()
		{
		}
		#endregion
		
		#region Public Properties
		public string Name
		{
			get {return name;}
			protected set {name = value;}
		}
		
		/// <summary>
		/// Get or set the media file associates with this sound
		/// </summary>
		public MediaFile MediaFile
		{
			get {return mediaFile;}			
			set	{mediaFile = value;}
		}

		[System.CLSCompliant(false)]
		public uint Milliseconds
		{
			get {return 0;}
		}
		
		[CLSCompliant(false)]
		public uint Position
		{
			get {return 0;}
			set {}
		}
		
		public virtual string OpenStateName
		{
			get {return null;}
		}
		
		public virtual bool BufferIsStarving
		{
			get {return false;}
		}

		[System.CLSCompliant(false)]
		public virtual uint PercentBuffered
		{
			get {return 0;}
		}
		
		public SoundStatus Status
		{
			get {return status;}
			protected internal set {status = value;}
		}
		#endregion
		
		#region Public Methods
		public virtual void Load()
		{
		}
		
		[CLSCompliant(false)]
		public virtual void Load(uint streamBufferSize)
		{
		}
		
		public virtual void Play()
		{
		}
		
		public virtual void Pause()
		{
		}
		
		public virtual void Stop()
		{
		}
		
		[EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public virtual void Save(string filePath)
		{
		}
		#endregion
	}
}
