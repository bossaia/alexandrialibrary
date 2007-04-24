using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Mp3Tunes
{
	public class AuthenticationException : Exception
	{
		public AuthenticationException(string message) : base(message)
		{
		}
	}
}
