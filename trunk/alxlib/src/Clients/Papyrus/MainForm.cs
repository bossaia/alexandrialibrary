using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Papyrus.Properties;
using Media.Playlists.Xspf;

namespace Papyrus
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            pnlAdditional.Visible = false;
		}

		#region Private Methods

		private void ValidateTitle()
		{
			string error = string.Empty;

			if (string.IsNullOrEmpty(txtTitle.Text))
				error = Resources.ErrorTitleMissing;

			errTitle.SetError(txtTitle, error);
		}

		private void ValidateInfoUri()
		{
			string error = string.Empty;

			if (!string.IsNullOrEmpty(txtInfo.Text))
			{
				if (!Uri.IsWellFormedUriString(txtInfo.Text, UriKind.RelativeOrAbsolute))
					error = Resources.ErrorInfoUriInvalid;
			}

			errInfoUri.SetError(txtInfo, error);
		}

		#endregion

		#region Event Methods

		private void lnkAdditional_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlAdditional.Visible = !pnlAdditional.Visible;

			//HACK: Toggle AutoSize to ensure that MainForm redraws correctly
			this.SuspendLayout();
			this.AutoSize = false;
			this.AutoSize = true;
			this.ResumeLayout(true);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
			if (Validate())
			{
				Playlist playlist = new Playlist();

				playlist.Title = txtTitle.Text;
				playlist.Creator = txtCreator.Text;
				//playlist.
			}
        }

		private void txtTitle_Validating(object sender, CancelEventArgs e)
		{
			ValidateTitle();
		}

		private void txtInfo_Validating(object sender, CancelEventArgs e)
		{
			ValidateInfoUri();
		}

		#endregion
	}
}
