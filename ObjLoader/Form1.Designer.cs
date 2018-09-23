namespace ObjLoader
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtObjFile = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblLoadProgress = new System.Windows.Forms.Label();
            this.lstMeshList = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MeshInfoBox = new System.Windows.Forms.GroupBox();
            this.lblVertexCount = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.MeshInfoBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lblLoadProgress);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnLoadFile);
            this.panel1.Controls.Add(this.btnBrowse);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtObjFile);
            this.panel1.Location = new System.Drawing.Point(9, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(783, 95);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(642, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Cancel Loading";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(106, 56);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(131, 27);
            this.btnLoadFile.TabIndex = 3;
            this.btnLoadFile.Text = "&Load File";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.AutoSize = true;
            this.btnBrowse.Location = new System.Drawing.Point(641, 19);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(61, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "&Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = ".obj File";
            // 
            // txtObjFile
            // 
            this.txtObjFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObjFile.Location = new System.Drawing.Point(106, 21);
            this.txtObjFile.Name = "txtObjFile";
            this.txtObjFile.Size = new System.Drawing.Size(529, 20);
            this.txtObjFile.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.MeshInfoBox);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lstMeshList);
            this.panel2.Location = new System.Drawing.Point(9, 112);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(783, 228);
            this.panel2.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(719, 361);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(54, 21);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblLoadProgress
            // 
            this.lblLoadProgress.AutoSize = true;
            this.lblLoadProgress.Location = new System.Drawing.Point(256, 59);
            this.lblLoadProgress.Name = "lblLoadProgress";
            this.lblLoadProgress.Size = new System.Drawing.Size(0, 13);
            this.lblLoadProgress.TabIndex = 6;
            // 
            // lstMeshList
            // 
            this.lstMeshList.FormattingEnabled = true;
            this.lstMeshList.Location = new System.Drawing.Point(20, 43);
            this.lstMeshList.Name = "lstMeshList";
            this.lstMeshList.Size = new System.Drawing.Size(259, 160);
            this.lstMeshList.TabIndex = 0;
            this.lstMeshList.SelectedIndexChanged += new System.EventHandler(this.LstMeshList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Meshes";
            // 
            // MeshInfoBox
            // 
            this.MeshInfoBox.Controls.Add(this.lblVertexCount);
            this.MeshInfoBox.Location = new System.Drawing.Point(314, 43);
            this.MeshInfoBox.Name = "MeshInfoBox";
            this.MeshInfoBox.Size = new System.Drawing.Size(450, 156);
            this.MeshInfoBox.TabIndex = 2;
            this.MeshInfoBox.TabStop = false;
            this.MeshInfoBox.Text = "Mesh Information";
            // 
            // lblVertexCount
            // 
            this.lblVertexCount.AutoSize = true;
            this.lblVertexCount.Location = new System.Drawing.Point(225, 72);
            this.lblVertexCount.Name = "lblVertexCount";
            this.lblVertexCount.Size = new System.Drawing.Size(0, 13);
            this.lblVertexCount.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 401);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = ".obj File Info Viewer";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.MeshInfoBox.ResumeLayout(false);
            this.MeshInfoBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtObjFile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblLoadProgress;
        private System.Windows.Forms.GroupBox MeshInfoBox;
        private System.Windows.Forms.Label lblVertexCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstMeshList;
    }
}

