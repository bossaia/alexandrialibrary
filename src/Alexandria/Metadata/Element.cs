using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public class Element<T> : IElement<T>
	{
		#region Constructors
		public Element(Uri identifier, T value)
		{
			this.identifier = identifier;
			this.value = value;
		}
		#endregion

		#region Private Fields
		private Uri identifier;
		private T value;
		#endregion

		#region Private Static Fields
		private static Dictionary<Type, Element<T>> unknownElements = new Dictionary<Type, Element<T>>();
		#endregion

		#region Public Static Members
		public static Element<T> Unknown
		{
			get
			{
				if (!unknownElements.ContainsKey(typeof(T)))
					unknownElements[typeof(T)] = new Element<T>(null, default(T));

				return unknownElements[typeof(T)];
			}
		}

		public static bool operator ==(Element<T> element1, Element<T> element2)
		{
			if (element1 != null && element2 != null)
			{
				return element1.Equals(element2);
			}
			else return false;
		}

		public static bool operator !=(Element<T> element1, Element<T> element2)
		{
			if (element1 != null && element2 != null)
			{
				return !element1.Equals(element2);
			}
			else return true;
		}
		#endregion

		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj != null)
			{
				Element<T> other = obj as Element<T>;
				if (other != null)
				{
					return (this.Identifier == other.Identifier && this.Value.Equals(other.Value));
				}
				else return false;
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override string ToString()
		{
			return Value.ToString();
		}
		#endregion

		#region IElement<T> Members
		public Uri Identifier
		{
			get { return identifier; }
		}

		public T Value
		{
			get { return value; }
		}
		#endregion

		#region IEquatable<T> Members
		public bool Equals(T other)
		{
			return this.Equals((object)other);
		}
		#endregion
	}
}
