using Telesophy.Alexandria.Clients.Ankh.Views.Data;

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
			this.artistTextBox = new System.Windows.Forms.TextBox();
			this.sourceTextBox = new System.Windows.Forms.TextBox();
			this.numberPicker = new System.Windows.Forms.NumericUpDown();
			this.sourceLabel = new System.Windows.Forms.Label();
			this.dateLabel = new System.Windows.Forms.Label();
			this.pathLabel = new System.Windows.Forms.Label();
			this.pathTextBox = new System.Windows.Forms.TextBox();
			this.datePicker = new System.Windows.Forms.DateTimePicker();
			this.identifierLabel = new System.Windows.Forms.Label();
			this.identifierTextBox = new System.Windows.Forms.TextBox();
			this.formatLabel = new System.Windows.Forms.Label();
			this.formatTextBox = new System.Windows.Forms.TextBox();
			this.itemGrid = new Telesophy.Alexandria.Clients.Ankh.Views.MediaItemDataGridView();
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
			((System.ComponentModel.ISupportInitialize)(this.numberPicker)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.itemGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// saveButton
			// 
			this.saveButton.Location = new System.Drawing.Point(668, 477);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(75, 23);
			this.saveButton.TabIndex = 11;
			this.saveButton.Text = "Save";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(578, 477);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 10;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// titleTextBox
			// 
			this.titleTextBox.Location = new System.Drawing.Point(67, 55);
			this.titleTextBox.Name = "titleTextBox";
			this.titleTextBox.Size = new System.Drawing.Size(302, 20);
			this.titleTextBox.TabIndex = 2;
			// 
			// titleLabel
			// 
			this.titleLabel.AutoSize = true;
			this.titleLabel.Location = new System.Drawing.Point(17, 58);
			this.titleLabel.Name = "titleLabel";
			this.titleLabel.Size = new System.Drawing.Size(27, 13);
			this.titleLabel.TabIndex = 3;
			this.titleLabel.Text = "Title";
			// 
			// artistLabel
			// 
			this.artistLabel.AutoSize = true;
			this.artistLabel.Location = new System.Drawing.Point(393, 61);
			this.artistLabel.Name = "artistLabel";
			this.artistLabel.Size = new System.Drawing.Size(30, 13);
			this.artistLabel.TabIndex = 4;
			this.artistLabel.Text = "Artist";
			// 
			// numberLabel
			// 
			this.numberLabel.AutoSize = true;
			this.numberLabel.Location = new System.Drawing.Point(20, 100);
			this.numberLabel.Name = "numberLabel";
			this.numberLabel.Size = new System.Drawing.Size(44, 13);
			this.numberLabel.TabIndex = 5;
			this.numberLabel.Text = "Number";
			// 
			// artistTextBox
			// 
			this.artistTextBox.Location = new System.Drawing.Point(427, 58);
			this.artistTextBox.Name = "artistTextBox";
			this.artistTextBox.Size = new System.Drawing.Size(316, 20);
			this.artistTextBox.TabIndex = 3;
			// 
			// sourceTextBox
			// 
			this.sourceTextBox.Location = new System.Drawing.Point(211, 97);
			this.sourceTextBox.Name = "sourceTextBox";
			this.sourceTextBox.Size = new System.Drawing.Size(158, 20);
			this.sourceTextBox.TabIndex = 5;
			// 
			// numberPicker
			// 
			this.numberPicker.Location = new System.Drawing.Point(67, 98);
			this.numberPicker.Name = "numberPicker";
			this.numberPicker.Size = new System.Drawing.Size(58, 20);
			this.numberPicker.TabIndex = 4;
			// 
			// sourceLabel
			// 
			this.sourceLabel.AutoSize = true;
			this.sourceLabel.Location = new System.Drawing.Point(164, 104);
			this.sourceLabel.Name = "sourceLabel";
			this.sourceLabel.Size = new System.Drawing.Size(41, 13);
			this.sourceLabel.TabIndex = 9;
			this.sourceLabel.Text = "Source";
			// 
			// dateLabel
			// 
			this.dateLabel.AutoSize = true;
			this.dateLabel.Location = new System.Drawing.Point(393, 103);
			this.dateLabel.Name = "dateLabel";
			this.dateLabel.Size = new System.Drawing.Size(30, 13);
			this.dateLabel.TabIndex = 10;
			this.dateLabel.Text = "Date";
			// 
			// pathLabel
			// 
			this.pathLabel.AutoSize = true;
			this.pathLabel.Location = new System.Drawing.Point(258, 143);
			this.pathLabel.Name = "pathLabel";
			this.pathLabel.Size = new System.Drawing.Size(29, 13);
			this.pathLabel.TabIndex = 12;
			this.pathLabel.Text = "Path";
			// 
			// pathTextBox
			// 
			this.pathTextBox.Location = new System.Drawing.Point(291, 140);
			this.pathTextBox.Name = "pathTextBox";
			this.pathTextBox.ReadOnly = true;
			this.pathTextBox.Size = new System.Drawing.Size(452, 20);
			this.pathTextBox.TabIndex = 8;
			// 
			// datePicker
			// 
			this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.datePicker.Location = new System.Drawing.Point(429, 98);
			this.datePicker.Name = "datePicker";
			this.datePicker.Size = new System.Drawing.Size(95, 20);
			this.datePicker.TabIndex = 6;
			this.datePicker.Value = new System.DateTime(2008, 3, 23, 0, 0, 0, 0);
			// 
			// identifierLabel
			// 
			this.identifierLabel.AutoSize = true;
			this.identifierLabel.Location = new System.Drawing.Point(17, 22);
			this.identifierLabel.Name = "identifierLabel";
			this.identifierLabel.Size = new System.Drawing.Size(47, 13);
			this.identifierLabel.TabIndex = 15;
			this.identifierLabel.Text = "Identifier";
			// 
			// identifierTextBox
			// 
			this.identifierTextBox.Location = new System.Drawing.Point(67, 19);
			this.identifierTextBox.Name = "identifierTextBox";
			this.identifierTextBox.ReadOnly = true;
			this.identifierTextBox.Size = new System.Drawing.Size(302, 20);
			this.identifierTextBox.TabIndex = 1;
			// 
			// formatLabel
			// 
			this.formatLabel.AutoSize = true;
			this.formatLabel.Location = new System.Drawing.Point(22, 143);
			this.formatLabel.Name = "formatLabel";
			this.formatLabel.Size = new System.Drawing.Size(39, 13);
			this.formatLabel.TabIndex = 17;
			this.formatLabel.Text = "Format";
			// 
			// formatTextBox
			// 
			this.formatTextBox.Location = new System.Drawing.Point(67, 140);
			this.formatTextBox.Name = "formatTextBox";
			this.formatTextBox.ReadOnly = true;
			this.formatTextBox.Size = new System.Drawing.Size(163, 20);
			this.formatTextBox.TabIndex = 7;
			// 
			// itemGrid
			// 
			this.itemGrid.AllowDrop = true;
			this.itemGrid.AllowUserToAddRows = false;
			this.itemGrid.AllowUserToOrderColumns = true;
			this.itemGrid.AllowUserToResizeRows = false;
			this.itemGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.itemGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
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
            this.idColumn});
			this.itemGrid.Location = new System.Drawing.Point(25, 197);
			this.itemGrid.MultiSelect = false;
			this.itemGrid.Name = "itemGrid";
			this.itemGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.itemGrid.Size = new System.Drawing.Size(718, 260);
			this.itemGrid.TabIndex = 18;
			this.itemGrid.DragOver += new System.Windows.Forms.DragEventHandler(this.itemGrid_DragOver);
			this.itemGrid.DragEnter += new System.Windows.Forms.DragEventHandler(this.itemGrid_DragEnter);
			this.itemGrid.DragDrop += new System.Windows.Forms.DragEventHandler(this.itemGrid_DragDrop);
			// 
			// typeColumn
			// 
			this.typeColumn.DataPropertyName = "Type";
			this.typeColumn.HeaderText = "Type";
			this.typeColumn.Name = "typeColumn";
			this.typeColumn.ReadOnly = true;
			this.typeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.typeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// sourceColumn
			// 
			this.sourceColumn.DataPropertyName = "Source";
			this.sourceColumn.HeaderText = "Source";
			this.sourceColumn.Name = "sourceColumn";
			this.sourceColumn.ReadOnly = true;
			this.sourceColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// numberColumn
			// 
			this.numberColumn.DataPropertyName = "Number";
			this.numberColumn.HeaderText = "Number";
			this.numberColumn.Name = "numberColumn";
			this.numberColumn.ReadOnly = true;
			this.numberColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// titleColumn
			// 
			this.titleColumn.DataPropertyName = "Title";
			this.titleColumn.HeaderText = "Title";
			this.titleColumn.Name = "titleColumn";
			this.titleColumn.ReadOnly = true;
			this.titleColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// artistColumn
			// 
			this.artistColumn.DataPropertyName = "Artist";
			this.artistColumn.HeaderText = "Artist";
			this.artistColumn.Name = "artistColumn";
			this.artistColumn.ReadOnly = true;
			this.artistColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// albumColumn
			// 
			this.albumColumn.DataPropertyName = "Album";
			this.albumColumn.HeaderText = "Album";
			this.albumColumn.Name = "albumColumn";
			this.albumColumn.ReadOnly = true;
			this.albumColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// durationColumn
			// 
			this.durationColumn.DataPropertyName = "Duration";
			this.durationColumn.HeaderText = "Duration";
			this.durationColumn.Name = "durationColumn";
			this.durationColumn.ReadOnly = true;
			this.durationColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// dateColumn
			// 
			this.dateColumn.DataPropertyName = "Date";
			this.dateColumn.HeaderText = "Date";
			this.dateColumn.Name = "dateColumn";
			this.dateColumn.ReadOnly = true;
			this.dateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// formatColumn
			// 
			this.formatColumn.DataPropertyName = "Format";
			this.formatColumn.HeaderText = "Format";
			this.formatColumn.Name = "formatColumn";
			this.formatColumn.ReadOnly = true;
			this.formatColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// pathColumn
			// 
			this.pathColumn.DataPropertyName = "Path";
			this.pathColumn.HeaderText = "Path";
			this.pathColumn.Name = "pathColumn";
			this.pathColumn.ReadOnly = true;
			this.pathColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			// 
			// idColumn
			// 
			this.idColumn.DataPropertyName = "Id";
			this.idColumn.HeaderText = "Id";
			this.idColumn.Name = "idColumn";
			this.idColumn.ReadOnly = true;
			this.idColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
			this.idColumn.Visible = false;
			// 
			// PlaylistSave
			// 
			this.AcceptButton = this.saveButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(766, 512);
			this.Controls.Add(this.itemGrid);
			this.Controls.Add(this.formatTextBox);
			this.Controls.Add(this.formatLabel);
			this.Controls.Add(this.identifierTextBox);
			this.Controls.Add(this.identifierLabel);
			this.Controls.Add(this.datePicker);
			this.Controls.Add(this.pathTextBox);
			this.Controls.Add(this.pathLabel);
			this.Controls.Add(this.dateLabel);
			this.Controls.Add(this.sourceLabel);
			this.Controls.Add(this.numberPicker);
			this.Controls.Add(this.sourceTextBox);
			this.Controls.Add(this.artistTextBox);
			this.Controls.Add(this.numberLabel);
			this.Controls.Add(this.artistLabel);
			this.Controls.Add(this.titleLabel);
			this.Controls.Add(this.titleTextBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.saveButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PlaylistSave";
			this.Text = "Playlist";
			((System.ComponentModel.ISupportInitialize)(this.numberPicker)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.itemGrid)).EndInit();
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
		private System.Windows.Forms.TextBox artistTextBox;
		private System.Windows.Forms.TextBox sourceTextBox;
		private System.Windows.Forms.NumericUpDown numberPicker;
		private System.Windows.Forms.Label sourceLabel;
		private System.Windows.Forms.Label dateLabel;
		private System.Windows.Forms.Label pathLabel;
		private System.Windows.Forms.TextBox pathTextBox;
		private System.Windows.Forms.DateTimePicker datePicker;
		private System.Windows.Forms.Label identifierLabel;
		private System.Windows.Forms.TextBox identifierTextBox;
		private System.Windows.Forms.Label formatLabel;
		private System.Windows.Forms.TextBox formatTextBox;
		private MediaItemDataGridView itemGrid;
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
	}
}