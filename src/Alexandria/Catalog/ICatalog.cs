#region License (MIT)
/*
Copyright (c) 2007 Dan Poage

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using Alexandria.Metadata;
using Alexandria.Persistence;

namespace Alexandria.Catalog
{
	[Record("Catalog")]
    public interface ICatalog : IRecord
    {
		[Field(2, FieldConstraint.Required)]
		string Name { get; }
    
		[Field(FieldType.Child, FieldRelationship.ManyToMany, "UserCatalog", "UserId", "CatalogId", FieldCascade.None)]
		IUser User { get; set; }
		
		[Field(FieldType.Parent, FieldRelationship.ManyToMany, "CatalogAlbum", "CatalogId", "AlbumId", FieldCascade.None)]
        IList<IAlbum> Albums { get; }
        
        [Field(FieldType.Parent, FieldRelationship.ManyToMany, "CatalogArtist", "CatalogId", "ArtistId", FieldCascade.None)]
        IList<IArtist> Artists { get; }
        
        [Field(FieldType.Parent, FieldRelationship.ManyToMany, "CatalogAudioTrack", "CatalogId", "AudioTrackId", FieldCascade.None)]
        IList<IAudioTrack> Tracks { get; }
    }
}
