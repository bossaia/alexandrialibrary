using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Messages.Interfaces;

namespace Gnosis.Alexandria.Handlers
{
    public abstract class TargetedHandler<T> : Handler<T>
        where T : ITargetedMessage
    {
        protected TargetedHandler(Guid hostId)
        {
            _hostId = hostId;
        }

        private readonly Guid _hostId;

        public override void Handle(T message)
        {
            if (message.Target == _hostId)
                HandleMessage(message);
        }
    }
}
