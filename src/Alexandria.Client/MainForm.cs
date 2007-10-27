#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using Alexandria.Client.Controllers;
using Alexandria.Client.Properties;

namespace Alexandria.Client
{
	public partial class MainForm : Form
	{
		#region Constructors
		public MainForm()
		{
			try
			{
				InitializeComponent();
				
				this.Resize += new EventHandler(MainForm_Resize);
				this.OpenToolStripMenuItem.Click += new EventHandler(OpenToolStripMenuItem_Click);				
				this.PlayPauseButton.Click += new EventHandler(PlayPauseButton_Click);
				this.StopButton.Click += new EventHandler(StopButton_Click);
				this.NextButton.Click += new EventHandler(NextButton_Click);
				this.PreviousButton.Click += new EventHandler(PreviousButton_Click);
				this.MuteButton.Click += new EventHandler(MuteButton_Click);
				//this.queueDataGrid.SelectionChanged += new EventHandler(queueDataGrid_SelectionChanged);
				//this.QueueListView.SelectedIndexChanged += new EventHandler(QueueListView_SelectedIndexChanged);
				
				//queueController.QueueListView = this.QueueListView;
				queueController.Grid = queueDataGrid;
				queueController.PlaybackController = playbackController;
				queueController.SmallImageList = queueSmallImageList;
				
				playbackController.AudioPlayer.PlayToggles = true;
				playbackController.AudioPlayer.MuteToggles = true;
				playbackController.PlaybackTrackBar = PlaybackTrackBar;
				playbackController.PlayPauseButton = PlayPauseButton;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
			}
		}
		#endregion
				
		#region Private Fields
		private PlaybackController playbackController = new PlaybackController();
		private QueueController queueController = new QueueController();
		private PluginController pluginController = new PluginController();
		
		private NotifyIcon notifyIcon = new NotifyIcon();
		private ContextMenu notifyMenu = new ContextMenu();
		private MenuItem notifyOpenItem;
		private MenuItem notifyPlayItem;
		private MenuItem notifyStopItem;
		private MenuItem notifyMuteItem;
		private MenuItem notifyPrevItem;
		private MenuItem notifyNextItem;
		private MenuItem notifyShowItem;				
		private MenuItem notifyExitItem;
		private FormWindowState oldWindowState = FormWindowState.Normal;
		//private bool seekIsPending;
		
		//private ListViewItem selectedItem;
		//private readonly string tempPath = string.Format("{0}Alexandria{1}", System.IO.Path.GetTempPath(), System.IO.Path.DirectorySeparatorChar);
		//private string dbPath;
		private readonly string dbDir = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Alexandria" + System.IO.Path.DirectorySeparatorChar);
		#endregion
		
		#region Private Methods
		
		#region InitializePlugins
		private void InitializePlugins()
		{
			IList<string> paths = new List<string>();
			foreach(string fileName in ConfigurationManager.AppSettings.AllKeys)
			{
				if (!string.IsNullOrEmpty(fileName))
					paths.Add(fileName); //new Uri(fileName));
			}	
			pluginController.Initialize(paths);
			//InitializePersistence();
		}
		#endregion
		
		#region InitializeInterface
		private void InitializeInterface()
		{
			InitializeNotifyIcon();
			InitializePluginMenu();
			InitializeToolbox();
		}
		#endregion
		
		#region InitializeNotifyIcon
		private void InitializeNotifyIcon()
		{
			Icon icon = new Icon(@"..\..\Alexandria.Client.ico");
			//Icon icon = Alexandria.Client.Properties.Resources.
			//Alexandria.Client.Properties.Resources.ic
			notifyIcon.Icon = icon;
			notifyIcon.Text = "Alexandria";
			notifyIcon.ContextMenu = notifyMenu;
			notifyIcon.Visible = true;
			notifyIcon.Click += new EventHandler(notifyIcon_Click);

			notifyOpenItem = new MenuItem("Open Media", new EventHandler(notifyOpenItem_Click));
			notifyPlayItem = new MenuItem("Play/Pause", new EventHandler(notifyPlayItem_Click), Shortcut.CtrlP);
			notifyStopItem = new MenuItem("Stop", new EventHandler(notifyStopItem_Click), Shortcut.CtrlS);
			notifyPrevItem = new MenuItem("Prev", new EventHandler(notifyPrevItem_Click), Shortcut.CtrlL);
			notifyNextItem = new MenuItem("Next", new EventHandler(notifyNextItem_Click), Shortcut.CtrlN);
			notifyMuteItem = new MenuItem("Mute", new EventHandler(notifyMuteItem_Click), Shortcut.CtrlM);
			notifyShowItem = new MenuItem("Show/Hide", new EventHandler(notifyShowItem_Click));
			notifyExitItem = new MenuItem("Exit", new EventHandler(notifyExitItem_Click), Shortcut.AltF4);

			notifyMenu.MenuItems.Add(notifyOpenItem);
			notifyMenu.MenuItems.Add("-");
			notifyMenu.MenuItems.Add(notifyPlayItem);
			notifyMenu.MenuItems.Add(notifyStopItem);
			notifyMenu.MenuItems.Add(notifyMuteItem);
			notifyMenu.MenuItems.Add("-");
			notifyMenu.MenuItems.Add(notifyPrevItem);
			notifyMenu.MenuItems.Add(notifyNextItem);
			notifyMenu.MenuItems.Add("-");
			notifyMenu.MenuItems.Add(notifyShowItem);
			notifyMenu.MenuItems.Add(notifyExitItem);
		}
		#endregion
		
		#region ShowHideForm
		private void ShowHideForm()
		{
			if (WindowState == FormWindowState.Normal || WindowState == FormWindowState.Maximized)
			{
				oldWindowState = WindowState;
				WindowState = FormWindowState.Minimized;
			}
			else
			{
				Show();
				WindowState = oldWindowState;
			}
		}
		#endregion
		
		#region LoadDefaultUser
		private void LoadDefaultUser()
		{
			/*
			Guid userId = new Guid("FC26A3CC-91DC-4d8b-BC54-F28DAE5BD9D6");
			IUser user = broker.LookupRecord<IUser>(userId);
			if (user != null)
			{
				//TODO: allow the default catalog to be user-defined
				if (user.Catalogs != null && user.Catalogs.Count > 0)
				{
					LoadTracks(user.Catalogs[0].Tracks);
				}
			}
			*/
		}
		#endregion
				
		#region InitializePluginMenu
		private void InitializePluginMenu()
		{
			pluginsToolStripMenuItem.DropDown.Items.Clear();

			IList<PluginInfo> plugins = pluginController.GetPluginInfo();
			foreach (PluginInfo plugin in plugins)
			{
				ToolStripMenuItem item = new ToolStripMenuItem(plugin.Title, (Image)plugin.Bitmap, new EventHandler(pluginConfigItem_Click));
				item.ToolTipText = plugin.Description;
				item.Tag = plugin;
				pluginsToolStripMenuItem.DropDown.Items.Add(item);				
			}
		}		
		#endregion
		
		#region InitializeToolbox
		private void InitializeToolbox()
		{
			ToolBoxListView.Items.Clear();
			foreach(DriveInfo drive in DriveInfo.GetDrives())
			{
				if (drive.DriveType == System.IO.DriveType.Fixed)
				{
					ListViewItem item = new ListViewItem(drive.Name, 0);
					item.ToolTipText = string.Format("{0}/{1}", drive.TotalFreeSpace / 1024, drive.TotalSize / 1024);
					ToolBoxListView.Items.Add(item);
				}
				else if (drive.DriveType == System.IO.DriveType.CDRom)
				{
					int imageIndex = 2;
					string toolTipText = "Not Ready";
					if (drive.IsReady)
					{
						imageIndex = 1;
						toolTipText = drive.VolumeLabel;
					}
					
					ListViewItem item = new ListViewItem(drive.Name, imageIndex);
					item.ToolTipText = toolTipText;
					item.Tag = new TrackSource(drive.Name);
					ToolBoxListView.Items.Add(item);
				}
			}
		}
		#endregion
		
		#region GetVolume
		private float GetVolume()
		{
			return Convert.ToSingle(VolumeTrackBar.Value * .1);
		}
		#endregion

		#endregion

		#region Private Event Methods
		void MainForm_Resize(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Minimized)
				Hide();
		}
		
		private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult result = FileOpenDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				queueController.OpenFile(FileOpenDialog.FileName);
			}
		}

		private void PlayPauseButton_Click(object sender, EventArgs e)
		{
			queueController.SelectTrack();
			playbackController.AudioPlayer.Play();
		}

		private void StopButton_Click(object sender, EventArgs e)
		{
			playbackController.AudioPlayer.Stop();
			/*
			if (controller != null)
			{
				Stop();
				PlaybackTrackBar.Value = 0;
				PlaybackTrackBar.Enabled = false;
				PlayPauseButton.BackgroundImage = Alexandria.Client.Properties.Resources.control_play_blue;
			}
			*/
		}

		private void MuteButton_Click(object sender, EventArgs e)
		{
			playbackController.AudioPlayer.Mute();
			/*
			if (controller != null)
			{
				Mute();
				if (IsMuted)
				{
					VolumeTrackBar.Enabled = false;
					MuteButton.BackgroundImage = Alexandria.Client.Properties.Resources.sound;
				}
				else
				{
					VolumeTrackBar.Enabled = true;
					MuteButton.BackgroundImage = Alexandria.Client.Properties.Resources.sound_mute;
				}
			}
			*/
		}

		private void VolumeTrackBar_ValueChanged(object sender, EventArgs e)
		{
			playbackController.AudioPlayer.SetVolume(GetVolume());
		
			//if (controller != null)
				//Volume = GetVolume();
		}

		private void PreviousButton_Click(object sender, EventArgs e)
		{
			queueController.Previous();
		}

		private void NextButton_Click(object sender, EventArgs e)
		{
			queueController.Next();
		}

		private void QueueListView_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void notifyIcon_Click(object sender, EventArgs e)
		{
			MouseEventArgs m = e as MouseEventArgs;
			if (m != null)
			{
				if (m.Button == MouseButtons.Left)
					ShowHideForm();
				else if (m.Button == MouseButtons.Right)
				{
				}				
				else if (m.Button == MouseButtons.Middle)
				{
				}
			}
		}

		private void notifyOpenItem_Click(object sender, EventArgs e)
		{
			OpenToolStripMenuItem_Click(sender, e);
		}

		private void notifyPlayItem_Click(object sender, EventArgs e)
		{
			PlayPauseButton_Click(sender, e);
		}

		private void notifyStopItem_Click(object sender, EventArgs e)
		{
			StopButton_Click(sender, e);
		}

		private void notifyNextItem_Click(object sender, EventArgs e)
		{
			NextButton_Click(sender, e);
		}

		private void notifyPrevItem_Click(object sender, EventArgs e)
		{
			PreviousButton_Click(sender, e);
		}

		private void notifyMuteItem_Click(object sender, EventArgs e)
		{
			notifyMuteItem.Checked = !notifyMuteItem.Checked;
			MuteButton_Click(sender, e);
		}

		private void notifyShowItem_Click(object sender, EventArgs e)
		{
			ShowHideForm();
		}

		private void notifyExitItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			About about = new About();
			about.Initialize(pluginController);
			about.ShowDialog(this);
		}
		
		private void pluginConfigItem_Click(object sender, EventArgs e)
		{
			PluginConfiguration config = new PluginConfiguration();
			ToolStripItem item = (ToolStripItem)sender;
			//config.PluginInfo = (PluginInfo)item.Tag;
			config.ShowDialog();
		}

		private void PlaybackTimer_Tick(object sender, EventArgs e)
		{
			playbackController.RefreshPlaybackStates();
		}

		void queueDataGrid_SelectionChanged(object sender, EventArgs e)
		{
			//throw new Exception("The method or operation is not implemented.");
		}
		
		private void OnSelectedTrackStart(object sender, EventArgs e)
		{
			/*
			if (controller != null && AudioStream != null)
			{
				if (SelectedTrack != null)
					NowPlayingLabel.Text = string.Format("{0} - {1}", SelectedTrack.Artist, SelectedTrack.Name);
				
				Volume = GetVolume();
				VolumeTrackBar.Enabled = true;

				PlaybackTrackBar.Enabled = true;
				PlaybackTrackBar.Minimum = 0;
				PlaybackTrackBar.Maximum = Convert.ToInt32(AudioStream.Duration.TotalSeconds);
				PlaybackTrackBar.Value = 0;
			}
			*/
		}
		
		private void OnCurrentAudioStreamEnded(object sender, EventArgs e)
		{
			//This is needed to avoid a momentary flicker
			PlaybackTrackBar.SuspendLayout();
			
			playbackController.AudioPlayer.Stop();
			queueController.Next();
			playbackController.AudioPlayer.Play();
			
			PlaybackTrackBar.ResumeLayout();
		}
		
		private void OnSelectedTrackEnd(object sender, EventArgs e)
		{
			queueController.Next();
			playbackController.AudioPlayer.Play();
			
			//if (controller != null)
			//{
				//Next();
				//Play();
			//}
		}

		private void PlaybackTrackBar_MouseDown(object sender, MouseEventArgs e)
		{
			playbackController.AudioPlayer.BeginSeek();
		}

		private void PlaybackTrackBar_MouseUp(object sender, MouseEventArgs e)
		{
			playbackController.AudioPlayer.Seek(PlaybackTrackBar.Value);
		}

		private void ToolBoxListView_MouseDown(object sender, MouseEventArgs e)
		{
			//if (ToolBoxListView.SelectedItems != null && ToolBoxListView.SelectedItems.Count > 0)
			//{
				//if (ToolBoxListView.SelectedItems[0].Tag != null && ToolBoxListView.SelectedItems[0].Tag is ITrackSource)
				//{
					//ToolBoxListView.DoDragDrop(ToolBoxListView.SelectedItems[0].Tag, DragDropEffects.Copy);
				//}
			//}
			//else
			//{
				//select the item based on e.X, e.Y then use the above logic
				ListViewItem item = ToolBoxListView.GetItemAt(e.X, e.Y);
				if (item != null && item.Tag != null && item.Tag is ITrackSource)
				{
					item.Selected = true;
					ToolBoxListView.DoDragDrop(item.Tag, DragDropEffects.Copy);
				}
			//}
		}

		private void ToolBoxListView_ItemDrag(object sender, ItemDragEventArgs e)
		{
			//object x = e.Item;
		}

		private void ToolBoxListView_DragLeave(object sender, EventArgs e)
		{
		}

		private void QueueListView_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.None;
			
			if (e.Data != null)
			{
				object source = e.Data.GetData(typeof(TrackSource));
				if (source != null)
					e.Effect = DragDropEffects.Copy;
			}
		}

		private void QueueListView_DragDrop(object sender, DragEventArgs e)
		{
			queueController.LoadData(e.Data);
		}

		private void ToolBoxContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem.Text == "Refresh")
			{
				InitializeToolbox();
			}
		}

		private void QueueListView_ItemActivate(object sender, EventArgs e)
		{
			queueController.SelectTrack();
			playbackController.AudioPlayer.Play();
		}
		#endregion
		
		#region Protected Overrides
		
		#region OnLoad
		protected override void OnLoad(EventArgs e)
		{			
			base.OnLoad(e);
			
			PlaybackTimer.Start();
			
			InitializePlugins();
			InitializeInterface();
			
			LoadDefaultUser();
			
			playbackController.AudioPlayer.CurrentAudioStreamEnded += new EventHandler<EventArgs>(OnCurrentAudioStreamEnded);
			
			//queueController.TrackStart += new EventHandler<EventArgs>(OnSelectedTrackStart);
			//queueController.TrackEnd += new EventHandler<EventArgs>(OnSelectedTrackEnd);
		}
		#endregion

		#region OnClosing
		protected override void OnClosing(CancelEventArgs e)
		{
			if (playbackController != null)
			{
				playbackController.Dispose();
				playbackController = null;
			}
			
			base.OnClosing(e);
		}
		#endregion
		
		#endregion		
	}
}