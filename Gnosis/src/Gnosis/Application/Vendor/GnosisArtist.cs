﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Vendor
{
    public class GnosisArtist
        : IArtist
    {
        public GnosisArtist(string name, DateTime fromDate, DateTime toDate, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail)
            : this(name, fromDate, toDate, creator, creatorName, catalog, catalogName, target, targetType, user, userName, thumbnail, Guid.NewGuid().ToUrn())
        {
        }

        public GnosisArtist(string name, DateTime fromDate, DateTime toDate, Uri creator, string creatorName, Uri catalog, string catalogName, Uri target, IMediaType targetType, Uri user, string userName, Uri thumbnail, Uri location)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (location == null)
                throw new ArgumentNullException("location");

            this.name = name;
            this.fromDate = fromDate;
            this.toDate = toDate;
            this.creator = creator;
            this.creatorName = creatorName;
            this.catalog = catalog;
            this.catalogName = catalogName;
            this.target = target;
            this.targetType = targetType;

            this.user = user;
            this.userName = userName;
            this.thumbnail = thumbnail;
            this.location = location;
        }

        private readonly Uri location;
        private readonly string name;
        private readonly DateTime fromDate;
        private readonly DateTime toDate;
        private readonly Uri creator;
        private readonly string creatorName;
        private readonly Uri catalog;
        private readonly string catalogName;
        private readonly Uri target;
        private readonly IMediaType targetType;
        private readonly Uri user;
        private readonly string userName;
        private readonly Uri thumbnail;

        public Uri Location
        {
            get { return location; }
        }

        public IMediaType Type
        {
            get { return MediaType.ApplicationGnosisArtist; }
        }

        public string Name
        {
            get { return name; }
        }

        public DateTime FromDate
        {
            get { return fromDate; }
        }

        public DateTime ToDate
        {
            get { return toDate; }
        }

        public Uri Creator
        {
            get { return creator; }
        }

        public string CreatorName
        {
            get { return creatorName; }
        }

        public Uri Catalog
        {
            get { return catalog; }
        }

        public string CatalogName
        {
            get { return catalogName; }
        }

        public Uri Target
        {
            get { return target; }
        }

        public IMediaType TargetType
        {
            get { return targetType; }
        }

        public Uri User
        {
            get { return user; }
        }

        public string UserName
        {
            get { return userName; }
        }

        public Uri Thumbnail
        {
            get { return thumbnail; }
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

        public static readonly IArtist Unknown = new GnosisArtist("Unknown", DateTime.MinValue, DateTime.MaxValue, Guid.Empty.ToUrn(), "Unknown", Guid.Empty.ToUrn(), "Unknown", Guid.Empty.ToUrn(), MediaType.ApplicationUnknown, Guid.Empty.ToUrn(), "Administrator", Guid.Empty.ToUrn(), Guid.Empty.ToUrn());
    }
}
