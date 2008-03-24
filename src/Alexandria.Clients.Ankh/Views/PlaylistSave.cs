using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Telesophy.Babel.Persistence;

using Telesophy.Alexandria.Clients.Ankh.Controllers;

namespace Telesophy.Alexandria.Clients.Ankh.Views
{
	public delegate void PlaylistSaveConfirmHandle();

	public partial class PlaylistSave : Form
	{
		#region Constructors
		public PlaylistSave()
		{
			InitializeComponent();
		}
		#endregion
		
		#region Private Constants
		private const string COLUMN_ID = "idColumn";
		private const string COLUMN_TYPE = "typeColumn";
		private const string COLUMN_SOURCE = "sourceCoumn";
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
		private ToolController toolController;
		private PlaylistSaveConfirmHandle saveConfirmHandle;
		#endregion
		
		#region Public Properties
		public ToolController ToolController
		{
			get { return toolController; }
			set { toolController = value; }
		}
		
		public PlaylistSaveConfirmHandle SaveConfirmHandle
		{
			get { return saveConfirmHandle; }
			set { saveConfirmHandle = value; }
		}
		
		public Guid Identifier
		{
			get
			{ 
				if (!string.IsNullOrEmpty(identifierTextBox.Text))
					return new Guid(identifierTextBox.Text);
				else return default(Guid);
			}
			set { identifierTextBox.Text = value.ToString(); }
		}
		
		public string Title
		{
			get { return titleTextBox.Text; }
			set { titleTextBox.Text = value; }
		}
		
		public string Artist
		{
			get { return artistTextBox.Text; }
			set { artistTextBox.Text = value; }
		}
		
		public int Number
		{
			get { return Convert.ToInt32(numberPicker.Value); }
			set { numberPicker.Value = (decimal)value; }
		}
		
		public string Source
		{
			get { return sourceTextBox.Text; }
			set { sourceTextBox.Text = value; }
		}
		
		public DateTime Date
		{
			get { return datePicker.Value; }
			set { datePicker.Value = value; }
		}
		
		public string Format
		{
			get { return formatTextBox.Text; }
			set { formatTextBox.Text = value; }
		}
		
		public Uri Path
		{
			get
			{
				if (!string.IsNullOrEmpty(pathTextBox.Text))
					return new Uri(pathTextBox.Text);
				else return null;
			}
			set { pathTextBox.Text = value.ToString(); }
		}		
		#endregion

		#region Public Methods
		public void AddItem(Guid id, string type, string source, int number, string title, string artist, string album, TimeSpan duration, DateTime date, string format, Uri path)
		{
			DataGridViewRow row = new DataGridViewRow();
			row.Cells[COLUMN_ID].Value = id;
			row.Cells[COLUMN_TYPE].Value = type;
			row.Cells[COLUMN_SOURCE].Value = source;
			row.Cells[COLUMN_NUMBER].Value = number;
			row.Cells[COLUMN_TITLE].Value = title;
			row.Cells[COLUMN_ARTIST].Value = artist;
			row.Cells[COLUMN_ALBUM].Value = album;
			row.Cells[COLUMN_DURATION].Value = duration;
			row.Cells[COLUMN_DATE].Value = date;
			row.Cells[COLUMN_FORMAT].Value = format;
			row.Cells[COLUMN_PATH].Value = path;
		
			itemGrid.Rows.Add(row);
		}
		
		public DataTable GetItemDataTable()
		{
			DataTable table = new DataTable("PlaylistItems");
			//table.Columns.Add(COLUMN_ID
			
			foreach (DataGridViewRow row in itemGrid.Rows)
			{
				//if (row.Cells[COLUMN_PATH].Value != null)
				//{
					//Uri path = new Uri(row.Cells[COLUMN_PATH].Value.ToString());
					//paths.Add(path);
				//}
			}
			return table;
		}
		#endregion

		#region Private Event Methods
		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void saveButton_Click(object sender, EventArgs e)
		{
			ToolController.SavePlaylist(this);
			if (SaveConfirmHandle != null)
				SaveConfirmHandle();
			
			Close();
		}
		#endregion
	}
}
