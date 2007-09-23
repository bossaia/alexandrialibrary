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
			Uri.TryCreate(node.Attributes["rel"].Value, UriKind.RelativeOrAbsolute, out rel);
			Uri.TryCreate(node.Value, UriKind.RelativeOrAbsolute, out content);
		}
		#endregion
	
		#region Private Fields
		private Uri rel;
		private Uri content;
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
