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
		private User user;
		#endregion

		#region Constructors
		public AuthenticationResult(bool userNameIsValid, bool passwordIsValid, User user)
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

		public User User
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

	#region MimeType
	/*
	public struct MimeType
	{
		#region Constructors
		public MimeType(ContentType type, string subtype)
		{
			this.uuid = Guid.NewGuid();
			this.type = type;
			this.subtype = subtype.ToLowerInvariant();
		}
		#endregion
		
		#region Private Fields
		private Guid uuid;
		private ContentType type;
		private string subtype;
		#endregion
		
		#region Public Properties
		public ContentType Type
		{
			get { return type; }
		}

		public string Subtype
		{
			get {return subtype;}
		}
		#endregion
		
		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj is MimeType)
			{
				MimeType otherMimeType = (MimeType)obj;
				return (this.Type == otherMimeType.Type && string.Compare(this.Subtype, otherMimeType.Subtype, true) == 0);
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return this.uuid.GetHashCode();
		}

		public override string ToString()
		{
			return this.type.ToString() + "/" + this.subtype.ToString();
		}
		#endregion
		
		#region Public Static Methods
		public static bool operator ==(MimeType m1, MimeType m2)
		{
			return m1.Equals(m2);
		}
		
		public static bool operator !=(MimeType m1, MimeType m2)
		{
			return !m1.Equals(m2);
		}
		#endregion
	}
	*/
	#endregion
}
