﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Gnosis.Tests.Media
{
    [TestFixture]
    public class MediaTypeItems
    {
        [Test]
        public void CanBeReadByName()
        {
            var list = new List<IMediaType>();

            foreach (var mediaType in MediaType.GetMediaTypes())
            {
                Assert.IsNotNull(mediaType);
                list.Add(mediaType);

                Assert.AreEqual(mediaType, MediaType.Parse(mediaType.ToString()));
                Assert.AreEqual(mediaType, MediaType.Parse(mediaType.Type + "/" + mediaType.SubType));

            }
        }

        [Test]
        public void CanBeReadByFileExtension()
        {
            foreach (var mediaType in MediaType.GetMediaTypes())
            {
                foreach (var fileExtension in mediaType.FileExtensions)
                {
                    var types = MediaType.GetMediaTypesByFileExtension(fileExtension);
                    Assert.IsNotNull(types);
                    Assert.IsTrue(types.Contains(mediaType));
                }
            }
        }

        [Test]
        public void CanBeReadBySuperType()
        {
            foreach (var mediaType in MediaType.GetMediaTypes())
            {
                var byTypeList = MediaType.GetMediaTypesByType(mediaType.Type);
                Assert.IsNotNull(byTypeList);
                Assert.IsTrue(byTypeList.Contains(mediaType));
            }
        }

        [Test]
        public void CanBeReadByMagicNumber()
        {
            var byMagicNumber = new Dictionary<byte[], IMediaType>();

            foreach (var mediaType in MediaType.GetMediaTypes())
            {
                foreach (var magicNumber in mediaType.MagicNumbers)
                {
                    byMagicNumber.Add(magicNumber, mediaType);
                    var check = MediaType.GetMediaTypeByMagicNumber(magicNumber);
                    Assert.AreEqual(mediaType, check);
                }
            }
        }

        [Test]
        public void CanGetMediaTypeForRemoteRssFeedWithInvalidContentType()
        {
            var location = new Uri("http://feeds.arstechnica.com/arstechnica/index");
            var mediaType = MediaType.GetMediaType(location);
            Assert.AreEqual(MediaType.ApplicationRssXml, mediaType);
        }

        [Test]
        public void CanGetMediaTypeForRemoteAtomFeed()
        {
            var location = new Uri("http://www.blogger.com/feeds/8677504/posts/default");
            var mediaType = MediaType.GetMediaType(location);
            Assert.AreEqual(MediaType.ApplicationAtomXml, mediaType);
        }

        [Test]
        public void CanGetMediaTypeForLocalPng()
        {
            var path = @".\Files\radiohead.png";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = MediaType.GetMediaType(location);
            Assert.AreEqual(MediaType.ImagePng, mediaType);
        }

        [Test]
        public void CanGetMediaTypeForLocalPngWithInvalidFileExtension()
        {
            var path = @".\Files\radiohead";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = MediaType.GetMediaType(location);
            Assert.AreEqual(MediaType.ImagePng, mediaType);
        }

        //TODO: Choose another remote PNG resource, the one at this URL is SLOW
        //[Test]
        //public void GetMediaTypeForRemotePng()
        //{
        //    var location = new Uri("http://upload.wikimedia.org/wikipedia/commons/thumb/4/47/PNG_transparency_demonstration_1.png/280px-PNG_transparency_demonstration_1.png");
        //    var mediaType = location.ToMediaType();
        //    Assert.AreEqual(MediaType.ImagePng, mediaType);
        //}

        [Test]
        public void CanGetMediaTypeForLocalGif()
        {
            var path = @".\Files\nin.gif";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = MediaType.GetMediaType(location);
            Assert.AreEqual(MediaType.ImageGif, mediaType);
        }

        [Test]
        public void CanGetMediaTypeForLocalGifWithInvalidFileExtension()
        {
            var path = @".\Files\nin";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = MediaType.GetMediaType(location);
            Assert.AreEqual(MediaType.ImageGif, mediaType);
        }

        [Test]
        public void CanGetMediaTypeForRemoteGif()
        {
            var location = new Uri("http://upload.wikimedia.org/wikipedia/commons/thumb/2/2c/Rotating_earth_%28large%29.gif/200px-Rotating_earth_%28large%29.gif");
            var mediaType = MediaType.GetMediaType(location);
            Assert.AreEqual(MediaType.ImageGif, mediaType);
        }

        [Test]
        public void CanGetMediaTypeForLocalJpeg()
        {
            var path = @".\Files\Undertow.jpg";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = MediaType.GetMediaType(location);
            Assert.AreEqual(MediaType.ImageJpeg, mediaType);
        }

        [Test]
        public void CanGetMediaTypeForLocalJpegWithInvalidFileExtension()
        {
            var path = @".\Files\Undertow";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = MediaType.GetMediaType(location);
            Assert.AreEqual(MediaType.ImageJpeg, mediaType);
        }

        [Test]
        public void CanGetMediaTypeForRemoteJpg()
        {
            var location = new Uri("http://upload.wikimedia.org/wikipedia/commons/b/b4/JPEG_example_JPG_RIP_100.jpg");
            var mediaType = MediaType.GetMediaType(location);
            Assert.AreEqual(MediaType.ImageJpeg, mediaType);
        }

        [Test]
        public void CanGetMediaTypeForRemoteHtml()
        {
            var location = new Uri("http://arstechnica.com/");
            var mediaType = MediaType.GetMediaType(location);
            Assert.AreEqual(MediaType.TextHtml, mediaType);
        }

        [Test]
        public void CanGetMediaTypeForLocalMp3ByMagicNumber()
        {
            var path = @".\Files\03 - Antes De Las Seis.mp3";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = MediaType.GetMediaType(location);
            Assert.AreEqual(MediaType.AudioMpeg, mediaType);
        }

        [Test]
        public void CanGetMediaTypeForLocalMp3ByMagicNumber2()
        {
            var path = @".\Files\13 - Loca (Featuring Dizzee Rascal).mp3";
            var fileInfo = new System.IO.FileInfo(path);
            Assert.IsTrue(System.IO.File.Exists(path));
            var location = new Uri(fileInfo.FullName, UriKind.Absolute);
            var mediaType = MediaType.GetMediaType(location);
            Assert.AreEqual(MediaType.AudioMpeg, mediaType);
        }
    }
}