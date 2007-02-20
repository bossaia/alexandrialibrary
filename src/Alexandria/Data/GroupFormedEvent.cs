using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.Data
{
	public class GroupFormedEvent : ArtistEvent
	{
		#region Constructors
		public GroupFormedEvent() : base("Group Formed")
		{
		}
		
		public GroupFormedEvent(string id) : base(id, "Group Formed")
		{
		}
		#endregion
	}
}