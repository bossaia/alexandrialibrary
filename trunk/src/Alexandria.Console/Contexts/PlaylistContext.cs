using System;
using System.Collections.Generic;

using Alexandria.Console.Commands;

namespace Alexandria.Console.Contexts
{
	public class PlaylistContext : Context
	{
		public PlaylistContext() : base(ContextConstants.Playlist)
		{
			Prompt = "playlist> ";
		}

		private void HandleAdd(string option)
		{
			System.Console.WriteLine("Playlist Add logic goes here");
		}

		public override void HandleCommand(Command command, string option)
		{
			switch(command.Name)
			{
				case CommandConstants.Add:
					HandleAdd(option);
					break;
				default:
					break;
			}
		}
	}
}
