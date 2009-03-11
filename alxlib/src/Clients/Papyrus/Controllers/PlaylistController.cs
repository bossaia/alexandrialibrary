using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;

using Papyrus.Data;
using Papyrus.Forms;
using Papyrus.Properties;
using Papyrus.Views;

using Alexandria.Core;
using Alexandria.Core.Model;

namespace Papyrus.Controllers
{
	public class PlaylistController : ControllerBase
	{
		public PlaylistController() : this(new PlaylistForm())
		{
		}

		public PlaylistController(IPlaylistView view)
		{
			this.view = view;
			view.Validating += new ViewActionCallback(ValidatingView);
			view.Loading += new ViewActionCallback(LoadingView);
			view.Accepted += new ViewActionCallback(AcceptedView);
		}

		private IPlaylistView view;

		public void DisplayView()
		{
			view.Display();
		}

		private PlaylistFormatter GetFormatterForFile(Uri path)
		{
			string fileName = UriUtility.GetFileNameFromUri(path);
			return GetFormatterForFile(fileName);
		}

		private PlaylistFormatter GetFormatterForFile(string fileName)
		{
			PlaylistFormatter formatter = null;

			if (!string.IsNullOrEmpty(fileName))
			{
				if (fileName.EndsWith(Resources.FileExtensionM3u))
					formatter = new M3uPlaylistFormatter();
				else if (fileName.EndsWith(Resources.FileExtensionPls))
					formatter = new PlsPlaylistFormatter();
				else
					formatter = new XspfPlaylistFormatter();
			}

			return formatter;
		}

		private void ValidatingView(ViewAction action)
		{
			if (action != null && action.IsRunning)
			{
				view.RefreshData();

				if (string.IsNullOrEmpty(view.Title.Value))
				{
					view.Title.Status = DataStatus.Missing;
					action.IsValid = false;
				}

				if (view.Tracks.Items.Count == 0)
				{
					view.Tracks.Status = DataStatus.Missing;
					action.IsValid = false;
				}
			}
		}

		private void LoadingView(ViewAction action)
		{
			if (action != null && action.IsRunning)
			{
				Playlist playlist = null;

				string fileName = UriUtility.GetFileNameFromUri(action.Path);

				PlaylistFormatter formatter = GetFormatterForFile(fileName);
				if (formatter != null)
				{
					playlist = formatter.LoadPlaylistFromFile(fileName);
					view.Title.Value = playlist.Title;
					view.Creator.Value = playlist.Creator;
					view.RefreshView();
				}
			}
		}

		private void AcceptedView(ViewAction action)
		{
			if (action != null && action.IsRunning)
			{
				string fileName = UriUtility.GetFileNameFromUri(action.Path);
				if (!string.IsNullOrEmpty(fileName))
				{
					Playlist playlist = new Playlist();
					playlist.Annotation = view.Comment.Value;
					playlist.Creator = view.Creator.Value;
					playlist.Date = view.Created.Value;
					playlist.Identifier = view.Identifier.Value;
					playlist.Image = view.Image.Value;
					playlist.Info = view.Info.Value;
					playlist.License = view.License.Value;
					playlist.Location = view.Location.Value;
					playlist.Title = view.Title.Value;

					foreach (DataItem<AttributionData> item in view.Attribution.Items)
					{
						playlist.Attribution.Add(new Attribution(item.Value.IsLocation, item.Value.Value));
					}

					foreach (DataItem<LinkData> item in view.Links.Items)
					{
						playlist.Link.Add(new Link(item.Value.Rel, item.Value.Value));
					}

					foreach (DataItem<MetaData> item in view.Metadata.Items)
					{
						playlist.Meta.Add(new Meta(item.Value.Rel, item.Value.Value));
					}

					foreach (DataItem<ExtensionData> item in view.Extensions.Items)
					{
						playlist.Extension.Add(new Extension(item.Value.Application, item.Value.Value));
					}

					foreach (DataItem<TrackData> item in view.Tracks.Items)
					{
						Track track = new Track()
						{
							//TODO: add logic for track-level links, meta and extensions
							Album = item.Value.Album,
							Annotation = item.Value.Annotation,
							Creator = item.Value.Creator,
							Duration = item.Value.Duration,
							Identifier = item.Value.Identifier,
							Image = item.Value.Image,
							Info = item.Value.Info,
							Location = item.Value.Location,
							Title = item.Value.Title,
							TrackNum = item.Value.TrackNum
						};

						playlist.TrackList.Add(track);
					}

					PlaylistFormatter formatter = GetFormatterForFile(fileName);

					formatter.SavePlaylistToFile(playlist, fileName);
				}
			}
		}
	}
}
