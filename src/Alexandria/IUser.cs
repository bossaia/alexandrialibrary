using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IUser
	{
		string Name { get; }
		string Password { get; }
		IList<IProfile> Profiles { get; }
		IProfile Profile { get; }
		bool Authenticate(string name, string password);
	}
}
