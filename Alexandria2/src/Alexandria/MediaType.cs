using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria
{
	public struct MediaType
		: IEquatable<MediaType>
	{
		public MediaType(string type, string subtype)
		{
			_type = type.ToLowerInvariant() ?? string.Empty;
			_subtype = subtype.ToLowerInvariant() ?? string.Empty;
		}

		private readonly string _type;
		private readonly string _subtype;

		public string Type
		{
			get { return _type; }
		}

		public string Subtype
		{
			get { return _subtype; }
		}

		#region Operators

		public static bool operator ==(MediaType mediaType1, MediaType mediaType2)
		{
			return mediaType1.Equals(mediaType2);
		}

		public static bool operator !=(MediaType mediaType1, MediaType mediaType2)
		{
			return !mediaType1.Equals(mediaType2);
		}

		#endregion

		#region Overrides

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != typeof(MediaType))
				return false;

			return Equals((MediaType)obj);
		}

		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		public override string ToString()
		{
			return _type + "/" + _subtype;
		}

		#endregion

		#region IEquatable<MediaType> Members

		public bool Equals(MediaType other)
		{
			return ToString() == other.ToString(); 
		}

		#endregion
	}
}
