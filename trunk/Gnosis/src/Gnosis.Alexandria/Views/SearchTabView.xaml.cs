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
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchTabView : ControlView, ITabView
    {
        public SearchTabView()
        {
            InitializeComponent();
        }

        public SearchTabView(IDispatcher parent)
            : base(parent, string.Empty)
        {
            InitializeComponent();
        }

        public SearchTabView(IDispatcher parent, string search)
            : base(parent, GetTitle(search))
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(search))
            {
                txtSearch.Text = search;
            }
        }

        private static string GetTitle(string search)
        {
            if (string.IsNullOrEmpty(search))
                return "New Search";

            return string.Format("Search: {0}", search);
        }
    }
}
