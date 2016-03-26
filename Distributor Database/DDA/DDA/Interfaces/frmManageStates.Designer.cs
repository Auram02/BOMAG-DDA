namespace DDA.Interfaces
{
    partial class frmManageStates
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManageStates));
            this.btnMainMenu = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.txtStateName = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lstCounties = new System.Windows.Forms.ListBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtAbbreviation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnMainMenu
            // 
            this.btnMainMenu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnMainMenu.Location = new System.Drawing.Point(499, 552);
            this.btnMainMenu.Name = "btnMainMenu";
            this.btnMainMenu.Size = new System.Drawing.Size(77, 23);
            this.btnMainMenu.TabIndex = 136;
            this.btnMainMenu.Text = "Close";
            this.btnMainMenu.UseVisualStyleBackColor = true;
            this.btnMainMenu.Click += new System.EventHandler(this.btnMainMenu_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRemove.BackColor = System.Drawing.Color.Firebrick;
            this.btnRemove.Location = new System.Drawing.Point(426, 440);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(77, 23);
            this.btnRemove.TabIndex = 135;
            this.btnRemove.Text = "REMOVE";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // txtStateName
            // 
            this.txtStateName.Location = new System.Drawing.Point(193, 495);
            this.txtStateName.Name = "txtStateName";
            this.txtStateName.Size = new System.Drawing.Size(235, 20);
            this.txtStateName.TabIndex = 133;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(62, 495);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(113, 17);
            this.label16.TabIndex = 134;
            this.label16.Text = "STATE NAME:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUpdate.Location = new System.Drawing.Point(437, 494);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(77, 23);
            this.btnUpdate.TabIndex = 132;
            this.btnUpdate.Text = "ADD";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lstCounties
            // 
            this.lstCounties.FormattingEnabled = true;
            this.lstCounties.Location = new System.Drawing.Point(65, 51);
            this.lstCounties.Name = "lstCounties";
            this.lstCounties.Size = new System.Drawing.Size(435, 381);
            this.lstCounties.TabIndex = 131;
            this.lstCounties.DoubleClick += new System.EventHandler(this.lstCounties_DoubleClick);
            this.lstCounties.SelectedIndexChanged += new System.EventHandler(this.lstCounties_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(12, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 24);
            this.label12.TabIndex = 129;
            this.label12.Text = "STATE";
            // 
            // txtAbbreviation
            // 
            this.txtAbbreviation.Location = new System.Drawing.Point(193, 523);
            this.txtAbbreviation.Name = "txtAbbreviation";
            this.txtAbbreviation.Size = new System.Drawing.Size(235, 20);
            this.txtAbbreviation.TabIndex = 137;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(42, 523);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 17);
            this.label1.TabIndex = 138;
            this.label1.Text = "ABBREVIATION:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // frmManageStates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 587);
            this.Controls.Add(this.txtAbbreviation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMainMenu);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.txtStateName);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lstCounties);
            this.Controls.Add(this.label12);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Name = "frmManageStates";
            this.Text = "frmManageStates";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Button btnMainMenu;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.TextBox txtStateName;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ListBox lstCounties;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtAbbreviation;
        private System.Windows.Forms.Label label1;
    }
}