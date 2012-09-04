﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotR.Core
{
    public interface ICost
    {
        string Description { get; }

        bool IsMetBy(IEnumerable<IPayment> payments);
    }
}
