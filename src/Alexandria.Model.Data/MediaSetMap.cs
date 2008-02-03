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
		public override IMediaSet Lookup(IResult result)
		{
			IMediaSet mediaSet = Record.GetModel(result.Contents[CommandTypes.LOOKUP_MEDIASET].Tuples[0]);

			IList<IMediaItem> items = Repository.List<IMediaItem>(result);
			foreach(IMediaItem item in items)
			{
				mediaSet.Items.Add(item);
			}

			return mediaSet;	
		}

		public override IList<IMediaSet> List(IResult result)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion

		#region Old IMap Members
		/*
		public override IMediaSet Lookup(Query query)
		{
			Batch batch = new Batch("Lookup MediaSet");
			
			ICommand setLookupCommand = Repository.Engine.GetLookupCommand(CommandTypes.LOOKUP_MEDIASET, query);
			Query childQuery = Relationships.GetRelationship<IMediaSet, IMediaItem>().GetListChildrenQuery(query);
			ICommand itemLookupCommand = Repository.Engine.GetLookupCommand(CommandTypes.LOOKUP_MEDIASET_ITEMS, childQuery);
						
			batch.Commands.Add(setLookupCommand);
			batch.Commands.Add(itemLookupCommand);
			
			IResult result = Repository.Engine.Run(batch);
			
			if (result.Successful)
			{
				IRecord<IMediaSet> setRecord = (IRecord<IMediaSet>)Record;
				IRecord<IMediaItem> itemRecord = (IRecord<IMediaItem>)Record.Schema.Records["MediaItem"];
				
				IMediaSet mediaSet = setRecord.GetModel(result.Contents[CommandTypes.LOOKUP_MEDIASET].Tuples[0]);
				
				foreach (Tuple tuple in result.Contents[CommandTypes.LOOKUP_MEDIASET_ITEMS].Tuples)
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
			IList<IMediaSet> list = new List<IMediaSet>();

			Batch batch = new Batch("Lookup MediaSet");

			ICommand setLookupCommand = Repository.Engine.GetLookupCommand(CommandTypes.LOOKUP_MEDIASET, query);
			Query childQuery = Relationships.GetRelationship<IMediaSet, IMediaItem>().GetListChildrenQuery(query);
			ICommand itemLookupCommand = Repository.Engine.GetLookupCommand(CommandTypes.LOOKUP_MEDIASET_ITEMS, childQuery);

			batch.Commands.Add(setLookupCommand);
			batch.Commands.Add(itemLookupCommand);

			IResult result = Repository.Engine.Run(batch);

			if (result.Successful)
			{
				IRecord<IMediaSet> setRecord = (IRecord<IMediaSet>)Record;
				IRecord<IMediaItem> itemRecord = (IRecord<IMediaItem>)Record.Schema.Records["MediaItem"];

				foreach (Tuple tuple in result.Contents[CommandTypes.LOOKUP_MEDIASET].Tuples)
				{
					IMediaSet item = setRecord.GetModel(tuple);
					list.Add(item);
				}

				foreach (Tuple tuple in result.Contents[CommandTypes.LOOKUP_MEDIASET_ITEMS].Tuples)
				{
					IMediaItem mediaItem = itemRecord.GetModel(tuple);
					if (mediaItem != null && list.Count > 0)
					{
						foreach (IMediaSet mediaSet in list)
						{
							if (mediaSet.Id == tuple.GetValue<Guid>(itemRecord.LinkFields[typeof(IMediaSet)]))
							{
								mediaSet.Items.Add(mediaItem);
							}
						}
					}
				}
			}
			
			return list;
		}

		public override void Save(IMediaSet model)
		{
			if (model != null)
			{
				Batch batch = new Batch("Save MediaSet");
				
				IRecord<IMediaSet> setRecord = (IRecord<IMediaSet>)Record;
				IRecord<IMediaItem> itemRecord = (IRecord<IMediaItem>)Record.Schema.Records["MediaItem"];
				
				ICommand setSaveCommand = Repository.Engine.GetSaveCommand(setRecord.GetTuple(model));
				batch.Commands.Add(setSaveCommand);
				
				foreach(IMediaItem item in model.Items)
				{
					ICommand itemSaveCommand = Repository.Engine.GetSaveCommand(itemRecord.GetTuple(item));
					batch.Commands.Add(itemSaveCommand);
				}
				
				IResult result = Repository.Engine.Run(batch);
				if (!result.Successful && result.Error != null)
					throw result.Error;
			}
		}

		public override void Delete(IMediaSet model)
		{
			if (model != null)
			{
				Batch batch = new Batch("Delete MediaSet");

				IRecord<IMediaSet> setRecord = (IRecord<IMediaSet>)Record;
				IRecord<IMediaItem> itemRecord = (IRecord<IMediaItem>)Record.Schema.Records["MediaItem"];

				ICommand setDeleteCommand = Repository.Engine.GetDeleteCommand(setRecord.GetTuple(model));
				batch.Commands.Add(setDeleteCommand);

				foreach (IMediaItem item in model.Items)
				{
					ICommand itemDeleteCommand = Repository.Engine.GetDeleteCommand(itemRecord.GetTuple(item));
					batch.Commands.Add(itemDeleteCommand);
				}

				IResult result = Repository.Engine.Run(batch);
				if (!result.Successful && result.Error != null)
					throw result.Error;
			}
		}
		*/
		#endregion
	}
}
