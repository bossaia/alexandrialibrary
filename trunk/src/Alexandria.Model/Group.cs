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
using System.Text;

namespace Telesophy.Alexandria.Model
{
	public class Group : IGroup
	{
		#region Constructors
		public Group()
		{
		}

		public Group(Guid id, string name, DateTime beginDate, DateTime endDate)
		{
			this.id = id;
			this.name = name;
			this.beginDate = beginDate;
			this.endDate = endDate;
		}
		#endregion

		#region Private Fields
		private Guid id;
		private string type = Constants.ARTIST_TYPE_GROUP;
		private string name;
		private DateTime beginDate;
		private DateTime endDate;
		private IList<IArtist> members = new List<IArtist>();
		#endregion

		#region IArtist Members
		public Guid Id
		{
			get { return id; }
			set { id = value; }
		}

		public string Type
		{
			get { return type; }
			set { }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public DateTime BeginDate
		{
			get { return beginDate; }
			set { beginDate = value; }
		}

		public DateTime EndDate
		{
			get { return endDate; }
			set { endDate = value; }
		}
		#endregion
		
		#region IGroup Members
		public IList<IArtist> Members
		{
			get { return members; }
		}
		#endregion
	}
}