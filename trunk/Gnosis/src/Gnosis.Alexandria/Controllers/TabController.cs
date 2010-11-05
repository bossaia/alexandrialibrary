using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

            var headerBlock = new TextBlock();
            headerBlock.Text = header;
            headerBlock.Margin = new Thickness(0, 1, 8, 0);
            var closeImage = new Image();
            closeImage.Source = (ImageSource)new ImageSourceConverter().ConvertFromString("pack://application:,,,/Alexandria;component/Images/Controls/CloseTab.png");
            var closeButton = new Button();
            closeButton.Tag = tabView.Id;
            closeButton.Width = 18;
            closeButton.Height = 18;
            closeButton.Content = closeImage;
            closeButton.Focusable = false;
            closeButton.Margin = new Thickness(10, 0, 0, 0);
            closeButton.VerticalAlignment = VerticalAlignment.Top;
            closeButton.HorizontalAlignment = HorizontalAlignment.Right;
            closeButton.VerticalContentAlignment = VerticalAlignment.Center;
            closeButton.Click += new RoutedEventHandler(CloseTab);
            var headerPanel = new DockPanel();
            headerPanel.Children.Add(headerBlock);
            headerPanel.Children.Add(closeButton);
            headerPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
            DockPanel.SetDock(headerBlock, Dock.Left);
            DockPanel.SetDock(closeButton, Dock.Right);
            tabItem.Header = headerPanel;
            tabItem.Content = tabView;
            _control.Items.Add(tabItem);
            _tabMap.Add(tabView.Id, tabItem);
            _control.SelectedItem = tabItem;
        }


        private void CloseTab(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var id = (Guid)button.Tag;
            RemoveTab(id);
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
