using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	[System.CLSCompliant(false)]
	public interface IAudioPlayer
	{
		IAudioResource CurrentAudio{get;}
		string CurrentStatus{get;}
		bool IsMuted{get;set;}
		//void SetCurrentMediaFile(MediaFile mediaFile);		
		uint Position{get;}
		float Volume{get;set;}
		string CurrentResult{get;}
		double PlaybackInterval{get;set;}
		double SoundLoadTimeout{get;set;}
		//System.ComponentModel.ISynchronizeInvoke TimerSynch{get;set;}
		void Play();
		void Pause();
		void Stop();
		void Dispose();
		void SetPosition(uint position);
		void Seek(bool isForward, uint distance);
		void SetStatus(PlaybackStatus status);
		void PlayCurrentSound();
		void PauseCurrentSound();
		void StopCurrentSound();
		//EventHandler<PlaybackEventArgs> OnPlay {get;set;}
		//EventHandler<PlaybackEventArgs> OnPause {get;set;}
		//EventHandler<PlaybackEventArgs> OnStop {get;set;}
		//EventHandler<System.Timers.ElapsedEventArgs> OnPlaybackTimerTick{get;set;}
		//EventHandler<PlaybackEventArgs> OnSoundEnd {get;set;}
		//EventHandler<System.EventArgs> OnSoundLoadTimeout {get;set;}
		//EventHandler<PlaybackEventArgs> OnPlaybackStatusChange {get;set;}
		//EventHandler<PlaybackEventArgs> OnStreamingStatusChange {get;set;}
		//EventHandler<PlaybackEventArgs> OnRippingStatusChange {get;set;}
		//GetAudioInfo GetAudioInfoHandler {get;}
		//void SaveStreamToLocalFile(string sourceFilePath, string destinationFilePath);
	}
}
