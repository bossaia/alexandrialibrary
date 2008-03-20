using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Extensions.Playlist.Xspf
{
	/// <summary>
	/// Represents metadata for an XSPF playlist or track
	/// </summary>
	public struct Metadata
	{
		#region Constructors
		/// <summary>
		/// Instantiate a Metadata item
		/// </summary>
		/// <param name="rel">The URI of the resource defining the metadata</param>
		/// <param name="content">The value of the metadata</param>
		public Metadata(Uri rel, string content)
		{
			this.rel = rel;
			this.content = content;
		}
		
		/// <summary>
		/// Instantiate a Metadata item
		/// </summary>
		/// <param name="node">The XML node to get the content from</param>
		public Metadata(XmlNode node)
		{
			rel = new Uri(node.Attributes["rel"].Value);
			content = node.InnerText;
		}
		#endregion

		#region Private Fields
		private Uri rel;
		private string content;
		#endregion

		#region Private Static Methods
		private static Uri GetRel(XmlNode node)
		{
			return new Uri(node.Attributes["rel"].Value);
		}

		private static  string GetContent(XmlNode node)
		{
			return node.InnerText;
		}
		#endregion
		
		#region Public Properties
		/// <summary>
		/// Get the URI of the resource defining the metadata
		/// </summary>
		public Uri Rel
		{
			get { return rel; }
		}

		/// <summary>
		/// Get the value of the metadata
		/// </summary>
		public string Content
		{
			get { return content; }
		}
		#endregion

		public override string ToString()
		{
			return (Content != null) ? Content : string.Empty;
		}
	}
}
