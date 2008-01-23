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
using Telesophy.Alexandria.Persistence;

namespace Telesophy.Alexandria.Model.Data
{
	public class MediaSetMap : MapBase
	{
		#region Constructors
		public MediaSetMap(Schema schema) : base(schema, "MediaSet", typeof(IMediaSet))
		{
			Record.AddField("Id", typeof(Guid), ConstraintType.Identifier);
			Record.AddField("Source", typeof(string));
			Record.AddField("Type", typeof(string));
			Record.AddField("Number", typeof(int));
			Record.AddField("Title", typeof(string));
			Record.AddField("Artist", typeof(string));
			Record.AddField("Date", typeof(string));
			Record.AddField("Path", typeof(Uri), ConstraintType.Unique);
		}
		#endregion

		#region IMap Members
		public override Model Lookup<Model>(Query query)
		{
 			throw new Exception("The method or operation is not implemented.");
		}

		public override IList<Model> List<Model>(Query query)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void Save<Model>(Model model)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void Delete<Model>(Model model)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion
	}
}
