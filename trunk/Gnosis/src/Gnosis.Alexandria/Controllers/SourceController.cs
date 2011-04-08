﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using log4net;

using Gnosis.Alexandria.Models;
using Gnosis.Alexandria.Repositories;
using System.Net;
using Gnosis.Alexandria.Helpers;

namespace Gnosis.Alexandria.Controllers
{
    public class SourceController : ISourceController
    {
        public SourceController(IRepository<ISource> repository, ITrackController trackController)
        {
            this.repository = repository;
            this.trackController = trackController;
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(SourceController));
        private readonly IRepository<ISource> repository;
        private readonly ITrackController trackController;

        public ISource Get(Guid id)
        {
            return repository.Get(id);
        }

        public void Save(ISource record)
        {
            repository.Save(record);
        }

        public void Save(IEnumerable<ISource> records)
        {
            repository.Save(records);
        }

        public void Delete(Guid id)
        {
            repository.Delete(id);
        }

        public void Delete(IEnumerable<Guid> ids)
        {
            repository.Delete(ids);
        }

        public IEnumerable<ISource> All()
        {
            return repository.All();
        }

        public IEnumerable<ISource> Search(IEnumerable<KeyValuePair<string, object>> criteria)
        {
            return repository.Search(criteria);
        }

        public ISource GetPlaylistItem(ISource parent, ITrack track)
        {
            return new PlaylistItemSource()
            {
                Parent = parent,
                Path = track.Path,
                ImagePath = track.ImagePath,
                ImageData = track.ImageData,
                Name = string.Format("{0} by {1}", track.Title ?? "Untitled", track.Artist ?? "Unknown Artist"),
                Number = parent.Children.Count() + 1
            };
        }

        public void LoadDirectories(ISource source)
        {
            if (source.Children.Count() == 1)
            {
                var proxy = source.Children.FirstOrDefault() as ProxySource;
                if (proxy != null)
                    source.RemoveChild(proxy);

                if (Directory.Exists(source.Path))
                {
                    var directory = new DirectoryInfo(source.Path);
                    foreach (var subDirectory in directory.GetDirectories())
                    {
                        var child = new DirectorySource() { Name = subDirectory.Name, Path = subDirectory.FullName, Parent = source };
                        source.AddChild(child);
                        //LoadDirectories(child);
                    }
                    foreach (var file in directory.GetFiles("*.mp3"))
                    {
                        try
                        {
                            var track = trackController.Search(new Dictionary<string, object> { { "Path", file.FullName } }).FirstOrDefault();
                            if (track == null)
                            {
                                track = trackController.ReadFromTag(file.FullName);
                                trackController.Save(track);
                            }

                            var item = GetPlaylistItem(source, track);
                            source.AddChild(item);
                        }
                        catch (Exception ex)
                        {
                            log.Error("SourceController.LoadDirectories: Could not load file path=" + file.FullName, ex);
                        }
                    }
                }
            }
        }

        private string GetPodcastXml(string path)
        {
            var request = HttpWebRequest.Create(path);

            using (var stream = request.GetResponse().GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public void LoadPodcast(ISource source)
        {
            try
            {
                //if (source.Children.Count() >= 1 && !(source.Children.FirstOrDefault() is ProxySource))
                //{
                //    log.Info("SourceController.LoadPodcast: Podcast already loaded");
                //    return;
                //}
                //else
                //{
                    if (source.Children.Count() == 1 && source.Children.FirstOrDefault() is ProxySource)
                    {
                        var children = Search(new Dictionary<string, object> { { "Parent", source.Id } });
                        if (children != null && children.Count() > 0)
                        {
                            foreach (var child in children)
                                source.AddChild(child);

                            log.Info("SourceController.LoadPodcast: Loaded podcast from repository");
                            return;
                        }
                    }
                //}

                var xml = new XmlDocument();
                xml.LoadXml(GetPodcastXml(source.Path));
                var nsmgr = new XmlNamespaceManager(xml.NameTable);
                nsmgr.AddNamespace("itunes", "http://www.itunes.com/dtds/podcast-1.0.dtd");
                nsmgr.AddNamespace("atom", "http://www.w3.org/2005/Atom");

                var imageNode = xml.SelectSingleNode("/rss/channel/image/url", nsmgr);
                if (imageNode != null)
                {
                    if (source.ImagePath != imageNode.InnerText)
                    {
                        source.ImagePath = imageNode.InnerText;
                        Save(source);
                    }
                }

                var itemNodes = xml.SelectNodes("/rss/channel/item", nsmgr);
                if (itemNodes != null && itemNodes.Count > 0)
                {
                    foreach (XmlNode itemNode in itemNodes)
                    {
                        var titleNode = itemNode.SelectSingleNode("title", nsmgr);
                        var linkNode = itemNode.SelectSingleNode("enclosure", nsmgr); //"link", nsmgr);
                        var authorNode = itemNode.SelectSingleNode("itunes:author", nsmgr);
                        var summaryNode = itemNode.SelectSingleNode("itunes:summary", nsmgr);
                        var dateNode = itemNode.SelectSingleNode("pubDate", nsmgr);

                        if (linkNode != null)
                        {
                            var path = linkNode.Attributes["url"].Value; //linkNode.InnerText;
                            var date = DateTime.MinValue;
                            Rfc822DateTime.TryParse(dateNode.InnerText, out date);

                            var child = source.Children.Where(x => x.Path == path).FirstOrDefault();

                            if (child != null)
                            {
                                if (child.Date != date && date != DateTime.MinValue)
                                {
                                    child.Date = date;
                                    Save(child);
                                    var track = trackController.Search(new Dictionary<string, object> { { "Path", child.Path } }).FirstOrDefault();
                                    if (track != null)
                                    {
                                        track.ReleaseDate = date;
                                        trackController.Save(track);
                                    }
                                }
                            }
                            else
                            {
                                var playlistItem = new PlaylistItemSource { Path = path, ImagePath = source.ImagePath, Date = date, Parent = source };
                                playlistItem.Name = titleNode != null ? titleNode.InnerText : "Unknown Podcast";
                                playlistItem.Creator = authorNode != null ? authorNode.InnerText : "Unknown Creator";
                                playlistItem.Summary = summaryNode != null ? summaryNode.InnerText : string.Empty;

                                Save(playlistItem);
                                source.AddChild(playlistItem);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("SourceController.LoadPodcast", ex);
            }
        }
    }
}