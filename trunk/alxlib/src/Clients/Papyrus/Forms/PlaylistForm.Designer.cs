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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
			this.lblTitle = new System.Windows.Forms.Label();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.dgAttribution = new System.Windows.Forms.DataGridView();
			this.colAttributionValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.btnLoad = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.lblCreator = new System.Windows.Forms.Label();
			this.txtCreator = new System.Windows.Forms.TextBox();
			this.lblComment = new System.Windows.Forms.Label();
			this.lblInfo = new System.Windows.Forms.Label();
			this.txtInfo = new System.Windows.Forms.TextBox();
			this.lblLocation = new System.Windows.Forms.Label();
			this.lblIdentifier = new System.Windows.Forms.Label();
			this.lblImage = new System.Windows.Forms.Label();
			this.lblDate = new System.Windows.Forms.Label();
			this.pnlPlaylist = new System.Windows.Forms.TableLayoutPanel();
			this.txtIndentifier = new System.Windows.Forms.TextBox();
			this.txtLocation = new System.Windows.Forms.TextBox();
			this.txtImage = new System.Windows.Forms.TextBox();
			this.dtCreatedDate = new System.Windows.Forms.DateTimePicker();
			this.dtCreatedTime = new System.Windows.Forms.DateTimePicker();
			this.lblLicense = new System.Windows.Forms.Label();
			this.cbLicense = new System.Windows.Forms.ComboBox();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.pnlAdditional = new System.Windows.Forms.TableLayoutPanel();
			this.lblMetadata = new System.Windows.Forms.Label();
			this.dgMetadata = new System.Windows.Forms.DataGridView();
			this.colMetaRel = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colMetaValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lblLinks = new System.Windows.Forms.Label();
			this.dgLinks = new System.Windows.Forms.DataGridView();
			this.colLinkRel = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colLinkValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lblExtensions = new System.Windows.Forms.Label();
			this.lblAttribution = new System.Windows.Forms.Label();
			this.dgExtensions = new System.Windows.Forms.DataGridView();
			this.colExtensionApplication = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colExtensionValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
			this.lnkAdditional = new System.Windows.Forms.LinkLabel();
			this.errTitle = new System.Windows.Forms.ErrorProvider(this.components);
			this.errInfoUri = new System.Windows.Forms.ErrorProvider(this.components);
			this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
			this.errTracks = new System.Windows.Forms.ErrorProvider(this.components);
			this.btnAddTrack = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgAttribution)).BeginInit();
			this.pnlPlaylist.SuspendLayout();
			this.pnlAdditional.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgMetadata)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgLinks)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgExtensions)).BeginInit();
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
			// dgAttribution
			// 
			this.dgAttribution.AllowUserToResizeColumns = false;
			this.dgAttribution.AllowUserToResizeRows = false;
			this.dgAttribution.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgAttribution.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
			this.dgAttribution.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgAttribution.ColumnHeadersVisible = false;
			this.dgAttribution.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAttributionValue});
			dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgAttribution.DefaultCellStyle = dataGridViewCellStyle20;
			this.dgAttribution.Location = new System.Drawing.Point(77, 3);
			this.dgAttribution.Name = "dgAttribution";
			dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgAttribution.RowHeadersDefaultCellStyle = dataGridViewCellStyle21;
			this.dgAttribution.Size = new System.Drawing.Size(695, 44);
			this.dgAttribution.TabIndex = 2;
			// 
			// colAttributionValue
			// 
			this.colAttributionValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colAttributionValue.HeaderText = "Attribution";
			this.colAttributionValue.Name = "colAttributionValue";
			this.colAttributionValue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// btnLoad
			// 
			this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoad.Location = new System.Drawing.Point(540, 692);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(75, 23);
			this.btnLoad.TabIndex = 3;
			this.btnLoad.Text = "Load";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(703, 692);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 4;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// lblCreator
			// 
			this.lblCreator.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblCreator.Location = new System.Drawing.Point(387, 2);
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
			this.txtCreator.Location = new System.Drawing.Point(460, 3);
			this.txtCreator.Name = "txtCreator";
			this.txtCreator.Size = new System.Drawing.Size(318, 20);
			this.txtCreator.TabIndex = 6;
			// 
			// lblComment
			// 
			this.lblComment.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblComment.Location = new System.Drawing.Point(387, 82);
			this.lblComment.Name = "lblComment";
			this.lblComment.Size = new System.Drawing.Size(67, 25);
			this.lblComment.TabIndex = 8;
			this.lblComment.Text = "Comment";
			this.lblComment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblInfo
			// 
			this.lblInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblInfo.Location = new System.Drawing.Point(3, 29);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(68, 23);
			this.lblInfo.TabIndex = 9;
			this.lblInfo.Text = "Info Uri";
			this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtInfo
			// 
			this.txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlPlaylist.SetColumnSpan(this.txtInfo, 2);
			this.txtInfo.Location = new System.Drawing.Point(83, 30);
			this.txtInfo.Name = "txtInfo";
			this.txtInfo.Size = new System.Drawing.Size(258, 20);
			this.txtInfo.TabIndex = 10;
			// 
			// lblLocation
			// 
			this.lblLocation.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblLocation.Location = new System.Drawing.Point(3, 56);
			this.lblLocation.Name = "lblLocation";
			this.lblLocation.Size = new System.Drawing.Size(68, 23);
			this.lblLocation.TabIndex = 11;
			this.lblLocation.Text = "Location Uri";
			this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblIdentifier
			// 
			this.lblIdentifier.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblIdentifier.Location = new System.Drawing.Point(3, 83);
			this.lblIdentifier.Name = "lblIdentifier";
			this.lblIdentifier.Size = new System.Drawing.Size(68, 23);
			this.lblIdentifier.TabIndex = 12;
			this.lblIdentifier.Text = "Identifier Uri";
			this.lblIdentifier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblImage
			// 
			this.lblImage.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblImage.Location = new System.Drawing.Point(3, 110);
			this.lblImage.Name = "lblImage";
			this.lblImage.Size = new System.Drawing.Size(68, 23);
			this.lblImage.TabIndex = 13;
			this.lblImage.Text = "Image Uri";
			this.lblImage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.pnlPlaylist.Controls.Add(this.txtTitle, 1, 0);
			this.pnlPlaylist.Controls.Add(this.btnLoad, 5, 11);
			this.pnlPlaylist.Controls.Add(this.lblTitle, 0, 0);
			this.pnlPlaylist.Controls.Add(this.btnSave, 6, 11);
			this.pnlPlaylist.Controls.Add(this.lblInfo, 0, 1);
			this.pnlPlaylist.Controls.Add(this.txtInfo, 1, 1);
			this.pnlPlaylist.Controls.Add(this.txtCreator, 5, 0);
			this.pnlPlaylist.Controls.Add(this.lblCreator, 4, 0);
			this.pnlPlaylist.Controls.Add(this.lblIdentifier, 0, 3);
			this.pnlPlaylist.Controls.Add(this.txtIndentifier, 1, 3);
			this.pnlPlaylist.Controls.Add(this.lblLocation, 0, 2);
			this.pnlPlaylist.Controls.Add(this.txtLocation, 1, 2);
			this.pnlPlaylist.Controls.Add(this.lblImage, 0, 4);
			this.pnlPlaylist.Controls.Add(this.txtImage, 1, 4);
			this.pnlPlaylist.Controls.Add(this.lblDate, 4, 1);
			this.pnlPlaylist.Controls.Add(this.dtCreatedDate, 5, 1);
			this.pnlPlaylist.Controls.Add(this.dtCreatedTime, 6, 1);
			this.pnlPlaylist.Controls.Add(this.lblLicense, 4, 2);
			this.pnlPlaylist.Controls.Add(this.cbLicense, 5, 2);
			this.pnlPlaylist.Controls.Add(this.lblComment, 4, 3);
			this.pnlPlaylist.Controls.Add(this.txtComment, 5, 3);
			this.pnlPlaylist.Controls.Add(this.pnlAdditional, 0, 7);
			this.pnlPlaylist.Controls.Add(this.dgTracks, 0, 10);
			this.pnlPlaylist.Controls.Add(this.lblTracks, 0, 9);
			this.pnlPlaylist.Controls.Add(this.lnkAdditional, 0, 6);
			this.pnlPlaylist.Controls.Add(this.btnAddTrack, 6, 9);
			this.pnlPlaylist.Location = new System.Drawing.Point(12, 12);
			this.pnlPlaylist.Name = "pnlPlaylist";
			this.pnlPlaylist.RowCount = 12;
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.pnlPlaylist.Size = new System.Drawing.Size(821, 718);
			this.pnlPlaylist.TabIndex = 15;
			// 
			// txtIndentifier
			// 
			this.txtIndentifier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlPlaylist.SetColumnSpan(this.txtIndentifier, 2);
			this.txtIndentifier.Location = new System.Drawing.Point(83, 84);
			this.txtIndentifier.Name = "txtIndentifier";
			this.txtIndentifier.Size = new System.Drawing.Size(258, 20);
			this.txtIndentifier.TabIndex = 16;
			// 
			// txtLocation
			// 
			this.txtLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlPlaylist.SetColumnSpan(this.txtLocation, 2);
			this.txtLocation.Location = new System.Drawing.Point(83, 57);
			this.txtLocation.Name = "txtLocation";
			this.txtLocation.Size = new System.Drawing.Size(258, 20);
			this.txtLocation.TabIndex = 15;
			// 
			// txtImage
			// 
			this.txtImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlPlaylist.SetColumnSpan(this.txtImage, 2);
			this.txtImage.Location = new System.Drawing.Point(83, 111);
			this.txtImage.Name = "txtImage";
			this.txtImage.Size = new System.Drawing.Size(258, 20);
			this.txtImage.TabIndex = 17;
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
			// dtCreatedTime
			// 
			this.dtCreatedTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dtCreatedTime.Location = new System.Drawing.Point(621, 30);
			this.dtCreatedTime.Name = "dtCreatedTime";
			this.dtCreatedTime.Size = new System.Drawing.Size(105, 20);
			this.dtCreatedTime.TabIndex = 22;
			// 
			// lblLicense
			// 
			this.lblLicense.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblLicense.Location = new System.Drawing.Point(387, 56);
			this.lblLicense.Name = "lblLicense";
			this.lblLicense.Size = new System.Drawing.Size(67, 23);
			this.lblLicense.TabIndex = 20;
			this.lblLicense.Text = "License";
			this.lblLicense.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbLicense
			// 
			this.cbLicense.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlPlaylist.SetColumnSpan(this.cbLicense, 2);
			this.cbLicense.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbLicense.FormattingEnabled = true;
			this.cbLicense.Location = new System.Drawing.Point(460, 57);
			this.cbLicense.Name = "cbLicense";
			this.cbLicense.Size = new System.Drawing.Size(318, 21);
			this.cbLicense.TabIndex = 26;
			// 
			// txtComment
			// 
			this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlPlaylist.SetColumnSpan(this.txtComment, 2);
			this.txtComment.Location = new System.Drawing.Point(460, 84);
			this.txtComment.Multiline = true;
			this.txtComment.Name = "txtComment";
			this.pnlPlaylist.SetRowSpan(this.txtComment, 2);
			this.txtComment.Size = new System.Drawing.Size(318, 48);
			this.txtComment.TabIndex = 29;
			// 
			// pnlAdditional
			// 
			this.pnlAdditional.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlAdditional.AutoScroll = true;
			this.pnlAdditional.AutoSize = true;
			this.pnlAdditional.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.pnlAdditional.ColumnCount = 2;
			this.pnlPlaylist.SetColumnSpan(this.pnlAdditional, 7);
			this.pnlAdditional.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.570552F));
			this.pnlAdditional.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.42945F));
			this.pnlAdditional.Controls.Add(this.lblMetadata, 0, 4);
			this.pnlAdditional.Controls.Add(this.dgMetadata, 1, 4);
			this.pnlAdditional.Controls.Add(this.lblLinks, 0, 2);
			this.pnlAdditional.Controls.Add(this.dgLinks, 1, 2);
			this.pnlAdditional.Controls.Add(this.lblExtensions, 0, 6);
			this.pnlAdditional.Controls.Add(this.lblAttribution, 0, 0);
			this.pnlAdditional.Controls.Add(this.dgAttribution, 1, 0);
			this.pnlAdditional.Controls.Add(this.dgExtensions, 1, 6);
			this.pnlAdditional.Location = new System.Drawing.Point(3, 175);
			this.pnlAdditional.Name = "pnlAdditional";
			this.pnlAdditional.RowCount = 7;
			this.pnlAdditional.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.pnlAdditional.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.pnlAdditional.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.pnlAdditional.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.pnlAdditional.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.pnlAdditional.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.pnlAdditional.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.pnlAdditional.Size = new System.Drawing.Size(775, 260);
			this.pnlAdditional.TabIndex = 33;
			// 
			// lblMetadata
			// 
			this.lblMetadata.Location = new System.Drawing.Point(3, 140);
			this.lblMetadata.Name = "lblMetadata";
			this.lblMetadata.Size = new System.Drawing.Size(68, 23);
			this.lblMetadata.TabIndex = 27;
			this.lblMetadata.Text = "Metadata";
			this.lblMetadata.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dgMetadata
			// 
			this.dgMetadata.AllowUserToResizeRows = false;
			this.dgMetadata.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgMetadata.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
			this.dgMetadata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgMetadata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMetaRel,
            this.colMetaValue});
			dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgMetadata.DefaultCellStyle = dataGridViewCellStyle23;
			this.dgMetadata.Location = new System.Drawing.Point(77, 143);
			this.dgMetadata.Name = "dgMetadata";
			dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgMetadata.RowHeadersDefaultCellStyle = dataGridViewCellStyle24;
			this.dgMetadata.Size = new System.Drawing.Size(695, 44);
			this.dgMetadata.TabIndex = 28;
			// 
			// colMetaRel
			// 
			this.colMetaRel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colMetaRel.FillWeight = 50F;
			this.colMetaRel.HeaderText = "Rel";
			this.colMetaRel.Name = "colMetaRel";
			// 
			// colMetaValue
			// 
			this.colMetaValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colMetaValue.FillWeight = 50F;
			this.colMetaValue.HeaderText = "Value";
			this.colMetaValue.Name = "colMetaValue";
			// 
			// lblLinks
			// 
			this.lblLinks.Location = new System.Drawing.Point(3, 70);
			this.lblLinks.Name = "lblLinks";
			this.lblLinks.Size = new System.Drawing.Size(68, 23);
			this.lblLinks.TabIndex = 24;
			this.lblLinks.Text = "Links";
			this.lblLinks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dgLinks
			// 
			this.dgLinks.AllowUserToResizeRows = false;
			this.dgLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgLinks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle25;
			this.dgLinks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgLinks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLinkRel,
            this.colLinkValue});
			dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgLinks.DefaultCellStyle = dataGridViewCellStyle26;
			this.dgLinks.Location = new System.Drawing.Point(77, 73);
			this.dgLinks.Name = "dgLinks";
			dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgLinks.RowHeadersDefaultCellStyle = dataGridViewCellStyle27;
			this.dgLinks.Size = new System.Drawing.Size(695, 44);
			this.dgLinks.TabIndex = 25;
			// 
			// colLinkRel
			// 
			this.colLinkRel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colLinkRel.FillWeight = 50F;
			this.colLinkRel.HeaderText = "Rel";
			this.colLinkRel.Name = "colLinkRel";
			// 
			// colLinkValue
			// 
			this.colLinkValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colLinkValue.FillWeight = 50F;
			this.colLinkValue.HeaderText = "Value";
			this.colLinkValue.Name = "colLinkValue";
			// 
			// lblExtensions
			// 
			this.lblExtensions.Location = new System.Drawing.Point(3, 210);
			this.lblExtensions.Name = "lblExtensions";
			this.lblExtensions.Size = new System.Drawing.Size(68, 23);
			this.lblExtensions.TabIndex = 30;
			this.lblExtensions.Text = "Extensions";
			this.lblExtensions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblAttribution
			// 
			this.lblAttribution.Location = new System.Drawing.Point(3, 0);
			this.lblAttribution.Name = "lblAttribution";
			this.lblAttribution.Size = new System.Drawing.Size(68, 23);
			this.lblAttribution.TabIndex = 23;
			this.lblAttribution.Text = "Attributions";
			this.lblAttribution.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dgExtensions
			// 
			this.dgExtensions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle28.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgExtensions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle28;
			this.dgExtensions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgExtensions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colExtensionApplication,
            this.colExtensionValue});
			dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle29.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle29.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle29.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgExtensions.DefaultCellStyle = dataGridViewCellStyle29;
			this.dgExtensions.Location = new System.Drawing.Point(77, 213);
			this.dgExtensions.Name = "dgExtensions";
			dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle30.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle30.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle30.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle30.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle30.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgExtensions.RowHeadersDefaultCellStyle = dataGridViewCellStyle30;
			this.dgExtensions.Size = new System.Drawing.Size(695, 44);
			this.dgExtensions.TabIndex = 31;
			// 
			// colExtensionApplication
			// 
			this.colExtensionApplication.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colExtensionApplication.FillWeight = 50F;
			this.colExtensionApplication.HeaderText = "Application";
			this.colExtensionApplication.Name = "colExtensionApplication";
			// 
			// colExtensionValue
			// 
			this.colExtensionValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.colExtensionValue.FillWeight = 50F;
			this.colExtensionValue.HeaderText = "Value";
			this.colExtensionValue.Name = "colExtensionValue";
			// 
			// dgTracks
			// 
			this.dgTracks.AllowUserToAddRows = false;
			this.dgTracks.AllowUserToResizeRows = false;
			this.dgTracks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgTracks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
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
			this.pnlPlaylist.SetColumnSpan(this.dgTracks, 7);
			dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgTracks.DefaultCellStyle = dataGridViewCellStyle17;
			this.dgTracks.Location = new System.Drawing.Point(3, 478);
			this.dgTracks.Name = "dgTracks";
			dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgTracks.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
			this.dgTracks.Size = new System.Drawing.Size(775, 205);
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
			this.lblTracks.Location = new System.Drawing.Point(3, 448);
			this.lblTracks.Name = "lblTracks";
			this.lblTracks.Size = new System.Drawing.Size(68, 21);
			this.lblTracks.TabIndex = 35;
			this.lblTracks.Text = "Track List";
			this.lblTracks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lnkAdditional
			// 
			this.lnkAdditional.Location = new System.Drawing.Point(3, 145);
			this.lnkAdditional.Name = "lnkAdditional";
			this.lnkAdditional.Size = new System.Drawing.Size(68, 23);
			this.lnkAdditional.TabIndex = 32;
			this.lnkAdditional.TabStop = true;
			this.lnkAdditional.Text = "Additional";
			this.lnkAdditional.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lnkAdditional.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAdditional_LinkClicked);
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
			// btnAddTrack
			// 
			this.btnAddTrack.Location = new System.Drawing.Point(621, 451);
			this.btnAddTrack.Name = "btnAddTrack";
			this.btnAddTrack.Size = new System.Drawing.Size(75, 21);
			this.btnAddTrack.TabIndex = 36;
			this.btnAddTrack.Text = "Add Track";
			this.btnAddTrack.UseVisualStyleBackColor = true;
			this.btnAddTrack.Click += new System.EventHandler(this.btnAddTrack_Click);
			// 
			// PlaylistForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(837, 737);
			this.Controls.Add(this.pnlPlaylist);
			this.Name = "PlaylistForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Papyrus";
			((System.ComponentModel.ISupportInitialize)(this.dgAttribution)).EndInit();
			this.pnlPlaylist.ResumeLayout(false);
			this.pnlPlaylist.PerformLayout();
			this.pnlAdditional.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgMetadata)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgLinks)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgExtensions)).EndInit();
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
        private System.Windows.Forms.DataGridView dgAttribution;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblCreator;
        private System.Windows.Forms.TextBox txtCreator;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblIdentifier;
        private System.Windows.Forms.Label lblImage;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.TableLayoutPanel pnlPlaylist;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.TextBox txtImage;
        private System.Windows.Forms.TextBox txtIndentifier;
        private System.Windows.Forms.Label lblLicense;
        private System.Windows.Forms.DateTimePicker dtCreatedDate;
        private System.Windows.Forms.DateTimePicker dtCreatedTime;
        private System.Windows.Forms.Label lblAttribution;
        private System.Windows.Forms.Label lblLinks;
        private System.Windows.Forms.DataGridView dgLinks;
        private System.Windows.Forms.ComboBox cbLicense;
        private System.Windows.Forms.Label lblMetadata;
        private System.Windows.Forms.DataGridView dgMetadata;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label lblExtensions;
        private System.Windows.Forms.LinkLabel lnkAdditional;
        private System.Windows.Forms.TableLayoutPanel pnlAdditional;
        private System.Windows.Forms.DataGridView dgExtensions;
        private System.Windows.Forms.DataGridView dgTracks;
        private System.Windows.Forms.Label lblTracks;
		private System.Windows.Forms.ErrorProvider errTitle;
		private System.Windows.Forms.ErrorProvider errInfoUri;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		private System.Windows.Forms.DataGridViewTextBoxColumn colAttributionValue;
		private System.Windows.Forms.DataGridViewTextBoxColumn colLinkRel;
		private System.Windows.Forms.DataGridViewTextBoxColumn colLinkValue;
		private System.Windows.Forms.DataGridViewTextBoxColumn colMetaRel;
		private System.Windows.Forms.DataGridViewTextBoxColumn colMetaValue;
		private System.Windows.Forms.DataGridViewTextBoxColumn colExtensionApplication;
		private System.Windows.Forms.DataGridViewTextBoxColumn colExtensionValue;
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
    }
}

