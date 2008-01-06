namespace Telesophy.Alexandria.Clients.Ankh.Views
{
	partial class AddFilter
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
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.valueTextBox = new System.Windows.Forms.TextBox();
			this.columnLabel = new System.Windows.Forms.Label();
			this.operatorLabel = new System.Windows.Forms.Label();
			this.valueLabel = new System.Windows.Forms.Label();
			this.columnTextBox = new System.Windows.Forms.TextBox();
			this.operatorComboBox = new System.Windows.Forms.ComboBox();
			this.sourceButton = new System.Windows.Forms.Button();
			this.typeButton = new System.Windows.Forms.Button();
			this.statusButton = new System.Windows.Forms.Button();
			this.numberButton = new System.Windows.Forms.Button();
			this.titleButton = new System.Windows.Forms.Button();
			this.artistButton = new System.Windows.Forms.Button();
			this.durationButton = new System.Windows.Forms.Button();
			this.albumButton = new System.Windows.Forms.Button();
			this.dateButton = new System.Windows.Forms.Button();
			this.formatButton = new System.Windows.Forms.Button();
			this.pathButton = new System.Windows.Forms.Button();
			this.anyButton = new System.Windows.Forms.Button();
			this.columnImageList = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(408, 237);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 99;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(322, 237);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 98;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// valueTextBox
			// 
			this.valueTextBox.Location = new System.Drawing.Point(322, 35);
			this.valueTextBox.Name = "valueTextBox";
			this.valueTextBox.Size = new System.Drawing.Size(161, 20);
			this.valueTextBox.TabIndex = 6;
			// 
			// columnLabel
			// 
			this.columnLabel.AutoSize = true;
			this.columnLabel.Location = new System.Drawing.Point(12, 17);
			this.columnLabel.Name = "columnLabel";
			this.columnLabel.Size = new System.Drawing.Size(42, 13);
			this.columnLabel.TabIndex = 1;
			this.columnLabel.Text = "Column";
			// 
			// operatorLabel
			// 
			this.operatorLabel.AutoSize = true;
			this.operatorLabel.Location = new System.Drawing.Point(186, 17);
			this.operatorLabel.Name = "operatorLabel";
			this.operatorLabel.Size = new System.Drawing.Size(48, 13);
			this.operatorLabel.TabIndex = 2;
			this.operatorLabel.Text = "Operator";
			// 
			// valueLabel
			// 
			this.valueLabel.AutoSize = true;
			this.valueLabel.Location = new System.Drawing.Point(325, 18);
			this.valueLabel.Name = "valueLabel";
			this.valueLabel.Size = new System.Drawing.Size(34, 13);
			this.valueLabel.TabIndex = 3;
			this.valueLabel.Text = "Value";
			// 
			// columnTextBox
			// 
			this.columnTextBox.Location = new System.Drawing.Point(13, 35);
			this.columnTextBox.Name = "columnTextBox";
			this.columnTextBox.Size = new System.Drawing.Size(156, 20);
			this.columnTextBox.TabIndex = 4;
			// 
			// operatorComboBox
			// 
			this.operatorComboBox.FormattingEnabled = true;
			this.operatorComboBox.Items.AddRange(new object[] {
            "LIKE",
            "IN",
            "=",
            "<>",
            ">",
            ">=",
            "<",
            "<="});
			this.operatorComboBox.Location = new System.Drawing.Point(184, 33);
			this.operatorComboBox.Name = "operatorComboBox";
			this.operatorComboBox.Size = new System.Drawing.Size(121, 21);
			this.operatorComboBox.TabIndex = 5;
			// 
			// sourceButton
			// 
			this.sourceButton.ImageList = this.columnImageList;
			this.sourceButton.Location = new System.Drawing.Point(94, 90);
			this.sourceButton.Name = "sourceButton";
			this.sourceButton.Size = new System.Drawing.Size(75, 23);
			this.sourceButton.TabIndex = 17;
			this.sourceButton.Text = "Source";
			this.sourceButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.sourceButton.UseVisualStyleBackColor = true;
			this.sourceButton.Click += new System.EventHandler(this.sourceButton_Click);
			// 
			// typeButton
			// 
			this.typeButton.ImageList = this.columnImageList;
			this.typeButton.Location = new System.Drawing.Point(13, 89);
			this.typeButton.Name = "typeButton";
			this.typeButton.Size = new System.Drawing.Size(75, 23);
			this.typeButton.TabIndex = 16;
			this.typeButton.Text = "Type";
			this.typeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.typeButton.UseVisualStyleBackColor = true;
			this.typeButton.Click += new System.EventHandler(this.typeButton_Click);
			// 
			// statusButton
			// 
			this.statusButton.ImageList = this.columnImageList;
			this.statusButton.Location = new System.Drawing.Point(93, 61);
			this.statusButton.Name = "statusButton";
			this.statusButton.Size = new System.Drawing.Size(75, 23);
			this.statusButton.TabIndex = 15;
			this.statusButton.Text = "Status";
			this.statusButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.statusButton.UseVisualStyleBackColor = true;
			this.statusButton.Click += new System.EventHandler(this.statusButton_Click);
			// 
			// numberButton
			// 
			this.numberButton.ImageList = this.columnImageList;
			this.numberButton.Location = new System.Drawing.Point(12, 118);
			this.numberButton.Name = "numberButton";
			this.numberButton.Size = new System.Drawing.Size(75, 23);
			this.numberButton.TabIndex = 18;
			this.numberButton.Text = "Number";
			this.numberButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.numberButton.UseVisualStyleBackColor = true;
			this.numberButton.Click += new System.EventHandler(this.numberButton_Click);
			// 
			// titleButton
			// 
			this.titleButton.ImageList = this.columnImageList;
			this.titleButton.Location = new System.Drawing.Point(93, 118);
			this.titleButton.Name = "titleButton";
			this.titleButton.Size = new System.Drawing.Size(75, 23);
			this.titleButton.TabIndex = 19;
			this.titleButton.Text = "Title";
			this.titleButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.titleButton.UseVisualStyleBackColor = true;
			this.titleButton.Click += new System.EventHandler(this.titleButton_Click);
			// 
			// artistButton
			// 
			this.artistButton.ImageList = this.columnImageList;
			this.artistButton.Location = new System.Drawing.Point(12, 147);
			this.artistButton.Name = "artistButton";
			this.artistButton.Size = new System.Drawing.Size(75, 23);
			this.artistButton.TabIndex = 20;
			this.artistButton.Text = "Artist";
			this.artistButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.artistButton.UseVisualStyleBackColor = true;
			this.artistButton.Click += new System.EventHandler(this.artistButton_Click);
			// 
			// durationButton
			// 
			this.durationButton.ImageList = this.columnImageList;
			this.durationButton.Location = new System.Drawing.Point(13, 176);
			this.durationButton.Name = "durationButton";
			this.durationButton.Size = new System.Drawing.Size(75, 23);
			this.durationButton.TabIndex = 22;
			this.durationButton.Text = "Duration";
			this.durationButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.durationButton.UseVisualStyleBackColor = true;
			this.durationButton.Click += new System.EventHandler(this.durationButton_Click);
			// 
			// albumButton
			// 
			this.albumButton.ImageList = this.columnImageList;
			this.albumButton.Location = new System.Drawing.Point(93, 148);
			this.albumButton.Name = "albumButton";
			this.albumButton.Size = new System.Drawing.Size(75, 23);
			this.albumButton.TabIndex = 21;
			this.albumButton.Text = "Album";
			this.albumButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.albumButton.UseVisualStyleBackColor = true;
			this.albumButton.Click += new System.EventHandler(this.albumButton_Click);
			// 
			// dateButton
			// 
			this.dateButton.ImageList = this.columnImageList;
			this.dateButton.Location = new System.Drawing.Point(93, 177);
			this.dateButton.Name = "dateButton";
			this.dateButton.Size = new System.Drawing.Size(75, 23);
			this.dateButton.TabIndex = 23;
			this.dateButton.Text = "Date";
			this.dateButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.dateButton.UseVisualStyleBackColor = true;
			this.dateButton.Click += new System.EventHandler(this.dateButton_Click);
			// 
			// formatButton
			// 
			this.formatButton.ImageList = this.columnImageList;
			this.formatButton.Location = new System.Drawing.Point(12, 205);
			this.formatButton.Name = "formatButton";
			this.formatButton.Size = new System.Drawing.Size(75, 23);
			this.formatButton.TabIndex = 24;
			this.formatButton.Text = "Format";
			this.formatButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.formatButton.UseVisualStyleBackColor = true;
			this.formatButton.Click += new System.EventHandler(this.formatButton_Click);
			// 
			// pathButton
			// 
			this.pathButton.ImageList = this.columnImageList;
			this.pathButton.Location = new System.Drawing.Point(93, 205);
			this.pathButton.Name = "pathButton";
			this.pathButton.Size = new System.Drawing.Size(75, 23);
			this.pathButton.TabIndex = 25;
			this.pathButton.Text = "Path";
			this.pathButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.pathButton.UseVisualStyleBackColor = true;
			this.pathButton.Click += new System.EventHandler(this.pathButton_Click);
			// 
			// anyButton
			// 
			this.anyButton.ImageList = this.columnImageList;
			this.anyButton.Location = new System.Drawing.Point(13, 60);
			this.anyButton.Name = "anyButton";
			this.anyButton.Size = new System.Drawing.Size(75, 23);
			this.anyButton.TabIndex = 14;
			this.anyButton.Text = "Any";
			this.anyButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.anyButton.UseVisualStyleBackColor = true;
			this.anyButton.Click += new System.EventHandler(this.anyButton_Click);
			// 
			// columnImageList
			// 
			this.columnImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.columnImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.columnImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// AddFilter
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(500, 272);
			this.Controls.Add(this.formatButton);
			this.Controls.Add(this.anyButton);
			this.Controls.Add(this.pathButton);
			this.Controls.Add(this.albumButton);
			this.Controls.Add(this.dateButton);
			this.Controls.Add(this.durationButton);
			this.Controls.Add(this.operatorComboBox);
			this.Controls.Add(this.titleButton);
			this.Controls.Add(this.artistButton);
			this.Controls.Add(this.statusButton);
			this.Controls.Add(this.numberButton);
			this.Controls.Add(this.columnTextBox);
			this.Controls.Add(this.typeButton);
			this.Controls.Add(this.columnLabel);
			this.Controls.Add(this.valueLabel);
			this.Controls.Add(this.operatorLabel);
			this.Controls.Add(this.sourceButton);
			this.Controls.Add(this.valueTextBox);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddFilter";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Add Filter";
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TextBox valueTextBox;
		private System.Windows.Forms.Label columnLabel;
		private System.Windows.Forms.Label operatorLabel;
		private System.Windows.Forms.Label valueLabel;
		private System.Windows.Forms.TextBox columnTextBox;
		private System.Windows.Forms.ComboBox operatorComboBox;
		private System.Windows.Forms.Button sourceButton;
		private System.Windows.Forms.Button typeButton;
		private System.Windows.Forms.Button statusButton;
		private System.Windows.Forms.Button numberButton;
		private System.Windows.Forms.Button titleButton;
		private System.Windows.Forms.Button artistButton;
		private System.Windows.Forms.Button durationButton;
		private System.Windows.Forms.Button albumButton;
		private System.Windows.Forms.Button dateButton;
		private System.Windows.Forms.Button formatButton;
		private System.Windows.Forms.Button pathButton;
		private System.Windows.Forms.Button anyButton;
		private System.Windows.Forms.ImageList columnImageList;
	}
}