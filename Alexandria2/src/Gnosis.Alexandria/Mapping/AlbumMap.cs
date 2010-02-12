using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Mapping
{
	public class AlbumMap
		: ClassMapBase<IAlbum>
	{
		public AlbumMap(IContext context)
			: base(context, "Album")
		{
			Columns.Add(COL_NAME, typeof(string));
			Columns.Add(COL_DISPLAY_NAME, typeof(string));
			Columns.Add(COL_SEARCH_NAME, typeof(string));
			Columns.Add(COL_HASH, typeof(string));
			Columns.Add(COL_ARTIST, typeof(int));
			Columns.Add(COL_TYPE, typeof(string));
			Columns.Add(COL_DATE, typeof(DateTime));
			Columns.Add(COL_COUNTRY, typeof(string));
			Columns.Add(COL_NUMBER, typeof(int));
		}

		private const string COL_NAME = "Name";
		private const string COL_DISPLAY_NAME = "DisplayName";
		private const string COL_SEARCH_NAME = "SearchName";
		private const string COL_HASH = "Hash";
		private const string COL_ARTIST = "Artist";
		private const string COL_TYPE = "Type";
		private const string COL_DATE = "Date";
		private const string COL_COUNTRY = "Country";
		private const string COL_NUMBER = "Number";

		protected override IAlbum CreateInstance(long id)
		{
			return Context.Albums.Get(id);
		}

		protected override object GetValue(IAlbum entity, string column)
		{
			if (entity == null)
				return null;

			switch (column)
			{
				case COL_ARTIST:
					return entity.Artist.Id;
				case COL_COUNTRY:
					return entity.Country.Code;
				case COL_DATE:
					return entity.Date;
				case COL_DISPLAY_NAME:
					return entity.Name.DisplayName;
				case COL_HASH:
					return entity.Name.Hash;
				case COL_NAME:
					return entity.Name.OriginalName;
				case COL_NUMBER:
					return entity.Number;
				case COL_SEARCH_NAME:
					return entity.Name.SearchName;
				case COL_TYPE:
					return entity.Type.Code;
				default:
					return null;
			}
		}

		protected override void SetValue(IAlbum entity, string column, object value)
		{
			if (entity != null)
			{
				switch (column)
				{
					case COL_ARTIST:
						entity.ChangeArtist(Context.Artists.Get(Convert.ToInt64(value)));
						break;
					case COL_COUNTRY:
						entity.ChangeCountry(Context.Countries.Get((string)value));
						break;
					case COL_DATE:
						entity.ChangeDate(DateTime.Parse((string)value));
						break;
					case COL_NAME:
						entity.Rename(new Name((string)value));
						break;
					case COL_NUMBER:
						entity.ChangeNumber(Convert.ToInt32(value));
						break;
					case COL_TYPE:
						entity.ChangeType(Context.AlbumTypes.Get((string)value));
						break;
					default:
						break;
				}
			}
		}
	}
}
