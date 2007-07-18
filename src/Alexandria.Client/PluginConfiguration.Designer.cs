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
			this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.Version = new System.Windows.Forms.TextBox();
			this.VersionLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// OKButton
			// 
			this.OKButton.Location = new System.Drawing.Point(196, 231);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(75, 23);
			this.OKButton.TabIndex = 0;
			this.OKButton.Text = "OK";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// PluginDescription
			// 
			this.PluginDescription.Location = new System.Drawing.Point(12, 40);
			this.PluginDescription.Multiline = true;
			this.PluginDescription.Name = "PluginDescription";
			this.PluginDescription.ReadOnly = true;
			this.PluginDescription.Size = new System.Drawing.Size(259, 75);
			this.PluginDescription.TabIndex = 2;
			// 
			// maskedTextBox1
			// 
			this.maskedTextBox1.Location = new System.Drawing.Point(171, 125);
			this.maskedTextBox1.Name = "maskedTextBox1";
			this.maskedTextBox1.Size = new System.Drawing.Size(100, 20);
			this.maskedTextBox1.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 128);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "label1";
			// 
			// Version
			// 
			this.Version.Location = new System.Drawing.Point(61, 10);
			this.Version.Name = "Version";
			this.Version.ReadOnly = true;
			this.Version.Size = new System.Drawing.Size(100, 20);
			this.Version.TabIndex = 5;
			// 
			// VersionLabel
			// 
			this.VersionLabel.AutoSize = true;
			this.VersionLabel.Location = new System.Drawing.Point(13, 13);
			this.VersionLabel.Name = "VersionLabel";
			this.VersionLabel.Size = new System.Drawing.Size(42, 13);
			this.VersionLabel.TabIndex = 6;
			this.VersionLabel.Text = "Version";
			// 
			// PluginConfiguration
			// 
			this.AcceptButton = this.OKButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(283, 266);
			this.Controls.Add(this.VersionLabel);
			this.Controls.Add(this.Version);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.maskedTextBox1);
			this.Controls.Add(this.PluginDescription);
			this.Controls.Add(this.OKButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PluginConfiguration";
			this.Text = "Plugin Configuration";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.TextBox PluginDescription;
		private System.Windows.Forms.MaskedTextBox maskedTextBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox Version;
		private System.Windows.Forms.Label VersionLabel;
	}
}