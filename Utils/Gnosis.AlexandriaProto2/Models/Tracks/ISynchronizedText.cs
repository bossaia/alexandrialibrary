﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Gnosis.Data;

namespace Gnosis.Alexandria.Models.Tracks
{
    public interface ISynchronizedText
        : IValue
    {
        long Time { get; }
        string Text { get; }
    }
}
