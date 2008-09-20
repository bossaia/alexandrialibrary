using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Alexandria.Extensions.Playlist.Xspf
{
	/// <summary>
	/// Extends XSPF playlist and track instances with supplemental content
	/// </summary>
	public struct Extension
	{
		#region Constructors
		/// <summary>
		/// Instantiate an extension
		/// </summary>
		/// <param name="application">The application URI</param>
		/// <param name="content">The supplemental content</param>
		/// <param name="contentNamespace">The namespace of the supplemental content</param>
		public Extension(Uri application, XmlNodeList content, string contentNamespace)
		{
			this.application = application;
			this.content = content;
			this.contentNamespace = contentNamespace;
		}
			
		/// <summary>
		/// Instantiate an Extension
		/// </summary>
		/// <param name="node">A node containing the application URI and the supplemental content</param>
		public Extension(XmlNode node)
		{
			application = new Uri(node.Attributes["application"].Value);
			this.content = node.ChildNodes;
			this.contentNamespace = node.NamespaceURI.ToString();
		}
		#endregion
		
		#region Private Fields
		private Uri application;
		private XmlNodeList content;
		private string contentNamespace;
		#endregion
		
		#region Public Properties
		/// <summary>
		/// Get the URI of a resource defining the structure and purpose of the content
		/// </summary>
		public Uri Application
		{
			get { return application; }
		}
		
		/// <summary>
		/// Get the supplemental content
		/// </summary>
		public XmlNodeList Content
		{
			get { return content; }
		}
		
		/// <summary>
		/// Get the namespace of the supplemental content
		/// </summary>
		public string ContentNamespace
		{
			get { return contentNamespace; }
		}
		#endregion

		public override string ToString()
		{
			return (Content != null) ? Content.ToString() : string.Empty;
		}
	}
}
