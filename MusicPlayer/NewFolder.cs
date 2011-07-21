using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Music
{
    public partial class frmNewFolder : Form
    {
        private Settings settings;

        public frmNewFolder(List<string> cFolders_, Settings settings_)
        {
            InitializeComponent();
            settings = settings_;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            addNewFolderBrowser.ShowDialog();
            txtFolderPath.Text = addNewFolderBrowser.SelectedPath;
        }

        private void frmNewFolder_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Update current folders

        }
    }
}
