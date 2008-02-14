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
using System.Data;
using System.Linq;

using Telesophy.Babel.Persistence;

namespace Telesophy.Alexandria.Model.Data
{
	public class MediaItemMap : MapBase<IMediaItem>
	{
		#region Constructors
		public MediaItemMap(ISchema schema) : base("IMediaItem", schema, MapFunction.Entity)
		{
			Fields.Add(new Field(this, "Id", typeof(Guid), FieldFunction.Identifier, FieldProperties.RequiredAndUnique));
			Fields.Add(new Field(this, "Name", typeof(string), FieldFunction.Name, FieldProperties.Required));
		}
		#endregion
		
		#region Public Overrides
		public override Field IdentifierField
		{
			get { return Fields["Id"]; }
		}
		
		public override DataTable GetTable(IEnumerable<IMediaItem> models)
		{
			return null;
		}

		public override IEnumerable<IMediaItem> GetModels(DataTable table)
		{
			return null;
		}

		public override void LoadChildren(IEnumerable<IMediaItem> models, IResult result)
		{
		}
		#endregion
	}
}
