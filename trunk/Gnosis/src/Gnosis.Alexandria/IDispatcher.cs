using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IDispatcher
    {
        void Dispatch<T>(Guid sender, T message)
            where T : IMessage;
    }
}
