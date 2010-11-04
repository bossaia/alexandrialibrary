using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Messages
{
    public interface ITargetedMessage : IMessage
    {
        Guid Target { get; }
    }
}
