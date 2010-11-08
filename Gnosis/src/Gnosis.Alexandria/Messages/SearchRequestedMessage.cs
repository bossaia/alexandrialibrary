using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Messages
{
    public class SearchRequestedMessage : TargetedMessage
    {
        public SearchRequestedMessage(Guid target, string search)
            : base(target)
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
