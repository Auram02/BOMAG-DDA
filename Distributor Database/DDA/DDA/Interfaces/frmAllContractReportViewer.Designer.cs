namespace DDA.Interfaces
{
    partial class frmAllContractReportViewer
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.btnMainMenu = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnGenerateAllReport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMainMenu
            // 
            this.btnMainMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMainMenu.Location = new System.Drawing.Point(150, 133);
            this.btnMainMenu.Name = "btnMainMenu";
            this.btnMainMenu.Size = new System.Drawing.Size(77, 23);
            this.btnMainMenu.TabIndex = 20;
            this.btnMainMenu.Text = "Close";
            this.btnMainMenu.UseVisualStyleBackColor = true;
            this.btnMainMenu.Click += new System.EventHandler(this.btnMainMenu_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 98);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(214, 23);
            this.progressBar1.TabIndex = 22;
            // 
            // reportViewer1
            // 
            this.reportViewer1.DocumentMapWidth = 45;
            reportDataSource1.Name = "DDA_DataObjects_Contract";
            reportDataSource1.Value = null;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "DDA.Reports.rptContractInfoQuickview.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 127);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(47, 44);
            this.reportViewer1.TabIndex = 24;
            this.reportViewer1.Visible = false;
            // 
            // btnGenerateAllReport
            // 
            this.btnGenerateAllReport.Location = new System.Drawing.Point(31, 22);
            this.btnGenerateAllReport.Name = "btnGenerateAllReport";
            this.btnGenerateAllReport.Size = new System.Drawing.Size(175, 56);
            this.btnGenerateAllReport.TabIndex = 25;
            this.btnGenerateAllReport.Text = "Generate All Contract Report";
            this.btnGenerateAllReport.UseVisualStyleBackColor = true;
            this.btnGenerateAllReport.Click += new System.EventHandler(this.btnGenerateAllReport_Click);
            // 
            // frmAllContractReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 168);
            this.Controls.Add(this.btnGenerateAllReport);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnMainMenu);
            this.Name = "frmAllContractReportViewer";
            this.Text = "All Contract Report";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMainMenu;
        private System.Windows.Forms.ProgressBar progressBar1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Button btnGenerateAllReport;
    }
}