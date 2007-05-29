using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Db4o
{
	public class User : IUser
	{
		#region Constructors
		public User()
		{
		}
		#endregion
		
		#region Private Fields
		private string name;
		private string password;
		private IList<IProfile> profiles = new List<IProfile>();
		private IProfile profile;
		#endregion
		
		#region IUser Members
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string Password
		{
			get { return password; }
			set { password = value; }
		}

		public IList<IProfile> Profiles
		{
			get { return profiles; }
		}

		public IProfile Profile
		{
			get { return profile; }
			set { profile = value; }
		}

		public void AddProfile(IProfile profile)
		{
			profiles.Add(profile);
		}

		public bool Authenticate(string name, string password)
		{
			bool userNameIsValid = false;
			bool passwordIsValid = false;
			
			if (name != null)
			{
				if (password != null)
				{
					if (this.name == name)
					{
						if (this.password == password)
						{
						}
						else passwordIsValid = false;
					}
					else userNameIsValid = false;
				}
				else passwordIsValid = false;
			}
			else userNameIsValid = false;
			
			return (userNameIsValid && passwordIsValid);
		}
		#endregion
	}
}
