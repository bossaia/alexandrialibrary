using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IEntity
    {
        long Id { get; }
        IUser CreatedBy { get; }
        DateTime CreatedOn { get; }
    }
}
