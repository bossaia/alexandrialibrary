using System;
using System.Collections.Generic;

using Alexandria.Console.Commands;
using Alexandria.Playlist;
using Alexandria.Playlist.Xspf;

namespace Alexandria.Console.Contexts
{
	public class PlaylistContext : Context
	{
		public PlaylistContext() : base(ContextConstants.Playlist)
		{
			Prompt = "playlist> ";
		}

		private XspfPlaylist xspfPlaylist;

		private void HandleAdd(string option)
		{
			System.Console.WriteLine("Playlist Add logic goes here");
		}

		private void HandleList(string option)
		{
			if (xspfPlaylist != null)
			{
				if (xspfPlaylist.Tracks.Count > 0)
				{
					System.Console.WriteLine(string.Format("Playlist contents: {0} tracks", xspfPlaylist.Tracks.Count));
					foreach(Track track in xspfPlaylist.Tracks)
						System.Console.WriteLine(track.ToString());
				}
			}
		}

		private void HandleLoad(string option)
		{
			Uri path;
			if (Uri.TryCreate(option, UriKind.RelativeOrAbsolute, out path))
			{
				xspfPlaylist = new XspfPlaylist(path);
				xspfPlaylist.Load();
				System.Console.WriteLine("Playlist loaded");
			}
			else System.Console.WriteLine("Could not load playlist: invalid path");
		}
		
		private void HandleSave(string option)
		{
			Uri path;
			if (Uri.TryCreate(option, UriKind.RelativeOrAbsolute, out path))
			{
				if (xspfPlaylist != null)
				{
					xspfPlaylist.Save(path);
					System.Console.WriteLine("Playlist saved to: {0}", option);
				}
				else System.Console.WriteLine("Could not save playlist: no playlist is currently loaded");
			}
			else System.Console.WriteLine("Could not save playlist: invalid path");
		}
		
		private void HandleStatus(string option)
		{
			if (xspfPlaylist != null)
				System.Console.WriteLine(string.Format("Playlist loaded: {0}", xspfPlaylist.Title));
			else System.Console.WriteLine("No playlist loaded");
		}

		public override void HandleCommand(Command command, string option)
		{
			if (IsActive)
			{
				switch(command.Name)
				{
					case CommandConstants.Add:
						HandleAdd(option);
						break;
					case CommandConstants.List:
						HandleList(option);
						break;
					case CommandConstants.Load:
						HandleLoad(option);
						break;
					case CommandConstants.Save:
						HandleSave(option);
						break;
					case CommandConstants.Status:
						HandleStatus(option);
						break;
					default:
						break;
				}
			}
		}
	}
}
