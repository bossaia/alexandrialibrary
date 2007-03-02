using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Encyclopedia
{
	public class GroupFormedEvent : ArtistEvent
	{
		#region Constructors
		public GroupFormedEvent() : base("Group Formed")
		{
		}
		
		public GroupFormedEvent(string id) : this()
		{
		}
		#endregion
	}
}