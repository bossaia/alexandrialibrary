namespace Telesophy.Alexandria.Clients.Ankh.Views
{
	partial class About
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
			this.OKButton = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.PluginListView = new System.Windows.Forms.ListView();
			this.columnPlugin = new System.Windows.Forms.ColumnHeader();
			this.columnVersion = new System.Windows.Forms.ColumnHeader();
			this.VersionLabel = new System.Windows.Forms.Label();
			this.FreeSoftwareLabel = new System.Windows.Forms.Label();
			this.VersionTextBox = new System.Windows.Forms.TextBox();
			this.LicenseGroupBox = new System.Windows.Forms.GroupBox();
			this.LicenseTextBox = new System.Windows.Forms.TextBox();
			this.ImageList = new System.Windows.Forms.ImageList(this.components);
			this.groupBox1.SuspendLayout();
			this.LicenseGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// OKButton
			// 
			this.OKButton.Location = new System.Drawing.Point(384, 435);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(75, 23);
			this.OKButton.TabIndex = 2;
			this.OKButton.Text = "OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.PluginListView);
			this.groupBox1.Location = new System.Drawing.Point(16, 235);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(455, 184);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Installed Plugins";
			// 
			// PluginListView
			// 
			this.PluginListView.AutoArrange = false;
			this.PluginListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPlugin,
            this.columnVersion});
			this.PluginListView.FullRowSelect = true;
			this.PluginListView.Location = new System.Drawing.Point(14, 20);
			this.PluginListView.MultiSelect = false;
			this.PluginListView.Name = "PluginListView";
			this.PluginListView.ShowItemToolTips = true;
			this.PluginListView.Size = new System.Drawing.Size(427, 149);
			this.PluginListView.SmallImageList = this.ImageList;
			this.PluginListView.TabIndex = 1;
			this.PluginListView.UseCompatibleStateImageBehavior = false;
			this.PluginListView.View = System.Windows.Forms.View.Details;
			// 
			// columnPlugin
			// 
			this.columnPlugin.Text = "Plugin";
			this.columnPlugin.Width = 336;
			// 
			// columnVersion
			// 
			this.columnVersion.Text = "Version";
			this.columnVersion.Width = 70;
			// 
			// VersionLabel
			// 
			this.VersionLabel.AutoSize = true;
			this.VersionLabel.Location = new System.Drawing.Point(13, 9);
			this.VersionLabel.Name = "VersionLabel";
			this.VersionLabel.Size = new System.Drawing.Size(45, 13);
			this.VersionLabel.TabIndex = 3;
			this.VersionLabel.Text = "Version:";
			// 
			// FreeSoftwareLabel
			// 
			this.FreeSoftwareLabel.Location = new System.Drawing.Point(12, 435);
			this.FreeSoftwareLabel.Name = "FreeSoftwareLabel";
			this.FreeSoftwareLabel.Size = new System.Drawing.Size(340, 35);
			this.FreeSoftwareLabel.TabIndex = 5;
			this.FreeSoftwareLabel.Text = "Alexandria is and always will be free software (as in beer and speech).  If you p" +
				"aid money for it Karl and Leon are very disappointed with you.";
			// 
			// VersionTextBox
			// 
			this.VersionTextBox.Location = new System.Drawing.Point(60, 6);
			this.VersionTextBox.Name = "VersionTextBox";
			this.VersionTextBox.ReadOnly = true;
			this.VersionTextBox.Size = new System.Drawing.Size(124, 20);
			this.VersionTextBox.TabIndex = 8;
			// 
			// LicenseGroupBox
			// 
			this.LicenseGroupBox.Controls.Add(this.LicenseTextBox);
			this.LicenseGroupBox.Location = new System.Drawing.Point(16, 46);
			this.LicenseGroupBox.Name = "LicenseGroupBox";
			this.LicenseGroupBox.Size = new System.Drawing.Size(455, 174);
			this.LicenseGroupBox.TabIndex = 9;
			this.LicenseGroupBox.TabStop = false;
			this.LicenseGroupBox.Text = "License";
			// 
			// LicenseTextBox
			// 
			this.LicenseTextBox.Location = new System.Drawing.Point(14, 21);
			this.LicenseTextBox.Multiline = true;
			this.LicenseTextBox.Name = "LicenseTextBox";
			this.LicenseTextBox.ReadOnly = true;
			this.LicenseTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.LicenseTextBox.Size = new System.Drawing.Size(427, 139);
			this.LicenseTextBox.TabIndex = 0;
			// 
			// ImageList
			// 
			this.ImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.ImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// About
			// 
			this.AcceptButton = this.OKButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(483, 473);
			this.Controls.Add(this.LicenseGroupBox);
			this.Controls.Add(this.VersionTextBox);
			this.Controls.Add(this.FreeSoftwareLabel);
			this.Controls.Add(this.VersionLabel);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.OKButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "About";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "About Alexandria";
			this.groupBox1.ResumeLayout(false);
			this.LicenseGroupBox.ResumeLayout(false);
			this.LicenseGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label VersionLabel;
		private System.Windows.Forms.Label FreeSoftwareLabel;
		private System.Windows.Forms.TextBox VersionTextBox;
		private System.Windows.Forms.GroupBox LicenseGroupBox;
		private System.Windows.Forms.TextBox LicenseTextBox;
		private System.Windows.Forms.ListView PluginListView;
		private System.Windows.Forms.ColumnHeader columnPlugin;
		private System.Windows.Forms.ColumnHeader columnVersion;
		private System.Windows.Forms.ImageList ImageList;
	}
}