#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2008 Dan Poage
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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Telesophy.Alexandria.Clients.Ankh.Controllers;
using Telesophy.Alexandria.Clients.Ankh.Views.Data;

namespace Telesophy.Alexandria.Clients.Ankh.Views
{
	public partial class MediaItemSearchBox : UserControl
	{
		#region Constructors
		public MediaItemSearchBox()
		{
			InitializeComponent();
		}
		#endregion

		#region Private Fields
		private PersistenceController persistenceController;
		private EventHandler<MediaItemSearchEventArgs> searchCompleted;
		#endregion

		#region Overrides
		protected override void OnPaint(PaintEventArgs pe)
		{

			base.OnPaint(pe);
		}
		#endregion

		#region Private Event Methods
		private void searchButton_Click(object sender, EventArgs e)
		{
			string search = searchTextBox.Text;
			IList<MediaItemData> list = persistenceController.ListMediaItemData(search);

			if (SearchCompleted != null)
			{
				MediaItemSearchEventArgs searchArgs = new MediaItemSearchEventArgs(list);
				SearchCompleted(this, searchArgs);
			}
		}
		#endregion

		#region Public Properties
		[CLSCompliant(false)]
		public PersistenceController PersistenceController
		{
			get { return persistenceController; }
			set { persistenceController = value; }
		}

		public EventHandler<MediaItemSearchEventArgs> SearchCompleted
		{
			get { return searchCompleted; }
			set { searchCompleted = value; }
		}
		#endregion
	}
}
