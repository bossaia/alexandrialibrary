﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.ViewModels
{
    public class AlbumViewModel
        : IAlbumViewModel
    {
        public AlbumViewModel(Uri album, string title, Uri artist, string artistName, DateTime date, Uri thumbnail, byte[] thumbnailData, string bio)
        {
            if (album == null)
                throw new ArgumentNullException("album");
            if (title == null)
                throw new ArgumentNullException("title");
            if (artist == null)
                throw new ArgumentNullException("artist");
            if (artistName == null)
                throw new ArgumentNullException("artistName");

            this.album = album;
            this.title = title;
            this.artist = artist;
            this.artistName = artistName;
            this.date = date;
            this.bio = bio;
            this.thumbnail = thumbnail;
            this.thumbnailData = thumbnailData;
        }

        private readonly Uri album;
        private readonly string title;
        private readonly Uri artist;
        private readonly string artistName;
        private readonly DateTime date;
        private readonly string bio;
        private readonly Uri thumbnail;
        private readonly byte[] thumbnailData;

        private readonly ObservableCollection<ITrackViewModel> tracks = new ObservableCollection<ITrackViewModel>();

        public Uri Album
        {
            get { return album; }
        }

        public string Title
        {
            get { return title; }
        }

        public Uri Artist
        {
            get { return artist; }
        }

        public string ArtistName
        {
            get { return artistName; }
        }

        public string Year
        {
            get { return date.Year.ToString(); }
        }

        public string Bio
        {
            get { return bio; }
        }

        public object Image
        {
            get { return thumbnailData != null && thumbnailData.Length > 0 ? (object)thumbnailData : thumbnail; }
        }

        public IEnumerable<ITrackViewModel> Tracks
        {
            get { return tracks; }
        }

        public void AddTrack(ITrackViewModel track)
        {
            if (track == null)
                throw new ArgumentNullException("track");

            tracks.Add(track);
        }

        public void RemoveTrack(ITrackViewModel track)
        {
            if (track == null)
                throw new ArgumentNullException("track");

            if (tracks.Contains(track))
                tracks.Remove(track);
        }
    }
}
