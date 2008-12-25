using Telesophy.Alexandria.Clients.Ankh.Views.Data;

namespace Telesophy.Alexandria.Clients.Ankh.Views
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
            this.clearSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateOperatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.andOperatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orOperatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notOperatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllFiltersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllSortsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.catalogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadCatalogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedCatalogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedCatalogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importCatalogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelAllTasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.currentStatusToolStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.FileOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.NextButton = new System.Windows.Forms.Button();
            this.PlaybackTimer = new System.Windows.Forms.Timer(this.components);
            this.PlaybackGroupBox = new System.Windows.Forms.GroupBox();
            this.modeComboBox = new System.Windows.Forms.ComboBox();
            this.PreviousButton = new System.Windows.Forms.Button();
            this.NowPlayingLabel = new System.Windows.Forms.Label();
            this.NowPlayingTitle = new System.Windows.Forms.Label();
            this.submitCheckBox = new System.Windows.Forms.CheckBox();
            this.TasksGroupBox = new System.Windows.Forms.GroupBox();
            this.taskDataGrid = new System.Windows.Forms.DataGridView();
            this.taskNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskStatusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskProgressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskDetailsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.taskRunMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskPauseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskCancelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskCancelAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBoxQueueSplit = new System.Windows.Forms.SplitContainer();
            this.ToolBoxGroupBox = new System.Windows.Forms.GroupBox();
            this.ToolBoxListView = new System.Windows.Forms.ListView();
            this.ToolBoxContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolRipDiscMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolCreatePlaylistMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolEditPlaylistMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolAddSelectedItemsToPlaylistMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolBoxSmallImageList = new System.Windows.Forms.ImageList(this.components);
            this.QueueGroupBox = new System.Windows.Forms.GroupBox();
            this.SortGroupBox = new System.Windows.Forms.GroupBox();
            this.sortListView = new System.Windows.Forms.ListView();
            this.sortContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sortContextMenuStripItemClearSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.sortContextMenuStripItemClearAll = new System.Windows.Forms.ToolStripMenuItem();
            this.sortSmallImageList = new System.Windows.Forms.ImageList(this.components);
            this.sortButton = new System.Windows.Forms.Button();
            this.mediaItemSearchBox = new Telesophy.Alexandria.Clients.Ankh.Views.MediaItemSearchBox();
            this.queueDataGrid = new Telesophy.Alexandria.Clients.Ankh.Views.MediaItemDataGridView();
            this.statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.sourceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.artistColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.albumColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.durationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.formatColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sourceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.artistDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.albumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.durationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.formatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filterContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.filterContextMenuItemAddFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.filterContextMenuItemUpdateOperator = new System.Windows.Forms.ToolStripMenuItem();
            this.filterOperatorItemAnd = new System.Windows.Forms.ToolStripMenuItem();
            this.filterOperatorItemOr = new System.Windows.Forms.ToolStripMenuItem();
            this.filterOperatorItemNot = new System.Windows.Forms.ToolStripMenuItem();
            this.filterContextMenuItemEditFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.filterContextMenuItemClearSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.filterContextMenuItemClearAll = new System.Windows.Forms.ToolStripMenuItem();
            this.filterSmallImageList = new System.Windows.Forms.ImageList(this.components);
            this.queueSmallImageList = new System.Windows.Forms.ImageList(this.components);
            this.DirectoryOpenDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.taskSmallImageList = new System.Windows.Forms.ImageList(this.components);
            this.modeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PlaybackTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeTrackBar)).BeginInit();
            this.FileMenuStrip.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            this.PlaybackGroupBox.SuspendLayout();
            this.TasksGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taskDataGrid)).BeginInit();
            this.taskContextMenuStrip.SuspendLayout();
            this.toolBoxQueueSplit.Panel1.SuspendLayout();
            this.toolBoxQueueSplit.Panel2.SuspendLayout();
            this.toolBoxQueueSplit.SuspendLayout();
            this.ToolBoxGroupBox.SuspendLayout();
            this.ToolBoxContextMenuStrip.SuspendLayout();
            this.QueueGroupBox.SuspendLayout();
            this.SortGroupBox.SuspendLayout();
            this.sortContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.queueDataGrid)).BeginInit();
            this.filterContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // PlayPauseButton
            // 
            this.PlayPauseButton.BackgroundImage = global::Telesophy.Alexandria.Clients.Ankh.Properties.Resources.control_play_blue;
            resources.ApplyResources(this.PlayPauseButton, "PlayPauseButton");
            this.PlayPauseButton.Name = "PlayPauseButton";
            this.PlayPauseButton.UseVisualStyleBackColor = true;
            // 
            // StopButton
            // 
            this.StopButton.BackgroundImage = global::Telesophy.Alexandria.Clients.Ankh.Properties.Resources.control_stop_blue;
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
            this.MuteButton.BackgroundImage = global::Telesophy.Alexandria.Clients.Ankh.Properties.Resources.sound_mute;
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
            this.ToolsToolStripMenuItem,
            this.tasksToolStripMenuItem,
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
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearSelectedToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.filterToolStripMenuItem,
            this.sortToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
            // 
            // clearSelectedToolStripMenuItem
            // 
            this.clearSelectedToolStripMenuItem.Name = "clearSelectedToolStripMenuItem";
            resources.ApplyResources(this.clearSelectedToolStripMenuItem, "clearSelectedToolStripMenuItem");
            this.clearSelectedToolStripMenuItem.Click += new System.EventHandler(this.clearSelectedToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            resources.ApplyResources(this.clearToolStripMenuItem, "clearToolStripMenuItem");
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFilterToolStripMenuItem,
            this.updateOperatorToolStripMenuItem,
            this.editFilterToolStripMenuItem,
            this.clearFilterToolStripMenuItem,
            this.clearAllFiltersToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            resources.ApplyResources(this.filterToolStripMenuItem, "filterToolStripMenuItem");
            // 
            // addFilterToolStripMenuItem
            // 
            this.addFilterToolStripMenuItem.Name = "addFilterToolStripMenuItem";
            resources.ApplyResources(this.addFilterToolStripMenuItem, "addFilterToolStripMenuItem");
            // 
            // updateOperatorToolStripMenuItem
            // 
            this.updateOperatorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.andOperatorToolStripMenuItem,
            this.orOperatorToolStripMenuItem,
            this.notOperatorToolStripMenuItem});
            this.updateOperatorToolStripMenuItem.Name = "updateOperatorToolStripMenuItem";
            resources.ApplyResources(this.updateOperatorToolStripMenuItem, "updateOperatorToolStripMenuItem");
            // 
            // andOperatorToolStripMenuItem
            // 
            this.andOperatorToolStripMenuItem.Name = "andOperatorToolStripMenuItem";
            resources.ApplyResources(this.andOperatorToolStripMenuItem, "andOperatorToolStripMenuItem");
            // 
            // orOperatorToolStripMenuItem
            // 
            this.orOperatorToolStripMenuItem.Name = "orOperatorToolStripMenuItem";
            resources.ApplyResources(this.orOperatorToolStripMenuItem, "orOperatorToolStripMenuItem");
            // 
            // notOperatorToolStripMenuItem
            // 
            this.notOperatorToolStripMenuItem.Name = "notOperatorToolStripMenuItem";
            resources.ApplyResources(this.notOperatorToolStripMenuItem, "notOperatorToolStripMenuItem");
            // 
            // editFilterToolStripMenuItem
            // 
            this.editFilterToolStripMenuItem.Name = "editFilterToolStripMenuItem";
            resources.ApplyResources(this.editFilterToolStripMenuItem, "editFilterToolStripMenuItem");
            // 
            // clearFilterToolStripMenuItem
            // 
            this.clearFilterToolStripMenuItem.Name = "clearFilterToolStripMenuItem";
            resources.ApplyResources(this.clearFilterToolStripMenuItem, "clearFilterToolStripMenuItem");
            // 
            // clearAllFiltersToolStripMenuItem
            // 
            this.clearAllFiltersToolStripMenuItem.Name = "clearAllFiltersToolStripMenuItem";
            resources.ApplyResources(this.clearAllFiltersToolStripMenuItem, "clearAllFiltersToolStripMenuItem");
            // 
            // sortToolStripMenuItem
            // 
            this.sortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearSortToolStripMenuItem,
            this.clearAllSortsToolStripMenuItem});
            this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            resources.ApplyResources(this.sortToolStripMenuItem, "sortToolStripMenuItem");
            this.sortToolStripMenuItem.DropDownOpening += new System.EventHandler(this.sortToolStripMenuItem_DropDownOpening);
            // 
            // clearSortToolStripMenuItem
            // 
            this.clearSortToolStripMenuItem.Name = "clearSortToolStripMenuItem";
            resources.ApplyResources(this.clearSortToolStripMenuItem, "clearSortToolStripMenuItem");
            this.clearSortToolStripMenuItem.Click += new System.EventHandler(this.clearSortToolStripMenuItem_Click);
            // 
            // clearAllSortsToolStripMenuItem
            // 
            this.clearAllSortsToolStripMenuItem.Name = "clearAllSortsToolStripMenuItem";
            resources.ApplyResources(this.clearAllSortsToolStripMenuItem, "clearAllSortsToolStripMenuItem");
            this.clearAllSortsToolStripMenuItem.Click += new System.EventHandler(this.clearAllSortsToolStripMenuItem_Click);
            // 
            // catalogToolStripMenuItem
            // 
            this.catalogToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadCatalogToolStripMenuItem,
            this.saveSelectedCatalogToolStripMenuItem,
            this.deleteSelectedCatalogToolStripMenuItem,
            this.importCatalogToolStripMenuItem});
            this.catalogToolStripMenuItem.Name = "catalogToolStripMenuItem";
            resources.ApplyResources(this.catalogToolStripMenuItem, "catalogToolStripMenuItem");
            // 
            // loadCatalogToolStripMenuItem
            // 
            this.loadCatalogToolStripMenuItem.Name = "loadCatalogToolStripMenuItem";
            resources.ApplyResources(this.loadCatalogToolStripMenuItem, "loadCatalogToolStripMenuItem");
            this.loadCatalogToolStripMenuItem.Click += new System.EventHandler(this.loadCatalogToolStripMenuItem_Click);
            // 
            // saveSelectedCatalogToolStripMenuItem
            // 
            this.saveSelectedCatalogToolStripMenuItem.Name = "saveSelectedCatalogToolStripMenuItem";
            resources.ApplyResources(this.saveSelectedCatalogToolStripMenuItem, "saveSelectedCatalogToolStripMenuItem");
            this.saveSelectedCatalogToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedCatalogToolStripMenuItem_Click);
            // 
            // deleteSelectedCatalogToolStripMenuItem
            // 
            this.deleteSelectedCatalogToolStripMenuItem.Name = "deleteSelectedCatalogToolStripMenuItem";
            resources.ApplyResources(this.deleteSelectedCatalogToolStripMenuItem, "deleteSelectedCatalogToolStripMenuItem");
            this.deleteSelectedCatalogToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectedCatalogToolStripMenuItem_Click);
            // 
            // importCatalogToolStripMenuItem
            // 
            this.importCatalogToolStripMenuItem.Name = "importCatalogToolStripMenuItem";
            resources.ApplyResources(this.importCatalogToolStripMenuItem, "importCatalogToolStripMenuItem");
            this.importCatalogToolStripMenuItem.Click += new System.EventHandler(this.importCatalogToolStripMenuItem_Click);
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
            // tasksToolStripMenuItem
            // 
            this.tasksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runTaskToolStripMenuItem,
            this.pauseTaskToolStripMenuItem,
            this.cancelTaskToolStripMenuItem,
            this.cancelAllTasksToolStripMenuItem});
            this.tasksToolStripMenuItem.Name = "tasksToolStripMenuItem";
            resources.ApplyResources(this.tasksToolStripMenuItem, "tasksToolStripMenuItem");
            // 
            // runTaskToolStripMenuItem
            // 
            this.runTaskToolStripMenuItem.Image = global::Telesophy.Alexandria.Clients.Ankh.Properties.Resources.clock_play;
            this.runTaskToolStripMenuItem.Name = "runTaskToolStripMenuItem";
            resources.ApplyResources(this.runTaskToolStripMenuItem, "runTaskToolStripMenuItem");
            this.runTaskToolStripMenuItem.Click += new System.EventHandler(this.runTaskToolStripMenuItem_Click);
            // 
            // pauseTaskToolStripMenuItem
            // 
            this.pauseTaskToolStripMenuItem.Image = global::Telesophy.Alexandria.Clients.Ankh.Properties.Resources.clock_pause;
            this.pauseTaskToolStripMenuItem.Name = "pauseTaskToolStripMenuItem";
            resources.ApplyResources(this.pauseTaskToolStripMenuItem, "pauseTaskToolStripMenuItem");
            this.pauseTaskToolStripMenuItem.Click += new System.EventHandler(this.pauseTaskToolStripMenuItem_Click);
            // 
            // cancelTaskToolStripMenuItem
            // 
            this.cancelTaskToolStripMenuItem.Image = global::Telesophy.Alexandria.Clients.Ankh.Properties.Resources.clock_stop;
            this.cancelTaskToolStripMenuItem.Name = "cancelTaskToolStripMenuItem";
            resources.ApplyResources(this.cancelTaskToolStripMenuItem, "cancelTaskToolStripMenuItem");
            this.cancelTaskToolStripMenuItem.Click += new System.EventHandler(this.cancelTaskToolStripMenuItem_Click);
            // 
            // cancelAllTasksToolStripMenuItem
            // 
            this.cancelAllTasksToolStripMenuItem.Image = global::Telesophy.Alexandria.Clients.Ankh.Properties.Resources.cancel;
            this.cancelAllTasksToolStripMenuItem.Name = "cancelAllTasksToolStripMenuItem";
            resources.ApplyResources(this.cancelAllTasksToolStripMenuItem, "cancelAllTasksToolStripMenuItem");
            this.cancelAllTasksToolStripMenuItem.Click += new System.EventHandler(this.cancelAllTasksToolStripMenuItem_Click);
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
            this.currentStatusToolStripLabel});
            resources.ApplyResources(this.StatusStrip, "StatusStrip");
            this.StatusStrip.Name = "StatusStrip";
            // 
            // currentStatusToolStripLabel
            // 
            this.currentStatusToolStripLabel.Name = "currentStatusToolStripLabel";
            resources.ApplyResources(this.currentStatusToolStripLabel, "currentStatusToolStripLabel");
            // 
            // NextButton
            // 
            this.NextButton.BackgroundImage = global::Telesophy.Alexandria.Clients.Ankh.Properties.Resources.control_end_blue;
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
            this.PlaybackGroupBox.Controls.Add(this.PreviousButton);
            this.PlaybackGroupBox.Controls.Add(this.NowPlayingLabel);
            this.PlaybackGroupBox.Controls.Add(this.NowPlayingTitle);
            this.PlaybackGroupBox.Controls.Add(this.NextButton);
            this.PlaybackGroupBox.Controls.Add(this.MuteButton);
            this.PlaybackGroupBox.Controls.Add(this.VolumeTrackBar);
            this.PlaybackGroupBox.Controls.Add(this.PlayPauseButton);
            this.PlaybackGroupBox.Controls.Add(this.StopButton);
            this.PlaybackGroupBox.Controls.Add(this.PlaybackTrackBar);
            resources.ApplyResources(this.PlaybackGroupBox, "PlaybackGroupBox");
            this.PlaybackGroupBox.Name = "PlaybackGroupBox";
            this.PlaybackGroupBox.TabStop = false;
            // 
            // modeComboBox
            // 
            resources.ApplyResources(this.modeComboBox, "modeComboBox");
            this.modeComboBox.FormattingEnabled = true;
            this.modeComboBox.Name = "modeComboBox";
            this.modeComboBox.SelectedIndexChanged += new System.EventHandler(this.modeComboBox_SelectedIndexChanged);
            // 
            // PreviousButton
            // 
            this.PreviousButton.BackgroundImage = global::Telesophy.Alexandria.Clients.Ankh.Properties.Resources.control_start_blue;
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
            // submitCheckBox
            // 
            resources.ApplyResources(this.submitCheckBox, "submitCheckBox");
            this.submitCheckBox.Name = "submitCheckBox";
            this.submitCheckBox.UseVisualStyleBackColor = true;
            this.submitCheckBox.CheckedChanged += new System.EventHandler(this.submitCheckBox_CheckedChanged);
            // 
            // TasksGroupBox
            // 
            resources.ApplyResources(this.TasksGroupBox, "TasksGroupBox");
            this.TasksGroupBox.Controls.Add(this.submitCheckBox);
            this.TasksGroupBox.Controls.Add(this.taskDataGrid);
            this.TasksGroupBox.Name = "TasksGroupBox";
            this.TasksGroupBox.TabStop = false;
            // 
            // taskDataGrid
            // 
            this.taskDataGrid.AllowUserToAddRows = false;
            this.taskDataGrid.AllowUserToResizeRows = false;
            resources.ApplyResources(this.taskDataGrid, "taskDataGrid");
            this.taskDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.taskDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.taskNameColumn,
            this.taskStatusColumn,
            this.taskProgressColumn,
            this.taskDetailsColumn});
            this.taskDataGrid.ContextMenuStrip = this.taskContextMenuStrip;
            this.taskDataGrid.MultiSelect = false;
            this.taskDataGrid.Name = "taskDataGrid";
            this.taskDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // taskNameColumn
            // 
            resources.ApplyResources(this.taskNameColumn, "taskNameColumn");
            this.taskNameColumn.Name = "taskNameColumn";
            // 
            // taskStatusColumn
            // 
            resources.ApplyResources(this.taskStatusColumn, "taskStatusColumn");
            this.taskStatusColumn.Name = "taskStatusColumn";
            // 
            // taskProgressColumn
            // 
            resources.ApplyResources(this.taskProgressColumn, "taskProgressColumn");
            this.taskProgressColumn.Name = "taskProgressColumn";
            this.taskProgressColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // taskDetailsColumn
            // 
            this.taskDetailsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.taskDetailsColumn, "taskDetailsColumn");
            this.taskDetailsColumn.Name = "taskDetailsColumn";
            // 
            // taskContextMenuStrip
            // 
            this.taskContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.taskRunMenuItem,
            this.taskPauseMenuItem,
            this.taskCancelMenuItem,
            this.taskCancelAllMenuItem});
            this.taskContextMenuStrip.Name = "taskContextMenuStrip";
            resources.ApplyResources(this.taskContextMenuStrip, "taskContextMenuStrip");
            this.taskContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.taskContextMenuStrip_Opening);
            // 
            // taskRunMenuItem
            // 
            this.taskRunMenuItem.Image = global::Telesophy.Alexandria.Clients.Ankh.Properties.Resources.clock_play;
            this.taskRunMenuItem.Name = "taskRunMenuItem";
            resources.ApplyResources(this.taskRunMenuItem, "taskRunMenuItem");
            this.taskRunMenuItem.Click += new System.EventHandler(this.taskRunMenuItem_Click);
            // 
            // taskPauseMenuItem
            // 
            this.taskPauseMenuItem.Image = global::Telesophy.Alexandria.Clients.Ankh.Properties.Resources.clock_pause;
            this.taskPauseMenuItem.Name = "taskPauseMenuItem";
            resources.ApplyResources(this.taskPauseMenuItem, "taskPauseMenuItem");
            this.taskPauseMenuItem.Click += new System.EventHandler(this.taskPauseMenuItem_Click);
            // 
            // taskCancelMenuItem
            // 
            this.taskCancelMenuItem.Image = global::Telesophy.Alexandria.Clients.Ankh.Properties.Resources.clock_stop;
            this.taskCancelMenuItem.Name = "taskCancelMenuItem";
            resources.ApplyResources(this.taskCancelMenuItem, "taskCancelMenuItem");
            this.taskCancelMenuItem.Click += new System.EventHandler(this.taskCancelMenuItem_Click);
            // 
            // taskCancelAllMenuItem
            // 
            this.taskCancelAllMenuItem.Image = global::Telesophy.Alexandria.Clients.Ankh.Properties.Resources.cancel;
            this.taskCancelAllMenuItem.Name = "taskCancelAllMenuItem";
            resources.ApplyResources(this.taskCancelAllMenuItem, "taskCancelAllMenuItem");
            this.taskCancelAllMenuItem.Click += new System.EventHandler(this.taskCancelAllMenuItem_Click);
            // 
            // toolBoxQueueSplit
            // 
            resources.ApplyResources(this.toolBoxQueueSplit, "toolBoxQueueSplit");
            this.toolBoxQueueSplit.Name = "toolBoxQueueSplit";
            // 
            // toolBoxQueueSplit.Panel1
            // 
            this.toolBoxQueueSplit.Panel1.Controls.Add(this.ToolBoxGroupBox);
            // 
            // toolBoxQueueSplit.Panel2
            // 
            this.toolBoxQueueSplit.Panel2.Controls.Add(this.QueueGroupBox);
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
            this.ToolBoxListView.AllowDrop = true;
            resources.ApplyResources(this.ToolBoxListView, "ToolBoxListView");
            this.ToolBoxListView.ContextMenuStrip = this.ToolBoxContextMenuStrip;
            this.ToolBoxListView.MultiSelect = false;
            this.ToolBoxListView.Name = "ToolBoxListView";
            this.ToolBoxListView.ShowItemToolTips = true;
            this.ToolBoxListView.SmallImageList = this.ToolBoxSmallImageList;
            this.ToolBoxListView.UseCompatibleStateImageBehavior = false;
            this.ToolBoxListView.View = System.Windows.Forms.View.List;
            this.ToolBoxListView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ToolBoxListView_ItemDrag);
            this.ToolBoxListView.DragOver += new System.Windows.Forms.DragEventHandler(this.ToolBoxListView_DragOver);
            // 
            // ToolBoxContextMenuStrip
            // 
            this.ToolBoxContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolRefresh,
            this.toolRipDiscMenuItem,
            this.toolCreatePlaylistMenuItem,
            this.toolEditPlaylistMenuItem,
            this.toolAddSelectedItemsToPlaylistMenuItem});
            this.ToolBoxContextMenuStrip.Name = "ToolBoxContextMenuStrip";
            resources.ApplyResources(this.ToolBoxContextMenuStrip, "ToolBoxContextMenuStrip");
            // 
            // toolRefresh
            // 
            this.toolRefresh.Name = "toolRefresh";
            resources.ApplyResources(this.toolRefresh, "toolRefresh");
            this.toolRefresh.Click += new System.EventHandler(this.toolRefresh_Click);
            // 
            // toolRipDiscMenuItem
            // 
            this.toolRipDiscMenuItem.Name = "toolRipDiscMenuItem";
            resources.ApplyResources(this.toolRipDiscMenuItem, "toolRipDiscMenuItem");
            this.toolRipDiscMenuItem.Click += new System.EventHandler(this.toolRipDiscMenuItem_Click);
            // 
            // toolCreatePlaylistMenuItem
            // 
            this.toolCreatePlaylistMenuItem.Name = "toolCreatePlaylistMenuItem";
            resources.ApplyResources(this.toolCreatePlaylistMenuItem, "toolCreatePlaylistMenuItem");
            this.toolCreatePlaylistMenuItem.Click += new System.EventHandler(this.toolCreatePlaylistMenuItem_Click);
            // 
            // toolEditPlaylistMenuItem
            // 
            this.toolEditPlaylistMenuItem.Name = "toolEditPlaylistMenuItem";
            resources.ApplyResources(this.toolEditPlaylistMenuItem, "toolEditPlaylistMenuItem");
            this.toolEditPlaylistMenuItem.Click += new System.EventHandler(this.toolEditPlaylistMenuItem_Click);
            // 
            // toolAddSelectedItemsToPlaylistMenuItem
            // 
            this.toolAddSelectedItemsToPlaylistMenuItem.Name = "toolAddSelectedItemsToPlaylistMenuItem";
            resources.ApplyResources(this.toolAddSelectedItemsToPlaylistMenuItem, "toolAddSelectedItemsToPlaylistMenuItem");
            this.toolAddSelectedItemsToPlaylistMenuItem.Click += new System.EventHandler(this.toolAddSelectedItemsToPlaylistMenuItem_Click);
            // 
            // ToolBoxSmallImageList
            // 
            this.ToolBoxSmallImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ToolBoxSmallImageList.ImageStream")));
            this.ToolBoxSmallImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ToolBoxSmallImageList.Images.SetKeyName(0, "drive.png");
            this.ToolBoxSmallImageList.Images.SetKeyName(1, "drive_cd.png");
            this.ToolBoxSmallImageList.Images.SetKeyName(2, "drive_cd_empty.png");
            this.ToolBoxSmallImageList.Images.SetKeyName(3, "feed.png");
            this.ToolBoxSmallImageList.Images.SetKeyName(4, "table.png");
            // 
            // QueueGroupBox
            // 
            resources.ApplyResources(this.QueueGroupBox, "QueueGroupBox");
            this.QueueGroupBox.Controls.Add(this.SortGroupBox);
            this.QueueGroupBox.Controls.Add(this.mediaItemSearchBox);
            this.QueueGroupBox.Controls.Add(this.queueDataGrid);
            this.QueueGroupBox.Name = "QueueGroupBox";
            this.QueueGroupBox.TabStop = false;
            // 
            // SortGroupBox
            // 
            resources.ApplyResources(this.SortGroupBox, "SortGroupBox");
            this.SortGroupBox.Controls.Add(this.modeLabel);
            this.SortGroupBox.Controls.Add(this.modeComboBox);
            this.SortGroupBox.Controls.Add(this.sortListView);
            this.SortGroupBox.Controls.Add(this.sortButton);
            this.SortGroupBox.Name = "SortGroupBox";
            this.SortGroupBox.TabStop = false;
            // 
            // sortListView
            // 
            this.sortListView.AllowDrop = true;
            resources.ApplyResources(this.sortListView, "sortListView");
            this.sortListView.ContextMenuStrip = this.sortContextMenuStrip;
            this.sortListView.MultiSelect = false;
            this.sortListView.Name = "sortListView";
            this.sortListView.SmallImageList = this.sortSmallImageList;
            this.sortListView.UseCompatibleStateImageBehavior = false;
            this.sortListView.View = System.Windows.Forms.View.List;
            this.sortListView.ItemActivate += new System.EventHandler(this.sortListView_ItemActivate);
            this.sortListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.sortListView_DragDrop);
            this.sortListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.sortListView_DragEnter);
            this.sortListView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.sortListView_KeyUp);
            this.sortListView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.sortListView_ItemDrag);
            // 
            // sortContextMenuStrip
            // 
            this.sortContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sortContextMenuStripItemClearSelected,
            this.sortContextMenuStripItemClearAll});
            this.sortContextMenuStrip.Name = "sortContextMenuStrip";
            resources.ApplyResources(this.sortContextMenuStrip, "sortContextMenuStrip");
            this.sortContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.sortContextMenuStrip_Opening);
            // 
            // sortContextMenuStripItemClearSelected
            // 
            this.sortContextMenuStripItemClearSelected.Name = "sortContextMenuStripItemClearSelected";
            resources.ApplyResources(this.sortContextMenuStripItemClearSelected, "sortContextMenuStripItemClearSelected");
            this.sortContextMenuStripItemClearSelected.Click += new System.EventHandler(this.sortContextMenuStripItemClearSelected_Click);
            // 
            // sortContextMenuStripItemClearAll
            // 
            this.sortContextMenuStripItemClearAll.Name = "sortContextMenuStripItemClearAll";
            resources.ApplyResources(this.sortContextMenuStripItemClearAll, "sortContextMenuStripItemClearAll");
            this.sortContextMenuStripItemClearAll.Click += new System.EventHandler(this.sortContextMenuStripItemClearAll_Click);
            // 
            // sortSmallImageList
            // 
            this.sortSmallImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("sortSmallImageList.ImageStream")));
            this.sortSmallImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.sortSmallImageList.Images.SetKeyName(0, "arrow_up.png");
            this.sortSmallImageList.Images.SetKeyName(1, "arrow_down.png");
            // 
            // sortButton
            // 
            resources.ApplyResources(this.sortButton, "sortButton");
            this.sortButton.Name = "sortButton";
            this.sortButton.UseVisualStyleBackColor = true;
            this.sortButton.Click += new System.EventHandler(this.sortButton_Click);
            // 
            // mediaItemSearchBox
            // 
            resources.ApplyResources(this.mediaItemSearchBox, "mediaItemSearchBox");
            this.mediaItemSearchBox.Name = "mediaItemSearchBox";
            this.mediaItemSearchBox.PersistenceController = null;
            this.mediaItemSearchBox.SearchCompleted = null;
            // 
            // queueDataGrid
            // 
            this.queueDataGrid.AllowDrop = true;
            this.queueDataGrid.AllowUserToAddRows = false;
            this.queueDataGrid.AllowUserToDeleteRows = false;
            this.queueDataGrid.AllowUserToOrderColumns = true;
            this.queueDataGrid.AllowUserToResizeRows = false;
            resources.ApplyResources(this.queueDataGrid, "queueDataGrid");
            this.queueDataGrid.AutoGenerateColumns = false;
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
            this.statusColumn,
            this.typeColumn,
            this.sourceColumn,
            this.numberColumn,
            this.titleColumn,
            this.artistColumn,
            this.albumColumn,
            this.durationColumn,
            this.dateColumn,
            this.formatColumn,
            this.pathColumn,
            this.idColumn,
            this.idDataGridViewTextBoxColumn,
            this.typeDataGridViewTextBoxColumn,
            this.sourceDataGridViewTextBoxColumn,
            this.numberDataGridViewTextBoxColumn,
            this.titleDataGridViewTextBoxColumn,
            this.artistDataGridViewTextBoxColumn,
            this.albumDataGridViewTextBoxColumn,
            this.durationDataGridViewTextBoxColumn,
            this.dateDataGridViewTextBoxColumn,
            this.formatDataGridViewTextBoxColumn,
            this.pathDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn});
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
            this.queueDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.queueDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.queueDataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.queueDataGrid_CellValueChanged);
            this.queueDataGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.queueDataGrid_ColumnHeaderMouseClick);
            this.queueDataGrid.DragOver += new System.Windows.Forms.DragEventHandler(this.queueDataGrid_DragOver);
            this.queueDataGrid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.queueDataGrid_CellMouseDoubleClick);
            this.queueDataGrid.DragEnter += new System.Windows.Forms.DragEventHandler(this.queueDataGrid_DragEnter);
            this.queueDataGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.queueDataGrid_DataError);
            this.queueDataGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.queueDataGrid_KeyUp);
            this.queueDataGrid.DragDrop += new System.Windows.Forms.DragEventHandler(this.queueDataGrid_DragDrop);
            // 
            // statusColumn
            // 
            this.statusColumn.DataPropertyName = "Status";
            resources.ApplyResources(this.statusColumn, "statusColumn");
            this.statusColumn.Name = "statusColumn";
            this.statusColumn.ReadOnly = true;
            this.statusColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.statusColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // typeColumn
            // 
            this.typeColumn.DataPropertyName = "Type";
            resources.ApplyResources(this.typeColumn, "typeColumn");
            this.typeColumn.Name = "typeColumn";
            this.typeColumn.ReadOnly = true;
            this.typeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.typeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // sourceColumn
            // 
            this.sourceColumn.DataPropertyName = "Source";
            resources.ApplyResources(this.sourceColumn, "sourceColumn");
            this.sourceColumn.Name = "sourceColumn";
            this.sourceColumn.ReadOnly = true;
            this.sourceColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // numberColumn
            // 
            this.numberColumn.DataPropertyName = "Number";
            resources.ApplyResources(this.numberColumn, "numberColumn");
            this.numberColumn.Name = "numberColumn";
            this.numberColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // titleColumn
            // 
            this.titleColumn.DataPropertyName = "Title";
            resources.ApplyResources(this.titleColumn, "titleColumn");
            this.titleColumn.Name = "titleColumn";
            this.titleColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // artistColumn
            // 
            this.artistColumn.DataPropertyName = "Artist";
            resources.ApplyResources(this.artistColumn, "artistColumn");
            this.artistColumn.Name = "artistColumn";
            this.artistColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // albumColumn
            // 
            this.albumColumn.DataPropertyName = "Album";
            resources.ApplyResources(this.albumColumn, "albumColumn");
            this.albumColumn.Name = "albumColumn";
            this.albumColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // durationColumn
            // 
            this.durationColumn.DataPropertyName = "Duration";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.durationColumn.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.durationColumn, "durationColumn");
            this.durationColumn.Name = "durationColumn";
            this.durationColumn.ReadOnly = true;
            this.durationColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dateColumn
            // 
            this.dateColumn.DataPropertyName = "Date";
            resources.ApplyResources(this.dateColumn, "dateColumn");
            this.dateColumn.Name = "dateColumn";
            this.dateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // formatColumn
            // 
            this.formatColumn.DataPropertyName = "Format";
            resources.ApplyResources(this.formatColumn, "formatColumn");
            this.formatColumn.Name = "formatColumn";
            this.formatColumn.ReadOnly = true;
            this.formatColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // pathColumn
            // 
            this.pathColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.pathColumn.DataPropertyName = "Path";
            resources.ApplyResources(this.pathColumn, "pathColumn");
            this.pathColumn.Name = "pathColumn";
            this.pathColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // idColumn
            // 
            this.idColumn.DataPropertyName = "Id";
            resources.ApplyResources(this.idColumn, "idColumn");
            this.idColumn.Name = "idColumn";
            this.idColumn.ReadOnly = true;
            this.idColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.idColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(this.idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
            resources.ApplyResources(this.typeDataGridViewTextBoxColumn, "typeDataGridViewTextBoxColumn");
            this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            // 
            // sourceDataGridViewTextBoxColumn
            // 
            this.sourceDataGridViewTextBoxColumn.DataPropertyName = "Source";
            resources.ApplyResources(this.sourceDataGridViewTextBoxColumn, "sourceDataGridViewTextBoxColumn");
            this.sourceDataGridViewTextBoxColumn.Name = "sourceDataGridViewTextBoxColumn";
            // 
            // numberDataGridViewTextBoxColumn
            // 
            this.numberDataGridViewTextBoxColumn.DataPropertyName = "Number";
            resources.ApplyResources(this.numberDataGridViewTextBoxColumn, "numberDataGridViewTextBoxColumn");
            this.numberDataGridViewTextBoxColumn.Name = "numberDataGridViewTextBoxColumn";
            // 
            // titleDataGridViewTextBoxColumn
            // 
            this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            resources.ApplyResources(this.titleDataGridViewTextBoxColumn, "titleDataGridViewTextBoxColumn");
            this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            // 
            // artistDataGridViewTextBoxColumn
            // 
            this.artistDataGridViewTextBoxColumn.DataPropertyName = "Artist";
            resources.ApplyResources(this.artistDataGridViewTextBoxColumn, "artistDataGridViewTextBoxColumn");
            this.artistDataGridViewTextBoxColumn.Name = "artistDataGridViewTextBoxColumn";
            // 
            // albumDataGridViewTextBoxColumn
            // 
            this.albumDataGridViewTextBoxColumn.DataPropertyName = "Album";
            resources.ApplyResources(this.albumDataGridViewTextBoxColumn, "albumDataGridViewTextBoxColumn");
            this.albumDataGridViewTextBoxColumn.Name = "albumDataGridViewTextBoxColumn";
            // 
            // durationDataGridViewTextBoxColumn
            // 
            this.durationDataGridViewTextBoxColumn.DataPropertyName = "Duration";
            resources.ApplyResources(this.durationDataGridViewTextBoxColumn, "durationDataGridViewTextBoxColumn");
            this.durationDataGridViewTextBoxColumn.Name = "durationDataGridViewTextBoxColumn";
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "Date";
            resources.ApplyResources(this.dateDataGridViewTextBoxColumn, "dateDataGridViewTextBoxColumn");
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            // 
            // formatDataGridViewTextBoxColumn
            // 
            this.formatDataGridViewTextBoxColumn.DataPropertyName = "Format";
            resources.ApplyResources(this.formatDataGridViewTextBoxColumn, "formatDataGridViewTextBoxColumn");
            this.formatDataGridViewTextBoxColumn.Name = "formatDataGridViewTextBoxColumn";
            // 
            // pathDataGridViewTextBoxColumn
            // 
            this.pathDataGridViewTextBoxColumn.DataPropertyName = "Path";
            resources.ApplyResources(this.pathDataGridViewTextBoxColumn, "pathDataGridViewTextBoxColumn");
            this.pathDataGridViewTextBoxColumn.Name = "pathDataGridViewTextBoxColumn";
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            resources.ApplyResources(this.statusDataGridViewTextBoxColumn, "statusDataGridViewTextBoxColumn");
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            // 
            // filterContextMenuStrip
            // 
            this.filterContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterContextMenuItemAddFilter,
            this.filterContextMenuItemUpdateOperator,
            this.filterContextMenuItemEditFilter,
            this.filterContextMenuItemClearSelected,
            this.filterContextMenuItemClearAll});
            this.filterContextMenuStrip.Name = "filterContextMenuStrip";
            resources.ApplyResources(this.filterContextMenuStrip, "filterContextMenuStrip");
            // 
            // filterContextMenuItemAddFilter
            // 
            this.filterContextMenuItemAddFilter.Name = "filterContextMenuItemAddFilter";
            resources.ApplyResources(this.filterContextMenuItemAddFilter, "filterContextMenuItemAddFilter");
            // 
            // filterContextMenuItemUpdateOperator
            // 
            this.filterContextMenuItemUpdateOperator.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterOperatorItemAnd,
            this.filterOperatorItemOr,
            this.filterOperatorItemNot});
            this.filterContextMenuItemUpdateOperator.Name = "filterContextMenuItemUpdateOperator";
            resources.ApplyResources(this.filterContextMenuItemUpdateOperator, "filterContextMenuItemUpdateOperator");
            // 
            // filterOperatorItemAnd
            // 
            this.filterOperatorItemAnd.Name = "filterOperatorItemAnd";
            resources.ApplyResources(this.filterOperatorItemAnd, "filterOperatorItemAnd");
            // 
            // filterOperatorItemOr
            // 
            this.filterOperatorItemOr.Name = "filterOperatorItemOr";
            resources.ApplyResources(this.filterOperatorItemOr, "filterOperatorItemOr");
            // 
            // filterOperatorItemNot
            // 
            this.filterOperatorItemNot.Name = "filterOperatorItemNot";
            resources.ApplyResources(this.filterOperatorItemNot, "filterOperatorItemNot");
            // 
            // filterContextMenuItemEditFilter
            // 
            this.filterContextMenuItemEditFilter.Name = "filterContextMenuItemEditFilter";
            resources.ApplyResources(this.filterContextMenuItemEditFilter, "filterContextMenuItemEditFilter");
            // 
            // filterContextMenuItemClearSelected
            // 
            this.filterContextMenuItemClearSelected.Name = "filterContextMenuItemClearSelected";
            resources.ApplyResources(this.filterContextMenuItemClearSelected, "filterContextMenuItemClearSelected");
            // 
            // filterContextMenuItemClearAll
            // 
            this.filterContextMenuItemClearAll.Name = "filterContextMenuItemClearAll";
            resources.ApplyResources(this.filterContextMenuItemClearAll, "filterContextMenuItemClearAll");
            // 
            // filterSmallImageList
            // 
            this.filterSmallImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("filterSmallImageList.ImageStream")));
            this.filterSmallImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.filterSmallImageList.Images.SetKeyName(0, "asterisk_yellow.png");
            this.filterSmallImageList.Images.SetKeyName(1, "chart_bar.png");
            this.filterSmallImageList.Images.SetKeyName(2, "brick.png");
            this.filterSmallImageList.Images.SetKeyName(3, "database_connect.png");
            this.filterSmallImageList.Images.SetKeyName(4, "text_list_numbers.png");
            this.filterSmallImageList.Images.SetKeyName(5, "award_star_bronze_3.png");
            this.filterSmallImageList.Images.SetKeyName(6, "user.png");
            this.filterSmallImageList.Images.SetKeyName(7, "cd.png");
            this.filterSmallImageList.Images.SetKeyName(8, "clock.png");
            this.filterSmallImageList.Images.SetKeyName(9, "date.png");
            this.filterSmallImageList.Images.SetKeyName(10, "page_white_cd.png");
            this.filterSmallImageList.Images.SetKeyName(11, "page_link.png");
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
            // taskSmallImageList
            // 
            this.taskSmallImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            resources.ApplyResources(this.taskSmallImageList, "taskSmallImageList");
            this.taskSmallImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // modeLabel
            // 
            resources.ApplyResources(this.modeLabel, "modeLabel");
            this.modeLabel.Name = "modeLabel";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.FileMenuStrip);
            this.Controls.Add(this.TasksGroupBox);
            this.Controls.Add(this.PlaybackGroupBox);
            this.Controls.Add(this.toolBoxQueueSplit);
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
            this.TasksGroupBox.ResumeLayout(false);
            this.TasksGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taskDataGrid)).EndInit();
            this.taskContextMenuStrip.ResumeLayout(false);
            this.toolBoxQueueSplit.Panel1.ResumeLayout(false);
            this.toolBoxQueueSplit.Panel2.ResumeLayout(false);
            this.toolBoxQueueSplit.ResumeLayout(false);
            this.ToolBoxGroupBox.ResumeLayout(false);
            this.ToolBoxContextMenuStrip.ResumeLayout(false);
            this.QueueGroupBox.ResumeLayout(false);
            this.SortGroupBox.ResumeLayout(false);
            this.SortGroupBox.PerformLayout();
            this.sortContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.queueDataGrid)).EndInit();
            this.filterContextMenuStrip.ResumeLayout(false);
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
		private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog FileOpenDialog;
		private System.Windows.Forms.Button NextButton;
		private System.Windows.Forms.Timer PlaybackTimer;
		private System.Windows.Forms.GroupBox PlaybackGroupBox;
		private System.Windows.Forms.GroupBox QueueGroupBox;
		private System.Windows.Forms.GroupBox TasksGroupBox;
		private System.Windows.Forms.Label NowPlayingLabel;
		private System.Windows.Forms.Label NowPlayingTitle;
		private System.Windows.Forms.Button PreviousButton;
		private System.Windows.Forms.ToolStripMenuItem ToolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem catalogToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pluginsToolStripMenuItem;
		private System.Windows.Forms.SplitContainer toolBoxQueueSplit;
		private System.Windows.Forms.GroupBox ToolBoxGroupBox;
		private System.Windows.Forms.ListView ToolBoxListView;
		private System.Windows.Forms.ImageList ToolBoxSmallImageList;
		private System.Windows.Forms.ContextMenuStrip ToolBoxContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem toolRefresh;
		private System.Windows.Forms.ToolStripMenuItem toolManagerToolStripMenuItem;
		private Telesophy.Alexandria.Clients.Ankh.Views.MediaItemDataGridView queueDataGrid;
		private System.Windows.Forms.ImageList queueSmallImageList;
		private System.Windows.Forms.ContextMenuStrip sortContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem sortContextMenuStripItemClearSelected;
		private System.Windows.Forms.ToolStripMenuItem OpenDirectoryToolStripMenuItem;
		private System.Windows.Forms.FolderBrowserDialog DirectoryOpenDialog;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearSelectedToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel currentStatusToolStripLabel;
		private System.Windows.Forms.CheckBox submitCheckBox;
		private System.Windows.Forms.ToolStripMenuItem saveSelectedCatalogToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteSelectedCatalogToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadCatalogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importCatalogToolStripMenuItem;
        private System.Windows.Forms.ImageList sortSmallImageList;
		private System.Windows.Forms.ToolStripMenuItem sortContextMenuStripItemClearAll;
        private System.Windows.Forms.ContextMenuStrip filterContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem filterContextMenuItemClearAll;
		private System.Windows.Forms.ImageList filterSmallImageList;
		private System.Windows.Forms.DataGridView taskDataGrid;
		private System.Windows.Forms.ImageList taskSmallImageList;
		private System.Windows.Forms.DataGridViewTextBoxColumn taskNameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn taskStatusColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn taskProgressColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn taskDetailsColumn;
		private System.Windows.Forms.ContextMenuStrip taskContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem taskPauseMenuItem;
		private System.Windows.Forms.ToolStripMenuItem taskCancelMenuItem;
		private System.Windows.Forms.ToolStripMenuItem taskCancelAllMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tasksToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pauseTaskToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cancelTaskToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cancelAllTasksToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem runTaskToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem taskRunMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolRipDiscMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filterContextMenuItemAddFilter;
		private System.Windows.Forms.ToolStripMenuItem filterContextMenuItemEditFilter;
		private System.Windows.Forms.ToolStripMenuItem filterContextMenuItemUpdateOperator;
		private System.Windows.Forms.ToolStripMenuItem filterOperatorItemAnd;
		private System.Windows.Forms.ToolStripMenuItem filterOperatorItemOr;
		private System.Windows.Forms.ToolStripMenuItem filterOperatorItemNot;
        private System.Windows.Forms.ToolStripMenuItem filterContextMenuItemClearSelected;
		private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addFilterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editFilterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem updateOperatorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem andOperatorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem orOperatorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem notOperatorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearFilterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearAllFiltersToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sortToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearSortToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearAllSortsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolCreatePlaylistMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolEditPlaylistMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolAddSelectedItemsToPlaylistMenuItem;
        private MediaItemSearchBox mediaItemSearchBox;
        private System.Windows.Forms.GroupBox SortGroupBox;
        private System.Windows.Forms.ListView sortListView;
        private System.Windows.Forms.Button sortButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
        private System.Windows.Forms.DataGridViewImageColumn typeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sourceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn artistColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn albumColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn durationColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn formatColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pathColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sourceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn artistDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn albumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn durationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn formatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.ComboBox modeComboBox;
        private System.Windows.Forms.Label modeLabel;
	}
}

