using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Alexandria.Persistence;

namespace Alexandria.Metadata
{
	public class MediaItemDataMap
	{
		public MediaItemDataMap()
		{
			table = new DataTable("MediaItem");
			table.Columns.Add("Id", typeof(Guid));
			table.Columns.Add("Source", typeof(string));
			table.Columns.Add("Type", typeof(string));
			table.Columns.Add("Number", typeof(int));
			table.Columns.Add("Title", typeof(string));
			table.Columns.Add("Artist", typeof(string));
			table.Columns.Add("Album", typeof(string));
			table.Columns.Add("Duration", typeof(TimeSpan));
			table.Columns.Add("Date", typeof(DateTime));
			table.Columns.Add("Format", typeof(string));
			table.Columns.Add("Path", typeof(Uri));
			table.Constraints.Add(new UniqueConstraint(table.Columns["Id"], true));
			table.Constraints.Add(new UniqueConstraint(table.Columns["Path"]));
		}
		
		private IPersistenceEngine engine;
		private DataTable table;
		
		private T GetValue<T>(object data)
		{
			if (data != null && data != DBNull.Value)
			{
				return (T)data;
			}
			
			return default(T);
		}
		
		private IMediaItem GetItemFromRow(DataRow row)
		{
			IMediaItem item = new MediaItem();
		
			if (row != null)
			{
				item.Id = GetValue<Guid>(row[0]);
				item.Source = GetValue<string>(row[1]);
				item.Type = GetValue<string>(row[2]);
				item.Number = GetValue<int>(row[3]);
				item.Title = GetValue<string>(row[4]);
				item.Artist = GetValue<string>(row[5]);
				item.Album = GetValue<string>(row[6]);
				item.Duration = GetValue<TimeSpan>(row[7]);
				item.Date = GetValue<DateTime>(row[8]);
				item.Format = GetValue<string>(row[9]);
				item.Path = GetValue<Uri>(row[10]);
			}
			
			return item;
		}
		
		private DataRow GetRowFromItem(IMediaItem item)
		{
			DataRow row = table.NewRow();
			
			if (item != null)
			{
				row[0] = item.Id;
				row[1] = item.Source;
				row[2] = item.Type;
				row[3] = item.Number;
				row[4] = item.Title;
				row[5] = item.Artist;
				row[6] = item.Album;
				row[7] = item.Duration;
				row[8] = item.Date;
				row[9] = item.Format;
				row[10] = item.Path;
			}
			
			return row;
		}
		
		public IPersistenceEngine Engine
		{
			get { return engine; }
			set { engine = value; }
		}
		
		public DataTable Table
		{
			get { return table; }
		}
		
		public IMediaItem LookupMediaItem(Guid id)
		{
			IMediaItem item = null;
			
			if (engine != null)
			{
				engine.FillTable(table, id);
				if (table.Rows.Count > 0)
				{
					item = GetItemFromRow(table.Rows[0]);
					table.Rows[0].Delete();
				}
			}
			
			return item;
		}
		
		public IList<IMediaItem> ListAll()
		{
			IList<IMediaItem> items = new List<IMediaItem>();
			
			if (engine != null)
			{
				engine.FillTable(Table, default(Guid));
				if (Table.Rows.Count > 0)
				{
					foreach(DataRow row in Table.Rows)
					{
						IMediaItem item = GetItemFromRow(row);
						items.Add(item);
					}
				
					Table.Rows.Clear();
				}
			}
			
			return items;
		}
		
		public void SaveMediaItem(IMediaItem item)
		{
			if (engine != null && item != null)
			{
				DataRow row = GetRowFromItem(item);
				engine.SaveRow(row);
				row.Delete();
			}
		}
		
		public void DeleteMediaItem(IMediaItem item)
		{
			if (engine != null && item != null)
			{
				DataRow row = GetRowFromItem(item);
				engine.DeleteRow(row);
				row.Delete();
			}
		}
	}
}
