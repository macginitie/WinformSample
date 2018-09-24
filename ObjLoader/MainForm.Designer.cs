namespace ObjLoader
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblLoadProgress = new System.Windows.Forms.Label();
            this.btnCancelLoading = new System.Windows.Forms.Button();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblFilename = new System.Windows.Forms.Label();
            this.txtObjFile = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.MeshInfoBox = new System.Windows.Forms.GroupBox();
            this.lblQuadFaceCount = new System.Windows.Forms.Label();
            this.lblTriangularFaceCount = new System.Windows.Forms.Label();
            this.lblNormalCount = new System.Windows.Forms.Label();
            this.lblVertexCount = new System.Windows.Forms.Label();
            this.lblMeshList = new System.Windows.Forms.Label();
            this.lstMeshList = new System.Windows.Forms.ListBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.lblTextureCoordCount = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.MeshInfoBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblLoadProgress);
            this.panel1.Controls.Add(this.btnCancelLoading);
            this.panel1.Controls.Add(this.btnLoadFile);
            this.panel1.Controls.Add(this.btnBrowse);
            this.panel1.Controls.Add(this.lblFilename);
            this.panel1.Controls.Add(this.txtObjFile);
            this.panel1.Location = new System.Drawing.Point(9, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(783, 95);
            this.panel1.TabIndex = 0;
            // 
            // lblLoadProgress
            // 
            this.lblLoadProgress.AutoSize = true;
            this.lblLoadProgress.Location = new System.Drawing.Point(256, 59);
            this.lblLoadProgress.Name = "lblLoadProgress";
            this.lblLoadProgress.Size = new System.Drawing.Size(0, 13);
            this.lblLoadProgress.TabIndex = 6;
            // 
            // btnCancelLoading
            // 
            this.btnCancelLoading.AutoSize = true;
            this.btnCancelLoading.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancelLoading.Location = new System.Drawing.Point(641, 56);
            this.btnCancelLoading.Name = "btnCancelLoading";
            this.btnCancelLoading.Size = new System.Drawing.Size(91, 23);
            this.btnCancelLoading.TabIndex = 5;
            this.btnCancelLoading.Text = "Cancel Loading";
            this.btnCancelLoading.UseVisualStyleBackColor = true;
            this.btnCancelLoading.Visible = false;
            this.btnCancelLoading.Click += new System.EventHandler(this.BtnCancelLoading_Click);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.AutoSize = true;
            this.btnLoadFile.Enabled = false;
            this.btnLoadFile.Location = new System.Drawing.Point(106, 56);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(91, 23);
            this.btnLoadFile.TabIndex = 3;
            this.btnLoadFile.Text = "&Load File";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.BtnLoadFile_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.AutoSize = true;
            this.btnBrowse.Location = new System.Drawing.Point(639, 19);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(91, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "&Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // lblFilename
            // 
            this.lblFilename.AutoSize = true;
            this.lblFilename.Location = new System.Drawing.Point(57, 24);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(43, 13);
            this.lblFilename.TabIndex = 1;
            this.lblFilename.Text = ".obj File";
            // 
            // txtObjFile
            // 
            this.txtObjFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObjFile.Location = new System.Drawing.Point(106, 21);
            this.txtObjFile.Name = "txtObjFile";
            this.txtObjFile.Size = new System.Drawing.Size(527, 20);
            this.txtObjFile.TabIndex = 0;
            this.txtObjFile.TextChanged += new System.EventHandler(this.TxtObjFile_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.MeshInfoBox);
            this.panel2.Controls.Add(this.lblMeshList);
            this.panel2.Controls.Add(this.lstMeshList);
            this.panel2.Location = new System.Drawing.Point(9, 112);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(783, 236);
            this.panel2.TabIndex = 1;
            // 
            // MeshInfoBox
            // 
            this.MeshInfoBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MeshInfoBox.Controls.Add(this.lblTextureCoordCount);
            this.MeshInfoBox.Controls.Add(this.lblQuadFaceCount);
            this.MeshInfoBox.Controls.Add(this.lblTriangularFaceCount);
            this.MeshInfoBox.Controls.Add(this.lblNormalCount);
            this.MeshInfoBox.Controls.Add(this.lblVertexCount);
            this.MeshInfoBox.Location = new System.Drawing.Point(312, 37);
            this.MeshInfoBox.Name = "MeshInfoBox";
            this.MeshInfoBox.Size = new System.Drawing.Size(450, 154);
            this.MeshInfoBox.TabIndex = 2;
            this.MeshInfoBox.TabStop = false;
            this.MeshInfoBox.Text = "Mesh Information";
            // 
            // lblQuadFaceCount
            // 
            this.lblQuadFaceCount.AutoSize = true;
            this.lblQuadFaceCount.Location = new System.Drawing.Point(30, 98);
            this.lblQuadFaceCount.Name = "lblQuadFaceCount";
            this.lblQuadFaceCount.Size = new System.Drawing.Size(107, 13);
            this.lblQuadFaceCount.TabIndex = 4;
            this.lblQuadFaceCount.Text = "4 Quadrilateral Faces";
            // 
            // lblTriangularFaceCount
            // 
            this.lblTriangularFaceCount.AutoSize = true;
            this.lblTriangularFaceCount.Location = new System.Drawing.Point(30, 74);
            this.lblTriangularFaceCount.Name = "lblTriangularFaceCount";
            this.lblTriangularFaceCount.Size = new System.Drawing.Size(95, 13);
            this.lblTriangularFaceCount.TabIndex = 3;
            this.lblTriangularFaceCount.Text = "3 Triangular Faces";
            // 
            // lblNormalCount
            // 
            this.lblNormalCount.AutoSize = true;
            this.lblNormalCount.Location = new System.Drawing.Point(30, 50);
            this.lblNormalCount.Name = "lblNormalCount";
            this.lblNormalCount.Size = new System.Drawing.Size(54, 13);
            this.lblNormalCount.TabIndex = 2;
            this.lblNormalCount.Text = "2 Normals";
            // 
            // lblVertexCount
            // 
            this.lblVertexCount.AutoSize = true;
            this.lblVertexCount.Location = new System.Drawing.Point(30, 26);
            this.lblVertexCount.Name = "lblVertexCount";
            this.lblVertexCount.Size = new System.Drawing.Size(54, 13);
            this.lblVertexCount.TabIndex = 1;
            this.lblVertexCount.Text = "1 Vertices";
            // 
            // lblMeshList
            // 
            this.lblMeshList.AutoSize = true;
            this.lblMeshList.Location = new System.Drawing.Point(18, 22);
            this.lblMeshList.Name = "lblMeshList";
            this.lblMeshList.Size = new System.Drawing.Size(44, 13);
            this.lblMeshList.TabIndex = 1;
            this.lblMeshList.Text = "Meshes";
            // 
            // lstMeshList
            // 
            this.lstMeshList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstMeshList.FormattingEnabled = true;
            this.lstMeshList.Location = new System.Drawing.Point(20, 43);
            this.lstMeshList.Name = "lstMeshList";
            this.lstMeshList.Size = new System.Drawing.Size(276, 147);
            this.lstMeshList.TabIndex = 0;
            this.lstMeshList.SelectedIndexChanged += new System.EventHandler(this.LstMeshList_SelectedIndexChanged);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoSize = true;
            this.btnClose.Location = new System.Drawing.Point(682, 366);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(91, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorker_RunWorkerCompleted);
            // 
            // lblTextureCoordCount
            // 
            this.lblTextureCoordCount.AutoSize = true;
            this.lblTextureCoordCount.Location = new System.Drawing.Point(30, 122);
            this.lblTextureCoordCount.Name = "lblTextureCoordCount";
            this.lblTextureCoordCount.Size = new System.Drawing.Size(111, 13);
            this.lblTextureCoordCount.TabIndex = 5;
            this.lblTextureCoordCount.Text = "5 Texture Coordinates";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 401);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "MainForm";
            this.Text = ".obj File Info Viewer";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.MeshInfoBox.ResumeLayout(false);
            this.MeshInfoBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.TextBox txtObjFile;
        private System.Windows.Forms.Button btnCancelLoading;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblLoadProgress;
        private System.Windows.Forms.GroupBox MeshInfoBox;
        private System.Windows.Forms.Label lblVertexCount;
        private System.Windows.Forms.Label lblMeshList;
        private System.Windows.Forms.ListBox lstMeshList;
        private System.Windows.Forms.Label lblQuadFaceCount;
        private System.Windows.Forms.Label lblTriangularFaceCount;
        private System.Windows.Forms.Label lblNormalCount;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Label lblTextureCoordCount;
    }
}

