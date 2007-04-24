using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class BaseArtist : BaseMetadata, IArtist
	{
		#region Constructors
		public BaseArtist(IIdentifier id, ILocation location, string name, bool isGroup, DateTime dateStarted, DateTime dateStopped) : base(id, location, name)
		{
			this.isGroup = isGroup;
			this.dateStarted = dateStarted;
			this.dateStopped = dateStopped;
		}
		#endregion
	
		#region Private Fields
		private bool isGroup;
		private DateTime dateStarted;
		private DateTime dateStopped;
		#endregion
	
		#region IArtist Members
		public bool IsGroup
		{
			get { return isGroup; }
		}

		public DateTime DateStarted
		{
			get { return dateStarted; }
		}

		public DateTime DateStopped
		{
			get { return dateStopped; }
		}
		#endregion
	}
}
