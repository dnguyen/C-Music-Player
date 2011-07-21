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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void btnNewFolder_Click(object sender, EventArgs e)
        {
            frmNewFolder newFolderForm = new frmNewFolder(frmMain.currentFolders, this);
            newFolderForm.Show();
        }
    }
}
