using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Alexandria.Core.Model;
using Papyrus.Properties;
using Papyrus.Views;

namespace Papyrus.Forms
{
    public partial class PlaylistForm : FormBase, IPlaylistView
    {
        public PlaylistForm()
        {
            InitializeComponent();

			LoadLicenses();

			SetAdditionalInfoVisibility(false);
		}

		#region Private Methods

		private void LoadLicenses()
		{
		}

		private void SetAdditionalInfoVisibility()
		{
			SetAdditionalInfoVisibility(!pnlAdditional.Visible);
		}

		private void SetAdditionalInfoVisibility(bool isVisible)
		{
			pnlAdditional.Visible = isVisible;

			//HACK: Toggle AutoSize to ensure that MainForm redraws correctly
			this.SuspendLayout();
			this.AutoSize = false;
			this.AutoSize = true;
			this.ResumeLayout(true);
		}

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
			SetAdditionalInfoVisibility();
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
