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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
			this.lblTitle = new System.Windows.Forms.Label();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.dgAttribution = new System.Windows.Forms.DataGridView();
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
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.dtCreatedDate = new System.Windows.Forms.DateTimePicker();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.lblLicense = new System.Windows.Forms.Label();
			this.cbLicense = new System.Windows.Forms.ComboBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.pnlAdditional = new System.Windows.Forms.TableLayoutPanel();
			this.lblMetadata = new System.Windows.Forms.Label();
			this.dgMetadata = new System.Windows.Forms.DataGridView();
			this.lblLinks = new System.Windows.Forms.Label();
			this.dgLinks = new System.Windows.Forms.DataGridView();
			this.lblExtensions = new System.Windows.Forms.Label();
			this.lblAttribution = new System.Windows.Forms.Label();
			this.dgExtensions = new System.Windows.Forms.DataGridView();
			this.dgTracks = new System.Windows.Forms.DataGridView();
			this.lblTracks = new System.Windows.Forms.Label();
			this.lnkAdditional = new System.Windows.Forms.LinkLabel();
			this.errTitle = new System.Windows.Forms.ErrorProvider(this.components);
			this.errInfoUri = new System.Windows.Forms.ErrorProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.dgAttribution)).BeginInit();
			this.pnlPlaylist.SuspendLayout();
			this.pnlAdditional.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgMetadata)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgLinks)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgExtensions)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgTracks)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errTitle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errInfoUri)).BeginInit();
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
			this.txtTitle.Validating += new System.ComponentModel.CancelEventHandler(this.txtTitle_Validating);
			// 
			// dgAttribution
			// 
			this.dgAttribution.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgAttribution.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
			this.dgAttribution.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgAttribution.DefaultCellStyle = dataGridViewCellStyle8;
			this.dgAttribution.Location = new System.Drawing.Point(77, 3);
			this.dgAttribution.Name = "dgAttribution";
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgAttribution.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
			this.dgAttribution.Size = new System.Drawing.Size(695, 44);
			this.dgAttribution.TabIndex = 2;
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
			this.txtInfo.Validating += new System.ComponentModel.CancelEventHandler(this.txtInfo_Validating);
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
			this.pnlPlaylist.Controls.Add(this.textBox7, 1, 4);
			this.pnlPlaylist.Controls.Add(this.lblDate, 4, 1);
			this.pnlPlaylist.Controls.Add(this.dtCreatedDate, 5, 1);
			this.pnlPlaylist.Controls.Add(this.dateTimePicker1, 6, 1);
			this.pnlPlaylist.Controls.Add(this.lblLicense, 4, 2);
			this.pnlPlaylist.Controls.Add(this.cbLicense, 5, 2);
			this.pnlPlaylist.Controls.Add(this.lblComment, 4, 3);
			this.pnlPlaylist.Controls.Add(this.textBox1, 5, 3);
			this.pnlPlaylist.Controls.Add(this.pnlAdditional, 0, 7);
			this.pnlPlaylist.Controls.Add(this.dgTracks, 0, 10);
			this.pnlPlaylist.Controls.Add(this.lblTracks, 0, 9);
			this.pnlPlaylist.Controls.Add(this.lnkAdditional, 0, 6);
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
			// textBox7
			// 
			this.textBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlPlaylist.SetColumnSpan(this.textBox7, 2);
			this.textBox7.Location = new System.Drawing.Point(83, 111);
			this.textBox7.Name = "textBox7";
			this.textBox7.Size = new System.Drawing.Size(258, 20);
			this.textBox7.TabIndex = 17;
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
			// dateTimePicker1
			// 
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dateTimePicker1.Location = new System.Drawing.Point(621, 30);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.Size = new System.Drawing.Size(105, 20);
			this.dateTimePicker1.TabIndex = 22;
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
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlPlaylist.SetColumnSpan(this.textBox1, 2);
			this.textBox1.Location = new System.Drawing.Point(460, 84);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.pnlPlaylist.SetRowSpan(this.textBox1, 2);
			this.textBox1.Size = new System.Drawing.Size(318, 48);
			this.textBox1.TabIndex = 29;
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
			this.dgMetadata.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgMetadata.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dgMetadata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgMetadata.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgMetadata.Location = new System.Drawing.Point(77, 143);
			this.dgMetadata.Name = "dgMetadata";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgMetadata.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dgMetadata.Size = new System.Drawing.Size(695, 44);
			this.dgMetadata.TabIndex = 28;
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
			this.dgLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgLinks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dgLinks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgLinks.DefaultCellStyle = dataGridViewCellStyle5;
			this.dgLinks.Location = new System.Drawing.Point(77, 73);
			this.dgLinks.Name = "dgLinks";
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgLinks.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.dgLinks.Size = new System.Drawing.Size(695, 44);
			this.dgLinks.TabIndex = 25;
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
			this.lblAttribution.Text = "Attribution";
			this.lblAttribution.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dgExtensions
			// 
			this.dgExtensions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgExtensions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
			this.dgExtensions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgExtensions.DefaultCellStyle = dataGridViewCellStyle11;
			this.dgExtensions.Location = new System.Drawing.Point(77, 213);
			this.dgExtensions.Name = "dgExtensions";
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgExtensions.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
			this.dgExtensions.Size = new System.Drawing.Size(695, 44);
			this.dgExtensions.TabIndex = 31;
			// 
			// dgTracks
			// 
			this.dgTracks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgTracks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
			this.dgTracks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.pnlPlaylist.SetColumnSpan(this.dgTracks, 7);
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgTracks.DefaultCellStyle = dataGridViewCellStyle14;
			this.dgTracks.Location = new System.Drawing.Point(3, 478);
			this.dgTracks.Name = "dgTracks";
			dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgTracks.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
			this.dgTracks.Size = new System.Drawing.Size(775, 205);
			this.dgTracks.TabIndex = 34;
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
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(837, 737);
			this.Controls.Add(this.pnlPlaylist);
			this.Name = "MainForm";
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
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox txtIndentifier;
        private System.Windows.Forms.Label lblLicense;
        private System.Windows.Forms.DateTimePicker dtCreatedDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label lblAttribution;
        private System.Windows.Forms.Label lblLinks;
        private System.Windows.Forms.DataGridView dgLinks;
        private System.Windows.Forms.ComboBox cbLicense;
        private System.Windows.Forms.Label lblMetadata;
        private System.Windows.Forms.DataGridView dgMetadata;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblExtensions;
        private System.Windows.Forms.LinkLabel lnkAdditional;
        private System.Windows.Forms.TableLayoutPanel pnlAdditional;
        private System.Windows.Forms.DataGridView dgExtensions;
        private System.Windows.Forms.DataGridView dgTracks;
        private System.Windows.Forms.Label lblTracks;
		private System.Windows.Forms.ErrorProvider errTitle;
		private System.Windows.Forms.ErrorProvider errInfoUri;
    }
}

