namespace DDA.Interfaces
{
    partial class frmManufacturerRepList
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
            this.btnSkip = new System.Windows.Forms.Button();
            this.dgManufacturerRepList = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgManufacturerRepList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSkip
            // 
            this.btnSkip.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSkip.Location = new System.Drawing.Point(804, 321);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(61, 23);
            this.btnSkip.TabIndex = 11;
            this.btnSkip.Text = "SKIP";
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // dgManufacturerRepList
            // 
            this.dgManufacturerRepList.AllowUserToAddRows = false;
            this.dgManufacturerRepList.AllowUserToDeleteRows = false;
            this.dgManufacturerRepList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgManufacturerRepList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgManufacturerRepList.Location = new System.Drawing.Point(12, 63);
            this.dgManufacturerRepList.MaximumSize = new System.Drawing.Size(853, 245);
            this.dgManufacturerRepList.MinimumSize = new System.Drawing.Size(853, 245);
            this.dgManufacturerRepList.Name = "dgManufacturerRepList";
            this.dgManufacturerRepList.Size = new System.Drawing.Size(853, 245);
            this.dgManufacturerRepList.TabIndex = 10;
            this.dgManufacturerRepList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgManufacturerRepList_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(286, 24);
            this.label1.TabIndex = 12;
            this.label1.Text = "MANUFACTURER REP LIST";
            // 
            // frmManufacturerRepList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 356);
            this.Controls.Add(this.btnSkip);
            this.Controls.Add(this.dgManufacturerRepList);
            this.Controls.Add(this.label1);
            this.Name = "frmManufacturerRepList";
            this.Text = "frmManufacturerRepList";
            ((System.ComponentModel.ISupportInitialize)(this.dgManufacturerRepList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.DataGridView dgManufacturerRepList;
        private System.Windows.Forms.Label label1;
    }
}