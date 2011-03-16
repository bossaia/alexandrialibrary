﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Archon
{
    public class Track : ITrack
    {
        public Track() : this(Guid.NewGuid())
        {
        }

        public Track(Guid id)
        {
            this.id = id;
            TitleHash = title.AsNameHash();
            TitleMetaphone = title.AsDoubleMetaphone();
            ArtistHash = artist.AsNameHash();
            ArtistMetaphone = artist.AsDoubleMetaphone();
            AlbumHash = album.AsNameHash();
            AlbumMetaphone = album.AsDoubleMetaphone();
        }

        public const string DEFAULT_TITLE = "Untitled";
        public const string DEFAULT_ARTIST = "Unknown Artist";
        public const string DEFAULT_ALBUM = "Unknown Album";
        public const uint DEFAULT_TRACK = 0;
        public const uint DEFAULT_DISC = 0;
        public const string DEFAULT_GENRE = "Unknown Genre";
        public static readonly DateTime DEFAULT_RELEASE_DATE = new DateTime(2000, 1, 1);

        private readonly Guid id;
        private string path;
        private string imagePath = string.Empty;
        private ICollection<byte> imageData;
        private string title = DEFAULT_TITLE;
        private string artist = DEFAULT_ARTIST;
        private string album = DEFAULT_ALBUM;
        private uint trackNumber = DEFAULT_TRACK;
        private uint discNumber = DEFAULT_DISC;
        private string genre = DEFAULT_GENRE;
        private DateTime releaseDate = DEFAULT_RELEASE_DATE;
        private bool isSelected;
        private string playbackStatus;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region ITrack Members

        public Guid Id
        {
            get { return id; }
        }

        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                if (path != value)
                {
                    path = value;
                    OnPropertyChanged("Path");
                }
            }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                if (imagePath != value)
                {
                    imagePath = value;
                    OnPropertyChanged("ImagePath");
                    OnPropertyChanged("ImageSource");
                }
            }
        }

        public ICollection<byte> ImageData
        {
            get { return imageData; }
            set
            {
                if (imageData != value)
                {
                    imageData = value;
                    OnPropertyChanged("ImageData");
                    OnPropertyChanged("ImageSource");
                }
            }
        }

        public object ImageSource
        {
            get
            {
                return !string.IsNullOrEmpty(imagePath) ? (object)imagePath : imageData;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public string TitleHash
        {
            get;
            private set;
        }

        public string TitleMetaphone
        {
            get;
            private set;
        }

        public string Artist
        {
            get
            {
                return artist;
            }
            set
            {
                if (artist != value)
                {
                    artist = value;
                    OnPropertyChanged("Artist");
                }
            }
        }

        public string ArtistHash
        {
            get;
            private set;
        }

        public string ArtistMetaphone
        {
            get;
            private set;
        }

        public string Album
        {
            get
            {
                return album;
            }
            set
            {
                if (album != value)
                {
                    album = value;
                    OnPropertyChanged("Album");
                }
            }
        }

        public string AlbumHash
        {
            get;
            private set;
        }

        public string AlbumMetaphone
        {
            get;
            private set;
        }

        public uint TrackNumber
        {
            get
            {
                return trackNumber;
            }
            set
            {
                if (trackNumber != value)
                {
                    trackNumber = value;
                    OnPropertyChanged("TrackNumber");
                }
            }
        }

        public uint DiscNumber
        {
            get
            {
                return discNumber;
            }
            set
            {
                if (discNumber != value)
                {
                    discNumber = value;
                    OnPropertyChanged("DiscNumber");
                }
            }
        }

        public string Genre
        {
            get { return genre; }
            set
            {
                if (genre != value)
                {
                    genre = value;
                    OnPropertyChanged("Genre");
                }
            }
        }

        public DateTime ReleaseDate
        {
            get { return releaseDate; }
            set
            {
                if (releaseDate != value)
                {
                    releaseDate = value;
                    OnPropertyChanged("ReleaseYear");
                    OnPropertyChanged("ReleaseDate");
                }
            }
        }

        public string ReleaseYear
        {
            get { return releaseDate.Year.ToString(); }
            set
            {
                if (releaseDate.Year != int.Parse(value))
                {
                    releaseDate = new DateTime(int.Parse(value), 1, 1);
                    OnPropertyChanged("ReleaseYear");
                    OnPropertyChanged("ReleaseDate");
                }
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        public string PlaybackStatus
        {
            get { return playbackStatus; }
            set
            {
                if (playbackStatus != value)
                {
                    playbackStatus = value;
                    OnPropertyChanged("PlaybackStatus");
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
