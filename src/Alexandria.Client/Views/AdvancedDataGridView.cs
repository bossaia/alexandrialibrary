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
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Alexandria.Metadata;

namespace Alexandria.Client.Views
{
	public class AdvancedDataGridRowDragDropEventArgs : EventArgs
	{
		public AdvancedDataGridRowDragDropEventArgs(int sourceIndex, int targetIndex, DataGridViewRow sourceRow)
		{
			this.sourceIndex = sourceIndex;
			this.targetIndex = targetIndex;
			this.sourceRow = sourceRow;
			
			if (SourceRow != null)
			{
				Guid id = (Guid)SourceRow.Cells["Id"].Value;
				string source = (string)SourceRow.Cells["Source"].Value;
				string type = (string)SourceRow.Cells["Type"].Value;
				int number = Convert.ToInt32(SourceRow.Cells["Number"].Value);
				string title = (string)SourceRow.Cells["Title"].Value;
				string artist = (string)SourceRow.Cells["Artist"].Value;
				string album = (string)SourceRow.Cells["Album"].Value;
				TimeSpan duration = (TimeSpan)SourceRow.Cells["Duration"].Value;
				DateTime date = (DateTime)SourceRow.Cells["Date"].Value;
				string format = (string)SourceRow.Cells["Format"].Value;
				Uri path = (Uri)SourceRow.Cells["Path"].Value;
				
				mediaItem = new MediaItem(id, source, type, number, title, artist, album, duration, date, format, path);
			}
		}
	
		private int sourceIndex;
		private int targetIndex;
		private DataGridViewRow sourceRow;
		private MediaItem mediaItem;
		
		public int SourceIndex
		{
			get { return sourceIndex; }
		}
		
		public int TargetIndex
		{
			get { return targetIndex; }
		}
		
		public DataGridViewRow SourceRow
		{
			get { return sourceRow; }
		}
		
		public IMediaItem MediaItem
		{
			get { return mediaItem; }
		}
	}
	
	public class AdvancedDataGridViewColumnDragDropEventArgs: EventArgs
	{
	
	}

	/// This class extends the DataGridView to allow for custom
	/// drag and drop reordering operations on both columns and rows.  
	/// The sequence of user interaction events occurs as follows: 
	/// 1. The user clicks (and releases) the left mouse button 
	/// on a row or column header cell to select the row or column. 
	/// 2. The user then clicks (and holds down) the left mouse 
	/// button to initiate a drag and drop operation which will allow 
	/// him/her to reorder the selected row or column within the 
	/// DataGridView. 
	/// 3. As the drag and drop operation begins, a horizontal (for 
	/// rows) or vertical (for columns) red line is displayed on the 
	/// DataGridView to indicate the target of the drag and drop operation 
	/// (i.e., to indicate where on the grid the selected row or column 
	/// will be dropped). 
	/// 4. When the user has selected the new target location for the 
	/// selected row/column, he/she releases the left mouse button, and 
	/// the appropriate reordering of columns or rows is carried out.
	/// ******************************************************************
	///  AUTHOR: Daniel S. Soper
	///     URL: http://www.danielsoper.com
	///    DATE: 02 February 2007
	/// LICENSE: Public Domain. Enjoy!   :-)
	/// ******************************************************************
	/// 
	public class AdvancedDataGridView : DataGridView
	{
		//vars for custom column/row drag/drop operations
		private Rectangle DragDropRectangle;
		private int DragDropSourceIndex;
		private int DragDropTargetIndex;
		private int DragDropCurrentIndex = -1;
		private int DragDropType; //0=column, 1=row
		private DataGridViewColumn DragDropColumn;
		private object[] DragDropColumnCellValue;
		private EventHandler<AdvancedDataGridRowDragDropEventArgs> rowDragDropping;
		private EventHandler<AdvancedDataGridRowDragDropEventArgs> rowDragDropped;
		private EventHandler<AdvancedDataGridViewColumnDragDropEventArgs> columnDragDropping;
		private EventHandler<AdvancedDataGridViewColumnDragDropEventArgs> columnDragDropped;

		public AdvancedDataGridView()
		{
			this.AllowUserToResizeRows = false;
			this.SelectionMode = DataGridViewSelectionMode.CellSelect;
			this.AllowUserToOrderColumns = false;
			this.AllowDrop = true;
			//this.ColumnCount = 20;
			//this.RowCount = 40;
			//this.Size = new Size(600, 400);
		}

		protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
		{
			//e.Column.SortMode = DataGridViewColumnSortMode.Programmatic;
			//e.Column.HeaderText = "column " + e.Column.Index;
			base.OnColumnAdded(e);
		}

		protected override void OnColumnHeaderMouseClick(DataGridViewCellMouseEventArgs e)
		{
			if (e.ColumnIndex > -1)
			{
				if (e.Button == MouseButtons.Left)
				{
					//single-click left mouse button
					if (this.SelectionMode != DataGridViewSelectionMode.ColumnHeaderSelect)
					{
						//this.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
						//this.Columns[e.ColumnIndex].Selected = true;
					}
				}
				else if (e.Button == MouseButtons.Right)
				{
					//single-click right mouse button
					if (this.SelectionMode != DataGridViewSelectionMode.ColumnHeaderSelect)
					{
						//this.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
					}
					if (this.SelectedColumns.Count <= 1)
					{
						this.ClearSelection();
						this.Columns[e.ColumnIndex].Selected = true;
						//show column right-click menu here
						MessageBox.Show("show column right-click menu");
					}
					else //more than one column is selected
					{
						//show column right-click menu here
						MessageBox.Show("show column right-click menu");
					}
				}
			}
			base.OnColumnHeaderMouseClick(e);
		}

		protected override void OnRowHeaderMouseClick(DataGridViewCellMouseEventArgs e)
		{
			//runs when the mouse is clicked over a row header cell
			if (e.RowIndex > -1)
			{
				if (e.Button == MouseButtons.Left)
				{
					//single-click left mouse button
					if (this.SelectionMode != DataGridViewSelectionMode.RowHeaderSelect)
					{
						this.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
						this.Rows[e.RowIndex].Selected = true;
						this.CurrentCell = this[0, e.RowIndex];
					}
				}
				else if (e.Button == MouseButtons.Right)
				{
					//single-click right mouse button
					if (this.SelectionMode != DataGridViewSelectionMode.RowHeaderSelect)
					{
						this.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
					} //end if 
					if (this.SelectedRows.Count <= 1)
					{
						this.ClearSelection();
						this.Rows[e.RowIndex].Selected = true;
						this.CurrentCell = this[0, e.RowIndex];
						//show row right-click menu here
						MessageBox.Show("show row right-click menu");
					}
					else //more than one row is selected
					{
						//show row right-click menu here
						MessageBox.Show("show row right-click menu");
					}
				}
			}
			base.OnRowHeaderMouseClick(e);
		}

		protected override void OnCellMouseClick(DataGridViewCellMouseEventArgs e)
		{
			//runs when the mouse is clicked over a cell
			if (e.RowIndex > -1 && e.ColumnIndex > -1)
			{
				if (e.Button == MouseButtons.Left)
				{
					//single-click left mouse button
					if (this.SelectionMode != DataGridViewSelectionMode.CellSelect)
					{
						this.SelectionMode = DataGridViewSelectionMode.CellSelect;
					}
				}
				else if (e.Button == MouseButtons.Right)
				{
					//single-click right mouse button
					if (this.SelectionMode != DataGridViewSelectionMode.CellSelect)
					{
						this.SelectionMode = DataGridViewSelectionMode.CellSelect;
					}
					if (this.SelectedCells.Count <= 1)
					{
						this.ClearSelection();
						this[e.ColumnIndex, e.RowIndex].Selected = true;
						this.CurrentCell = this[e.ColumnIndex, e.RowIndex];
						//show cell right-click menu here
						MessageBox.Show("show cell right-click menu");
					}
					else //more than one cell is selected
					{
						//show cell right-click menu here
						MessageBox.Show("show cell right-click menu");
					}
				}
			}
			base.OnCellMouseClick(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			//stores values for drag/drop operations if necessary
			if (this.AllowDrop)
			{
				if (this.HitTest(e.X, e.Y).ColumnIndex == -1 && this.HitTest(e.X, e.Y).RowIndex > -1)
				{
					//if this is a row header cell
					if (this.Rows[this.HitTest(e.X, e.Y).RowIndex].Selected)
					{
						//if this row is selected
						DragDropType = 1;
						Size DragSize = SystemInformation.DragSize;
						DragDropRectangle = new Rectangle(new Point(e.X - (DragSize.Width / 2), e.Y - (DragSize.Height / 2)), DragSize);
						DragDropSourceIndex = this.HitTest(e.X, e.Y).RowIndex;
					}
					else
					{
						DragDropRectangle = Rectangle.Empty;
					}
				}
				else if (this.HitTest(e.X, e.Y).ColumnIndex > -1 && this.HitTest(e.X, e.Y).RowIndex == -1)
				{
					//if this is a column header cell
					if (this.Columns[this.HitTest(e.X, e.Y).ColumnIndex].Selected)
					{
						DragDropType = 0;
						DragDropSourceIndex = this.HitTest(e.X, e.Y).ColumnIndex;
						Size DragSize = SystemInformation.DragSize;
						DragDropRectangle = new Rectangle(new Point(e.X - (DragSize.Width / 2), e.Y - (DragSize.Height / 2)), DragSize);
					}
					else
					{
						DragDropRectangle = Rectangle.Empty;
					}
				}
				else
				{
					DragDropRectangle = Rectangle.Empty;
				}
			}
			else
			{
				DragDropRectangle = Rectangle.Empty;
			}
			base.OnMouseDown(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			//handles drag/drop operations if necessary
			if (this.AllowDrop)
			{
				if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
				{
					if (DragDropRectangle != Rectangle.Empty && !DragDropRectangle.Contains(e.X, e.Y))
					{
						if (DragDropType == 0)
						{
							//column drag/drop
							DragDropEffects DropEffect = this.DoDragDrop(this.Columns[DragDropSourceIndex], DragDropEffects.Move);
						}
						else if (DragDropType == 1)
						{
							//row drag/drop
							DragDropEffects DropEffect = this.DoDragDrop(this.Rows[DragDropSourceIndex], DragDropEffects.Move);
						}
					}
				}
			}
			base.OnMouseMove(e);
		}

		protected override void OnDragOver(DragEventArgs e)
		{
			//runs while the drag/drop is in progress
			if (this.AllowDrop)
			{
				e.Effect = DragDropEffects.Move;
				if (DragDropType == 0)
				{
					//column drag/drop
					int CurCol = this.HitTest(this.PointToClient(new Point(e.X, e.Y)).X, this.PointToClient(new Point(e.X, e.Y)).Y).ColumnIndex;
					if (DragDropCurrentIndex != CurCol)
					{
						DragDropCurrentIndex = CurCol;
						this.Invalidate(); //repaint
					}
				}
				else if (DragDropType == 1)
				{
					//row drag/drop
					int CurRow = this.HitTest(this.PointToClient(new Point(e.X, e.Y)).X, this.PointToClient(new Point(e.X, e.Y)).Y).RowIndex;
					if (DragDropCurrentIndex != CurRow)
					{
						DragDropCurrentIndex = CurRow;
						this.Invalidate(); //repaint
					}
				}
			}
			base.OnDragOver(e);
		}

		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			//runs after a drag/drop operation for column/row has completed
			if (this.AllowDrop)
			{
				if (drgevent.Effect == DragDropEffects.Move)
				{
					Point ClientPoint = this.PointToClient(new Point(drgevent.X, drgevent.Y));
					if (DragDropType == 0)
					{
						//if this is a column drag/drop operation
						DragDropTargetIndex = this.HitTest(ClientPoint.X, ClientPoint.Y).ColumnIndex;
						if (DragDropTargetIndex > -1 && DragDropCurrentIndex < this.ColumnCount - 1)
						{
							DragDropCurrentIndex = -1;
							
							//holds the appearance of the source column
							DragDropColumn = this.Columns[DragDropSourceIndex];
							
							//holds the values of the cells in the source column
							DragDropColumnCellValue = new object[this.RowCount - 1];
							for (int i = 0; i < this.RowCount; i++)
							{
								//for each cell in the source column
								if (this.Rows[i].Cells[DragDropSourceIndex].Value != null)
								{
									//if this cell has a value, store it in the object array
									DragDropColumnCellValue[i] = this.Rows[i].Cells[DragDropSourceIndex].Value;
								}
							}

							if (ColumnDragDropping != null)
								ColumnDragDropping(this, new AdvancedDataGridViewColumnDragDropEventArgs());

							//remove the source column
							this.Columns.RemoveAt(DragDropSourceIndex);

							//insert a new column at the target index using the source column as a template
							this.Columns.Insert(DragDropTargetIndex, new DataGridViewColumn(DragDropColumn.CellTemplate));

							//copy the source column's header cell to the new column
							this.Columns[DragDropTargetIndex].HeaderCell = DragDropColumn.HeaderCell;

							//select the newly-inserted column
							this.Columns[DragDropTargetIndex].Selected = true;

							//update the position of the cuurent cell in the DGV
							this.CurrentCell = this[DragDropTargetIndex, 0];
							for (int i = 0; i < this.RowCount; i++)
							{
								//for each cell in the new column
								if (DragDropColumnCellValue[i] != null)
								{
									//set the cell's value equal to that of the corresponding cell in the source column
									this.Rows[i].Cells[DragDropTargetIndex].Value = DragDropColumnCellValue[i];
								}
							}
							
							if (ColumnDragDropped != null)
								ColumnDragDropped(this, new AdvancedDataGridViewColumnDragDropEventArgs());
							
							//release resources
							DragDropColumnCellValue = null;
							DragDropColumn = null;
						}
					}
					else if (DragDropType == 1)
					{
						//if this is a row drag/drop operation
						DragDropTargetIndex = this.HitTest(ClientPoint.X, ClientPoint.Y).RowIndex;
						if (DragDropTargetIndex > -1 && DragDropCurrentIndex < this.RowCount - 1)
						{
						
							DragDropCurrentIndex = -1;
							DataGridViewRow SourceRow = drgevent.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;

							if (RowDragDropping != null)
								RowDragDropping(this, new AdvancedDataGridRowDragDropEventArgs(DragDropSourceIndex, DragDropTargetIndex, SourceRow));
							
							//this.Rows.RemoveAt(DragDropSourceIndex);
							//this.Rows.Insert(DragDropTargetIndex, SourceRow);
							//this.Rows[DragDropTargetIndex].Selected = true;
							
							if (RowDragDropped != null)
								RowDragDropped(this, new AdvancedDataGridRowDragDropEventArgs(DragDropSourceIndex, DragDropTargetIndex, SourceRow));

							this.CurrentCell = this[0, DragDropTargetIndex];
						}
					}
				}
			}
			base.OnDragDrop(drgevent);
		}

		protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
		{
			//draws red drag/drop target indicator lines if necessary
			if (DragDropCurrentIndex > -1)
			{
				if (DragDropType == 0)
				{
					//column drag/drop
					if (e.ColumnIndex == DragDropCurrentIndex && DragDropCurrentIndex < this.ColumnCount - 1)
					{
						//if this cell is in the same column as the mouse cursor
						Pen p = new Pen(Color.Red, 1);
						e.Graphics.DrawLine(p, e.CellBounds.Left - 1, e.CellBounds.Top, e.CellBounds.Left - 1, e.CellBounds.Bottom);
					}
				}
				else if (DragDropType == 1)
				{
					//row drag/drop
					if (e.RowIndex == DragDropCurrentIndex && DragDropCurrentIndex < this.RowCount - 1)
					{
						//if this cell is in the same row as the mouse cursor
						Pen p = new Pen(Color.Red, 1);
						e.Graphics.DrawLine(p, e.CellBounds.Left, e.CellBounds.Top - 1, e.CellBounds.Right, e.CellBounds.Top - 1);
					}
				}
			}
			base.OnCellPainting(e);
		}

		public EventHandler<AdvancedDataGridRowDragDropEventArgs> RowDragDropping
		{
			get { return rowDragDropping; }
			set { rowDragDropping = value; }
		}
		
		public EventHandler<AdvancedDataGridRowDragDropEventArgs> RowDragDropped
		{
			get { return rowDragDropped; }
			set { rowDragDropped = value; }
		}
		
		public EventHandler<AdvancedDataGridViewColumnDragDropEventArgs> ColumnDragDropping
		{
			get { return columnDragDropping; }
			set { columnDragDropping = value; }
		}
		
		public EventHandler<AdvancedDataGridViewColumnDragDropEventArgs> ColumnDragDropped
		{
			get { return columnDragDropped; }
			set { columnDragDropped = value; }
		}
	}
}