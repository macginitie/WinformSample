using System;
using System.IO;
using System.Windows.Forms;

namespace ObjLoader
{
    public partial class MainForm : Form
    {
        string _objFilePath;
        ObjFileInfo _objFileInfo = null;

        public MainForm()
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

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UpdateMeshInfoBox(int itemIndex)
        {
            MessageBox.Show(String.Format("{0}", itemIndex));
        }

        private void LstMeshList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMeshInfoBox(lstMeshList.SelectedIndex);
        }

        private void BtnLoadFile_Click(object sender, EventArgs e)
        {
            if ( ! File.Exists(_objFilePath))
            {
                MessageBox.Show(String.Format("File [{0}] not found", _objFilePath));
                return;
            }

            EnableButtons(false);
            btnCancelLoading.Visible = true;
            _objFileInfo = new ObjFileInfo(_objFilePath);
            if ( ! _objFileInfo.LoadFile())
            {
                MessageBox.Show(String.Format("Failed to load {0};\n{1}",
                    _objFilePath, _objFileInfo.GetErrorInfo()));
            }
            btnCancelLoading.Visible = false;
            EnableButtons();
        }

        private void EnableButtons(bool newState=true)
        {
            btnBrowse.Enabled = newState;
            btnLoadFile.Enabled = newState;
            btnClose.Enabled = newState;
        }

        private void TxtObjFile_TextChanged(object sender, EventArgs e)
        {
            btnLoadFile.Enabled = (!String.IsNullOrWhiteSpace(txtObjFile.Text));
        }
    }
}
