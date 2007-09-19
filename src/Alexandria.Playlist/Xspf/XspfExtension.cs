using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Alexandria.Playlist.Xspf
{
	/// <summary>
	/// Extends XSPF playlist and track instances with supplemental content
	/// </summary>
	public class Extension
	{
		#region Constructors
		/// <summary>
		/// Instantiate an Extension
		/// </summary>
		public Extension()
		{
		}

		/// <summary>
		/// Instantiate an Extension
		/// </summary>
		/// <param name="application">The URI of a resource defining the structure and purpose of the content</param>
		/// <param name="content">The supplemental content</param>
		public Extension(Uri application, XmlNode content)
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
			this.application = new Uri(node.Attributes["application"].Value);
			this.content = node.FirstChild;
		}
		#endregion
		
		#region Private Fields
		private Uri application;
		private XmlNode content;
		#endregion
		
		#region Public Properties
		/// <summary>
		/// Get or set the URI of a resource defining the structure and purpose of the content
		/// </summary>
		public Uri Application
		{
			get { return application; }
			set { application = value; }
		}
		
		/// <summary>
		/// Get or set the supplemental content
		/// </summary>
		public XmlNode Content
		{
			get { return content; }
			set { content = value; }
		}
		#endregion
	}
}
