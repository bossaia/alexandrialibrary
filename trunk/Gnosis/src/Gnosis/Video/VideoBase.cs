﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Video
{
    public abstract class VideoBase
        : IVideo
    {
        protected VideoBase(Uri location, IMediaType type)
        {
            this.location = location;
            this.type = type;
        }

        private readonly Uri location;
        private readonly IMediaType type;

        public Uri Location
        {
            get { return location; }
        }

        public IMediaType Type
        {
            get { return type; }
        }

        public virtual void Load()
        {
        }

        public virtual IEnumerable<ILink> GetLinks()
        {
            return Enumerable.Empty<ILink>();
        }

        public virtual IEnumerable<ITag> GetTags()
        {
            return Enumerable.Empty<ITag>();
        }

        public virtual IArtist GetArtist(ISecurityContext securityContext, IMediaItemRepository<IClip> clipRepository, IMediaItemRepository<IArtist> artistRepository)
        {
            return null;
        }
        
        public virtual IAlbum GetAlbum(ISecurityContext securityContext, IMediaItemRepository<IClip> clipRepository, IMediaItemRepository<IAlbum> albumRepository, IArtist artist)
        {
            return null;
        }

        public virtual IClip GetClip(ISecurityContext securityContext, IMediaItemRepository<IClip> clipRepository, IArtist artist, IAlbum album)
        {
            return null;
        }
    }
}