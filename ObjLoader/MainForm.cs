using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace ObjLoader
{
    public partial class MainForm : Form
    {
        string _objFilePath;
        ObjFileInfo _objFileInfo = null;
        int _meshCount = 0;

        public MainForm()
        {
            InitializeComponent();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
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
            // 2DO
            MessageBox.Show(String.Format("{0}", itemIndex));
        }

        private void LstMeshList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMeshInfoBox(lstMeshList.SelectedIndex);
        }

        public void UpdateProgress()
        {
            lblLoadProgress.Text = String.Format("Loading mesh #{0}", _meshCount);
            Refresh();
        }

        private void BtnLoadFile_Click(object sender, EventArgs e)
        {
            if (bgWorker.IsBusy != true)
            {
                EnableButtons(false);
                btnCancelLoading.Visible = true;
                UpdateProgress();
                // Start the asynchronous operation.
                bgWorker.RunWorkerAsync();
            }
        }

        // This event handler is where the time-consuming work is done.
        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for (int i = 1; i <= 10; i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(500);
                    worker.ReportProgress(i);
                }
            }
        }

        // This event handler updates the progress.
        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _meshCount = e.ProgressPercentage;
            UpdateProgress();
        }

        // This event handler deals with the results of the background operation.
        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            btnCancelLoading.Visible = false;
            EnableButtons();

            if (e.Cancelled == true)
            {
                MessageBox.Show("Canceled!");
            }
            else if (e.Error != null)
            {
                MessageBox.Show("Error: " + e.Error.Message);
            }
            else
            {
                UpdateMeshList();
            }
        }

        private void UpdateMeshList()
        {
            // 2DO
            MessageBox.Show("2DO: UpdateMeshList()");
        }

        private void AsyncLoadFile()
        { 
            if ( ! File.Exists(_objFilePath))
            {
                MessageBox.Show(String.Format("File [{0}] not found", _objFilePath));
                return;
            }

            _objFileInfo = new ObjFileInfo(_objFilePath);
            if ( ! _objFileInfo.LoadFile())
            {
                MessageBox.Show(String.Format("Failed to load {0};\n{1}",
                    _objFilePath, _objFileInfo.GetErrorInfo()));
            }
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

        private void BtnCancelLoading_Click(object sender, EventArgs e)
        {
            // this is set true at init, and never set false, but...
            // there's no real harm leaving this check here anyway
            if (bgWorker.WorkerSupportsCancellation) // belt & suspenders :)
            {
                // Cancel the asynchronous operation.
                bgWorker.CancelAsync();
            }
        }
    }
}
