using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Alexandria;

namespace AlexandriaOrg.Alexandria.Fmod
{
	public class SoundSystem : IDisposable
	{
		#region Private Fields
		private IntPtr handle = IntPtr.Zero;
		private Result currentResult = Result.Ok;
		private ChannelGroup masterChannelGroup;
		private Dsp dspHead;
		private bool isInitialized;
		private OutputType outputType = OutputType.Unknown;
		private DriverCollection drivers;
		private int numberOf2dHardwareChannels;
		private int minimum2dHardwareChannels;
		private int maximum2dHardwareChannels;
		private int numberOf3dHardwareChannels;
		private int minimum3dHardwareChannels;
		private int maximum3dHardwareChannels;
		private int totalHardwareChannels;
		private int numberOfSoftwareChannels;
		private int sampleRate;
		private FmodSoundFormat soundFormat;
		private int numberOfOutputChannels;
		private int maximumInputChannels;
		private DspResampler resampleMethod;
		private int bitsPerSample;
		private uint dspBufferLength;
		private int numberOfDspBuffers;
		private FileOpenCallback userFileOpenCallback;
		private FileCloseCallback userFileCloseCallback;
		private FileReadCallback userFileReadCallback;
		private FileSeekCallback userFileSeekCallback;
		private int userFileCallbackReadBufferSize = 2048;
		private SpeakerMode speakerMode = SpeakerMode.Mono;
		private string pluginPath = string.Empty;
		private PluginCollection codecPlugins;
		private PluginCollection dspPlugins;
		private PluginCollection outputPlugins;
		//private Plugin currentOutputPlugin = null;
		private float dopplerScale;
		private float distanceFactor;
		private float rollOffScale;
		private ListenerCollection listeners;
		private Speaker currentSpeaker;
		private float currentSpeakerPositionX;
		private float currentSpeakerPositionY;
		private uint streamBufferSize;
		private TimeUnits streamBufferUnit = TimeUnits.Millisecond;
		private uint version;
		private IntPtr outputHandle = IntPtr.Zero;
		private int numberOfChannelsPlaying;
		private float cpuPercentDsp;
		private float cpuPercentStream;
		private float cpuPercentUpdate;
		private float cpuPercentTotal;
		private OpticalDriveCollection opticalDrives;
		
		// spectrum and wave fields - no longer used
		//private float[] spectrumArray;
		//private int spectrumResolution = 2048; // min = 64, max = 8192, use powers of 2
		//private int spectrumChannelOffset; // 0 = left, 1 = right
		//private DspFftWindow spectrumDspWindowType = DspFftWindow.Rectangle;
		//private float[] waveArray;
		//private int waveResolution = 100;
		//private int waveChannelOffset; // 0 = left, 1 = right
		
		//NOTE: there seems to be a bug in FMODex 4.04 with reverbProperties
		//private ReverbProperties reverbProperties = null;
		private RecordDriverCollection recordDrivers;
		private Driver currentRecordDriver;
		private bool isRecording;
		private uint recordPosition;
		private float maximumWorldSize;
		//private string networkProxy = string.Empty;
		private IntPtr userData = IntPtr.Zero;
		private bool disposed;
		#endregion
		
		#region Private Methods
		
		#region GetHardwareChannels
		private void GetHardwareChannels()
		{
			currentResult = NativeMethods.FMOD_System_GetHardwareChannels(handle, ref numberOf2dHardwareChannels, ref numberOf3dHardwareChannels, ref totalHardwareChannels);
		}		
		#endregion
		
		#region SetHardwareChannels
		private void SetHardwareChannels()
		{
			currentResult = NativeMethods.FMOD_System_SetHardwareChannels(handle, minimum2dHardwareChannels, maximum2dHardwareChannels, minimum3dHardwareChannels, maximum3dHardwareChannels);
		}
		#endregion
		
		#region GetSoftwareFormat
		private void GetSoftwareFormat()
		{
			currentResult = NativeMethods.FMOD_System_GetSoftwareFormat(handle, ref sampleRate, ref soundFormat, ref numberOfOutputChannels, ref maximumInputChannels, ref resampleMethod, ref bitsPerSample);
		}
		#endregion
		
		#region SetSoftwareFormat
		private void SetSoftwareFormat()
		{
			currentResult = NativeMethods.FMOD_System_SetSoftwareFormat(handle, sampleRate, soundFormat, numberOfOutputChannels, maximumInputChannels, resampleMethod); 
		}
		#endregion
		
		#region GetDspBufferInfo
		private void GetDspBufferInfo()
		{
			currentResult = NativeMethods.FMOD_System_GetDSPBufferSize(handle, ref dspBufferLength, ref numberOfDspBuffers);
		}
		#endregion
		
		#region SetDspBufferInfo
		private void SetDspBufferInfo()
		{
			currentResult = NativeMethods.FMOD_System_SetDSPBufferSize(handle, dspBufferLength, numberOfDspBuffers);
		}
		#endregion
		
		#region SetUserFileCallbacks
		private void SetUserFileCallbacks()
		{
			//if (attachUserFileCallbacks)
			//{
				currentResult = NativeMethods.FMOD_System_AttachFileSystem(handle, userFileOpenCallback, userFileCloseCallback, userFileReadCallback, userFileSeekCallback);
				//currentResult = NativeMethods.FMOD_System_SetFileSystem(handle, null, null, null, null, userFileCallbackReadBufferSize);
				//}
				//else
				//{
				//currentResult = NativeMethods.FMOD_System_AttachFileSystem(handle, null, null, null, null);
				//currentResult = NativeMethods.FMOD_System_SetFileSystem(handle, userFileOpenCallback, userFileCloseCallback, userFileReadCallback, userFileSeekCallback, userFileCallbackReadBufferSize);
			//}
		}
		#endregion

		#region Get3dSettings
		private void Get3dSettings()
		{
			currentResult = NativeMethods.FMOD_System_Get3DSettings(handle, ref dopplerScale, ref distanceFactor, ref rollOffScale);
		}
		#endregion

		#region Set3dSettings
		private void Set3dSettings()
		{
			currentResult = NativeMethods.FMOD_System_Set3DSettings(handle, dopplerScale, distanceFactor, rollOffScale);
		}
		#endregion		
		
		#region GetSpeakerPosition
		private void GetSpeakerPosition()
		{
			currentResult = NativeMethods.FMOD_System_GetSpeakerPosition(handle, currentSpeaker, ref currentSpeakerPositionX, ref currentSpeakerPositionY);
		}
		#endregion
		
		#region SetSpeakerPosition
		private void SetSpeakerPosition()
		{
			currentResult = NativeMethods.FMOD_System_SetSpeakerPosition(handle, currentSpeaker, currentSpeakerPositionX, currentSpeakerPositionY);
		}
		#endregion
		
		#region SetStreamBufferInfo
		public void SetStreamBufferInfo()
		{
			currentResult = NativeMethods.FMOD_System_SetStreamBufferSize(handle, streamBufferSize, streamBufferUnit);
		}
		#endregion
		
		#region GetStreamBufferInfo
		public void GetStreamBufferInfo()
		{
			currentResult = NativeMethods.FMOD_System_GetStreamBufferSize(handle, ref streamBufferSize, ref streamBufferUnit);
		}
		#endregion

		#region GetCpuUsageInfo
		private void GetCpuUsageInfo()
		{
			currentResult = NativeMethods.FMOD_System_GetCPUUsage(handle, ref cpuPercentDsp, ref cpuPercentStream, ref cpuPercentUpdate, ref cpuPercentTotal);
		}
		#endregion
				
		#endregion
		
		#region Constructors
		public SoundSystem()
		{
		}
		#endregion
		
		#region Finalizer
		~SoundSystem()
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
					if (drivers != null)
					{
						drivers.Dispose();
						drivers = null;
					}
					
					if (recordDrivers != null)
					{
						recordDrivers.Dispose();
						recordDrivers = null;
					}
					
					if (masterChannelGroup != null)
					{
						masterChannelGroup.Dispose();
						masterChannelGroup = null;
					}
					
					if (dspHead != null)
					{
						dspHead.Dispose();
						dspHead = null;
					}
				}
				
				if (handle != IntPtr.Zero)
				{
					NativeMethods.FMOD_System_Release(handle);
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
		
		#region Public Properties
		
		#region Handle
		public IntPtr Handle
		{
			get {return handle;}
			set {handle = value;}
		}
		#endregion
		
		#region CurrentResult
		public Result CurrentResult
		{
			get {return currentResult;}
		}
		#endregion

		#region MasterChannelGroup
		public ChannelGroup MasterChannelGroup
		{
			get
			{
				if (masterChannelGroup == null)
				{
					currentResult = Result.Ok;
					IntPtr channelGroupHandle = new IntPtr();

					try
					{
						currentResult = NativeMethods.FMOD_System_GetMasterChannelGroup(handle, ref channelGroupHandle);
					}
					catch (System.Runtime.InteropServices.ExternalException)
					{
						currentResult = Result.InvalidParameterError;
					}

					if (currentResult == Result.Ok)
					{
						masterChannelGroup = new ChannelGroup();
						masterChannelGroup.Handle = channelGroupHandle;
					}
				}

				return masterChannelGroup;
			}
		}
		#endregion

		#region DspHead
		public Dsp DspHead
		{
			get
			{
				if (dspHead == null)
				{
					currentResult = Result.Ok;
					IntPtr dspHandle = new IntPtr();

					try
					{
						currentResult = NativeMethods.FMOD_System_GetDSPHead(handle, ref dspHandle);
					}
					catch (System.Runtime.InteropServices.ExternalException)
					{
						currentResult = Result.InvalidParameterError;
					}

					if (currentResult == Result.Ok)
					{
						dspHead = new Dsp();
						dspHead.Handle = dspHandle;
					}
				}

				return dspHead;
			}
		}
		#endregion

		#region IsInitialized
		public bool IsInitialized
		{
			get {return isInitialized;}
		}
		#endregion

		#region OutputType
		public OutputType OutputType
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_GetOutput(handle, ref outputType);
				return outputType;
			}
			set
			{
				outputType = value;
				currentResult = NativeMethods.FMOD_System_SetOutput(handle, outputType);
			}
		}
		#endregion
		
		#region Drivers
		public DriverCollection Drivers
		{
			get
			{
				// Lazy initialization
				if (drivers == null)
				{
					drivers = new DriverCollection(this, true);
				}
				
				return drivers;
			}
		}
		#endregion
		
		#region NumberOf2dHardwareChannels
		public int NumberOf2dHardwareChannels
		{
			get
			{
				GetHardwareChannels();
				return numberOf2dHardwareChannels;
			}			
		}
		#endregion
		
		#region Minimum2dHardwareChannels
		public int Minimum2dHardwareChannels
		{
			get
			{
				GetHardwareChannels();
				return minimum2dHardwareChannels;
			}
			set
			{
				minimum2dHardwareChannels = value;
				SetHardwareChannels();
			}
		}
		#endregion
		
		#region Maximum2dHardwareChannels
		public int Maximum2dHardwareChannels
		{
			get
			{
				GetHardwareChannels();
				return maximum2dHardwareChannels;
			}
			set
			{
				maximum2dHardwareChannels = value;
				SetHardwareChannels();
			}
		}
		#endregion
		
		#region NumberOf3dHardwareChannels
		public int NumberOf3dHardwareChannels
		{
			get
			{
				GetHardwareChannels();
				return numberOf3dHardwareChannels;
			}
		}
		#endregion
		
		#region Minimum3dHardwareChannels
		public int Minimum3dHardwareChannels
		{
			get
			{
				GetHardwareChannels();
				return minimum3dHardwareChannels;
			}
			set
			{
				minimum3dHardwareChannels = value;
				SetHardwareChannels();
			}
		}
		#endregion
		
		#region Maximum3dHardwareChannels
		public int Maximum3dHardwareChannels
		{
			get
			{
				GetHardwareChannels();
				return maximum3dHardwareChannels;
			}
			set
			{
				maximum3dHardwareChannels = value;
				SetHardwareChannels();
			}
		}
		#endregion
		
		#region NumberOfSoftwareChannels
		public int NumberOfSoftwareChannels
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_GetSoftwareChannels(handle, ref numberOfSoftwareChannels);
				return numberOfSoftwareChannels;
			}
			set
			{
				numberOfSoftwareChannels = value;
				currentResult = NativeMethods.FMOD_System_SetSoftwareChannels(handle, numberOfSoftwareChannels);
			}
		}
		#endregion
		
		#region SampleRate
		public int SampleRate
		{
			get
			{
				GetSoftwareFormat();
				return sampleRate;
			}
			set
			{
				sampleRate = value;
				SetSoftwareFormat();
			}
		}
		#endregion
		
		#region FmodSoundFormat
		public FmodSoundFormat SoundFormat
		{
			get
			{
				GetSoftwareFormat();
				return soundFormat;
			}
			set
			{
				soundFormat = value;
				SetSoftwareFormat();
			}
		}
		#endregion
		
		#region NumberOfOutputChannels
		public int NumberOfOutputChannels
		{
			get
			{
				GetSoftwareFormat();
				return numberOfOutputChannels;
			}
			set
			{
				numberOfOutputChannels = value;
				SetSoftwareFormat();
			}
		}
		#endregion
		
		#region MaximumInputChannels
		public int MaximumInputChannels
		{
			get
			{
				GetSoftwareFormat();
				return maximumInputChannels;
			}
			set
			{
				maximumInputChannels = value;
				SetSoftwareFormat();
			}
		}
		#endregion
		
		#region ResampleMethod
		public DspResampler ResampleMethod
		{
			get
			{
				GetSoftwareFormat();
				return resampleMethod;
			}
			set
			{
				resampleMethod = value;
				SetSoftwareFormat();
			}
		}
		#endregion
		
		#region BitsPerSample
		public int BitsPerSample
		{
			get
			{
				GetSoftwareFormat();
				return bitsPerSample;
			}
			set
			{
				bitsPerSample = value;
				SetSoftwareFormat();
			}
		}
		#endregion		
		
		#region DspBufferLength
		[CLSCompliant(false)]
		public uint DspBufferLength
		{
			get
			{
				GetDspBufferInfo();
				return dspBufferLength;
			}
			set
			{
				dspBufferLength = value;
				SetDspBufferInfo();
			}
		}
		#endregion
		
		#region NumberOfDspBuffers
		public int NumberOfDspBuffers
		{
			get
			{
				GetDspBufferInfo();
				return numberOfDspBuffers;
			}
			set
			{
				numberOfDspBuffers = value;
				SetDspBufferInfo();
			}
		}
		#endregion
				
		#region UserFileOpenCallback
		[CLSCompliant(false)]
		public FileOpenCallback UserFileOpenCallback
		{
			get {return userFileOpenCallback;}
			set	{userFileOpenCallback = value;}			
		}
		#endregion
		
		#region UserFileCloseCallback
		public FileCloseCallback UserFileCloseCallback
		{
			get {return userFileCloseCallback;}
			set {userFileCloseCallback = value;}
		}
		#endregion
		
		#region UserFileReadCallback
		[CLSCompliant(false)]
		public FileReadCallback UserFileReadCallback
		{
			get {return userFileReadCallback;}
			set {userFileReadCallback = value;}
		}
		#endregion
		
		#region UserFileSeekCallback
		public FileSeekCallback UserFileSeekCallback
		{
			get {return userFileSeekCallback;}
			set {userFileSeekCallback = value;}
		}
		#endregion
		
		#region UserFileCallbackReadBufferSize
		public int UserFileCallbackReadBufferSize
		{
			get {return userFileCallbackReadBufferSize;}
			set {userFileCallbackReadBufferSize = value;}
		}
		#endregion
		
		#region SpeakerMode
		public SpeakerMode SpeakerMode
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_GetSpeakerMode(handle, ref speakerMode);
				return speakerMode;				
			}
			set
			{
				speakerMode = value;
				currentResult = NativeMethods.FMOD_System_SetSpeakerMode(handle, speakerMode);
			}						
		}
		#endregion

		#region PluginPath
		public string PluginPath
		{
			get
			{
				return pluginPath;
			}
			set
			{	
				pluginPath = value;
				currentResult = NativeMethods.FMOD_System_SetPluginPath(handle, pluginPath);
			}
		}		
		#endregion

		#region CodecPlugins
		public PluginCollection CodecPlugins
		{
			get
			{
				// Lazy initialization
				if (codecPlugins == null)
				{
					codecPlugins = new PluginCollection(handle, PluginType.Codec, true);
				}
				
				return codecPlugins;
			}			
		}
		#endregion

		#region DspPlugins
		public PluginCollection DspPlugins
		{
			get				
			{
				// Lazy initialization
				if (dspPlugins == null)
				{
					dspPlugins = new PluginCollection(handle, PluginType.Dsp, true);
				}
				
				return dspPlugins;
			}
		}
		#endregion
		
		#region OutputPlugins
		public PluginCollection OutputPlugins
		{
			get
			{
				// Lazy initialization
				if (outputPlugins == null)
				{
					outputPlugins = new PluginCollection(handle, PluginType.Output, true);
				}
				
				return outputPlugins;
			}
		}
		#endregion
		
		#region CurrentOutputPlugin
		/*
		public Plugin CurrentOutputPlugin
		{
			get
			{
				int numberOfOutputPlugins = 0;
				currentResult = FMOD_System_GetNumPlugins(handle, PluginType.Output, ref numberOfOutputPlugins);
				if (numberOfOutputPlugins > 0)
				{
					outputPlugins.Refresh();
				
				
					int id = -1;
					currentResult = FMOD_System_GetOutputByPlugin(handle, ref id);
					if (id > -1 && id <= outputPlugins.Items.Count-1)
					{
						currentOutputPlugin = outputPlugins.Items[id];
					}
					else currentOutputPlugin = null;
				
					return currentOutputPlugin;
				}				
				return null;
				//throw new NotImplementedException("OutputPlugin is not implemented");				 
			}
			set
			{
				// Output plugins cannot be set after Init() is invoked				
				if (!isInitialized)
				{
					currentOutputPlugin = value;
					if (currentOutputPlugin != null)
					{
						currentResult = FMOD_System_SetOutputByPlugin(handle, currentOutputPlugin.Id);
					}
				}				
				//throw new NotImplementedException("OutputPlugin is not implemented");
			}
		}
		*/
		#endregion
		
		#region DopplerScale
		public float DopplerScale
		{
			get
			{
				Get3dSettings();
				return dopplerScale;
			}
			set
			{
				dopplerScale = value;
				Set3dSettings();
			}
		}
		#endregion
		
		#region DistanceFactor
		public float DistanceFactor
		{
			get
			{
				Get3dSettings();
				return distanceFactor;
			}
			set
			{
				distanceFactor = value;
				Set3dSettings();
			}
		}
		#endregion
		
		#region RollOffScale
		public float RollOffScale
		{
			get
			{
				Get3dSettings();
				return rollOffScale;
			}
			set
			{
				rollOffScale = value;
				Set3dSettings();
			}
		}
		#endregion
		
		#region Listeners
		public ListenerCollection Listeners
		{
			get
			{
				// Lazy initialization
				if (listeners == null)
				{
					listeners = new ListenerCollection(handle, true);
				}
				
				return listeners;
			}			
		}
		#endregion
		
		#region CurrentSpeaker
		public Speaker CurrentSpeaker
		{
			get {return currentSpeaker;}
			set {currentSpeaker = value;}
		}
		#endregion

		#region CurrentSpeakerPositionX
		public float CurrentSpeakerPositionX
		{
			get
			{
				GetSpeakerPosition();
				return currentSpeakerPositionX;
			}
			set
			{
				currentSpeakerPositionX = value;
				SetSpeakerPosition();
			}
		}
		#endregion
		
		#region CurrentSpeakerPositionY
		public float CurrentSpeakerPositionY
		{
			get
			{
				GetSpeakerPosition();
				return currentSpeakerPositionY;
			}
			set
			{
				currentSpeakerPositionY = value;
				SetSpeakerPosition();
			}
		}
		#endregion

		#region StreamBufferSize
		[CLSCompliant(false)]
		public uint StreamBufferSize
		{
			get
			{
				GetStreamBufferInfo();
				return streamBufferSize;
			}
			set
			{
				streamBufferSize = value;
				SetStreamBufferInfo();
			}
		}
		#endregion
		
		#region StreamBufferUnit
		public TimeUnits StreamBufferUnit
		{
			get
			{
				GetStreamBufferInfo();
				return streamBufferUnit;
			}
			set
			{
				streamBufferUnit = value;
				//SetStreamBufferInfo();
			}
		}
		#endregion
		
		#region Version
		private uint Version
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_GetVersion(handle, ref version);
				return version;
			}
		}
		#endregion
		
		#region OutputHandle
		public IntPtr OutputHandle
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_GetOutputHandle(handle, ref outputHandle);
				return outputHandle;
			}
		}
		#endregion
		
		#region NumberOfChannelsPlaying
		public int NumberOfChannelsPlaying
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_GetChannelsPlaying(handle, ref numberOfChannelsPlaying);
				return numberOfChannelsPlaying;
			}
		}
		#endregion

		#region CpuPercentageDsp
		public float CpuPercentDsp
		{
			get
			{
				GetCpuUsageInfo();
				return cpuPercentDsp;
			}
		}
		#endregion
		
		#region CpuPercentStream
		public float CpuPercentStream
		{
			get
			{
				GetCpuUsageInfo();
				return cpuPercentStream;
			}
		}
		#endregion
		
		#region CpuPercentUpdate
		public float CpuPercentUpdate
		{
			get
			{
				GetCpuUsageInfo();
				return cpuPercentUpdate;
			}
		}
		#endregion
		
		#region CpuPercentTotal
		public float CpuPercentTotal
		{
			get
			{
				GetCpuUsageInfo();
				return cpuPercentTotal;
			}
		}
		#endregion
		
		#region OpticalDrives
		public OpticalDriveCollection OpticalDrives
		{
			get
			{
				// Lazy initialization
				if (opticalDrives == null)
				{
					opticalDrives = new OpticalDriveCollection(handle, true);
				}
				
				return opticalDrives;
			}
		}
		#endregion

		#region SpectrumArray
		/*
		public float[] SpectrumArray
		{
			get
			{
				GetSpectrumData();
				return spectrumArray;
			}
		}
		*/
		#endregion
		
		#region SpectrumResolution
		/*
		/// <summary>
		/// Gets or sets the resolution to use for spectrum analysis
		/// </summary>
		/// <remarks>minimum = 64, maximum = 8192, only use powers of 2</remarks>
		public int SpectrumResolution
		{
			get {return spectrumResolution;}
			set {spectrumResolution = value;}
		}
		*/
		#endregion

		#region SpectrumChannelOffset
		/*
		/// <summary>
		/// Gets or sets which current channel for spectrum analysis
		/// </summary>
		/// <remarks>0 = left, 1 = right</remarks>
		public int SpectrumChannelOffset
		{
			get {return spectrumChannelOffset;}
			set {spectrumChannelOffset = value;}
		}
		*/
		#endregion
		
		#region SpectrumDspWindowType
		/*
		public DspFftWindow SpectrumDspWindowType
		{
			get {return spectrumDspWindowType;}
			set {spectrumDspWindowType = value;}
		}
		*/
		#endregion
		
		#region WaveArray
		/*
		public float[] WaveArray
		{
			get
			{
				GetWaveData();
				return waveArray;
			}
		}
		*/
		#endregion
		
		#region WaveResolution
		/*
		public int WaveResolution
		{
			get	{return waveResolution;}
		}
		*/
		#endregion
		
		#region WaveChannelOffset
		/*
		public int WaveChannelOffset
		{
			get {return waveChannelOffset;}
		}
		*/
		#endregion
		
		#region	ReverbProperties
		/*
		public ReverbProperties ReverbProperties
		{
			get
			{
				//currentResult = FMOD_System_GetReverbProperties(handle, ref reverbProperties);
				//return reverbProperties;
				throw new NotImplementedException("ReverbProperties is not currently implemented");				
			}
			set
			{
				//reverbProperties = value;
				//currentResult = FMOD_System_SetReverbProperties(handle, reverbProperties);
				throw new NotImplementedException("ReverbProperties is not currently implemented");
			}
		}
		*/
		#endregion
				
		#region RecordDrivers
		public RecordDriverCollection RecordDrivers
		{		
			get
			{
				// Lazy initialization
				if (recordDrivers == null)
				{
					// Output has to be set before this list can be accessed
					recordDrivers = new RecordDriverCollection(this, true);
				}
				
				return recordDrivers;
			}
		}
		#endregion

		#region CurrentRecordDriver
		public Driver CurrentRecordDriver
		{
			get
			{
				int id = -1;
				StringBuilder nameBuilder = new StringBuilder(100);
				currentResult = NativeMethods.FMOD_System_GetRecordDriver(handle, ref id);
				currentResult = NativeMethods.FMOD_System_GetRecordDriverName(handle, id, nameBuilder, nameBuilder.Capacity);
				currentRecordDriver = new Driver(handle, id, nameBuilder.ToString());
				return currentRecordDriver;
			}
			set
			{
				currentRecordDriver = value;
				if (currentRecordDriver != null)
				{
					currentResult = NativeMethods.FMOD_System_SetRecordDriver(handle, currentRecordDriver.Id);
				}
			}
		}
		#endregion

		#region IsRecording
		public bool IsRecording
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_IsRecording(handle, ref isRecording);
				return isRecording;		
			}
		}
		#endregion
		
		#region RecordPosition
		[CLSCompliant(false)]
		public uint RecordPosition
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_GetRecordPosition(handle, ref recordPosition);
				return recordPosition;
			}
		}
		#endregion
		
		#region MaximumWorldSize
		public float MaximumWorldSize
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_GetGeometrySettings(handle, ref maximumWorldSize);
				return maximumWorldSize;
			}
			set
			{
				maximumWorldSize = value;
				currentResult = NativeMethods.FMOD_System_SetGeometrySettings(handle, maximumWorldSize);
			}
		}
		#endregion				
		
		#region NetworkProxy
		/*
		public string NetworkProxy
		{
			get
			{
				StringBuilder networkProxyBuilder = new StringBuilder(100);
				currentResult = FMOD_System_GetProxy(handle, networkProxyBuilder, networkProxyBuilder.Capacity);
				return networkProxy.ToString();
				//throw new NotImplementedException("NetworkProxy is not currently implemented");
			}
			set
			{
				networkProxy = value;
				currentResult = FMOD_System_SetProxy(handle, networkProxy);
				//throw new NotImplementedException("NetworkProxy is not currently implemented");
			}
		}
		*/
		#endregion
		
		#region UserData
		public IntPtr UserData
		{
			get
			{
				currentResult = NativeMethods.FMOD_System_GetUserData(handle, ref userData);
				return userData;
			}
			set
			{				
				userData = value;
				currentResult = NativeMethods.FMOD_System_SetUserData(handle, userData);
			}
		}
		#endregion
		
		#endregion
		
		#region Public Methods

		#region Initialize
		public void Initialize(int maximumChannels, InitializationOptions flags, IntPtr extraData)
		{
			isInitialized = true;
			currentResult = NativeMethods.FMOD_System_Init(handle, maximumChannels, flags, extraData);
		}
		#endregion

		#region Close
		public void Close()
		{
			isInitialized = false;
			currentResult = NativeMethods.FMOD_System_Close(handle);
		}
		#endregion
		
		#region Update
		public void Update()
		{
			currentResult = NativeMethods.FMOD_System_Update(handle);
		}
		#endregion

		#region LoadPlugin
		public void LoadPlugin(Plugin plugin)
		{
			if (plugin != null)
			{
				PluginType type = plugin.Type;
				int id = plugin.Id;
				currentResult = NativeMethods.FMOD_System_LoadPlugin(handle, plugin.FileName, ref type, ref id);
			}
			else throw new ArgumentNullException("plugin");
		}
		#endregion
		
		#region UnloadPlugin
		public void UnloadPlugin(Plugin plugin)
		{
			if (plugin != null)
			{
				currentResult = NativeMethods.FMOD_System_UnloadPlugin(handle, plugin.Type, plugin.Id);
			}
			else throw new ArgumentNullException("plugin");
		}
		#endregion

		#region CreateSound
		/*
		public Sound CreateSound(string filePath, Modes mode, ref CreateSoundExtendedInfo info)
		{
			currentResult = Result.Ok;
			IntPtr soundHandle = new IntPtr();
			Sound sound = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateSound(handle, filePath, mode, ref info, ref soundHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult == Result.Ok)
			{
				sound = new Sound();
				sound.Handle = soundHandle;				
			}

			return sound;
		}

		public void CreateSound(string filePath, Modes mode, ref CreateSoundExtendedInfo info, ref Sound sound)
		{
			currentResult = Result.Ok;
			IntPtr soundHandle = new IntPtr();
			if (sound == null) sound = new Sound();

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateSound(handle, filePath, mode, ref info, ref soundHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}

			if (currentResult == Result.Ok)
			{
				sound.Handle = soundHandle;
			}
		}
		*/
		
		public void CreateSound(string filePath, Modes mode, Sound sound)
		{
			if (sound != null)
			{
				currentResult = Result.Ok;
				IntPtr soundHandle = new IntPtr();				

				try
				{
					currentResult = NativeMethods.FMOD_System_CreateSound(handle, filePath, mode, 0, ref soundHandle);
				}
				catch (System.Runtime.InteropServices.ExternalException)
				{
					currentResult = Result.InvalidParameterError;
				}

				if (currentResult == Result.Ok)
				{
					sound.Handle = soundHandle;
					sound.SoundSystem = this;
				}
				else throw new AlexandriaException("could not create sound: " + currentResult.ToString());
			}
			else throw new ArgumentNullException("sound");
		}

		/* 
		public Sound CreateSound(byte[] data, Modes mode, ref CreateSoundExtendedInfo info)
		{
			currentResult = Result.Ok;
			IntPtr soundHandle = new IntPtr();
			Sound sound = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateSound(handle, data, mode, ref info, ref soundHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult == Result.Ok)
			{
				sound = new Sound();
				sound.Handle = soundHandle;				
			}

			return sound;
		}

		public Sound CreateSound(string filePath, Modes mode)
		{
			currentResult = Result.Ok;
			IntPtr soundHandle = new IntPtr();
			Sound sound = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateSound(handle, filePath, mode, 0, ref soundHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult == Result.Ok)
			{
				sound = new Sound();
				sound.Handle = soundHandle;				
			}

			return sound;
		}
		*/

		public void CreateSound(Sound sound, string filePath, Modes mode)
		{
			if (sound != null)
			{
				currentResult = Result.Ok;
				IntPtr soundHandle = new IntPtr();			

				try
				{
					currentResult = NativeMethods.FMOD_System_CreateSound(handle, filePath, mode, 0, ref soundHandle);
				}
				catch (System.Runtime.InteropServices.ExternalException)
				{
					currentResult = Result.InvalidParameterError;
				}

				if (currentResult == Result.Ok)
				{					
					sound.Handle = soundHandle;
					sound.SoundSystem = this;
				}
				else throw new AlexandriaException("could not create sound: " + currentResult.ToString());
			}
			else throw new ArgumentNullException("sound");
		}
		
		public CompactDiscSound CreateCompactDiscSound(IAudioCompactDisc disc)
		{
			if (disc != null)
			{
				currentResult = Result.Ok;
				IntPtr soundHandle = new IntPtr();
				CompactDiscSound sound = null;

				try
				{
					currentResult = NativeMethods.FMOD_System_CreateSound(handle, disc.Location.RelativePath, (Modes.Hardware | Modes.Fmod2D | Modes.CreateStream | Modes.OpenOnly | Modes.IgnoreTags), 0, ref soundHandle);
					//currentResult = FMOD_System_CreateSound(handle, driveName, (Mode.Hardware | Mode.Fmod2D | Mode.IgnoreTags), 0, ref soundHandle);
				}
				catch (System.Runtime.InteropServices.ExternalException)
				{
					currentResult = Result.InvalidParameterError;
				}

				if (currentResult == Result.Ok)
				{
					sound = new CompactDiscSound(this, disc);
					sound.Handle = soundHandle;
				}
				else throw new AlexandriaException("could not create compact disc sound: " + currentResult.ToString());

				return sound;
			}
			else throw new ArgumentNullException("drive");
		}
		
		//public CompactDiscSound CreateCompactDiscSound(char driveLetter)
		//{
			//string driveName = driveLetter + ":";
			//return CreateCompactDiscSound(driveName);
		//}
		#endregion

		#region CreateStream
		public Sound CreateStream(string filePath, Modes mode, ref CreateSoundExtendedInfo info)
		{
			currentResult = Result.Ok;
			IntPtr soundHandle = new IntPtr();
			Sound sound = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateStream(handle, filePath, mode, ref info, ref soundHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult == Result.Ok)
			{
				sound = new Sound(this, new Uri(filePath));
				sound.Handle = soundHandle;
				sound.SoundSystem = this;
			}
			else throw new AlexandriaException("could not create stream: " + currentResult.ToString());

			return sound;
		}

		public Sound CreateStream(Sound sound, byte[] data, Modes mode, ref CreateSoundExtendedInfo info)
		{
			if (sound != null)
			{
				currentResult = Result.Ok;
				IntPtr soundHandle = new IntPtr();				

				try
				{
					currentResult = NativeMethods.FMOD_System_CreateStream(handle, data, mode, ref info, ref soundHandle);
				}
				catch (System.Runtime.InteropServices.ExternalException)
				{
					currentResult = Result.InvalidParameterError;
				}
				
				if (currentResult == Result.Ok)
				{					
					sound.Handle = soundHandle;				
					sound.SoundSystem = this;
				}
				else throw new AlexandriaException("could not create stream: " + currentResult.ToString());

				return sound;
			}
			else throw new ArgumentNullException("sound");
		}

		public Sound CreateStream(string filePath, Modes mode)
		{
			currentResult = Result.Ok;
			IntPtr soundHandle = new IntPtr();
			Sound sound = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateStream(handle, filePath, mode, 0, ref soundHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult == Result.Ok)
			{
				sound = new Sound(this, new Uri(filePath));
				sound.Handle = soundHandle;
				sound.SoundSystem = this;
			}
			else throw new AlexandriaException("could not create stream: " + currentResult.ToString());

			return sound;
		}
		
		public void CreateStream(Sound sound, string filePath, Modes mode)
		{
			if (sound != null)
			{
				currentResult = Result.Ok;
				IntPtr soundHandle = new IntPtr();

				try
				{
					currentResult = NativeMethods.FMOD_System_CreateStream(handle, filePath, mode, 0, ref soundHandle);
				}
				catch (System.Runtime.InteropServices.ExternalException)
				{
					currentResult = Result.InvalidParameterError;
				}

				if (currentResult == Result.Ok)
				{					
					sound.Handle = soundHandle;
					sound.SoundSystem = this;
				}
				else throw new AlexandriaException("could not create stream: " + currentResult.ToString());
			}
			else throw new ArgumentNullException("sound");
		}
		#endregion

		#region CreateDsp
		public Dsp CreateDsp(ref DspDescription description)
		{
			currentResult = Result.Ok;
			IntPtr dspHandle = new IntPtr();
			Dsp dsp = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateDSP(handle, ref description, ref dspHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult == Result.Ok)
			{
				dsp = new Dsp();
				dsp.Handle = dspHandle;				
			}

			return dsp;
		}

		/// <summary>
		/// Create a Dsp by DspType
		/// </summary>
		/// <param name="type">The type of Dsp to create</param>
		/// <returns>A new Dsp</returns>
		public Dsp CreateDsp(DspType type)
		{
			currentResult = Result.Ok;
			IntPtr dspHandle = new IntPtr();
			Dsp dsp = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateDSPByType(handle, type, ref dspHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult == Result.Ok)
			{
				dsp = new Dsp();
				dsp.Handle = dspHandle;				
			}

			return dsp;
		}

		/// <summary>
		/// Create Dsp by index
		/// </summary>
		/// <param name="index">The index of the Dsp to create</param>		
		/// <returns>A new Dsp</returns>
		public Dsp CreateDsp(int index)
		{
			currentResult = Result.Ok;
			IntPtr dspHandle = new IntPtr();
			Dsp dsp = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateDSPByIndex(handle, index, ref dspHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult == Result.Ok)
			{
				dsp = new Dsp();
				dsp.Handle = dspHandle;				
			}

			return dsp;
		}
		#endregion

		#region CreateChannelGroup
		public ChannelGroup CreateChannelGroup(string name)
		{
			currentResult = Result.Ok;
			IntPtr channelGroupHandle = new IntPtr();
			ChannelGroup channelGroup = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateChannelGroup(handle, name, ref channelGroupHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult == Result.Ok)
			{			
				channelGroup = new ChannelGroup();
				channelGroup.Handle = channelGroupHandle;				
			}

			return channelGroup;
		}
		#endregion

		#region PlaySound
		public void PlaySound(ChannelIndex channelId, Sound sound, bool paused, ref Channel channel)
		{
			if (sound != null)
			{
				currentResult = Result.Ok;
				IntPtr channelHandle;			

				if (channel != null)
				{
					channelHandle = channel.Handle;
				}
				else
				{
					channel = new Channel();
					channelHandle = new IntPtr();
				}

				try
				{
					currentResult = NativeMethods.FMOD_System_PlaySound(handle, channelId, sound.Handle, paused, ref channelHandle);
				}
				catch (System.Runtime.InteropServices.ExternalException)
				{
					currentResult = Result.InvalidParameterError;
				}
				
				if (currentResult == Result.Ok)
				{
					channel.Handle = channelHandle;				
				}
				else
				{
					channel = null;
				}
			}
		}
		#endregion

		#region PlayDsp
		public void PlayDsp(ChannelIndex channelId, Dsp dsp, bool paused, ref Channel channel)
		{
			if (dsp != null)
			{
				currentResult = Result.Ok;
				IntPtr channelHandle;

				if (channel != null)
				{
					channelHandle = channel.Handle;
				}
				else
				{
					channel = new Channel();
					channelHandle = new IntPtr();
				}

				try
				{
					currentResult = NativeMethods.FMOD_System_PlayDSP(handle, channelId, dsp.Handle, paused, ref channelHandle);
				}
				catch (System.Runtime.InteropServices.ExternalException)
				{
					currentResult = Result.InvalidParameterError;
				}
				
				if (currentResult != Result.Ok)
				{
					channel = new Channel();
					channel.Handle = channelHandle;				
				}
				else
				{
					channel = null;
				}
			}
			else throw new ArgumentNullException("dsp");
		}
		#endregion

		#region AttachUserFileCallbacks
		/// <summary>
		/// Attach the user file callbacks to this system
		/// </summary>
		public void AttachUserFileCallbacks()
		{
			SetUserFileCallbacks();
		}
		#endregion

		#region GetChannel
		public Channel GetChannel(int channelId)
		{
			currentResult = Result.Ok;
			IntPtr channelHandle = new IntPtr();
			Channel channel = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_GetChannel(handle, channelId, ref channelHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult == Result.Ok)
			{
				channel = new Channel();
				channel.Handle = channelHandle;				
			}

			return channel;
		}
		#endregion

		#region AddDsp
		public void AddDsp(Dsp dsp)
		{
			if (dsp != null)
			{
				currentResult = NativeMethods.FMOD_System_AddDSP(handle, dsp.Handle);
			}
			else throw new ArgumentNullException("dsp");
		}
		#endregion

		#region LockDsp
		public void LockDsp()
		{
			currentResult = NativeMethods.FMOD_System_LockDSP(handle);
		}
		#endregion

		#region UnlockDsp
		public void UnlockDsp()
		{
			currentResult = NativeMethods.FMOD_System_UnlockDSP(handle);
		}
		#endregion

		#region RecordStart
		/// <summary>
		/// Records from audio input to the given sound
		/// </summary>
		/// <param name="sound">The sound to store input audio data to</param>
		/// <param name="loop">Indicates whether or not to loop recording to the beginning of the sound once the end of the sound is reached</param>
		public void RecordStart(Sound sound, bool loop)
		{
			if (sound != null)
			{
				currentResult = NativeMethods.FMOD_System_RecordStart(handle, sound.Handle, loop);
			}
		}
		#endregion

		#region RecordStop
		public void RecordStop()
		{
			currentResult = NativeMethods.FMOD_System_RecordStop(handle);
		}		
		#endregion

		#region CreateGeometry
		/// <summary>
		/// Creates a geometry object with the given polygon and vertext constraints
		/// </summary>
		/// <param name="maximumPolygons"></param>
		/// <param name="maximumVertices"></param>
		/// <returns></returns>
		public Geometry CreateGeometry(int maximumPolygons, int maximumVertices)
		{
			currentResult = Result.Ok;
			IntPtr geometryHandle = new IntPtr();
			Geometry geometry = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_CreateGeometry(handle, maximumPolygons, maximumVertices, ref geometryHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}

			if (currentResult == Result.Ok)
			{
				geometry = new Geometry();
				geometry.Handle = geometryHandle;
			}

			return geometry;
		}	

		/// <summary>
		/// Creates a geometry object from memory
		/// </summary>
		/// <param name="data"></param>
		/// <param name="dataSize"></param>
		/// <returns></returns>
		public Geometry CreateGeometry(IntPtr data, int dataSize)
		{
			currentResult = Result.Ok;
			IntPtr geometryHandle = new IntPtr();
			Geometry geometry = null;

			try
			{
				currentResult = NativeMethods.FMOD_System_LoadGeometry(handle, data, dataSize, ref geometryHandle);
			}
			catch (System.Runtime.InteropServices.ExternalException)
			{
				currentResult = Result.InvalidParameterError;
			}
			
			if (currentResult != Result.Ok)
			{
				geometry = new Geometry();
				geometry.Handle = geometryHandle;				
			}
			
			return geometry;
		}		
		#endregion

		#region GetSpectrumData
		public float[] GetSpectrumData(int spectrumResolution, int spectrumChannelOffset, DspFftWindow spectrumDspWindowType)
		{
			float[] spectrumArray = null;
			currentResult = NativeMethods.FMOD_System_GetSpectrum(handle, spectrumArray, spectrumResolution, spectrumChannelOffset, spectrumDspWindowType);
			return spectrumArray;

		}
		#endregion

		#region GetWaveData
		public float[] GetWaveData(int waveResolution, int waveChannelOffset)
		{
			float[] waveArray = null;
			currentResult = NativeMethods.FMOD_System_GetWaveData(handle, waveArray, waveResolution, waveChannelOffset);
			return waveArray;
		}
		#endregion
		
		#endregion
	}
}
