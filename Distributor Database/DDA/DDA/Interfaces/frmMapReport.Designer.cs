namespace DDA.Interfaces
{
    partial class frmMapReport
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
            this.chkCategories = new System.Windows.Forms.CheckedListBox();
            this.chkStates = new System.Windows.Forms.CheckedListBox();
            this.btnCreateMaps = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectNone = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMapReportStatus = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // chkCategories
            // 
            this.chkCategories.CheckOnClick = true;
            this.chkCategories.FormattingEnabled = true;
            this.chkCategories.Location = new System.Drawing.Point(405, 92);
            this.chkCategories.Name = "chkCategories";
            this.chkCategories.Size = new System.Drawing.Size(348, 499);
            this.chkCategories.TabIndex = 5;
            // 
            // chkStates
            // 
            this.chkStates.CheckOnClick = true;
            this.chkStates.FormattingEnabled = true;
            this.chkStates.Location = new System.Drawing.Point(12, 92);
            this.chkStates.Name = "chkStates";
            this.chkStates.Size = new System.Drawing.Size(347, 544);
            this.chkStates.TabIndex = 6;
            // 
            // btnCreateMaps
            // 
            this.btnCreateMaps.Location = new System.Drawing.Point(943, 610);
            this.btnCreateMaps.Name = "btnCreateMaps";
            this.btnCreateMaps.Size = new System.Drawing.Size(173, 66);
            this.btnCreateMaps.TabIndex = 8;
            this.btnCreateMaps.Text = "Create Maps";
            this.btnCreateMaps.UseVisualStyleBackColor = true;
            this.btnCreateMaps.Click += new System.EventHandler(this.btnCreateMaps_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(167, 642);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(93, 34);
            this.btnSelectAll.TabIndex = 9;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.Location = new System.Drawing.Point(266, 642);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(93, 34);
            this.btnSelectNone.TabIndex = 10;
            this.btnSelectNone.Text = "Select None";
            this.btnSelectNone.UseVisualStyleBackColor = true;
            this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(405, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Select Categories";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Select States";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(237, 24);
            this.label3.TabIndex = 13;
            this.label3.Text = "CREATE MAP REPORT";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 8.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(31, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(269, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "A map will be generated for each category selected";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(941, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "The maps below are out of date";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(778, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(157, 24);
            this.label6.TabIndex = 17;
            this.label6.Text = "MAP ALERT(S)";
            // 
            // txtMapReportStatus
            // 
            this.txtMapReportStatus.Location = new System.Drawing.Point(782, 92);
            this.txtMapReportStatus.Multiline = true;
            this.txtMapReportStatus.Name = "txtMapReportStatus";
            this.txtMapReportStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMapReportStatus.Size = new System.Drawing.Size(334, 499);
            this.txtMapReportStatus.TabIndex = 16;
            // 
            // frmMapReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 682);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtMapReportStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelectNone);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnCreateMaps);
            this.Controls.Add(this.chkStates);
            this.Controls.Add(this.chkCategories);
            this.Name = "frmMapReport";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox chkCategories;
        private System.Windows.Forms.CheckedListBox chkStates;
        private System.Windows.Forms.Button btnCreateMaps;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSelectNone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMapReportStatus;
    }
}