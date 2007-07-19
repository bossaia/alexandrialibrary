namespace Alexandria.Client
{
	partial class PluginConfiguration
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginConfiguration));
			this.OKButton = new System.Windows.Forms.Button();
			this.PluginDescription = new System.Windows.Forms.TextBox();
			this.Version = new System.Windows.Forms.TextBox();
			this.VersionLabel = new System.Windows.Forms.Label();
			this.SettingsLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.SettingsGroupBox = new System.Windows.Forms.GroupBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.CancelBtn = new System.Windows.Forms.Button();
			this.SettingsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// OKButton
			// 
			this.OKButton.Location = new System.Drawing.Point(198, 206);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(75, 23);
			this.OKButton.TabIndex = 0;
			this.OKButton.Text = "OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// PluginDescription
			// 
			this.PluginDescription.Location = new System.Drawing.Point(12, 47);
			this.PluginDescription.Multiline = true;
			this.PluginDescription.Name = "PluginDescription";
			this.PluginDescription.ReadOnly = true;
			this.PluginDescription.Size = new System.Drawing.Size(342, 69);
			this.PluginDescription.TabIndex = 2;
			// 
			// Version
			// 
			this.Version.Location = new System.Drawing.Point(254, 10);
			this.Version.Name = "Version";
			this.Version.ReadOnly = true;
			this.Version.Size = new System.Drawing.Size(100, 20);
			this.Version.TabIndex = 5;
			// 
			// VersionLabel
			// 
			this.VersionLabel.AutoSize = true;
			this.VersionLabel.Location = new System.Drawing.Point(206, 13);
			this.VersionLabel.Name = "VersionLabel";
			this.VersionLabel.Size = new System.Drawing.Size(42, 13);
			this.VersionLabel.TabIndex = 6;
			this.VersionLabel.Text = "Version";
			// 
			// SettingsLayoutPanel
			// 
			this.SettingsLayoutPanel.AutoSize = true;
			this.SettingsLayoutPanel.ColumnCount = 2;
			this.SettingsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.SettingsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.SettingsLayoutPanel.Location = new System.Drawing.Point(6, 19);
			this.SettingsLayoutPanel.Name = "SettingsLayoutPanel";
			this.SettingsLayoutPanel.RowCount = 1;
			this.SettingsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.SettingsLayoutPanel.Size = new System.Drawing.Size(308, 21);
			this.SettingsLayoutPanel.TabIndex = 7;
			// 
			// SettingsGroupBox
			// 
			this.SettingsGroupBox.AutoSize = true;
			this.SettingsGroupBox.Controls.Add(this.SettingsLayoutPanel);
			this.SettingsGroupBox.Location = new System.Drawing.Point(12, 132);
			this.SettingsGroupBox.Name = "SettingsGroupBox";
			this.SettingsGroupBox.Size = new System.Drawing.Size(342, 59);
			this.SettingsGroupBox.TabIndex = 8;
			this.SettingsGroupBox.TabStop = false;
			this.SettingsGroupBox.Text = "Settings";
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkBox1.Location = new System.Drawing.Point(12, 12);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(65, 17);
			this.checkBox1.TabIndex = 9;
			this.checkBox1.Text = "Enabled";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// CancelBtn
			// 
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new System.Drawing.Point(279, 206);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(75, 23);
			this.CancelBtn.TabIndex = 10;
			this.CancelBtn.Text = "Cancel";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
			// 
			// PluginConfiguration
			// 
			this.AcceptButton = this.OKButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.CancelBtn;
			this.ClientSize = new System.Drawing.Size(366, 240);
			this.Controls.Add(this.CancelBtn);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.PluginDescription);
			this.Controls.Add(this.SettingsGroupBox);
			this.Controls.Add(this.VersionLabel);
			this.Controls.Add(this.Version);
			this.Controls.Add(this.OKButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PluginConfiguration";
			this.Text = "Plugin Configuration";
			this.SettingsGroupBox.ResumeLayout(false);
			this.SettingsGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.TextBox PluginDescription;
		private System.Windows.Forms.TextBox Version;
		private System.Windows.Forms.Label VersionLabel;
		private System.Windows.Forms.TableLayoutPanel SettingsLayoutPanel;
		private System.Windows.Forms.GroupBox SettingsGroupBox;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button CancelBtn;
	}
}