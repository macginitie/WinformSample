using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;


namespace ObjLoader
{
    public partial class MainForm : Form
    {
        static string _objFilePath;
        static int _meshCount = 0;
        static List<MeshInfo> _meshInfoList = null;
        static BackgroundWorker _bw;

        public MainForm()
        {
            InitializeComponent();
            ResetMeshInfoLabels();
            _bw = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _bw.DoWork += LoadFile;
            _bw.ProgressChanged += UpdateProgress;
            _bw.RunWorkerCompleted += LoadFileCompleted;
        }

        private void ResetMeshInfoLabels()
        {
            MeshInfoBox.Text = "Mesh Info";
            lblNormalCount.Text = "";
            lblQuadFaceCount.Text = "";
            lblTriangularFaceCount.Text = "";
            lblVertexCount.Text = "";
            lblTextureCoordCount.Text = "";
        }

        // handles browsing for a file to load, 
        // using the standard Windows file open dialog
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
                MeshInfo meshInfo = _meshInfoList[itemIndex];
                lblQuadFaceCount.Text = String.Format("{0} quadrilateral faces", meshInfo.QuadFaceCount);
                lblTriangularFaceCount.Text = String.Format("{0} triangular faces", meshInfo.TriangularFaceCount);
                lblVertexCount.Text = String.Format("{0} vertices", meshInfo.VertexCount);
                lblNormalCount.Text = String.Format("{0} normals", meshInfo.NormalCount);
                lblTextureCoordCount.Text = String.Format("{0} uv coordinates", meshInfo.UVCoordCount);
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

        private void UpdateProgress(object sender, ProgressChangedEventArgs e)
        {
            _meshCount = e.ProgressPercentage;
            lblLoadProgress.Text = String.Format("Loading mesh #{0}", _meshCount);
            lblLoadProgress.Visible = true;
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
            if (_bw.IsBusy)
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
                // Start the file load operation.
                _bw.RunWorkerAsync();
            }
        }

        // This event handler is where the time-consuming work is done.
        static void LoadFile(object sender, DoWorkEventArgs e)
        {
            int currentMesh = 1;
            _bw.ReportProgress(currentMesh);
            string meshName = "";
            string[] fileLines = null;
            int index = 0;
            // require at least "g mesh[\n]v x y z[\n]v a b c[\n]v u v w[\n]f 0 1 2"
            const int MinFileLines = 5; 
            // 3-state FSM: 1) reading file; 2) scanning for line with "g [meshname]"; 3) loading mesh data
            int state = 0;
            while (state < 3)
            {
                if (_bw.CancellationPending)
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
                                _bw.CancelAsync();
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
                                        if (parts.Length > 1)
                                        {
                                            meshName = parts[1];
                                        }
                                        else
                                        {
                                            meshName = "[unnamed]";
                                        }
                                        _meshInfoList = new List<MeshInfo>();
                                        ++state;
                                        break;
                                    }
                                }
                            }
                            if (index == fileLines.Length)
                            {
                                // notify user of error condition if no "g" found in the file
                                MessageBox.Show("Error: no named mesh (group) found in file");
                                _bw.CancelAsync();
                                e.Cancel = true;
                                return;
                            }
                            break;
                        case 2:
                            MeshInfo meshInfo = new MeshInfo
                            {
                                MeshName = meshName
                            };
                            meshName = meshInfo.LoadFileLines(fileLines, ref index);
                            _meshInfoList.Add(meshInfo);
                            if (MeshInfo.MeshEndFlag == meshName)
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
                    _bw.ReportProgress(currentMesh);
                }
            }
        }

        // This handles completion of the background operation.
        private void LoadFileCompleted(object sender,
                                       RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                UseWaitCursor = false;
                MessageBox.Show("file load canceled; one or more meshes may not have been loaded");
            }
            lblLoadProgress.Visible = false;
            btnCancelLoading.Visible = false;
            EnableButtons();
            UpdateMeshList();
        }

        // load the names of all meshes found in the file into the listbox
        private void UpdateMeshList()
        {
            if (_meshInfoList != null)
            {
                foreach (MeshInfo meshInfo in _meshInfoList)
                {
                    lstMeshList.Items.Add(meshInfo.MeshName);
                }
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
            btnCancelLoading.Enabled = false;   // let the user know we noticed
            UseWaitCursor = true;
            _bw.CancelAsync();
        }

        // Escape key is commonly used as a shortcut for cancelling an operation
        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            // handle Escape key
            if (e.KeyChar == (char)27)
            {
                BtnCancelLoading_Click(sender, e);
            }
        }
    }
}
