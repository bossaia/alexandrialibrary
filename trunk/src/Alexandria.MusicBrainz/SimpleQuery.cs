
/***************************************************************************
 *  SimpleQuery.cs
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

using System;
using System.Security.Permissions;
using Alexandria;

namespace Alexandria.MusicBrainz
{
    public static class SimpleQuery
    {
		[EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
        public static SimpleTrack FileLookup(MusicBrainzClient client, string artistName, string albumName, string trackName, int trackNumber, int duration)
        {
			Rdf rdf = new Rdf();
        
			SimpleTrack track = null;
        
			if (client != null)
			{
				client.QueryDepth = 4;

				if (!client.Query(rdf.QueryFileInfoLookup, new string[]
					{	string.Empty, // trmid
						artistName,
						albumName,
						trackName,
						trackNumber.ToString(System.Globalization.CultureInfo.InvariantCulture),
						duration.ToString(System.Globalization.CultureInfo.InvariantCulture),
						string.Empty, // filename
						string.Empty, // artistid
						string.Empty, // albumid
						string.Empty
					}))
				{
					//"File Lookup Query unsuccessful"
					throw new AlexandriaException("File Lookup Query unsuccessful");
				}

				client.Select(rdf.SelectRewind);

				if (!client.Select(rdf.SelectLookupResult, 1))
				{
					// "Selection failed"
					throw new AlexandriaException("Selection failed");
				}
				
				track = new SimpleTrack();
				string result_type = client.GetId(client.GetResultData(rdf.ExpressionLookupGetType));
            
				switch(result_type)
				{
					case "AlbumTrackResult":
						client.Select(rdf.SelectLookupResultTrack);
						track.Title = client.GetResultData(rdf.ExpressionTrackGetTrackName);
						track.Artist = client.GetResultData(rdf.ExpressionTrackGetArtistName);
						track.Length = client.GetResultInt(rdf.ExpressionTrackGetTrackDuration);
						client.Select(rdf.SelectBack);

						client.Select(rdf.SelectLookupResultAlbum, 1);
						track.Album = client.GetResultData(rdf.ExpressionAlbumGetAlbumName);
						track.TrackCount = client.GetResultInt(rdf.ExpressionAlbumGetNumberTracks);
						track.TrackNumber = client.GetResultInt(rdf.ExpressionAlbumGetTrackNumber);
						track.Asin = client.GetResultData(rdf.ExpressionAlbumGetAmazonAsin);
						client.Select(rdf.SelectBack);
						break;
					case "AlbumResult":
						client.Select(rdf.SelectLookupResultAlbum, 1);
						track.TrackCount = client.GetResultInt(rdf.ExpressionAlbumGetNumberTracks);
						track.Album = client.GetResultData(rdf.ExpressionAlbumGetAlbumName);
						track.Asin = client.GetResultData(rdf.ExpressionAlbumGetAmazonAsin);

						string track_id = client.GetResultData(rdf.ExpressionAlbumGetTrackId, trackNumber);

						if (client.Query(rdf.QueryGetTrackById, new string[] { client.GetId(track_id) }))
						{
							client.Select(rdf.SelectTrack, 1);
							track.Title = client.GetResultData(rdf.ExpressionTrackGetTrackName);
							track.Artist = client.GetResultData(rdf.ExpressionTrackGetArtistName);
							track.TrackNumber = client.GetResultInt(rdf.ExpressionTrackGetTrackNumber);
							track.Length = client.GetResultInt(rdf.ExpressionTrackGetTrackDuration);
							client.Select(rdf.SelectBack);
						}

						client.Select(rdf.SelectBack);
						break;
					default:
						//"Invalid result type: " + result_type
						throw new AlexandriaException("Invalid result type: " + result_type);
				}
            }
            return track;
        }
    }
}
