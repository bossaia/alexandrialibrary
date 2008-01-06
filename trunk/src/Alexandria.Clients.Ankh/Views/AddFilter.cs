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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Telesophy.Alexandria.Clients.Ankh.Views
{
	public partial class AddFilter : Form
	{
		internal AddFilter(ImageList columnImageList, FilterUpdateCallback updateCallback)
		{
			InitializeComponent();
			
			this.columnImageList = columnImageList;
			this.updateCallback = updateCallback;
			
			this.anyButton.Image = columnImageList.Images[0];
			this.statusButton.Image = columnImageList.Images[1];
			this.typeButton.Image = columnImageList.Images[2];
			this.sourceButton.Image = columnImageList.Images[3];
			this.numberButton.Image = columnImageList.Images[4];
			this.titleButton.Image = columnImageList.Images[5];
			this.artistButton.Image = columnImageList.Images[6];
			this.albumButton.Image = columnImageList.Images[7];
			this.durationButton.Image = columnImageList.Images[8];
			this.dateButton.Image = columnImageList.Images[9];
			this.formatButton.Image = columnImageList.Images[10];
			this.pathButton.Image = columnImageList.Images[11];
		}
		
		private FilterUpdateCallback updateCallback;
		
		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(columnTextBox.Text) && !string.IsNullOrEmpty(operatorComboBox.Text))
			{
				updateCallback(columnTextBox.Text, operatorComboBox.Text, valueTextBox.Text);
				Close();
			}
			else MessageBox.Show("Please select a column, operator and (optionally) a value for this filter", "Invalid Filter");
		}

		private void anyButton_Click(object sender, EventArgs e)
		{
			this.columnTextBox.Text = "Any";
		}

		private void statusButton_Click(object sender, EventArgs e)
		{
			this.columnTextBox.Text = "Status";
		}

		private void typeButton_Click(object sender, EventArgs e)
		{
			this.columnTextBox.Text = "Type";
		}

		private void sourceButton_Click(object sender, EventArgs e)
		{
			this.columnTextBox.Text = "Source";
		}

		private void numberButton_Click(object sender, EventArgs e)
		{
			this.columnTextBox.Text = "Number";
		}

		private void titleButton_Click(object sender, EventArgs e)
		{
			this.columnTextBox.Text = "Title";
		}

		private void artistButton_Click(object sender, EventArgs e)
		{
			this.columnTextBox.Text = "Artist";
		}

		private void albumButton_Click(object sender, EventArgs e)
		{
			this.columnTextBox.Text = "Album";
		}

		private void durationButton_Click(object sender, EventArgs e)
		{
			this.columnTextBox.Text = "Duration";
		}

		private void dateButton_Click(object sender, EventArgs e)
		{
			this.columnTextBox.Text = "Date";
		}

		private void formatButton_Click(object sender, EventArgs e)
		{
			this.columnTextBox.Text = "Format";
		}

		private void pathButton_Click(object sender, EventArgs e)
		{
			this.columnTextBox.Text = "Path";
		}
	}
}