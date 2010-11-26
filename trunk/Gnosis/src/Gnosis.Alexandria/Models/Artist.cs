using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models
{
    public class Artist : NamedDatedNational, IArtist
    {
        public Artist()
        {
            Note = string.Empty;
        }

        private Artist(long id)
        {
            Initialize(id);
        }

        public string Note { get; set; }

        public static readonly IArtist Unknown = new Artist(1L);
    }
}
