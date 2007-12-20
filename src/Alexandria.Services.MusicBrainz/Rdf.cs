#region License (MIT)
/***************************************************************************
 *  Rdf.cs
 *
 *  Copyright (C) 2005 Novell
 *  Written by Aaron Bockover (aaron@aaronbock.net)
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
 #endregion
 
namespace Telesophy.Alexandria.MusicBrainz
{
    public class Rdf
    {
		#region Private Fields
		private string AMAZON_COVER_ART_URL_PREFIX = "http://www.amazon.com/gp/product/images/";
        private string MBI_VARIOUS_ARTIST_ID = "89ad4ac3-39f7-470e-963a-56509c546377",
            MBS_Rewind =
             "[REWIND]",
            MBS_Back =
             "[BACK]",
            MBS_SelectArtist =
             "http://musicbrainz.org/mm/mm-2.1#artistList []",
            MBS_SelectAlbum =
             "http://musicbrainz.org/mm/mm-2.1#albumList []",
            MBS_SelectTrack =
             "http://musicbrainz.org/mm/mm-2.1#trackList []",
            MBS_SelectTrackArtist =
             "http://purl.org/dc/elements/1.1/creator",
            MBS_SelectTrackAlbum =
             "http://musicbrainz.org/mm/mq-1.1#album",
            MBS_SelectTrmid =
             "http://musicbrainz.org/mm/mm-2.1#trmidList []",
            MBS_SelectCdindexid =
             "http://musicbrainz.org/mm/mm-2.1#cdindexidList []",
            MBS_SelectReleaseDate =
             "http://musicbrainz.org/mm/mm-2.1#releaseDateList []",
            MBS_SelectLookupResult =
             "http://musicbrainz.org/mm/mq-1.1#lookupResultList []",
            MBS_SelectLookupResultArtist =
             "http://musicbrainz.org/mm/mq-1.1#artist",
            MBS_SelectLookupResultAlbum =
             "http://musicbrainz.org/mm/mq-1.1#album",
            MBS_SelectLookupResultTrack =
             "http://musicbrainz.org/mm/mq-1.1#track",
            MBE_GetStatus =
             "http://musicbrainz.org/mm/mq-1.1#status",
            MBE_GetNumArtists =
             "http://musicbrainz.org/mm/mm-2.1#artistList [COUNT]",
            MBE_GetNumAlbums =
             "http://musicbrainz.org/mm/mm-2.1#albumList [COUNT]",
            MBE_GetNumTracks =
             "http://musicbrainz.org/mm/mm-2.1#trackList [COUNT]",
            MBE_GetNumTrmids =
             "http://musicbrainz.org/mm/mm-2.1#trmidList [COUNT]",
            MBE_GetNumLookupResults =
             "http://musicbrainz.org/mm/mm-2.1#lookupResultList [COUNT]",
            MBE_ArtistGetArtistName =
             "http://purl.org/dc/elements/1.1/title",
            MBE_ArtistGetArtistSortName =
             "http://musicbrainz.org/mm/mm-2.1#sortName",
            MBE_ArtistGetArtistId =
             "",
            MBE_ArtistGetAlbumName =
             "http://musicbrainz.org/mm/mm-2.1#albumList [] http://purl.org/dc/elements/1.1/title",
            MBE_ArtistGetAlbumId =
             "http://musicbrainz.org/mm/mm-2.1#albumList []",
            MBE_AlbumGetAlbumName =
             "http://purl.org/dc/elements/1.1/title",
            MBE_AlbumGetAlbumId =
             "",
            MBE_AlbumGetAlbumStatus =
             "http://musicbrainz.org/mm/mm-2.1#releaseStatus",
            MBE_AlbumGetAlbumType =
             "http://musicbrainz.org/mm/mm-2.1#releaseType",
            MBE_AlbumGetAmazonAsin =
             "http://www.amazon.com/gp/aws/landing.html#Asin",
            MBE_AlbumGetAmazonCoverartURL =
             "http://musicbrainz.org/mm/mm-2.1#coverart",
            MBE_AlbumGetNumCdindexIds =
             "http://musicbrainz.org/mm/mm-2.1#cdindexidList [COUNT]",
            MBE_AlbumGetNumReleaseDates =
             "http://musicbrainz.org/mm/mm-2.1#releaseDateList [COUNT]",
            MBE_AlbumGetAlbumArtistId =
             "http://purl.org/dc/elements/1.1/creator",
            MBE_AlbumGetNumTracks =
             "http://musicbrainz.org/mm/mm-2.1#trackList [COUNT]",
            MBE_AlbumGetTrackId =
             "http://musicbrainz.org/mm/mm-2.1#trackList [] ",
            MBE_AlbumGetTrackList =
             "http://musicbrainz.org/mm/mm-2.1#trackList",
            MBE_AlbumGetTrackNum =
             "http://musicbrainz.org/mm/mm-2.1#trackList [?] http://musicbrainz.org/mm/mm-2.1#trackNum",
            MBE_AlbumGetTrackName =
             "http://musicbrainz.org/mm/mm-2.1#trackList [] http://purl.org/dc/elements/1.1/title",
            MBE_AlbumGetTrackDuration =
             "http://musicbrainz.org/mm/mm-2.1#trackList [] http://musicbrainz.org/mm/mm-2.1#duration",
            MBE_AlbumGetArtistName =
             "http://musicbrainz.org/mm/mm-2.1#trackList [] http://purl.org/dc/elements/1.1/creator http://purl.org/dc/elements/1.1/title",
            MBE_AlbumGetArtistSortName =
             "http://musicbrainz.org/mm/mm-2.1#trackList [] http://purl.org/dc/elements/1.1/creator http://musicbrainz.org/mm/mm-2.1#sortName",
            MBE_AlbumGetArtistId =
             "http://musicbrainz.org/mm/mm-2.1#trackList [] http://purl.org/dc/elements/1.1/creator",
            MBE_TrackGetTrackName =
             "http://purl.org/dc/elements/1.1/title",
            MBE_TrackGetTrackId =
             "",
            MBE_TrackGetTrackNum =
             "http://musicbrainz.org/mm/mm-2.1#trackNum",
            MBE_TrackGetTrackDuration =
             "http://musicbrainz.org/mm/mm-2.1#duration",
            MBE_TrackGetArtistName =
             "http://purl.org/dc/elements/1.1/creator http://purl.org/dc/elements/1.1/title",
            MBE_TrackGetArtistSortName =
             "http://purl.org/dc/elements/1.1/creator http://musicbrainz.org/mm/mm-2.1#sortName",
            MBE_TrackGetArtistId =
             "http://purl.org/dc/elements/1.1/creator",
            MBE_QuickGetArtistName =
             "http://musicbrainz.org/mm/mq-1.1#artistName",
            MBE_QuickGetArtistSortName =
             "http://musicbrainz.org/mm/mm-2.1#sortName",
            MBE_QuickGetArtistId =
             "http://musicbrainz.org/mm/mm-2.1#artistid",
            MBE_QuickGetAlbumName =
             "http://musicbrainz.org/mm/mq-1.1#albumName",
            MBE_QuickGetTrackName =
             "http://musicbrainz.org/mm/mq-1.1#trackName",
            MBE_QuickGetTrackNum =
             "http://musicbrainz.org/mm/mm-2.1#trackNum",
            MBE_QuickGetTrackId =
             "http://musicbrainz.org/mm/mm-2.1#trackid",
            MBE_QuickGetTrackDuration =
             "http://musicbrainz.org/mm/mm-2.1#duration",
            MBE_ReleaseGetDate =
             "http://purl.org/dc/elements/1.1/date",
            MBE_ReleaseGetCountry =
             "http://musicbrainz.org/mm/mm-2.1#country",
            MBE_LookupGetType =
             "http://www.w3.org/1999/02/22-rdf-syntax-ns#type",
            MBE_LookupGetRelevance =
             "http://musicbrainz.org/mm/mq-1.1#relevance",
            MBE_LookupGetArtistId =
             "http://musicbrainz.org/mm/mq-1.1#artist",
            MBE_LookupGetAlbumId =
             "http://musicbrainz.org/mm/mq-1.1#album",
            MBE_LookupGetAlbumArtistId =
             "http://musicbrainz.org/mm/mq-1.1#album " +
             "http://purl.org/dc/elements/1.1/creator",
            MBE_LookupGetTrackId =
             "http://musicbrainz.org/mm/mq-1.1#track",
            MBE_LookupGetTrackArtistId =
             "http://musicbrainz.org/mm/mq-1.1#track " +
             "http://purl.org/dc/elements/1.1/creator",
            MBE_TOCGetCDIndexId =
             "http://musicbrainz.org/mm/mm-2.1#cdindexid",
            MBE_TOCGetFirstTrack =
             "http://musicbrainz.org/mm/mm-2.1#firstTrack",
            MBE_TOCGetLastTrack =
             "http://musicbrainz.org/mm/mm-2.1#lastTrack",
            MBE_TOCGetTrackSectorOffset =
             "http://musicbrainz.org/mm/mm-2.1#toc [] http://musicbrainz.org/mm/mm-2.1#sectorOffset",
            MBE_TOCGetTrackNumSectors =
             "http://musicbrainz.org/mm/mm-2.1#toc [] http://musicbrainz.org/mm/mm-2.1#numSectors",
            MBE_AuthGetSessionId =
             "http://musicbrainz.org/mm/mq-1.1#sessionId",
            MBE_AuthGetChallenge =
             "http://musicbrainz.org/mm/mq-1.1#authChallenge",
            MBQ_GetCDInfo =
             "@CDINFO@",
            MBQ_GetCDTOC =
             "@LOCALCDINFO@",
            MBQ_AssociateCD =
             "@CDINFOASSOCIATECD@",
            MBQ_Authenticate =
             "<mq:AuthenticateQuery>\n" +
             "   <mq:username>@1@</mq:username>\n" +
             "</mq:AuthenticateQuery>\n",
            MBQ_GetCDInfoFromCDIndexId =
             "<mq:GetCDInfo>\n" +
             "   <mq:depth>@DEPTH@</mq:depth>\n" +
             "   <mm:cdindexid>@1@</mm:cdindexid>\n" +
             "</mq:GetCDInfo>\n",
            MBQ_TrackInfoFromTRMId =
             "<mq:TrackInfoFromTRMId>\n" +
             "   <mm:trmid>@1@</mm:trmid>\n" +
             "   <mq:artistName>@2@</mq:artistName>\n" +
             "   <mq:albumName>@3@</mq:albumName>\n" +
             "   <mq:trackName>@4@</mq:trackName>\n" +
             "   <mm:trackNum>@5@</mm:trackNum>\n" +
             "   <mm:duration>@6@</mm:duration>\n" +
             "</mq:TrackInfoFromTRMId>\n",
            MBQ_QuickTrackInfoFromTrackId =
             "<mq:QuickTrackInfoFromTrackId>\n" +
             "   <mm:trackid>@1@</mm:trackid>\n" +
             "   <mm:albumid>@2@</mm:albumid>\n" +
             "</mq:QuickTrackInfoFromTrackId>\n",
            MBQ_FindArtistByName =
             "<mq:FindArtist>\n" +
             "   <mq:depth>@DEPTH@</mq:depth>\n" +
             "   <mq:artistName>@1@</mq:artistName>\n" +
             "   <mq:maxItems>@MAX_ITEMS@</mq:maxItems>\n" +
             "</mq:FindArtist>\n",
            MBQ_FindAlbumByName =
             "<mq:FindAlbum>\n" +
             "   <mq:depth>@DEPTH@</mq:depth>\n" +
             "   <mq:maxItems>@MAX_ITEMS@</mq:maxItems>\n" +
             "   <mq:albumName>@1@</mq:albumName>\n" +
             "</mq:FindAlbum>\n",
            MBQ_FindTrackByName =
             "<mq:FindTrack>\n" +
             "   <mq:depth>@DEPTH@</mq:depth>\n" +
             "   <mq:maxItems>@MAX_ITEMS@</mq:maxItems>\n" +
             "   <mq:trackName>@1@</mq:trackName>\n" +
             "</mq:FindTrack>\n",
            MBQ_FindDistinctTRMId =
             "<mq:FindDistinctTRMID>\n" +
             "   <mq:depth>@DEPTH@</mq:depth>\n" +
             "   <mq:artistName>@1@</mq:artistName>\n" +
             "   <mq:trackName>@2@</mq:trackName>\n" +
             "</mq:FindDistinctTRMID>\n",
            MBQ_GetArtistById =
             "http://@URL@/mm-2.1/artist/@1@/@DEPTH@",
            MBQ_GetAlbumById =
             "http://@URL@/mm-2.1/album/@1@/@DEPTH@",
            MBQ_GetTrackById =
             "http://@URL@/mm-2.1/track/@1@/@DEPTH@",
            MBQ_GetTrackByTRMId =
             "http://@URL@/mm-2.1/trmid/@1@/@DEPTH@",
            MBQ_SubmitTrackTRMId =
             "<mq:SubmitTRMList>\n" +
             " <mm:trmidList>\n" +
             "  <rdf:Bag>\n" +
             "   <rdf:li>\n" +
             "    <mq:trmTrackPair>\n" +
             "     <mm:trackid>@1@</mm:trackid>\n" +
             "     <mm:trmid>@2@</mm:trmid>\n" +
             "    </mq:trmTrackPair>\n" +
             "   </rdf:li>\n" +
             "  </rdf:Bag>\n" +
             " </mm:trmidList>\n" +
             " <mq:sessionId>@SESSID@</mq:sessionId>\n" +
             " <mq:sessionKey>@SESSKEY@</mq:sessionKey>\n" +
             " <mq:clientVersion>@CLIENTVER@</mq:clientVersion>\n" +
             "</mq:SubmitTRMList>\n",
            MBQ_FileInfoLookup =
             "<mq:FileInfoLookup>\n" +
             "   <mm:trmid>@1@</mm:trmid>\n" +
             "   <mq:artistName>@2@</mq:artistName>\n" +
             "   <mq:albumName>@3@</mq:albumName>\n" +
             "   <mq:trackName>@4@</mq:trackName>\n" +
             "   <mm:trackNum>@5@</mm:trackNum>\n" +
             "   <mm:duration>@6@</mm:duration>\n" +
             "   <mq:fileName>@7@</mq:fileName>\n" +
             "   <mm:artistid>@8@</mm:artistid>\n" +
             "   <mm:albumid>@9@</mm:albumid>\n" +
             "   <mm:trackid>@10@</mm:trackid>\n" +
             "   <mq:maxItems>@MAX_ITEMS@</mq:maxItems>\n" +
             "</mq:FileInfoLookup>\n";
		 #endregion
		
		#region Constructors
		public Rdf()
		{
		}
		#endregion
		
		#region Public Properties
		public string AmazonCoverArtUrlPrefix {get {return this.AMAZON_COVER_ART_URL_PREFIX;}}
		public string IdVariousArtist {get {return this.MBI_VARIOUS_ARTIST_ID;}}
		public string SelectBack {get {return this.MBS_Back;}}
		public string SelectRewind {get {return this.MBS_Rewind;}}
		public string SelectAlbum {get {return this.MBS_SelectAlbum;}}
		public string SelectArtist {get {return this.MBS_SelectArtist;}}
		public string SelectCDIndexId {get {return this.MBS_SelectCdindexid;}}
		public string SelectLookupResult {get {return this.MBS_SelectLookupResult;}}
		public string SelectLookupResultAlbum {get {return this.MBS_SelectLookupResultAlbum;}}
		public string SelectLookupResultArtist {get {return this.MBS_SelectLookupResultArtist;}}
		public string SelectLookupResultTrack {get {return this.MBS_SelectLookupResultTrack;}}
		public string SelectReleaseDate {get {return this.MBS_SelectReleaseDate;}}
		public string SelectTrack {get {return this.MBS_SelectTrack;}}
		public string SelectTrackAlbum {get {return this.MBS_SelectTrackAlbum;}}
		public string SelectTrackArtist {get {return this.MBS_SelectTrackArtist;}}
		public string SelectTrmId {get {return this.MBS_SelectTrmid;}}
		public string ExpressionGetStatus {get {return this.MBE_GetStatus;}}
		public string ExpressionGetNumberArtists {get {return this.MBE_GetNumArtists;}}
		public string ExpressionGetNumberAlbums {get {return this.MBE_GetNumAlbums;}}
		public string ExpressionGetNumberTracks {get {return this.MBE_GetNumTracks;}}
		public string ExpressionGetNumberTrmIds {get {return this.MBE_GetNumTrmids;}}
		public string ExpressionGetNumberLookupResults {get {return this.MBE_GetNumLookupResults;}}
		public string ExpressionArtistGetArtistName {get {return this.MBE_ArtistGetArtistName;}}
		public string ExpressionArtistGetArtistSortName {get {return this.MBE_ArtistGetArtistSortName;}}
		public string ExpressionArtistGetArtistId {get {return this.MBE_ArtistGetArtistId;}}
		public string ExpressionArtistGetAlbumName {get {return this.MBE_ArtistGetAlbumName;}}
		public string ExpressionArtistGetAlbumId {get {return this.MBE_ArtistGetAlbumId;}}
		public string ExpressionAlbumGetAlbumName {get {return this.MBE_AlbumGetAlbumName;}}
		public string ExpressionAlbumGetAlbumId {get {return this.MBE_AlbumGetAlbumId;}}
		public string ExpressionAlbumGetAlbumStatus {get {return this.MBE_AlbumGetAlbumStatus;}}
		public string ExpressionAlbumGetAlbumType {get {return this.MBE_AlbumGetAlbumType;}}
		public string ExpressionAlbumGetAmazonAsin {get {return this.MBE_AlbumGetAmazonAsin;}}
		public string ExpressionAlbumGetAmazonCoverArtUrl {get {return this.MBE_AlbumGetAmazonCoverartURL;}}
		public string ExpressionAlbumGetNumberCDIndexIds {get {return this.MBE_AlbumGetNumCdindexIds;}}
		public string ExpressionAlbumGetNumberReleaseDates {get {return this.MBE_AlbumGetNumReleaseDates;}}
		public string ExpressionAlbumGetAlbumArtistId {get {return this.MBE_AlbumGetAlbumArtistId;}}
		public string ExpressionAlbumGetNumberTracks {get {return this.MBE_AlbumGetNumTracks;}}
		public string ExpressionAlbumGetTrackId {get {return this.MBE_AlbumGetTrackId;}}
		public string ExpressionAlbumGetTrackList {get {return this.MBE_AlbumGetTrackList;}}
		public string ExpressionAlbumGetTrackNumber {get {return this.MBE_AlbumGetTrackNum;}}
		public string ExpressionAlbumGetTrackName {get {return this.MBE_AlbumGetTrackName;}}
		public string ExpressionAlbumGetTrackDuration {get {return this.MBE_AlbumGetTrackDuration;}}
		public string ExpressionAlbumGetArtistName {get {return this.MBE_AlbumGetArtistName;}}
		public string ExpressionAlbumGetArtistSortName {get {return this.MBE_AlbumGetArtistSortName;}}
		public string ExpressionAlbumGetArtistId {get {return this.MBE_AlbumGetArtistId;}}
		public string ExpressionTrackGetTrackName {get {return this.MBE_TrackGetTrackName;}}
		public string ExpressionTrackGetTrackId {get {return this.MBE_TrackGetTrackId;}}
		public string ExpressionTrackGetTrackNumber {get {return this.MBE_TrackGetTrackNum;}}
		public string ExpressionTrackGetTrackDuration {get {return this.MBE_TrackGetTrackDuration;}}
		public string ExpressionTrackGetArtistName {get {return this.MBE_TrackGetArtistName;}}
		public string ExpressionTrackGetArtistSortName {get {return this.MBE_TrackGetArtistSortName;}}
		public string ExpressionTrackGetArtistId {get {return this.MBE_TrackGetArtistId;}}
		public string ExpressionQuickGetArtistName {get {return this.MBE_QuickGetArtistName;}}
		public string ExpressionQuickGetArtistSortName {get {return this.MBE_QuickGetArtistSortName;}}
		public string ExpressionQuickGetArtistId {get {return this.MBE_QuickGetArtistId;}}
		public string ExpressionQuickGetAlbumName {get {return this.MBE_QuickGetAlbumName;}}
		public string ExpressionQuickGetTrackName {get {return this.MBE_QuickGetTrackName;}}
		public string ExpressionQuickGetTrackNumber {get {return this.MBE_QuickGetTrackNum;}}
		public string ExpressionQuickGetTrackId {get {return this.MBE_QuickGetTrackId;}}
		public string ExpressionQuickGetTrackDuration {get {return this.MBE_QuickGetTrackDuration;}}
		public string ExpressionReleaseGetDate {get {return this.MBE_ReleaseGetDate;}}
		public string ExpressionReleaseGetCountry {get {return this.MBE_ReleaseGetCountry;}}
		public string ExpressionLookupGetType {get {return this.MBE_LookupGetType;}}
		public string ExpressionLookupGetRelevance {get {return this.MBE_LookupGetRelevance;}}
		public string ExpressionLookupGetArtistId {get {return this.MBE_LookupGetArtistId;}}
		public string ExpressionLookupGetAlbumId {get {return this.MBE_LookupGetAlbumId;}}
		public string ExpressionLookupGetAlbumArtistId {get {return this.MBE_LookupGetAlbumArtistId;}}
		public string ExpressionLookupGetTrackId {get {return this.MBE_LookupGetTrackId;}}
		public string ExpressionLookupGetTrackArtistId {get {return this.MBE_LookupGetTrackArtistId;}}
		public string ExpressionTocGetCDIndexId {get {return this.MBE_TOCGetCDIndexId;}}
		public string ExpressionTocGetFirstTrack {get {return this.MBE_TOCGetFirstTrack;}}
		public string ExpressionTocGetLastTrack {get {return this.MBE_TOCGetLastTrack;}}
		public string ExpressionTocGetTrackSectorOffset {get {return this.MBE_TOCGetTrackSectorOffset;}}
		public string ExpressionTocGetTrackNumberSectors {get {return this.MBE_TOCGetTrackNumSectors;}}
		public string ExpressionAuthGetSessionId {get {return this.MBE_AuthGetSessionId;}}
		public string ExpressionAuthGetChallenge {get {return this.MBE_AuthGetChallenge;}}
		public string QueryGetCDInfo {get {return this.MBQ_GetCDInfo;}}
		public string QueryGetCDToc {get {return this.MBQ_GetCDTOC;}}
		public string QueryAssociateCD {get {return this.MBQ_AssociateCD;}}
		public string QueryAuthenticate {get {return this.MBQ_Authenticate;}}
		public string QueryGetCDInfoFromCDIndexId {get {return this.MBQ_GetCDInfoFromCDIndexId;}}
		public string QueryTrackInfoFromTrmId {get {return this.MBQ_TrackInfoFromTRMId;}}
		public string QueryQuickTrackInfoFromTrackId {get {return this.MBQ_QuickTrackInfoFromTrackId;}}
		public string QueryFindArtistByName {get {return this.MBQ_FindArtistByName;}}
		public string QueryFindAlbumByName {get {return this.MBQ_FindAlbumByName;}}
		public string QueryFindTrackByName {get {return this.MBQ_FindTrackByName;}}
		public string QueryFindDistinctTrmId {get {return this.MBQ_FindDistinctTRMId;}}
		public string QueryGetArtistById {get {return this.MBQ_GetArtistById;}}
		public string QueryGetAlbumById {get {return this.MBQ_GetAlbumById;}}
		public string QueryGetTrackById {get {return this.MBQ_GetTrackById;}}
		public string QueryGetTrackByTrmId {get {return this.MBQ_GetTrackByTRMId;}}
		public string QuerySubmitTrackTrmId {get {return this.MBQ_SubmitTrackTRMId;}}
		public string QueryFileInfoLookup {get {return this.MBQ_FileInfoLookup;}}
		#endregion 
		
		#region Old RDF Query Constants
		/*
		public static readonly string MBI_VARIOUS_ARTIST_ID = "89ad4ac3-39f7-470e-963a-56509c546377";
		public static readonly string MBS_Rewind = "[REWIND]";
		public static readonly string MBS_Back = "[BACK]";
		public static readonly string MBS_SelectArtist = "http://musicbrainz.org/mm/mm-2.1#artistList []";
		public static readonly string MBS_SelectAlbum = "http://musicbrainz.org/mm/mm-2.1#albumList []";
		public static readonly string MBS_SelectTrack =	"http://musicbrainz.org/mm/mm-2.1#trackList []";
		public static readonly string MBS_SelectTrackArtist = "http://purl.org/dc/elements/1.1/creator";
		public static readonly string MBS_SelectTrackAlbum = "http://musicbrainz.org/mm/mq-1.1#album";
		public static readonly string MBS_SelectTrmid = "http://musicbrainz.org/mm/mm-2.1#trmidList []";
		public static readonly string MBS_SelectCdindexid = "http://musicbrainz.org/mm/mm-2.1#cdindexidList []";
		public static readonly string MBS_SelectReleaseDate = "http://musicbrainz.org/mm/mm-2.1#releaseDateList []";
		public static readonly string MBS_SelectLookupResult = "http://musicbrainz.org/mm/mq-1.1#lookupResultList []";
		public static readonly string MBS_SelectLookupResultArtist = "http://musicbrainz.org/mm/mq-1.1#artist";
		public static readonly string MBS_SelectLookupResultAlbum = "http://musicbrainz.org/mm/mq-1.1#album";
		public static readonly string MBS_SelectLookupResultTrack = "http://musicbrainz.org/mm/mq-1.1#track";
		public static readonly string MBE_GetStatus = "http://musicbrainz.org/mm/mq-1.1#status";
		public static readonly string MBE_GetNumArtists = "http://musicbrainz.org/mm/mm-2.1#artistList [COUNT]";
		public static readonly string MBE_GetNumAlbums = "http://musicbrainz.org/mm/mm-2.1#albumList [COUNT]";
		public static readonly string MBE_GetNumTracks = "http://musicbrainz.org/mm/mm-2.1#trackList [COUNT]";
		public static readonly string MBE_GetNumTrmids = "http://musicbrainz.org/mm/mm-2.1#trmidList [COUNT]";
		public static readonly string MBE_GetNumLookupResults = "http://musicbrainz.org/mm/mm-2.1#lookupResultList [COUNT]";
		public static readonly string MBE_ArtistGetArtistName = "http://purl.org/dc/elements/1.1/title";
		public static readonly string MBE_ArtistGetArtistSortName = "http://musicbrainz.org/mm/mm-2.1#sortName";
		public static readonly string MBE_ArtistGetArtistId = string.Empty;
		public static readonly string MBE_ArtistGetAlbumName = "http://musicbrainz.org/mm/mm-2.1#albumList [] http://purl.org/dc/elements/1.1/title";
		public static readonly string MBE_ArtistGetAlbumId = "http://musicbrainz.org/mm/mm-2.1#albumList []";
		public static readonly string MBE_AlbumGetAlbumName = "http://purl.org/dc/elements/1.1/title";
		public static readonly string MBE_AlbumGetAlbumId = string.Empty;
		public static readonly string MBE_AlbumGetAlbumStatus = "http://musicbrainz.org/mm/mm-2.1#releaseStatus";
		public static readonly string MBE_AlbumGetAlbumType = "http://musicbrainz.org/mm/mm-2.1#releaseType";
		public static readonly string MBE_AlbumGetAmazonAsin = "http://www.amazon.com/gp/aws/landing.html#Asin";
		public static readonly string MBE_AlbumGetAmazonCoverartURL = "http://musicbrainz.org/mm/mm-2.1#coverart";
		public static readonly string MBE_AlbumGetNumCdindexIds = "http://musicbrainz.org/mm/mm-2.1#cdindexidList [COUNT]";
		public static readonly string MBE_AlbumGetNumReleaseDates = "http://musicbrainz.org/mm/mm-2.1#releaseDateList [COUNT]";
		public static readonly string MBE_AlbumGetAlbumArtistId = "http://purl.org/dc/elements/1.1/creator";
		public static readonly string MBE_AlbumGetNumTracks = "http://musicbrainz.org/mm/mm-2.1#trackList [COUNT]";
		public static readonly string MBE_AlbumGetTrackId = "http://musicbrainz.org/mm/mm-2.1#trackList [] ";
		public static readonly string MBE_AlbumGetTrackList = "http://musicbrainz.org/mm/mm-2.1#trackList";
		public static readonly string MBE_AlbumGetTrackNum = "http://musicbrainz.org/mm/mm-2.1#trackList [?] http://musicbrainz.org/mm/mm-2.1#trackNum";
		public static readonly string MBE_AlbumGetTrackName = "http://musicbrainz.org/mm/mm-2.1#trackList [] http://purl.org/dc/elements/1.1/title";
		public static readonly string MBE_AlbumGetTrackDuration = "http://musicbrainz.org/mm/mm-2.1#trackList [] http://musicbrainz.org/mm/mm-2.1#duration";
		public static readonly string MBE_AlbumGetArtistName = "http://musicbrainz.org/mm/mm-2.1#trackList [] http://purl.org/dc/elements/1.1/creator http://purl.org/dc/elements/1.1/title";
		public static readonly string MBE_AlbumGetArtistSortName = "http://musicbrainz.org/mm/mm-2.1#trackList [] http://purl.org/dc/elements/1.1/creator http://musicbrainz.org/mm/mm-2.1#sortName";
		public static readonly string MBE_AlbumGetArtistId = "http://musicbrainz.org/mm/mm-2.1#trackList [] http://purl.org/dc/elements/1.1/creator";
		public static readonly string MBE_TrackGetTrackName = "http://purl.org/dc/elements/1.1/title";
		public static readonly string MBE_TrackGetTrackId = string.Empty;
		public static readonly string MBE_TrackGetTrackNum = "http://musicbrainz.org/mm/mm-2.1#trackNum";
		public static readonly string MBE_TrackGetTrackDuration = "http://musicbrainz.org/mm/mm-2.1#duration";
		public static readonly string MBE_TrackGetArtistName = "http://purl.org/dc/elements/1.1/creator http://purl.org/dc/elements/1.1/title";
		public static readonly string MBE_TrackGetArtistSortName = "http://purl.org/dc/elements/1.1/creator http://musicbrainz.org/mm/mm-2.1#sortName";
		public static readonly string MBE_TrackGetArtistId = "http://purl.org/dc/elements/1.1/creator";
		public static readonly string MBE_QuickGetArtistName = "http://musicbrainz.org/mm/mq-1.1#artistName";
		public static readonly string MBE_QuickGetArtistSortName = "http://musicbrainz.org/mm/mm-2.1#sortName";
		public static readonly string MBE_QuickGetArtistId = "http://musicbrainz.org/mm/mm-2.1#artistid";
		public static readonly string MBE_QuickGetAlbumName = "http://musicbrainz.org/mm/mq-1.1#albumName";
		public static readonly string MBE_QuickGetTrackName = "http://musicbrainz.org/mm/mq-1.1#trackName";
		public static readonly string MBE_QuickGetTrackNum = "http://musicbrainz.org/mm/mm-2.1#trackNum";
		public static readonly string MBE_QuickGetTrackId = "http://musicbrainz.org/mm/mm-2.1#trackid";
		public static readonly string MBE_QuickGetTrackDuration = "http://musicbrainz.org/mm/mm-2.1#duration";
		public static readonly string MBE_ReleaseGetDate = "http://purl.org/dc/elements/1.1/date";
		public static readonly string MBE_ReleaseGetCountry = "http://musicbrainz.org/mm/mm-2.1#country";
		public static readonly string MBE_LookupGetType = "http://www.w3.org/1999/02/22-rdf-syntax-ns#type";
		public static readonly string MBE_LookupGetRelevance = "http://musicbrainz.org/mm/mq-1.1#relevance";
		public static readonly string MBE_LookupGetArtistId = "http://musicbrainz.org/mm/mq-1.1#artist";
		public static readonly string MBE_LookupGetAlbumId = "http://musicbrainz.org/mm/mq-1.1#album";
		public static readonly string MBE_LookupGetAlbumArtistId = "http://musicbrainz.org/mm/mq-1.1#album http://purl.org/dc/elements/1.1/creator";
		public static readonly string MBE_LookupGetTrackId = "http://musicbrainz.org/mm/mq-1.1#track";
		public static readonly string MBE_LookupGetTrackArtistId = "http://musicbrainz.org/mm/mq-1.1#track http://purl.org/dc/elements/1.1/creator";
		public static readonly string MBE_TOCGetCDIndexId =	"http://musicbrainz.org/mm/mm-2.1#cdindexid";
		public static readonly string MBE_TOCGetFirstTrack = "http://musicbrainz.org/mm/mm-2.1#firstTrack";
		public static readonly string MBE_TOCGetLastTrack = "http://musicbrainz.org/mm/mm-2.1#lastTrack";
		public static readonly string MBE_TOCGetTrackSectorOffset = "http://musicbrainz.org/mm/mm-2.1#toc [] http://musicbrainz.org/mm/mm-2.1#sectorOffset";
		public static readonly string MBE_TOCGetTrackNumSectors = "http://musicbrainz.org/mm/mm-2.1#toc [] http://musicbrainz.org/mm/mm-2.1#numSectors";
		public static readonly string MBE_AuthGetSessionId = "http://musicbrainz.org/mm/mq-1.1#sessionId";
		public static readonly string MBE_AuthGetChallenge = "http://musicbrainz.org/mm/mq-1.1#authChallenge";
		public static readonly string MBQ_GetCDInfo = "@CDINFO@";
		public static readonly string MBQ_GetCDTOC = "@LOCALCDINFO@";
		public static readonly string MBQ_AssociateCD = "@CDINFOASSOCIATECD@";
		public static readonly string MBQ_Authenticate = 
			"<mq:AuthenticateQuery>\n" +
			"   <mq:username>@1@</mq:username>\n" +
			"</mq:AuthenticateQuery>\n";
		public static readonly string MBQ_GetCDInfoFromCDIndexId = 
			"<mq:GetCDInfo>\n" +
			"   <mq:depth>@DEPTH@</mq:depth>\n" +
			"   <mm:cdindexid>@1@</mm:cdindexid>\n" +
			"</mq:GetCDInfo>\n";
		public static readonly string MBQ_TrackInfoFromTRMId = 
			"<mq:TrackInfoFromTRMId>\n" +
			"   <mm:trmid>@1@</mm:trmid>\n" +
			"   <mq:artistName>@2@</mq:artistName>\n" +
			"   <mq:albumName>@3@</mq:albumName>\n" +
			"   <mq:trackName>@4@</mq:trackName>\n" +
			"   <mm:trackNum>@5@</mm:trackNum>\n" +
			"   <mm:duration>@6@</mm:duration>\n" +
			"</mq:TrackInfoFromTRMId>\n";
		public static readonly string MBQ_QuickTrackInfoFromTrackId =
			"<mq:QuickTrackInfoFromTrackId>\n" +
			"   <mm:trackid>@1@</mm:trackid>\n" +
			"   <mm:albumid>@2@</mm:albumid>\n" +
			"</mq:QuickTrackInfoFromTrackId>\n";
		public static readonly string MBQ_FindArtistByName =
			"<mq:FindArtist>\n" +
			"   <mq:depth>@DEPTH@</mq:depth>\n" +
			"   <mq:artistName>@1@</mq:artistName>\n" +
			"   <mq:maxItems>@MAX_ITEMS@</mq:maxItems>\n" +
			"</mq:FindArtist>\n";
		public static readonly string MBQ_FindAlbumByName =
			"<mq:FindAlbum>\n" +
			"   <mq:depth>@DEPTH@</mq:depth>\n" +
			"   <mq:maxItems>@MAX_ITEMS@</mq:maxItems>\n" +
			"   <mq:albumName>@1@</mq:albumName>\n" +
			"</mq:FindAlbum>\n";
		public static readonly string MBQ_FindTrackByName =
			"<mq:FindTrack>\n" +
			"   <mq:depth>@DEPTH@</mq:depth>\n" +
			"   <mq:maxItems>@MAX_ITEMS@</mq:maxItems>\n" +
			"   <mq:trackName>@1@</mq:trackName>\n" +
			"</mq:FindTrack>\n";
		public static readonly string MBQ_FindDistinctTRMId =
			"<mq:FindDistinctTRMID>\n" +
			"   <mq:depth>@DEPTH@</mq:depth>\n" +
			"   <mq:artistName>@1@</mq:artistName>\n" +
			"   <mq:trackName>@2@</mq:trackName>\n" +
			"</mq:FindDistinctTRMID>\n";
		public static readonly string MBQ_GetArtistById = "http://@URL@/mm-2.1/artist/@1@/@DEPTH@";
		public static readonly string MBQ_GetAlbumById = "http://@URL@/mm-2.1/album/@1@/@DEPTH@";
		public static readonly string MBQ_GetTrackById = "http://@URL@/mm-2.1/track/@1@/@DEPTH@";
		public static readonly string MBQ_GetTrackByTRMId = "http://@URL@/mm-2.1/trmid/@1@/@DEPTH@";
		public static readonly string MBQ_SubmitTrackTRMId =
			"<mq:SubmitTRMList>\n" +
			" <mm:trmidList>\n" +
			"  <rdf:Bag>\n" +
			"   <rdf:li>\n" +
			"    <mq:trmTrackPair>\n" +
			"     <mm:trackid>@1@</mm:trackid>\n" +
			"     <mm:trmid>@2@</mm:trmid>\n" +
			"    </mq:trmTrackPair>\n" +
			"   </rdf:li>\n" +
			"  </rdf:Bag>\n" +
			" </mm:trmidList>\n" +
			" <mq:sessionId>@SESSID@</mq:sessionId>\n" +
			" <mq:sessionKey>@SESSKEY@</mq:sessionKey>\n" +
			" <mq:clientVersion>@CLIENTVER@</mq:clientVersion>\n" +
			"</mq:SubmitTRMList>\n";
		public static readonly string MBQ_FileInfoLookup =
			"<mq:FileInfoLookup>\n" +
			"   <mm:trmid>@1@</mm:trmid>\n" +
			"   <mq:artistName>@2@</mq:artistName>\n" +
			"   <mq:albumName>@3@</mq:albumName>\n" +
			"   <mq:trackName>@4@</mq:trackName>\n" +
			"   <mm:trackNum>@5@</mm:trackNum>\n" +
			"   <mm:duration>@6@</mm:duration>\n" +
			"   <mq:fileName>@7@</mq:fileName>\n" +
			"   <mm:artistid>@8@</mm:artistid>\n" +
			"   <mm:albumid>@9@</mm:albumid>\n" +
			"   <mm:trackid>@10@</mm:trackid>\n" +
			"   <mq:maxItems>@MAX_ITEMS@</mq:maxItems>\n" +
			"</mq:FileInfoLookup>\n";
		*/
		#endregion

		#region Old MusicBrainz Query Constants
		/*
				// ========= Query Constants ==========

		public static readonly String
		  MBI_VARIOUS_ARTIST_ID =
			"89ad4ac3-39f7-470e-963a-56509c546377",
		  MBS_Rewind =
			"[REWIND]",
		  MBS_Back =
			"[BACK]",
		  MBS_SelectArtist =
			"http://musicbrainz.org/mm/mm-2.1#artistList []",
		  MBS_SelectAlbum =
			"http://musicbrainz.org/mm/mm-2.1#albumList []",
		  MBS_SelectTrack =
			"http://musicbrainz.org/mm/mm-2.1#trackList []",
		  MBS_SelectTrackArtist =
			"http://purl.org/dc/elements/1.1/creator",
		  MBS_SelectTrackAlbum =
			"http://musicbrainz.org/mm/mq-1.1#album",
		  MBS_SelectTrmid =
			"http://musicbrainz.org/mm/mm-2.1#trmidList []",
		  MBS_SelectCdindexid =
			"http://musicbrainz.org/mm/mm-2.1#cdindexidList []",
		  MBS_SelectReleaseDate =
			"http://musicbrainz.org/mm/mm-2.1#releaseDateList []",
		  MBS_SelectLookupResult =
			"http://musicbrainz.org/mm/mq-1.1#lookupResultList []",
		  MBS_SelectLookupResultArtist =
			"http://musicbrainz.org/mm/mq-1.1#artist",
		  MBS_SelectLookupResultAlbum =
			"http://musicbrainz.org/mm/mq-1.1#album",
		  MBS_SelectLookupResultTrack =
			"http://musicbrainz.org/mm/mq-1.1#track",
		  MBE_GetStatus =
			"http://musicbrainz.org/mm/mq-1.1#status",
		  MBE_GetNumArtists =
			"http://musicbrainz.org/mm/mm-2.1#artistList [COUNT]",
		  MBE_GetNumAlbums =
			"http://musicbrainz.org/mm/mm-2.1#albumList [COUNT]",
		  MBE_GetNumTracks =
			"http://musicbrainz.org/mm/mm-2.1#trackList [COUNT]",
		  MBE_GetNumTrmids =
			"http://musicbrainz.org/mm/mm-2.1#trmidList [COUNT]",
		  MBE_GetNumLookupResults =
			"http://musicbrainz.org/mm/mm-2.1#lookupResultList [COUNT]",
		  MBE_ArtistGetArtistName =
			"http://purl.org/dc/elements/1.1/title",
		  MBE_ArtistGetArtistSortName =
			"http://musicbrainz.org/mm/mm-2.1#sortName",
		  MBE_ArtistGetArtistId =
			"",
		  MBE_ArtistGetAlbumName =
			"http://musicbrainz.org/mm/mm-2.1#albumList [] http://purl.org/dc/elements/1.1/title",
		  MBE_ArtistGetAlbumId =
			"http://musicbrainz.org/mm/mm-2.1#albumList []",
		  MBE_AlbumGetAlbumName =
			"http://purl.org/dc/elements/1.1/title",
		  MBE_AlbumGetAlbumId =
			"",
		  MBE_AlbumGetAlbumStatus =
			"http://musicbrainz.org/mm/mm-2.1#releaseStatus",
		  MBE_AlbumGetAlbumType =
			"http://musicbrainz.org/mm/mm-2.1#releaseType",
		  MBE_AlbumGetAmazonAsin =
			"http://www.amazon.com/gp/aws/landing.html#Asin",
		  MBE_AlbumGetAmazonCoverartURL =
			"http://musicbrainz.org/mm/mm-2.1#coverart",
		  MBE_AlbumGetNumCdindexIds =
			"http://musicbrainz.org/mm/mm-2.1#cdindexidList [COUNT]",
		  MBE_AlbumGetNumReleaseDates =
			"http://musicbrainz.org/mm/mm-2.1#releaseDateList [COUNT]",
		  MBE_AlbumGetAlbumArtistId =
			"http://purl.org/dc/elements/1.1/creator",
		  MBE_AlbumGetNumTracks =
			"http://musicbrainz.org/mm/mm-2.1#trackList [COUNT]",
		  MBE_AlbumGetTrackId =
			"http://musicbrainz.org/mm/mm-2.1#trackList [] ",
		  MBE_AlbumGetTrackList =
			"http://musicbrainz.org/mm/mm-2.1#trackList",
		  MBE_AlbumGetTrackNum =
			"http://musicbrainz.org/mm/mm-2.1#trackList [?] http://musicbrainz.org/mm/mm-2.1#trackNum",
		  MBE_AlbumGetTrackName =
			"http://musicbrainz.org/mm/mm-2.1#trackList [] http://purl.org/dc/elements/1.1/title",
		  MBE_AlbumGetTrackDuration =
			"http://musicbrainz.org/mm/mm-2.1#trackList [] http://musicbrainz.org/mm/mm-2.1#duration",
		  MBE_AlbumGetArtistName =
			"http://musicbrainz.org/mm/mm-2.1#trackList [] http://purl.org/dc/elements/1.1/creator http://purl.org/dc/elements/1.1/title",
		  MBE_AlbumGetArtistSortName =
			"http://musicbrainz.org/mm/mm-2.1#trackList [] http://purl.org/dc/elements/1.1/creator http://musicbrainz.org/mm/mm-2.1#sortName",
		  MBE_AlbumGetArtistId =
			"http://musicbrainz.org/mm/mm-2.1#trackList [] http://purl.org/dc/elements/1.1/creator",
		  MBE_TrackGetTrackName =
			"http://purl.org/dc/elements/1.1/title",
		  MBE_TrackGetTrackId =
			"",
		  MBE_TrackGetTrackNum =
			"http://musicbrainz.org/mm/mm-2.1#trackNum",
		  MBE_TrackGetTrackDuration =
			"http://musicbrainz.org/mm/mm-2.1#duration",
		  MBE_TrackGetArtistName =
			"http://purl.org/dc/elements/1.1/creator http://purl.org/dc/elements/1.1/title",
		  MBE_TrackGetArtistSortName =
			"http://purl.org/dc/elements/1.1/creator http://musicbrainz.org/mm/mm-2.1#sortName",
		  MBE_TrackGetArtistId =
			"http://purl.org/dc/elements/1.1/creator",
		  MBE_QuickGetArtistName =
			"http://musicbrainz.org/mm/mq-1.1#artistName",
		  MBE_QuickGetArtistSortName =
			"http://musicbrainz.org/mm/mm-2.1#sortName",
		  MBE_QuickGetArtistId =
			"http://musicbrainz.org/mm/mm-2.1#artistid",
		  MBE_QuickGetAlbumName =
			"http://musicbrainz.org/mm/mq-1.1#albumName",
		  MBE_QuickGetTrackName =
			"http://musicbrainz.org/mm/mq-1.1#trackName",
		  MBE_QuickGetTrackNum =
			"http://musicbrainz.org/mm/mm-2.1#trackNum",
		  MBE_QuickGetTrackId =
			"http://musicbrainz.org/mm/mm-2.1#trackid",
		  MBE_QuickGetTrackDuration =
			"http://musicbrainz.org/mm/mm-2.1#duration",
		  MBE_ReleaseGetDate =
			"http://purl.org/dc/elements/1.1/date",
		  MBE_ReleaseGetCountry =
			"http://musicbrainz.org/mm/mm-2.1#country",
		  MBE_LookupGetType =
			"http://www.w3.org/1999/02/22-rdf-syntax-ns#type",
		  MBE_LookupGetRelevance =
			"http://musicbrainz.org/mm/mq-1.1#relevance",
		  MBE_LookupGetArtistId =
			"http://musicbrainz.org/mm/mq-1.1#artist",
		  MBE_LookupGetAlbumId =
			"http://musicbrainz.org/mm/mq-1.1#album",
		  MBE_LookupGetAlbumArtistId =
			"http://musicbrainz.org/mm/mq-1.1#album " +
			"http://purl.org/dc/elements/1.1/creator",
		  MBE_LookupGetTrackId =
			"http://musicbrainz.org/mm/mq-1.1#track",
		  MBE_LookupGetTrackArtistId =
			"http://musicbrainz.org/mm/mq-1.1#track " +
			"http://purl.org/dc/elements/1.1/creator",
		  MBE_TOCGetCDIndexId =
			"http://musicbrainz.org/mm/mm-2.1#cdindexid",
		  MBE_TOCGetFirstTrack =
			"http://musicbrainz.org/mm/mm-2.1#firstTrack",
		  MBE_TOCGetLastTrack =
			"http://musicbrainz.org/mm/mm-2.1#lastTrack",
		  MBE_TOCGetTrackSectorOffset =
			"http://musicbrainz.org/mm/mm-2.1#toc [] http://musicbrainz.org/mm/mm-2.1#sectorOffset",
		  MBE_TOCGetTrackNumSectors =
			"http://musicbrainz.org/mm/mm-2.1#toc [] http://musicbrainz.org/mm/mm-2.1#numSectors",
		  MBE_AuthGetSessionId =
			"http://musicbrainz.org/mm/mq-1.1#sessionId",
		  MBE_AuthGetChallenge =
			"http://musicbrainz.org/mm/mq-1.1#authChallenge",
		  MBQ_GetCDInfo =
			"@CDINFO@",
		  MBQ_GetCDTOC =
			"@LOCALCDINFO@",
		  MBQ_AssociateCD =
			"@CDINFOASSOCIATECD@",
		  MBQ_Authenticate =
			"<mq:AuthenticateQuery>\n" +
			"   <mq:username>@1@</mq:username>\n" +
			"</mq:AuthenticateQuery>\n",
		  MBQ_GetCDInfoFromCDIndexId =
			"<mq:GetCDInfo>\n" +
			"   <mq:depth>@DEPTH@</mq:depth>\n" +
			"   <mm:cdindexid>@1@</mm:cdindexid>\n" +
			"</mq:GetCDInfo>\n",
		  MBQ_TrackInfoFromTRMId =
			"<mq:TrackInfoFromTRMId>\n" +
			"   <mm:trmid>@1@</mm:trmid>\n" +
			"   <mq:artistName>@2@</mq:artistName>\n" +
			"   <mq:albumName>@3@</mq:albumName>\n" +
			"   <mq:trackName>@4@</mq:trackName>\n" +
			"   <mm:trackNum>@5@</mm:trackNum>\n" +
			"   <mm:duration>@6@</mm:duration>\n" +
			"</mq:TrackInfoFromTRMId>\n",
		  MBQ_QuickTrackInfoFromTrackId =
			"<mq:QuickTrackInfoFromTrackId>\n" +
			"   <mm:trackid>@1@</mm:trackid>\n" +
			"   <mm:albumid>@2@</mm:albumid>\n" +
			"</mq:QuickTrackInfoFromTrackId>\n",
		  MBQ_FindArtistByName =
			"<mq:FindArtist>\n" +
			"   <mq:depth>@DEPTH@</mq:depth>\n" +
			"   <mq:artistName>@1@</mq:artistName>\n" +
			"   <mq:maxItems>@MAX_ITEMS@</mq:maxItems>\n" +
			"</mq:FindArtist>\n",
		  MBQ_FindAlbumByName =
			"<mq:FindAlbum>\n" +
			"   <mq:depth>@DEPTH@</mq:depth>\n" +
			"   <mq:maxItems>@MAX_ITEMS@</mq:maxItems>\n" +
			"   <mq:albumName>@1@</mq:albumName>\n" +
			"</mq:FindAlbum>\n",
		  MBQ_FindTrackByName =
			"<mq:FindTrack>\n" +
			"   <mq:depth>@DEPTH@</mq:depth>\n" +
			"   <mq:maxItems>@MAX_ITEMS@</mq:maxItems>\n" +
			"   <mq:trackName>@1@</mq:trackName>\n" +
			"</mq:FindTrack>\n",
		  MBQ_FindDistinctTRMId =
			"<mq:FindDistinctTRMID>\n" +
			"   <mq:depth>@DEPTH@</mq:depth>\n" +
			"   <mq:artistName>@1@</mq:artistName>\n" +
			"   <mq:trackName>@2@</mq:trackName>\n" +
			"</mq:FindDistinctTRMID>\n",
		  MBQ_GetArtistById =
			"http://@URL@/mm-2.1/artist/@1@/@DEPTH@",
		  MBQ_GetAlbumById =
			"http://@URL@/mm-2.1/album/@1@/@DEPTH@",
		  MBQ_GetTrackById =
			"http://@URL@/mm-2.1/track/@1@/@DEPTH@",
		  MBQ_GetTrackByTRMId =
			"http://@URL@/mm-2.1/trmid/@1@/@DEPTH@",
		  MBQ_SubmitTrackTRMId =
			"<mq:SubmitTRMList>\n" +
			" <mm:trmidList>\n" +
			"  <rdf:Bag>\n" +
			"   <rdf:li>\n" +
			"    <mq:trmTrackPair>\n" +
			"     <mm:trackid>@1@</mm:trackid>\n" +
			"     <mm:trmid>@2@</mm:trmid>\n" +
			"    </mq:trmTrackPair>\n" +
			"   </rdf:li>\n" +
			"  </rdf:Bag>\n" +
			" </mm:trmidList>\n" +
			" <mq:sessionId>@SESSID@</mq:sessionId>\n" +
			" <mq:sessionKey>@SESSKEY@</mq:sessionKey>\n" +
			" <mq:handleVersion>@handleVER@</mq:handleVersion>\n" +
			"</mq:SubmitTRMList>\n",
		  MBQ_FileInfoLookup =
			"<mq:FileInfoLookup>\n" +
			"   <mm:trmid>@1@</mm:trmid>\n" +
			"   <mq:artistName>@2@</mq:artistName>\n" +
			"   <mq:albumName>@3@</mq:albumName>\n" +
			"   <mq:trackName>@4@</mq:trackName>\n" +
			"   <mm:trackNum>@5@</mm:trackNum>\n" +
			"   <mm:duration>@6@</mm:duration>\n" +
			"   <mq:fileName>@7@</mq:fileName>\n" +
			"   <mm:artistid>@8@</mm:artistid>\n" +
			"   <mm:albumid>@9@</mm:albumid>\n" +
			"   <mm:trackid>@10@</mm:trackid>\n" +
			"   <mq:maxItems>@MAX_ITEMS@</mq:maxItems>\n" +
			"</mq:FileInfoLookup>\n";

		 */
		#endregion
    }
}
