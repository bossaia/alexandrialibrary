namespace Papyrus
{
    partial class MainForm
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
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.txtIndentifier = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.lblLicense = new System.Windows.Forms.Label();
            this.dtCreatedDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lblAttribution = new System.Windows.Forms.Label();
            this.lblLinks = new System.Windows.Forms.Label();
            this.dgLinks = new System.Windows.Forms.DataGridView();
            this.cbLicense = new System.Windows.Forms.ComboBox();
            this.lblMetadata = new System.Windows.Forms.Label();
            this.dgMetadata = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblExtensions = new System.Windows.Forms.Label();
            this.lnkAdditional = new System.Windows.Forms.LinkLabel();
            this.pnlAdditional = new System.Windows.Forms.TableLayoutPanel();
            this.dgExtensions = new System.Windows.Forms.DataGridView();
            this.dgTracks = new System.Windows.Forms.DataGridView();
            this.lblTracks = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgAttribution)).BeginInit();
            this.pnlPlaylist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLinks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMetadata)).BeginInit();
            this.pnlAdditional.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgExtensions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTracks)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTitle.Location = new System.Drawing.Point(3, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(69, 23);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPlaylist.SetColumnSpan(this.txtTitle, 2);
            this.txtTitle.Location = new System.Drawing.Point(78, 3);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(283, 20);
            this.txtTitle.TabIndex = 1;
            // 
            // dgAttribution
            // 
            this.dgAttribution.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgAttribution.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAttribution.Location = new System.Drawing.Point(80, 3);
            this.dgAttribution.Name = "dgAttribution";
            this.dgAttribution.Size = new System.Drawing.Size(692, 44);
            this.dgAttribution.TabIndex = 2;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(620, 702);
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
            this.btnSave.Location = new System.Drawing.Point(718, 702);
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
            this.lblCreator.Location = new System.Drawing.Point(388, 2);
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
            this.txtCreator.Location = new System.Drawing.Point(461, 3);
            this.txtCreator.Name = "txtCreator";
            this.txtCreator.Size = new System.Drawing.Size(317, 20);
            this.txtCreator.TabIndex = 6;
            // 
            // lblComment
            // 
            this.lblComment.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblComment.Location = new System.Drawing.Point(388, 82);
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
            this.lblInfo.Size = new System.Drawing.Size(69, 23);
            this.lblInfo.TabIndex = 9;
            this.lblInfo.Text = "Info Uri";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtInfo
            // 
            this.txtInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPlaylist.SetColumnSpan(this.txtInfo, 2);
            this.txtInfo.Location = new System.Drawing.Point(78, 30);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(283, 20);
            this.txtInfo.TabIndex = 10;
            // 
            // lblLocation
            // 
            this.lblLocation.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLocation.Location = new System.Drawing.Point(3, 56);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(69, 23);
            this.lblLocation.TabIndex = 11;
            this.lblLocation.Text = "Location Uri";
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIdentifier
            // 
            this.lblIdentifier.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblIdentifier.Location = new System.Drawing.Point(3, 83);
            this.lblIdentifier.Name = "lblIdentifier";
            this.lblIdentifier.Size = new System.Drawing.Size(69, 23);
            this.lblIdentifier.TabIndex = 12;
            this.lblIdentifier.Text = "Identifier Uri";
            this.lblIdentifier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblImage
            // 
            this.lblImage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblImage.Location = new System.Drawing.Point(3, 110);
            this.lblImage.Name = "lblImage";
            this.lblImage.Size = new System.Drawing.Size(69, 23);
            this.lblImage.TabIndex = 13;
            this.lblImage.Text = "Image Uri";
            this.lblImage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDate
            // 
            this.lblDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDate.Location = new System.Drawing.Point(388, 29);
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
            this.pnlPlaylist.ColumnCount = 7;
            this.pnlPlaylist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.32759F));
            this.pnlPlaylist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.67242F));
            this.pnlPlaylist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132F));
            this.pnlPlaylist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.pnlPlaylist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.pnlPlaylist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 161F));
            this.pnlPlaylist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 161F));
            this.pnlPlaylist.Controls.Add(this.txtTitle, 1, 0);
            this.pnlPlaylist.Controls.Add(this.lblTitle, 0, 0);
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
            this.pnlPlaylist.RowCount = 11;
            this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.91439F));
            this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.523809F));
            this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.93175F));
            this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.670623F));
            this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.231454F));
            this.pnlPlaylist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.pnlPlaylist.Size = new System.Drawing.Size(781, 684);
            this.pnlPlaylist.TabIndex = 15;
            // 
            // txtLocation
            // 
            this.txtLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPlaylist.SetColumnSpan(this.txtLocation, 2);
            this.txtLocation.Location = new System.Drawing.Point(78, 57);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(283, 20);
            this.txtLocation.TabIndex = 15;
            // 
            // txtIndentifier
            // 
            this.txtIndentifier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPlaylist.SetColumnSpan(this.txtIndentifier, 2);
            this.txtIndentifier.Location = new System.Drawing.Point(78, 84);
            this.txtIndentifier.Name = "txtIndentifier";
            this.txtIndentifier.Size = new System.Drawing.Size(283, 20);
            this.txtIndentifier.TabIndex = 16;
            // 
            // textBox7
            // 
            this.textBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPlaylist.SetColumnSpan(this.textBox7, 2);
            this.textBox7.Location = new System.Drawing.Point(78, 111);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(283, 20);
            this.textBox7.TabIndex = 17;
            // 
            // lblLicense
            // 
            this.lblLicense.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLicense.Location = new System.Drawing.Point(388, 56);
            this.lblLicense.Name = "lblLicense";
            this.lblLicense.Size = new System.Drawing.Size(67, 23);
            this.lblLicense.TabIndex = 20;
            this.lblLicense.Text = "License";
            this.lblLicense.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtCreatedDate
            // 
            this.dtCreatedDate.CustomFormat = "";
            this.dtCreatedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtCreatedDate.Location = new System.Drawing.Point(461, 30);
            this.dtCreatedDate.Name = "dtCreatedDate";
            this.dtCreatedDate.Size = new System.Drawing.Size(110, 20);
            this.dtCreatedDate.TabIndex = 21;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker1.Location = new System.Drawing.Point(622, 30);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(105, 20);
            this.dateTimePicker1.TabIndex = 22;
            // 
            // lblAttribution
            // 
            this.lblAttribution.Location = new System.Drawing.Point(3, 0);
            this.lblAttribution.Name = "lblAttribution";
            this.lblAttribution.Size = new System.Drawing.Size(71, 23);
            this.lblAttribution.TabIndex = 23;
            this.lblAttribution.Text = "Attribution";
            this.lblAttribution.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLinks
            // 
            this.lblLinks.Location = new System.Drawing.Point(3, 70);
            this.lblLinks.Name = "lblLinks";
            this.lblLinks.Size = new System.Drawing.Size(71, 23);
            this.lblLinks.TabIndex = 24;
            this.lblLinks.Text = "Links";
            this.lblLinks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgLinks
            // 
            this.dgLinks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgLinks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLinks.Location = new System.Drawing.Point(80, 73);
            this.dgLinks.Name = "dgLinks";
            this.dgLinks.Size = new System.Drawing.Size(692, 44);
            this.dgLinks.TabIndex = 25;
            // 
            // cbLicense
            // 
            this.cbLicense.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPlaylist.SetColumnSpan(this.cbLicense, 2);
            this.cbLicense.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLicense.FormattingEnabled = true;
            this.cbLicense.Location = new System.Drawing.Point(461, 57);
            this.cbLicense.Name = "cbLicense";
            this.cbLicense.Size = new System.Drawing.Size(317, 21);
            this.cbLicense.TabIndex = 26;
            // 
            // lblMetadata
            // 
            this.lblMetadata.Location = new System.Drawing.Point(3, 140);
            this.lblMetadata.Name = "lblMetadata";
            this.lblMetadata.Size = new System.Drawing.Size(71, 23);
            this.lblMetadata.TabIndex = 27;
            this.lblMetadata.Text = "Metadata";
            this.lblMetadata.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgMetadata
            // 
            this.dgMetadata.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgMetadata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMetadata.Location = new System.Drawing.Point(80, 143);
            this.dgMetadata.Name = "dgMetadata";
            this.dgMetadata.Size = new System.Drawing.Size(692, 44);
            this.dgMetadata.TabIndex = 28;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPlaylist.SetColumnSpan(this.textBox1, 2);
            this.textBox1.Location = new System.Drawing.Point(461, 84);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.pnlPlaylist.SetRowSpan(this.textBox1, 2);
            this.textBox1.Size = new System.Drawing.Size(317, 48);
            this.textBox1.TabIndex = 29;
            // 
            // lblExtensions
            // 
            this.lblExtensions.Location = new System.Drawing.Point(3, 210);
            this.lblExtensions.Name = "lblExtensions";
            this.lblExtensions.Size = new System.Drawing.Size(71, 23);
            this.lblExtensions.TabIndex = 30;
            this.lblExtensions.Text = "Extensions";
            this.lblExtensions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lnkAdditional
            // 
            this.lnkAdditional.Location = new System.Drawing.Point(3, 144);
            this.lnkAdditional.Name = "lnkAdditional";
            this.lnkAdditional.Size = new System.Drawing.Size(69, 23);
            this.lnkAdditional.TabIndex = 32;
            this.lnkAdditional.TabStop = true;
            this.lnkAdditional.Text = "Additional";
            this.lnkAdditional.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAdditional_LinkClicked);
            // 
            // pnlAdditional
            // 
            this.pnlAdditional.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAdditional.ColumnCount = 2;
            this.pnlPlaylist.SetColumnSpan(this.pnlAdditional, 7);
            this.pnlAdditional.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pnlAdditional.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.pnlAdditional.Controls.Add(this.lblMetadata, 0, 4);
            this.pnlAdditional.Controls.Add(this.dgMetadata, 1, 4);
            this.pnlAdditional.Controls.Add(this.lblLinks, 0, 2);
            this.pnlAdditional.Controls.Add(this.dgLinks, 1, 2);
            this.pnlAdditional.Controls.Add(this.lblExtensions, 0, 6);
            this.pnlAdditional.Controls.Add(this.lblAttribution, 0, 0);
            this.pnlAdditional.Controls.Add(this.dgAttribution, 1, 0);
            this.pnlAdditional.Controls.Add(this.dgExtensions, 1, 6);
            this.pnlAdditional.Location = new System.Drawing.Point(3, 179);
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
            // dgExtensions
            // 
            this.dgExtensions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgExtensions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgExtensions.Location = new System.Drawing.Point(80, 213);
            this.dgExtensions.Name = "dgExtensions";
            this.dgExtensions.Size = new System.Drawing.Size(692, 44);
            this.dgExtensions.TabIndex = 31;
            // 
            // dgTracks
            // 
            this.dgTracks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgTracks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pnlPlaylist.SetColumnSpan(this.dgTracks, 7);
            this.dgTracks.Location = new System.Drawing.Point(3, 475);
            this.dgTracks.Name = "dgTracks";
            this.dgTracks.Size = new System.Drawing.Size(775, 206);
            this.dgTracks.TabIndex = 34;
            // 
            // lblTracks
            // 
            this.lblTracks.Location = new System.Drawing.Point(3, 451);
            this.lblTracks.Name = "lblTracks";
            this.lblTracks.Size = new System.Drawing.Size(69, 21);
            this.lblTracks.TabIndex = 35;
            this.lblTracks.Text = "Track List";
            this.lblTracks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 737);
            this.Controls.Add(this.pnlPlaylist);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Name = "MainForm";
            this.Text = "Papyrus";
            ((System.ComponentModel.ISupportInitialize)(this.dgAttribution)).EndInit();
            this.pnlPlaylist.ResumeLayout(false);
            this.pnlPlaylist.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLinks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMetadata)).EndInit();
            this.pnlAdditional.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgExtensions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTracks)).EndInit();
            this.ResumeLayout(false);

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
    }
}

