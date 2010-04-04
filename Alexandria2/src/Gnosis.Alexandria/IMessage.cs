﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
    public interface IMessage
    {
        Uri Id { get; }
        IMap<string, string> Headers { get; }
        string ContentType { get; }
        byte[] Content { get; }
    }
}
