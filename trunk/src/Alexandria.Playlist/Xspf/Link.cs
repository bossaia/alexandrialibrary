using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Playlist.Xspf
{
	/// <summary>
	/// Represents a link to a related resource
	/// </summary>
	public struct Link
	{
		#region Constructors
		/// <summary>
		/// Instantiate a Link
		/// </summary>
		/// <param name="rel">The URI of the related resource type</param>
		/// <param name="content">The URI of the related resource</param>
		public Link(Uri rel, Uri content)
		{
			this.rel = rel;
			this.content = content;
		}
		
		/// <summary>
		/// Instantiate a Link
		/// </summary>
		/// <param name="node">The XML node to get the content from</param>
		public Link(XmlNode node)
		{
			rel = GetRel(node);
			content = GetContent(node);
		}
		#endregion
	
		#region Private Fields
		private Uri rel;
		private Uri content;
		#endregion
		
		#region Private Static Methods
		private static Uri GetRel(XmlNode node)
		{
			return new Uri(node.Attributes["rel"].Value);
		}
		
		private static Uri GetContent(XmlNode node)
		{
			return new Uri(node.Value);
		}
		#endregion
		
		#region Public Properties
		/// <summary>
		/// Get the URI of the related resource type
		/// </summary>
		public Uri Rel
		{
			get { return rel; }
		}
		
		/// <summary>
		/// Get the URI of the related resource
		/// </summary>
		public Uri Content
		{
			get { return content; }
		}
		#endregion
	}
}
