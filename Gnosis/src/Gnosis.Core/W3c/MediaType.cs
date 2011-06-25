﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.W3c
{
    public class MediaType
        : IMediaType
    {
        private MediaType(string type, string subType)
            : this(type, subType, new List<string>(), new List<string>())
        {
        }

        private MediaType(string type, string subType, IEnumerable<string> fileExtensions)
            : this(type, subType, fileExtensions, new List<string>())
        {
        }

        private MediaType(string type, string subType, IEnumerable<string> fileExtensions, IEnumerable<string> magicNumbers)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (subType == null)
                throw new ArgumentNullException("subType");
            if (fileExtensions == null)
                throw new ArgumentNullException("fileExtensions");
            if (magicNumbers == null)
                throw new ArgumentNullException("magicNumbers");

            this.type = type;
            this.subType = subType;
            this.fileExtensions = fileExtensions;
            this.magicNumbers = magicNumbers;
        }

        private readonly string type;
        private readonly string subType;
        private readonly IEnumerable<string> fileExtensions;
        private readonly IEnumerable<string> magicNumbers;

        #region IMediaType Members

        public string Type
        {
            get { return type; }
        }

        public string SubType
        {
            get { return type; }
        }

        public IEnumerable<string> FileExtensions
        {
            get { return fileExtensions; }
        }

        public IEnumerable<string> MagicNumbers
        {
            get { return magicNumbers; }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0}/{1}", type, subType);
        }

        static MediaType()
        {
            InitializeMediaTypes();

            foreach (var mediaType in mediaTypes)
            {
                byCode.Add(mediaType.ToString(), mediaType);

                if (!byType.ContainsKey(mediaType.Type))
                    byType.Add(mediaType.Type, new List<IMediaType> { mediaType });
                else
                    byType[mediaType.Type].Add(mediaType); 

                if (mediaType.FileExtensions != null && mediaType.FileExtensions.Count() > 0)
                {
                    foreach (var fileExtension in mediaType.FileExtensions)
                    {
                        if (!byFileExtension.ContainsKey(fileExtension))
                            byFileExtension.Add(fileExtension, new List<IMediaType> { mediaType });
                        else
                            byFileExtension[fileExtension].Add(mediaType);
                    }
                }
            }
        }

        private static readonly IList<IMediaType> mediaTypes = new List<IMediaType>();
        private static readonly IDictionary<string, IMediaType> byCode = new Dictionary<string, IMediaType>();
        private static readonly IDictionary<string, IList<IMediaType>> byType = new Dictionary<string, IList<IMediaType>>();
        private static readonly IDictionary<string, IList<IMediaType>> byFileExtension = new Dictionary<string, IList<IMediaType>>();

        #region InitializeMediaTypes

        private static void InitializeMediaTypes()
        {
            mediaTypes.Add(AtomFeed);
            mediaTypes.Add(MpegAudio);
            mediaTypes.Add(RssFeed);
            mediaTypes.Add(Unknown);
        }

        #endregion

        #region Public Static Methods

        public static IMediaType Parse(string value)
        {
            return byCode.ContainsKey(value) ? byCode[value] : Unknown;
        }

        public static IEnumerable<IMediaType> GetMediaTypesByType(string type)
        {
            return (byType.ContainsKey(type)) ?
                byType[type]
                : new List<IMediaType>();
        }

        public static IEnumerable<IMediaType> GetMediaTypesByFileExtension(string fileExtension)
        {
            return (byFileExtension.ContainsKey(fileExtension)) ?
                byFileExtension[fileExtension]
                : new List<IMediaType>();
        }

        public static IEnumerable<IMediaType> GetMediaTypes()
        {
            return mediaTypes;
        }

        #endregion

        #region Media Types

        public static readonly IMediaType AtomFeed = new MediaType("application", "atom+xml", new List<string> { ".atom", ".xml" });
        public static readonly IMediaType MpegAudio = new MediaType("audio", "mpeg", new List<string> { ".mp3", ".mp2", ".mp1" });
        public static readonly IMediaType RssFeed = new MediaType("application", "rss+xml", new List<string> { ".rss", ".xml" });
        
        public static readonly IMediaType Unknown = new MediaType("application", "unknown");

        #endregion
    }
}
