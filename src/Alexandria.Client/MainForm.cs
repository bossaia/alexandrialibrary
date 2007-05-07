using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Alexandria;
using Alexandria.Client.Properties;

namespace Alexandria.Client
{
	public partial class MainForm : Form
	{
		#region Constructors
		public MainForm()
		{
			try
			{
				InitializeComponent();
			}
			catch (AlexandriaException ex)
			{

				MessageBox.Show(ex.Message, Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
			}
		}
		#endregion		
	}
}