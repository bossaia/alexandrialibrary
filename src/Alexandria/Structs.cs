using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Alexandria
{
	#region AuthenticationResult
	public struct AuthenticationResult
	{
		#region Private Fields
		private bool userNameIsValid;
		private bool passwordIsValid;
		private IUser user;
		#endregion

		#region Constructors
		public AuthenticationResult(bool userNameIsValid, bool passwordIsValid, IUser user)
		{
			this.userNameIsValid = userNameIsValid;
			this.passwordIsValid = passwordIsValid;
			this.user = user;
		}
		#endregion

		#region Public Properties
		public bool UserNameIsValid
		{
			get { return userNameIsValid; }
		}

		public bool PasswordIsValid
		{
			get { return passwordIsValid; }
		}

		public IUser User
		{
			get { return user; }
		}

		public bool IsValid
		{
			get
			{
				if (userNameIsValid && passwordIsValid && user != null) return true;
				else return false;
			}
		}
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj is AuthenticationResult)
			{
				AuthenticationResult otherResult = (AuthenticationResult)obj;
				return (this.IsValid == otherResult.IsValid);
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return Convert.ToInt32(IsValid);
		}
		#endregion
		
		#region Public Static Methods
		public static bool operator ==(AuthenticationResult r1, AuthenticationResult r2)
		{
			return r1.Equals(r2);
		}
		
		public static bool operator !=(AuthenticationResult r1, AuthenticationResult r2)
		{
			return !r1.Equals(r2);
		}
		#endregion
	}
	#endregion

	#region Version
	public struct Version : IComparable<Version>
	{
		#region Constructors
		public Version(int majorPart, int minorPart, int buildPart, int revisionPart)
		{
			numbers = new int[4];
			numbers[0] = majorPart;
			numbers[1] = minorPart;
			numbers[2] = buildPart;
			numbers[3] = revisionPart;	
		}
		#endregion
		
		#region Private Fields
		private int[] numbers;// = new int[4];
		#endregion
				
		#region Public Properties
		public int MajorPart
		{
			get { return numbers[0]; }
		}
		
		public int MinorPart
		{
			get { return numbers[1]; }
		}
		
		public int BuildPart
		{
			get { return numbers[2]; }
		}
		
		public int RevisionPart
		{
			get { return numbers[3]; }
		}
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj != null && obj.GetType() == typeof(Version))
			{
				return (this.CompareTo((Version)obj) == 0);
			}
			else return false;
		}
		#endregion

		#region IComparable<Version> Members
		public int CompareTo(Version other)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion
	}
	#endregion
}
