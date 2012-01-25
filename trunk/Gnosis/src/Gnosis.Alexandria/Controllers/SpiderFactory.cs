﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Audio;
using Gnosis.Spiders;

namespace Gnosis.Alexandria.Controllers
{
    public class SpiderFactory
    {
        public SpiderFactory(ILogger logger, ISecurityContext securityContext, IContentTypeFactory contentTypeFactory, IMediaTypeFactory mediaTypeFactory, ILinkRepository linkRepository, ITagRepository tagRepository, IMediaRepository mediaRepository, IMediaItemRepository mediaItemRepository, IAudioStreamFactory audioStreamFactory)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (securityContext == null)
                throw new ArgumentNullException("securityContext");
            if (contentTypeFactory == null)
                throw new ArgumentNullException("contentTypeFactory");
            if (mediaTypeFactory == null)
                throw new ArgumentNullException("mediaTypeFactory");
            if (linkRepository == null)
                throw new ArgumentNullException("linkRepository");
            if (tagRepository == null)
                throw new ArgumentNullException("tagRepository");
            if (mediaRepository == null)
                throw new ArgumentNullException("mediaRepository");
            if (mediaItemRepository == null)
                throw new ArgumentNullException("mediaItemRepository");

            this.logger = logger;
            this.securityContext = securityContext;
            this.securityContext = securityContext;
            this.contentTypeFactory = contentTypeFactory;
            this.mediaTypeFactory = mediaTypeFactory;
            this.linkRepository = linkRepository;
            this.tagRepository = tagRepository;
            this.mediaRepository = mediaRepository;
            this.mediaItemRepository = mediaItemRepository;
            this.audioStreamFactory = audioStreamFactory;
        }

        private readonly ILogger logger;
        private readonly ISecurityContext securityContext;
        private readonly IContentTypeFactory contentTypeFactory;
        private readonly IMediaTypeFactory mediaTypeFactory;
        private readonly ILinkRepository linkRepository;
        private readonly ITagRepository tagRepository;
        private readonly IMediaRepository mediaRepository;
        private readonly IMediaItemRepository mediaItemRepository;
        private readonly IAudioStreamFactory audioStreamFactory;
        
        public ISpider CreateCatalogSpider()
        {
            return new CatalogSpider(logger, securityContext, contentTypeFactory, mediaTypeFactory, linkRepository, tagRepository, mediaRepository, mediaItemRepository, audioStreamFactory);
        }
    }
}
