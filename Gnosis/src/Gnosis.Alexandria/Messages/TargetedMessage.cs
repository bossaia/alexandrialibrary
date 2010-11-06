using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Messages.Interfaces;

namespace Gnosis.Alexandria.Messages
{
    public abstract class TargetedMessage : ITargetedMessage
    {
        protected TargetedMessage(Guid target)
        {
            _target = target;
        }

        private readonly Guid _target;

        public Guid Target
        {
            get { return _target; }
        }
    }
}
