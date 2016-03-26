namespace DDA.Interfaces
{
    partial class frmRepList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRepList));
            this.lstRep = new System.Windows.Forms.ListBox();
            this.btnSelectRep = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstRep
            // 
            this.lstRep.FormattingEnabled = true;
            this.lstRep.Location = new System.Drawing.Point(12, 57);
            this.lstRep.Name = "lstRep";
            this.lstRep.Size = new System.Drawing.Size(310, 147);
            this.lstRep.TabIndex = 0;
            // 
            // btnSelectRep
            // 
            this.btnSelectRep.Location = new System.Drawing.Point(353, 89);
            this.btnSelectRep.Name = "btnSelectRep";
            this.btnSelectRep.Size = new System.Drawing.Size(88, 44);
            this.btnSelectRep.TabIndex = 1;
            this.btnSelectRep.Text = "Select Representative";
            this.btnSelectRep.UseVisualStyleBackColor = true;
            this.btnSelectRep.Click += new System.EventHandler(this.btnSelectRep_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "SERVICE REP LIST";
            // 
            // frmRepList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 216);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelectRep);
            this.Controls.Add(this.lstRep);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRepList";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstRep;
        private System.Windows.Forms.Button btnSelectRep;
        private System.Windows.Forms.Label label1;
    }
}