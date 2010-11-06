using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Messages.Interfaces;
using Gnosis.Alexandria.Views;

namespace Gnosis.Alexandria.Handlers
{
    public class NewHomeTabRequestedHandler : Handler<INewHomeTabRequestedMessage>
    {
        public NewHomeTabRequestedHandler(ITabController parent)
        {
            _parent = parent;
        }

        private readonly ITabController _parent;

        protected override void HandleMessage(INewHomeTabRequestedMessage message)
        {
            _parent.AddTab(new HomeTabView(_parent));
        }
    }
}
