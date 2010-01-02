using System;

namespace Telesophy.Alexandria.Core
{
    [CLSCompliant(true)]
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
