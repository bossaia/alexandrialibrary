using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Gnosis.Alexandria;
using StructureMap;

namespace Gnosis.Sphinx
{
	public partial class Main : Form
	{
		public Main()
		{
			InitializeComponent();

			var album = ObjectFactory.GetInstance<IAlbum>();
			var x = album;
		}
	}
}
