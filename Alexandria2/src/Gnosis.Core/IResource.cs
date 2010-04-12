using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IResource
    {
        Uri Id { get; }
        IMessage Read(IMessage request);
        IMessage Write(IMessage request);
        IMessage Execute(IMessage request);
        IMessage Delete(IMessage request);
    }
}
