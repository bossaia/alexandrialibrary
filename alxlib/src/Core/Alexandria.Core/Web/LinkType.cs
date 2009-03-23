using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Web
{
	public enum LinkType
	{
		/// <summary>
		/// Authors may wish to define additional link types not described in this specification. If they do so, they should use a profile to cite the conventions used to define the link types.
		/// </summary>
		Other = 0,

		/// <summary>
		/// Designates substitute versions for the document in which the link occurs. When used together with the lang attribute, it implies a translated version of the document. When used together with the media attribute, it implies a version designed for a different medium (or media).
		/// </summary>
		Alternate,

		/// <summary>
		/// Refers to an external style sheet. See the section on external style sheets for details. This is used together with the link type "Alternate" for user-selectable alternate style sheets.
		/// </summary>
		Stylesheet,

		/// <summary>
		/// Refers to the first document in a collection of documents. This link type tells search engines which document is considered by the author to be the starting point of the collection.
		/// </summary>
		Start,

		/// <summary>
		/// Refers to the next document in a linear sequence of documents. User agents may choose to preload the "next" document, to reduce the perceived load time.
		/// </summary>
		Next,

		/// <summary>
		/// Refers to the previous document in an ordered series of documents. Some user agents also support the synonym "Previous".
		/// </summary>
		Prev,

		/// <summary>
		/// Refers to a document serving as a table of contents. Some user agents also support the synonym ToC (from "Table of Contents").
		/// </summary>
		Contents,

		/// <summary>
		/// Refers to a document providing an index for the current document.
		/// </summary>
		Index,

		/// <summary>
		/// Refers to a document providing a glossary of terms that pertain to the current document.
		/// </summary>
		Glossary,

		/// <summary>
		/// Refers to a copyright statement for the current document.
		/// </summary>
		Copyright,

		/// <summary>
		/// Refers to a document serving as a chapter in a collection of documents.
		/// </summary>
		Chapter,

		/// <summary>
		/// Refers to a document serving as a section in a collection of documents.
		/// </summary>
		Section,

		/// <summary>
		/// Refers to a document serving as a subsection in a collection of documents.
		/// </summary>
		Subsection,

		/// <summary>
		/// Refers to a document serving as an appendix in a collection of documents.
		/// </summary>
		Appendix,

		/// <summary>
		/// Refers to a document offering help (more information, links to other sources information, etc.)
		/// </summary>
		Help,

		/// <summary>
		/// Refers to a bookmark. A bookmark is a link to a key entry point within an extended document. The title attribute may be used, for example, to label the bookmark. Note that several bookmarks may be defined in each document.
		/// </summary>
		Bookmark
	}
}
