using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using Alexandria.Core;
using Alexandria.Core.Model;
using Papyrus.Data;
using Papyrus.Properties;
using Papyrus.Views;

namespace Papyrus.Forms
{
    public partial class PlaylistForm : FormBase, IPlaylistView
    {
        public PlaylistForm()
        {
            InitializeComponent();

			SetAdditionalInfoVisibility(false);

			Validated += new ViewActionCallback(CheckValidation);
		}

		#region Private Methods

		private void SetAdditionalInfoVisibility(bool isVisible)
		{
			pnlAdditional.Visible = isVisible;

			//HACK: Toggle AutoSize to ensure that MainForm redraws correctly
			this.SuspendLayout();
			this.AutoSize = false;
			this.AutoSize = true;
			this.ResumeLayout(true);
		}

		private void CheckValidation(ViewAction action)
		{
			errTitle.SetError(txtTitle, string.Empty);
			errTitle.SetError(txtInfo, string.Empty);
			errTracks.SetError(lblTracks, string.Empty);

			if (action != null)
			{
				if (!action.IsValid)
				{
					if (title.Status == DataStatus.Missing)
						errTitle.SetError(txtTitle, Resources.ErrorTitleMissing);

					if (info.Status == DataStatus.Invalid)
						errInfoUri.SetError(txtInfo, Resources.ErrorInfoUriInvalid);

					if (tracks.Status == DataStatus.Missing)
						errTracks.SetError(lblTracks, Resources.ErrorTracksMissing);

					MessageBox.Show(Resources.MessageValidationBody, Resources.MessageValidationTitle);
				}
			}
		}

		#endregion

		#region Event Methods

		private void lnkAdditional_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
			SetAdditionalInfoVisibility(!pnlAdditional.Visible);
        }

		private void btnAddTrack_Click(object sender, EventArgs e)
		{
			dgTracks.Rows.Add();
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			DialogResult result = dlgOpenFile.ShowDialog();
			if (result == DialogResult.OK && !string.IsNullOrEmpty(dlgOpenFile.FileName))
			{
				if (dlgOpenFile.FileName.EndsWith(Resources.FileExtensionXspf, StringComparison.CurrentCultureIgnoreCase))
				{
					try
					{
						ViewAction action = new ViewAction() { Path = UriUtility.GetUriFromFileName(dlgOpenFile.FileName) };

						LoadForm(action);
					}
					catch (Exception ex)
					{
						ShowMessage(Resources.MessageLoadingPlaylistTitle, string.Format(Resources.MessageLoadingPlaylistBody, ex.Message, ex.StackTrace));
					}
				}
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			ViewAction action = new ViewAction();
			ValidateForm(action);

			if (action.IsValid)
			{
				DialogResult result = dlgSaveFile.ShowDialog();
				if (result == DialogResult.OK && !string.IsNullOrEmpty(dlgSaveFile.FileName))
				{
					action.Path = UriUtility.GetUriFromFileName(dlgSaveFile.FileName);
					AcceptForm(action);
				}
			}
		}

		#endregion

		#region IPlaylistView Members

		private DataItem<string> title = new DataItem<string>();
		private DataItem<string> creator = new DataItem<string>();
		private DataItem<DateTime> created = new DataItem<DateTime>();
		private DataItem<Uri> info = new DataItem<Uri>();
		private DataItem<Uri> identifier = new DataItem<Uri>();
		private DataItem<Uri> location = new DataItem<Uri>();
		private DataItem<Uri> image = new DataItem<Uri>();
		private DataItem<Uri> license = new DataItem<Uri>();
		private DataItem<string> comment = new DataItem<string>();
		private DataList<AttributionData> attribution = new DataList<AttributionData>();
		private DataList<LinkData> links = new DataList<LinkData>();
		private DataList<MetaData> metadata = new DataList<MetaData>();
		private DataList<ExtensionData> extensions = new DataList<ExtensionData>();
		private DataList<TrackData> tracks = new DataList<TrackData>();

		DataItem<string> IPlaylistView.Title
		{
			get { return title; }
		}

		DataItem<string> IPlaylistView.Creator
		{
			get { return creator; }
		}

		DataItem<DateTime> IPlaylistView.Created
		{
			get { return created; }
		}

		DataItem<Uri> IPlaylistView.Info
		{
			get { return info; }
		}

		DataItem<Uri> IPlaylistView.Location
		{
			get { return location; }
		}

		DataItem<Uri> IPlaylistView.Identifier
		{
			get { return identifier; }
		}

		DataItem<Uri> IPlaylistView.Image
		{
			get { return image; }
		}

		DataItem<Uri> IPlaylistView.License
		{
			get { return license; }
		}

		DataItem<string> IPlaylistView.Comment
		{
			get { return comment; }
		}

		DataList<AttributionData> IPlaylistView.Attribution
		{
			get { return attribution; }
		}

		DataList<LinkData> IPlaylistView.Links
		{
			get { return links; }
		}

		DataList<MetaData> IPlaylistView.Metadata
		{
			get { return metadata; }
		}

		DataList<ExtensionData> IPlaylistView.Extensions
		{
			get { return extensions; }
		}

		DataList<TrackData> IPlaylistView.Tracks
		{
			get { return tracks; }
		}

		public override void RefreshData()
		{
			title.Value = txtTitle.Text;

			tracks.Items.Clear();
			foreach (DataGridViewRow row in dgTracks.Rows)
			{
				TrackData item = new TrackData();
				item.Album = DataUtility.GetString(row.Cells[Resources.ColumnTrackAlbum].Value);
				item.Annotation = DataUtility.GetString(row.Cells[Resources.ColumnTrackAnnotation].Value);
				item.Creator = DataUtility.GetString(row.Cells[Resources.ColumnTrackCreator].Value);
				item.Duration = DataUtility.GetTimeSpan(row.Cells[Resources.ColumnTrackDuration].Value);
				item.Identifier = DataUtility.GetUri(row.Cells[Resources.ColumnTrackIdentifier].Value);
				item.Image = DataUtility.GetUri(row.Cells[Resources.ColumnTrackImage].Value);
				item.Info = DataUtility.GetUri(row.Cells[Resources.ColumnTrackInfo].Value);
				item.Location = DataUtility.GetUri(row.Cells[Resources.ColumnTrackLocation].Value);
				item.Title = DataUtility.GetString(row.Cells[Resources.ColumnTrackTitle].Value);
				item.TrackNum = DataUtility.GetUInt32(row.Cells[Resources.ColumnTrackNumber].Value);

				tracks.Items.Add(new DataItem<TrackData>() { Value = item });
			}
		}

		#endregion
	}
}