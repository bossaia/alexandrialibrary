using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using Alexandria;

namespace AlexandriaOrg.Alexandria.Fmod
{	
	//[AudioPlayerClass]
	public class FmodAudioPlayer : IAudioPlayer, IDisposable
	{		
		#region Private Fields
		private Channel channel;
		private SoundSystem soundSystem;
		private Sound currentAudio;
		private bool currentSoundIsCreated;
		private bool currentSoundIsStreaming;
		private System.Timers.Timer timer;
		private DateTime soundLoadStart = DateTime.MinValue;
		//private PlaybackStatus status;
		private string streamingStatus;				
		private static Sound ripSound;
		private static Channel ripChannel;
		private static FileStream ripFileStream;
		private static string ripSourceFileName;
		private static string ripDestinationFileName;
		private static SoundType ripSoundType = SoundType.Unknown;
		private static bool ripUpdateFileName;
		private static string ripTrackArtist = string.Empty;
		private static string ripTrackTitle = string.Empty;
		private static System.Timers.Timer ripTimer = new Timer(1000);
		private bool disposed;
		#endregion
		
		#region Constructors
		public FmodAudioPlayer()
		{
			soundSystem = SoundSystemFactory.CreateSoundSystem(true, 64);
		
			InitializeComponent();								
		}
		#endregion
		
		#region IDisposable Members
		protected void Dispose(bool disposing)
		{
			try
			{
				if (!disposed)
				{
					if (disposing)
					{
						if (currentAudio != null)
						{
							currentAudio.Dispose();
							currentAudio = null;
						}

						if (channel != null)
						{
							channel.Dispose();
							channel = null;
						}
					
						if (timer != null)
						{
							timer.Dispose();
							timer = null;
						}
					
						if (ripSound != null)
						{
							ripSound.Dispose();
							ripSound = null;
						}
					
						if (ripChannel != null)
						{
							ripChannel.Dispose();
							ripChannel = null;
						}
					
						if (ripTimer != null)
						{
							ripTimer.Dispose();
							ripTimer = null;
						}
					
						if (soundSystem != null)
						{
							soundSystem.Close();
							soundSystem.Dispose();
							soundSystem = null;
						}
					}
				}
				disposed = true;
			}
			finally
			{
				//base.Dispose(disposing);
			}
		}

		public new void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
		
		#region Private Methods
		private void InitializeComponent()
		{
			this.timer = new Timer(1000);
			this.timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
			this.timer.Enabled = true;
		}
		#endregion
		
		#region Private Events
		private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{	
			/*
			if (this.Status.Name == PlaybackStatus.Playing.Name)
			{			
				if (this.channel != null && this.currentAudio != null)
				{
					if (this.channel.Position == currentAudio.Milliseconds)
					{
						PlaybackEventArgs args = new PlaybackEventArgs();
					
						if (OnSoundEnd != null)
						{
							OnSoundEnd(this, args);
						}
					}
				}
			}
		
			if (currentSoundIsStreaming) // && this.Status.Name == PlaybackStatus.Playing.Name)
			{
				bool checkForTimeout = false;
			
				if (currentSoundIsCreated)
				{
					if (currentAudio.OpenState == OpenState.Ready && channel == null && !currentAudio.BufferIsStarving)
					{
						if (streamingStatus != currentAudio.OpenState.ToString())
						{
							streamingStatus = currentAudio.OpenState.ToString();
							if (OnStreamingStatusChange != null)
								OnStreamingStatusChange(this, new PlaybackEventArgs(true, null, streamingStatus, null));
						}
							
						soundSystem.PlaySound(ChannelIndex.Free, currentAudio, false, ref channel);
					}
					else if (currentAudio.OpenState == OpenState.Loading)
					{
						if (streamingStatus != currentAudio.OpenState.ToString())
						{
							streamingStatus = currentAudio.OpenState.ToString();
							if (OnStreamingStatusChange != null)
								OnStreamingStatusChange(this, new PlaybackEventArgs(true, null, streamingStatus, null));
						}
					
						checkForTimeout = true;
					}
					else if (currentAudio.OpenState == OpenState.Connecting)
					{
						if (streamingStatus != currentAudio.OpenState.ToString())
						{
							streamingStatus = currentAudio.OpenState.ToString();
							if (OnStreamingStatusChange != null)
								OnStreamingStatusChange(this, new PlaybackEventArgs(true, null, streamingStatus, null));
						}

						checkForTimeout = true;
					}
					
					if (currentAudio.CurrentResult != Result.Ok)
						throw new AlexandriaException("Could not play streaming sound: " + currentAudio.CurrentResult.ToString());
					
					if (soundSystem.CurrentResult != Result.Ok)
						throw new AlexandriaException("Could not play streaming sound: " + soundSystem.CurrentResult.ToString());
				}
				else checkForTimeout = true;
				
				if (checkForTimeout)
				{
					if (soundLoadStart == DateTime.MinValue)
					{
						soundLoadStart = DateTime.Now;
					}
					else
					{
						TimeSpan span = DateTime.Now - soundLoadStart;
						if (span.TotalMilliseconds >= this.SoundLoadTimeout)
						{
							OnSoundLoadTimeout(this, EventArgs.Empty);
							
							if (streamingStatus != "Streaming Timeout")
							{
								streamingStatus = "Streaming Timeout";
								if (OnStreamingStatusChange != null)
									OnStreamingStatusChange(this, new PlaybackEventArgs(false, null, streamingStatus, null));
							}
						}
					}
				}

				if (this.OnPlaybackTimerTick != null)
					OnPlaybackTimerTick(this, e);

				if (channel != null)
				{
					//uint ms = 0;
					//bool playing = false;
					//bool paused = false;
					
					//for (; ; )
					//{						
						//Tag tag = new Tag();						
						//if (currentSound.GetTag(null, -1, ref tag) != Result.Ok)
						//{
						//	break;
						//}
						//if (tag.DataType == TagDataType.String)
						//{
							//textBox.Text = tag.name + " = " + Marshal.PtrToStringAnsi(tag.data) + " (" + tag.datalen + " bytes)";
						//}
						//else
						//{
						//	break;
						//}
					//}
					
					if (soundSystem.CurrentResult != Result.Ok)
						throw new AlexandriaException("Error streaming current sound: " + soundSystem.CurrentResult.ToString());

					//result = channel.getPaused(ref paused);
					//ERRCHECK(result);
					//result = channel.isPlaying(ref playing);
					//ERRCHECK(result);
					//result = channel.getPosition(ref ms, FMOD.TIMEUNIT.MS);
					//ERRCHECK(result);

					//statusBar.Text = "Time " + (ms / 1000 / 60) + ":" + (ms / 1000 % 60) + ":" + (ms / 10 % 100) + (openstate == FMOD.OPENSTATE.BUFFERING ? " Buffering..." : (openstate == FMOD.OPENSTATE.CONNECTING ? " Connecting..." : (paused ? " Paused       " : (playing ? " Playing      " : " Stopped      ")))) + "(" + percentbuffered + "%)" + (starving ? " STARVING" : "        ");
				}			

				if (soundSystem != null)
				{
					soundSystem.Update();					
				}
			}
			*/
		}
		#endregion
		
		#region Internal Properties
		internal SoundSystem SoundSystem
		{
			get {return soundSystem;}
		}
		#endregion
		
		#region Public Properties
		[CLSCompliant(false)]
		public IAudio CurrentAudio
		{
			get {return currentAudio;}
		}
		
		public bool IsMuted
		{
			get
			{
				if (channel != null)
					return this.channel.Mute;				
				else return false;
			}
			set
			{
				if (channel != null)
					this.channel.Mute = value;
			}
		}

		[CLSCompliant(false)]
		public uint Position
		{
			get
			{
				if (channel != null)
					return this.channel.Position;				
				else return 0;
			}
		}
		
		public Channel Channel
		{
			get
			{
				if (channel == null) channel = new Channel();
				
				return channel;
			}
		}
		
		public float Volume
		{
			get
			{
				if (channel != null)
					return this.channel.Volume;
				else return 0f;
			}
			set
			{
				if (channel != null)
					this.channel.Volume = value;
			}
		}
		
		//public override GetAudioInfo GetAudioInfoHandler
		//{
			//get {return new GetAudioInfo(this.GetAudioInfo);}
		//}
		
		public string CurrentResult
		{
			get {return this.soundSystem.CurrentResult.ToString();}
		}
		
		//public System.ComponentModel.ISynchronizeInvoke TimerSynch
		//{
			//get {return timer.SynchronizingObject;}
			//set {timer.SynchronizingObject = value;}
		//}
		
		/*
		public override double PlaybackInterval
		{
			get {return this.timer.Interval;}
			set {this.timer.Interval = value;}
		}*/
		
		/// <summary>
		/// Get or set the size of the stream buffer in raw bytes
		/// </summary>
		[CLSCompliant(false)]
		public uint StreamBufferSize
		{
			get
			{
				soundSystem.StreamBufferUnit = TimeUnits.RawByte;
				return soundSystem.StreamBufferSize;
			}
			set
			{
				soundSystem.StreamBufferUnit = TimeUnits.RawByte;
				soundSystem.StreamBufferSize = value;
			}
		}
		#endregion
		
		#region Public Methods
		/*
		public override void SetCurrentMediaFile(MediaFile mediaFile)
		{
			currentAudio = null;
			currentSoundIsCreated = false;
			currentSoundIsStreaming = false;
			
			streamingStatus = "Not Streaming";
			
			if (OnStreamingStatusChange != null)
				OnStreamingStatusChange(this, new PlaybackEventArgs(true, null, streamingStatus, null));
			
			base.SetCurrentMediaFile(mediaFile);
			if (mediaFile.IsLocal)
			{
				currentSoundIsStreaming = false;
				//currentSound = soundSystem.CreateStream(mediaFile.Path, Modes.None);
				soundSystem.CreateStream(currentAudio, mediaFile.Path, Modes.None);
				//SoundType t = currentSound.Type;
				//Result x = soundSystem.CurrentResult;				
			}
			else
			{
				try
				{
					if (!currentSoundIsCreated)
					{
						// Increase stream buffer size to account for lag
						soundSystem.StreamBufferUnit = TimeUnits.RawByte;
						soundSystem.StreamBufferSize = (64 * 1024);
						//currentSound = soundSystem.CreateSound(mediaFile.Path, (Mode.Hardware | Mode.Fmod2D | Mode.CreateStream | Mode.NonBlocking));
						//currentSound = soundSystem.CreateSound(mediaFile.Path, (Modes.Software | Modes.Fmod2D | Modes.CreateStream | Modes.NonBlocking));
						soundSystem.CreateSound(currentAudio, mediaFile.Path, (Modes.Software | Modes.Fmod2D | Modes.CreateStream | Modes.NonBlocking));

						currentSoundIsCreated = true;
						currentSoundIsStreaming = true;
						
						if (soundSystem.CurrentResult != Result.Ok)
						{
							streamingStatus = "Error: " + soundSystem.CurrentResult.ToString();
							if (OnStreamingStatusChange != null)
								OnStreamingStatusChange(this, new PlaybackEventArgs(false, null, streamingStatus, null));
							throw new AlexandriaException(soundSystem.CurrentResult.ToString());
						}
						else
						{
							streamingStatus = "Started Streaming";
							if (OnStreamingStatusChange != null)
								OnStreamingStatusChange(this, new PlaybackEventArgs(true, null, streamingStatus, null));
						}
					}
				}
				catch (Exception ex)
				{
					throw new AlexandriaException("There was an error loading sound from file: " + mediaFile.Path + "\n" + ex.Message, ex);
				}
			}
			
			if (currentAudio != null)
			{
				currentAudio.MediaFile = mediaFile;
			}
		}
		*/

		[CLSCompliant(false)]
		public void SetPosition(uint position)
		{
			if (channel != null)
				this.channel.Position = position;
			else throw new AlexandriaException("Could not set the position in the current sound because the output channel was not initialized");
		}

		/*
		public AudioInfo GetAudioInfo(MediaFile file)
		{
			AudioInfo info = null;
			if (file != null)
			{
				uint length = 0;
				if (file.IsLocal)
				{
					Sound sound = new Sound(this.SoundSystem, file);
					soundSystem.CreateSound(sound, file.Path, (Modes.Software|Modes.Fmod2D|Modes.OpenOnly|Modes.IgnoreTags));
					sound.LengthUnit = TimeUnits.Millisecond;
					length = sound.Length;
					sound.Dispose();
				}
				info = new AudioInfo(null, length);
			}
			return info;
		}
		*/

		#region Command Methods
		public SoundLoadCommand CreateSoundLoadCommand(Fmod.Sound sound)
		{
			return new SoundLoadCommand(this, sound);
		}
		
		[CLSCompliant(false)]
		public SoundStreamCommand CreateSoundStreamCommand(Fmod.Sound sound, uint streamBufferSize)
		{
			return new SoundStreamCommand(this, sound, streamBufferSize);
		}
		#endregion
		
		#endregion
		
		#region IAudioPlayer Members
		public string CurrentStatus
		{
			get { return null; }
		}
		
		public double PlaybackInterval
		{
			get { return 0L; }
			set { }
		}
		
		public double SoundLoadTimeout
		{
			get { return 0L; }
			set { }
		}
		
		public void Play()
		{
		}
		
		public void Pause()
		{
		}
		
		public void Stop()
		{
		}
		
		public void Seek(bool forward, uint length)
		{
		}
		
		//public void SetStatus(PlaybackStatus status)
		//{
			//this.status = status;
		//}

		public void PlayCurrentSound()
		{
			/*					
			if (currentAudio != null)
			{
				PlaybackEventArgs e = new PlaybackEventArgs();

				if (OnPlay != null)
				{
					OnPlay(this, e);
				}

				if (e.IsValid)
				{
					if (!currentSoundIsStreaming)
					{
						if (channel == null) channel = new Channel();										
				
						if (!channel.Paused)
						{
							try
							{
								Debug.WriteLine("soundSystem.PlaySound");
								// The channel was not already paused so start playing a new stream
								soundSystem.PlaySound(ChannelIndex.Free, currentAudio, false, ref channel);
							}
							catch (Exception ex)
							{
								throw new AlexandriaException(ex);
							}
						}
						else
						{
							// The channel was already paused so we can just resume playback
							channel.Paused = false;
						}
					}
					else
					{
						if (channel != null)
						{
							if (channel.IsPlaying && !channel.Paused)
							{
								channel.Paused = true;
							}
							else
							{
								channel.Paused = false;
							}
						}
					}
				}
			}
			else throw new InvalidOperationException("No sound was defined for the audio player to play");
			*/
		}

		public void PauseCurrentSound()
		{
			/*
			if (currentAudio != null)
			{
				PlaybackEventArgs e = new PlaybackEventArgs();
				if (this.OnPause != null)
				{
					OnPause(this, e);
				}
				
				if (e.IsValid)
				{
					if (channel != null)
					{
						Debug.WriteLine("channel.Pause");
						this.channel.Paused = true;
					}
					else throw new InvalidOperationException("Could not pause the audio player: the output channel is not defined");
				}
			}
			else throw new InvalidOperationException("Could not pause the audio player: the current sound is not defined");
			*/
		}

		public void StopCurrentSound()
		{
			/*
			if (currentAudio != null)
			{
				PlaybackEventArgs e = new PlaybackEventArgs();
				if (OnStop != null)
				{
					OnStop(this, e);
				}
				
				if (e.IsValid)
				{
					if (channel != null)
					{
						Debug.WriteLine("channel.Stop");
						this.channel.Stop();
					}
					else
					{
						//throw new InvalidOperationException("Could not stop the audio player: the output channel is not defined");
					}
				}
			}
			else throw new InvalidOperationException("Could not stop the audio player: the current sound is not defined");
			*/
		}
		#endregion

		#region Stream Save Methods
		
		#region SaveStreamToLocalFile
		/*
		public override void SaveStreamToLocalFile(string sourceFilePath, string destinationFilePath)
		{
			System.Diagnostics.Debug.WriteLine("SaveStreamToLocalFile");
		
			try
			{
				ripSourceFileName = sourceFilePath;
				ripDestinationFileName = destinationFilePath;
			
				// Increase the size of the buffer
				soundSystem.StreamBufferUnit = TimeUnits.RawByte;
				soundSystem.StreamBufferSize = (128 * 1024);

				// Attach the file system callbacks
				soundSystem.UserFileOpenCallback = new FileOpenCallback(OpenRippingStream);
				soundSystem.UserFileCloseCallback = new FileCloseCallback(CloseRippingStream);
				soundSystem.UserFileReadCallback = new FileReadCallback(ReadRippingStream);
				soundSystem.AttachUserFileCallbacks();

				// Create the ripSound
				ripSound = null; //new Sound(this, MediaFile.Load(ripSourceFileName));
				soundSystem.CreateSound(ripSourceFileName, (Modes.Hardware | Modes.Fmod2D | Modes.CreateStream | Modes.NonBlocking), ripSound);
				if (soundSystem.CurrentResult != Result.Ok)
					throw new AlexandriaException("The sound system could not create a new sound: " + soundSystem.CurrentResult.ToString());
				else if (ripSound == null)
					throw new AlexandriaException("The sound was not created successfully: " + soundSystem.CurrentResult.ToString());
				else if (ripSound.CurrentResult != Result.Ok)
					throw new AlexandriaException("The sound was not created successfully: " + ripSound.CurrentResult.ToString());

				// Initialize the ripTimer and start it
				ripTimer.AutoReset = true;				
				ripTimer.Elapsed += new ElapsedEventHandler(ripTimer_Elapsed);
				ripTimer.Enabled = true;
			}
			catch (Exception ex)
			{
				throw new AlexandriaException("There was an error saving this stream to a local file: " + ripSound.CurrentResult.ToString(), ex);
			}
		}
		*/
		#endregion

		#region SaveStreamToLocalFile Events
		private void ripTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{						
			if (ripSound != null)
			{
				System.Diagnostics.Debug.WriteLine("ripTimer_Elapsed OpenState=" + ripSound.OpenState.ToString());	
				if (ripSound.OpenState == OpenState.Ready && ripChannel == null)
				{
					System.Diagnostics.Debug.WriteLine("Playing streaming sound");
				
					soundSystem.PlaySound(ChannelIndex.Free, ripSound, false, ref ripChannel);

					if (ripSound.CurrentResult != Result.Ok)
						throw new AlexandriaException("Unable to read from streaming sound for ripping: " + ripSound.CurrentResult.ToString());
					if (soundSystem.CurrentResult != Result.Ok)
						throw new AlexandriaException("Unable to read from streaming sound for ripping: " + soundSystem.CurrentResult.ToString());
				}
			}

			if (ripChannel != null)
			{
				if (ripSound.NumberOfUpdatedTags != 0)
				{
					for (; ; )
					{
						//Tag tag = ripSound.GetTag(null, -1);

						Tag tag = new Tag();
						if (NativeMethods.FMOD_Sound_GetTag(ripSound.Handle, null, -1, ref tag) != Result.Ok)
						{
							// If no tag was found then stop
							break;
						}

						if (tag.DataType == TagDataType.String)
						{
							FmodSoundFormat testFormat = ripSound.FmodSoundFormat;

							if (tag.Name == "ARTIST")
							{
								if (Marshal.PtrToStringAnsi(tag.Data) != ripTrackArtist)
								{
									ripTrackArtist = Marshal.PtrToStringAnsi(tag.Data);
									ripUpdateFileName = true;
								}
							}
							if (tag.Name == "TITLE")
							{
								if (Marshal.PtrToStringAnsi(tag.Data) != ripTrackTitle)
								{
									ripTrackTitle = Marshal.PtrToStringAnsi(tag.Data);
									ripUpdateFileName = true;
								}
							}
							break;
						}
						else
						{
							break;
						}
					}
				}

				if (ripSound.CurrentResult != Result.Ok || !ripChannel.IsPlaying)
				{
					//System.Diagnostics.Debug.WriteLine("Disposing of ripSound due to invalid CurrentResult or the channel stopped");
					
					ripSound.Dispose();
					ripSound = null;
					ripChannel = null;
				}
				else
				{
					//currentResult = ripChannel.Paused;
					//ERRCHECK(result);
					//result = channel.getPosition(ref ms, FMOD.TIMEUNIT.MS);
					//ERRCHECK(result);
					//statusBar.Text = "Time " + (ms / 1000 / 60) + ":" + (ms / 1000 % 60) + ":" + (ms / 10 % 100) + (paused ? " Paused " : playing ? " Playing" : " Stopped");
				}
			}

			if (ripSound != null)
			{
				if (ripSound.OpenState == OpenState.Error)
				{
					//System.Diagnostics.Debug.WriteLine("Disposing of ripSound due to invalid OpenState");
				
					ripSound.Dispose();
					ripSound = null;
					ripChannel = null;
				}
			}

			if (ripSound == null || ripSound.OpenState == OpenState.Error)
			{
				if (ripSound == null)
					ripSound = new Sound(this.SoundSystem, new Uri(ripSourceFileName));
			
				soundSystem.CreateSound(ripSourceFileName, (Modes.Hardware | Modes.Fmod2D | Modes.CreateStream | Modes.NonBlocking), ripSound);				

				if (soundSystem.CurrentResult != Result.Ok)
					throw new AlexandriaException("An error occurred while ripping the stream, the stream could not be restarted: " + soundSystem.CurrentResult.ToString());
				if (ripSound == null)
					throw new AlexandriaException("Could not restart the sound for ripping: " + soundSystem.CurrentResult.ToString());
			}

			if (soundSystem != null)
			{
				soundSystem.Update();
			}
		}
		#endregion

		#region SaveStreamToLocalFile Callback Methods
		private static Result OpenRippingStream(string name, int unicode, ref uint filelength, ref IntPtr handle, ref IntPtr userData)
		{
			//System.Diagnostics.Debug.WriteLine("OpenRippingStream ripFileName=" + ripDestinationFileName);
		
			try
			{
				ripFileStream = new FileStream(ripDestinationFileName, FileMode.Create, FileAccess.Write);
			}
			catch (Exception ex)
			{
				throw new AlexandriaException("There was an error opening the ripping stream: " + ex.Message, ex);
			}
			return Result.Ok;
		}

		private static Result CloseRippingStream(IntPtr handle, IntPtr userData)
		{
			//System.Diagnostics.Debug.WriteLine("CloseRippingStream ripDestinationFileName=" + ripDestinationFileName == null ? string.Empty : ripDestinationFileName);
		
			try
			{
				// Close the file
				ripFileStream.Close();
			
				// Rename the file if the destination file name has changed
				//if (ripDestinationFileName != ripFileStream.Name)
				//{
					//File.Move(ripFileStream.Name, ripDestinationFileName);
				//}

				// Stop the channel and the timer
				ripChannel.Stop();
				ripTimer.Stop();
			}
			catch (Exception ex)
			{
				throw new AlexandriaException("There was an error closing the ripping stream: " + ex.Message, ex);
			}

			return Result.Ok;
		}

		private static Result ReadRippingStream(IntPtr handle, IntPtr buffer, uint sizeInBytes, ref uint bytesRead, IntPtr userData)
		{
			//System.Diagnostics.Debug.WriteLine("ReadRippingStream sizeInBytes=" + sizeInBytes.ToString() + " timestamp=" + DateTime.Now.ToString());
		
			if (sizeInBytes > 0)
			{
				// Write to the file stream
				byte[] fileBuffer = new byte[(int)sizeInBytes];
				Marshal.Copy(buffer, fileBuffer, 0, (int)sizeInBytes);
				ripFileStream.Write(fileBuffer, 0, (int)sizeInBytes);

				// If the main thread detected a change in the file name
				// update the ripDestinationFileName accordingly
				if (ripUpdateFileName)
				{
					string ext;

					ripUpdateFileName = false;

					switch (ripSoundType)
					{
						case SoundType.Mpeg:       /* MP2/MP3 */
							{
								ext = ".mp3";
								break;
							}
						case SoundType.OggVorbis:  /* Ogg vorbis */
							{
								ext = ".ogg";
								break;
							}
						default:
							{
								ext = ".unknown";
								break;
							}
					}

					ripDestinationFileName = @"C:\" + ripTrackArtist + " - " + ripTrackTitle + ext;
				}
			}
			else
			{				
				CloseRippingStream(IntPtr.Zero, IntPtr.Zero);
			}

			return Result.Ok;
		}
		#endregion
		
		#endregion
	}
}