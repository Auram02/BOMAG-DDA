namespace DDA.Interfaces
{
    partial class frmContractNumberLabel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmContractNumberLabel));
            this.btnMainMenu = new System.Windows.Forms.Button();
            this.lstDistributors = new System.Windows.Forms.ListBox();
            this.lstContractNumbers = new System.Windows.Forms.ListBox();
            this.btnSelectContract = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.LabelWriterCmb = new System.Windows.Forms.ComboBox();
            this.TrayCmb = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.FileNameEdit = new System.Windows.Forms.TextBox();
            this.BrowseBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMainMenu
            // 
            this.btnMainMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMainMenu.Location = new System.Drawing.Point(257, 440);
            this.btnMainMenu.Name = "btnMainMenu";
            this.btnMainMenu.Size = new System.Drawing.Size(77, 23);
            this.btnMainMenu.TabIndex = 10;
            this.btnMainMenu.Text = "Back";
            this.btnMainMenu.UseVisualStyleBackColor = true;
            this.btnMainMenu.Click += new System.EventHandler(this.btnMainMenu_Click);
            // 
            // lstDistributors
            // 
            this.lstDistributors.FormattingEnabled = true;
            this.lstDistributors.Location = new System.Drawing.Point(43, 47);
            this.lstDistributors.Name = "lstDistributors";
            this.lstDistributors.Size = new System.Drawing.Size(254, 108);
            this.lstDistributors.TabIndex = 11;
            this.lstDistributors.SelectedIndexChanged += new System.EventHandler(this.lstDistributors_SelectedIndexChanged);
            this.lstDistributors.Click += new System.EventHandler(this.lstDistributors_Click);
            // 
            // lstContractNumbers
            // 
            this.lstContractNumbers.FormattingEnabled = true;
            this.lstContractNumbers.Location = new System.Drawing.Point(43, 201);
            this.lstContractNumbers.Name = "lstContractNumbers";
            this.lstContractNumbers.Size = new System.Drawing.Size(254, 108);
            this.lstContractNumbers.TabIndex = 13;
            // 
            // btnSelectContract
            // 
            this.btnSelectContract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectContract.Location = new System.Drawing.Point(153, 440);
            this.btnSelectContract.Name = "btnSelectContract";
            this.btnSelectContract.Size = new System.Drawing.Size(98, 23);
            this.btnSelectContract.TabIndex = 14;
            this.btnSelectContract.Text = "Create Label";
            this.btnSelectContract.UseVisualStyleBackColor = true;
            this.btnSelectContract.Click += new System.EventHandler(this.btnSelectContract_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 340);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "LabelWriter:";
            // 
            // LabelWriterCmb
            // 
            this.LabelWriterCmb.FormattingEnabled = true;
            this.LabelWriterCmb.Location = new System.Drawing.Point(43, 356);
            this.LabelWriterCmb.Name = "LabelWriterCmb";
            this.LabelWriterCmb.Size = new System.Drawing.Size(254, 21);
            this.LabelWriterCmb.TabIndex = 20;
            this.LabelWriterCmb.SelectedIndexChanged += new System.EventHandler(this.LabelWriterCmb_SelectedIndexChanged);
            // 
            // TrayCmb
            // 
            this.TrayCmb.FormattingEnabled = true;
            this.TrayCmb.Location = new System.Drawing.Point(43, 383);
            this.TrayCmb.Name = "TrayCmb";
            this.TrayCmb.Size = new System.Drawing.Size(254, 21);
            this.TrayCmb.TabIndex = 22;
            this.TrayCmb.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FileNameEdit
            // 
            this.FileNameEdit.Location = new System.Drawing.Point(43, 410);
            this.FileNameEdit.Name = "FileNameEdit";
            this.FileNameEdit.Size = new System.Drawing.Size(208, 20);
            this.FileNameEdit.TabIndex = 23;
            this.FileNameEdit.Visible = false;
            // 
            // BrowseBtn
            // 
            this.BrowseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowseBtn.Location = new System.Drawing.Point(257, 407);
            this.BrowseBtn.Name = "BrowseBtn";
            this.BrowseBtn.Size = new System.Drawing.Size(77, 23);
            this.BrowseBtn.TabIndex = 24;
            this.BrowseBtn.Text = "Browse";
            this.BrowseBtn.UseVisualStyleBackColor = true;
            this.BrowseBtn.Visible = false;
            this.BrowseBtn.Click += new System.EventHandler(this.BrowseBtn_Click);
            // 
            // frmContractNumberLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 475);
            this.Controls.Add(this.BrowseBtn);
            this.Controls.Add(this.FileNameEdit);
            this.Controls.Add(this.TrayCmb);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.LabelWriterCmb);
            this.Controls.Add(this.btnSelectContract);
            this.Controls.Add(this.lstContractNumbers);
            this.Controls.Add(this.lstDistributors);
            this.Controls.Add(this.btnMainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmContractNumberLabel";
            this.Load += new System.EventHandler(this.frmContractNumberLabel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.Button btnMainMenu;
        private System.Windows.Forms.ListBox lstDistributors;
        private System.Windows.Forms.ListBox lstContractNumbers;
        private System.Windows.Forms.Button btnSelectContract;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox LabelWriterCmb;
        private System.Windows.Forms.ComboBox TrayCmb;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox FileNameEdit;
        private System.Windows.Forms.Button BrowseBtn;
    }
}