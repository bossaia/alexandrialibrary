using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Alexandria;

namespace Alexandria.Fmod
{	
	public class SoundCollection : IEnumerable<Sound>
	{
		#region Contructors
		public SoundCollection(Sound parentSound, bool initialize)
		{
			this.parentSound = parentSound;

			if (initialize) Refresh();
		}
		#endregion
	
		#region Private Fields
		private Sound parentSound;
		private Result currentResult = Result.Ok;
		private SortedDictionary<uint, Sound> subSounds = new SortedDictionary<uint, Sound>();
		private int totalCount;
		#endregion
				
		#region Public Properties
		
		#region ParentSound
		public Sound ParentSound
		{
			get {return parentSound;}
		}
		#endregion
		
		#region CurrentResult
		public Result CurrentResult
		{
			get {return currentResult;}
		}
		#endregion
		
		#region Count
		public int Count
		{
			get {return subSounds.Count;}
		}
		#endregion
		
		#endregion
		
		#region Public Methods
		
		#region Refresh
		public void Refresh()
		{
			currentResult = NativeMethods.FMOD_Sound_GetNumSubSounds(parentSound.Handle, ref totalCount);
			
			Sound subSound;
			IntPtr subSoundHandle;
			
			subSounds.Clear();
			//subSounds.Capacity = totalCount + 1;
			
			for (uint i = 0; i < totalCount; i++)
			{				
				subSoundHandle = IntPtr.Zero;
				currentResult = NativeMethods.FMOD_Sound_GetSubSound(parentSound.Handle, (int)i, ref subSoundHandle);
				subSound = new Sound(parentSound);
				subSound.Handle = subSoundHandle;
				subSounds.Add((uint)i + 1, subSound);
			}
		}
		#endregion
		
		#region SetSubSound
		[CLSCompliant(false)]
		public void SetSubSound(uint index, Sound subSound)
		{
			if (index > 0 && index <= subSounds.Count)
			{
				IntPtr subSoundHandle = subSound.Handle;
				currentResult = NativeMethods.FMOD_Sound_SetSubSound(parentSound.Handle, (int)index - 1, subSoundHandle);
				subSounds[index] = subSound;
			}
			else
			{
				//TODO: handle invalid index
			}
		}
		#endregion
		
		#region Stitch
		/// <summary>
		/// For any sound that has subsounds, this function will determine the order of playback of these subsounds, and it will play / stitch together the subsounds without gaps.
		/// </summary>
		/// <remarks>This is a very useful feature for those users wanting to do seamless / gapless stream playback. (ie sports commentary, gapless playback media players etc).</remarks>
		public void Stitch()
		{
			int count = subSounds.Count;
			
			if (count > 0)
			{
				int[] subSoundIntList = new int[count];
				
				for(uint i = 0; i < count-1; i++)
				{
					IntPtr subSoundHandle = subSounds[i].Handle;
					subSoundIntList[i] = subSoundHandle.ToInt32();					
				}

				currentResult = NativeMethods.FMOD_Sound_SetSubSoundSentence(parentSound.Handle, subSoundIntList, count);
			}						
		}
		#endregion
		
		#region Remove
		[CLSCompliant(false)]
		public bool Remove(uint key)
		{
			return subSounds.Remove(key);
		}
		#endregion
		
		#endregion

		#region IEnumerable<Sound> Members
		public IEnumerator<Sound> GetEnumerator()
		{
			foreach(Sound sound in subSounds.Values)
				yield return sound;
		}
		#endregion

		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (Sound sound in subSounds.Values)
				yield return sound;
		}
		#endregion
	}
}