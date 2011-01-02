using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IResultElement
    {
        string Message { get; set; }
        string StackTrace { get; set; }
        string InnerMessage { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
