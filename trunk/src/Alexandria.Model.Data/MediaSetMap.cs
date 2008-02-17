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
	public class MediaSetMap : MapBase<IMediaSet>
	{
		#region Constructors
		public MediaSetMap(ISchema schema) : base("MediaSet", schema, MapFunction.Entity)
		{
			Fields.Add(new Field(this, "Id", typeof(Guid), FieldFunction.Identifier, FieldProperties.RequiredAndUnique));
			Fields.Add(new Field(this, "Source", typeof(string), FieldFunction.OpenEndedValue, FieldProperties.Required));
			Fields.Add(new Field(this, "Type", typeof(string), FieldFunction.TypeDescriminator, FieldProperties.Required));
			Fields.Add(new Field(this, "Number", typeof(int), FieldFunction.ComparableValue, FieldProperties.Required));
			Fields.Add(new Field(this, "Title", typeof(string), FieldFunction.Name, FieldProperties.Required));
			Fields.Add(new Field(this, "Artist", typeof(string), FieldFunction.Name, FieldProperties.Required));
			Fields.Add(new Field(this, "Date", typeof(DateTime), FieldFunction.ComparableValue, FieldProperties.Required));
			Fields.Add(new Field(this, "Path", typeof(Uri), FieldFunction.FormattedValue, FieldProperties.RequiredAndUnique));
			Fields.Add(new Field(this, "Items", typeof(IMediaItem), FieldFunction.ManyToManyChildren, FieldProperties.None));
			Fields.Add(new Field(this, "MediaSetParentId", typeof(Guid), FieldFunction.ParentIdentifier, FieldProperties.None));
		}
		#endregion
	
		#region Public Overrides
		public override Field IdentifierField
		{
			get { return Fields["Id"]; }
		}

		public override Field ParentIdentifierField
		{
			get { return Fields["MediaSetParentId"]; }
		}
		
		public override DataTable GetTable()
		{
			throw new NotImplementedException();
		}

		public override DataTable GetTable(IEnumerable<IMediaSet> models)
		{
			throw new NotImplementedException();
		}

		public override IEnumerable<IMediaSet> GetModels(DataTable table)
		{
			throw new NotImplementedException();
		}

		public override void LoadChildren(IEnumerable<IMediaSet> models, IResult result)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
