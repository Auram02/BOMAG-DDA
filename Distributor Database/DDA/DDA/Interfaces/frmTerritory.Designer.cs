namespace DDA.Interfaces
{
    partial class frmTerritory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTerritory));
            this.lblChooseState = new System.Windows.Forms.Label();
            this.cboContractNumber = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkViewContract = new System.Windows.Forms.CheckBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.chkCategories = new System.Windows.Forms.CheckedListBox();
            this.lblNewContractNumberCaption = new System.Windows.Forms.Label();
            this.lblNewContractNumber = new System.Windows.Forms.Label();
            this.btnEditCategories = new System.Windows.Forms.Button();
            this.btnMainMenu = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMainDistributor = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cboParentContractNumber = new System.Windows.Forms.ComboBox();
            this.lblParentContractNumber = new System.Windows.Forms.Label();
            this.autoRadio = new System.Windows.Forms.RadioButton();
            this.manualRadio = new System.Windows.Forms.RadioButton();
            this.cachedrptFullContract1 = new DDA.Reports.CachedrptFullContract();
            this.lblParentContractNum = new System.Windows.Forms.Label();
            this.cboState = new MattBerther.Controls.AutoCompleteComboBox();
            this.SuspendLayout();
            // 
            // lblChooseState
            // 
            this.lblChooseState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChooseState.AutoSize = true;
            this.lblChooseState.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChooseState.Location = new System.Drawing.Point(231, 468);
            this.lblChooseState.Name = "lblChooseState";
            this.lblChooseState.Size = new System.Drawing.Size(142, 17);
            this.lblChooseState.TabIndex = 16;
            this.lblChooseState.Text = "CHOOSE A STATE:";
            this.lblChooseState.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cboContractNumber
            // 
            this.cboContractNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboContractNumber.FormattingEnabled = true;
            this.cboContractNumber.Location = new System.Drawing.Point(274, 52);
            this.cboContractNumber.Name = "cboContractNumber";
            this.cboContractNumber.Size = new System.Drawing.Size(155, 21);
            this.cboContractNumber.TabIndex = 0;
            this.cboContractNumber.SelectedIndexChanged += new System.EventHandler(this.cboContractNumber_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(156, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "CONTRACT #:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkViewContract
            // 
            this.chkViewContract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkViewContract.AutoSize = true;
            this.chkViewContract.BackColor = System.Drawing.Color.LimeGreen;
            this.chkViewContract.Location = new System.Drawing.Point(135, 59);
            this.chkViewContract.Name = "chkViewContract";
            this.chkViewContract.Size = new System.Drawing.Size(15, 14);
            this.chkViewContract.TabIndex = 12;
            this.chkViewContract.UseVisualStyleBackColor = false;
            this.chkViewContract.Visible = false;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnNext.Location = new System.Drawing.Point(424, 491);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(61, 23);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "NEXT";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.Location = new System.Drawing.Point(326, 491);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "BACK";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.BackColor = System.Drawing.Color.LimeGreen;
            this.btnView.Enabled = false;
            this.btnView.Location = new System.Drawing.Point(440, 52);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(64, 23);
            this.btnView.TabIndex = 1;
            this.btnView.Text = "VIEW";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "CHOOSE CATEGORIES:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkCategories
            // 
            this.chkCategories.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCategories.CheckOnClick = true;
            this.chkCategories.FormattingEnabled = true;
            this.chkCategories.Location = new System.Drawing.Point(15, 124);
            this.chkCategories.Name = "chkCategories";
            this.chkCategories.Size = new System.Drawing.Size(489, 259);
            this.chkCategories.TabIndex = 3;
            this.chkCategories.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkCategories_ItemCheck);
            this.chkCategories.SelectedIndexChanged += new System.EventHandler(this.chkCategories_SelectedIndexChanged);
            this.chkCategories.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chkCategories_MouseDown);
            // 
            // lblNewContractNumberCaption
            // 
            this.lblNewContractNumberCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNewContractNumberCaption.AutoSize = true;
            this.lblNewContractNumberCaption.Location = new System.Drawing.Point(153, 76);
            this.lblNewContractNumberCaption.Name = "lblNewContractNumberCaption";
            this.lblNewContractNumberCaption.Size = new System.Drawing.Size(115, 13);
            this.lblNewContractNumberCaption.TabIndex = 13;
            this.lblNewContractNumberCaption.Text = "New Contract Number:";
            this.lblNewContractNumberCaption.Visible = false;
            // 
            // lblNewContractNumber
            // 
            this.lblNewContractNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNewContractNumber.AutoSize = true;
            this.lblNewContractNumber.Location = new System.Drawing.Point(309, 76);
            this.lblNewContractNumber.Name = "lblNewContractNumber";
            this.lblNewContractNumber.Size = new System.Drawing.Size(10, 13);
            this.lblNewContractNumber.TabIndex = 14;
            this.lblNewContractNumber.Text = " ";
            // 
            // btnEditCategories
            // 
            this.btnEditCategories.Location = new System.Drawing.Point(13, 394);
            this.btnEditCategories.Name = "btnEditCategories";
            this.btnEditCategories.Size = new System.Drawing.Size(75, 23);
            this.btnEditCategories.TabIndex = 8;
            this.btnEditCategories.Text = "Edit List";
            this.btnEditCategories.UseVisualStyleBackColor = true;
            this.btnEditCategories.Click += new System.EventHandler(this.btnEditCategories_Click);
            // 
            // btnMainMenu
            // 
            this.btnMainMenu.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnMainMenu.Location = new System.Drawing.Point(242, 491);
            this.btnMainMenu.Name = "btnMainMenu";
            this.btnMainMenu.Size = new System.Drawing.Size(77, 23);
            this.btnMainMenu.TabIndex = 7;
            this.btnMainMenu.Text = "Main List";
            this.btnMainMenu.UseVisualStyleBackColor = true;
            this.btnMainMenu.Click += new System.EventHandler(this.btnMainMenu_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 22);
            this.label1.TabIndex = 9;
            this.label1.Text = "DISTRIBUTOR:";
            // 
            // lblMainDistributor
            // 
            this.lblMainDistributor.AutoSize = true;
            this.lblMainDistributor.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainDistributor.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblMainDistributor.Location = new System.Drawing.Point(157, 12);
            this.lblMainDistributor.Name = "lblMainDistributor";
            this.lblMainDistributor.Size = new System.Drawing.Size(193, 19);
            this.lblMainDistributor.TabIndex = 10;
            this.lblMainDistributor.Text = "BRANCH DISTRIBUTOR";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.Firebrick;
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(440, 81);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(64, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cboParentContractNumber
            // 
            this.cboParentContractNumber.FormattingEnabled = true;
            this.cboParentContractNumber.Location = new System.Drawing.Point(383, 437);
            this.cboParentContractNumber.Name = "cboParentContractNumber";
            this.cboParentContractNumber.Size = new System.Drawing.Size(121, 21);
            this.cboParentContractNumber.TabIndex = 20;
            this.cboParentContractNumber.Visible = false;
            // 
            // lblParentContractNumber
            // 
            this.lblParentContractNumber.AutoSize = true;
            this.lblParentContractNumber.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParentContractNumber.Location = new System.Drawing.Point(200, 438);
            this.lblParentContractNumber.Name = "lblParentContractNumber";
            this.lblParentContractNumber.Size = new System.Drawing.Size(177, 17);
            this.lblParentContractNumber.TabIndex = 21;
            this.lblParentContractNumber.Text = "PARENT CONTRACT #:";
            this.lblParentContractNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblParentContractNumber.Visible = false;
            // 
            // autoRadio
            // 
            this.autoRadio.AutoSize = true;
            this.autoRadio.Location = new System.Drawing.Point(418, 390);
            this.autoRadio.Name = "autoRadio";
            this.autoRadio.Size = new System.Drawing.Size(72, 17);
            this.autoRadio.TabIndex = 22;
            this.autoRadio.TabStop = true;
            this.autoRadio.Text = "Automatic";
            this.autoRadio.UseVisualStyleBackColor = true;
            this.autoRadio.Visible = false;
            this.autoRadio.CheckedChanged += new System.EventHandler(this.autoRadio_CheckedChanged);
            // 
            // manualRadio
            // 
            this.manualRadio.AutoSize = true;
            this.manualRadio.Location = new System.Drawing.Point(418, 413);
            this.manualRadio.Name = "manualRadio";
            this.manualRadio.Size = new System.Drawing.Size(60, 17);
            this.manualRadio.TabIndex = 23;
            this.manualRadio.TabStop = true;
            this.manualRadio.Text = "Manual";
            this.manualRadio.UseVisualStyleBackColor = true;
            this.manualRadio.Visible = false;
            this.manualRadio.CheckedChanged += new System.EventHandler(this.manualRadio_CheckedChanged);
            // 
            // lblParentContractNum
            // 
            this.lblParentContractNum.AutoSize = true;
            this.lblParentContractNum.Location = new System.Drawing.Point(383, 441);
            this.lblParentContractNum.Name = "lblParentContractNum";
            this.lblParentContractNum.Size = new System.Drawing.Size(110, 13);
            this.lblParentContractNum.TabIndex = 24;
            this.lblParentContractNum.Text = "lblParentContractNum";
            this.lblParentContractNum.Visible = false;
            // 
            // cboState
            // 
            this.cboState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboState.FormattingEnabled = true;
            this.cboState.LimitToList = true;
            this.cboState.Location = new System.Drawing.Point(383, 464);
            this.cboState.Name = "cboState";
            this.cboState.Size = new System.Drawing.Size(121, 21);
            this.cboState.TabIndex = 4;
            this.cboState.SelectedIndexChanged += new System.EventHandler(this.cboState_SelectedIndexChanged);
            // 
            // frmTerritory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 526);
            this.Controls.Add(this.lblParentContractNum);
            this.Controls.Add(this.manualRadio);
            this.Controls.Add(this.autoRadio);
            this.Controls.Add(this.cboParentContractNumber);
            this.Controls.Add(this.lblParentContractNumber);
            this.Controls.Add(this.cboState);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblMainDistributor);
            this.Controls.Add(this.btnMainMenu);
            this.Controls.Add(this.btnEditCategories);
            this.Controls.Add(this.lblNewContractNumber);
            this.Controls.Add(this.lblNewContractNumberCaption);
            this.Controls.Add(this.chkCategories);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chkViewContract);
            this.Controls.Add(this.cboContractNumber);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblChooseState);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = global::DDA.Properties.Settings.Default.Center;
            this.Name = "frmTerritory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TERRITORY";
            this.Load += new System.EventHandler(this.frmTerritory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }





        #endregion

        private System.Windows.Forms.Label lblChooseState;
        private System.Windows.Forms.ComboBox cboContractNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkViewContract;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox chkCategories;
        private System.Windows.Forms.Label lblNewContractNumberCaption;
        private System.Windows.Forms.Label lblNewContractNumber;
        private System.Windows.Forms.Button btnEditCategories;
        private System.Windows.Forms.Button btnMainMenu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMainDistributor;
        private System.Windows.Forms.Button btnDelete;
        private MattBerther.Controls.AutoCompleteComboBox cboState;
        private System.Windows.Forms.ComboBox cboParentContractNumber;
        private System.Windows.Forms.Label lblParentContractNumber;
        private System.Windows.Forms.RadioButton autoRadio;
        private System.Windows.Forms.RadioButton manualRadio;
        private Reports.CachedrptFullContract cachedrptFullContract1;
        private System.Windows.Forms.Label lblParentContractNum;
    }
}