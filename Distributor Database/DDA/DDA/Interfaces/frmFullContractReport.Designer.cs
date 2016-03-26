namespace DDA.Interfaces
{
    partial class frmFullContractReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFullContractReport));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrintAll = new System.Windows.Forms.Button();
            this.btnPrintDate = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.dtBegin = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstContractNumbers = new System.Windows.Forms.ListBox();
            this.lstDistributors = new System.Windows.Forms.ListBox();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(524, 478);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrintAll
            // 
            this.btnPrintAll.Location = new System.Drawing.Point(453, 368);
            this.btnPrintAll.Name = "btnPrintAll";
            this.btnPrintAll.Size = new System.Drawing.Size(116, 43);
            this.btnPrintAll.TabIndex = 1;
            this.btnPrintAll.Text = "Print All Contracts";
            this.btnPrintAll.UseVisualStyleBackColor = true;
            this.btnPrintAll.Click += new System.EventHandler(this.btnPrintAll_Click);
            // 
            // btnPrintDate
            // 
            this.btnPrintDate.Location = new System.Drawing.Point(453, 309);
            this.btnPrintDate.Name = "btnPrintDate";
            this.btnPrintDate.Size = new System.Drawing.Size(116, 43);
            this.btnPrintDate.TabIndex = 2;
            this.btnPrintDate.Text = "Print Contracts By Modified Date";
            this.btnPrintDate.UseVisualStyleBackColor = true;
            this.btnPrintDate.Click += new System.EventHandler(this.btnPrintDate_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(453, 196);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(116, 43);
            this.button3.TabIndex = 3;
            this.button3.Text = "Print Individual Contract";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(12, 273);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(563, 16);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MULTIPLE REPORTS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 460);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Printing Progress:";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(19, 476);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(406, 23);
            this.progressBar1.TabIndex = 8;
            // 
            // dtEnd
            // 
            this.dtEnd.Location = new System.Drawing.Point(248, 320);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(200, 20);
            this.dtEnd.TabIndex = 6;
            // 
            // dtBegin
            // 
            this.dtBegin.Location = new System.Drawing.Point(19, 320);
            this.dtBegin.Name = "dtBegin";
            this.dtBegin.Size = new System.Drawing.Size(200, 20);
            this.dtBegin.TabIndex = 5;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(245, 304);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(55, 13);
            this.lblEnd.TabIndex = 4;
            this.lblEnd.Text = "End Date:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 304);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Begin Date:";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(12, 107);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(562, 16);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "SINGLE REPORT";
            // 
            // lstContractNumbers
            // 
            this.lstContractNumbers.FormattingEnabled = true;
            this.lstContractNumbers.Location = new System.Drawing.Point(264, 131);
            this.lstContractNumbers.Name = "lstContractNumbers";
            this.lstContractNumbers.Size = new System.Drawing.Size(168, 108);
            this.lstContractNumbers.TabIndex = 15;
            // 
            // lstDistributors
            // 
            this.lstDistributors.FormattingEnabled = true;
            this.lstDistributors.Location = new System.Drawing.Point(19, 129);
            this.lstDistributors.Name = "lstDistributors";
            this.lstDistributors.Size = new System.Drawing.Size(200, 108);
            this.lstDistributors.TabIndex = 14;
            this.lstDistributors.SelectedIndexChanged += new System.EventHandler(this.lstDistributors_SelectedIndexChanged);
            this.lstDistributors.Click += new System.EventHandler(this.lstDistributors_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(12, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(281, 24);
            this.label12.TabIndex = 15;
            this.label12.Text = "FULL CONTRACT REPORT";
            // 
            // frmFullContractReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 511);
            this.Controls.Add(this.lstContractNumbers);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstDistributors);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnPrintAll);
            this.Controls.Add(this.dtEnd);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dtBegin);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPrintDate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFullContractReport";
            this.Load += new System.EventHandler(this.frmFullContractReport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        
        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrintAll;
        private System.Windows.Forms.Button btnPrintDate;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.DateTimePicker dtBegin;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lstContractNumbers;
        private System.Windows.Forms.ListBox lstDistributors;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label12;
    }
}