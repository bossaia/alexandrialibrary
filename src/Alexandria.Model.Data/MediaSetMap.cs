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

using Telesophy.Alexandria.Model;
using Telesophy.Alexandria.Persistence;

namespace Telesophy.Alexandria.Model.Data
{
	public class MediaSetMap : MapBase<IMediaSet>
	{
		#region Constructors
		public MediaSetMap(IRepository repository, IRecord<IMediaSet> record) : base(repository, record)
		{
		}
		#endregion

		#region IMap Members
		public override IMediaSet Lookup(Query query)
		{
			Batch batch = new Batch("Lookup MediaSet");
			
			ICommand setLookupCommand = Repository.Engine.GetLookupCommand(query);
			Query childQuery = Relationships.GetRelationship<IMediaSet, IMediaItem>().GetListChildrenQuery(query);
			ICommand itemLookupCommand = Repository.Engine.GetLookupCommand(childQuery);
			
			//TODO: Figure out how to use the IMediaItem map here instead of just a command
			//      e.g. Maps could return commands so that we can recursively add commands to the batch
			
			batch.Commands.Add(setLookupCommand);
			batch.Commands.Add(itemLookupCommand);
			
			IResult result = Repository.Engine.Run(batch);
			
			if (result.Successful)
			{
				IRecord<IMediaSet> setRecord = (IRecord<IMediaSet>)Record;
				IRecord<IMediaItem> itemRecord = (IRecord<IMediaItem>)Record.Schema.Records["MediaItem"];
				
				IMediaSet mediaSet = setRecord.GetModel(result.CommandResults[setLookupCommand].Tuples[0]);
				
				foreach (Tuple tuple in result.CommandResults[itemLookupCommand].Tuples)
				{
					IMediaItem mediaItem = itemRecord.GetModel(tuple);				
					mediaSet.Items.Add(mediaItem);
				}
				
				return mediaSet;
			}
			
			return null;
		}

		public override IList<IMediaSet> List(Query query)
		{
			return null;
		}

		public override void Save(IMediaSet model)
		{
		}

		public override void Delete(IMediaSet model)
		{
		}
		#endregion
	}
}
