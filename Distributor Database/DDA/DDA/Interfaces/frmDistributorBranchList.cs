using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DDA.Interfaces
{

    public partial class frmDistributorBranchList : Form
    {

        private ArrayList usedBranches = new ArrayList();
        private ArrayList checkedBranches = new ArrayList();
        private DataSet dsDist;
        private int associateColumnIndex;

        public frmDistributorBranchList()
        {
            InitializeComponent();

            DDA.DataObjects.AppData.DisableCloseButton(Handle);
            associateColumnIndex = 6;

            if (DDA.DataObjects.AppData.PreviousForm == "frmDistributorInformation")
            {
                btnSave.Visible = false;
            }
        }

        private void GetCheckedBranches()
        {
            int i;
            string message;
            message = "";
            checkedBranches.Clear();

            for (i = 0; i < dgDistributorBranchList.Rows.Count; i++)
            {
                if (Convert.ToInt32(dgDistributorBranchList.Rows[i].Cells[associateColumnIndex].Value) == 1)
                    checkedBranches.Add(dgDistributorBranchList.Rows[i].Cells[0].Value);
                message = message + "," + dgDistributorBranchList.Rows[i].Cells[associateColumnIndex].Value.ToString();
            }
            MessageBox.Show(message);
        }

        private void btnAddBranch_Click(object sender, EventArgs e)
        {

            //            DDA.DataObjects.AppData.DistributorID = 0;
            DDA.DataObjects.AppData.DistributorMode = "Add";
            DDA.DataObjects.AppData.DistributorEditType = "Branch";

            frmDistributorInformation frmBI = new frmDistributorInformation();
            frmBI.Show();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (DDA.DataObjects.AppData.DistributorBranchListContractMode == false)
            {
                DDA.DataObjects.AppData.DistributorEditType = "Main";
                frmDistributorInformation frmDistInfo = new frmDistributorInformation();
                frmDistInfo.Show();
                this.Close();
            }
            else
            {
                DDA.DataObjects.AppData.DistributorBranchListContractMode = false;
                frmCounties frmCounties = new frmCounties();
                frmCounties.Show();
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (DDA.DataObjects.AppData.DistributorBranchListContractMode == true)
            {
                // save the distributors that are checked
                int i;

                // remove old branches
                DDA.DataObjects.AppData.CurrentContract.ClearBranches();

                for (i = 0; i < dgDistributorBranchList.Rows.Count; i++)
                {

                    if (Convert.ToInt32(dgDistributorBranchList.Rows[i].Cells[associateColumnIndex].Value) == 1)
                    {
                        DDA.DataObjects.AppData.CurrentContract.AddBranch(Convert.ToInt32(dgDistributorBranchList.Rows[i].Cells[0].Value));
                    }
                }
                
                frmContractInformation frmCI = new frmContractInformation();
                frmCI.Show();

                this.Close();
            }
            else
            {
                frmTerritory frmTerritory = new frmTerritory();
                frmTerritory.Show();
                this.Close();
            }
        }

        private void dgDistributorBranchList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                // Column index for an association


                int rowIndex, columnIndex;
                //rowIndex = e.RowIndex;
                //rowIndex = Convert.ToInt32(dgDistributorBranchList.SelectedRows.ToString());
                rowIndex = Convert.ToInt32(dgDistributorBranchList.CurrentRow.Index);
                columnIndex = Convert.ToInt32(dgDistributorBranchList.CurrentCell.ColumnIndex);
                //columnIndex = e.ColumnIndex;

                int id;

                id = Convert.ToInt32(dgDistributorBranchList.Rows[rowIndex].Cells[0].Value);

                if (DDA.DataObjects.AppData.DistributorBranchListContractMode == false)
                {
                    if (columnIndex == 8)
                    {
                        // Edit
                        DDA.DataObjects.AppData.BranchID = id;
                        DDA.DataObjects.AppData.DistributorMode = "Edit";
                        DDA.DataObjects.AppData.DistributorEditType = "Branch";

                        frmDistributorInformation frmDI = new frmDistributorInformation();
                        frmDI.Show();
                        this.Close();
                    }

                    if (columnIndex == 9)
                    {
                        // Delete
                        if (MessageBox.Show("Are you sure you want to remove this branch?  This action cannot be undone.", "Confirm Removal", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            DDA.DataAccess.Distributor_da.DeleteBranchDistributor(id);
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
                }
                else
                {
                    if (columnIndex == associateColumnIndex)
                    {

                        DDA.DataObjects.AppData.DataChanged = true; // modified date changed

                        int distID;
                        distID = Convert.ToInt32(dgDistributorBranchList.Rows[rowIndex].Cells[0].Value);

                        int i;
                        bool inList;
                        inList = false;

                        for (i = 0; i < usedBranches.Count; i++)
                        {
                            if (Convert.ToInt32(usedBranches[i]) == distID)
                            {
                                inList = true;

                                if (MessageBox.Show("This branch is already assigned to another contract that shares a category with this one.  Do you want to re-assign this branch to the current contract?  (This will remove them from their old contract)", "Re-association Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    dgDistributorBranchList.Rows[rowIndex].Cells[associateColumnIndex].Value = 1;

                                    for (i = 0; i < dsDist.Tables[0].Rows.Count; i++)
                                    {
                                        if (Convert.ToInt32(dsDist.Tables[0].Rows[i]["pk_DistributorID"]) == distID)
                                        {
                                            dsDist.Tables[0].Rows[i]["srvcrep"] = 1;
                                            DDA.DataObjects.AppData.CurrentContract.AddDeleteBranch(distID);
                                            break;
                                        }
                                    }

                                    usedBranches.RemoveAt(i);
                                    DDA.DataObjects.AppData.CurrentContract.RemoveBranch(distID);
                                }
                                else
                                {
                                    // cancelled
                                    dgDistributorBranchList.Rows[rowIndex].Cells[associateColumnIndex].Value = 2;
                                }

                                break;
                            }
                        }

                        if (inList == false)
                        {
                            //int i;

                            for (i = 0; i < dsDist.Tables[0].Rows.Count; i++)
                            {
                                for (i = 0; i < dsDist.Tables[0].Rows.Count; i++)
                                {
                                    if (Convert.ToInt32(dsDist.Tables[0].Rows[i]["pk_DistributorID"]) == distID)
                                    {
                                        if (Convert.ToInt32(dsDist.Tables[0].Rows[i]["srvcrep"]) == 1)
                                        {
                                            dsDist.Tables[0].Rows[i]["srvcrep"] = 0;
                                            dgDistributorBranchList.Rows[rowIndex].Cells[associateColumnIndex].Value = 0;
                                        }
                                        else
                                        {
                                            dsDist.Tables[0].Rows[i]["srvcrep"] = 1;
                                            dgDistributorBranchList.Rows[rowIndex].Cells[associateColumnIndex].Value = 1;
                                        }
                                    }
                                }

                            }

                        }

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


        private void frmDistributorBranchList_Load(object sender, EventArgs e)
        {

            #region Old code
            //MainDistributorID = DDA.DataObjects.AppData.DistributorID;

            //DataSet ds;
            //ds = DDA.DataAccess.Distributor_da.GetDistributorBranchList(MainDistributorID);


            //if (Convert.ToInt32(ds.Tables[0].Rows.Count) != 0)
            //{

            //    dgDistributorBranchList.DataSource = ds.Tables[0];



            //    DataGridViewButtonColumn myDataCol = new DataGridViewButtonColumn();
            //    myDataCol.Text = "Edit";
            //    myDataCol.HeaderText = "ACTION";
            //    myDataCol.UseColumnTextForButtonValue = true;


            //    DataGridViewButtonColumn myDataCol2 = new DataGridViewButtonColumn();
            //    myDataCol2.Text = "Delete";
            //    myDataCol2.UseColumnTextForButtonValue = true;

            //    DataGridViewCheckBoxColumn myDataCol3 = new DataGridViewCheckBoxColumn();
            //    myDataCol3.ThreeState = true;
            //    myDataCol3.Name = "Add to Contract";
            //    myDataCol3.HeaderText = "Add to Contract";

            //    DataGridViewTextBoxColumn myDataCol4 = new DataGridViewTextBoxColumn();
            //    myDataCol4.HeaderText = "Srvc. Rep Count";
            //    myDataCol4.Width = 60;
            //    myDataCol4.DisplayIndex = 5;

            //    DataGridViewTextBoxColumn myDataCol5 = new DataGridViewTextBoxColumn();
            //    myDataCol5.HeaderText = "Terr. Rep Count";
            //    myDataCol5.Width = 60;
            //    myDataCol5.DisplayIndex = 6;
            //    myDataCol5.Name = "Terr";



            //    if (DDA.DataObjects.AppData.DistributorBranchListContractMode == false)
            //    {

            //        dgDistributorBranchList.Columns.Add(myDataCol);
            //        dgDistributorBranchList.Columns.Add(myDataCol2);
            //        //dgDistributorBranchList.Columns.Add(myDataCol4);
            //        //dgDistributorBranchList.Columns.Add(myDataCol5);


            //        int i;
            //        for (i = 0; i < dgDistributorBranchList.Rows.Count; i++)
            //        {
            //            int id;
            //            id = Convert.ToInt32(dgDistributorBranchList.Rows[i].Cells["pk_DistributorID"].Value);

            //            int srvRepCount = 0, terrRepCount = 0;

            //            srvRepCount = DDA.DataObjects.Distributor.GetRepresentativeCount("Service", id);
            //            terrRepCount = DDA.DataObjects.Distributor.GetRepresentativeCount("Territory", id);

            //            //Srvc. Rep Count
            //            dgDistributorBranchList.Rows[i].Cells["srvcrep"].Value = srvRepCount;


            //            //Terr. Rep Count
            //            dgDistributorBranchList.Rows[i].Cells["terrrep"].Value = terrRepCount;
            //        }

            //        dgDistributorBranchList.Columns["terrrep"].HeaderText = "Terr. Rep Count";
            //        dgDistributorBranchList.Columns["srvcrep"].HeaderText = "Srvc. Rep Count";


            //    }
            //    else
            //    {
            //        // true
            //        dgDistributorBranchList.Columns.Add(myDataCol3);

            //        int i;

            //        for (i = 0; i < dgDistributorBranchList.Rows.Count; i++)
            //        {
            //            dgDistributorBranchList.Rows[i].Cells["Add to Contract"].Value = 0;
            //        }

            //        dgDistributorBranchList.Columns["terrrep"].Visible = false;
            //        dgDistributorBranchList.Columns["srvcrep"].Visible = false;

            //    }

            //    dgDistributorBranchList.Columns["pk_DistributorID"].Visible = false;

            //}
            //else
            //{
            //    MessageBox.Show("This distributor does not contain any branches yet.");
            //}
            #endregion

            BindDatagrid("");

            //GetCheckedBranches();
            lblMainDistributor.Text = DDA.DataAccess.Distributor_da.GetDistributorName(DDA.DataObjects.AppData.DistributorID);


        }

        private void BindDatagrid(string searchKey)
        {

            int i;
            dsDist = DDA.DataObjects.Distributor.LoadDistList(DDA.DataObjects.AppData.DistributorID, ref dgDistributorBranchList, DDA.DataObjects.AppData.DistributorBranchListContractMode, searchKey);

            if (DDA.DataObjects.AppData.DistributorBranchListContractMode == true)
            {

                for (i = 0; i < dgDistributorBranchList.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dgDistributorBranchList.Rows[i].Cells["Associate"].Value) == 2)
                    {
                        usedBranches.Add(dgDistributorBranchList.Rows[i].Cells[0].Value);
                    }
                }
            }

            // Mark all "checked" boxes
            ArrayList tempList;
            tempList = DDA.DataObjects.AppData.CurrentContract.Branches;

            int j;

            for (i = 0; i < tempList.Count; i++)
            {
                for (j = 0; j < dgDistributorBranchList.Rows.Count; j++)
                {
                    if (tempList[i].ToString() == dgDistributorBranchList.Rows[j].Cells[0].Value.ToString())
                    {
                        dgDistributorBranchList.Rows[j].Cells[associateColumnIndex].Value = 1;
                        break;
                    }

                }
            }

            // Requested to be hidden
            dgDistributorBranchList.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msg;
            msg = "";
            int i;
            string msg2;
            msg2 = "";

            for (i = 0; i < dsDist.Tables[0].Rows.Count; i++)
            {
                msg = msg + "," + dsDist.Tables[0].Rows[i]["srvcrep"].ToString();
                msg2 = msg2 + "," + dgDistributorBranchList.Rows[i].Cells[associateColumnIndex].Value.ToString();
            }

            //MessageBox.Show(msg);
            MessageBox.Show(msg2);
        }


        private void dgDistributorBranchList_CellContentDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

            int rowIndex;
            //rowIndex = e.RowIndex;
            //rowIndex = Convert.ToInt32(dgDistributorBranchList.SelectedRows.ToString());
            rowIndex = Convert.ToInt32(dgDistributorBranchList.CurrentRow.Index);

            if (Convert.ToInt32(dsDist.Tables[0].Rows[rowIndex]["srvcrep"].ToString()) == 0)
            {
                dsDist.Tables[0].Rows[rowIndex]["srvcrep"] = 1;
                dgDistributorBranchList.Rows[rowIndex].Cells[associateColumnIndex].Value = 1;
            }
            else
            {
                dsDist.Tables[0].Rows[rowIndex]["srvcrep"] = 0;
                dgDistributorBranchList.Rows[rowIndex].Cells[associateColumnIndex].Value = 0;
            }


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string name;
            name = txtSearch.Text;

            dgDistributorBranchList.Columns.Clear();
            BindDatagrid(name);

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            dgDistributorBranchList.Columns.Clear();
            BindDatagrid("");
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {

            frmDistributorList frmDistList = new frmDistributorList();

            frmDistList.Show();
            this.Close();
        }

    }
}