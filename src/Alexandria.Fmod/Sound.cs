using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Timers;
using AlexandriaOrg.Alexandria;

namespace AlexandriaOrg.Alexandria.Fmod
{
	[PluginAttributes.SoundClass]
	public class Sound : IDisposable,ILoopTarget,IRangeTarget,IHasDefault,ISound
	{
		#region Constructors
		/// <summary>
		/// The full Sound constructor
		/// </summary>
		/// <param name="soundSystem">The sound system that this sound is associated with</param>
		/// <param name="mediaFile">The path or url of the file that the sound comes from</param>
		public Sound(SoundSystem soundSystem, MediaFile mediaFile)
		{			
			this.soundSystem = soundSystem;
			this.mediaFile = mediaFile;
			if (mediaFile != null)
			{
				if (this.mediaFile.IsLocal)
					status = LocalSoundNotLoaded.Example;
				else
					status = RemoteSoundNotReady.Example;
			}
		}
		
		public Sound(SoundSystem soundSystem, OpticalDrive drive)
		{
			this.soundSystem = soundSystem;
			if (drive != null)
			{
				mediaFile = new MediaFile(drive.Name, true);
				if (this.mediaFile.IsLocal)
					status = LocalSoundNotLoaded.Example;
				else
					status = RemoteSoundNotReady.Example;
			}
		}
		
		/// <summary>
		/// SubSound constructor
		/// </summary>
		/// <param name="parentSound">The parent Sound of this SubSound</param>
		public Sound(Sound parentSound)
		{
			if (parentSound != null)
			{
				this.soundSystem = parentSound.SoundSystem;
				this.parentSound = parentSound;

				if (parentSound.mediaFile.IsLocal)
					status = LocalSoundNotLoaded.Example;
				else
					status = RemoteSoundNotReady.Example;
			}
		}
		#endregion
		
		#region Finalizer
		~Sound()
		{
			Dispose(false);
		}
		#endregion
		
		#region IDisposable Members
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					this.channel.Dispose();
					this.channel = null;
				
					if (this.subSounds != null)
					{
						uint i =0;
						foreach(Sound subSound in this.subSounds)
						{
							subSound.Dispose();
							this.subSounds.Remove(i);
							i++;						
						}
					}
				}
				
				if (handle != IntPtr.Zero)
				{
					NativeMethods.FMOD_Sound_Release(handle);
					handle = IntPtr.Zero;
				}
			}
			disposed = true;
		}
		
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion

		#region Private Fields
		private Channel channel = new Channel();
		private string name;
		private uint length;
		private Sound parentSound;
		private IntPtr handle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		private SoundSystem soundSystem;
		private SoundLock soundLock;
		private SoundDefault soundDefault;
		private SoundVariation soundVariation;
		private Range range;
		private TimeUnits lengthUnit = TimeUnits.Millisecond;
		private SoundType type = SoundType.Unknown;
		private SoundFormat format = SoundFormat.None;
		private int numberOfChannels;
		private int numberOfBitsPerSample;
		private OpenState openState;
		private uint percentBuffered;
		private bool bufferIsStarving;
		private Modes mode;
		private SoundLoop loop;		
		private SoundCollection subSounds;
		private TagCollection tags;
		private IntPtr userData = IntPtr.Zero;
		private MediaFile mediaFile;
		private bool disposed;
		private SoundStatus status;
		#endregion
		
		#region Internal Methods
		internal void StartPlayback()
		{
			this.soundSystem.PlaySound(ChannelIndex.Free, this, false, ref channel);
		}

		internal void PausePlayback()
		{
			this.channel.Paused = true;
		}

		internal void ResumePlayback()
		{
			this.channel.Paused = false;
		}

		internal void StopPlayback()
		{
			this.channel.Stop();
		}
		#endregion
				
		#region Public Properties
		
		#region Channel
		public Channel Channel
		{
			get {return channel;}
		}
		#endregion
		
		#region ParentSound
		public Sound ParentSound
		{
			get {return parentSound;}
		}
		#endregion
		
		#region Handle
		/// <summary>
		/// A pointer handle to the unmanaged sound resources
		/// </summary>
		public IntPtr Handle
		{
			get {return handle;}
			set {handle = value;}
		}
		#endregion
		
		#region CurrentResult
		/// <summary>
		/// Gets the current result state of this sound
		/// </summary>
		public Result CurrentResult
		{
			get {return currentResult;}
		}
		#endregion
		
		#region SoundSystem
		public SoundSystem SoundSystem
		{
			get
			{
				#region Legacy Code
				/*
				if (soundSystem == null)
				{
					currentResult = Result.Ok;
					IntPtr systemHandle = new IntPtr();

					try
					{
						currentResult = NativeMethods.FMOD_Sound_GetSystemObject(handle, ref systemHandle);
					}
					catch (System.Runtime.InteropServices.ExternalException)
					{
						currentResult = Result.InvalidParameterError;
					}
					
					if (currentResult == Result.Ok)
					{
						soundSystem = new SoundSystem();
						soundSystem.Handle = systemHandle;
					}
				}
				*/
				#endregion
			
				return soundSystem;
			}
			internal set {soundSystem = value;}
		}
		#endregion
		
		#region Lock
		/// <summary>
		/// Gets and sets the lock currently conntected to this sound
		/// </summary>
		public SoundLock Lock
		{
			get {return soundLock;}
			set {soundLock = value;}
		}
		#endregion
		
		#region Default
		public SoundDefault Default
		{
			get {return soundDefault;}
			set
			{
				soundDefault = value;
				if (soundDefault != null)
				{
					currentResult = NativeMethods.FMOD_Sound_SetDefaults(handle, soundDefault.Frequency, soundDefault.Volume, soundDefault.Pan, soundDefault.Priority);
				}
			}
		}
		#endregion
		
		#region Variation
		public SoundVariation Variation
		{
			get {return soundVariation;}
			set {soundVariation = value;}
		}
		#endregion

		#region Range
		public Range Range
		{
			get {return range;}
			set
			{
				range = value;
				if (range != null)
					currentResult = NativeMethods.FMOD_Sound_Set3DMinMaxDistance(handle, range.Minimum, range.Maximum);
			}
		}
		#endregion

		#region SubSounds
		public SoundCollection SubSounds
		{
			get
			{
				// Lazy initialization
				if (subSounds == null && this.Handle != IntPtr.Zero)
				{
					subSounds = new SoundCollection(this, true);
				}
				
				return subSounds;
			}
		}		
		#endregion
		
		#region Name
		public string Name
		{
			get
			{
				StringBuilder nameBuilder = new StringBuilder(100);
				currentResult = NativeMethods.FMOD_Sound_GetName(handle, nameBuilder, nameBuilder.Capacity);
				name = nameBuilder.ToString();
				return name;
			}
		}
		#endregion
		
		#region LengthUnit
		public TimeUnits LengthUnit
		{
			get {return lengthUnit;}
			set {lengthUnit = value;}
		}
		#endregion
		
		#region Length
		[CLSCompliant(false)]
		public uint Length
		{
			get
			{
				currentResult = NativeMethods.FMOD_Sound_GetLength(handle, ref length, lengthUnit);
				return length;
			}
		}
		#endregion
		
		#region Type
		public SoundType Type
		{
			get
			{
				currentResult = NativeMethods.FMOD_Sound_GetFormat(handle, ref type, ref format, ref numberOfChannels, ref numberOfBitsPerSample);
				return type;				
			}
		}
		#endregion
		
		#region Format
		public SoundFormat Format
		{
			get
			{
				currentResult = NativeMethods.FMOD_Sound_GetFormat(handle, ref type, ref format, ref numberOfChannels, ref numberOfBitsPerSample);
				return format;
			}
		}
		#endregion
		
		#region NumberOfChannels
		public int NumberOfChannels
		{
			get
			{
				currentResult = NativeMethods.FMOD_Sound_GetFormat(handle, ref type, ref format, ref numberOfChannels, ref numberOfBitsPerSample);
				return numberOfChannels;
			}
		}
		#endregion
		
		#region NumberOfBitsPerSample
		public int NumberOfBitsPerSample
		{
			get
			{
				currentResult = NativeMethods.FMOD_Sound_GetFormat(handle, ref type, ref format, ref numberOfChannels, ref numberOfBitsPerSample);
				return numberOfBitsPerSample;
			}
		}
		#endregion
		
		#region Tags
		public TagCollection Tags
		{
			get
			{
				// Lazy initialization
				if (tags == null && handle != IntPtr.Zero)
				{
					tags = new TagCollection(handle, true);
				}
				
				return tags;
			}
		}
		#endregion
		
		//TODO: move all status related properties into SoundStatus classes
		
		#region OpenState
		public OpenState OpenState
		{
			get
			{
				currentResult = NativeMethods.FMOD_Sound_GetOpenState(handle, ref openState, ref percentBuffered, ref bufferIsStarving);
				return openState;
			}
		}
		#endregion
		
		#region OpenStateName
		public string OpenStateName
		{
			get {return OpenState.ToString();}
		}
		#endregion
		
		#region PercentBuffered
		[CLSCompliant(false)]
		public uint PercentBuffered
		{
			get
			{
				currentResult = NativeMethods.FMOD_Sound_GetOpenState(handle, ref openState, ref percentBuffered, ref bufferIsStarving);
				return percentBuffered;
			}
		}
		#endregion
		
		#region BufferIsStarving
		public bool BufferIsStarving
		{
			get
			{
				currentResult = NativeMethods.FMOD_Sound_GetOpenState(handle, ref openState, ref percentBuffered, ref bufferIsStarving);
				return bufferIsStarving;
			}
		}
		#endregion
		
		
				
		
		#region Mode
		public Modes Mode
		{
			get
			{
				currentResult = NativeMethods.FMOD_Sound_GetMode(handle, ref mode);
				return mode;
			}
			set
			{
				mode = value;
				currentResult = NativeMethods.FMOD_Sound_SetMode(handle, mode);
			}
		}
		#endregion
		
		#region Loop
		[CLSCompliant(false)]
		public SoundLoop Loop
		{
			get {return loop;}
			set
			{
				loop = value;
				if (loop != null)
				{
					currentResult = NativeMethods.FMOD_Sound_SetLoopPoints(handle, loop.Start, loop.StartUnit, loop.End, loop.EndUnit);
					currentResult = NativeMethods.FMOD_Sound_SetLoopCount(handle, loop.Count);
				}
			}
		}
		#endregion
				
		#region UserData
		public IntPtr UserData
		{
			get
			{
				currentResult = NativeMethods.FMOD_Sound_GetUserData(handle, ref userData);
				return userData;
			}
			set
			{
				userData = value;
				currentResult = NativeMethods.FMOD_Sound_SetUserData(handle, userData);
			}
		}
		#endregion
		
		#region MediaFile
		public MediaFile MediaFile
		{
			get {return mediaFile;}
			set {mediaFile = value;}
		}
		#endregion
		
		#region Milliseconds
		/// <summary>
		/// Get the length of the sound in milliseconds
		/// </summary>
		[CLSCompliant(false)]
		public uint Milliseconds
		{
			get
			{
				this.LengthUnit = TimeUnits.Millisecond;
				return this.Length;
			}
		}
		#endregion
		
		#region Position
		/// <summary>
		/// Get the current position of the sound in milliseconds
		/// </summary>
		[CLSCompliant(false)]
		public uint Position
		{
			get { return this.channel.Position; }
			set { this.channel.Position = value; }
		}
		#endregion
				
		#region NumberOfTags
		public int NumberOfTags
		{
			get
			{
				int numberOfTags = 0;
				int numberOfUpdatedTags = 0;
				currentResult = NativeMethods.FMOD_Sound_GetNumTags(this.handle, ref numberOfTags, ref numberOfUpdatedTags);
				return numberOfTags;
			}
		}
		#endregion
		
		#region NumberOfUpdatedTags
		public int NumberOfUpdatedTags
		{
			get
			{
				int numberOfTags = 0;
				int numberOfUpdatedTags = 0;
				NativeMethods.FMOD_Sound_GetNumTags(this.handle, ref numberOfTags, ref numberOfUpdatedTags);
				return numberOfUpdatedTags;
			}
		}
		#endregion
		
		#endregion

		#region Public Methods
		
		#region Load
		public void Load()
		{
			if (this.MediaFile.IsLocal)
			{
				this.SoundSystem.CreateStream(this, this.MediaFile.Path, Modes.None);
			}
			else
			{
				//soundSystem.StreamBufferUnit = streamBufferUnit;
				//soundSystem.StreamBufferSize = streamBufferSize;
				//soundSystem.StreamBufferUnit = TimeUnits.RawByte;
				//soundSystem.StreamBufferSize = (64 * 1024);
				Load((64 * 1024));
			}
		}

		[CLSCompliant(false)]
		public void Load(uint streamBufferSize)
		{
			if (this.MediaFile.IsLocal)
			{
				Load();
			}
			else
			{
				this.SoundSystem.StreamBufferUnit = TimeUnits.RawByte; //streamBufferUnit;
				this.SoundSystem.StreamBufferSize = streamBufferSize;
				Modes mode = (Modes.Software | Modes.Fmod2D | Modes.CreateStream | Modes.NonBlocking);
				this.SoundSystem.CreateSound(this, this.MediaFile.Path, mode);
			}
		}
		#endregion

		#region Play
		public void Play()
		{
		}
		#endregion

		#region Pause
		public void Pause()
		{			
		}
		#endregion

		#region Stop
		public void Stop()
		{
		}
		#endregion

		#region Read
		/// <summary>
		/// Reads the given number of bytes from the sound and returns them as a SoundBuffer
		/// </summary>
		/// <param name="buffer">A pointer to a buffer that the data will be read into</param>
		/// <param name="length">The length in bytes to read</param>
		/// <returns>The actual number of bytes that were read (this may be less than length, for example when reaching the end of the sound)</returns>
		[CLSCompliant(false)]
		public uint Read(IntPtr buffer, uint length)
		{
			uint bytesRead = 0;
			currentResult = NativeMethods.FMOD_Sound_ReadData(handle, buffer, length, ref bytesRead);
			return bytesRead;
		}
		#endregion

		#region Save
		/// <summary>
		/// Save this sound to a PCM Wav file with the given path
		/// </summary>
		/// <param name="filePath">The path of the file to save this sound to</param>
		[EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public unsafe void Save(string filePath)
		{
			const int chunkSize = 4096;
			FileStream file = null;
			IntPtr data = IntPtr.Zero;

			try
			{
				// Create the file
				file = new FileStream(filePath, FileMode.Create, FileAccess.Write);

				// Alocate memory for the PCM data buffer
				data = Marshal.AllocHGlobal(chunkSize);

				// Create a holding buffer that the PCM data will be copied into
				byte[] buffer = new byte[chunkSize];

				uint totalSize = 0;
				uint bytesRead = 0;

				// Get the length of the sound in PCM bytes
				this.LengthUnit = TimeUnits.PcmByte;
				uint pcmLength = this.Length;

				// Create the WAV structures
				FmtChunk fmtChunk = new FmtChunk();
				DataChunk dataChunk = new DataChunk();
				WavHeader wavHeader = new WavHeader();
				RiffChunk riffChunk = new RiffChunk();

				fmtChunk.Chunk = new RiffChunk();
				fmtChunk.Chunk.Id = new char[4] { 'f', 'm', 't', ' ' };
				fmtChunk.Chunk.Size = Marshal.SizeOf(fmtChunk) - Marshal.SizeOf(riffChunk);
				fmtChunk.FormatTag = 1;
				fmtChunk.NumberOfChannels = (ushort)this.NumberOfChannels;
				fmtChunk.SamplesPerSecond = (uint)this.Default.Frequency;
				fmtChunk.AverageBytesPerSecond = (uint)(this.Default.Frequency * this.NumberOfChannels * this.NumberOfBitsPerSample / 8);
				fmtChunk.BlockSize = (ushort)(1 * this.NumberOfChannels * this.NumberOfBitsPerSample / 8);
				fmtChunk.NumberOfBitsPerSample = (ushort)this.NumberOfBitsPerSample;

				dataChunk.Chunk = new RiffChunk();
				dataChunk.Chunk.Id = new char[4] { 'd', 'a', 't', 'a' };
				dataChunk.Chunk.Size = (int)pcmLength;

				wavHeader.Chunk = new RiffChunk();
				wavHeader.Chunk.Id = new char[4] { 'R', 'I', 'F', 'F' };
				wavHeader.Chunk.Size = (int)(Marshal.SizeOf(fmtChunk) + Marshal.SizeOf(riffChunk) + pcmLength);
				wavHeader.RiffType = new char[4] { 'W', 'A', 'V', 'E' };

				// Write out the WAV header.			
				IntPtr wavHeaderPtr = Marshal.AllocHGlobal(Marshal.SizeOf(wavHeader));
				IntPtr fmtChunkPtr = Marshal.AllocHGlobal(Marshal.SizeOf(fmtChunk));
				IntPtr dataChunkPtr = Marshal.AllocHGlobal(Marshal.SizeOf(dataChunk));
				byte[] wavHeaderBytes = new byte[Marshal.SizeOf(wavHeader)];
				byte[] fmtChunkBytes = new byte[Marshal.SizeOf(fmtChunk)];
				byte[] dataChunkBytes = new byte[Marshal.SizeOf(dataChunk)];

				Marshal.StructureToPtr(wavHeader, wavHeaderPtr, false);
				Marshal.Copy(wavHeaderPtr, wavHeaderBytes, 0, Marshal.SizeOf(wavHeader));

				Marshal.StructureToPtr(fmtChunk, fmtChunkPtr, false);
				Marshal.Copy(fmtChunkPtr, fmtChunkBytes, 0, Marshal.SizeOf(fmtChunk));

				Marshal.StructureToPtr(dataChunk, dataChunkPtr, false);
				Marshal.Copy(dataChunkPtr, dataChunkBytes, 0, Marshal.SizeOf(dataChunk));

				file.Write(wavHeaderBytes, 0, Marshal.SizeOf(wavHeader));
				file.Write(fmtChunkBytes, 0, Marshal.SizeOf(fmtChunk));
				file.Write(dataChunkBytes, 0, Marshal.SizeOf(dataChunk));

				do
				{
					// Read PCM data from the sound
					bytesRead = Read(data, chunkSize);

					// Copy the PCM data into the buffer
					Marshal.Copy(data, buffer, 0, chunkSize);

					// Write the PCM data to the FileStream
					file.Write(buffer, 0, (int)bytesRead);

					// Increment totalRead
					totalSize += bytesRead;
				}
				while (currentResult == Result.Ok && bytesRead == chunkSize);

				//file.Close();
			}
			catch (Exception ex)
			{
				throw new AlexandriaException("There was an error saving this sound to " + filePath + " : " + currentResult.ToString() + ":" + ex.Message, ex);
			}
			finally
			{
				// Always free the data pointer
				if (data != IntPtr.Zero) Marshal.FreeHGlobal(data);

				// Always close the file
				if (file != null) file.Close();
			}
		}
		#endregion

		#endregion

		#region ISound Members
		[CLSCompliant(false)]
		public uint Minutes
		{
			get
			{
				this.LengthUnit = TimeUnits.Millisecond;
				return (this.Length / 60000);
			}
		}

		[CLSCompliant(false)]
		public uint Seconds
		{
			get
			{
				this.LengthUnit = TimeUnits.Millisecond;
				return (this.Length / 10000);
			}
		}

		public SoundStatus Status
		{
			get { return status; }
			internal set { status = value; }
		}
		#endregion
	}
}
