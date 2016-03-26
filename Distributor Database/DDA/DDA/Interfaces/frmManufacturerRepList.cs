using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DDA.Interfaces
{
    public partial class frmManufacturerRepList : Form
    {

        public frmManufacturerRepList()
        {
            InitializeComponent();
            BindDatagrid();
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            frmContractInformation frmCI = new frmContractInformation();
            frmCI.Show();

            this.Close();
        }

        private void BindDatagrid()
        {

            int i;
            DataSet dsDist = DDA.DataObjects.Distributor.LoadManufacturerReps(DDA.DataObjects.AppData.DistributorID);

            if (dsDist.Tables[0].Rows.Count > 0)
            {
                BindingSource bindingSource1 = new BindingSource();
                bindingSource1.Add(dsDist);

                // Initialize the DataGridView.
                dgManufacturerRepList.AutoGenerateColumns = true;
                dgManufacturerRepList.AutoSize = true;
                dgManufacturerRepList.DataSource = dsDist.Tables[0];
                

                DataGridViewButtonColumn dgbc1 = new DataGridViewButtonColumn();
                dgbc1.HeaderText = "ACTION";
                dgbc1.Text = "Select";
                dgbc1.UseColumnTextForButtonValue = true;
                dgManufacturerRepList.Columns.Add(dgbc1);

                dgManufacturerRepList.Columns["MANUFACTURER REP NAME"].Width = 300;

                // Requested to be hidden
                dgManufacturerRepList.Columns[0].Visible = false;
            }
        }


        private void dgManufacturerRepList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                // Column index for an association


                int rowIndex, columnIndex;
                //rowIndex = e.RowIndex;
                //rowIndex = Convert.ToInt32(dgDistributorBranchList.SelectedRows.ToString());
                rowIndex = Convert.ToInt32(dgManufacturerRepList.CurrentRow.Index);
                columnIndex = Convert.ToInt32(dgManufacturerRepList.CurrentCell.ColumnIndex);
                //columnIndex = e.ColumnIndex;

                int id;

                id = Convert.ToInt32(dgManufacturerRepList.Rows[rowIndex].Cells[1].Value);

                if (columnIndex == 0)
                {
                    if (MessageBox.Show("This manufacturer rep will be associated to all counties, overriding any previous associations.  This action cannot be undone.", "Confirm Removal", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // Edit
                        DDA.DataObjects.AppData.CurrentContract.ManufacturerRep = id;

                        frmContractInformation frmCI = new frmContractInformation();
                        frmCI.Show();
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Association cancelled");
                    }
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            // Debug only
            //MessageBox.Show(Convert.ToString(columnIndex));
        }
    }
}
