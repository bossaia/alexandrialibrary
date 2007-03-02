using System;
using System.Collections.Generic;
using System.Text;
using AlexandriaOrg.Alexandria;

namespace Alexandria
{
	public class User
	{	
		#region Private Fields
		private string name;
		private string password;
		#endregion
		
		#region Constructors
		public User() : base()
		{
		}
		
		public User(string id) : base(id)
		{
		}
		#endregion
		
		#region Public Properties
		public string Name
		{
			get {return name;}
			set {name = value;}
		}
		
		public string Password
		{
			get {return password;}
			set {password = value;}
		}

		public IList<Profile> Profiles
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
			}
		}

		public Profile CurrentProfile
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
			}
		}
		#endregion
		
		#region Public Methods
		public static AuthenticationResult Authenticate(string name, string password)
		{
			AuthenticationResult result;
		
			User user = PluginManager.DataFactory.GetUser(name);
			if (user != null)
			{
				if (password == user.Password)
				{
					result = new AuthenticationResult(true, true, user);
				}
				else result = new AuthenticationResult(true, false, null);
			}
			else result = new AuthenticationResult(false, false, null);
			
			return result;					
		}
		#endregion
	}
}
