using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.LastFM
{
	public class OldLoginResponse
	{
		#region Constructors
		public OldLoginResponse(string sessionId, string streamUrl, string username, string passwordMd5)
		{
			this._sessionId = sessionId;
			this._streamUrl = streamUrl;
			this._username = username;
			this._passwordMd5 = passwordMd5;
		}
		#endregion
	
		#region Private Fields
		private string _sessionId = string.Empty;
		private string _streamUrl = string.Empty;
		private string _username = string.Empty;
		private string _passwordMd5 = string.Empty;
		private bool _paidAccount = false;
		#endregion

		#region Public Properties
		public string SessionId
		{
			get { return this._sessionId; }
		}

		public string StreamUrl
		{
			get { return this._streamUrl; }
		}

		public string Username
		{
			get { return this._username; }
		}

		public string PasswordMd5
		{
			get { return this._passwordMd5; }
		}

		public bool PaidAccount
		{
			get { return this._paidAccount; }
			set { this._paidAccount = value; }
		}
		#endregion
	}
}
