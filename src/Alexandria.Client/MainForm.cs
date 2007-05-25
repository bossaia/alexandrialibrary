using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Alexandria;
using Alexandria.Client.Properties;
using Alexandria.LastFM;

namespace Alexandria.Client
{
	public partial class MainForm : Form
	{
		private ILocalAudio audio;
	
		#region Constructors
		public MainForm()
		{
			try
			{
				InitializeComponent();				
				
				this.PlayPauseButton.Click += new EventHandler(PlayPauseButton_Click);
				this.StopButton.Click += new EventHandler(StopButton_Click);
			}
			catch (AlexandriaException ex)
			{

				MessageBox.Show(ex.Message, Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
			}
		}
		#endregion

		protected override void OnLoad(EventArgs e)
		{
			try
			{
				IAudioscrobblerTrack track = new AudioscrobblerTrack();
				track.AlbumName = "Undertow";
				track.ArtistName = "Tool";
				track.TrackName = "Sober";
				track.MusicBrainzID = "0dfaa81e-9326-4eff-9604-c20d1c613227";
				track.TrackPlayed = DateTime.Now - new TimeSpan(0, 2, 4);
				track.TrackLength = new TimeSpan(0, 5, 6).Milliseconds;
				AudioscrobblerRequest request = new AudioscrobblerRequest();
				request.Username = "uberweasel";
				request.Password = "automatic";
				//request.SubmitTrack(track);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "LastFM error");
			}
						
			base.OnLoad(e);
		}
		
		#region Private Event Methods
		private void PlayPauseButton_Click(object sender, EventArgs e)
		{
 			audio = new Fmod.LocalSound(@"C:\Dev\Testing\Dael.OGG");
 			audio.Load();
 			audio.Play();
		}

		void StopButton_Click(object sender, EventArgs e)
		{
			if (audio != null)
			{
				audio.Stop();
				if (audio is IDisposable)
				{
					IDisposable disposable = audio as IDisposable;
					disposable.Dispose();
					audio = null;
				}
			}
		}
		#endregion
	}
}