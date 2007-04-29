/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : tag.cpp from TagLib
 ***************************************************************************/

/***************************************************************************
 *   This library is free software; you can redistribute it and/or modify  *
 *   it  under the terms of the GNU Lesser General Public License version  *
 *   2.1 as published by the Free Software Foundation.                     *
 *                                                                         *
 *   This library is distributed in the hope that it will be useful, but   *
 *   WITHOUT ANY WARRANTY; without even the implied warranty of            *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU     *
 *   Lesser General Public License for more details.                       *
 *                                                                         *
 *   You should have received a copy of the GNU Lesser General Public      *
 *   License along with this library; if not, write to the Free Software   *
 *   Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  *
 *   USA                                                                   *
 ***************************************************************************/

using System;
using System.Collections.Generic;
using Alexandria;

namespace Alexandria.TagLib
{
	public class CombinedTag : Tag
	{
		#region Constructors
		public CombinedTag(params Tag[] tags): base()
		{
			SetTags(tags);
		}
		
		public CombinedTag(IList<Tag> tags) : base()
		{
			SetTags(tags);
		}
		#endregion
	
		#region Private Fields
		private List<Tag> tags = new List<Tag>();
		#endregion	

		#region Public Properties
		public override string Title
		{
			get
			{
				string output = null;
				foreach (Tag tag in tags)
					if (tag != null && output == null)
						output = tag.Title;
				return output;
			}
			set
			{
				foreach (Tag tag in tags)
					if (tag != null)
						tag.Title = value;
			}
		}

		public override IList<string> Artists
		{
			get
			{
				IList<string> output = new List<string>();
				foreach (Tag tag in tags)
					if (tag != null && output.Count == 0)
						output = tag.Artists;
				return output;
			}
			set
			{
				foreach (Tag tag in tags)
					if (tag != null)
						tag.Artists = value;
			}
		}

		public override IList<string> Performers
		{
			get
			{
				IList<string> output = new List<string>();
				foreach (Tag tag in tags)
					if (tag != null && output.Count == 0)
						output = tag.Performers;
				return output;
			}
			set
			{
				foreach (Tag tag in tags)
					if (tag != null)
						tag.Performers = value;
			}
		}

		public override IList<string> Composers
		{
			get
			{
				IList<string> output = new List<string>();
				foreach (Tag tag in tags)
					if (tag != null && output.Count == 0)
						output = tag.Composers;
				return output;
			}
			set
			{
				foreach (Tag tag in tags)
					if (tag != null)
						tag.Composers = value;
			}
		}

		public override string Album
		{
			get
			{
				string output = null;
				foreach (Tag tag in tags)
					if (tag != null && output == null)
						output = tag.Album;
				return output;
			}
			set
			{
				foreach (Tag tag in tags)
					if (tag != null)
						tag.Album = value;
			}
		}

		public override string Comment
		{
			get
			{
				string output = null;
				foreach (Tag tag in tags)
					if (tag != null && output == null)
						output = tag.Comment;
				return output;
			}
			set
			{
				foreach (Tag tag in tags)
					if (tag != null)
						tag.Comment = value;
			}
		}

		public override IList<string> Genres
		{
			get
			{
				IList<string> output = new List<string>();
				foreach (Tag tag in tags)
					if (tag != null && output.Count == 0)
						output = tag.Genres;
				return output;
			}
			set
			{
				foreach (Tag tag in tags)
					if (tag != null)
						tag.Genres = value;
			}
		}

		[System.CLSCompliant(false)]
		public override uint Year
		{
			get
			{
				uint output = 0;
				foreach (Tag tag in tags)
					if (tag != null && output == 0)
						output = tag.Year;
				return output;
			}
			set
			{
				foreach (Tag tag in tags)
					if (tag != null)
						tag.Year = value;
			}
		}

		[System.CLSCompliant(false)]
		public override uint Track
		{
			get
			{
				uint output = 0;
				foreach (Tag tag in tags)
					if (tag != null && output == 0)
						output = tag.Track;
				return output;
			}
			set
			{
				foreach (Tag tag in tags)
					if (tag != null)
						tag.Track = value;
			}
		}

		[System.CLSCompliant(false)]
		public override uint TrackCount
		{
			get
			{
				uint output = 0;
				foreach (Tag tag in tags)
					if (tag != null && output == 0)
						output = tag.TrackCount;
				return output;
			}
			set
			{
				foreach (Tag tag in tags)
					if (tag != null)
						tag.TrackCount = value;
			}
		}

		[System.CLSCompliant(false)]
		public override uint Disc
		{
			get
			{
				uint output = 0;
				foreach (Tag tag in tags)
					if (tag != null && output == 0)
						output = tag.Disc;
				return output;
			}
			set
			{
				foreach (Tag tag in tags)
					if (tag != null)
						tag.Disc = value;
			}
		}

		[System.CLSCompliant(false)]
		public override uint DiscCount
		{
			get
			{
				uint output = 0;
				foreach (Tag tag in tags)
					if (tag != null && output == 0)
						output = tag.DiscCount;
				return output;
			}
			set
			{
				foreach (Tag tag in tags)
					if (tag != null)
						tag.DiscCount = value;
			}
		}

		public override IList<IImage> Pictures
		{
			get
			{
				foreach (Tag tag in tags)
				{
					if (tag != null && tag.Pictures.Count > 0)
					{
						return tag.Pictures;
					}
				}

				return base.Pictures;
			}

			set
			{
				foreach (Tag tag in tags)
				{
					if (tag != null)
					{
						tag.Pictures = value;
					}
				}
			}
		}
		#endregion

		#region Public Methods
		public void SetTags(params Tag[] tags)
		{
			this.tags.Clear();
			if (tags != null)
			{
				foreach (Tag tag in tags)
					this.tags.Add(tag);
			}
		}
		
		public void SetTags(IList<Tag> tags)
		{
			this.tags.Clear();
			if (tags != null)			
				foreach (Tag tag in tags)
					this.tags.Add(tag);
				//this.tags = tags;
		}
		#endregion
	}
}
