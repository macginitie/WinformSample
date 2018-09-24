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
        int _meshCount = 0;
        List<MeshInfo> _meshInfoList = null;
        
        public MainForm()
        {
            InitializeComponent();
            ResetMeshInfoLabels();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;
        }

        private void ResetMeshInfoLabels()
        {
            lblNormalCount.Text = "";
            lblQuadFaceCount.Text = "";
            lblTriangularFaceCount.Text = "";
            lblVertexCount.Text = "";
            lblTextureCoordCount.Text = "";
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = txtObjFile.Text;
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
            if (itemIndex < 0)
            {
                // nothing selected
                ResetMeshInfoLabels();
            }
            else
            {
                MeshInfoBox.Text = String.Format("Mesh Info for {0}", _meshInfoList[itemIndex].MeshName);
                try
                {
                    lblVertexCount.Text = String.Format("{0} vertices", _meshInfoList[itemIndex].VertexCount);
                    lblNormalCount.Text = String.Format("{0} normals", 99 - itemIndex);
                    lblQuadFaceCount.Text = String.Format("{0} quadrilateral faces", 99 - itemIndex);
                    lblTriangularFaceCount.Text = String.Format("{0} normals", 99 - itemIndex);
                    lblTextureCoordCount.Text = String.Format("{0} normals", 99 - itemIndex);
                }
                catch (InvalidOperationException ex)
                {
                    // 
                    string forDebugging = ex.Message;
                }
            }
        }

        private void LstMeshList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMeshInfoBox(lstMeshList.SelectedIndex);
        }

        private void ClearMeshInfoList()
        {
            lstMeshList.Items.Clear();
            Refresh();
        }

        public void UpdateProgress()
        {
            lblLoadProgress.Text = String.Format("Loading mesh #{0}", _meshCount);
            Refresh();
        }

        private void BtnLoadFile_Click(object sender, EventArgs e)
        {
            _objFilePath = txtObjFile.Text;
            if (!File.Exists(_objFilePath)) // guard clause
            {
                MessageBox.Show(String.Format("File [{0}] not found", _objFilePath));
                // early exit
                return;
            }
            if (bgWorker.IsBusy)
            {
                // I don't expect this to happen, but just in case
                MessageBox.Show("Load already in progress");
            }
            else
            {
                ResetMeshInfoLabels();
                ClearMeshInfoList();
                EnableButtons(false);
                btnCancelLoading.Visible = true;
                UpdateProgress();
                // Start the asynchronous operation.
                bgWorker.RunWorkerAsync();
            }
        }

        // This event handler is where the time-consuming work is done.
        // this method is a mess & should be refactored :-/ 
        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            int currentMesh = 1;
            string meshName = "";
            string[] fileLines = null;
            int index = 0;
            // require at least "g mesh[\n]v x y z[\n]v a b c[\n]v u v w[\n]f 0 1 2"
            const int MinFileLines = 5; 
            // 3-state FSM: reading file; scanning for 1st "g"; loading mesh data
            int state = 0;
            while (state < 3)
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
                            if (fileLines.Length < MinFileLines)
                            {
                                // notify user about error condition
                                MessageBox.Show("Error: file format not recognized");
                                worker.CancelAsync();
                                e.Cancel = true;
                                return;
                            }
                            ++state;
                            break;
                        case 1:
                            // scan for 1st occurrence of "g"                            
                            while (index < fileLines.Length)
                            {
                                string[] parts = fileLines[index++].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

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
                            if (state == 1)
                            {
                                // notify user of error condition if no "g" in the file
                                MessageBox.Show("Error: no named mesh (group) found in file");
                                worker.CancelAsync();
                                e.Cancel = true;
                                return;
                            }
                            break;
                        case 2:
                            MeshInfo meshInfo = new MeshInfo
                            {
                                MeshName = meshName
                            };
                            meshInfo.LoadFileLines(fileLines, ref index, out meshName);
                            _meshInfoList.Add(meshInfo);
                            if (String.IsNullOrEmpty(meshName))
                            {
                                // all done loading
                                ++state;
                            }
                            else
                            {
                                // another mesh to load
                                ++currentMesh;
                            }
                            break;
                        default:
                            // "should never happen"
                            break;
                    }
                    worker.ReportProgress(currentMesh);
                }
            }
        }

        // This event handler updates the progress. Using "percentage"
        // to hold the mesh # is a hack, but BackgroundWorker has limitations  
        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _meshCount = e.ProgressPercentage;
            UpdateProgress();
        }

        // This event handler deals with the results of the background operation.
        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblLoadProgress.Visible = false;
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
            foreach (MeshInfo meshInfo in _meshInfoList)
            {
                lstMeshList.Items.Add(meshInfo.MeshName);
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
