using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Gnosis.Archon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TrackListView.ItemsSource = tracks;

            AddSampleTracks();
        }

        private readonly ObservableCollection<ITrack> tracks = new ObservableCollection<ITrack>();

        private void AddSampleTracks()
        {
            var pathAlbum1 = @"\\vmware-host\Shared Folders\Documents\My Music\A Perfect Circle\Thirteenth Step\";
            var date1 = new DateTime(2003, 9, 16);
            var track1 = new Track { Path = pathAlbum1 + "01 The Package.mp3", Number = 1, Title = "The Package", Album = "Thirteenth Step", Artist = "A Perfect Circle", ImagePath = pathAlbum1 + "folder.jpg", ReleaseDate = date1 };
            var track2 = new Track { Path = pathAlbum1 + "02 Weak and Powerless.mp3", Number = 2, Title = "Weak and Powerless", Album = "Thirteenth Step", Artist = "A Perfect Circle", ImagePath = pathAlbum1 + "folder.jpg", ReleaseDate = date1 };

            var pathAlbum2 = @"\\vmware-host\Shared Folders\Documents\My Music\A Perfect Circle\Mer De Noms\";
            var date2 = new DateTime(2000, 5, 23);
            var track3 = new Track { Path = pathAlbum2 + "01 The Hollow.mp3", Number = 1, Title = "The Hollow", Album = "Mer De Noms", Artist = "A Perfect Circle", ImagePath = pathAlbum2 + "folder.jpg", ReleaseDate = date2 };
            var track4 = new Track { Path = pathAlbum1 + "02 Magdalena.mp3", Number = 2, Title = "Magdalena", Album = "Mer De Noms", Artist = "A Perfect Circle", ImagePath = pathAlbum2 + "folder.jpg", ReleaseDate = date2 };

            tracks.Add(track1);
            tracks.Add(track2);
            tracks.Add(track3);
            tracks.Add(track4);
        }

        private void TrackListView_SelectionChanged(object sender, RoutedEventArgs args)
        {
            var track = TrackListView.SelectedItem as ITrack;
            if (track != null)
            {
                MessageBox.Show("Play this track " + track.Title, "TEST");
            }
        }
    }
}
