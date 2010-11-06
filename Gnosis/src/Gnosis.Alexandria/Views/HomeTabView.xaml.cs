using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Gnosis.Alexandria.Views.Interfaces;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for HomeTabView.xaml
    /// </summary>
    public partial class HomeTabView : ControlView, ITabView
    {
        public HomeTabView()
        {
            InitializeComponent();
        }

        public HomeTabView(IDispatcher parent)
            : base(parent, "New Tab")
        {
            InitializeComponent();
        }

        public HomeTabView(IDispatcher parent, string title)
            : base(parent, title)
        {
            InitializeComponent();
        }
    }
}
