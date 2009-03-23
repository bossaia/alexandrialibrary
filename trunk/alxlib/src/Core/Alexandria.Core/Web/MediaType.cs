using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Web
{
	public struct MediaType
	{
		public enum ContentType
		{
			other = 0,
			application,
			audio,
			example,
			image,
			message,
			model,
			multipart,
			text,
			video
		}

		private MediaType(ContentType type, string subType)
			: this(type, subType, null)
		{
		}

		private MediaType(ContentType type, string subType, params string[] aliases)
		{
			this.type = type;
			this.subType = subType;
			this.aliases = new List<string>();

			if (aliases != null)
			{
				foreach (string alias in aliases)
					this.aliases.Add(alias);
			}
		}

		private ContentType type;
		private string subType;
		private IList<string> aliases;

		public ContentType Type
		{
			get { return type; }
		}

		public string SubType
		{
			get { return subType; }
		}

		public IList<string> Aliases
		{
			get { return aliases; }
		}

		public override string ToString()
		{
			return GetMediaTypeName(Type, SubType);
		}

		#region Static Members
		public static MediaType Empty = default(MediaType);

		private static IList<MediaType> types = new List<MediaType>();
		private static IDictionary<string, MediaType> byName = new Dictionary<string, MediaType>();

		static MediaType()
		{
			types.Add(new MediaType(ContentType.audio, "mpeg"));
			types.Add(new MediaType(ContentType.audio, "ogg"));

			foreach (MediaType type in types)
			{
				byName.Add(GetMediaTypeName(type.Type, type.SubType), type);

				foreach (string alias in type.Aliases)
				{
					byName.Add(GetMediaTypeName(type.Type, alias), type);
				}
			}
		}

		private static string GetMediaTypeName(ContentType type, string subType)
		{
			return string.Format("{0}/{1}", System.Enum.GetName(typeof(ContentType), type), subType);
		}

		public static MediaType GetByName(string name)
		{
			if (byName.ContainsKey(name))
				return byName[name];
			else
				return MediaType.Empty;
		}
		#endregion
	}
}
