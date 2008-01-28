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
	public class CatalogSchema : Schema
	{
		#region Constructors
		public CatalogSchema() : base("Catalog")
		{
			MediaSetRecord mediaSet = new MediaSetRecord(this);
			MediaItemRecord mediaItem = new MediaItemRecord(this);
			MediaSetMediaItemRecord mediaSetMediaItem = new MediaSetMediaItemRecord(this);
			IRelationship mediaSetItems = new Relationship<IMediaSet, IMediaItem>("MediaSetItems", this, RelationshipType.ManyToMany, mediaSet, mediaItem, mediaSetMediaItem, mediaSet.Fields[0], mediaItem.Fields[0], mediaSetMediaItem.Fields[0], mediaSetMediaItem.Fields[1]);
			//IRelationship mediaSetParent = new Relationship("MediaSetParent", this, RelationshipType.OneToMany, mediaSet.GetIdentifierFields()[0], mediaSetMediaItem.GetIdentifierFields()[0], "MediaItemChild");
			//IRelationship mediaItemChild = new Relationship("MediaItemChild", this, RelationshipType.ManyToOne, mediaSetMediaItem.GetIdentifierFields()[1], mediaItem.GetIdentifierFields()[0], "MediaSetParent");
		
			Records.Add(mediaSet);
			Records.Add(mediaItem);
			Records.Add(mediaSetMediaItem);
			Relationships.Add(mediaSetItems);
			//Relationships.Add(mediaItemChild);
		}
		#endregion
	}
}
