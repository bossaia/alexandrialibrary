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
			table.Columns.Add("Status", typeof(string));
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
			table.Constraints.Add("pk_Id", table.Columns["Id"], true);
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
				item.Status = GetValue<string>(row[1]);
				item.Source = GetValue<string>(row[2]);
				item.Type = GetValue<string>(row[3]);
				item.Number = GetValue<int>(row[4]);
				item.Title = GetValue<string>(row[5]);
				item.Artist = GetValue<string>(row[6]);
				item.Album = GetValue<string>(row[7]);
				item.Duration = GetValue<TimeSpan>(row[8]);
				item.Date = GetValue<DateTime>(row[9]);
				item.Format = GetValue<string>(row[10]);
				item.Path = GetValue<Uri>(row[11]);
			}
			
			return item;
		}
		
		private DataRow GetRowFromItem(IMediaItem item)
		{
			DataRow row = table.NewRow();
			
			if (item != null)
			{
				row[0] = item.Id;
				row[1] = item.Status;
				row[2] = item.Source;
				row[3] = item.Type;
				row[4] = item.Number;
				row[5] = item.Title;
				row[6] = item.Artist;
				row[7] = item.Album;
				row[8] = item.Duration;
				row[9] = item.Date;
				row[10] = item.Format;
				row[11] = item.Path;
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
				DataRow row = table.NewRow();
				engine.FillRow(row, id);
				item = GetItemFromRow(row);
			}
			
			return item;
		}
		
		public void SaveMediaItem(IMediaItem item)
		{
			if (engine != null && item != null)
			{
				DataRow row = GetRowFromItem(item);
				engine.SaveRow(row);
			}
		}
		
		public void DeleteMediaItem(IMediaItem item)
		{
			if (engine != null && item != null)
			{
				DataRow row = GetRowFromItem(item);
				engine.DeleteRow(row);
			}
		}
	}
}
