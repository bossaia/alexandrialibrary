using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//using Gnosis.Babel;
//using Gnosis.Babel.Domain;
//using Gnosis.Babel.Relational;
//using Gnosis.Core;
using Gnosis.Alexandria;
using Gnosis.Alexandria.Persistence;
using Gnosis.Alexandria.Persistence.SQLite;

namespace Gnosis.Sphinx
{
	public partial class Main : Form
	{
		public Main()
		{
			InitializeComponent();

            ArtistRepository repo = new ArtistRepository();
            repo.Initialize();

            /*
            Command<IArtist> cmd =
                new Command<IArtist>(x => x.Name, x => (x.Name.GetValue<string>() == "Radiohead" || x.Name.GetValue<string>() == "Tool") && x.StartDate.GetValue<DateTime>() > new DateTime(1940, 1, 1)); 
            */

            /*
            IRepository repo = null;

            IEnumerable<IAlbum> result = repo.Search<IAlbum>(
                x => x.Name == "Lateralus" || x.Artist.Name == "Tool");
            */
		}
	}
}
