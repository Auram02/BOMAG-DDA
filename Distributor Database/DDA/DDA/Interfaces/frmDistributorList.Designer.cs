namespace DDA.Interfaces
{
    partial class frmDistributorList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDistributorList));
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddDistributor = new System.Windows.Forms.Button();
            this.dgDistributorList = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnMainMenu = new System.Windows.Forms.Button();
            this.btnSalesReps = new System.Windows.Forms.Button();
            this.btnTerritoryReps = new System.Windows.Forms.Button();
            this.btnEditList = new System.Windows.Forms.Button();
            this.txtMapReportStatus = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgDistributorList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(265, 24);
            this.label1.TabIndex = 10;
            this.label1.Text = "MAIN DISTRIBUTOR LIST";
            // 
            // btnAddDistributor
            // 
            this.btnAddDistributor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddDistributor.BackColor = System.Drawing.Color.LimeGreen;
            this.btnAddDistributor.Location = new System.Drawing.Point(1117, 12);
            this.btnAddDistributor.Name = "btnAddDistributor";
            this.btnAddDistributor.Size = new System.Drawing.Size(99, 23);
            this.btnAddDistributor.TabIndex = 1;
            this.btnAddDistributor.Text = "Add Distributor";
            this.btnAddDistributor.UseVisualStyleBackColor = false;
            this.btnAddDistributor.Click += new System.EventHandler(this.btnAddDistributor_Click);
            // 
            // dgDistributorList
            // 
            this.dgDistributorList.AllowUserToAddRows = false;
            this.dgDistributorList.AllowUserToDeleteRows = false;
            this.dgDistributorList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDistributorList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgDistributorList.Location = new System.Drawing.Point(16, 64);
            this.dgDistributorList.Name = "dgDistributorList";
            this.dgDistributorList.Size = new System.Drawing.Size(849, 245);
            this.dgDistributorList.TabIndex = 0;
            this.dgDistributorList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDistributorList_CellContentClick);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(335, 336);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 20);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 321);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Enter Partial or Full Name to Search";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(16, 336);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(313, 20);
            this.txtSearch.TabIndex = 2;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(416, 336);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(50, 20);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnMainMenu
            // 
            this.btnMainMenu.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnMainMenu.Location = new System.Drawing.Point(1127, 335);
            this.btnMainMenu.Name = "btnMainMenu";
            this.btnMainMenu.Size = new System.Drawing.Size(77, 23);
            this.btnMainMenu.TabIndex = 8;
            this.btnMainMenu.Text = "Main Menu";
            this.btnMainMenu.UseVisualStyleBackColor = true;
            this.btnMainMenu.Click += new System.EventHandler(this.btnMainMenu_Click);
            // 
            // btnSalesReps
            // 
            this.btnSalesReps.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSalesReps.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSalesReps.Location = new System.Drawing.Point(919, 335);
            this.btnSalesReps.Name = "btnSalesReps";
            this.btnSalesReps.Size = new System.Drawing.Size(77, 23);
            this.btnSalesReps.TabIndex = 6;
            this.btnSalesReps.Text = "S-REPS";
            this.btnSalesReps.UseVisualStyleBackColor = false;
            this.btnSalesReps.Click += new System.EventHandler(this.btnSalesReps_Click);
            // 
            // btnTerritoryReps
            // 
            this.btnTerritoryReps.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnTerritoryReps.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnTerritoryReps.Location = new System.Drawing.Point(836, 335);
            this.btnTerritoryReps.Name = "btnTerritoryReps";
            this.btnTerritoryReps.Size = new System.Drawing.Size(77, 23);
            this.btnTerritoryReps.TabIndex = 5;
            this.btnTerritoryReps.Text = "T-REPS";
            this.btnTerritoryReps.UseVisualStyleBackColor = false;
            this.btnTerritoryReps.Click += new System.EventHandler(this.btnTerritoryReps_Click);
            // 
            // btnEditList
            // 
            this.btnEditList.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnEditList.BackColor = System.Drawing.Color.Yellow;
            this.btnEditList.Location = new System.Drawing.Point(1002, 335);
            this.btnEditList.Name = "btnEditList";
            this.btnEditList.Size = new System.Drawing.Size(98, 23);
            this.btnEditList.TabIndex = 11;
            this.btnEditList.Text = "Edit State List";
            this.btnEditList.UseVisualStyleBackColor = false;
            this.btnEditList.Click += new System.EventHandler(this.btnEditList_Click_1);
            // 
            // txtMapReportStatus
            // 
            this.txtMapReportStatus.Location = new System.Drawing.Point(882, 64);
            this.txtMapReportStatus.Multiline = true;
            this.txtMapReportStatus.Name = "txtMapReportStatus";
            this.txtMapReportStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMapReportStatus.Size = new System.Drawing.Size(334, 245);
            this.txtMapReportStatus.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(878, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 24);
            this.label3.TabIndex = 13;
            this.label3.Text = "MAP ALERT(S)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1041, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "The maps below are out of date";
            // 
            // frmDistributorList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 368);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMapReportStatus);
            this.Controls.Add(this.btnEditList);
            this.Controls.Add(this.btnSalesReps);
            this.Controls.Add(this.btnTerritoryReps);
            this.Controls.Add(this.btnMainMenu);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgDistributorList);
            this.Controls.Add(this.btnAddDistributor);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = global::DDA.Properties.Settings.Default.Center;
            this.Name = "frmDistributorList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmDistributorList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgDistributorList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddDistributor;
        private System.Windows.Forms.DataGridView dgDistributorList;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnMainMenu;
        private System.Windows.Forms.Button btnSalesReps;
        private System.Windows.Forms.Button btnTerritoryReps;
        private System.Windows.Forms.Button btnEditList;
        private System.Windows.Forms.TextBox txtMapReportStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}