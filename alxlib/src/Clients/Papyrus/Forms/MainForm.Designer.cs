namespace Papyrus.Forms
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Catalogs", 0, 0);
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Devices", 2, 2);
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Playlists", 6, 6);
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Services", 7, 7);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.mainPanel = new System.Windows.Forms.Panel();
			this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
			this.toolPanel = new System.Windows.Forms.Panel();
			this.toolTreeView = new System.Windows.Forms.TreeView();
			this.toolImageList = new System.Windows.Forms.ImageList(this.components);
			this.actionPanel = new System.Windows.Forms.Panel();
			this.actionTabControl = new System.Windows.Forms.TabControl();
			this.playTab = new System.Windows.Forms.TabPage();
			this.searchTab = new System.Windows.Forms.TabPage();
			this.browseTab = new System.Windows.Forms.TabPage();
			this.tagTab = new System.Windows.Forms.TabPage();
			this.actionImageList = new System.Windows.Forms.ImageList(this.components);
			this.mainPanel.SuspendLayout();
			this.mainSplitContainer.Panel1.SuspendLayout();
			this.mainSplitContainer.Panel2.SuspendLayout();
			this.mainSplitContainer.SuspendLayout();
			this.toolPanel.SuspendLayout();
			this.actionPanel.SuspendLayout();
			this.actionTabControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainPanel
			// 
			this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.mainPanel.Controls.Add(this.mainSplitContainer);
			this.mainPanel.Location = new System.Drawing.Point(12, 12);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(910, 242);
			this.mainPanel.TabIndex = 0;
			// 
			// mainSplitContainer
			// 
			this.mainSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.mainSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.mainSplitContainer.Name = "mainSplitContainer";
			// 
			// mainSplitContainer.Panel1
			// 
			this.mainSplitContainer.Panel1.Controls.Add(this.toolPanel);
			// 
			// mainSplitContainer.Panel2
			// 
			this.mainSplitContainer.Panel2.Controls.Add(this.actionPanel);
			this.mainSplitContainer.Size = new System.Drawing.Size(910, 242);
			this.mainSplitContainer.SplitterDistance = 226;
			this.mainSplitContainer.TabIndex = 0;
			// 
			// toolPanel
			// 
			this.toolPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.toolPanel.Controls.Add(this.toolTreeView);
			this.toolPanel.Location = new System.Drawing.Point(3, 0);
			this.toolPanel.Name = "toolPanel";
			this.toolPanel.Size = new System.Drawing.Size(447, 239);
			this.toolPanel.TabIndex = 0;
			// 
			// toolTreeView
			// 
			this.toolTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.toolTreeView.ImageIndex = 0;
			this.toolTreeView.ImageList = this.toolImageList;
			this.toolTreeView.Location = new System.Drawing.Point(3, 3);
			this.toolTreeView.Name = "toolTreeView";
			treeNode1.ImageIndex = 0;
			treeNode1.Name = "CatalogNode";
			treeNode1.SelectedImageIndex = 0;
			treeNode1.Text = "Catalogs";
			treeNode2.ImageIndex = 2;
			treeNode2.Name = "DeviceNode";
			treeNode2.SelectedImageIndex = 2;
			treeNode2.Text = "Devices";
			treeNode3.ImageIndex = 6;
			treeNode3.Name = "PlaylistNode";
			treeNode3.SelectedImageIndex = 6;
			treeNode3.Text = "Playlists";
			treeNode4.ImageIndex = 7;
			treeNode4.Name = "ServiceNode";
			treeNode4.SelectedImageIndex = 7;
			treeNode4.Text = "Services";
			this.toolTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
			this.toolTreeView.SelectedImageIndex = 0;
			this.toolTreeView.Size = new System.Drawing.Size(221, 233);
			this.toolTreeView.TabIndex = 0;
			// 
			// toolImageList
			// 
			this.toolImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolImageList.ImageStream")));
			this.toolImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.toolImageList.Images.SetKeyName(0, "catalog.png");
			this.toolImageList.Images.SetKeyName(1, "catalog_folder.png");
			this.toolImageList.Images.SetKeyName(2, "device.png");
			this.toolImageList.Images.SetKeyName(3, "device_disc.png");
			this.toolImageList.Images.SetKeyName(4, "device_ipod.png");
			this.toolImageList.Images.SetKeyName(5, "device_phone.png");
			this.toolImageList.Images.SetKeyName(6, "playlist.png");
			this.toolImageList.Images.SetKeyName(7, "service.png");
			this.toolImageList.Images.SetKeyName(8, "service_email.png");
			this.toolImageList.Images.SetKeyName(9, "service_feed.png");
			// 
			// actionPanel
			// 
			this.actionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.actionPanel.Controls.Add(this.actionTabControl);
			this.actionPanel.Location = new System.Drawing.Point(3, 3);
			this.actionPanel.Name = "actionPanel";
			this.actionPanel.Size = new System.Drawing.Size(674, 236);
			this.actionPanel.TabIndex = 0;
			// 
			// actionTabControl
			// 
			this.actionTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.actionTabControl.Controls.Add(this.playTab);
			this.actionTabControl.Controls.Add(this.searchTab);
			this.actionTabControl.Controls.Add(this.browseTab);
			this.actionTabControl.Controls.Add(this.tagTab);
			this.actionTabControl.ImageList = this.actionImageList;
			this.actionTabControl.Location = new System.Drawing.Point(3, 3);
			this.actionTabControl.Name = "actionTabControl";
			this.actionTabControl.SelectedIndex = 0;
			this.actionTabControl.Size = new System.Drawing.Size(668, 233);
			this.actionTabControl.TabIndex = 0;
			// 
			// playTab
			// 
			this.playTab.ImageIndex = 0;
			this.playTab.Location = new System.Drawing.Point(4, 23);
			this.playTab.Name = "playTab";
			this.playTab.Padding = new System.Windows.Forms.Padding(3);
			this.playTab.Size = new System.Drawing.Size(660, 206);
			this.playTab.TabIndex = 0;
			this.playTab.Text = "Play";
			this.playTab.UseVisualStyleBackColor = true;
			// 
			// searchTab
			// 
			this.searchTab.ImageIndex = 1;
			this.searchTab.Location = new System.Drawing.Point(4, 23);
			this.searchTab.Name = "searchTab";
			this.searchTab.Padding = new System.Windows.Forms.Padding(3);
			this.searchTab.Size = new System.Drawing.Size(660, 206);
			this.searchTab.TabIndex = 1;
			this.searchTab.Text = "Search";
			this.searchTab.UseVisualStyleBackColor = true;
			// 
			// browseTab
			// 
			this.browseTab.ImageIndex = 2;
			this.browseTab.Location = new System.Drawing.Point(4, 23);
			this.browseTab.Name = "browseTab";
			this.browseTab.Padding = new System.Windows.Forms.Padding(3);
			this.browseTab.Size = new System.Drawing.Size(660, 206);
			this.browseTab.TabIndex = 2;
			this.browseTab.Text = "Browse";
			this.browseTab.UseVisualStyleBackColor = true;
			// 
			// tagTab
			// 
			this.tagTab.ImageIndex = 3;
			this.tagTab.Location = new System.Drawing.Point(4, 23);
			this.tagTab.Name = "tagTab";
			this.tagTab.Padding = new System.Windows.Forms.Padding(3);
			this.tagTab.Size = new System.Drawing.Size(660, 206);
			this.tagTab.TabIndex = 3;
			this.tagTab.Text = "Tag";
			this.tagTab.UseVisualStyleBackColor = true;
			// 
			// actionImageList
			// 
			this.actionImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("actionImageList.ImageStream")));
			this.actionImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.actionImageList.Images.SetKeyName(0, "play.png");
			this.actionImageList.Images.SetKeyName(1, "search.png");
			this.actionImageList.Images.SetKeyName(2, "browse.png");
			this.actionImageList.Images.SetKeyName(3, "tag.png");
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(934, 266);
			this.Controls.Add(this.mainPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Alexandria";
			this.mainPanel.ResumeLayout(false);
			this.mainSplitContainer.Panel1.ResumeLayout(false);
			this.mainSplitContainer.Panel2.ResumeLayout(false);
			this.mainSplitContainer.ResumeLayout(false);
			this.toolPanel.ResumeLayout(false);
			this.actionPanel.ResumeLayout(false);
			this.actionTabControl.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel mainPanel;
		private System.Windows.Forms.SplitContainer mainSplitContainer;
		private System.Windows.Forms.Panel actionPanel;
		private System.Windows.Forms.Panel toolPanel;
		private System.Windows.Forms.TreeView toolTreeView;
		private System.Windows.Forms.TabControl actionTabControl;
		private System.Windows.Forms.TabPage playTab;
		private System.Windows.Forms.TabPage searchTab;
		private System.Windows.Forms.TabPage browseTab;
		private System.Windows.Forms.TabPage tagTab;
		private System.Windows.Forms.ImageList toolImageList;
		private System.Windows.Forms.ImageList actionImageList;
	}
}