﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class GnosisPic
        : IPic
    {
        public GnosisPic(string title, uint number, uint height, uint width, Uri creator, string creatorName, Uri album, string albumTitle, Uri target, IMediaType targetType, Uri thumbnail)
            : this(title, number, height, width, creator, creatorName, album, albumTitle, target, targetType, thumbnail, Guid.NewGuid().ToUrn())
        {
        }

        public GnosisPic(string title, uint number, uint height, uint width, Uri creator, string creatorName, Uri album, string albumTitle, Uri target, IMediaType targetType, Uri thumbnail, Uri location)
        {
            if (title == null)
                throw new ArgumentNullException("title");
            if (creator == null)
                throw new ArgumentNullException("creator");
            if (creatorName == null)
                throw new ArgumentNullException("creatorName");
            if (album == null)
                throw new ArgumentNullException("album");
            if (albumTitle == null)
                throw new ArgumentNullException("albumTitle");
            if (location == null)
                throw new ArgumentNullException("location");

            this.title = title;
            this.number = number;
            this.height = height;
            this.width = width;
            this.creator = creator;
            this.creatorName = creatorName;
            this.album = album;
            this.albumTitle = albumTitle;
            this.target = target;
            this.targetType = targetType;
            this.thumbnail = thumbnail;
            this.location = location;
        }

        private readonly string title;
        private readonly uint number;
        private readonly uint height;
        private readonly uint width;
        private readonly Uri creator;
        private readonly string creatorName;
        private readonly Uri album;
        private readonly string albumTitle;
        private readonly Uri target;
        private readonly IMediaType targetType;
        private readonly Uri thumbnail;
        private readonly Uri location;

        public string Title
        {
            get { return title; }
        }

        public uint Number
        {
            get { return number; }
        }

        public uint Height
        {
            get { return height; }
        }

        public uint Width
        {
            get { return width; }
        }

        public Uri Creator
        {
            get { return creator; }
        }

        public string CreatorName
        {
            get { return creatorName; }
        }

        public Uri Album
        {
            get { return album; }
        }

        public string AlbumTitle
        {
            get { return albumTitle; }
        }

        public Uri Target
        {
            get { return target; }
        }

        public IMediaType TargetType
        {
            get { return targetType; }
        }

        public Uri Thumbnail
        {
            get { return thumbnail; }
        }

        public Uri Location
        {
            get { return location; }
        }

        public IMediaType Type
        {
            get { return MediaType.ApplicationGnosisPic; }
        }

        public void Load()
        {
        }

        public IEnumerable<ILink> GetLinks()
        {
            return Enumerable.Empty<ILink>();
        }

        public IEnumerable<ITag> GetTags()
        {
            return Enumerable.Empty<ITag>();
        }
    }
}
