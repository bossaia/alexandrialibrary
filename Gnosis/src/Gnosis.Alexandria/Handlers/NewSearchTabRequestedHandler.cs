using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Messages.Interfaces;
using Gnosis.Alexandria.Views;
using Gnosis.Alexandria.Views.Interfaces;

namespace Gnosis.Alexandria.Handlers
{
    public class NewSearchTabRequestedHandler : Handler<INewSearchTabRequestedMessage>
    {
        public NewSearchTabRequestedHandler(ITabController parent)
        {
            _parent = parent;
        }

        private readonly ITabController _parent;

        protected override void HandleMessage(INewSearchTabRequestedMessage message)
        {
            var view = ServiceLocator.GetObject<ISearchTabView>();
            view.Search = message.Search;
            _parent.AddTab(view);
        }
    }
}
