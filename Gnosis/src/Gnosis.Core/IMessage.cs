using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IMessage
    {
        Uri Location { get; set; }
        RequestFunction Function { get; set; }
        string StatusCode { get; set; }
        IDictionary<string, string> Header { get; }
        IBody Body { get; set; }
    }
}
