﻿using System;
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
			
			//itemGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(itemGrid_CellFormatting);
			mediaItemSearchBox.SearchCompleted += new EventHandler<MediaItemSearchEventArgs>(mediaItemSearchBox_SearchCompleted);
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

		private ImageList smallImageList;
		//private DataGridViewRow selectedRow;
		//private int selectedRowSaveIndex;
		#endregion
		
		#region Private Methods
		//private void itemGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		//{
		//    if (itemGrid.Columns[e.ColumnIndex].Name == COLUMN_DURATION)
		//    {
		//        TimeSpan duration = (e.Value != null) ? (TimeSpan)e.Value : TimeSpan.Zero;
		//        if (duration.Hours > 0)
		//        {
		//            e.Value = string.Format("{0:00}:{1:00}:{2:00}", duration.Hours, duration.Minutes, duration.Seconds);
		//        }
		//        else if (duration.Minutes > 0)
		//        {
		//            e.Value = string.Format("{0:00}:{1:00}", duration.Minutes, duration.Seconds);
		//        }
		//        else
		//        {
		//            e.Value = string.Format("0:{0:00}", duration.Seconds);
		//        }
		//    }
		//    else if (itemGrid.Columns[e.ColumnIndex].Name == COLUMN_TYPE)
		//    {
		//        string value = (e.Value != null) ? e.Value.ToString() : string.Empty;
			
		//        switch(value)
		//        {
		//            case ControllerConstants.TYPE_AUDIO:
		//                e.Value = smallImageList.Images[ControllerConstants.INDEX_AUDIO];
		//                break;
		//            case ControllerConstants.TYPE_BOOK:
		//                e.Value = smallImageList.Images[ControllerConstants.INDEX_BOOK];
		//                break;
		//            case ControllerConstants.TYPE_IMAGE:
		//                e.Value = smallImageList.Images[ControllerConstants.INDEX_IMAGE];
		//                break;
		//            case ControllerConstants.TYPE_MOVIE:
		//                e.Value = smallImageList.Images[ControllerConstants.INDEX_MOVIE];
		//                break;
		//            case ControllerConstants.TYPE_TELEVISION:
		//                e.Value = smallImageList.Images[ControllerConstants.INDEX_TELEVISION];
		//                break;
		//            default:
		//                e.Value = smallImageList.Images[ControllerConstants.INDEX_AUDIO];
		//                break;
		//        }
		//    }
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
		
		private void mediaItemSearchBox_SearchCompleted(object sender, MediaItemSearchEventArgs e)
		{
			if (e != null && e.Data != null)
			{
				if (e.Data.Count > 0)
				{
					if (e.Data.Count == 1 || e.AllowMultiple)
					{
						foreach (MediaItemData item in e.Data)
							itemGrid.AddItem(item);
					}
					else
					{
						MediaItemSearchResults results = new MediaItemSearchResults();
						results.LoadData(e.Data);
						results.Instructions = "Multiple items were found that matched your search criteria. Please select which item(s) you want to add to the playlist and click OK.";
						results.SmallImageList = smallImageList;
						results.ChoiceAccepted += new EventHandler<MediaItemSearchEventArgs>(mediaItemSearchBox_SearchCompleted);
						results.Show();
					}
				}
				else
				{
					MessageBox.Show("No items were found that matched your search criteria.\nYou may want to widen your search.", "NO MATCHING ITEMS FOUND");
				}
			}
		}
		#endregion
		
		#region Public Properties
		public ToolController ToolController
		{
			get { return toolController; }
			set
			{
				toolController = value;
				if (toolController != null)
				{
					mediaItemSearchBox.PersistenceController = toolController.PersistenceController;
				}
			}
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
