using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abraxas
{
	public struct MediaType
	{
		public MediaType(ContentType type, string subtype)
		{
			_type = type;
			_subtype = subtype;
			_name = string.Format("{0}/{1}", Enum.GetName(typeof(ContentType), _type), _subtype);
		}

		private ContentType _type;
		private string _subtype;
		private string _name;

		ContentType ContentType { get { return _type; } }
		string ContentSubtype { get { return _subtype; } }
		string Name { get { return _name; } }

		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		public override string ToString()
		{
			return _name;
		}
	}
}
