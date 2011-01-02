using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface ITrackElement
    {
        string Identifier { get; set; }
        string Title { get; set; }
        string Creator { get; set; }
        int Number { get; set; }
        DateTime ReleaseDate { get; set; }
        string Path { get; set; }
        string MediaType { get; set; }
        TimeSpan Duration { get; set; }
    }
}
