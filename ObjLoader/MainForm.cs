using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

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
            //if (bgWorker.IsBusy)
            if (false)
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
                // Start the file load operation.
                LoadFile();
                LoadFileCompleted();
            }
        }

        // This event handler is where the time-consuming work is done.
        // this method is a mess & should be refactored :-/ 
        private void LoadFile()
        {
            int currentMesh = 1;
            string meshName = "";
            string[] fileLines = null;
            int index = 0;
            // require at least "g mesh[\n]v x y z[\n]v a b c[\n]v u v w[\n]f 0 1 2"
            const int MinFileLines = 5; 
            // 3-state FSM: 1) reading file; 2) scanning for line with "g [meshname]"; 3) loading mesh data
            int state = 0;
            while (state < 3)
            {
                //if (worker.CancellationPending == true)
                if (false)
                {
//                    e.Cancel = true;
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
                                //worker.CancelAsync();
                                //e.Cancel = true;
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
                            if (index == fileLines.Length)
                            {
                                // notify user of error condition if no "g" found in the file
                                MessageBox.Show("Error: no named mesh (group) found in file");
                                //worker.CancelAsync();
                                //e.Cancel = true;
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
                    //worker.ReportProgress(currentMesh);
                    _meshCount = currentMesh;
                    UpdateProgress();
                }
            }
        }

        // This handles completion of the background operation.
        private void LoadFileCompleted()
        {
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
            // 2DO
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
