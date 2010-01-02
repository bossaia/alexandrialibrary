using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Telesophy.Alexandria.Clients.Ankh.Controllers
{
	internal struct ColumnMap
	{
		public ColumnMap(string name, string propertyName)
		{
			Name = name;
			PropertyName = propertyName;
		}

		public string Name;
		public string PropertyName;
	}

	internal class QueueColumns
	{
		static QueueColumns()
		{
			ColumnsByName = new Dictionary<string, ColumnMap>();
			ColumnsByName.Add(Status.Name, Status);
			ColumnsByName.Add(Type.Name, Type);
			ColumnsByName.Add(Source.Name, Source);
			ColumnsByName.Add(Number.Name, Number);
			ColumnsByName.Add(Title.Name, Title);
			ColumnsByName.Add(Artist.Name, Artist);
			ColumnsByName.Add(Album.Name, Album);
			ColumnsByName.Add(Duration.Name, Duration);
			ColumnsByName.Add(Date.Name, Date);
			ColumnsByName.Add(Format.Name, Format);
			ColumnsByName.Add(Path.Name, Path);
			ColumnsByName.Add(Id.Name, Id);

			ColumnsByPropertyName = new Dictionary<string, ColumnMap>();
			ColumnsByPropertyName.Add(Status.PropertyName, Status);
			ColumnsByPropertyName.Add(Type.PropertyName, Type);
			ColumnsByPropertyName.Add(Source.PropertyName, Source);
			ColumnsByPropertyName.Add(Number.PropertyName, Number);
			ColumnsByPropertyName.Add(Title.PropertyName, Title);
			ColumnsByPropertyName.Add(Artist.PropertyName, Artist);
			ColumnsByPropertyName.Add(Album.PropertyName, Album);
			ColumnsByPropertyName.Add(Duration.PropertyName, Duration);
			ColumnsByPropertyName.Add(Date.PropertyName, Date);
			ColumnsByPropertyName.Add(Format.PropertyName, Format);
			ColumnsByPropertyName.Add(Path.PropertyName, Path);
			ColumnsByPropertyName.Add(Id.PropertyName, Id);
		}

		public readonly static Dictionary<string, ColumnMap> ColumnsByName;

		public readonly static Dictionary<string, ColumnMap> ColumnsByPropertyName;

		public readonly static ColumnMap Status = new ColumnMap("statusColumn", "Status");
		public readonly static ColumnMap Type = new ColumnMap("typeColumn", "Type");
		public readonly static ColumnMap Source = new ColumnMap("sourceColumn", "Source");
		public readonly static ColumnMap Number = new ColumnMap("numberColumn", "Number");
		public readonly static ColumnMap Title = new ColumnMap("titleColumn", "Title");
		public readonly static ColumnMap Artist = new ColumnMap("artistColumn", "Artist");
		public readonly static ColumnMap Album = new ColumnMap("albumColumn", "Album");
		public readonly static ColumnMap Duration = new ColumnMap("durationColumn", "Duration");
		public readonly static ColumnMap Date = new ColumnMap("dateColumn", "Date");
		public readonly static ColumnMap Format = new ColumnMap("formatColumn", "Format");
		public readonly static ColumnMap Path = new ColumnMap("pathColumn", "Path");
		public readonly static ColumnMap Id = new ColumnMap("idColumn", "Id");
	}

	internal struct PlayModes
	{
		public const string RepeatList = "RepeatList";
		public const string RepeatTrack = "RepeatTrack";
		public const string Random = "Random";
	}

	internal struct MediaTypes
	{
		public const string Audio = "Audio";
		public const string Image = "Image";
		public const string Text = "Text";
		public const string Video = "Video";
	}

    internal static class ContextHelper
    {
		private static string GetAppConfigString(string key)
		{
			return ConfigurationManager.AppSettings[key] ?? string.Empty;
		}

		public static string GetAudioDirectory()
		{
			return GetAppConfigString("Config.Directory.Audio");
		}

		public static int GetMaximumSortColumns()
		{
			string value = GetAppConfigString("Config.UI.MaxSorts");

			int maxSorts = 6;
			int.TryParse(value, out maxSorts);

			return maxSorts;
		}

		public static string GetPlaylistDirectory()
		{
			return GetAppConfigString("Config.Directory.Playlist");
		}

		public static string GetPlaylistFormats()
		{
			return GetAppConfigString("Config.Formats.Playlist");
		}

		public static string GetAudioFormats()
		{
			return GetAppConfigString("Config.Formats.Audio");
		}
    }
}