using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Papyrus
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            pnlAdditional.Visible = false;
        }

        private void lnkAdditional_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //pnlAdditional.Visible = !pnlAdditional.Visible;
            bool visible = !pnlAdditional.Visible;


            if (visible)
            {
                pnlAdditional.Visible = true;
            }
            else
            {
                pnlAdditional.Visible = false;
                //pnlAdditional.Hide();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
