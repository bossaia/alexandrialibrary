using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Controllers;
using Gnosis.Alexandria.Messages;
using Gnosis.Alexandria.Views;

namespace Gnosis.Alexandria.Handlers
{
    public class NewHomeTabRequestedHandler : Handler<INewHomeTabRequested>
    {
        public NewHomeTabRequestedHandler(TabController parent)
        {
            _parent = parent;
        }

        private readonly TabController _parent;

        protected override void HandleMessage(INewHomeTabRequested message)
        {
            _parent.AddTab(new HomeTabView(_parent), "New Tab");
        }
    }
}
