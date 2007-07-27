using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Alexandria.Media
{
	public class BaseMediaFormat : IMediaFormat
	{
		#region Constructors
		public BaseMediaFormat(string name, string description)
		{
			this.name = name;
			this.description = description;
		}
		#endregion
	
		#region Private Fields
		private string name;
		private string description;
		private IList<ContentType> contentTypes = new List<ContentType>();
		private IList<string> fileExtentions = new List<string>();
		private IList<ICodec> codecs = new List<ICodec>();
		#endregion
	
		#region IMediaFormat Members
		public string Name
		{
			get { return name; }
		}

		public string Description
		{
			get { return description; }
		}

		public IList<ContentType> ContentTypes
		{
			get { return contentTypes; }
		}

		public IList<string> FileExtensions
		{
			get { return fileExtentions; }
		}

		public IList<ICodec> Codecs
		{
			get { return codecs; }
		}
		#endregion
	}
}
