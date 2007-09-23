using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Alexandria.Playlist.Xspf
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
		public Extension(Uri application, XmlNodeList content)
		{
			this.application = application;
			this.content = content;
		}
			
		/// <summary>
		/// Instantiate an Extension
		/// </summary>
		/// <param name="node">A node containing the application URI and the supplemental content</param>
		public Extension(XmlNode node)
		{
			Uri.TryCreate(node.Attributes["application"].Value, UriKind.RelativeOrAbsolute, out application);
			this.content = node.ChildNodes;
		}
		#endregion
		
		#region Private Fields
		private Uri application;
		private XmlNodeList content;
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
		#endregion
	}
}
