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
using System.Text;

using Telesophy.Babel.Persistence;

namespace Telesophy.Alexandria.Model.Data
{
	public class MediaSetWithAllChildren : Aggregate<IMediaSet>
	{
		#region Constructors
		public MediaSetWithAllChildren(ISchema schema) : base("MediaSetWithAllChildren", schema)
		{
			Map<IMediaSet, IMediaItem> itemsMap = new Map<IMediaSet, IMediaItem>("MediaSetItems", schema);
			itemsMap.Branches.Add(Root.Associations["MediaSetItems"]);
			
			//Map<IMediaSet, IArtist> creatorsMap = new Map<IMediaSet, IArtist>("MediaSetCreators", schema);
			//creatorsMap.Branches.Add(Root.Associations["MediaSetCreators"]);
			
			//Map<IMediaSet, IArtist> itemsArtistMap = new Map<IMediaSet, IArtist>("MediaItemArtist", schema);
			//itemsArtistMap.Branches.Add(Root.Associations["MediaSetItems"]);
			//itemsArtistMap.Branches.Add(Schema.Entities[typeof(IMediaItem)].Associations["MediaItemArtist"]);
			
			Maps.Add(itemsMap);
			//Maps.Add(creatorsMap);
			//Maps.Add(itemsArtistMap);
		}
		#endregion		
		
		#region Overrides
		public override DataSet GetDataSet(IEnumerable<IMediaSet> models, DateTime timeStamp)
		{
			DataSet dataSet = new DataSet(Name);
			
			DataTable rootTable = Root.GetDataTable(Root.Name);			
			dataSet.Tables.Add(rootTable);
			
			foreach (Map map in Maps)
			{
				foreach (Association branch in map.Branches)
				{
					if (!dataSet.Tables.Contains(branch.Name))
					{
						DataTable table = branch.GetDataTable();
						dataSet.Tables.Add(table);
					}
				}
			}
			
			foreach (IMediaSet model in models)
			{
				Root.AddDataRow(dataSet.Tables[Root.Name], model);
				
				IList<Guid> childrenIds = new List<Guid>();
				foreach (IMediaItem item in model.Items)
				{
					 childrenIds.Add(item.Id);
				}
				
				Maps["MediaSetItems"].Branches[0].AddDataRows<Guid, Guid>(dataSet.Tables["MediaSetItems"], model.Id, childrenIds, timeStamp);
			}
			
			return dataSet;
		}
				
		public override IList<IMediaSet> Load(DataSet dataSet)
		{
			IList<IMediaSet> list = new List<IMediaSet>();
			
			return list;
		}
		#endregion
	}
}
