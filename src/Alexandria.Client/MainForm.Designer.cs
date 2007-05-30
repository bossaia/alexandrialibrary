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
			this.VolumeLabel = new System.Windows.Forms.Label();
			this.FileMenuStrip = new System.Windows.Forms.MenuStrip();
			this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.StatusStrip = new System.Windows.Forms.StatusStrip();
			this.LoadStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.PlaybackStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.PositionStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.StreamingStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.FileOpenDialog = new System.Windows.Forms.OpenFileDialog();
			this.NextButton = new System.Windows.Forms.Button();
			this.PlaybackTimer = new System.Windows.Forms.Timer(this.components);
			this.PlaybackGroupBox = new System.Windows.Forms.GroupBox();
			this.NowPlayingLabel = new System.Windows.Forms.Label();
			this.NowPlayingTitle = new System.Windows.Forms.Label();
			this.PreviousButton = new System.Windows.Forms.Button();
			this.InnerArtistAlbumSplit = new System.Windows.Forms.SplitContainer();
			this.ArtistGroupBox = new System.Windows.Forms.GroupBox();
			this.ArtistListView = new System.Windows.Forms.ListView();
			this.ArtistNameColumn = new System.Windows.Forms.ColumnHeader();
			this.ArtistTypeColumn = new System.Windows.Forms.ColumnHeader();
			this.AlbumGroupBox = new System.Windows.Forms.GroupBox();
			this.AlbumListView = new System.Windows.Forms.ListView();
			this.AlbumNameColumn = new System.Windows.Forms.ColumnHeader();
			this.AlbumReleaseDateColumn = new System.Windows.Forms.ColumnHeader();
			this.AlbumTypeColumn = new System.Windows.Forms.ColumnHeader();
			this.OuterPlaybackQueueSplit = new System.Windows.Forms.SplitContainer();
			this.UpperPlaybackStatusSplit = new System.Windows.Forms.SplitContainer();
			this.DetailGroupBox = new System.Windows.Forms.GroupBox();
			this.DetailTabs = new System.Windows.Forms.TabControl();
			this.ImageTab = new System.Windows.Forms.TabPage();
			this.ImageExifLabel = new System.Windows.Forms.Label();
			this.ImageCaption3 = new System.Windows.Forms.Label();
			this.ImageCaption2 = new System.Windows.Forms.Label();
			this.ImageCaption1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.DetailPicture3 = new System.Windows.Forms.PictureBox();
			this.DetailPicture2 = new System.Windows.Forms.PictureBox();
			this.DetailPicture1 = new System.Windows.Forms.PictureBox();
			this.BioTab = new System.Windows.Forms.TabPage();
			this.BioAuthorLabel = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.GenreTab = new System.Windows.Forms.TabPage();
			this.GenreListView = new System.Windows.Forms.ListView();
			this.AddGenreButton = new System.Windows.Forms.Button();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.StyleListView = new System.Windows.Forms.ListView();
			this.AddStyleButton = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.MoodListView = new System.Windows.Forms.ListView();
			this.button3 = new System.Windows.Forms.Button();
			this.MidFilterQueueSplit = new System.Windows.Forms.SplitContainer();
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
			((System.ComponentModel.ISupportInitialize)(this.PlaybackTrackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.VolumeTrackBar)).BeginInit();
			this.FileMenuStrip.SuspendLayout();
			this.StatusStrip.SuspendLayout();
			this.PlaybackGroupBox.SuspendLayout();
			this.InnerArtistAlbumSplit.Panel1.SuspendLayout();
			this.InnerArtistAlbumSplit.Panel2.SuspendLayout();
			this.InnerArtistAlbumSplit.SuspendLayout();
			this.ArtistGroupBox.SuspendLayout();
			this.AlbumGroupBox.SuspendLayout();
			this.OuterPlaybackQueueSplit.Panel1.SuspendLayout();
			this.OuterPlaybackQueueSplit.Panel2.SuspendLayout();
			this.OuterPlaybackQueueSplit.SuspendLayout();
			this.UpperPlaybackStatusSplit.Panel1.SuspendLayout();
			this.UpperPlaybackStatusSplit.Panel2.SuspendLayout();
			this.UpperPlaybackStatusSplit.SuspendLayout();
			this.DetailGroupBox.SuspendLayout();
			this.DetailTabs.SuspendLayout();
			this.ImageTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DetailPicture3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DetailPicture2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DetailPicture1)).BeginInit();
			this.BioTab.SuspendLayout();
			this.GenreTab.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.MidFilterQueueSplit.Panel1.SuspendLayout();
			this.MidFilterQueueSplit.Panel2.SuspendLayout();
			this.MidFilterQueueSplit.SuspendLayout();
			this.QueueGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// PlayPauseButton
			// 			
			resources.ApplyResources(this.PlayPauseButton, "PlayPauseButton");
			this.PlayPauseButton.Name = "PlayPauseButton";
			this.PlayPauseButton.UseVisualStyleBackColor = true;
			// 
			// StopButton
			// 
			resources.ApplyResources(this.StopButton, "StopButton");
			this.StopButton.Name = "StopButton";
			this.StopButton.UseVisualStyleBackColor = true;
			// 
			// PlaybackTrackBar
			// 
			resources.ApplyResources(this.PlaybackTrackBar, "PlaybackTrackBar");
			this.PlaybackTrackBar.Name = "PlaybackTrackBar";
			this.PlaybackTrackBar.TickFrequency = 10000;
			this.PlaybackTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
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
			this.MuteButton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.MuteButton.Name = "MuteButton";
			this.MuteButton.UseVisualStyleBackColor = true;
			// 
			// VolumeLabel
			// 
			resources.ApplyResources(this.VolumeLabel, "VolumeLabel");
			this.VolumeLabel.Name = "VolumeLabel";
			// 
			// FileMenuStrip
			// 
			this.FileMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem});
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
			// FileOpenDialog
			// 
			this.FileOpenDialog.FileName = "openFileDialog1";
			// 
			// NextButton
			// 
			resources.ApplyResources(this.NextButton, "NextButton");
			this.NextButton.Name = "NextButton";
			this.NextButton.UseVisualStyleBackColor = true;
			// 
			// PlaybackTimer
			// 
			this.PlaybackTimer.Enabled = true;
			this.PlaybackTimer.Interval = 1000;
			// 
			// PlaybackGroupBox
			// 
			resources.ApplyResources(this.PlaybackGroupBox, "PlaybackGroupBox");
			this.PlaybackGroupBox.Controls.Add(this.NowPlayingLabel);
			this.PlaybackGroupBox.Controls.Add(this.NowPlayingTitle);
			this.PlaybackGroupBox.Controls.Add(this.PreviousButton);
			this.PlaybackGroupBox.Controls.Add(this.NextButton);
			this.PlaybackGroupBox.Controls.Add(this.MuteButton);
			this.PlaybackGroupBox.Controls.Add(this.VolumeLabel);
			this.PlaybackGroupBox.Controls.Add(this.VolumeTrackBar);
			this.PlaybackGroupBox.Controls.Add(this.PlayPauseButton);
			this.PlaybackGroupBox.Controls.Add(this.StopButton);
			this.PlaybackGroupBox.Controls.Add(this.PlaybackTrackBar);
			this.PlaybackGroupBox.Name = "PlaybackGroupBox";
			this.PlaybackGroupBox.TabStop = false;
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
			// PreviousButton
			//
			resources.ApplyResources(this.PreviousButton, "PreviousButton");
			this.PreviousButton.Name = "PreviousButton";
			this.PreviousButton.UseVisualStyleBackColor = true;
			// 
			// InnerArtistAlbumSplit
			// 
			resources.ApplyResources(this.InnerArtistAlbumSplit, "InnerArtistAlbumSplit");
			this.InnerArtistAlbumSplit.Name = "InnerArtistAlbumSplit";
			// 
			// InnerArtistAlbumSplit.Panel1
			// 
			this.InnerArtistAlbumSplit.Panel1.Controls.Add(this.ArtistGroupBox);
			// 
			// InnerArtistAlbumSplit.Panel2
			// 
			this.InnerArtistAlbumSplit.Panel2.Controls.Add(this.AlbumGroupBox);
			// 
			// ArtistGroupBox
			// 
			resources.ApplyResources(this.ArtistGroupBox, "ArtistGroupBox");
			this.ArtistGroupBox.Controls.Add(this.ArtistListView);
			this.ArtistGroupBox.Name = "ArtistGroupBox";
			this.ArtistGroupBox.TabStop = false;
			// 
			// ArtistListView
			// 
			resources.ApplyResources(this.ArtistListView, "ArtistListView");
			this.ArtistListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ArtistNameColumn,
            this.ArtistTypeColumn});
			this.ArtistListView.Name = "ArtistListView";
			this.ArtistListView.UseCompatibleStateImageBehavior = false;
			this.ArtistListView.View = System.Windows.Forms.View.Details;
			// 
			// ArtistNameColumn
			// 
			resources.ApplyResources(this.ArtistNameColumn, "ArtistNameColumn");
			// 
			// ArtistTypeColumn
			// 
			resources.ApplyResources(this.ArtistTypeColumn, "ArtistTypeColumn");
			// 
			// AlbumGroupBox
			// 
			resources.ApplyResources(this.AlbumGroupBox, "AlbumGroupBox");
			this.AlbumGroupBox.Controls.Add(this.AlbumListView);
			this.AlbumGroupBox.Name = "AlbumGroupBox";
			this.AlbumGroupBox.TabStop = false;
			// 
			// AlbumListView
			// 
			resources.ApplyResources(this.AlbumListView, "AlbumListView");
			this.AlbumListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.AlbumNameColumn,
            this.AlbumReleaseDateColumn,
            this.AlbumTypeColumn});
			this.AlbumListView.Name = "AlbumListView";
			this.AlbumListView.UseCompatibleStateImageBehavior = false;
			this.AlbumListView.View = System.Windows.Forms.View.Details;
			// 
			// AlbumNameColumn
			// 
			resources.ApplyResources(this.AlbumNameColumn, "AlbumNameColumn");
			// 
			// AlbumReleaseDateColumn
			// 
			resources.ApplyResources(this.AlbumReleaseDateColumn, "AlbumReleaseDateColumn");
			// 
			// AlbumTypeColumn
			// 
			resources.ApplyResources(this.AlbumTypeColumn, "AlbumTypeColumn");
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
			this.OuterPlaybackQueueSplit.Panel2.Controls.Add(this.MidFilterQueueSplit);
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
			this.UpperPlaybackStatusSplit.Panel2.Controls.Add(this.DetailGroupBox);
			// 
			// DetailGroupBox
			// 
			resources.ApplyResources(this.DetailGroupBox, "DetailGroupBox");
			this.DetailGroupBox.Controls.Add(this.DetailTabs);
			this.DetailGroupBox.Name = "DetailGroupBox";
			this.DetailGroupBox.TabStop = false;
			// 
			// DetailTabs
			// 
			resources.ApplyResources(this.DetailTabs, "DetailTabs");
			this.DetailTabs.Controls.Add(this.ImageTab);
			this.DetailTabs.Controls.Add(this.BioTab);
			this.DetailTabs.Controls.Add(this.GenreTab);
			this.DetailTabs.Controls.Add(this.tabPage1);
			this.DetailTabs.Controls.Add(this.tabPage2);
			this.DetailTabs.Name = "DetailTabs";
			this.DetailTabs.SelectedIndex = 0;
			// 
			// ImageTab
			// 
			this.ImageTab.Controls.Add(this.ImageExifLabel);
			this.ImageTab.Controls.Add(this.ImageCaption3);
			this.ImageTab.Controls.Add(this.ImageCaption2);
			this.ImageTab.Controls.Add(this.ImageCaption1);
			this.ImageTab.Controls.Add(this.button1);
			this.ImageTab.Controls.Add(this.DetailPicture3);
			this.ImageTab.Controls.Add(this.DetailPicture2);
			this.ImageTab.Controls.Add(this.DetailPicture1);
			resources.ApplyResources(this.ImageTab, "ImageTab");
			this.ImageTab.Name = "ImageTab";
			this.ImageTab.UseVisualStyleBackColor = true;
			// 
			// ImageExifLabel
			// 
			this.ImageExifLabel.BackColor = System.Drawing.Color.LightGray;
			resources.ApplyResources(this.ImageExifLabel, "ImageExifLabel");
			this.ImageExifLabel.Name = "ImageExifLabel";
			// 
			// ImageCaption3
			// 
			resources.ApplyResources(this.ImageCaption3, "ImageCaption3");
			this.ImageCaption3.Name = "ImageCaption3";
			// 
			// ImageCaption2
			// 
			resources.ApplyResources(this.ImageCaption2, "ImageCaption2");
			this.ImageCaption2.Name = "ImageCaption2";
			// 
			// ImageCaption1
			// 
			resources.ApplyResources(this.ImageCaption1, "ImageCaption1");
			this.ImageCaption1.Name = "ImageCaption1";
			// 
			// button1
			// 
			resources.ApplyResources(this.button1, "button1");
			this.button1.Name = "button1";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// DetailPicture3
			// 
			resources.ApplyResources(this.DetailPicture3, "DetailPicture3");
			this.DetailPicture3.BackColor = System.Drawing.Color.DarkGray;
			this.DetailPicture3.Name = "DetailPicture3";
			this.DetailPicture3.TabStop = false;
			// 
			// DetailPicture2
			// 
			resources.ApplyResources(this.DetailPicture2, "DetailPicture2");
			this.DetailPicture2.BackColor = System.Drawing.Color.DarkGray;
			this.DetailPicture2.Name = "DetailPicture2";
			this.DetailPicture2.TabStop = false;
			// 
			// DetailPicture1
			// 
			resources.ApplyResources(this.DetailPicture1, "DetailPicture1");
			this.DetailPicture1.BackColor = System.Drawing.Color.Transparent;
			this.DetailPicture1.Name = "DetailPicture1";
			this.DetailPicture1.TabStop = false;
			// 
			// BioTab
			// 
			this.BioTab.Controls.Add(this.BioAuthorLabel);
			this.BioTab.Controls.Add(this.button2);
			this.BioTab.Controls.Add(this.richTextBox1);
			resources.ApplyResources(this.BioTab, "BioTab");
			this.BioTab.Name = "BioTab";
			this.BioTab.UseVisualStyleBackColor = true;
			// 
			// BioAuthorLabel
			// 
			this.BioAuthorLabel.BackColor = System.Drawing.Color.LightGray;
			resources.ApplyResources(this.BioAuthorLabel, "BioAuthorLabel");
			this.BioAuthorLabel.Name = "BioAuthorLabel";
			// 
			// button2
			// 
			resources.ApplyResources(this.button2, "button2");
			this.button2.Name = "button2";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// richTextBox1
			// 
			resources.ApplyResources(this.richTextBox1, "richTextBox1");
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			// 
			// GenreTab
			// 
			this.GenreTab.Controls.Add(this.GenreListView);
			this.GenreTab.Controls.Add(this.AddGenreButton);
			resources.ApplyResources(this.GenreTab, "GenreTab");
			this.GenreTab.Name = "GenreTab";
			this.GenreTab.UseVisualStyleBackColor = true;
			// 
			// GenreListView
			// 
			resources.ApplyResources(this.GenreListView, "GenreListView");
			this.GenreListView.Name = "GenreListView";
			this.GenreListView.UseCompatibleStateImageBehavior = false;
			this.GenreListView.View = System.Windows.Forms.View.Details;
			// 
			// AddGenreButton
			// 
			resources.ApplyResources(this.AddGenreButton, "AddGenreButton");
			this.AddGenreButton.Name = "AddGenreButton";
			this.AddGenreButton.UseVisualStyleBackColor = true;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.StyleListView);
			this.tabPage1.Controls.Add(this.AddStyleButton);
			resources.ApplyResources(this.tabPage1, "tabPage1");
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// StyleListView
			// 
			resources.ApplyResources(this.StyleListView, "StyleListView");
			this.StyleListView.Name = "StyleListView";
			this.StyleListView.UseCompatibleStateImageBehavior = false;
			this.StyleListView.View = System.Windows.Forms.View.Details;
			// 
			// AddStyleButton
			// 
			resources.ApplyResources(this.AddStyleButton, "AddStyleButton");
			this.AddStyleButton.Name = "AddStyleButton";
			this.AddStyleButton.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.MoodListView);
			this.tabPage2.Controls.Add(this.button3);
			resources.ApplyResources(this.tabPage2, "tabPage2");
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// MoodListView
			// 
			resources.ApplyResources(this.MoodListView, "MoodListView");
			this.MoodListView.Name = "MoodListView";
			this.MoodListView.UseCompatibleStateImageBehavior = false;
			this.MoodListView.View = System.Windows.Forms.View.Details;
			// 
			// button3
			// 
			resources.ApplyResources(this.button3, "button3");
			this.button3.Name = "button3";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// MidFilterQueueSplit
			// 
			resources.ApplyResources(this.MidFilterQueueSplit, "MidFilterQueueSplit");
			this.MidFilterQueueSplit.Name = "MidFilterQueueSplit";
			// 
			// MidFilterQueueSplit.Panel1
			// 
			this.MidFilterQueueSplit.Panel1.Controls.Add(this.InnerArtistAlbumSplit);
			// 
			// MidFilterQueueSplit.Panel2
			// 
			this.MidFilterQueueSplit.Panel2.Controls.Add(this.QueueGroupBox);
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
			this.QueueListView.Name = "QueueListView";
			this.QueueListView.UseCompatibleStateImageBehavior = false;
			this.QueueListView.View = System.Windows.Forms.View.Details;
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
			this.InnerArtistAlbumSplit.Panel1.ResumeLayout(false);
			this.InnerArtistAlbumSplit.Panel2.ResumeLayout(false);
			this.InnerArtistAlbumSplit.ResumeLayout(false);
			this.ArtistGroupBox.ResumeLayout(false);
			this.AlbumGroupBox.ResumeLayout(false);
			this.OuterPlaybackQueueSplit.Panel1.ResumeLayout(false);
			this.OuterPlaybackQueueSplit.Panel2.ResumeLayout(false);
			this.OuterPlaybackQueueSplit.ResumeLayout(false);
			this.UpperPlaybackStatusSplit.Panel1.ResumeLayout(false);
			this.UpperPlaybackStatusSplit.Panel2.ResumeLayout(false);
			this.UpperPlaybackStatusSplit.ResumeLayout(false);
			this.DetailGroupBox.ResumeLayout(false);
			this.DetailTabs.ResumeLayout(false);
			this.ImageTab.ResumeLayout(false);
			this.ImageTab.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DetailPicture3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DetailPicture2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DetailPicture1)).EndInit();
			this.BioTab.ResumeLayout(false);
			this.GenreTab.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.MidFilterQueueSplit.Panel1.ResumeLayout(false);
			this.MidFilterQueueSplit.Panel2.ResumeLayout(false);
			this.MidFilterQueueSplit.ResumeLayout(false);
			this.QueueGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button PlayPauseButton;
		private System.Windows.Forms.Button StopButton;
		private System.Windows.Forms.TrackBar PlaybackTrackBar;
		private System.Windows.Forms.TrackBar VolumeTrackBar;
		private System.Windows.Forms.Button MuteButton;
		private System.Windows.Forms.Label VolumeLabel;
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
		private System.Windows.Forms.SplitContainer InnerArtistAlbumSplit;
		private System.Windows.Forms.SplitContainer OuterPlaybackQueueSplit;
		private System.Windows.Forms.SplitContainer MidFilterQueueSplit;
		private System.Windows.Forms.Button PreviousButton;
		private System.Windows.Forms.GroupBox ArtistGroupBox;
		private System.Windows.Forms.ListView ArtistListView;
		private System.Windows.Forms.GroupBox AlbumGroupBox;
		private System.Windows.Forms.ListView AlbumListView;
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
		private System.Windows.Forms.ColumnHeader ArtistNameColumn;
		private System.Windows.Forms.ColumnHeader ArtistTypeColumn;
		private System.Windows.Forms.ColumnHeader AlbumNameColumn;
		private System.Windows.Forms.ColumnHeader AlbumReleaseDateColumn;
		private System.Windows.Forms.ColumnHeader AlbumTypeColumn;
		private System.Windows.Forms.SplitContainer UpperPlaybackStatusSplit;
		private System.Windows.Forms.GroupBox DetailGroupBox;
		private System.Windows.Forms.TabControl DetailTabs;
		private System.Windows.Forms.TabPage ImageTab;
		private System.Windows.Forms.TabPage BioTab;
		private System.Windows.Forms.PictureBox DetailPicture3;
		private System.Windows.Forms.PictureBox DetailPicture2;
		private System.Windows.Forms.PictureBox DetailPicture1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Label ImageCaption1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TabPage GenreTab;
		private System.Windows.Forms.Button AddGenreButton;
		private System.Windows.Forms.Label ImageExifLabel;
		private System.Windows.Forms.Label ImageCaption3;
		private System.Windows.Forms.Label ImageCaption2;
		private System.Windows.Forms.Label BioAuthorLabel;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ListView GenreListView;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.ListView StyleListView;
		private System.Windows.Forms.Button AddStyleButton;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.ListView MoodListView;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label NowPlayingLabel;
		private System.Windows.Forms.Label NowPlayingTitle;
	}
}

