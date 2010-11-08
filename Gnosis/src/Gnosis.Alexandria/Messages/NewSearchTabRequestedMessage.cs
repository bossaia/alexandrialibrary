using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Messages.Interfaces;

namespace Gnosis.Alexandria.Messages
{
    public class NewSearchTabRequestedMessage : INewSearchTabRequestedMessage
    {
        public NewSearchTabRequestedMessage(string search)
        {
            _search = search;
        }

        private readonly string _search;

        public string Search
        {
            get { return _search; }
        }
    }
}
