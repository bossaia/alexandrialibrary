using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AlexandriaOrg.Alexandria;
using AlexandriaOrg.Alexandria.Data;
using com.db4o;

namespace AlexandriaOrg.Alexandria.Db4o
{
	[PluginAttributes.DataFactoryClass]
	public class Db4oDataFactory : DataFactory
	{
		#region Private Constants
		private const string USER_DATABASE_PATH = @"Data\";
		private const string USER_DATABASE_NAME = "Users.alx";
		#endregion
		
		#region Private Fields
		private ObjectContainer userContainer;
		#endregion
	
		#region Constructors
		public Db4oDataFactory()
		{
		}
		#endregion
	
		#region Protected Properties
		[CLSCompliant(false)]
		protected ObjectContainer UserContainer
		{
			get
			{
				if (userContainer == null)
				{
					if (!System.IO.Directory.Exists(USER_DATABASE_PATH))
					{
						System.IO.Directory.CreateDirectory(USER_DATABASE_PATH);
					}
				
					userContainer = com.db4o.Db4o.OpenFile(USER_DATABASE_PATH + USER_DATABASE_NAME);
				}
				else userContainer = com.db4o.Db4o.OpenFile(USER_DATABASE_PATH + USER_DATABASE_NAME);
				
				return userContainer;
			}
		}
		#endregion
	
		#region Public Methods
		
		#region GetAlbum
		public override Album GetAlbum()
		{
			return base.GetAlbum();
		}

		public override Album GetAlbum(Guid id)
		{
			return base.GetAlbum(id);
		}
		#endregion
		
		#region GetArtist
		public override Artist GetArtist()
		{
			return base.GetArtist();
		}

		public override Artist GetArtist(Guid id)
		{
			return base.GetArtist(id);
		}
		#endregion
		
		#region GetCollaboration
		public override Collaboration GetCollaboration()
		{
			return base.GetCollaboration();
		}

		public override Collaboration GetCollaboration(Guid id)
		{
			return base.GetCollaboration(id);
		}
		#endregion
		
		#region GetSong
		public override Song GetSong()
		{
			return base.GetSong();
		}

		public override Song GetSong(Guid id)
		{
			return base.GetSong(id);
		}
		#endregion
		
		#region GetTrack
		public override Track GetTrack()
		{
			return base.GetTrack();
		}

		public override Track GetTrack(Guid id)
		{
			return base.GetTrack(id);
		}
		#endregion
		
		#region GetUser
		public override Alexandria.Data.User GetUser()
		{
			return base.GetUser();
		}

		public override Alexandria.Data.User GetUser(Guid id)
		{
			return base.GetUser(id);
		}

		public override Alexandria.Data.User GetUser(string name)
		{
			Alexandria.Data.User user = null;
		
			try
			{
				user = new Alexandria.Data.User();
				user.Name = name;
				user.Password = null;
				
				ObjectSet set = UserContainer.Get(user);
				if (set.HasNext())
				{
					user = (Alexandria.Data.User)set.Next();
				}
				else user = null;
			}
			catch (Exception ex)
			{
				//("Could not get user\n" + ex.Message);
				throw new AlexandriaException(Subsystem.Data, ex);
			}
			finally
			{
				userContainer.Close();
			}
			return user;
		}
		#endregion
		
		#region AddUser
		public override bool AddUser(Alexandria.Data.User user)
		{
			try
			{
				UserContainer.Set(user);
			}
			catch (Exception ex)
			{
				// Could not add user
				throw new AlexandriaException(Subsystem.Data, ex);
			}
			finally
			{
				userContainer.Close();
			}
			
			return true;
		}
		#endregion
		
		#endregion
	}
}
