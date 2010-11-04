using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

using Gnosis.Alexandria.Handlers;
using Gnosis.Alexandria.Messages;

namespace Gnosis.Alexandria.Controllers
{
    public class TabController : Controller
    {
        public TabController(IDispatcher parent, TabControl control)
            : base(parent)
        {
            _control = control;

            AddHandler(new NewHomeTabRequestedHandler(this));
        }

        private readonly TabControl _control;
        private readonly IDictionary<Guid, TabItem> _tabMap = new Dictionary<Guid, TabItem>();

        public void AddTab(IView tabView, string header)
        {
            AddChild(tabView);
            var tabItem = new TabItem();
            tabItem.Header = header;
            tabItem.Content = tabView;
            _control.Items.Add(tabItem);
            _tabMap.Add(tabView.Id, tabItem);
            _control.SelectedItem = tabItem;
        }

        public void RemoveTab(Guid id)
        {
            RemoveChild(id);
            var tabItem = _tabMap[id];
            _control.Items.Remove(tabItem);
            _tabMap.Remove(id);
        }
    }
}
