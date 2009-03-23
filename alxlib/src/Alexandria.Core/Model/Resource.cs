using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Model
{
	public abstract class Resource
	{
		#region Protected Constructors

		protected Resource()
		{
		}

		protected Resource(Uri type)
		{
			Type = type;
		}

		#endregion

		#region Private Members

		private IList<Tag> tags = new List<Tag>();
		private IList<Link> links = new List<Link>();

		#endregion

		/// <summary>
		/// Get the identifier of this resource
		/// </summary>
		public Uri Identifier { get; set; }

		/// <summary>
		/// Get the type of this resource
		/// </summary>
		public Uri Type { get; set; }

		/// <summary>
		/// Get or set the name of this resource
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Get or set the name that is primary credited with creating this resource
		/// </summary>
		public string Creator { get; set; }

		/// <summary>
		/// Get or set the date that is associated with this resource
		/// (e.g. date of birth for a person, date founded for a band, date created for media)
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>
		/// Get a list of metadata tags applied to this resource
		/// </summary>
		public IList<Tag> Tags
		{
			get { return tags; }
		}

		/// <summary>
		/// Get a list of links that associate this resource with other resources
		/// </summary>
		public IList<Link> Links
		{
			get { return links; }
		}
	}
}
