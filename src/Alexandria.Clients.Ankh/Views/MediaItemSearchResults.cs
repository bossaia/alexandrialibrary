using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Telesophy.Alexandria.Clients.Ankh.Controllers;

using Telesophy.Alexandria.Clients.Ankh.Views.Data;

namespace Telesophy.Alexandria.Clients.Ankh.Views
{
	public partial class MediaItemSearchResults : Form
	{
		#region Constructors
		public MediaItemSearchResults()
		{
			InitializeComponent();

			grid.CellFormatting += new DataGridViewCellFormattingEventHandler(grid_CellFormatting);
		}
		#endregion
		
		#region Private Constants
		private const string COLUMN_ID = "idColumn";
		private const string COLUMN_STATUS = "statusColumn";
		private const string COLUMN_TYPE = "typeColumn";
		private const string COLUMN_SOURCE = "sourceColumn";
		private const string COLUMN_NUMBER = "numberColumn";
		private const string COLUMN_TITLE = "titleColumn";
		private const string COLUMN_ARTIST = "artistColumn";
		private const string COLUMN_ALBUM = "albumColumn";
		private const string COLUMN_DURATION = "durationColumn";
		private const string COLUMN_DATE = "dateColumn";
		private const string COLUMN_FORMAT = "formatColumn";
		private const string COLUMN_PATH = "pathColumn";
		#endregion
		
		#region Private Fields
		private ImageList smallImageList;
		private EventHandler<MediaItemSearchEventArgs> choiceAccepted;
		#endregion
		
		#region Private Event Methods
		private void grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (grid.Columns[e.ColumnIndex].Name == COLUMN_DURATION)
			{
				TimeSpan duration = (e.Value != null) ? (TimeSpan)e.Value : TimeSpan.Zero;
				if (duration.Hours > 0)
				{
					e.Value = string.Format("{0:00}:{1:00}:{2:00}", duration.Hours, duration.Minutes, duration.Seconds);
				}
				else if (duration.Minutes > 0)
				{
					e.Value = string.Format("{0:00}:{1:00}", duration.Minutes, duration.Seconds);
				}
				else
				{
					e.Value = string.Format("0:{0:00}", duration.Seconds);
				}
			}
			else if (grid.Columns[e.ColumnIndex].Name == COLUMN_TYPE)
			{
				string value = (e.Value != null) ? e.Value.ToString() : string.Empty;

				switch (value)
				{
					case ControllerConstants.TYPE_AUDIO:
						e.Value = smallImageList.Images[ControllerConstants.INDEX_AUDIO];
						break;
					case ControllerConstants.TYPE_BOOK:
						e.Value = smallImageList.Images[ControllerConstants.INDEX_BOOK];
						break;
					case ControllerConstants.TYPE_IMAGE:
						e.Value = smallImageList.Images[ControllerConstants.INDEX_IMAGE];
						break;
					case ControllerConstants.TYPE_MOVIE:
						e.Value = smallImageList.Images[ControllerConstants.INDEX_MOVIE];
						break;
					case ControllerConstants.TYPE_TELEVISION:
						e.Value = smallImageList.Images[ControllerConstants.INDEX_TELEVISION];
						break;
					default:
						e.Value = smallImageList.Images[ControllerConstants.INDEX_AUDIO];
						break;
				}
			}
		}
		
		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			if (ChoiceAccepted != null)
			{
				if (grid.SelectedRows != null && grid.SelectedRows.Count > 0)
				{
					IList<MediaItemData> data = new List<MediaItemData>();
					foreach (DataGridViewRow row in grid.SelectedRows)
					{
						MediaItemData item = grid.GetItem(row.Index);
						data.Add(item);
					}
					
					MediaItemSearchEventArgs searchArgs = new MediaItemSearchEventArgs(data, true);
					ChoiceAccepted(this, searchArgs);
				}
			}
		
			Close();
		}
		#endregion
		
		#region Public Properties
		public ImageList SmallImageList
		{
			get { return smallImageList; }
			set { smallImageList = value; }
		}
		
		public string Instructions
		{
			get { return instructionsLabel.Text; }
			set { instructionsLabel.Text = value; }
		}
		
		public EventHandler<MediaItemSearchEventArgs> ChoiceAccepted
		{
			get { return choiceAccepted; }
			set { choiceAccepted = value; }
		}
		#endregion
		
		#region Public Methods
		public void LoadData(IEnumerable<MediaItemData> data)
		{
			if (data != null)
			{
				foreach (MediaItemData item in data)
				{
					grid.AddItem(item);
				}
				
				if (grid.Rows.Count > 0)
					grid.Rows[0].Selected = true;
			}
		}
		#endregion
	}
}
