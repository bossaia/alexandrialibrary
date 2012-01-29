﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Gnosis.Application;
using Gnosis.Application.Xml;
using Gnosis.Application.Xml.Xspf;

using NUnit.Framework;

namespace Gnosis.Tests.Unit.Application.Xml
{
    [TestFixture]
    public class XspfDocuments
    {
        public XspfDocuments()
        {
            logger = new Gnosis.Utilities.DebugLogger();
            mediaTypeFactory = new MediaTypeFactory(logger);
            contentTypeFactory = new ContentTypeFactory(logger, mediaTypeFactory);
        }

        private ILogger logger;
        private IMediaTypeFactory mediaTypeFactory;
        private IContentTypeFactory contentTypeFactory;

        [Test]
        public void CanBeParsedFromLocalXspfFile()
        {
            const string path = @".\Files\playlist.xspf";
            const int trackCount = 3;

            var fileInfo = new FileInfo(path);
            Assert.IsTrue(fileInfo.Exists);

            var location = new Uri(fileInfo.FullName);
            var contentType = contentTypeFactory.GetByLocation(location);
            Assert.AreEqual("application/xspf+xml", contentType.Type.ToString());

            var document = mediaTypeFactory.GetByLocation(location, contentTypeFactory).CreateMedia(location) as IXmlDocument;
            Assert.IsNotNull(document);
            Assert.IsNull(document.Xml);
            
            document.Load();
            Assert.IsNotNull(document.Xml);
            Assert.IsNotNull(document.Xml.Root);

            var playlist = document.Xml.Root as IXspfPlaylist;
            Assert.IsNotNull(playlist);

            var tracks = playlist.Where<IXspfTrack>(x => x != null);
            Assert.AreEqual(trackCount, tracks.Count());
        }
    }
}