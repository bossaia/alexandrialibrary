using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.Data
{
	public class DataFactory
	{
		#region Constructors
		public DataFactory()
		{
		}
		#endregion
		
		#region Public Methods
	
		#region OpenDatabase
		#endregion
		
		#region CloseDatabase
		#endregion
	
		#region GetAlbum
		public virtual Album GetAlbum()
		{
			return null;
		}

		public virtual Album GetAlbum(Guid id)
		{
			return null;
		}
		#endregion
	
		#region GetArtist
		public virtual Artist GetArtist()
		{
			return null;
		}
		
		public virtual Artist GetArtist(Guid id)
		{
			return null;
		}
		#endregion

		#region GetCollaboration
		public virtual Collaboration GetCollaboration()
		{
			return null;
		}

		public virtual Collaboration GetCollaboration(Guid id)
		{
			return null;
		}
		#endregion
		
		#region GetSong
		public virtual Song GetSong()
		{
			return null;
		}
		
		public virtual Song GetSong(Guid id)
		{
			return null;
		}
		#endregion
		
		#region GetTrack
		public virtual Track GetTrack()
		{
			return null;
		}
		
		public virtual Track GetTrack(Guid id)
		{
			return null;
		}
		#endregion
		
		#region GetUser
		public virtual User GetUser()
		{
			return null;
		}
		
		public virtual User GetUser(Guid id)
		{
			return null;
		}

		public virtual User GetUser(string name)
		{
			return null;
		}
		#endregion
		
		#region AddUser
		public virtual bool AddUser(User user)
		{
			return false;
		}
		#endregion		
		
		#endregion
	}
}
