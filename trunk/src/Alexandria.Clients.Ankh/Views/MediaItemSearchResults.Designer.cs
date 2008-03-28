namespace Telesophy.Alexandria.Clients.Ankh.Views
{
	partial class MediaItemSearchResults
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MediaItemSearchResults));
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.instructionsLabel = new System.Windows.Forms.Label();
			this.grid = new Telesophy.Alexandria.Clients.Ankh.Views.MediaItemDataGridView();
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
			this.statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(734, 228);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(641, 228);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// instructionsLabel
			// 
			this.instructionsLabel.AutoSize = true;
			this.instructionsLabel.Location = new System.Drawing.Point(12, 25);
			this.instructionsLabel.Name = "instructionsLabel";
			this.instructionsLabel.Size = new System.Drawing.Size(0, 13);
			this.instructionsLabel.TabIndex = 3;
			// 
			// grid
			// 
			this.grid.AllowDrop = true;
			this.grid.AllowUserToAddRows = false;
			this.grid.AllowUserToDeleteRows = false;
			this.grid.AllowUserToResizeRows = false;
			this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.grid.AutoGenerateColumns = false;
			this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
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
            this.statusColumn});
			this.grid.Location = new System.Drawing.Point(12, 56);
			this.grid.Name = "grid";
			this.grid.ReadOnly = true;
			this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.grid.Size = new System.Drawing.Size(797, 150);
			this.grid.TabIndex = 0;
			// 
			// typeColumn
			// 
			this.typeColumn.DataPropertyName = "Type";
			this.typeColumn.HeaderText = "Type";
			this.typeColumn.MinimumWidth = 50;
			this.typeColumn.Name = "typeColumn";
			this.typeColumn.ReadOnly = true;
			this.typeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.typeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.typeColumn.Width = 50;
			// 
			// sourceColumn
			// 
			this.sourceColumn.DataPropertyName = "Source";
			this.sourceColumn.HeaderText = "Source";
			this.sourceColumn.MinimumWidth = 75;
			this.sourceColumn.Name = "sourceColumn";
			this.sourceColumn.ReadOnly = true;
			this.sourceColumn.Width = 75;
			// 
			// numberColumn
			// 
			this.numberColumn.DataPropertyName = "Number";
			this.numberColumn.HeaderText = "Number";
			this.numberColumn.MinimumWidth = 60;
			this.numberColumn.Name = "numberColumn";
			this.numberColumn.ReadOnly = true;
			this.numberColumn.Width = 60;
			// 
			// titleColumn
			// 
			this.titleColumn.DataPropertyName = "Title";
			this.titleColumn.HeaderText = "Title";
			this.titleColumn.Name = "titleColumn";
			this.titleColumn.ReadOnly = true;
			this.titleColumn.Width = 150;
			// 
			// artistColumn
			// 
			this.artistColumn.DataPropertyName = "Artist";
			this.artistColumn.HeaderText = "Artist";
			this.artistColumn.Name = "artistColumn";
			this.artistColumn.ReadOnly = true;
			this.artistColumn.Width = 150;
			// 
			// albumColumn
			// 
			this.albumColumn.DataPropertyName = "Album";
			this.albumColumn.HeaderText = "Album";
			this.albumColumn.Name = "albumColumn";
			this.albumColumn.ReadOnly = true;
			this.albumColumn.Width = 150;
			// 
			// durationColumn
			// 
			this.durationColumn.DataPropertyName = "Duration";
			this.durationColumn.HeaderText = "Duration";
			this.durationColumn.MinimumWidth = 60;
			this.durationColumn.Name = "durationColumn";
			this.durationColumn.ReadOnly = true;
			this.durationColumn.Width = 60;
			// 
			// dateColumn
			// 
			this.dateColumn.DataPropertyName = "Date";
			this.dateColumn.HeaderText = "Date";
			this.dateColumn.MinimumWidth = 60;
			this.dateColumn.Name = "dateColumn";
			this.dateColumn.ReadOnly = true;
			this.dateColumn.Width = 60;
			// 
			// formatColumn
			// 
			this.formatColumn.DataPropertyName = "Format";
			this.formatColumn.HeaderText = "Format";
			this.formatColumn.Name = "formatColumn";
			this.formatColumn.ReadOnly = true;
			this.formatColumn.Width = 60;
			// 
			// pathColumn
			// 
			this.pathColumn.DataPropertyName = "Path";
			this.pathColumn.HeaderText = "Path";
			this.pathColumn.Name = "pathColumn";
			this.pathColumn.ReadOnly = true;
			this.pathColumn.Width = 300;
			// 
			// idColumn
			// 
			this.idColumn.DataPropertyName = "Id";
			this.idColumn.HeaderText = "Id";
			this.idColumn.Name = "idColumn";
			this.idColumn.ReadOnly = true;
			this.idColumn.Visible = false;
			// 
			// statusColumn
			// 
			this.statusColumn.DataPropertyName = "Status";
			this.statusColumn.HeaderText = "Status";
			this.statusColumn.Name = "statusColumn";
			this.statusColumn.ReadOnly = true;
			this.statusColumn.Visible = false;
			// 
			// MediaItemSearchResults
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(821, 266);
			this.Controls.Add(this.instructionsLabel);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.grid);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MediaItemSearchResults";
			this.Text = "Search Results";
			((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MediaItemDataGridView grid;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label instructionsLabel;
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
		private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
	}
}