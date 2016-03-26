using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DDA.Interfaces
{
    public partial class frmDistributorList : Form
    {
        public frmDistributorList()
        {
            InitializeComponent();
            DDA.DataObjects.AppData.DisableCloseButton(Handle);

            // For service/territory rep list buttons.
            DDA.DataObjects.AppData.FromMainDistList = false;

            txtMapReportStatus.Text = DDA.DataObjects.MapReport.GetMapReport();
        }

        private void btnAddDistributor_Click(object sender, EventArgs e)
        {


            DDA.DataObjects.AppData.DistributorID = 0;
            DDA.DataObjects.AppData.DistributorMode = "Add";
            DDA.DataObjects.AppData.DistributorEditType = "Main";

            frmDistributorInformation frmDI = new frmDistributorInformation();
            frmDI.Show();
            this.Close();

        }

        private void frmDistributorList_Load(object sender, EventArgs e)
        {
            BindDatagrid("");
        }

        private void BindDatagrid(string searchKey)
        {
            DataSet dsDistributorList;
            dsDistributorList = DataAccess.Distributor_da.GetDistributorList("Main", searchKey);

            dgDistributorList.DataSource = dsDistributorList.Tables[0];



            DataGridViewButtonColumn myDataCol3 = new DataGridViewButtonColumn();
            myDataCol3.Text = "Territory";
            myDataCol3.UseColumnTextForButtonValue = true;
            myDataCol3.HeaderText = "";

            DataGridViewButtonColumn myDataCol = new DataGridViewButtonColumn();
            myDataCol.Text = "Edit";
            myDataCol.UseColumnTextForButtonValue = true;
            myDataCol.HeaderText = "ACTION";


            DataGridViewButtonColumn myDataCol2 = new DataGridViewButtonColumn();
            myDataCol2.Text = "Delete";
            myDataCol2.UseColumnTextForButtonValue = true;


            dgDistributorList.Columns.Add(myDataCol3);
            dgDistributorList.Columns.Add(myDataCol);
            dgDistributorList.Columns.Add(myDataCol2);


            dgDistributorList.Columns["pk_DistributorID"].Visible = false;
            dgDistributorList.Columns["STATE"].Width = 60;

        }

        private void dgDistributorList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

            int rowIndex, columnIndex;
            rowIndex = e.RowIndex;
            columnIndex = e.ColumnIndex;
         
            int id;
            int isManufacturerRep = 0;

            id = Convert.ToInt32(dgDistributorList.Rows[rowIndex].Cells["pk_DistributorID"].Value);
            isManufacturerRep = Convert.ToInt32(dgDistributorList.Rows[rowIndex].Cells["ManufacturerRep"].Value);

            if (columnIndex == 7)
            {
                // Edit
                DDA.DataObjects.AppData.DistributorID = id;
                DDA.DataObjects.AppData.DistributorMode = "Edit";
                DDA.DataObjects.AppData.DistributorEditType = "Main";
                DDA.DataObjects.AppData.IsManufacturerRep = isManufacturerRep;

                frmDistributorInformation frmDI = new frmDistributorInformation();
                frmDI.Show();
                this.Close();
            }

            if (columnIndex == 8)
            {
                // Delete
                if (MessageBox.Show("Are you sure you want to remove this Main Distributor?  All associated data will be removed.  This action cannot be undone.", "Confirm Removal", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DDA.DataAccess.Distributor_da.DeleteDistributor(id);
                    MessageBox.Show("Removal Successful");

                    frmDistributorList frmDistList = new frmDistributorList();

                    frmDistList.Show();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Removal Cancelled");
                }

            }

            if (columnIndex == 6)
            {
                // territory
                DDA.DataObjects.AppData.CurrentContract.ClearContractData();
                DDA.DataObjects.AppData.DistributorID = id;
                DDA.DataObjects.AppData.IsManufacturerRep = isManufacturerRep;
                DDA.DataObjects.AppData.FromMainDistList = true;

                frmTerritory frmTerritory = new frmTerritory();
                frmTerritory.Show();
                this.Close();
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
            //MessageBox.Show(rowIndex + " Column: " + columnIndex);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string name;
            name = txtSearch.Text;

            dgDistributorList.Columns.Clear();
            BindDatagrid(name);

        }

        private void btnReset_Click(object sender, EventArgs e)
        {

            dgDistributorList.Columns.Clear();
            txtSearch.Text = "";
            BindDatagrid("");
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {

            this.Close();

            DDA.DataObjects.AppData.activeMain.Show();
        }

        private void btnTerritoryReps_Click(object sender, EventArgs e)
        {
            DDA.DataObjects.AppData.FromMainDistList = true;

            frmTerritoryRepList frmTRL = new frmTerritoryRepList();
            frmTRL.Show();
            this.Close();
        }

        private void btnSalesReps_Click(object sender, EventArgs e)
        {
            DDA.DataObjects.AppData.FromMainDistList = true;

            frmServiceRepList frmSR = new frmServiceRepList();
            frmSR.Show();
            this.Close();
        }

        private void btnEditList_Click(object sender, EventArgs e)
        {
            frmManageCountyList frmMCL = new frmManageCountyList();
            frmMCL.Show();
            this.Close();
        }

        private void btnEditList_Click_1(object sender, EventArgs e)
        {
            frmManageStates fms = new frmManageStates();
            
            this.Hide();
            fms.ShowDialog();
            this.Show();
        }
    }
}