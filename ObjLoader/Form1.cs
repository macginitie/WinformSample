using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObjLoader
{
    public partial class Form1 : Form
    {
        string _objFilePath;

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (DialogResult.OK == result)
            {
                _objFilePath = openFileDialog1.FileName;
                txtObjFile.Text = _objFilePath;
            }
        }

        private bool OK2Close()
        {
            // just return true for now
            return true;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (OK2Close())
            {
                Application.Exit();
            }
        }
    }
}
