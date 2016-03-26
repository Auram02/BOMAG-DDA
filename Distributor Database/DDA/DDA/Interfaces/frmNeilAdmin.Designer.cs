namespace DDA.Interfaces
{
    partial class frmNeilAdmin
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
            this.chkCategoriesFrom = new System.Windows.Forms.CheckedListBox();
            this.chkCategoriesTo = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtConversionResults = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.chkCorrectCOContracts = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.chkCorrectTOContracts = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkCategoriesFrom
            // 
            this.chkCategoriesFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCategoriesFrom.CheckOnClick = true;
            this.chkCategoriesFrom.FormattingEnabled = true;
            this.chkCategoriesFrom.Location = new System.Drawing.Point(12, 12);
            this.chkCategoriesFrom.Name = "chkCategoriesFrom";
            this.chkCategoriesFrom.Size = new System.Drawing.Size(347, 544);
            this.chkCategoriesFrom.TabIndex = 4;
            // 
            // chkCategoriesTo
            // 
            this.chkCategoriesTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCategoriesTo.CheckOnClick = true;
            this.chkCategoriesTo.FormattingEnabled = true;
            this.chkCategoriesTo.Location = new System.Drawing.Point(405, 12);
            this.chkCategoriesTo.Name = "chkCategoriesTo";
            this.chkCategoriesTo.Size = new System.Drawing.Size(347, 499);
            this.chkCategoriesTo.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(366, 254);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "==>";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1296, 490);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 66);
            this.button1.TabIndex = 7;
            this.button1.Text = "Convert";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtConversionResults
            // 
            this.txtConversionResults.Location = new System.Drawing.Point(759, 13);
            this.txtConversionResults.Multiline = true;
            this.txtConversionResults.Name = "txtConversionResults";
            this.txtConversionResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConversionResults.Size = new System.Drawing.Size(710, 459);
            this.txtConversionResults.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1056, 490);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(173, 66);
            this.button2.TabIndex = 9;
            this.button2.Text = "Get CO + Parent Counts";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // chkCorrectCOContracts
            // 
            this.chkCorrectCOContracts.AutoSize = true;
            this.chkCorrectCOContracts.Location = new System.Drawing.Point(1056, 562);
            this.chkCorrectCOContracts.Name = "chkCorrectCOContracts";
            this.chkCorrectCOContracts.Size = new System.Drawing.Size(129, 17);
            this.chkCorrectCOContracts.TabIndex = 10;
            this.chkCorrectCOContracts.Text = "Correct  CO Contracts";
            this.chkCorrectCOContracts.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(817, 490);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(173, 66);
            this.button3.TabIndex = 11;
            this.button3.Text = "Get CO + Parent SalesLocs";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(543, 530);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(130, 39);
            this.button4.TabIndex = 12;
            this.button4.Text = "Get TO + Parent SalesLocs";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // chkCorrectTOContracts
            // 
            this.chkCorrectTOContracts.AutoSize = true;
            this.chkCorrectTOContracts.Location = new System.Drawing.Point(679, 552);
            this.chkCorrectTOContracts.Name = "chkCorrectTOContracts";
            this.chkCorrectTOContracts.Size = new System.Drawing.Size(129, 17);
            this.chkCorrectTOContracts.TabIndex = 13;
            this.chkCorrectTOContracts.Text = "Correct  TO Contracts";
            this.chkCorrectTOContracts.UseVisualStyleBackColor = true;
            // 
            // frmNeilAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1481, 581);
            this.Controls.Add(this.chkCorrectTOContracts);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.chkCorrectCOContracts);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtConversionResults);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkCategoriesTo);
            this.Controls.Add(this.chkCategoriesFrom);
            this.Name = "frmNeilAdmin";
            this.Text = "frmNeilAdmin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox chkCategoriesFrom;
        private System.Windows.Forms.CheckedListBox chkCategoriesTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtConversionResults;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox chkCorrectCOContracts;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox chkCorrectTOContracts;
    }
}