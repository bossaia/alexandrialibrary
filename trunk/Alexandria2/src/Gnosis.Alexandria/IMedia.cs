using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IMedia
        : IEntity, IMutable
    {
        MediaType Type { get; }
        Uri Path { get; }

        void ChangeType(MediaType type);
        void ChangePath(Uri path);
    }
}
