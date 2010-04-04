using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Entities
{
    public interface INamed
    {
        string Name { get; }
        string SearchName { get; }

        void Rename(string name);
    }
}
