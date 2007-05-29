using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.Fmod
{
	public class RemoteSound : IStreamingAudio
	{
		#region Constructors
		public RemoteSound(string path)
		{
		}
		#endregion
	
		#region IMedia Members
		public IMediaFormat Format
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public IIdentifier Id
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public void Load()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public ILocation Location
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		#endregion

		#region IAudible Members

		public bool IsMuted
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public void Mute()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void SetVolume(float value)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Unmute()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public float Volume
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		#endregion

		#region IPlayable Members

		public void Pause()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Play()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public PlaybackState PlaybackState
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public void Resume()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Stop()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion

		#region IStreaming Members

		public StreamingState StreamingState
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		#endregion

		#region IBuffered Members

		public BufferState BufferState
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public float PercentBuffered
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		#endregion

		#region IHasDuration Members

		public TimeSpan Duration
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		#endregion

		#region IHasElapsed Members

		public TimeSpan GetElapsed()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion
	}
}
