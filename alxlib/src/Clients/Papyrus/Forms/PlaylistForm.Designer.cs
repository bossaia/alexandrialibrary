namespace Papyrus.Forms
{
    partial class PlaylistForm
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.lblTitle = new System.Windows.Forms.Label();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.btnLoad = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.lblCreator = new System.Windows.Forms.Label();
			this.txtCreator = new System.Windows.Forms.TextBox();
			this.lblIdentifier = new System.Windows.Forms.Label();
			this.txtIdentifier = new System.Windows.Forms.TextBox();
			this.lblDate = new System.Windows.Forms.Label();
			this.pnlPlaylist = new System.Windows.Forms.TableLayoutPanel();
			this.dtCreatedDate = new System.Windows.Forms.DateTimePicker();
			this.dgTags = new System.Windows.Forms.DataGridView();
			this.dgTracks = new System.Windows.Forms.DataGridView();
			this.colTrackSequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTrackTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTrackCreator = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTrackAlbum = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTrackNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTrackDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTrackAnnotation = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTrackImage = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTrackInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTrackIdentifier = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTrackLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lblTracks = new System.Windows.Forms.Label();
			this.btnAddTrack = new System.Windows.Forms.Button();
			this.errTitle = new System.Windows.Forms.ErrorProvider(this.components);
			this.errInfoUri = new System.Windows.Forms.ErrorProvider(this.components);
			this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
			this.errTracks = new System.Windows.Forms.ErrorProvider(this.components);
			this.colTagsSchema = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTagsKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colTagsValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lblTags = new System.Windows.Forms.Label();
			this.pnlPlaylist.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgTags)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgTracks)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errTitle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errInfoUri)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errTracks)).BeginInit();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblTitle.Location = new System.Drawing.Point(3, 2);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(68, 23);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "Title";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtTitle
			// 
			this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlPlaylist.SetColumnSpan(this.txtTitle, 2);
			this.txtTitle.Location = new System.Drawing.Point(83, 3);
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(258, 20);
			this.txtTitle.TabIndex = 1;
			// 
			// btnLoad
			// 
			this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoad.Location = new System.Drawing.Point(623, 438);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(75, 21);
			this.btnLoad.TabIndex = 3;
			this.btnLoad.Text = "Load";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(715, 438);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 21);
			this.btnSave.TabIndex = 4;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// lblCreator
			// 
			this.lblCreator.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblCreator.Location = new System.Drawing.Point(3, 29);
			this.lblCreator.Name = "lblCreator";
			this.lblCreator.Size = new System.Drawing.Size(67, 23);
			this.lblCreator.TabIndex = 5;
			this.lblCreator.Text = "Creator";
			this.lblCreator.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtCreator
			// 
			this.txtCreator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlPlaylist.SetColumnSpan(this.txtCreator, 2);
			this.txtCreator.Location = new System.Drawing.Point(83, 30);
			this.txtCreator.Name = "txtCreator";
			this.txtCreator.Size = new System.Drawing.Size(258, 20);
			this.txtCreator.TabIndex = 6;
			// 
			// lblIdentifier
			// 
			this.lblIdentifier.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblIdentifier.Location = new System.Drawing.Point(387, 2);
			this.lblIdentifier.Name = "lblIdentifier";
			this.lblIdentifier.Size = new System.Drawing.Size(67, 23);
			this.lblIdentifier.TabIndex = 9;
			this.lblIdentifier.Text = "Identifier";
			this.lblIdentifier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtIdentifier
			// 
			this.txtIdentifier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlPlaylist.SetColumnSpan(this.txtIdentifier, 2);
			this.txtIdentifier.Location = new System.Drawing.Point(460, 3);
			this.txtIdentifier.Name = "txtIdentifier";
			this.txtIdentifier.ReadOnly = true;
			this.txtIdentifier.Size = new System.Drawing.Size(318, 20);
			this.txtIdentifier.TabIndex = 10;
			// 
			// lblDate
			// 
			this.lblDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblDate.Location = new System.Drawing.Point(387, 29);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(67, 23);
			this.lblDate.TabIndex = 14;
			this.lblDate.Text = "Created";
			this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnlPlaylist
			// 
			this.pnlPlaylist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlPlaylist.AutoSize = true;
			this.pnlPlaylist.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.pnlPlaylist.ColumnCount = 8;
			this.pnlPlaylist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.pnlPlaylist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
			this.pnlPlaylist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
			this.pnlPlaylist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.pnlPlaylist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73F));
			this.pnlPlaylist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 161F));
			this.pnlPlaylist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 163F));
			this.pnlPlaylist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.pnlPlaylist.Controls.Add(this.dgTags, 1, 3);
			this.pnlPlaylist.Controls.Add(this.dgTracks, 1, 6);
			this.pnlPlaylist.Controls.Add(this.lblTracks, 0, 6);
			this.pnlPlaylist.Controls.Add(this.lblTags, 0, 3);
			this.pnlPlaylist.Controls.Add(this.btnAddTrack, 0, 7);
			this.pnlPlaylist.Controls.Add(this.lblTitle, 0, 0);
			this.pnlPlaylist.Controls.Add(this.txtTitle, 1, 0);
			this.pnlPlaylist.Controls.Add(this.lblCreator, 0, 1);
			this.pnlPlaylist.Controls.Add(this.txtIdentifier, 5, 0);
			this.pnlPlaylist.Controls.Add(this.lblIdentifier, 4, 0);
			this.pnlPlaylist.Controls.Add(this.txtCreator, 1, 1);
			this.pnlPlaylist.Controls.Add(this.lblDate, 4, 1);
			this.pnlPlaylist.Controls.Add(this.dtCreatedDate, 5, 1);
			this.pnlPlaylist.Location = new System.Drawing.Point(12, 12);
			this.pnlPlaylist.Name = "pnlPlaylist";
			this.pnlPlaylist.RowCount = 10;
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.pnlPlaylist.Size = new System.Drawing.Size(821, 403);
			this.pnlPlaylist.TabIndex = 15;
			// 
			// dtCreatedDate
			// 
			this.dtCreatedDate.CustomFormat = "";
			this.dtCreatedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtCreatedDate.Location = new System.Drawing.Point(460, 30);
			this.dtCreatedDate.Name = "dtCreatedDate";
			this.dtCreatedDate.Size = new System.Drawing.Size(110, 20);
			this.dtCreatedDate.TabIndex = 21;
			// 
			// dgTags
			// 
			this.dgTags.AllowUserToResizeRows = false;
			this.dgTags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgTags.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dgTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgTags.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTagsSchema,
            this.colTagsKey,
            this.colTagsValue});
			this.pnlPlaylist.SetColumnSpan(this.dgTags, 6);
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgTags.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgTags.Location = new System.Drawing.Point(83, 67);
			this.dgTags.Name = "dgTags";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgTags.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.pnlPlaylist.SetRowSpan(this.dgTags, 2);
			this.dgTags.Size = new System.Drawing.Size(695, 75);
			this.dgTags.TabIndex = 28;
			// 
			// dgTracks
			// 
			this.dgTracks.AllowUserToAddRows = false;
			this.dgTracks.AllowUserToResizeRows = false;
			this.dgTracks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgTracks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dgTracks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgTracks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTrackSequence,
            this.colTrackTitle,
            this.colTrackCreator,
            this.colTrackAlbum,
            this.colTrackNumber,
            this.colTrackDuration,
            this.colTrackAnnotation,
            this.colTrackImage,
            this.colTrackInfo,
            this.colTrackIdentifier,
            this.colTrackLocation});
			this.pnlPlaylist.SetColumnSpan(this.dgTracks, 6);
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgTracks.DefaultCellStyle = dataGridViewCellStyle5;
			this.dgTracks.Location = new System.Drawing.Point(83, 158);
			this.dgTracks.Name = "dgTracks";
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgTracks.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.pnlPlaylist.SetRowSpan(this.dgTracks, 2);
			this.dgTracks.Size = new System.Drawing.Size(695, 205);
			this.dgTracks.TabIndex = 34;
			// 
			// colTrackSequence
			// 
			this.colTrackSequence.HeaderText = "Seq";
			this.colTrackSequence.MinimumWidth = 40;
			this.colTrackSequence.Name = "colTrackSequence";
			this.colTrackSequence.ReadOnly = true;
			this.colTrackSequence.Width = 40;
			// 
			// colTrackTitle
			// 
			this.colTrackTitle.HeaderText = "Title";
			this.colTrackTitle.MinimumWidth = 50;
			this.colTrackTitle.Name = "colTrackTitle";
			this.colTrackTitle.Width = 120;
			// 
			// colTrackCreator
			// 
			this.colTrackCreator.HeaderText = "Artist";
			this.colTrackCreator.MinimumWidth = 50;
			this.colTrackCreator.Name = "colTrackCreator";
			this.colTrackCreator.Width = 120;
			// 
			// colTrackAlbum
			// 
			this.colTrackAlbum.HeaderText = "Album";
			this.colTrackAlbum.MinimumWidth = 50;
			this.colTrackAlbum.Name = "colTrackAlbum";
			this.colTrackAlbum.Width = 120;
			// 
			// colTrackNumber
			// 
			this.colTrackNumber.HeaderText = "Track #";
			this.colTrackNumber.MinimumWidth = 70;
			this.colTrackNumber.Name = "colTrackNumber";
			this.colTrackNumber.Width = 70;
			// 
			// colTrackDuration
			// 
			this.colTrackDuration.HeaderText = "Duration";
			this.colTrackDuration.Name = "colTrackDuration";
			// 
			// colTrackAnnotation
			// 
			this.colTrackAnnotation.HeaderText = "Note";
			this.colTrackAnnotation.Name = "colTrackAnnotation";
			// 
			// colTrackImage
			// 
			this.colTrackImage.HeaderText = "Image";
			this.colTrackImage.Name = "colTrackImage";
			// 
			// colTrackInfo
			// 
			this.colTrackInfo.HeaderText = "Info";
			this.colTrackInfo.Name = "colTrackInfo";
			// 
			// colTrackIdentifier
			// 
			this.colTrackIdentifier.HeaderText = "Identifier";
			this.colTrackIdentifier.Name = "colTrackIdentifier";
			// 
			// colTrackLocation
			// 
			this.colTrackLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colTrackLocation.HeaderText = "Location";
			this.colTrackLocation.MinimumWidth = 50;
			this.colTrackLocation.Name = "colTrackLocation";
			// 
			// lblTracks
			// 
			this.lblTracks.Location = new System.Drawing.Point(3, 155);
			this.lblTracks.Name = "lblTracks";
			this.lblTracks.Size = new System.Drawing.Size(68, 21);
			this.lblTracks.TabIndex = 35;
			this.lblTracks.Text = "Tracks";
			this.lblTracks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnAddTrack
			// 
			this.btnAddTrack.Location = new System.Drawing.Point(3, 185);
			this.btnAddTrack.Name = "btnAddTrack";
			this.btnAddTrack.Size = new System.Drawing.Size(74, 21);
			this.btnAddTrack.TabIndex = 36;
			this.btnAddTrack.Text = "Add Track";
			this.btnAddTrack.UseVisualStyleBackColor = true;
			this.btnAddTrack.Click += new System.EventHandler(this.btnAddTrack_Click);
			// 
			// errTitle
			// 
			this.errTitle.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.errTitle.ContainerControl = this;
			// 
			// errInfoUri
			// 
			this.errInfoUri.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.errInfoUri.ContainerControl = this;
			// 
			// dlgOpenFile
			// 
			this.dlgOpenFile.Filter = "XSPF Playlists|*.xspf|M3U Playlists|*.m3u|PLS Playlists|*.pls";
			this.dlgOpenFile.Title = "Load A Playlist";
			// 
			// errTracks
			// 
			this.errTracks.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.errTracks.ContainerControl = this;
			// 
			// colTagsSchema
			// 
			this.colTagsSchema.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colTagsSchema.FillWeight = 33F;
			this.colTagsSchema.HeaderText = "Schema";
			this.colTagsSchema.Name = "colTagsSchema";
			// 
			// colTagsKey
			// 
			this.colTagsKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colTagsKey.FillWeight = 33F;
			this.colTagsKey.HeaderText = "Key";
			this.colTagsKey.Name = "colTagsKey";
			// 
			// colTagsValue
			// 
			this.colTagsValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colTagsValue.FillWeight = 33F;
			this.colTagsValue.HeaderText = "Value";
			this.colTagsValue.Name = "colTagsValue";
			// 
			// lblTags
			// 
			this.lblTags.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblTags.Location = new System.Drawing.Point(3, 66);
			this.lblTags.Name = "lblTags";
			this.lblTags.Size = new System.Drawing.Size(68, 23);
			this.lblTags.TabIndex = 37;
			this.lblTags.Text = "Tags";
			this.lblTags.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// PlaylistForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(837, 480);
			this.Controls.Add(this.pnlPlaylist);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnLoad);
			this.Name = "PlaylistForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Papyrus";
			this.pnlPlaylist.ResumeLayout(false);
			this.pnlPlaylist.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgTags)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgTracks)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errTitle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errInfoUri)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errTracks)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblCreator;
		private System.Windows.Forms.TextBox txtCreator;
        private System.Windows.Forms.Label lblIdentifier;
		private System.Windows.Forms.TextBox txtIdentifier;
        private System.Windows.Forms.Label lblDate;
		private System.Windows.Forms.TableLayoutPanel pnlPlaylist;
		private System.Windows.Forms.DateTimePicker dtCreatedDate;
		private System.Windows.Forms.DataGridView dgTags;
        private System.Windows.Forms.DataGridView dgTracks;
        private System.Windows.Forms.Label lblTracks;
		private System.Windows.Forms.ErrorProvider errTitle;
		private System.Windows.Forms.ErrorProvider errInfoUri;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		private System.Windows.Forms.SaveFileDialog dlgSaveFile;
		private System.Windows.Forms.ErrorProvider errTracks;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTrackSequence;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTrackTitle;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTrackCreator;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTrackAlbum;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTrackNumber;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTrackDuration;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTrackAnnotation;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTrackImage;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTrackInfo;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTrackIdentifier;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTrackLocation;
		private System.Windows.Forms.Button btnAddTrack;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTagsSchema;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTagsKey;
		private System.Windows.Forms.DataGridViewTextBoxColumn colTagsValue;
		private System.Windows.Forms.Label lblTags;
    }
}

