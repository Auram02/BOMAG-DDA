namespace DDA.Interfaces
{
    partial class frmDistributorBranchList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDistributorBranchList));
            this.dgDistributorBranchList = new System.Windows.Forms.DataGridView();
            this.btnAddBranch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnMainMenu = new System.Windows.Forms.Button();
            this.lblMainDistributor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgDistributorBranchList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgDistributorBranchList
            // 
            this.dgDistributorBranchList.AllowUserToAddRows = false;
            this.dgDistributorBranchList.AllowUserToDeleteRows = false;
            this.dgDistributorBranchList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDistributorBranchList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgDistributorBranchList.Location = new System.Drawing.Point(16, 64);
            this.dgDistributorBranchList.MaximumSize = new System.Drawing.Size(853, 245);
            this.dgDistributorBranchList.MinimumSize = new System.Drawing.Size(853, 245);
            this.dgDistributorBranchList.Name = "dgDistributorBranchList";
            this.dgDistributorBranchList.Size = new System.Drawing.Size(853, 245);
            this.dgDistributorBranchList.TabIndex = 0;
            this.dgDistributorBranchList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDistributorBranchList_CellContentClick);
            this.dgDistributorBranchList.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDistributorBranchList_CellContentDoubleClick);
            // 
            // btnAddBranch
            // 
            this.btnAddBranch.BackColor = System.Drawing.Color.LimeGreen;
            this.btnAddBranch.Location = new System.Drawing.Point(770, 9);
            this.btnAddBranch.Name = "btnAddBranch";
            this.btnAddBranch.Size = new System.Drawing.Size(99, 23);
            this.btnAddBranch.TabIndex = 7;
            this.btnAddBranch.Text = "Add Location";
            this.btnAddBranch.UseVisualStyleBackColor = false;
            this.btnAddBranch.Click += new System.EventHandler(this.btnAddBranch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(390, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "DISTRIBUTOR SALES LOCATION LIST";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.Location = new System.Drawing.Point(808, 333);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(61, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "NEXT";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.Location = new System.Drawing.Point(711, 333);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "BACK";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Location = new System.Drawing.Point(488, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Tester button";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(16, 336);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(313, 20);
            this.txtSearch.TabIndex = 4;
            this.txtSearch.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 321);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Enter Partial or Full Name to Search";
            this.label2.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(335, 336);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 20);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Visible = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(416, 336);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(50, 20);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Visible = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnMainMenu
            // 
            this.btnMainMenu.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnMainMenu.Location = new System.Drawing.Point(628, 332);
            this.btnMainMenu.Name = "btnMainMenu";
            this.btnMainMenu.Size = new System.Drawing.Size(77, 23);
            this.btnMainMenu.TabIndex = 3;
            this.btnMainMenu.Text = "Main List";
            this.btnMainMenu.UseVisualStyleBackColor = true;
            this.btnMainMenu.Click += new System.EventHandler(this.btnMainMenu_Click);
            // 
            // lblMainDistributor
            // 
            this.lblMainDistributor.AutoSize = true;
            this.lblMainDistributor.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainDistributor.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblMainDistributor.Location = new System.Drawing.Point(50, 37);
            this.lblMainDistributor.Name = "lblMainDistributor";
            this.lblMainDistributor.Size = new System.Drawing.Size(232, 19);
            this.lblMainDistributor.TabIndex = 10;
            this.lblMainDistributor.Text = "BRANCH DISTRIBUTOR LIST";
            // 
            // frmDistributorBranchList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 367);
            this.Controls.Add(this.lblMainDistributor);
            this.Controls.Add(this.btnMainMenu);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgDistributorBranchList);
            this.Controls.Add(this.btnAddBranch);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = global::DDA.Properties.Settings.Default.Center;
            this.Name = "frmDistributorBranchList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmDistributorBranchList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgDistributorBranchList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgDistributorBranchList;
        private System.Windows.Forms.Button btnAddBranch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnMainMenu;
        private System.Windows.Forms.Label lblMainDistributor;
    }
}