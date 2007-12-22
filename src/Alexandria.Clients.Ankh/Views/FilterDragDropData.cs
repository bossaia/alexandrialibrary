using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Telesophy.Alexandria.Clients.Ankh.Controllers;

namespace Telesophy.Alexandria.Clients.Ankh.Views
{
	public class FilterDragDropData
	{
		public FilterDragDropData(ListViewItem item)
		{
			if (item != null && item.Tag != null)
			{
				this.item = item;
				
				FilterInfo filterInfo = (FilterInfo)item.Tag;
				column = filterInfo.Column;
				@operator = filterInfo.Operator;
				value = filterInfo.Value;
				imageIndex = item.ImageIndex;
			}
		}
		
		public FilterDragDropData(string column, string @operator, string value)
		{
			this.column = column;
			this.@operator = @operator;
			this.value = value;
			this.imageIndex = GetFilterImageIndex(column);
		}
				
		#region Private Fields
		private string column;
		private string @operator;
		private string value;
		private int imageIndex;
		private ListViewItem item;
		#endregion
		
		private int GetFilterImageIndex(string column)
		{
			switch (column)
			{
				case ControllerConstants.COL_STATUS:
					return 1;
				case ControllerConstants.COL_TYPE:
					return 2;
				case ControllerConstants.COL_SOURCE:
					return 3;
				case ControllerConstants.COL_NUMBER:
					return 4;
				case ControllerConstants.COL_TITLE:
					return 5;
				case ControllerConstants.COL_ARTIST:
					return 6;
				case ControllerConstants.COL_ALBUM:
					return 7;
				case ControllerConstants.COL_DURATION:
					return 8;
				case ControllerConstants.COL_DATE:
					return 9;
				case ControllerConstants.COL_FORMAT:
					return 10;
				case ControllerConstants.COL_PATH:
					return 11;
				default:
					return 0;
			}
		}
		
		public string Column
		{
			get { return column; }
		}
		
		public string Operator
		{
			get { return @operator; }
		}
		
		public string Value
		{
			get { return value; }
		}
		
		public int ImageIndex
		{
			get { return imageIndex; }
		}
		
		public ListViewItem Item
		{
			get { return item; }
		}
		
		public FilterInfo GetFilterInfo()
		{
			return new FilterInfo(Column, Operator, Value);
		}
	}
}
