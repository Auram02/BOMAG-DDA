using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DDA.DataObjects;

namespace DDA.Interfaces
{
    public partial class frmServiceRepList : Form
    {

        private int distributorID = 0;
        private bool repSelected;
        private DataSet dsRepList = new DataSet();
        private int repID;


        public frmServiceRepList()
        {
            InitializeComponent();
            DDA.DataObjects.AppData.DisableCloseButton(Handle);

            // TODO: Check the distributors that are associated to this person, and the others that are 
            // associated with others grey out or otherwise check...talk to tim about this.
            distributorID = DDA.DataObjects.AppData.BranchID;

            dsRepList = Representative.LoadRepList(distributorID, "Service", ref dgServiceRepList);


            if (DDA.DataObjects.AppData.FromMainDistList == true)
            {
                if (DDA.DataObjects.AppData.DistributorBranchListContractMode == false)
                    btnCancel.Visible = false;
            } else {

                repID = DDA.DataAccess.Representative_da.GetActiveRepForDistributor(distributorID, "service");

                int i;

                repSelected = false;

                for (i = 0; i < dgServiceRepList.Rows.Count; i++)
                {
                    dgServiceRepList.Rows[i].Cells[5].Value = 0;

                    if (dgServiceRepList.Rows[i].Cells["RepID"].Value.ToString() == Convert.ToString(repID))
                    {
                        dgServiceRepList.Rows[i].Cells[5].Value = 1;
                        repSelected = true;
                    }
                }
            }

            dgServiceRepList.Columns["RepID"].Visible = false;

        }


        private void btnAddRep_Click(object sender, EventArgs e)
        {

            DDA.DataObjects.AppData.RepMode = "Add";
            DDA.DataObjects.AppData.RepID = 0;
            frmServiceRepInformation frmSRI = new frmServiceRepInformation();
            frmSRI.Show();
            this.Close();
        }

        private void dgServiceRepList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                int rowIndex, columnIndex;
            rowIndex = e.RowIndex;
            columnIndex = e.ColumnIndex;
            
            int id;

            id = Convert.ToInt32(dgServiceRepList.Rows[rowIndex].Cells["RepID"].Value);

            if (columnIndex == 5)
            {

                if (repSelected == true)
                {
                    if (MessageBox.Show("Are you sure you want to change Rep?", "Confirm Representative Change", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        MessageBox.Show("Representative Change Cancelled");
                        return;
                    }
                }

                int i;

                int rowCount;
                rowCount = 0;
                string sRowCount;
                sRowCount = DDA.DataAccess.Representative_da.GetRepresentativeDistributorCount(distributorID);

                rowCount = Convert.ToInt32(sRowCount);
                bool bUnchecked;
                bUnchecked = false;

                for (i = 0; i < dgServiceRepList.Rows.Count; i++)
                {
                    if (i != rowIndex)
                    {
                        dgServiceRepList.Rows[i].Cells[columnIndex].Value = 0;
                    }
                    else
                    {
                        if (repID == Convert.ToInt32(dgServiceRepList.Rows[rowIndex].Cells["RepID"].Value.ToString()))
                        {
                            dgServiceRepList.Rows[i].Cells[columnIndex].Value = 0;
                            bUnchecked = true;
                            repID = -1;

                        }
                        else
                        {
                            dgServiceRepList.Rows[i].Cells[columnIndex].Value = 1;
                        }
                    }
                }


                repID = Convert.ToInt32(dgServiceRepList.Rows[rowIndex].Cells["RepID"].Value);

                if (bUnchecked == false)
                {
                    if (rowCount > 0)
                    {
                        // update
                        DDA.DataAccess.Representative_da.UpdateRepresentativeDistributor(repID, DDA.DataObjects.AppData.BranchID, "service");
                    }
                    else
                    {
                        // insert
                        DDA.DataAccess.Representative_da.InsertRepresentativeDistributor(repID, DDA.DataObjects.AppData.BranchID, "service");
                    }
                }
                else
                {
                    DDA.DataAccess.Representative_da.ReassignRepresentative(repID, "service", 0, true);  // fake the reassign to set it equal 0
                    repID = -1;
                }


            }

            // edit
            if (columnIndex == 7)
            {
                DDA.DataObjects.AppData.RepMode = "Edit";
                DDA.DataObjects.AppData.RepID = id;
                frmServiceRepInformation frmSRI = new frmServiceRepInformation();
                frmSRI.Show();
                this.Close();
            }

            // delete
            if (columnIndex == 8)
            {
                // Delete
                // id is the same as the current rep id;

                // check if they are assigned to any distributors
                int repCount;
                repCount = DDA.DataAccess.Representative_da.GetRepresentativeCount_Active("service", distributorID, id);

                int newID;
                newID = 0;
                if (repCount > 0)
                {
                    if (MessageBox.Show("This representative is assigned to one or more distributors. Would you like to reassign another representative to the distributor(s)", "Existing Distributor Notification", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        frmRepList frmRepList = new frmRepList("SERVICE", id);
                        frmRepList.ShowDialog();

                        newID = DDA.DataAccess.Representative_da.GetRepID(DDA.DataObjects.AppData.NewRepName);

                        DDA.DataAccess.Representative_da.ReassignRepresentative(id, "service", newID, false);

                        dgServiceRepList.Rows.RemoveAt(rowIndex);
                        int l;
                        for (l = 0; l < dgServiceRepList.Rows.Count; l++)
                        {
                            if (dgServiceRepList.Rows[l].Cells["RepID"].Value.ToString() == Convert.ToString(newID))
                                dgServiceRepList.Rows[l].Cells[5].Value = 1;
                        }

                        MessageBox.Show("Reassignment Successful.  Removal of Representative Successful.");
                    }
                    else
                    {
                        if (MessageBox.Show("Do you want to continue with the removal of this representative without assigning a new representative to their distributors?", "Confirm Removal", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            DDA.DataAccess.Representative_da.RemoveRepresentative(id, "service");

                            dgServiceRepList.Rows.RemoveAt(rowIndex);  // remove old guy
                            
                            MessageBox.Show("Removal Successful");
                        }
                        else
                        {
                            MessageBox.Show("Removal cancelled");
                            return;

                        }
                    }
                }
                else
                {

                    if (MessageBox.Show("This representative is not currently associationed to any distributors.  Are you sure you want to continue removing them?", "Confirm Removal", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DDA.DataAccess.Representative_da.RemoveRepresentative(id, "service");

                        dgServiceRepList.Rows.RemoveAt(rowIndex);  // remove old guy

                        MessageBox.Show("Removal Successful");
                    }
                    else
                    {
                        MessageBox.Show("Remove Cancelled");
                    }
                }
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmDistributorInformation frmDistInfo = new frmDistributorInformation();
            frmDistInfo.Show();
            this.Close();
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            frmDistributorList frmDistList = new frmDistributorList();

            frmDistList.Show();
            this.Close();
        }
    }
}