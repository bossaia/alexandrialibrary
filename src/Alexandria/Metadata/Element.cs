using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public class Element<T> : IElement<T>
	{
		#region Constructors
		public Element(IIdentifier identifier, T value)
		{
			this.identifier = identifier;
			this.value = value;
		}
		#endregion

		#region Private Fields
		private IIdentifier identifier;
		private T value;
		private IElement parent;
		private List<IElement> children = new List<IElement>();
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

		#region IElement Members
		object IElement.Value
		{
			get { return value; }
			set { this.value = (T)value; }
		}
		
		public IElement Parent
		{
			get { return parent; }
			set { parent = value; }
		}
		
		public IList<IElement> Children
		{
			get { return children; }
		}
		#endregion

		#region IElement<T> Members
		public IIdentifier Identifier
		{
			get { return identifier; }
		}

		public T Value
		{
			get { return value; }
			set { this.value = value; }
		}
		#endregion

		#region IEquatable<T> Members
		public bool Equals(T other)
		{
			return this.Equals((object)other);
		}
		#endregion
		
		#region Static Members
		private static Dictionary<Type, Element<T>> unknownElements = new Dictionary<Type, Element<T>>();

		public static Element<T> Unknown
		{
			get
			{
				if (!unknownElements.ContainsKey(typeof(T)))
					unknownElements[typeof(T)] = new Element<T>(Alexandria.Identifier.Undefined, default(T));

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
	}
}
