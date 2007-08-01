namespace Alexandria.Client
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				// Dispose other objects here
							
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.PlayPauseButton = new System.Windows.Forms.Button();
			this.StopButton = new System.Windows.Forms.Button();
			this.PlaybackTrackBar = new System.Windows.Forms.TrackBar();
			this.VolumeTrackBar = new System.Windows.Forms.TrackBar();
			this.MuteButton = new System.Windows.Forms.Button();
			this.FileMenuStrip = new System.Windows.Forms.MenuStrip();
			this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.catalogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.StatusStrip = new System.Windows.Forms.StatusStrip();
			this.LoadStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.PlaybackStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.PositionStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.StreamingStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.FileOpenDialog = new System.Windows.Forms.OpenFileDialog();
			this.NextButton = new System.Windows.Forms.Button();
			this.PlaybackTimer = new System.Windows.Forms.Timer(this.components);
			this.PlaybackGroupBox = new System.Windows.Forms.GroupBox();
			this.PreviousButton = new System.Windows.Forms.Button();
			this.RepeatButton = new System.Windows.Forms.Button();
			this.VolumeDownButton = new System.Windows.Forms.Button();
			this.VolumeUpButton = new System.Windows.Forms.Button();
			this.NowPlayingLabel = new System.Windows.Forms.Label();
			this.NowPlayingTitle = new System.Windows.Forms.Label();
			this.OuterPlaybackQueueSplit = new System.Windows.Forms.SplitContainer();
			this.UpperPlaybackStatusSplit = new System.Windows.Forms.SplitContainer();
			this.TasksGroupBox = new System.Windows.Forms.GroupBox();
			this.QueueGroupBox = new System.Windows.Forms.GroupBox();
			this.QueueListView = new System.Windows.Forms.ListView();
			this.TrackNumberColumn = new System.Windows.Forms.ColumnHeader();
			this.TrackNameColumn = new System.Windows.Forms.ColumnHeader();
			this.TrackArtistColumn = new System.Windows.Forms.ColumnHeader();
			this.TrackAlbumColumn = new System.Windows.Forms.ColumnHeader();
			this.TrackDurationColumn = new System.Windows.Forms.ColumnHeader();
			this.TrackReleaseDateColumn = new System.Windows.Forms.ColumnHeader();
			this.TrackFileColumn = new System.Windows.Forms.ColumnHeader();
			this.TrackFormatColumn = new System.Windows.Forms.ColumnHeader();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.ToolBoxGroupBox = new System.Windows.Forms.GroupBox();
			this.ToolBoxListView = new System.Windows.Forms.ListView();
			this.ToolBoxSmallImageList = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.PlaybackTrackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.VolumeTrackBar)).BeginInit();
			this.FileMenuStrip.SuspendLayout();
			this.StatusStrip.SuspendLayout();
			this.PlaybackGroupBox.SuspendLayout();
			this.OuterPlaybackQueueSplit.Panel1.SuspendLayout();
			this.OuterPlaybackQueueSplit.Panel2.SuspendLayout();
			this.OuterPlaybackQueueSplit.SuspendLayout();
			this.UpperPlaybackStatusSplit.Panel1.SuspendLayout();
			this.UpperPlaybackStatusSplit.Panel2.SuspendLayout();
			this.UpperPlaybackStatusSplit.SuspendLayout();
			this.QueueGroupBox.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.ToolBoxGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// PlayPauseButton
			// 
			this.PlayPauseButton.BackgroundImage = global::Alexandria.Client.Properties.Resources.control_play_blue;
			resources.ApplyResources(this.PlayPauseButton, "PlayPauseButton");
			this.PlayPauseButton.Name = "PlayPauseButton";
			this.PlayPauseButton.UseVisualStyleBackColor = true;
			// 
			// StopButton
			// 
			resources.ApplyResources(this.StopButton, "StopButton");
			this.StopButton.BackgroundImage = global::Alexandria.Client.Properties.Resources.control_stop_blue;
			this.StopButton.Name = "StopButton";
			this.StopButton.UseVisualStyleBackColor = true;
			// 
			// PlaybackTrackBar
			// 
			resources.ApplyResources(this.PlaybackTrackBar, "PlaybackTrackBar");
			this.PlaybackTrackBar.Name = "PlaybackTrackBar";
			this.PlaybackTrackBar.TickFrequency = 10000;
			this.PlaybackTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.PlaybackTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PlaybackTrackBar_MouseUp);
			// 
			// VolumeTrackBar
			// 
			resources.ApplyResources(this.VolumeTrackBar, "VolumeTrackBar");
			this.VolumeTrackBar.Name = "VolumeTrackBar";
			this.VolumeTrackBar.Value = 10;
			// 
			// MuteButton
			// 
			resources.ApplyResources(this.MuteButton, "MuteButton");
			this.MuteButton.BackgroundImage = global::Alexandria.Client.Properties.Resources.sound_mute;
			this.MuteButton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.MuteButton.Name = "MuteButton";
			this.MuteButton.UseVisualStyleBackColor = true;
			// 
			// FileMenuStrip
			// 
			this.FileMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.catalogToolStripMenuItem,
            this.userToolStripMenuItem,
            this.ToolsToolStripMenuItem,
            this.HelpToolStripMenuItem});
			resources.ApplyResources(this.FileMenuStrip, "FileMenuStrip");
			this.FileMenuStrip.Name = "FileMenuStrip";
			// 
			// FileToolStripMenuItem
			// 
			this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.ExitToolStripMenuItem});
			this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
			resources.ApplyResources(this.FileToolStripMenuItem, "FileToolStripMenuItem");
			// 
			// OpenToolStripMenuItem
			// 
			this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
			resources.ApplyResources(this.OpenToolStripMenuItem, "OpenToolStripMenuItem");
			// 
			// ExitToolStripMenuItem
			// 
			this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
			resources.ApplyResources(this.ExitToolStripMenuItem, "ExitToolStripMenuItem");
			// 
			// catalogToolStripMenuItem
			// 
			this.catalogToolStripMenuItem.Name = "catalogToolStripMenuItem";
			resources.ApplyResources(this.catalogToolStripMenuItem, "catalogToolStripMenuItem");
			// 
			// userToolStripMenuItem
			// 
			this.userToolStripMenuItem.Name = "userToolStripMenuItem";
			resources.ApplyResources(this.userToolStripMenuItem, "userToolStripMenuItem");
			// 
			// ToolsToolStripMenuItem
			// 
			this.ToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pluginsToolStripMenuItem});
			this.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem";
			resources.ApplyResources(this.ToolsToolStripMenuItem, "ToolsToolStripMenuItem");
			// 
			// pluginsToolStripMenuItem
			// 
			this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
			resources.ApplyResources(this.pluginsToolStripMenuItem, "pluginsToolStripMenuItem");
			// 
			// HelpToolStripMenuItem
			// 
			this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem});
			this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
			resources.ApplyResources(this.HelpToolStripMenuItem, "HelpToolStripMenuItem");
			// 
			// AboutToolStripMenuItem
			// 
			this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
			resources.ApplyResources(this.AboutToolStripMenuItem, "AboutToolStripMenuItem");
			this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
			// 
			// StatusStrip
			// 
			this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadStatus,
            this.PlaybackStatusLabel,
            this.PositionStatus,
            this.StreamingStatus});
			resources.ApplyResources(this.StatusStrip, "StatusStrip");
			this.StatusStrip.Name = "StatusStrip";
			// 
			// LoadStatus
			// 
			this.LoadStatus.Name = "LoadStatus";
			resources.ApplyResources(this.LoadStatus, "LoadStatus");
			// 
			// PlaybackStatusLabel
			// 
			this.PlaybackStatusLabel.Name = "PlaybackStatusLabel";
			resources.ApplyResources(this.PlaybackStatusLabel, "PlaybackStatusLabel");
			// 
			// PositionStatus
			// 
			this.PositionStatus.Name = "PositionStatus";
			resources.ApplyResources(this.PositionStatus, "PositionStatus");
			// 
			// StreamingStatus
			// 
			this.StreamingStatus.Name = "StreamingStatus";
			resources.ApplyResources(this.StreamingStatus, "StreamingStatus");
			// 
			// NextButton
			// 
			resources.ApplyResources(this.NextButton, "NextButton");
			this.NextButton.BackgroundImage = global::Alexandria.Client.Properties.Resources.control_end_blue;
			this.NextButton.Name = "NextButton";
			this.NextButton.UseVisualStyleBackColor = true;
			// 
			// PlaybackTimer
			// 
			this.PlaybackTimer.Enabled = true;
			this.PlaybackTimer.Interval = 1000;
			this.PlaybackTimer.Tick += new System.EventHandler(this.PlaybackTimer_Tick);
			// 
			// PlaybackGroupBox
			// 
			resources.ApplyResources(this.PlaybackGroupBox, "PlaybackGroupBox");
			this.PlaybackGroupBox.Controls.Add(this.PreviousButton);
			this.PlaybackGroupBox.Controls.Add(this.RepeatButton);
			this.PlaybackGroupBox.Controls.Add(this.VolumeDownButton);
			this.PlaybackGroupBox.Controls.Add(this.VolumeUpButton);
			this.PlaybackGroupBox.Controls.Add(this.NowPlayingLabel);
			this.PlaybackGroupBox.Controls.Add(this.NowPlayingTitle);
			this.PlaybackGroupBox.Controls.Add(this.NextButton);
			this.PlaybackGroupBox.Controls.Add(this.MuteButton);
			this.PlaybackGroupBox.Controls.Add(this.VolumeTrackBar);
			this.PlaybackGroupBox.Controls.Add(this.PlayPauseButton);
			this.PlaybackGroupBox.Controls.Add(this.StopButton);
			this.PlaybackGroupBox.Controls.Add(this.PlaybackTrackBar);
			this.PlaybackGroupBox.Name = "PlaybackGroupBox";
			this.PlaybackGroupBox.TabStop = false;
			// 
			// PreviousButton
			// 
			this.PreviousButton.BackgroundImage = global::Alexandria.Client.Properties.Resources.control_start_blue;
			resources.ApplyResources(this.PreviousButton, "PreviousButton");
			this.PreviousButton.Name = "PreviousButton";
			this.PreviousButton.UseVisualStyleBackColor = true;
			// 
			// RepeatButton
			// 
			this.RepeatButton.BackgroundImage = global::Alexandria.Client.Properties.Resources.control_repeat_blue;
			resources.ApplyResources(this.RepeatButton, "RepeatButton");
			this.RepeatButton.Name = "RepeatButton";
			this.RepeatButton.UseVisualStyleBackColor = true;
			// 
			// VolumeDownButton
			// 
			resources.ApplyResources(this.VolumeDownButton, "VolumeDownButton");
			this.VolumeDownButton.BackgroundImage = global::Alexandria.Client.Properties.Resources.sound_low;
			this.VolumeDownButton.Name = "VolumeDownButton";
			this.VolumeDownButton.UseVisualStyleBackColor = true;
			// 
			// VolumeUpButton
			// 
			resources.ApplyResources(this.VolumeUpButton, "VolumeUpButton");
			this.VolumeUpButton.BackgroundImage = global::Alexandria.Client.Properties.Resources.sound;
			this.VolumeUpButton.Name = "VolumeUpButton";
			this.VolumeUpButton.UseVisualStyleBackColor = true;
			// 
			// NowPlayingLabel
			// 
			this.NowPlayingLabel.BackColor = System.Drawing.SystemColors.ControlLight;
			resources.ApplyResources(this.NowPlayingLabel, "NowPlayingLabel");
			this.NowPlayingLabel.Name = "NowPlayingLabel";
			// 
			// NowPlayingTitle
			// 
			resources.ApplyResources(this.NowPlayingTitle, "NowPlayingTitle");
			this.NowPlayingTitle.Name = "NowPlayingTitle";
			// 
			// OuterPlaybackQueueSplit
			// 
			resources.ApplyResources(this.OuterPlaybackQueueSplit, "OuterPlaybackQueueSplit");
			this.OuterPlaybackQueueSplit.Name = "OuterPlaybackQueueSplit";
			// 
			// OuterPlaybackQueueSplit.Panel1
			// 
			this.OuterPlaybackQueueSplit.Panel1.Controls.Add(this.UpperPlaybackStatusSplit);
			// 
			// OuterPlaybackQueueSplit.Panel2
			// 
			this.OuterPlaybackQueueSplit.Panel2.Controls.Add(this.splitContainer1);
			// 
			// UpperPlaybackStatusSplit
			// 
			resources.ApplyResources(this.UpperPlaybackStatusSplit, "UpperPlaybackStatusSplit");
			this.UpperPlaybackStatusSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.UpperPlaybackStatusSplit.Name = "UpperPlaybackStatusSplit";
			// 
			// UpperPlaybackStatusSplit.Panel1
			// 
			this.UpperPlaybackStatusSplit.Panel1.Controls.Add(this.PlaybackGroupBox);
			// 
			// UpperPlaybackStatusSplit.Panel2
			// 
			this.UpperPlaybackStatusSplit.Panel2.Controls.Add(this.TasksGroupBox);
			// 
			// TasksGroupBox
			// 
			resources.ApplyResources(this.TasksGroupBox, "TasksGroupBox");
			this.TasksGroupBox.Name = "TasksGroupBox";
			this.TasksGroupBox.TabStop = false;
			// 
			// QueueGroupBox
			// 
			resources.ApplyResources(this.QueueGroupBox, "QueueGroupBox");
			this.QueueGroupBox.Controls.Add(this.QueueListView);
			this.QueueGroupBox.Name = "QueueGroupBox";
			this.QueueGroupBox.TabStop = false;
			// 
			// QueueListView
			// 
			this.QueueListView.AllowColumnReorder = true;
			this.QueueListView.AllowDrop = true;
			resources.ApplyResources(this.QueueListView, "QueueListView");
			this.QueueListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TrackNumberColumn,
            this.TrackNameColumn,
            this.TrackArtistColumn,
            this.TrackAlbumColumn,
            this.TrackDurationColumn,
            this.TrackReleaseDateColumn,
            this.TrackFileColumn,
            this.TrackFormatColumn});
			this.QueueListView.FullRowSelect = true;
			this.QueueListView.HideSelection = false;
			this.QueueListView.Name = "QueueListView";
			this.QueueListView.UseCompatibleStateImageBehavior = false;
			this.QueueListView.View = System.Windows.Forms.View.Details;
			this.QueueListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.QueueListView_DragEnter);
			this.QueueListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.QueueListView_DragDrop);
			// 
			// TrackNumberColumn
			// 
			resources.ApplyResources(this.TrackNumberColumn, "TrackNumberColumn");
			// 
			// TrackNameColumn
			// 
			resources.ApplyResources(this.TrackNameColumn, "TrackNameColumn");
			// 
			// TrackArtistColumn
			// 
			resources.ApplyResources(this.TrackArtistColumn, "TrackArtistColumn");
			// 
			// TrackAlbumColumn
			// 
			resources.ApplyResources(this.TrackAlbumColumn, "TrackAlbumColumn");
			// 
			// TrackDurationColumn
			// 
			resources.ApplyResources(this.TrackDurationColumn, "TrackDurationColumn");
			// 
			// TrackReleaseDateColumn
			// 
			resources.ApplyResources(this.TrackReleaseDateColumn, "TrackReleaseDateColumn");
			// 
			// TrackFileColumn
			// 
			resources.ApplyResources(this.TrackFileColumn, "TrackFileColumn");
			// 
			// TrackFormatColumn
			// 
			resources.ApplyResources(this.TrackFormatColumn, "TrackFormatColumn");
			// 
			// splitContainer1
			// 
			resources.ApplyResources(this.splitContainer1, "splitContainer1");
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.ToolBoxGroupBox);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.QueueGroupBox);
			// 
			// ToolBoxGroupBox
			// 
			resources.ApplyResources(this.ToolBoxGroupBox, "ToolBoxGroupBox");
			this.ToolBoxGroupBox.Controls.Add(this.ToolBoxListView);
			this.ToolBoxGroupBox.Name = "ToolBoxGroupBox";
			this.ToolBoxGroupBox.TabStop = false;
			// 
			// ToolBoxListView
			// 
			resources.ApplyResources(this.ToolBoxListView, "ToolBoxListView");
			this.ToolBoxListView.MultiSelect = false;
			this.ToolBoxListView.Name = "ToolBoxListView";
			this.ToolBoxListView.ShowItemToolTips = true;
			this.ToolBoxListView.SmallImageList = this.ToolBoxSmallImageList;
			this.ToolBoxListView.UseCompatibleStateImageBehavior = false;
			this.ToolBoxListView.View = System.Windows.Forms.View.SmallIcon;
			this.ToolBoxListView.DragLeave += new System.EventHandler(this.ToolBoxListView_DragLeave);
			this.ToolBoxListView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ToolBoxListView_ItemDrag);
			this.ToolBoxListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ToolBoxListView_MouseDown);
			// 
			// ToolBoxSmallImageList
			// 
			this.ToolBoxSmallImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ToolBoxSmallImageList.ImageStream")));
			this.ToolBoxSmallImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.ToolBoxSmallImageList.Images.SetKeyName(0, "drive.png");
			this.ToolBoxSmallImageList.Images.SetKeyName(1, "drive_cd.png");
			this.ToolBoxSmallImageList.Images.SetKeyName(2, "drive_cd_empty.png");
			this.ToolBoxSmallImageList.Images.SetKeyName(3, "feed.png");
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.OuterPlaybackQueueSplit);
			this.Controls.Add(this.StatusStrip);
			this.Controls.Add(this.FileMenuStrip);
			this.MainMenuStrip = this.FileMenuStrip;
			this.Name = "MainForm";
			((System.ComponentModel.ISupportInitialize)(this.PlaybackTrackBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.VolumeTrackBar)).EndInit();
			this.FileMenuStrip.ResumeLayout(false);
			this.FileMenuStrip.PerformLayout();
			this.StatusStrip.ResumeLayout(false);
			this.StatusStrip.PerformLayout();
			this.PlaybackGroupBox.ResumeLayout(false);
			this.PlaybackGroupBox.PerformLayout();
			this.OuterPlaybackQueueSplit.Panel1.ResumeLayout(false);
			this.OuterPlaybackQueueSplit.Panel2.ResumeLayout(false);
			this.OuterPlaybackQueueSplit.ResumeLayout(false);
			this.UpperPlaybackStatusSplit.Panel1.ResumeLayout(false);
			this.UpperPlaybackStatusSplit.Panel2.ResumeLayout(false);
			this.UpperPlaybackStatusSplit.ResumeLayout(false);
			this.QueueGroupBox.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ToolBoxGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button PlayPauseButton;
		private System.Windows.Forms.Button StopButton;
		private System.Windows.Forms.TrackBar PlaybackTrackBar;
		private System.Windows.Forms.TrackBar VolumeTrackBar;
		private System.Windows.Forms.Button MuteButton;
		private System.Windows.Forms.MenuStrip FileMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
		private System.Windows.Forms.StatusStrip StatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel PlaybackStatusLabel;
		private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog FileOpenDialog;
		private System.Windows.Forms.ToolStripStatusLabel PositionStatus;
		private System.Windows.Forms.Button NextButton;
		private System.Windows.Forms.Timer PlaybackTimer;
		private System.Windows.Forms.ToolStripStatusLabel StreamingStatus;
		private System.Windows.Forms.ToolStripStatusLabel LoadStatus;
		private System.Windows.Forms.GroupBox PlaybackGroupBox;
		private System.Windows.Forms.SplitContainer OuterPlaybackQueueSplit;
		private System.Windows.Forms.GroupBox QueueGroupBox;
		private System.Windows.Forms.ListView QueueListView;
		private System.Windows.Forms.ColumnHeader TrackNumberColumn;
		private System.Windows.Forms.ColumnHeader TrackNameColumn;
		private System.Windows.Forms.ColumnHeader TrackAlbumColumn;
		private System.Windows.Forms.ColumnHeader TrackArtistColumn;
		private System.Windows.Forms.ColumnHeader TrackDurationColumn;
		private System.Windows.Forms.ColumnHeader TrackReleaseDateColumn;
		private System.Windows.Forms.ColumnHeader TrackFileColumn;
		private System.Windows.Forms.ColumnHeader TrackFormatColumn;
		private System.Windows.Forms.SplitContainer UpperPlaybackStatusSplit;
		private System.Windows.Forms.GroupBox TasksGroupBox;
		private System.Windows.Forms.Label NowPlayingLabel;
		private System.Windows.Forms.Label NowPlayingTitle;
		private System.Windows.Forms.Button VolumeUpButton;
		private System.Windows.Forms.Button VolumeDownButton;
		private System.Windows.Forms.Button RepeatButton;
		private System.Windows.Forms.Button PreviousButton;
		private System.Windows.Forms.ToolStripMenuItem ToolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem catalogToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pluginsToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.GroupBox ToolBoxGroupBox;
		private System.Windows.Forms.ListView ToolBoxListView;
		private System.Windows.Forms.ImageList ToolBoxSmallImageList;
	}
}

