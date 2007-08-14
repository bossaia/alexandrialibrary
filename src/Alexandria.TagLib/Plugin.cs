using System;
using System.Collections.Generic;
using Alexandria.Plugins;

namespace Alexandria.TagLib
{
	public class Plugin : BasePlugin
	{
		#region Constructors
		public Plugin() : base(new Guid("89282138-E7BA-4ada-86DC-0967C54F0481"))
		{
		}
		#endregion

		#region Public Methods
		public override void Load()
		{
			base.Load();
		}

		public override void Save()
		{
			base.Save();
		}
		#endregion
	}
}