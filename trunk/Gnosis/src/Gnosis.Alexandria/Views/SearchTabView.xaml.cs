using Gnosis.Alexandria.Views.Interfaces;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchTabView : ControlView, ISearchTabView
    {
        public SearchTabView()
        {
            InitializeComponent();
        }

        public string Search
        {
            get { return txtSearch.Text; }
            set
            {
                txtSearch.Text = value;
                ((IView)this).Title = GetTitle(value);
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
