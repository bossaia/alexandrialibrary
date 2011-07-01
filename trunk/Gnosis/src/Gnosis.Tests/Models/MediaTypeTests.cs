﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Core;
using Gnosis.Core.W3c;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class MediaTypeTests
    {
        [Test]
        public void LookupMediaType()
        {
            //const int total = 8;
            //const int magicNumberTotal = 3;
            var list = new List<IMediaType>();
            var byMagicNumber = new Dictionary<byte[], IMediaType>();

            foreach (var mediaType in MediaType.GetMediaTypes())
            {
                Assert.IsNotNull(mediaType);
                list.Add(mediaType);

                Assert.AreEqual(mediaType, MediaType.Parse(mediaType.ToString()));

                var byTypeList = MediaType.GetMediaTypesByType(mediaType.Type);
                Assert.IsNotNull(byTypeList);
                Assert.IsTrue(byTypeList.Contains(mediaType));

                foreach (var fileExtension in mediaType.FileExtensions)
                {
                    var types = MediaType.GetMediaTypesByFileExtension(fileExtension);
                    Assert.IsNotNull(types);
                    Assert.IsTrue(types.Contains(mediaType));
                }

                foreach (var magicNumber in mediaType.MagicNumbers)
                    byMagicNumber.Add(magicNumber, mediaType);
            }

            //Assert.AreEqual(total, list.Count());
            //Assert.AreEqual(magicNumberTotal, byMagicNumber.Count);
        }

        [Test]
        public void GetMediaTypeForRemoteRssFeedWithInvalidContentType()
        {
            var location = new Uri("http://feeds.arstechnica.com/arstechnica/index");
            var mediaType = location.ToMediaType();
            Assert.AreEqual(MediaType.RssFeed, mediaType);
        }

        [Test]
        public void GetMediaTypeForRemoteAtomFeed()
        {
            var location = new Uri("http://www.blogger.com/feeds/8677504/posts/default");
            var mediaType = location.ToMediaType();
            Assert.AreEqual(MediaType.AtomFeed, mediaType);
        }

        [Test]
        public void GetMediaTypeForLocalPng()
        {
            var path = @".\Files\radiohead.png";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = location.ToMediaType();
            Assert.AreEqual(MediaType.PngImage, mediaType);
        }

        [Test]
        public void GetMediaTypeForLocalPngWithInvalidFileExtension()
        {
            var path = @".\Files\radiohead";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = location.ToMediaType();
            Assert.AreEqual(MediaType.PngImage, mediaType);
        }

        [Test]
        public void GetMediaTypeForRemotePng()
        {
            var location = new Uri("http://upload.wikimedia.org/wikipedia/commons/thumb/4/47/PNG_transparency_demonstration_1.png/280px-PNG_transparency_demonstration_1.png");
            var mediaType = location.ToMediaType();
            Assert.AreEqual(MediaType.PngImage, mediaType);
        }

        [Test]
        public void GetMediaTypeForLocalGif()
        {
            var path = @".\Files\nin.gif";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = location.ToMediaType();
            Assert.AreEqual(MediaType.GifImage, mediaType);
        }

        [Test]
        public void GetMediaTypeForLocalGifWithInvalidFileExtension()
        {
            var path = @".\Files\nin";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = location.ToMediaType();
            Assert.AreEqual(MediaType.GifImage, mediaType);
        }

        [Test]
        public void GetMediaTypeForRemoteGif()
        {
            var location = new Uri("http://upload.wikimedia.org/wikipedia/commons/thumb/2/2c/Rotating_earth_%28large%29.gif/200px-Rotating_earth_%28large%29.gif");
            var mediaType = location.ToMediaType();
            Assert.AreEqual(MediaType.GifImage, mediaType);
        }

        [Test]
        public void GetMediaTypeForLocalJpeg()
        {
            var path = @".\Files\Undertow.jpg";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = location.ToMediaType();
            Assert.AreEqual(MediaType.JpegImage, mediaType);
        }

        [Test]
        public void GetMediaTypeForLocalJpegWithInvalidFileExtension()
        {
            var path = @".\Files\Undertow";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = location.ToMediaType();
            Assert.AreEqual(MediaType.JpegImage, mediaType);
        }

        [Test]
        public void GetMediaTypeForRemoteJpg()
        {
            var location = new Uri("http://upload.wikimedia.org/wikipedia/commons/b/b4/JPEG_example_JPG_RIP_100.jpg");
            var mediaType = location.ToMediaType();
            Assert.AreEqual(MediaType.JpegImage, mediaType);
        }

        [Test]
        public void GetMediaTypeForRemoteHtml()
        {
            var location = new Uri("http://arstechnica.com/");
            var mediaType = location.ToMediaType();
            Assert.AreEqual(MediaType.HtmlDoc, mediaType);
        }
    }
}
