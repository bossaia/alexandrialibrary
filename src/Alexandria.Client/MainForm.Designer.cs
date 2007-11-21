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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.PlayPauseButton = new System.Windows.Forms.Button();
			this.StopButton = new System.Windows.Forms.Button();
			this.PlaybackTrackBar = new System.Windows.Forms.TrackBar();
			this.VolumeTrackBar = new System.Windows.Forms.TrackBar();
			this.MuteButton = new System.Windows.Forms.Button();
			this.FileMenuStrip = new System.Windows.Forms.MenuStrip();
			this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.catalogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
			this.NowPlayingLabel = new System.Windows.Forms.Label();
			this.NowPlayingTitle = new System.Windows.Forms.Label();
			this.OuterPlaybackQueueSplit = new System.Windows.Forms.SplitContainer();
			this.UpperPlaybackStatusSplit = new System.Windows.Forms.SplitContainer();
			this.TasksGroupBox = new System.Windows.Forms.GroupBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.ToolBoxGroupBox = new System.Windows.Forms.GroupBox();
			this.ToolBoxListView = new System.Windows.Forms.ListView();
			this.ToolBoxContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ToolBoxContextMenuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolBoxSmallImageList = new System.Windows.Forms.ImageList(this.components);
			this.QueueGroupBox = new System.Windows.Forms.GroupBox();
			this.sortListView = new System.Windows.Forms.ListView();
			this.sortContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.contextCoolStripMenuItemClear = new System.Windows.Forms.ToolStripMenuItem();
			this.sortLabel = new System.Windows.Forms.Label();
			this.queueDataGrid = new System.Windows.Forms.DataGridView();
			this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Type = new System.Windows.Forms.DataGridViewImageColumn();
			this.Source = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Artist = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Album = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Format = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.queueSmallImageList = new System.Windows.Forms.ImageList(this.components);
			this.DirectoryOpenDialog = new System.Windows.Forms.FolderBrowserDialog();
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
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.ToolBoxGroupBox.SuspendLayout();
			this.ToolBoxContextMenuStrip.SuspendLayout();
			this.QueueGroupBox.SuspendLayout();
			this.sortContextMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.queueDataGrid)).BeginInit();
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
			this.StopButton.BackgroundImage = global::Alexandria.Client.Properties.Resources.control_stop_blue;
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
			this.PlaybackTrackBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlaybackTrackBar_MouseDown);
			this.PlaybackTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PlaybackTrackBar_MouseUp);
			// 
			// VolumeTrackBar
			// 
			resources.ApplyResources(this.VolumeTrackBar, "VolumeTrackBar");
			this.VolumeTrackBar.Name = "VolumeTrackBar";
			this.VolumeTrackBar.Value = 10;
			this.VolumeTrackBar.ValueChanged += new System.EventHandler(this.VolumeTrackBar_ValueChanged);
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
            this.viewToolStripMenuItem,
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
            this.OpenDirectoryToolStripMenuItem,
            this.ExitToolStripMenuItem});
			this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
			resources.ApplyResources(this.FileToolStripMenuItem, "FileToolStripMenuItem");
			// 
			// OpenToolStripMenuItem
			// 
			this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
			resources.ApplyResources(this.OpenToolStripMenuItem, "OpenToolStripMenuItem");
			// 
			// OpenDirectoryToolStripMenuItem
			// 
			this.OpenDirectoryToolStripMenuItem.Name = "OpenDirectoryToolStripMenuItem";
			resources.ApplyResources(this.OpenDirectoryToolStripMenuItem, "OpenDirectoryToolStripMenuItem");
			this.OpenDirectoryToolStripMenuItem.Click += new System.EventHandler(this.OpenDirectoryToolStripMenuItem_Click);
			// 
			// ExitToolStripMenuItem
			// 
			this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
			resources.ApplyResources(this.ExitToolStripMenuItem, "ExitToolStripMenuItem");
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
			// 
			// clearToolStripMenuItem
			// 
			this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
			resources.ApplyResources(this.clearToolStripMenuItem, "clearToolStripMenuItem");
			this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
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
            this.pluginsToolStripMenuItem,
            this.toolManagerToolStripMenuItem});
			this.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem";
			resources.ApplyResources(this.ToolsToolStripMenuItem, "ToolsToolStripMenuItem");
			// 
			// pluginsToolStripMenuItem
			// 
			this.pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
			resources.ApplyResources(this.pluginsToolStripMenuItem, "pluginsToolStripMenuItem");
			// 
			// toolManagerToolStripMenuItem
			// 
			this.toolManagerToolStripMenuItem.Name = "toolManagerToolStripMenuItem";
			resources.ApplyResources(this.toolManagerToolStripMenuItem, "toolManagerToolStripMenuItem");
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
			this.NextButton.BackgroundImage = global::Alexandria.Client.Properties.Resources.control_end_blue;
			resources.ApplyResources(this.NextButton, "NextButton");
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
			// NowPlayingLabel
			// 
			resources.ApplyResources(this.NowPlayingLabel, "NowPlayingLabel");
			this.NowPlayingLabel.BackColor = System.Drawing.SystemColors.ControlLight;
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
			this.ToolBoxListView.ContextMenuStrip = this.ToolBoxContextMenuStrip;
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
			// ToolBoxContextMenuStrip
			// 
			this.ToolBoxContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolBoxContextMenuItemRefresh});
			this.ToolBoxContextMenuStrip.Name = "ToolBoxContextMenuStrip";
			resources.ApplyResources(this.ToolBoxContextMenuStrip, "ToolBoxContextMenuStrip");
			this.ToolBoxContextMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ToolBoxContextMenuStrip_ItemClicked);
			// 
			// ToolBoxContextMenuItemRefresh
			// 
			this.ToolBoxContextMenuItemRefresh.Name = "ToolBoxContextMenuItemRefresh";
			resources.ApplyResources(this.ToolBoxContextMenuItemRefresh, "ToolBoxContextMenuItemRefresh");
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
			// QueueGroupBox
			// 
			resources.ApplyResources(this.QueueGroupBox, "QueueGroupBox");
			this.QueueGroupBox.Controls.Add(this.sortListView);
			this.QueueGroupBox.Controls.Add(this.sortLabel);
			this.QueueGroupBox.Controls.Add(this.queueDataGrid);
			this.QueueGroupBox.Name = "QueueGroupBox";
			this.QueueGroupBox.TabStop = false;
			// 
			// sortListView
			// 
			this.sortListView.ContextMenuStrip = this.sortContextMenuStrip;
			resources.ApplyResources(this.sortListView, "sortListView");
			this.sortListView.MultiSelect = false;
			this.sortListView.Name = "sortListView";
			this.sortListView.UseCompatibleStateImageBehavior = false;
			this.sortListView.View = System.Windows.Forms.View.List;
			// 
			// sortContextMenuStrip
			// 
			this.sortContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextCoolStripMenuItemClear});
			this.sortContextMenuStrip.Name = "sortContextMenuStrip";
			resources.ApplyResources(this.sortContextMenuStrip, "sortContextMenuStrip");
			// 
			// contextCoolStripMenuItemClear
			// 
			this.contextCoolStripMenuItemClear.Name = "contextCoolStripMenuItemClear";
			resources.ApplyResources(this.contextCoolStripMenuItemClear, "contextCoolStripMenuItemClear");
			// 
			// sortLabel
			// 
			resources.ApplyResources(this.sortLabel, "sortLabel");
			this.sortLabel.Name = "sortLabel";
			// 
			// queueDataGrid
			// 
			this.queueDataGrid.AllowUserToAddRows = false;
			this.queueDataGrid.AllowUserToResizeRows = false;
			resources.ApplyResources(this.queueDataGrid, "queueDataGrid");
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.queueDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.queueDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.queueDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Type,
            this.Source,
            this.Number,
            this.Title,
            this.Artist,
            this.Album,
            this.Duration,
            this.Date,
            this.Format,
            this.Path});
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.queueDataGrid.DefaultCellStyle = dataGridViewCellStyle3;
			this.queueDataGrid.Name = "queueDataGrid";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.queueDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.queueDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.queueDataGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.queueDataGrid_ColumnHeaderMouseClick);
			this.queueDataGrid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.queueDataGrid_CellMouseDoubleClick);
			// 
			// Id
			// 
			this.Id.DataPropertyName = "Id";
			resources.ApplyResources(this.Id, "Id");
			this.Id.Name = "Id";
			this.Id.ReadOnly = true;
			this.Id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// Type
			// 
			this.Type.DataPropertyName = "Type";
			resources.ApplyResources(this.Type, "Type");
			this.Type.Name = "Type";
			this.Type.ReadOnly = true;
			this.Type.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.Type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// Source
			// 
			this.Source.DataPropertyName = "Source";
			resources.ApplyResources(this.Source, "Source");
			this.Source.Name = "Source";
			this.Source.ReadOnly = true;
			this.Source.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// Number
			// 
			this.Number.DataPropertyName = "Number";
			resources.ApplyResources(this.Number, "Number");
			this.Number.Name = "Number";
			this.Number.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// Title
			// 
			this.Title.DataPropertyName = "Title";
			resources.ApplyResources(this.Title, "Title");
			this.Title.Name = "Title";
			this.Title.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// Artist
			// 
			this.Artist.DataPropertyName = "Artist";
			resources.ApplyResources(this.Artist, "Artist");
			this.Artist.Name = "Artist";
			this.Artist.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// Album
			// 
			this.Album.DataPropertyName = "Album";
			resources.ApplyResources(this.Album, "Album");
			this.Album.Name = "Album";
			this.Album.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// Duration
			// 
			this.Duration.DataPropertyName = "Duration";
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			this.Duration.DefaultCellStyle = dataGridViewCellStyle2;
			resources.ApplyResources(this.Duration, "Duration");
			this.Duration.Name = "Duration";
			this.Duration.ReadOnly = true;
			this.Duration.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// Date
			// 
			this.Date.DataPropertyName = "Date";
			resources.ApplyResources(this.Date, "Date");
			this.Date.Name = "Date";
			this.Date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// Format
			// 
			this.Format.DataPropertyName = "Format";
			resources.ApplyResources(this.Format, "Format");
			this.Format.Name = "Format";
			this.Format.ReadOnly = true;
			this.Format.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// Path
			// 
			this.Path.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Path.DataPropertyName = "Path";
			resources.ApplyResources(this.Path, "Path");
			this.Path.Name = "Path";
			this.Path.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// queueSmallImageList
			// 
			this.queueSmallImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("queueSmallImageList.ImageStream")));
			this.queueSmallImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.queueSmallImageList.Images.SetKeyName(0, "music.png");
			this.queueSmallImageList.Images.SetKeyName(1, "picture.png");
			this.queueSmallImageList.Images.SetKeyName(2, "book_open.png");
			this.queueSmallImageList.Images.SetKeyName(3, "film.png");
			this.queueSmallImageList.Images.SetKeyName(4, "television.png");
			// 
			// DirectoryOpenDialog
			// 
			resources.ApplyResources(this.DirectoryOpenDialog, "DirectoryOpenDialog");
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
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ToolBoxGroupBox.ResumeLayout(false);
			this.ToolBoxContextMenuStrip.ResumeLayout(false);
			this.QueueGroupBox.ResumeLayout(false);
			this.QueueGroupBox.PerformLayout();
			this.sortContextMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.queueDataGrid)).EndInit();
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
		private System.Windows.Forms.SplitContainer UpperPlaybackStatusSplit;
		private System.Windows.Forms.GroupBox TasksGroupBox;
		private System.Windows.Forms.Label NowPlayingLabel;
		private System.Windows.Forms.Label NowPlayingTitle;
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
		private System.Windows.Forms.ContextMenuStrip ToolBoxContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem ToolBoxContextMenuItemRefresh;
		private System.Windows.Forms.ToolStripMenuItem toolManagerToolStripMenuItem;
		private System.Windows.Forms.DataGridView queueDataGrid;
		private System.Windows.Forms.ImageList queueSmallImageList;
		private System.Windows.Forms.ListView sortListView;
		private System.Windows.Forms.Label sortLabel;
		private System.Windows.Forms.ContextMenuStrip sortContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem contextCoolStripMenuItemClear;
		private System.Windows.Forms.DataGridViewTextBoxColumn Id;
		private System.Windows.Forms.DataGridViewImageColumn Type;
		private System.Windows.Forms.DataGridViewTextBoxColumn Source;
		private System.Windows.Forms.DataGridViewTextBoxColumn Number;
		private System.Windows.Forms.DataGridViewTextBoxColumn Title;
		private System.Windows.Forms.DataGridViewTextBoxColumn Artist;
		private System.Windows.Forms.DataGridViewTextBoxColumn Album;
		private System.Windows.Forms.DataGridViewTextBoxColumn Duration;
		private System.Windows.Forms.DataGridViewTextBoxColumn Date;
		private System.Windows.Forms.DataGridViewTextBoxColumn Format;
		private System.Windows.Forms.DataGridViewTextBoxColumn Path;
		private System.Windows.Forms.ToolStripMenuItem OpenDirectoryToolStripMenuItem;
		private System.Windows.Forms.FolderBrowserDialog DirectoryOpenDialog;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
	}
}

