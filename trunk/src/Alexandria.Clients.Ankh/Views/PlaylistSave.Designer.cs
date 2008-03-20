namespace Telesophy.Alexandria.Clients.Ankh.Views
{
	partial class PlaylistSave
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlaylistSave));
			this.saveButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.titleTextBox = new System.Windows.Forms.TextBox();
			this.titleLabel = new System.Windows.Forms.Label();
			this.artistLabel = new System.Windows.Forms.Label();
			this.numberLabel = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.sourceLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// saveButton
			// 
			this.saveButton.Location = new System.Drawing.Point(313, 231);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(75, 23);
			this.saveButton.TabIndex = 0;
			this.saveButton.Text = "Save";
			this.saveButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(222, 231);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// titleTextBox
			// 
			this.titleTextBox.Location = new System.Drawing.Point(74, 13);
			this.titleTextBox.Name = "titleTextBox";
			this.titleTextBox.Size = new System.Drawing.Size(314, 20);
			this.titleTextBox.TabIndex = 2;
			// 
			// titleLabel
			// 
			this.titleLabel.AutoSize = true;
			this.titleLabel.Location = new System.Drawing.Point(15, 18);
			this.titleLabel.Name = "titleLabel";
			this.titleLabel.Size = new System.Drawing.Size(27, 13);
			this.titleLabel.TabIndex = 3;
			this.titleLabel.Text = "Title";
			// 
			// artistLabel
			// 
			this.artistLabel.AutoSize = true;
			this.artistLabel.Location = new System.Drawing.Point(15, 52);
			this.artistLabel.Name = "artistLabel";
			this.artistLabel.Size = new System.Drawing.Size(30, 13);
			this.artistLabel.TabIndex = 4;
			this.artistLabel.Text = "Artist";
			// 
			// numberLabel
			// 
			this.numberLabel.AutoSize = true;
			this.numberLabel.Location = new System.Drawing.Point(15, 86);
			this.numberLabel.Name = "numberLabel";
			this.numberLabel.Size = new System.Drawing.Size(44, 13);
			this.numberLabel.TabIndex = 5;
			this.numberLabel.Text = "Number";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(74, 48);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(314, 20);
			this.textBox1.TabIndex = 6;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(74, 120);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(100, 20);
			this.textBox2.TabIndex = 7;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(74, 84);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(100, 20);
			this.numericUpDown1.TabIndex = 8;
			// 
			// sourceLabel
			// 
			this.sourceLabel.AutoSize = true;
			this.sourceLabel.Location = new System.Drawing.Point(18, 123);
			this.sourceLabel.Name = "sourceLabel";
			this.sourceLabel.Size = new System.Drawing.Size(41, 13);
			this.sourceLabel.TabIndex = 9;
			this.sourceLabel.Text = "Source";
			// 
			// PlaylistSave
			// 
			this.AcceptButton = this.saveButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(400, 266);
			this.Controls.Add(this.sourceLabel);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.numberLabel);
			this.Controls.Add(this.artistLabel);
			this.Controls.Add(this.titleLabel);
			this.Controls.Add(this.titleTextBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.saveButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PlaylistSave";
			this.Text = "Playlist";
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TextBox titleTextBox;
		private System.Windows.Forms.Label titleLabel;
		private System.Windows.Forms.Label artistLabel;
		private System.Windows.Forms.Label numberLabel;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label sourceLabel;
	}
}