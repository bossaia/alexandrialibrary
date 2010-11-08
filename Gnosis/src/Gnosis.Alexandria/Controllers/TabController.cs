using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Gnosis.Alexandria.Controllers.Interfaces;
using Gnosis.Alexandria.Handlers;
using Gnosis.Alexandria.Messages;
using Gnosis.Alexandria.Views;
using Gnosis.Alexandria.Views.Interfaces;

namespace Gnosis.Alexandria.Controllers
{
    public class TabController : Controller, ITabController
    {
        public TabController(IDispatcher parent, TabControl control)
            : base(parent)
        {
            _control = control;

            AddHandler(new NewHomeTabRequestedHandler(this));
            AddHandler(new NewSearchTabRequestedHandler(this));
        }

        private readonly TabControl _control;
        private readonly IDictionary<Guid, TabItem> _tabMap = new Dictionary<Guid, TabItem>();

        #region AddTab Helper Methods

        private static Label CreateTabTitle(ITabView tabView)
        {
            return new Label
            {
                Content = tabView.Title,
                Margin = new Thickness(0, 1, 8, 0)
            };
        }

        private Button CreateTabCloseBox(ITabView tabView)
        {
            var closeBoxImage = new Image
            {
                Source =
                    (ImageSource)
                    new ImageSourceConverter().ConvertFromString(
                        "pack://application:,,,/Alexandria;component/Images/Controls/CloseTab.png")
            };

            var closeButton = new Button
            {
                Tag = tabView.Id,
                Width = 18,
                Height = 18,
                Content = closeBoxImage,
                Focusable = false,
                Margin = new Thickness(10, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            closeButton.Click += new RoutedEventHandler(delegate(object sender, RoutedEventArgs e)
            {
                var button = sender as Button;
                var id = (Guid)button.Tag;
                RemoveTab(id);
            }
            );

            return closeButton;
        }

        private UIElement CreateTabHeader(ITabView tabView)
        {
            var title = CreateTabTitle(tabView);
            var closeBox = CreateTabCloseBox(tabView);

            var header = new DockPanel { HorizontalAlignment = HorizontalAlignment.Stretch };
            header.Children.Add(title);
            header.Children.Add(closeBox);
            DockPanel.SetDock(title, Dock.Left);
            DockPanel.SetDock(closeBox, Dock.Right);

            return header;
        }

        private UIElement CreateTabContent(ITabView tabView)
        {
            DockPanel content = null;
            var viewElement = tabView as UIElement;
            if (viewElement != null)
            {
                content = new DockPanel
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    LastChildFill = true
                };
                content.Children.Add(viewElement);
            }

            return content;
        }

        private TabItem CreateTabItem(ITabView tabView)
        {
            var tabItem = new TabItem
            {
                Header = CreateTabHeader(tabView),
                Content = CreateTabContent(tabView)
            };

            return tabItem;
        }

        private void AddTabItemAndView(TabItem tabItem, ITabView tabView)
        {
            _control.Items.Add(tabItem);
            _tabMap.Add(tabView.Id, tabItem);
        }

        #endregion

        #region RemoveTab Helper Methods

        private void RemoveTabItemAndView(TabItem tabItem, Guid id)
        {
            _control.Items.Remove(tabItem);
            _tabMap.Remove(id);
        }

        #endregion

        #region General Helper Methods

        private TabItem GetTabItem(Guid id)
        {
            if (_tabMap.ContainsKey(id))
                return _tabMap[id];
            else
                throw new InvalidOperationException(string.Format("Tab does not exist: {0}", id));
        }

        private void SelectTabItem(TabItem tabItem)
        {
            _control.SelectedItem = tabItem;
        }

        #endregion

        #region ITabController Members

        public void AddTab(ITabView tabView)
        {
            AddChild(tabView);
      
            var tabItem = CreateTabItem(tabView);

            AddTabItemAndView(tabItem, tabView);
            
            SelectTabItem(tabItem);
        }

        public void RemoveTab(Guid id)
        {
            RemoveChild(id);

            var tabItem = GetTabItem(id);

            RemoveTabItemAndView(tabItem, id);
        }

        public void SelectTab(Guid id)
        {
            var tabItem = GetTabItem(id);
            SelectTabItem(tabItem);
        }

        #endregion
    }
}
