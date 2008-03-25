using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Telesophy.Alexandria.Clients.Ankh.Views.Data;
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
			
			itemGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(itemGrid_CellFormatting);
		}
		#endregion
		
		#region Private Constants
		private const string COLUMN_ID = "idColumn";
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
		private ToolController toolController;
		private PlaylistSaveConfirmHandle saveConfirmHandle;

		//private BindingListView<MediaItemData> bindingList;
		//private BindingSource bindingSource;
		private ImageList smallImageList;
		private DataGridViewRow selectedRow;
		private int selectedRowSaveIndex;
		#endregion
		
		#region Private Methods
		private void itemGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			//if (e.Value != null)
			//{
				//queueTable.Columns[e.ColumnIndex].ColumnName == COL_DURATION)
				if (itemGrid.Columns[e.ColumnIndex].Name == COLUMN_DURATION)
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
				//if (queueTable.Columns[e.ColumnIndex].ColumnName == COL_TYPE)
				if (itemGrid.Columns[e.ColumnIndex].Name == COLUMN_TYPE)
				{
					string value = (e.Value != null) ? e.Value.ToString() : string.Empty;
				
					switch(value)
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
			//}
			//else e.Value = string.Empty;
		}

		//private void RowDragDropping(object sender, AdvancedDataGridRowDragDropEventArgs e)
		//{
		//    if (e != null)
		//    {
		//        DataGridViewRow x = e.SourceRow;
		//    }
		//}

		//private void RowDragDropped(object sender, AdvancedDataGridRowDragDropEventArgs e)
		//{
		//    bindingList.RemoveAt(e.SourceIndex);
		//    bindingList.Insert(e.TargetIndex, e.MediaItemData);
		//    itemGrid.Rows[e.TargetIndex].Selected = true;
		//}

		//private void ColumnDragDropping(object sender, AdvancedDataGridViewColumnDragDropEventArgs e)
		//{
		//    selectedRowSaveIndex = selectedRow.Index;
		//}

		//private void ColumnDragDropped(object sender, AdvancedDataGridViewColumnDragDropEventArgs e)
		//{
		//    selectedRow = itemGrid.Rows[selectedRowSaveIndex];
		//}
		
		private void CheckForValidDrag(DragEventArgs e)
		{
			e.Effect = DragDropEffects.None;

			if (e != null && e.Data != null)
			{
				object row = e.Data.GetData(typeof(DataGridViewRow));
				if (row != null)
				{
					e.Effect = DragDropEffects.Move;
				}
			}
		}

		private void itemGrid_DragEnter(object sender, DragEventArgs e)
		{
			CheckForValidDrag(e);
		}

		private void itemGrid_DragOver(object sender, DragEventArgs e)
		{
			CheckForValidDrag(e);
		}

		private void itemGrid_DragDrop(object sender, DragEventArgs e)
		{
			object x = e;
		}

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
		
		#region Public Properties
		public ToolController ToolController
		{
			get { return toolController; }
			set { toolController = value; }
		}

		public ImageList SmallImageList
		{
			get { return smallImageList; }
			set { smallImageList = value; }
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
		//public void Initialize()
		//{
		//    bindingList = new BindingListView<MediaItemData>();
		//    bindingList.AllowRemove = true;

		//    bindingSource = new BindingSource();
		//    bindingSource.DataSource = bindingList;
			
		//    //itemGrid.AutoGenerateColumns = false;
		//    //itemGrid.DataSource = bindingSource;
		//    //itemGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(grid_CellFormatting);
		//    //itemGrid.RowDragDropping += new EventHandler<AdvancedDataGridRowDragDropEventArgs>(RowDragDropping);
		//    //itemGrid.RowDragDropped += new EventHandler<AdvancedDataGridRowDragDropEventArgs>(RowDragDropped);
		//    //itemGrid.ColumnDragDropping += new EventHandler<AdvancedDataGridViewColumnDragDropEventArgs>(ColumnDragDropping);
		//    //itemGrid.ColumnDragDropped += new EventHandler<AdvancedDataGridViewColumnDragDropEventArgs>(ColumnDragDropped);
		//}
		
		public void AddItem(MediaItemData item)
		{
			itemGrid.AddItem(item);
		}
		
		public IList<MediaItemData> GetItemData()
		{
			return itemGrid.GetItems();
		}
		#endregion
	}
}
