namespace DDA.Interfaces
{
    partial class frmContractInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmContractInformation));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDistributorName = new System.Windows.Forms.Label();
            this.lblBranchLocations = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.lblCounties = new System.Windows.Forms.Label();
            this.lblContractNumber = new System.Windows.Forms.Label();
            this.dtpContract = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.lstCategory = new System.Windows.Forms.ListBox();
            this.lstState = new System.Windows.Forms.ListBox();
            this.lstBranchLocations = new System.Windows.Forms.ListBox();
            this.lstCounties = new System.Windows.Forms.ListBox();
            this.btnMainMenu = new System.Windows.Forms.Button();
            this.lblModifyDate = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblContractDate = new System.Windows.Forms.Label();
            this.ThePrintDocument = new System.Drawing.Printing.PrintDocument();
            this.btnPrintLabels = new System.Windows.Forms.Button();
            this.BrowseBtn = new System.Windows.Forms.Button();
            this.FileNameEdit = new System.Windows.Forms.TextBox();
            this.TrayCmb = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LabelWriterCmb = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cachedrptFullContract1 = new DDA.Reports.CachedrptFullContract();
            this.btnPrintMap = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.Location = new System.Drawing.Point(184, 521);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(61, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.Location = new System.Drawing.Point(123, 521);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(55, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Back";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(210, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 17);
            this.label10.TabIndex = 16;
            this.label10.Text = "CONTRACT #:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(176, 431);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(145, 17);
            this.label9.TabIndex = 22;
            this.label9.Text = "CONTRACT DATE:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(230, 349);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 17);
            this.label8.TabIndex = 21;
            this.label8.Text = "COUNTIES:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(260, 269);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 17);
            this.label5.TabIndex = 20;
            this.label5.Text = "STATE:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(94, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 17);
            this.label4.TabIndex = 19;
            this.label4.Text = "CATEGORY:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(168, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "SALES LOCATIONS:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(153, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "DISTRIBUTOR NAME:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblDistributorName
            // 
            this.lblDistributorName.AutoSize = true;
            this.lblDistributorName.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistributorName.Location = new System.Drawing.Point(330, 44);
            this.lblDistributorName.Name = "lblDistributorName";
            this.lblDistributorName.Size = new System.Drawing.Size(12, 17);
            this.lblDistributorName.TabIndex = 1;
            this.lblDistributorName.Text = ".";
            this.lblDistributorName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblBranchLocations
            // 
            this.lblBranchLocations.AutoSize = true;
            this.lblBranchLocations.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBranchLocations.Location = new System.Drawing.Point(329, 81);
            this.lblBranchLocations.Name = "lblBranchLocations";
            this.lblBranchLocations.Size = new System.Drawing.Size(12, 17);
            this.lblBranchLocations.TabIndex = 8;
            this.lblBranchLocations.Text = ".";
            this.lblBranchLocations.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.Location = new System.Drawing.Point(329, 189);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(12, 17);
            this.lblCategory.TabIndex = 9;
            this.lblCategory.Text = ".";
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.Location = new System.Drawing.Point(329, 269);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(12, 17);
            this.lblState.TabIndex = 10;
            this.lblState.Text = ".";
            this.lblState.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCounties
            // 
            this.lblCounties.AutoSize = true;
            this.lblCounties.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCounties.Location = new System.Drawing.Point(328, 349);
            this.lblCounties.Name = "lblCounties";
            this.lblCounties.Size = new System.Drawing.Size(12, 17);
            this.lblCounties.TabIndex = 11;
            this.lblCounties.Text = ".";
            this.lblCounties.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblContractNumber
            // 
            this.lblContractNumber.AutoSize = true;
            this.lblContractNumber.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContractNumber.Location = new System.Drawing.Point(329, 19);
            this.lblContractNumber.Name = "lblContractNumber";
            this.lblContractNumber.Size = new System.Drawing.Size(12, 17);
            this.lblContractNumber.TabIndex = 0;
            this.lblContractNumber.Text = ".";
            this.lblContractNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // dtpContract
            // 
            this.dtpContract.Location = new System.Drawing.Point(332, 430);
            this.dtpContract.Name = "dtpContract";
            this.dtpContract.Size = new System.Drawing.Size(200, 20);
            this.dtpContract.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(183, 461);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 17);
            this.label6.TabIndex = 23;
            this.label6.Text = "MODIFIED DATE:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lstCategory
            // 
            this.lstCategory.FormattingEnabled = true;
            this.lstCategory.Location = new System.Drawing.Point(97, 163);
            this.lstCategory.Name = "lstCategory";
            this.lstCategory.Size = new System.Drawing.Size(430, 95);
            this.lstCategory.TabIndex = 3;
            // 
            // lstState
            // 
            this.lstState.FormattingEnabled = true;
            this.lstState.Location = new System.Drawing.Point(332, 269);
            this.lstState.Name = "lstState";
            this.lstState.Size = new System.Drawing.Size(195, 69);
            this.lstState.TabIndex = 4;
            // 
            // lstBranchLocations
            // 
            this.lstBranchLocations.FormattingEnabled = true;
            this.lstBranchLocations.Location = new System.Drawing.Point(333, 81);
            this.lstBranchLocations.Name = "lstBranchLocations";
            this.lstBranchLocations.Size = new System.Drawing.Size(195, 69);
            this.lstBranchLocations.TabIndex = 2;
            // 
            // lstCounties
            // 
            this.lstCounties.FormattingEnabled = true;
            this.lstCounties.Location = new System.Drawing.Point(332, 349);
            this.lstCounties.Name = "lstCounties";
            this.lstCounties.Size = new System.Drawing.Size(195, 69);
            this.lstCounties.TabIndex = 5;
            // 
            // btnMainMenu
            // 
            this.btnMainMenu.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnMainMenu.Location = new System.Drawing.Point(53, 521);
            this.btnMainMenu.Name = "btnMainMenu";
            this.btnMainMenu.Size = new System.Drawing.Size(64, 23);
            this.btnMainMenu.TabIndex = 14;
            this.btnMainMenu.Text = "Main List";
            this.btnMainMenu.UseVisualStyleBackColor = true;
            this.btnMainMenu.Click += new System.EventHandler(this.btnMainMenu_Click);
            // 
            // lblModifyDate
            // 
            this.lblModifyDate.AutoSize = true;
            this.lblModifyDate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModifyDate.Location = new System.Drawing.Point(330, 461);
            this.lblModifyDate.Name = "lblModifyDate";
            this.lblModifyDate.Size = new System.Drawing.Size(12, 17);
            this.lblModifyDate.TabIndex = 7;
            this.lblModifyDate.Text = ".";
            this.lblModifyDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPrint.Location = new System.Drawing.Point(287, 521);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(61, 23);
            this.btnPrint.TabIndex = 15;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lblContractDate
            // 
            this.lblContractDate.AutoSize = true;
            this.lblContractDate.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContractDate.Location = new System.Drawing.Point(330, 432);
            this.lblContractDate.Name = "lblContractDate";
            this.lblContractDate.Size = new System.Drawing.Size(12, 17);
            this.lblContractDate.TabIndex = 0;
            this.lblContractDate.Text = ".";
            this.lblContractDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ThePrintDocument
            // 
            this.ThePrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.ThePrintDocument_PrintPage);
            // 
            // btnPrintLabels
            // 
            this.btnPrintLabels.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPrintLabels.Location = new System.Drawing.Point(437, 521);
            this.btnPrintLabels.Name = "btnPrintLabels";
            this.btnPrintLabels.Size = new System.Drawing.Size(91, 23);
            this.btnPrintLabels.TabIndex = 24;
            this.btnPrintLabels.Text = "PRINT LABEL";
            this.btnPrintLabels.UseVisualStyleBackColor = true;
            this.btnPrintLabels.Click += new System.EventHandler(this.btnPrintLabels_Click);
            // 
            // BrowseBtn
            // 
            this.BrowseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowseBtn.Location = new System.Drawing.Point(393, 322);
            this.BrowseBtn.Name = "BrowseBtn";
            this.BrowseBtn.Size = new System.Drawing.Size(77, 23);
            this.BrowseBtn.TabIndex = 29;
            this.BrowseBtn.Text = "Browse";
            this.BrowseBtn.UseVisualStyleBackColor = true;
            this.BrowseBtn.Visible = false;
            // 
            // FileNameEdit
            // 
            this.FileNameEdit.Location = new System.Drawing.Point(97, 343);
            this.FileNameEdit.Name = "FileNameEdit";
            this.FileNameEdit.Size = new System.Drawing.Size(208, 20);
            this.FileNameEdit.TabIndex = 28;
            this.FileNameEdit.Visible = false;
            // 
            // TrayCmb
            // 
            this.TrayCmb.FormattingEnabled = true;
            this.TrayCmb.Location = new System.Drawing.Point(97, 316);
            this.TrayCmb.Name = "TrayCmb";
            this.TrayCmb.Size = new System.Drawing.Size(254, 21);
            this.TrayCmb.TabIndex = 27;
            this.TrayCmb.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(306, 493);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "LabelWriter:";
            // 
            // LabelWriterCmb
            // 
            this.LabelWriterCmb.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelWriterCmb.FormattingEnabled = true;
            this.LabelWriterCmb.Location = new System.Drawing.Point(376, 489);
            this.LabelWriterCmb.Name = "LabelWriterCmb";
            this.LabelWriterCmb.Size = new System.Drawing.Size(158, 17);
            this.LabelWriterCmb.TabIndex = 25;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnPrintMap
            // 
            this.btnPrintMap.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPrintMap.Location = new System.Drawing.Point(354, 521);
            this.btnPrintMap.Name = "btnPrintMap";
            this.btnPrintMap.Size = new System.Drawing.Size(77, 23);
            this.btnPrintMap.TabIndex = 30;
            this.btnPrintMap.Text = "PRINT MAP";
            this.btnPrintMap.UseVisualStyleBackColor = true;
            this.btnPrintMap.Click += new System.EventHandler(this.btnPrintMap_Click);
            // 
            // frmContractInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 556);
            this.Controls.Add(this.btnPrintMap);
            this.Controls.Add(this.BrowseBtn);
            this.Controls.Add(this.FileNameEdit);
            this.Controls.Add(this.TrayCmb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LabelWriterCmb);
            this.Controls.Add(this.btnPrintLabels);
            this.Controls.Add(this.lblContractDate);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblModifyDate);
            this.Controls.Add(this.btnMainMenu);
            this.Controls.Add(this.lstCounties);
            this.Controls.Add(this.lstBranchLocations);
            this.Controls.Add(this.lstState);
            this.Controls.Add(this.lstCategory);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpContract);
            this.Controls.Add(this.lblContractNumber);
            this.Controls.Add(this.lblCounties);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblBranchLocations);
            this.Controls.Add(this.lblDistributorName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = global::DDA.Properties.Settings.Default.Center;
            this.Name = "frmContractInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CONTRACT INFORMATION";
            this.Load += new System.EventHandler(this.frmContractInformation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDistributorName;
        private System.Windows.Forms.Label lblBranchLocations;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblCounties;
        private System.Windows.Forms.Label lblContractNumber;
        private System.Windows.Forms.DateTimePicker dtpContract;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lstCategory;
        private System.Windows.Forms.ListBox lstState;
        private System.Windows.Forms.ListBox lstBranchLocations;
        private System.Windows.Forms.ListBox lstCounties;
        private System.Windows.Forms.Button btnMainMenu;
        private System.Windows.Forms.Label lblModifyDate;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lblContractDate;
        private System.Drawing.Printing.PrintDocument ThePrintDocument;
        private System.Windows.Forms.Button btnPrintLabels;
        private System.Windows.Forms.Button BrowseBtn;
        private System.Windows.Forms.TextBox FileNameEdit;
        private System.Windows.Forms.ComboBox TrayCmb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox LabelWriterCmb;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Reports.CachedrptFullContract cachedrptFullContract1;
        private System.Windows.Forms.Button btnPrintMap;
    }
}