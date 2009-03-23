using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Schema
{
	public static class MessageSchema
	{
		private const string QUERY_DELIMITER = "?";
		private const string PATH_ALXLIB = "http://alxlib.com/";
		private const string PATH_SCHEMA = "schema/";
		private const string PATH_VERSION = "1/";
		private const string SCHEMA_ROOT = PATH_ALXLIB + PATH_SCHEMA + PATH_VERSION;
		private const string SCHEMA_ACTIONS =  SCHEMA_ROOT + "actions/";
		private const string SCHEMA_REQUESTS = SCHEMA_ROOT + "requests/";
		private const string SCHEMA_CONTENT = SCHEMA_ROOT + "content/";

		public const string ACTIONS_DISPLAY = SCHEMA_ACTIONS + "display";
		public const string ACTIONS_VALIDATE = SCHEMA_ACTIONS + "validate";
		public const string ACTIONS_REFRESH = SCHEMA_ACTIONS + "refresh";
		public const string ACTIONS_SAVE = SCHEMA_ACTIONS + "save";
		public const string ACTIONS_CANCEL = SCHEMA_ACTIONS + "cancel";
		public const string ACTIONS_LOOKUP = SCHEMA_ACTIONS + "lookup";
		public const string ACTIONS_CREATE = SCHEMA_ACTIONS + "create";
		public const string ACTIONS_UPDATE = SCHEMA_ACTIONS + "update";
		public const string ACTIONS_DELETE = SCHEMA_ACTIONS + "delete";

		public const string REQUESTS_GET = SCHEMA_REQUESTS + "get";
		public const string REQUESTS_PUT = SCHEMA_REQUESTS + "put";
		public const string REQUESTS_POST = SCHEMA_REQUESTS + "post";
		public const string REQUESTS_DELETE = SCHEMA_REQUESTS + "delete";

		public const string CONTENT_ERROR = SCHEMA_CONTENT + "error";
	}
}
