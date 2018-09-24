using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace ObjLoader
{
    public partial class MainForm : Form
    {
        string _objFilePath;
        //ObjFileInfo _objFileInfo = null;
        int _meshCount = 0;
        List<MeshInfo> _meshInfoList = null;

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
            foreach (MeshInfo mesh in _meshInfoList)
            {
                lstMeshList.Items.Add(mesh.MeshName);
            }
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
            if (!File.Exists(_objFilePath)) // guard clause
            {
                MessageBox.Show(String.Format("File [{0}] not found", _objFilePath));
                // early exit
                return;
            }
            if (bgWorker.IsBusy != true)
            {
                EnableButtons(false);
                btnCancelLoading.Visible = true;
                UpdateProgress();
                // Start the asynchronous operation.
                bgWorker.RunWorkerAsync();
            }
            else
            {
                // I don't expect this to happen, but just in case
                MessageBox.Show("Load already in progress");
            }
        }

        // This event handler is where the time-consuming work is done.
        // this should be refactored :-/ 
        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            int currentMesh = 1;
            string meshName = "";
            string[] fileLines = null;
            int index = 0;
            // super-simple FSM
            for (int state = 0; state < 3; state++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    switch (state)
                    {
                        case 0:
                            // read file data
                            fileLines = File.ReadAllLines(_objFilePath);
                            ++state;
                            break;
                        case 1:
                            // scan for 1st occurrence of "g"                            
                            while (index < fileLines.Length)
                            {
                                string[] parts = fileLines[index].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                if (parts.Length > 0)
                                {
                                    if (parts[0] == "g")
                                    {
                                        meshName = parts[1]; // danger: exception if line contains only "g" (2DO)
                                        _meshInfoList = new List<MeshInfo>();
                                        ++state;
                                        break;
                                    }
                                }
                            }
                            // error condition if no "g" in the file
                            worker.CancelAsync();
                            e.Cancel = true;
                            break;
                        case 2:
                            MeshInfo meshInfo = new MeshInfo
                            {
                                MeshName = meshName
                            };
                            meshInfo.LoadFileLines(fileLines, ref index, out meshName);
                            if (String.IsNullOrEmpty(meshName))
                            {
                                ++state;
                            }
                            _meshInfoList.Add(meshInfo);
                            break;
                    }
                    worker.ReportProgress(currentMesh);
                }
            }
        }

        // This event handler updates the progress. This is a hack, using "percentage"
        // to hold the mesh #, but BackgroundWorker has limitations  
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
