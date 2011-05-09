﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;
using Gnosis.Core.Attributes;
using Gnosis.Core.Collections;

namespace Gnosis.Alexandria.Models.Tracks
{
    [Table("Track")]
    [Index("Track_TimeStamp_CreatedDate", "TimeStamp_CreatedDate")]
    [UniqueIndex("Track_Location", "Location")]
    [Index("Track_Title", "Title")]
    [Index("Track_TitleSort", "TitleSort")]
    [Index("Track_Album", "Album")]
    [Index("Track_AlbumSort", "AlbumSort")]
    [Index("Track_Composers", "Composers")]
    [Index("Track_Conductors", "Conductors")]
    [Index("Track_Sort", "Artists", "ReleaseDate", "Album", "TrackNumber")]
    public interface ITrack :
        IEntity
    {
        Uri Location { get; }
        string MediaType { get; set; }

        string Title { get; set; }
        string TitleSort { get; set; }
        string Subtitle { get; set; }
        string Grouping { get; set; }
        string Comment { get; set; }

        string Album { get; set; }
        string AlbumSort { get; set; }
        string AlbumSubtitle { get; set; }

        string Artists { get; set; }
        string ArtistsSort { get; set; }
        string AlbumArtists { get; set; }

        string Composers { get; set; }
        string Conductors { get; set; }
        string Genres { get; set; }
        string Moods { get; set; }
        string Languages { get; set; }
        
        DateTime RecordingDate { get; set; }
        DateTime ReleaseDate { get; set; }
        
        string OriginalTitle { get; set; }
        DateTime OriginalReleaseDate { get; set; }

        uint TrackNumber { get; set; }
        uint TrackCount { get; set; }
        uint DiscNumber { get; set; }
        uint DiscCount { get; set; }
        
        TimeSpan Duration { get; set; }
        uint BeatsPerMinute { get; set; }
        
        ulong PlayCount { get; set; }
        TimeSpan PlaylistDelay { get; set; }

        string OriginalFileName { get; set; }
        DateTime EncodingDate { get; set; }
        DateTime TaggingDate { get; set; }

        string Copyright { get; set; }
        string Publisher { get; set; }
        string InternationalStandardRecordingCode { get; set; }

        [OneToMany("TrackPictures", HasSequence=true)]
        IOrderedSet<ITrackPicture> Pictures { get; }

        [OneToMany("TrackLyrics", HasSequence = true)]
        IOrderedSet<ITrackUnsynchronizedLyrics> Lyrics { get; }

        [OneToMany("TrackSynchronizedLyrics", HasSequence = true)]
        IOrderedSet<ITrackSynchronizedLyrics> SynchronizedLyrics { get; }

        [OneToMany("TrackIdentifiers", HasSequence = true)]
        [ForeignUniqueIndex("TrackIdentifiers_Parent_Scheme_Identifier", "Parent", "Scheme", "Identifier")]
        [ForeignIndex("TrackIdentifiers_Identifier", "Identifier")]
        IOrderedSet<ITrackIdentifier> Identifiers { get; }

        [OneToMany("TrackRatings", HasSequence = true)]
        [ForeignUniqueIndex("TrackRatings_Parent_User", "Parent", "User")]
        IOrderedSet<ITrackRating> Ratings { get; }

        [OneToMany("TrackLinks", HasSequence = true)]
        IOrderedSet<ITrackLink> Links { get; }

        [OneToMany("TrackMetadata", HasSequence = true)]
        IOrderedSet<ITrackMetadata> Metadata { get; }


        [OneToMany("TrackTitleHashCodes")]
        Gnosis.Core.Collections.ISet<IHashCode> TitleHashCodes { get; }

        [OneToMany("TrackAlbumHashCodes")]
        Gnosis.Core.Collections.ISet<IHashCode> AlbumHashCodes { get; }

        [OneToMany("TrackArtistHashCodes")]
        Gnosis.Core.Collections.ISet<IHashCode> ArtistHashCodes { get; }

        [OneToMany("TrackAlbumArtistHashCodes")]
        Gnosis.Core.Collections.ISet<IHashCode> AlbumArtistHashCodes { get; }

        [OneToMany("TrackComposerHashCodes")]
        Gnosis.Core.Collections.ISet<IHashCode> ComposerHashCodes { get; }

        [OneToMany("TrackConductorHashCodes")]
        Gnosis.Core.Collections.ISet<IHashCode> ConductorHashCodes { get; }

        [OneToMany("TrackOriginalTitleHashCodes")]
        Gnosis.Core.Collections.ISet<IHashCode> OriginalTitleHashCodes { get; }
    }
}
