using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using com.db4o;
using Alexandria;

namespace Alexandria.Db4o
{
	//[Alexandria.DataFactoryClass]
	public class Db4oDataFactory
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
		public Alexandria.Data.IAlbum GetAlbum()
		{
			return null;
		}

		public Alexandria.Data.IAlbum GetAlbum(Guid id)
		{
			return null;
		}
		#endregion
		
		#region GetArtist
		public Alexandria.Data.IArtist GetArtist()
		{
			return null;
		}

		public Alexandria.Data.IArtist GetArtist(Guid id)
		{
			return null;
		}
		#endregion
		
		#region GetCollaboration
		/*
		public override Collaboration GetCollaboration()
		{
			return base.GetCollaboration();
		}

		public override Collaboration GetCollaboration(Guid id)
		{
			return base.GetCollaboration(id);
		}
		*/
		#endregion
		
		#region GetSong
		public ISong GetSong()
		{
			return null;
		}

		public ISong GetSong(Guid id)
		{
			return null;
		}
		#endregion
		
		#region GetTrack
		/*
		public override Alexandria.I GetTrack()
		{
			return base.GetTrack();
		}

		public override Track GetTrack(Guid id)
		{
			return base.GetTrack(id);
		}
		*/
		#endregion
		
		#region GetUser
		public User GetUser()
		{
			return null;
		}

		public User GetUser(Guid id)
		{
			return null;
		}

		public User GetUser(string name)
		{
			User user = null;
		
			try
			{
				user = new User();
				user.Name = name;
				user.Password = null;
				
				ObjectSet set = UserContainer.Get(user);
				if (set.HasNext())
				{
					user = (User)set.Next();
				}
				else user = null;
			}
			catch (Exception ex)
			{
				//("Could not get user\n" + ex.Message);
				throw new AlexandriaException(ex);
			}
			finally
			{
				userContainer.Close();
			}
			return user;
		}
		#endregion
		
		#region AddUser
		public bool AddUser(IUser user)
		{
			try
			{
				UserContainer.Set(user);
			}
			catch (Exception ex)
			{
				// Could not add user
				throw new AlexandriaException(ex);
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
