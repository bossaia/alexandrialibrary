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

using Telesophy.Alexandria.Clients.Ankh.Controllers;
using Telesophy.Alexandria.Clients.Ankh.Properties;
using Telesophy.Alexandria.Clients.Ankh.Views;

namespace Telesophy.Alexandria.Clients.Ankh
{
	public partial class MainForm : Form
	{
		#region Constructors
		public MainForm()
		{
			try
			{
				InitializeComponent();
				CustomInitialize();
				
				this.Resize += new EventHandler(MainForm_Resize);
				this.OpenToolStripMenuItem.Click += new EventHandler(OpenToolStripMenuItem_Click);				
				this.PlayPauseButton.Click += new EventHandler(PlayPauseButton_Click);
				this.StopButton.Click += new EventHandler(StopButton_Click);
				this.NextButton.Click += new EventHandler(NextButton_Click);
				this.PreviousButton.Click += new EventHandler(PreviousButton_Click);
				this.MuteButton.Click += new EventHandler(MuteButton_Click);

				queueController.Grid = queueDataGrid;
				queueController.SortListView = sortListView;
				queueController.PlaybackController = playbackController;
				queueController.PersistenceController = persistenceController;
				queueController.TaskController = taskController;
				queueController.SmallImageList = queueSmallImageList;
				
				playbackController.PlayToggles = true;
				playbackController.MuteToggles = true;
				playbackController.PlaybackTrackBar = PlaybackTrackBar;
				playbackController.PlayPauseButton = PlayPauseButton;
				playbackController.QueueController = queueController;
				playbackController.WireStatusUpdated(new EventHandler<PlaybackEventArgs>(OnStatusUpdated));
				
				persistenceController.Initialize();
				
				taskController.Grid = taskDataGrid;
				taskController.QueueController = queueController;
				taskController.PersistenceController = persistenceController;
				
				queueController.LoadDefaultCatalog();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
			}
		}
		#endregion
		
		#region Private Constants
		private const string KEY_OPEN_DIR_ROOT = "OpenDirectoryRoot";
		private const int MAX_SORT_COLUMNS = 5;
		private const string DEFAULT_COLUMN_FILTER = "Any";
		private const string FILTER_LINK_AND = "And";
		private const string FILTER_LINK_OR = "Or";
		private const string FILTER_LINK_NOT = "Not";
		
		private const string FILTER_OP_LI = "~";
		private const string FILTER_OP_NE = "<>";
		private const string FILTER_OP_GE = ">=";
		private const string FILTER_OP_GT = ">";
		private const string FILTER_OP_LE = "<=";
		private const string FILTER_OP_LT = "<";
		private const string FILTER_OP_EQ = "=";
		private readonly string[] FILTER_OPERATORS = new string[]{FILTER_OP_LI, FILTER_OP_NE, FILTER_OP_GE, FILTER_OP_GT, FILTER_OP_LE, FILTER_OP_LT, FILTER_OP_EQ};
		#endregion
		
		#region Private Fields
		private PlaybackController playbackController = new PlaybackController();
		private QueueController queueController = new QueueController();
		private PersistenceController persistenceController = new PersistenceController();
		private PluginController pluginController = new PluginController();
		private TaskController taskController = new TaskController();
		
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

		#region Protected Overrides

		#region OnLoad
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			PlaybackTimer.Start();

			//InitializeTaskMenu();
			InitializePlugins();
			InitializeInterface();

			LoadDefaultUser();

			playbackController.WireCurrentAudioSteamEnded(new EventHandler<EventArgs>(OnCurrentAudioStreamEnded));
			queueController.WireSelectedTrackChanged(new EventHandler<QueueEventArgs>(OnSelectedTrackChanged));
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
		
		#region Private Methods
		
		#region CustomInitialize
		private void CustomInitialize()
		{
			InitializeTaskMenu();
		}
		#endregion
		
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
			notifyIcon.Icon = Resources.AnkhIcon;
			notifyIcon.Text = Resources.ApplicationTitle;
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

		#region InitializeTaskMenu
		private void InitializeTaskMenu()
		{
			//for(int i=0; i<taskContextMenuStrip.Items.Count; i++)
				//tasksToolStripMenuItem.DropDownItems.Add(taskContextMenuStrip.Items[i]);
		}
		#endregion
				
		#region InitializePluginMenu
		private void InitializePluginMenu()
		{
			//pluginsToolStripMenuItem.DropDown.Items.Clear();

			//IList<PluginInfo> plugins = pluginController.GetPluginInfo();
			//foreach (PluginInfo plugin in plugins)
			//{
			//    ToolStripMenuItem item = new ToolStripMenuItem(plugin.Title, (Image)plugin.Bitmap, new EventHandler(pluginConfigItem_Click));
			//    item.ToolTipText = plugin.Description;
			//    item.Tag = plugin;
			//    pluginsToolStripMenuItem.DropDown.Items.Add(item);				
			//}
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

		#region ImportUpdate
		private
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

		private void OpenDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ConfigurationManager.AppSettings[KEY_OPEN_DIR_ROOT] != null)
			{
				string rootFolder = ConfigurationManager.AppSettings[KEY_OPEN_DIR_ROOT].ToString();
				if (System.IO.Directory.Exists(rootFolder))
					DirectoryOpenDialog.SelectedPath = rootFolder;
			}
		
			DialogResult result = DirectoryOpenDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				queueController.OpenDirectory(DirectoryOpenDialog.SelectedPath);
			}
		}

		private void PlayPauseButton_Click(object sender, EventArgs e)
		{
			queueController.LoadSelectedRow();
			playbackController.Play();
		}

		private void StopButton_Click(object sender, EventArgs e)
		{		
			playbackController.Stop();
		}

		private void MuteButton_Click(object sender, EventArgs e)
		{
			playbackController.Mute();
		}

		private void VolumeTrackBar_ValueChanged(object sender, EventArgs e)
		{
			playbackController.SetVolume(GetVolume());
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
			
			playbackController.Stop();
			queueController.Next();
			playbackController.Play();
			
			PlaybackTrackBar.ResumeLayout();
		}
		
		private void OnSelectedTrackEnd(object sender, EventArgs e)
		{
			queueController.Next();
			playbackController.Play();
		}

		private void PlaybackTrackBar_MouseDown(object sender, MouseEventArgs e)
		{
			playbackController.BeginSeek();
		}

		private void PlaybackTrackBar_MouseUp(object sender, MouseEventArgs e)
		{
			playbackController.Seek(PlaybackTrackBar.Value);
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

		//private void QueueListView_ItemActivate(object sender, EventArgs e)
		//{
			//queueDataGrid.Rows[e].Selected = true;
			//queueController.LoadSelectedRow();
			//playbackController.AudioPlayer.Play();
		//}

		private void queueDataGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			string sortName = queueDataGrid.Columns[e.ColumnIndex].Name;
			bool sortExists = false;
			
			if (sortListView.Items.Count > 0)
			{
				foreach(ListViewItem item in sortListView.Items)
				{
					if (item.Text == sortName)
					{
						sortExists = true;
						if (item.ImageIndex == 0)
							item.ImageIndex = 1;
						else if (item.ImageIndex == 1)
							item.Remove();
					}
				}
			}
			
			if (!sortExists)
			{
				if (sortListView.Items.Count == MAX_SORT_COLUMNS)
					sortListView.Items[0].Remove();
				
				sortListView.Items.Add(sortName, 0);
			}
			
			sortButton_Click(this, EventArgs.Empty);
		}

		private void clearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			playbackController.Stop();
			queueController.Clear();
		}

		private void clearSelectedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			queueController.ClearSelectedRows();
			//if (queueDataGrid.Rows != null && queueDataGrid.Rows.Count > 0)
			//{
				//foreach(DataGridViewRow row in queueDataGrid.
				//queueController.ClearRow(queueDataGrid.SelectedRows[0].Index);
			//}
		}

		private void queueDataGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			queueController.SelectRow(e.RowIndex);
		}
		
		private void OnSelectedTrackChanged(object sender, QueueEventArgs e)
		{
			if (queueController.SelectedTrack != null)
			{
				string artist = (!string.IsNullOrEmpty(queueController.SelectedTrack.Artist)) ? queueController.SelectedTrack.Artist : "Unknown Artist";
				string title = (!string.IsNullOrEmpty(queueController.SelectedTrack.Title)) ? queueController.SelectedTrack.Title : "Untitled";
				NowPlayingLabel.Text = string.Format("{0} - {1}", artist, title);
			}
			else NowPlayingLabel.Text = string.Empty;
		}

		private void queueDataGrid_KeyUp(object sender, KeyEventArgs e)
		{
			//46 is the DEL key
			if (e.KeyValue == 46 && queueDataGrid.SelectedRows != null && queueDataGrid.SelectedRows.Count > 0)
			{
				queueController.ClearRow(queueDataGrid.SelectedRows[0].Index);
			}
		}
		
		private void OnStatusUpdated(object sender, PlaybackEventArgs e)
		{
			//currentStatusToolStripLabel.Text = string.Format("{0} ({1})", e.Status, e.Description);
			
			queueController.SelectedRowStatus = e.PlaybackState.ToString();
		}

		private void queueDataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			string x = e.Exception.Message;
		}

		private void submitCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			playbackController.EnableSubmitTracksToLastFM = submitCheckBox.Checked;
		}

		private void saveSelectedCatalogToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (queueDataGrid.SelectedRows != null && queueDataGrid.SelectedRows.Count > 0)
				{
					queueController.SaveRow(queueDataGrid.SelectedRows[0].Index);
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "ERROR: Could not save catalog entry");
			}
		}

		private void deleteSelectedCatalogToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (queueDataGrid.SelectedRows != null && queueDataGrid.SelectedRows.Count > 0)
				{
					queueController.DeleteRow(queueDataGrid.SelectedRows[0].Index);
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "ERROR: Could not delete catalog entry");
			}
		}

		private void loadCatalogToolStripMenuItem_Click(object sender, EventArgs e)
		{
			queueController.LoadDefaultCatalog();
		}

		private void importCatalogToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ConfigurationManager.AppSettings[KEY_OPEN_DIR_ROOT] != null)
			{
				string rootFolder = ConfigurationManager.AppSettings[KEY_OPEN_DIR_ROOT].ToString();
				if (System.IO.Directory.Exists(rootFolder))
					DirectoryOpenDialog.SelectedPath = rootFolder;
			}

			DialogResult result = DirectoryOpenDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				ImportStatusUpdateDelegate updateCallback = new ImportStatusUpdateDelegate(importStatusUpdated);
				taskController.BeginImportDirectory(DirectoryOpenDialog.SelectedPath, updateCallback);
			}
		}
		
		private void importStatusUpdated(object sender, ImportStatusUpdateEventArgs args)
		{
			if (InvokeRequired)
			{
				ImportStatusUpdateDelegate updateDelegate = new ImportStatusUpdateDelegate(importStatusUpdated);
				Invoke(updateDelegate, new object[]{sender, args});
			}
			else
			{
				currentStatusToolStripLabel.Text = string.Format("Importing: [{0}/{1}/{2}] {3}", args.ImportCount, args.ScanCount, args.ErrorCount, args.Path);
				if (args.Completed)
				{
					MessageBox.Show(string.Format("Files Imported: {0}\nFiles Scanned: {1}\nErrors: {2}\nTime Elapsed: {3}", args.ImportCount, args.ScanCount, args.ErrorCount, args.CompletedTime), "IMPORT COMPLETED");
				}
			}
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}
		
		#region Sort Methods
		private void sortButton_Click(object sender, EventArgs e)
		{
			DoSort();
		}

		private void sortListView_ItemActivate(object sender, EventArgs e)
		{
			if (sortListView.SelectedItems != null && sortListView.SelectedItems.Count > 0)
			{
				int currentIndex = sortListView.SelectedItems[0].ImageIndex;
				int newIndex = (currentIndex == 0) ? 1 : 0;
				sortListView.SelectedItems[0].ImageIndex = newIndex;
				DoSort();
			}
		}

		private void sortListView_ItemDrag(object sender, ItemDragEventArgs e)
		{
			ListViewItem item = e.Item as ListViewItem;
			if (item != null)
				DoDragDrop(new SortDragDropData(item), DragDropEffects.Move);
		}

		private void sortListView_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(SortDragDropData)))
				e.Effect = DragDropEffects.Move;
			else e.Effect = DragDropEffects.None;
		}

		private void sortListView_DragDrop(object sender, DragEventArgs e)
		{
			SortDragDropData sortData = e.Data.GetData(typeof(SortDragDropData)) as SortDragDropData;
			if (sortData != null)
			{
				int index = 0;
				
				if (sortListView.Items.Count > 0)
				{
					Point dropPoint = sortListView.PointToClient(new Point(e.X, e.Y));
					
					for(int i=0;i<sortListView.Items.Count;i++)
					{
						Point itemPoint = sortListView.Items[i].Position;
						if (itemPoint.X + 15 < dropPoint.X)
						{
							index = sortListView.Items[i].Index+1;
						}
						else break;
					}
				}
								
				int imageIndex = (sortData.Direction == ListSortDirection.Ascending) ? 0 : 1;
				sortListView.Items.Insert(index, sortData.ColumnName, imageIndex);
				sortListView.Items[index].Selected = true;

				if (sortData.Item != null)
				{
					sortListView.Items.Remove(sortData.Item);
				}
				
				DoSort();
			}
		}

		private void sortListView_KeyUp(object sender, KeyEventArgs e)
		{
			if (sortListView.SelectedItems != null && sortListView.SelectedItems.Count > 0)
			{
				if (e.KeyCode == Keys.Delete)
				{
					sortListView.Items.Remove(sortListView.SelectedItems[0]);
					DoSort();		
				}
			}
		}

		private void sortContextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			bool enableClear = false;
			bool enableClearAll = false;
			
			if (sortListView.Items.Count > 0)
			{
				enableClearAll = true;
				if (sortListView.SelectedItems != null && sortListView.SelectedItems.Count > 0)
					enableClear = true;
			}
			
			sortContextMenuStrip.Items[0].Enabled = enableClear;
			sortContextMenuStrip.Items[1].Enabled = enableClearAll;
		}

		private void sortContextMenuStripItemClearSelected_Click(object sender, EventArgs e)
		{
			ClearSelectedSort();
		}
		
		private void sortContextMenuStripItemClearAll_Click(object sender, EventArgs e)
		{
			ClearAllSorts();
		}

		private void clearSelectedSortButton_Click(object sender, EventArgs e)
		{
			ClearSelectedSort();
		}

		private void clearAllSortButton_Click(object sender, EventArgs e)
		{
			ClearAllSorts();
		}
		
		private void ClearSelectedSort()
		{
			if (sortListView.Items.Count > 0)
			{
				if (sortListView.SelectedItems != null && sortListView.SelectedItems.Count > 0)
				{
					sortListView.Items.Remove(sortListView.SelectedItems[0]);
					DoSort();
				}
			}
		}
		
		private void ClearAllSorts()
		{
			if (sortListView.Items.Count > 0)
			{
				sortListView.Items.Clear();
				DoSort();
			}
		}
		
		private void DoSort()
		{
			if (sortListView.Items.Count > 0)
			{
				IDictionary<string, bool> columns = new Dictionary<string, bool>();
				foreach (ListViewItem item in sortListView.Items)
				{
					bool ascending = (item.ImageIndex == 0);
					columns.Add(item.Text, ascending);
				}

				queueController.Sort(columns);
			}
			else
			{
				queueController.RemoveSort();
			}		
		}
		#endregion

		#region Filter Methods
		private void filterButton_Click(object sender, EventArgs e)
		{
			DoFilter();
		}

		private void notFilterButton_Click(object sender, EventArgs e)
		{
			filterListView.Items.Add("Not");
		}

		private void andFilterButton_Click(object sender, EventArgs e)
		{
			filterListView.Items.Add("And");
		}

		private void orFilterButton_Click(object sender, EventArgs e)
		{
			filterListView.Items.Add("Or");
		}

		private void filterContextMenuItemAddFilter_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				bool filterIsValid = false;				
				string column = DEFAULT_COLUMN_FILTER;
				string value = filterContextMenuItemAddFilter.Text;
				string op = string.Empty;
				int opIndex = 0;
				int opLength = 1;
				
				for(int i=0; i<FILTER_OPERATORS.Length; i++)
				{
					if (value.Contains(FILTER_OPERATORS[i]))
					{
						op = FILTER_OPERATORS[i];
						opIndex = value.IndexOf(FILTER_OPERATORS[i]);
						opLength = FILTER_OPERATORS[i].Length;
						column = value.Substring(0, opIndex);
						value = value.Substring(opIndex + opLength, value.Length - opIndex - opLength);
						
						if (queueDataGrid.Columns.Contains(column))
						{
							filterIsValid = true;
							filterContextMenuStrip.Close();
						}
						else MessageBox.Show(string.Format("{0} is not a valid column", column), "Invalid Filter");
						break;
					}
				} 

				if (filterIsValid)
				{
					FilterDragDropData filterData = new FilterDragDropData(column, op, value);
					FilterInfo filterInfo = filterData.GetFilterInfo();
					
					ListViewItem item = new ListViewItem(filterInfo.GetDescription());
					item.Tag = filterInfo;
					item.ImageIndex = filterData.ImageIndex;
					item.ToolTipText = filterInfo.ToString();
					
					filterListView.Items.Add(item);

					DoFilter();
				}
			}
		}

		private void filterContextMenuItemClearAll_Click(object sender, EventArgs e)
		{
			if (filterListView.Items.Count > 0)
			{
				filterListView.Items.Clear();
				
				//DoFilter();
			}
		}

		private void filterListView_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete && filterListView.SelectedItems.Count > 0)
			{
				filterListView.Items.Remove(filterListView.SelectedItems[0]);
				
				//DoFilter();
			}
		}

		private void filterListView_ItemDrag(object sender, ItemDragEventArgs e)
		{
			ListViewItem item = e.Item as ListViewItem;
			if (item != null)
			{
				DoDragDrop(new FilterDragDropData(item), DragDropEffects.Move);
			}
		}

		private void filterListView_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(FilterDragDropData)))
				e.Effect = DragDropEffects.Move;
			else e.Effect = DragDropEffects.None;
		}

		private void filterListView_DragDrop(object sender, DragEventArgs e)
		{
			FilterDragDropData filterData = e.Data.GetData(typeof(FilterDragDropData)) as FilterDragDropData;
			if (filterData != null)
			{
				int index = 0;

				if (filterListView.Items.Count > 0)
				{
					Point dropPoint = filterListView.PointToClient(new Point(e.X, e.Y));

					for (int i = 0; i < filterListView.Items.Count; i++)
					{
						Point itemPoint = filterListView.Items[i].Position;
						if (itemPoint.X + 15 < dropPoint.X)
						{
							index = filterListView.Items[i].Index + 1;
						}
						else break;
					}
				}

				//int imageIndex = (sortData.Direction == ListSortDirection.Ascending) ? 0 : 1;
				//int imageIndex = filterData.Item.ImageIndex;
				
				FilterInfo filterInfo = new FilterInfo(filterData.Column, filterData.Operator, filterData.Value);
				ListViewItem item = new ListViewItem(filterInfo.GetDescription(), filterData.ImageIndex);
				item.Tag = filterInfo;
				item.ToolTipText = filterInfo.ToString();
				
				filterListView.Items.Insert(index, item);
				filterListView.Items[index].Selected = true;

				if (filterData.Item != null)
				{
					filterListView.Items.Remove(filterData.Item);
				}

				//DoFilter();
			}
		}
		
		private void DoFilter()
		{
			try
			{
				if (filterListView.Items.Count > 0)
				{
					StringBuilder filter = new StringBuilder();

					bool negateNextFilter = false;
					bool andNextFilter = false;
					bool orNextFilter = false;

					for (int i = 0; i < filterListView.Items.Count; i++)
					{
						if (filterListView.Items[i].Tag != null)
						{
							FilterInfo filterInfo = (FilterInfo)filterListView.Items[i].Tag;
							if (filterInfo.Column != DEFAULT_COLUMN_FILTER)
							{
								if (negateNextFilter)
								{
									filterInfo = filterInfo.Negate();
									negateNextFilter = false;
								}
								if (orNextFilter)
								{
									filter.AppendFormat(" {0} ", FILTER_LINK_OR);
									orNextFilter = false;
								}
								if (andNextFilter)
								{
									filter.AppendFormat(" {0} ", FILTER_LINK_AND);
									andNextFilter = false;
								}

								filter.Append(filterInfo.ToString());
							}
						}
						else
						{
							switch (filterListView.Items[i].Text)
							{
								case FILTER_LINK_NOT:
									negateNextFilter = true;
									break;
								case FILTER_LINK_AND:
									andNextFilter = true;
									break;
								case FILTER_LINK_OR:
									orNextFilter = true;
									break;
								default:
									break;
							}
						}
					}

					queueController.Filter(filter.ToString());
				}
				else
				{
					queueController.LoadDefaultCatalog();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("There was an error trying to apply this filter:\n{0}", ex.Message), "Filter Error");
			}		
		}
		#endregion

		#region Task Methods
		private void RunTask()
		{
			taskController.RunSelectedTask();
		}
				
		private void PauseTask()
		{
			taskController.PauseSelectedTask();
		}

		private void CancelTask()
		{
			if (taskDataGrid.SelectedRows != null && taskDataGrid.SelectedRows.Count > 0)
			{
				string taskName = taskDataGrid.SelectedRows[0].Cells[0].Value.ToString();
				if (MessageBox.Show(string.Format("Are you sure that you want to cancel the '{0}' task?", taskName), "Cancel Task", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					taskController.CancelSelectedTask();
				}
			}
		}
		
		private void CancelAllTasks()
		{
			taskController.CancelAllTasks();
		}
	
		private void taskContextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			bool cancelAllEnabled = false;
			bool cancelEnabled = false;
			bool pauseEnabled = false;
		
			if (taskDataGrid.Rows != null && taskDataGrid.Rows.Count > 0)
			{
				cancelAllEnabled = true;
			}
		
			if (taskDataGrid.SelectedRows != null && taskDataGrid.SelectedRows.Count > 0)
			{
				cancelEnabled = true;
				pauseEnabled = true;
			}
			
			taskCancelMenuItem.Enabled = cancelEnabled;
			taskCancelAllMenuItem.Enabled = cancelAllEnabled;
			taskPauseMenuItem.Enabled = pauseEnabled;
		}

		private void taskRunMenuItem_Click(object sender, EventArgs e)
		{
			RunTask();
		}
				
		private void taskPauseMenuItem_Click(object sender, EventArgs e)
		{
			PauseTask();
		}

		private void taskCancelMenuItem_Click(object sender, EventArgs e)
		{
			CancelTask();
		}

		private void taskCancelAllMenuItem_Click(object sender, EventArgs e)
		{
			CancelAllTasks();
		}

		private void runTaskToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RunTask();
		}

		private void pauseTaskToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PauseTask();
		}

		private void cancelTaskToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CancelTask();
		}

		private void cancelAllTasksToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CancelAllTasks();
		}
		#endregion

		#endregion
	}
}