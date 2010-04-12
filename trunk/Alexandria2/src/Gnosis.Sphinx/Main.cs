using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Gnosis.Babel;
using Gnosis.Babel.Domain;
using Gnosis.Babel.Relational;
using Gnosis.Core;
using Gnosis.Alexandria;
using Gnosis.Alexandria.Entities;

namespace Gnosis.Sphinx
{
	public partial class Main : Form
	{
		public Main()
		{
			InitializeComponent();

            IRepository repo = null;

            IEnumerable<IAlbum> result = repo.Search<IAlbum>(
                x => x.Name == "Lateralus" || x.Artist.Name == "Tool");
		}
	}
}
