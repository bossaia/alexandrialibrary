﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Gnosis.Application.Vendor;
using Gnosis.Links;
using Gnosis.Tasks;

namespace Gnosis.Spiders
{
    public class CatalogSpider
        : ISpider
    {
        public CatalogSpider(ILogger logger, ISecurityContext securityContext, IMediaFactory mediaFactory, ILinkRepository linkRepository, ITagRepository tagRepository, IMediaRepository mediaRepository, IMediaItemRepository<IArtist> artistRepository, IMediaItemRepository<IAlbum> albumRepository, IMediaItemRepository<ITrack> trackRepository)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (securityContext == null)
                throw new ArgumentNullException("securityContext");
            if (mediaFactory == null)
                throw new ArgumentNullException("mediaFactory");
            if (linkRepository == null)
                throw new ArgumentNullException("linkRepository");
            if (tagRepository == null)
                throw new ArgumentNullException("tagRepository");
            if (mediaRepository == null)
                throw new ArgumentNullException("mediaRepository");
            if (artistRepository == null)
                throw new ArgumentNullException("artistRepository");
            if (albumRepository == null)
                throw new ArgumentNullException("albumRepository");
            if (trackRepository == null)
                throw new ArgumentNullException("trackRepository");

            this.logger = logger;
            this.securityContext = securityContext;
            this.mediaFactory = mediaFactory;
            this.linkRepository = linkRepository;
            this.tagRepository = tagRepository;
            this.mediaRepository = mediaRepository;
            this.artistRepository = artistRepository;
            this.albumRepository = albumRepository;
            this.trackRepository = trackRepository;

            Delay = TimeSpan.Zero;
            MaxErrors = 0;
        }

        private readonly ILogger logger;
        private readonly ISecurityContext securityContext;
        private readonly IMediaFactory mediaFactory;
        private readonly ILinkRepository linkRepository;
        private readonly ITagRepository tagRepository;
        private readonly IMediaRepository mediaRepository;
        private readonly IMediaItemRepository<IArtist> artistRepository;
        private readonly IMediaItemRepository<IAlbum> albumRepository;
        private readonly IMediaItemRepository<ITrack> trackRepository;

        public TimeSpan Delay
        {
            get;
            set;
        }

        public int MaxErrors
        {
            get;
            set;
        }

        public IMedia GetMedia(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            return mediaFactory.Create(location);
        }

        private string GetArtistThumbnailPath(FileInfo fileInfo)
        {
            //TODO: Go back a directory with fileInfo and look for folder.jpg for the artist
            if (fileInfo.Directory.Parent != null)
            {
                var artistThumbnailPath = Path.Combine(fileInfo.Directory.Parent.FullName, "folder.jpg");
                if (File.Exists(artistThumbnailPath))
                    return artistThumbnailPath;

                artistThumbnailPath = Path.Combine(fileInfo.Directory.Parent.FullName, "folder.jpeg");
                if (File.Exists(artistThumbnailPath))
                    return artistThumbnailPath;

                artistThumbnailPath = Path.Combine(fileInfo.Directory.Parent.FullName, "Folder.jpg");
                if (File.Exists(artistThumbnailPath))
                    return artistThumbnailPath;

                artistThumbnailPath = Path.Combine(fileInfo.Directory.Parent.FullName, "Folder.jpeg");
                if (File.Exists(artistThumbnailPath))
                    return artistThumbnailPath;
            }

            return null;
        }

        private void SaveMediaItems(IAudio audio)
        {
            var track = trackRepository.GetByTarget(audio.Location).FirstOrDefault();
            if (track == null)
            {
                var artist = audio.GetArtist(securityContext, artistRepository);
                artistRepository.Save(new List<IArtist> { artist });

                var album = audio.GetAlbum(securityContext, albumRepository, artist);
                albumRepository.Save(new List<IAlbum> { album });

                track = audio.GetTrack(securityContext, trackRepository, artist, album);
                trackRepository.Save(new List<ITrack> { track });
            }
        }

        public void HandleMedia(IMedia media)
        {
            if (media == null)
                throw new ArgumentNullException("media");

            mediaRepository.Save(new List<IMedia> { media });

            if (media is IAudio)
            {
                SaveMediaItems(media as IAudio);
            }

            var location = media.Location;
            if (location.IsFile && (location.ToString().ToLower().EndsWith("folder.jpg") || location.ToString().EndsWith("folder.jpeg")))
            {
                var fileInfo = new System.IO.FileInfo(location.LocalPath);
                var pattern = new Uri(fileInfo.DirectoryName).ToString() + "%";

                var artistThumbnailPath = GetArtistThumbnailPath(fileInfo);

                var thumbnails = new List<ILink>();
                foreach (var related in mediaRepository.ByLocation(pattern))
                {
                    thumbnails.Add(new Link(related.Location, media.Location, MediaType.ApplicationGnosisAlbumThumbnail.ToString(), fileInfo.Name));

                    if (artistThumbnailPath != null)
                    {
                        thumbnails.Add(new Link(related.Location, new Uri(artistThumbnailPath), MediaType.ApplicationGnosisArtistThumbnail.ToString(), fileInfo.Name));
                    }
                }

                System.Diagnostics.Debug.WriteLine("Saving thumbnail links: " + thumbnails.Count());
                linkRepository.Save(thumbnails);
            }
        }

        public void HandleLinks(IEnumerable<ILink> links)
        {
            if (links == null)
                throw new ArgumentNullException("links");

            linkRepository.Save(links);
        }

        public void HandleTags(IEnumerable<ITag> tags)
        {
            if (tags == null)
                throw new ArgumentNullException("tags");

            tagRepository.Save(tags);
        }

        public ITask<IEnumerable<IMedia>> Crawl(Uri target)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (!target.IsFile)
                throw new ArgumentException("target must be a local file path");

            return new CatalogMediaTask(logger, this, target, Delay, MaxErrors);
        }
    }
}
