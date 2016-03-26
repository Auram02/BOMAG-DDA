namespace DDA.Interfaces
{
    partial class frmLabelReports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLabelReports));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnDeselectAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.txtAttnGeneral = new System.Windows.Forms.TextBox();
            this.chkProductType = new System.Windows.Forms.CheckedListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel2 = new System.Windows.Forms.Button();
            this.cboDistType = new System.Windows.Forms.ComboBox();
            this.chkState = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCreateGeneralLabel = new System.Windows.Forms.Button();
            this.txtAttnTerritory = new System.Windows.Forms.TextBox();
            this.cboRepType = new System.Windows.Forms.ComboBox();
            this.cboRepName = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCreateTerritoryLabel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDeselectAllProductType = new System.Windows.Forms.Button();
            this.btnSelectAllProductType = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(743, 506);
            this.splitContainer1.SplitterDistance = 389;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnDeselectAllProductType);
            this.panel2.Controls.Add(this.btnSelectAllProductType);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Controls.Add(this.btnDeselectAll);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnSelectAll);
            this.panel2.Controls.Add(this.txtAttnGeneral);
            this.panel2.Controls.Add(this.chkProductType);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnCancel2);
            this.panel2.Controls.Add(this.cboDistType);
            this.panel2.Controls.Add(this.chkState);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnCreateGeneralLabel);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(386, 506);
            this.panel2.TabIndex = 19;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(202, 24);
            this.lblTitle.TabIndex = 14;
            this.lblTitle.Text = "GENERAL LABELS";
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Location = new System.Drawing.Point(266, 428);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size(75, 23);
            this.btnDeselectAll.TabIndex = 18;
            this.btnDeselectAll.Text = "DeSelect All";
            this.btnDeselectAll.UseVisualStyleBackColor = true;
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "PRODUCT TYPE:";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(173, 428);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 17;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // txtAttnGeneral
            // 
            this.txtAttnGeneral.Location = new System.Drawing.Point(173, 59);
            this.txtAttnGeneral.Name = "txtAttnGeneral";
            this.txtAttnGeneral.Size = new System.Drawing.Size(168, 20);
            this.txtAttnGeneral.TabIndex = 0;
            // 
            // chkProductType
            // 
            this.chkProductType.CheckOnClick = true;
            this.chkProductType.FormattingEnabled = true;
            this.chkProductType.Location = new System.Drawing.Point(173, 98);
            this.chkProductType.Name = "chkProductType";
            this.chkProductType.Size = new System.Drawing.Size(168, 94);
            this.chkProductType.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(128, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "ATTN:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "DISTRIBUTOR TYPE:";
            // 
            // btnCancel2
            // 
            this.btnCancel2.Location = new System.Drawing.Point(202, 470);
            this.btnCancel2.Name = "btnCancel2";
            this.btnCancel2.Size = new System.Drawing.Size(59, 23);
            this.btnCancel2.TabIndex = 9;
            this.btnCancel2.Text = "CANCEL";
            this.btnCancel2.UseVisualStyleBackColor = true;
            this.btnCancel2.Click += new System.EventHandler(this.btnCancel2_Click);
            // 
            // cboDistType
            // 
            this.cboDistType.FormattingEnabled = true;
            this.cboDistType.Location = new System.Drawing.Point(173, 241);
            this.cboDistType.Name = "cboDistType";
            this.cboDistType.Size = new System.Drawing.Size(168, 21);
            this.cboDistType.TabIndex = 2;
            // 
            // chkState
            // 
            this.chkState.CheckOnClick = true;
            this.chkState.FormattingEnabled = true;
            this.chkState.Location = new System.Drawing.Point(173, 282);
            this.chkState.Name = "chkState";
            this.chkState.Size = new System.Drawing.Size(168, 139);
            this.chkState.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(122, 282);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "STATE:";
            // 
            // btnCreateGeneralLabel
            // 
            this.btnCreateGeneralLabel.Location = new System.Drawing.Point(275, 470);
            this.btnCreateGeneralLabel.Name = "btnCreateGeneralLabel";
            this.btnCreateGeneralLabel.Size = new System.Drawing.Size(66, 23);
            this.btnCreateGeneralLabel.TabIndex = 8;
            this.btnCreateGeneralLabel.Text = "CREATE";
            this.btnCreateGeneralLabel.UseVisualStyleBackColor = true;
            this.btnCreateGeneralLabel.Click += new System.EventHandler(this.btnCreateGeneralLabel_Click);
            // 
            // txtAttnTerritory
            // 
            this.txtAttnTerritory.Location = new System.Drawing.Point(145, 59);
            this.txtAttnTerritory.Name = "txtAttnTerritory";
            this.txtAttnTerritory.Size = new System.Drawing.Size(168, 20);
            this.txtAttnTerritory.TabIndex = 1;
            // 
            // cboRepType
            // 
            this.cboRepType.FormattingEnabled = true;
            this.cboRepType.Location = new System.Drawing.Point(145, 117);
            this.cboRepType.Name = "cboRepType";
            this.cboRepType.Size = new System.Drawing.Size(168, 21);
            this.cboRepType.TabIndex = 4;
            this.cboRepType.SelectedIndexChanged += new System.EventHandler(this.cboRepType_SelectedIndexChanged);
            // 
            // cboRepName
            // 
            this.cboRepName.FormattingEnabled = true;
            this.cboRepName.Location = new System.Drawing.Point(145, 177);
            this.cboRepName.Name = "cboRepName";
            this.cboRepName.Size = new System.Drawing.Size(168, 21);
            this.cboRepName.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(174, 470);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(59, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCreateTerritoryLabel
            // 
            this.btnCreateTerritoryLabel.Location = new System.Drawing.Point(247, 470);
            this.btnCreateTerritoryLabel.Name = "btnCreateTerritoryLabel";
            this.btnCreateTerritoryLabel.Size = new System.Drawing.Size(66, 23);
            this.btnCreateTerritoryLabel.TabIndex = 6;
            this.btnCreateTerritoryLabel.Text = "CREATE";
            this.btnCreateTerritoryLabel.UseVisualStyleBackColor = true;
            this.btnCreateTerritoryLabel.Click += new System.EventHandler(this.btnCreateTerritoryLabel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(100, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "ATTN:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(76, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "REP TYPE:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(73, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "REP NAME:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(19, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(222, 24);
            this.label7.TabIndex = 17;
            this.label7.Text = "TERRITORY LABELS";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtAttnTerritory);
            this.panel1.Controls.Add(this.cboRepType);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboRepName);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnCreateTerritoryLabel);
            this.panel1.Location = new System.Drawing.Point(384, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(359, 506);
            this.panel1.TabIndex = 1;
            // 
            // btnDeselectAllProductType
            // 
            this.btnDeselectAllProductType.Location = new System.Drawing.Point(266, 195);
            this.btnDeselectAllProductType.Name = "btnDeselectAllProductType";
            this.btnDeselectAllProductType.Size = new System.Drawing.Size(75, 23);
            this.btnDeselectAllProductType.TabIndex = 20;
            this.btnDeselectAllProductType.Text = "DeSelect All";
            this.btnDeselectAllProductType.UseVisualStyleBackColor = true;
            this.btnDeselectAllProductType.Click += new System.EventHandler(this.btnDeselectAllProductType_Click);
            // 
            // btnSelectAllProductType
            // 
            this.btnSelectAllProductType.Location = new System.Drawing.Point(173, 195);
            this.btnSelectAllProductType.Name = "btnSelectAllProductType";
            this.btnSelectAllProductType.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAllProductType.TabIndex = 19;
            this.btnSelectAllProductType.Text = "Select All";
            this.btnSelectAllProductType.UseVisualStyleBackColor = true;
            this.btnSelectAllProductType.Click += new System.EventHandler(this.btnSelectAllProductType_Click);
            // 
            // frmLabelReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 506);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLabelReports";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckedListBox chkState;
        private System.Windows.Forms.ComboBox cboDistType;
        private System.Windows.Forms.CheckedListBox chkProductType;
        private System.Windows.Forms.TextBox txtAttnGeneral;
        private System.Windows.Forms.ComboBox cboRepName;
        private System.Windows.Forms.ComboBox cboRepType;
        private System.Windows.Forms.TextBox txtAttnTerritory;
        private System.Windows.Forms.Button btnCancel2;
        private System.Windows.Forms.Button btnCreateGeneralLabel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCreateTerritoryLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnDeselectAll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnDeselectAllProductType;
        private System.Windows.Forms.Button btnSelectAllProductType;
    }
}