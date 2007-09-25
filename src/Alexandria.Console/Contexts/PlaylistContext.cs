using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

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
			if (xspfPlaylist != null)
			{
				try
				{
					Uri path;
					if (Uri.TryCreate(option, UriKind.RelativeOrAbsolute, out path))
					{
						Track track = new Track();
						track.Locations.Add(new Location(path));
						xspfPlaylist.Tracks.Add(track);
						Result = string.Format("Added track {0} to playlist", path.LocalPath);
					}
					else Result = "There was an error adding to playlist: invalid track location";
				}
				catch (Exception ex)
				{
					Result = "There was an error adding to playlist: " + ex.Message;
				}
			}
			else Result = "Could not add to playlist: no playlist loaded";
		}

		private void HandleList(string option)
		{
			if (xspfPlaylist != null)
			{
				if (xspfPlaylist.Tracks.Count > 0)
				{
					StringBuilder builder = new StringBuilder();
					builder.AppendFormat("Playlist contents: {0} tracks\n", xspfPlaylist.Tracks.Count);
					foreach(Track track in xspfPlaylist.Tracks)
						builder.AppendFormat("{0}\n", track);
					
					Result = builder.ToString();
				}
				else Result = "Playlist is empty";
			}
			else Result = "Could not list playlist contents: no playlist loaded";
		}

		private void HandleLoad(string option)
		{
			try
			{
				Uri path;
				if (Uri.TryCreate(option, UriKind.RelativeOrAbsolute, out path))
				{
					if (path.IsFile)
					{
						if (File.Exists(path.LocalPath))
						{
							xspfPlaylist = new XspfPlaylist(path);
							xspfPlaylist.Load();
							Result = "Playlist loaded";
						}
						else Result = "Could not load playlist: file does not exist";
					}
				}
			}
			catch(Exception ex)
			{
				Result = "There was an error loading this playlist: " + ex.Message;
			}
		}
		
		private void HandleSave(string option)
		{
			try
			{
				Uri path;
				if (Uri.TryCreate(option, UriKind.RelativeOrAbsolute, out path))
				{
					if (xspfPlaylist != null)
					{
						xspfPlaylist.Save(path);
						Result = "Playlist saved to: " + path.LocalPath;
					}
					else Result = "Could not save playlist: no playlist is currently loaded";
				}
				else Result = "Could not save playlist: invalid path";
			}
			catch(Exception ex)
			{
				Result = "There was an error saving the playlist: " + ex.Message;
			}
		}
		
		private void HandleStatus(string option)
		{
			if (xspfPlaylist != null)
				Result = string.Format("Playlist loaded: {0}", xspfPlaylist.Title);
			else Result = "No playlist loaded";
		}

		public override void HandleCommand(Command command, string option)
		{
			if (IsActive)
			{
				switch(command.Name)
				{
					case CommandConstants.Add:
						HandleAdd(option);
						WriteResult();
						break;
					case CommandConstants.List:
						HandleList(option);
						WriteResult();
						break;
					case CommandConstants.Load:
						HandleLoad(option);
						WriteResult();
						break;
					case CommandConstants.Save:
						HandleSave(option);
						WriteResult();
						break;
					case CommandConstants.Status:
						HandleStatus(option);
						WriteResult();
						break;
					default:
						break;
				}
			}
		}
	}
}
