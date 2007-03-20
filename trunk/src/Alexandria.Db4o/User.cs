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
		private IList<IProfile> profiles;
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

		public AuthenticationResult Authenticate(string name, string password)
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
			
			return new AuthenticationResult(userNameIsValid, passwordIsValid, this);
		}
		#endregion
	}
}
