using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Entities
{
	public class Identifier :
		IKey<IEntity>,
		IEquatable<IKey<IEntity>>
	{
		public Identifier(string scheme, string host)
		{
			if (string.IsNullOrEmpty(scheme))
				throw new ArgumentNullException("scheme");

			if (string.IsNullOrEmpty(host))
				throw new ArgumentNullException("host");

			_scheme = scheme;
			_host = host;
		}

		public Identifier(string scheme, string host, string path)
			: this(scheme, host)
		{
			_path = path ?? string.Empty;
		}

		public Identifier(string scheme, string host, string path, string query)
			: this(scheme, host, path)
		{
			_query = query ?? string.Empty;
		}

		public Identifier(string scheme, string host, int port, string path, string query, string fragment)
			: this(scheme, host, path, query)
		{
			_port = port;
			_fragment = fragment ?? string.Empty;
		}

		private string _scheme;
		private string _host;
		private int _port;
		private string _path;
		private string _query;
		private string _fragment;

		private const string schemeSeparator = "://";
		private const string portSeparator = ":";
		private const string querySeparator = "?";
		private const string fragmentSeparator = "#";

		public string Scheme
		{
			get { return _scheme; }
		}

		public string Host
		{
			get { return _host; }
		}

		public int Port
		{
			get { return _port; }
		}

		public string Path
		{
			get { return _path; }
		}

		public string Query
		{
			get { return _query; }
		}

		public string Fragment
		{
			get { return _fragment; }
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(obj, null))
				return false;

			if (obj is IKey<IEntity>) 
				return Equals((IKey<IEntity>)obj);

			return false;
		}

		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			builder.Append(_scheme);
			builder.Append(schemeSeparator);
			builder.Append(_host);
			
			if (_port > 0)
			{
				builder.Append(portSeparator);
				builder.Append(_port);
			}

			if (!string.IsNullOrEmpty(_path))
			{
				builder.Append(_path);
			}

			if (!string.IsNullOrEmpty(_query))
			{
				builder.Append(querySeparator);
				builder.Append(_query);
			}

			if (!string.IsNullOrEmpty(_fragment))
			{
				builder.Append(fragmentSeparator);
				builder.Append(_fragment);
			}

			return builder.ToString();
		}

		#region IEquatable<IKey<IEntity>> Members

		public bool Equals(IKey<IEntity> other)
		{
			return ToString() == other.ToString();
		}

		#endregion

		public static Identifier Undefined = new Identifier("urn", "0");
	}
}
