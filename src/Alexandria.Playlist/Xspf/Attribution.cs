using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Playlist.Xspf
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
		/// <param name="isIdentifier">A value indicating whether or not this attribution represents a canonical identifier</param>
		/// <param name="content">A URI of the content of this attribution</param>
		public Attribution(bool isIdentifier, Uri content)
		{
			this.isIdentifier = isIdentifier;
			this.content = content;
		}
		
		/// <summary>
		/// Instantiate an Attribution
		/// </summary>
		/// <param name="node">A XmlNode containing the attribution information</param>
		public Attribution(XmlNode node)
		{
			isIdentifier = GetIsIdentifier(node);
			content = GetContent(node);
		}
		#endregion
		
		#region Private Fields
		private bool isIdentifier;
		private Uri content;
		#endregion
		
		#region Private Static Methods
		private static bool GetIsIdentifier(XmlNode node)
		{
			return (string.Compare(node.Name, "identifier", true) == 0);
		}
		
		private static Uri GetContent(XmlNode node)
		{
			return new Uri(node.Value);
		}
		#endregion
		
		#region Public Properties
		/// <summary>
		/// Get a value indicating whether or not this attribution represents a canonical identifier
		/// </summary>
		public bool IsIdentifier
		{
			get { return isIdentifier; }
		}
		
		/// <summary>
		/// Get a URI of the content of this attribution
		/// </summary>
		/// <remarks>If IsIdentifier is true then this Uri represents an identifier, otherwise it represents a location</remarks>
		public Uri Content
		{
			get { return content; }
		}
		#endregion
	}
}
