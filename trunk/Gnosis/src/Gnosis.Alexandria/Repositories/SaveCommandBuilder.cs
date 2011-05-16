﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
    public class SaveCommandBuilder : CommandBuilder
    {
        public SaveCommandBuilder()
            : base()
        {
        }

        public SaveCommandBuilder(IEnumerable<KeyValuePair<string, object>> parameters)
            : base(parameters)
        {
        }
    }
}
