using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Media.IO
{
	public interface IAudioPlayer : IDisposable
	{
		IAudioStreamFactory AudioStreamFactory { get; set; }
		IAudioStream CurrentAudioStream { get; }
		TimeSpan Duration { get; }
		TimeSpan Elapsed { get; }
		bool IsMuted { get; }
		bool MuteToggles { get; set; }
		bool PlayToggles { get; set; }
		bool SeekIsPending { get; }
		float Volume { get; }
		EventHandler<MediaStateChangedEventArgs> BufferStateChanged { get; set; }
		EventHandler<EventArgs> CurrentAudioStreamChanged { get; set; }
		EventHandler<MediaStateChangedEventArgs> NetworkStateChanged { get; set; }
		EventHandler<MediaStateChangedEventArgs> PlaybackStateChanged { get; set; }
		EventHandler<MediaStateChangedEventArgs> SeekStateChanged { get; set; }
		EventHandler<AudioStateChangedEventArgs> VolumeChanged { get; set; }
		void BeginSeek();
		void CancelSeek();
		void LoadAudioStream(Uri path);
		void LoadAudioStream(IAudioStream audioStream);
		void Mute();
		void Pause();
		void Play();
		void RefreshPlayerStates();
		void Resume();
		void Seek(int position);
		void SetVolume(float volume);
		void Stop();
		void Unmute();
	}
}
