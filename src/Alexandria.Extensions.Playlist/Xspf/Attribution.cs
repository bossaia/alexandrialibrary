using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Extensions.Playlist.Xspf
{
	/// <summary>
	/// Represents and attribution to a previous playlist that this playlist is derived from
	/// </summary>
	public struct Attribution
	{
		#region Constructors
		/// <summary>
		/// Instantiate an Attribution
		/// </summary>
		/// <param name="value">The attributable data</param>
		public Attribution(IAttributable value)
		{
			this.value = value;
		}
		
		/// <summary>
		/// Instantiate an Attribution
		/// </summary>
		/// <param name="node">A XmlNode containing the attribution information</param>
		public Attribution(XmlNode node)
		{
			value = LoadAttributable(node);
		}
		#endregion
		
		#region Private Fields
		private IAttributable value;
		#endregion
		
		#region Private Static Methods
		private static IAttributable LoadAttributable(XmlNode node)
		{
			if (string.Compare(node.Name, "identifier", true) == 0)
				return new Identifier(node);
			else if (string.Compare(node.Name, "location", true) == 0)
				return new Location(node);
			else return null;
		}
		#endregion
		
		#region Public Properties
		/// <summary>
		/// Get the value for this attribution
		/// </summary>
		public IAttributable Value
		{
			get { return value; }
		}
		#endregion
	}
}
