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
		public MediaSetWithAllChildren() : base("MediaSetWithAllChildren")
		{
		}
		#endregion		
		
		#region Overrides
		public override void Initialize(ISchema schema)
		{
			base.Initialize(schema);

			Map<IMediaSet, IMediaItem> itemsMap = new Map<IMediaSet, IMediaItem>("MediaSetItems", schema);
			itemsMap.Branches.Add(Root.Associations["MediaSetItems"]);
			Maps.Add(itemsMap);
		}
		
		public override IList<Tuple> GetTuples(IEnumerable<IMediaSet> models, DateTime timeStamp)
		{
			IList<Tuple> tuples = new List<Tuple>();
		
			Entity<IMediaItem> itemEntity = Root.Schema.GetEntity<IMediaItem>();
		
			foreach (IMediaSet model in models)
			{
				Tuple rootTuple = Root.GetTuple(model);
				tuples.Add(rootTuple);
				
				foreach (IMediaItem item in model.Items)
				{
					Tuple assocTuple = Root.Associations["MediaSetItems"].GetTuple(model.Id, item.Id, timeStamp);
					tuples.Add(assocTuple);
					
					Tuple itemTuple = itemEntity.GetTuple(item);
					tuples.Add(itemTuple);
				}
			}
			
			return tuples;
		}
		
		public override ICollection<IMediaSet> List(DataSet dataSet)
		{
			IDictionary<string, ICollection<IMediaSet>> dict = Root.GetModels(dataSet.Tables[Root.Name], null);
			
			Entity<IMediaItem> itemEntity = Schema.GetEntity<IMediaItem>();
			IDictionary<string, ICollection<IMediaItem>> childrenById = itemEntity.GetModels(dataSet.Tables["MediaSetItems"], Root.Associations["MediaSetItems"]);
			
			foreach (IMediaSet model in dict[Root.Name])
			{
				string key = model.Id.ToString();
				if (childrenById.ContainsKey(key))
				{
					foreach (IMediaItem child in childrenById[key])
					{
						child.Parent = model;
						model.Items.Add(child);
					}
				}
			}
			
			if (dict.Count > 0)
			{
				return dict[Root.Name];
			}
			else return new List<IMediaSet>();
		}
		#endregion
	}
}
