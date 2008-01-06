#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Telesophy.Alexandria.Clients.Ankh.Controllers;

namespace Telesophy.Alexandria.Clients.Ankh.Views
{
	public class FilterDragDropData
	{
		#region Constructors
		public FilterDragDropData(ListViewItem item)
		{
			if (item != null && item.Tag != null)
			{
				this.item = item;
				
				FilterInfo filterInfo = (FilterInfo)item.Tag;
				column = filterInfo.Column;
				columnType = filterInfo.ColumnType;
				@operator = filterInfo.Operator;
				value = filterInfo.Value;
				imageIndex = item.ImageIndex;
			}
		}
		
		public FilterDragDropData(string column, Type columnType, string @operator, string value)
		{
			this.column = column;
			this.columnType = columnType;
			this.@operator = @operator;
			this.value = value;
			this.imageIndex = GetFilterImageIndex(column);
		}
		#endregion
		
		#region Private Fields
		private string column;
		private Type columnType;
		private string @operator;
		private string value;
		private int imageIndex;
		private ListViewItem item;
		#endregion
		
		#region Private Methods
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
		#endregion
		
		#region Public Properties
		public string Column
		{
			get { return column; }
		}
		
		public Type ColumnType
		{
			get { return columnType; }
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
		#endregion
		
		#region Public Methods
		public FilterInfo GetFilterInfo()
		{
			return new FilterInfo(Column, ColumnType, Operator, Value);
		}
		#endregion
	}
}
