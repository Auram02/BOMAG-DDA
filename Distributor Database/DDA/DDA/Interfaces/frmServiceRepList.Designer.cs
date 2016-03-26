namespace DDA.Interfaces
{
    partial class frmServiceRepList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServiceRepList));
            this.dgServiceRepList = new System.Windows.Forms.DataGridView();
            this.btnAddRep = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMainMenu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgServiceRepList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgServiceRepList
            // 
            this.dgServiceRepList.AllowUserToAddRows = false;
            this.dgServiceRepList.AllowUserToDeleteRows = false;
            this.dgServiceRepList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgServiceRepList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgServiceRepList.Location = new System.Drawing.Point(16, 64);
            this.dgServiceRepList.MaximumSize = new System.Drawing.Size(759, 212);
            this.dgServiceRepList.MinimumSize = new System.Drawing.Size(759, 212);
            this.dgServiceRepList.Name = "dgServiceRepList";
            this.dgServiceRepList.Size = new System.Drawing.Size(759, 212);
            this.dgServiceRepList.TabIndex = 8;
            this.dgServiceRepList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgServiceRepList_CellContentClick);
            // 
            // btnAddRep
            // 
            this.btnAddRep.BackColor = System.Drawing.Color.LimeGreen;
            this.btnAddRep.Location = new System.Drawing.Point(692, 12);
            this.btnAddRep.Name = "btnAddRep";
            this.btnAddRep.Size = new System.Drawing.Size(83, 23);
            this.btnAddRep.TabIndex = 7;
            this.btnAddRep.Text = "Add REP";
            this.btnAddRep.UseVisualStyleBackColor = false;
            this.btnAddRep.Click += new System.EventHandler(this.btnAddRep_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "SERVICE REP LIST";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.Location = new System.Drawing.Point(99, 288);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 23);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "BACK";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnMainMenu
            // 
            this.btnMainMenu.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnMainMenu.Location = new System.Drawing.Point(16, 288);
            this.btnMainMenu.Name = "btnMainMenu";
            this.btnMainMenu.Size = new System.Drawing.Size(77, 23);
            this.btnMainMenu.TabIndex = 112;
            this.btnMainMenu.Text = "Main List";
            this.btnMainMenu.UseVisualStyleBackColor = true;
            this.btnMainMenu.Click += new System.EventHandler(this.btnMainMenu_Click);
            // 
            // frmServiceRepList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 323);
            this.Controls.Add(this.btnMainMenu);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgServiceRepList);
            this.Controls.Add(this.btnAddRep);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = global::DDA.Properties.Settings.Default.Center;
            this.Name = "frmServiceRepList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dgServiceRepList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgServiceRepList;
        private System.Windows.Forms.Button btnAddRep;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnMainMenu;
    }
}