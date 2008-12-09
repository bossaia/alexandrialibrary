namespace Telesophy.Alexandria.Clients.Ankh.Views
{
    partial class MediaItemSortBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SortListView = new System.Windows.Forms.ListView();
            this.SortButton = new System.Windows.Forms.Button();
            this.SortIcons = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // SortListView
            // 
            this.SortListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SortListView.LargeImageList = this.SortIcons;
            this.SortListView.Location = new System.Drawing.Point(8, 8);
            this.SortListView.Name = "SortListView";
            this.SortListView.Size = new System.Drawing.Size(453, 38);
            this.SortListView.SmallImageList = this.SortIcons;
            this.SortListView.StateImageList = this.SortIcons;
            this.SortListView.TabIndex = 0;
            this.SortListView.UseCompatibleStateImageBehavior = false;
            // 
            // SortButton
            // 
            this.SortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SortButton.Location = new System.Drawing.Point(472, 16);
            this.SortButton.Name = "SortButton";
            this.SortButton.Size = new System.Drawing.Size(75, 23);
            this.SortButton.TabIndex = 1;
            this.SortButton.Text = "Sort";
            this.SortButton.UseVisualStyleBackColor = true;
            // 
            // SortIcons
            // 
            this.SortIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.SortIcons.ImageSize = new System.Drawing.Size(16, 16);
            this.SortIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // MediaItemSortBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SortButton);
            this.Controls.Add(this.SortListView);
            this.Name = "MediaItemSortBox";
            this.Size = new System.Drawing.Size(558, 54);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView SortListView;
        private System.Windows.Forms.Button SortButton;
        private System.Windows.Forms.ImageList SortIcons;
    }
}
