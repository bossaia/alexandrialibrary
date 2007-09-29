using System;
using System.Collections.Generic;
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

namespace Alexandria.WpfClient
{
	/// <summary>
	/// Interaction logic for PlaybackButton.xaml
	/// </summary>

	public partial class PlaybackButton : System.Windows.Controls.UserControl
	{
		public PlaybackButton()
		{
			InitializeComponent();
		}

		private void OnMouseEnter(object sender, RoutedEventArgs e)
		{
			//circle.Style = "{StaticResource SelectedStyle}";
		}

		private void OnMouseLeftButtonUp(object sender, RoutedEventArgs e)
		{
			object x = e.Source;
		}
	}
}