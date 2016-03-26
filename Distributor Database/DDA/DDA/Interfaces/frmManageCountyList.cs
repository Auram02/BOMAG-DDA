using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DDA.Interfaces
{
    public partial class frmManageCountyList : Form
    {

        private DataSet dsCounties;
        private int curCountyID;

        int stateID;

        public frmManageCountyList()
        {
            InitializeComponent();

            DDA.DataObjects.AppData.DisableCloseButton(Handle);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            frmDistributorBranchList frmDBL = new frmDistributorBranchList();
            frmDBL.Show();
            this.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmDistributorBranchList frmDBL = new frmDistributorBranchList();
            frmDBL.Show();
            this.Close();
        }

        private void frmManageCountyList_Load(object sender, EventArgs e)
        {
            DataSet ds;
            ds = DDA.DataAccess.Location_da.GetStateList();

            int i;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                cboState.Items.Add(ds.Tables[0].Rows[i]["FullName"]);
            }

            cboSplitEastWest.Items.Add("None");
            cboSplitNorthSouth.Items.Add("None");
            cboSplitEastWest.Items.Add("East");
            cboSplitNorthSouth.Items.Add("North");
            cboSplitEastWest.Items.Add("West");
            cboSplitNorthSouth.Items.Add("South");


        }

        private void cboState_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }

        private void StateChanged()
        {
            int i;
            int countyID;

            stateID = DDA.DataAccess.Location_da.GetStateID(cboState.Text);

            // Now load new counties into the list
            lstCounties.Items.Clear();


            dsCounties = DDA.DataAccess.Location_da.GetCountyList(stateID);

            for (i = 0; i < dsCounties.Tables[0].Rows.Count; i++)
            {
                lstCounties.Items.Add(dsCounties.Tables[0].Rows[i]["CountyName"]);
                cboSplitParentCounty.Items.Add(dsCounties.Tables[0].Rows[i]["CountyName"]);
            }
        }

        void lstCounties_DoubleClick(object sender, System.EventArgs e)
        {
            curCountyID = Convert.ToInt32(dsCounties.Tables[0].Rows[lstCounties.SelectedIndex][1].ToString());
            txtCountyName.Text = lstCounties.SelectedItem.ToString();
            txtCountyName.BackColor = System.Drawing.Color.Yellow;
            btnUpdate.Text = "UPDATE";

        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cboState.Text != "")
            {
                if (btnUpdate.Text == "ADD")
                {

                    stateID = DDA.DataAccess.Location_da.GetStateID(cboState.Text);
                    DDA.DataAccess.Location_da.AddCounty(txtCountyName.Text, stateID);
                    lstCounties.Items.Add(txtCountyName.Text);

                    lstCounties.Sorted = true;
                    
                }
                else
                {

                    // curCountyID // for update
                    DDA.DataAccess.Location_da.UpdateCounty(curCountyID, txtCountyName.Text);
                    StateChanged();
                }

                txtCountyName.BackColor = System.Drawing.SystemColors.Window;
                btnUpdate.Text = "ADD";

                MessageBox.Show("Data Saved");
                txtCountyName.Text = "";

                
                cboState_SelectedIndexChanged(sender, e);
            }
            else
            {
                MessageBox.Show("Please select a state to add this county to first");
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            // if in a contract, it will be removed

            int contractCount;
            int id;
            int curIndex;

            curIndex = lstCounties.SelectedIndex;

            id = Convert.ToInt32(dsCounties.Tables[0].Rows[lstCounties.SelectedIndex][1].ToString());

            contractCount = DDA.DataAccess.Contract_da.GetCountyContractCount(id);

            if (contractCount > 0) 
            {
                if (MessageBox.Show("This county is currently associated with " + contractCount + " contract(s).  If you remove it, all contracts will no longer have it listed as a county.  Are you sure you want to continue?", "Removal Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            } else {
                if (MessageBox.Show("This county is not associated with any contracts.  Are you sure you wish to continue removing it?", "Removal Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            DDA.DataAccess.Contract_da.DeleteCountyFromContract(id);

            lstCounties.Items.RemoveAt(curIndex);
            dsCounties.Tables[0].Rows.RemoveAt(curIndex);  // update our dataset

            MessageBox.Show("County Removed Successfully");

        }

        private void lstCounties_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSplitSelectedCounty.Text = lstCounties.SelectedItem.ToString();

            int countyID = DDA.DataAccess.Location_da.GetCountyID(lblSplitSelectedCounty.Text, stateID);

            DataSet ds = DDA.DataAccess.SplitCounty_da.GetSplit(countyID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                chkSplit.Checked = true;


                cboSplitEastWest.Text  = ds.Tables[0].Rows[0]["EastWest"].ToString();
                cboSplitNorthSouth.Text = ds.Tables[0].Rows[0]["NorthSouth"].ToString();

                string countyName = DDA.DataAccess.Location_da.GetCountyName(Convert.ToInt32(ds.Tables[0].Rows[0]["fk_countyID"].ToString()));

                cboSplitParentCounty.Text = countyName;
                txtSplitLatitude.Text = ds.Tables[0].Rows[0]["latitude"].ToString();
                txtSplitLongitude.Text = ds.Tables[0].Rows[0]["longitude"].ToString();
                                
            }
            else
            {
                // clear all fields
                // uncheck the box
                chkSplit.Checked = false;

                cboSplitEastWest.SelectedIndex = 0;
                cboSplitNorthSouth.SelectedIndex = 0;
                cboSplitParentCounty.SelectedIndex = 0;
                txtSplitLatitude.Text = "";
                txtSplitLongitude.Text = "";
                
                
            }

            chkSplit_CheckedChanged(sender, e);


        }

        private void cboState_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            StateChanged();
        }

        private void cboSplitNorthSouth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chkSplit_CheckedChanged(object sender, EventArgs e)
        {
            cboSplitEastWest.Enabled = chkSplit.Checked;
            cboSplitNorthSouth.Enabled = chkSplit.Checked;
            cboSplitParentCounty.Enabled = chkSplit.Checked;
            txtSplitLatitude.Enabled = chkSplit.Checked;
            txtSplitLongitude.Enabled = chkSplit.Checked;
        }

        private void btnSplitUpdate_Click(object sender, EventArgs e)
        {
            int fakeCountyID = -1;

            string countyName = lblSplitSelectedCounty.Text;

            fakeCountyID = DataAccess.Location_da.GetCountyID(countyName, stateID);


            DataAccess.SplitCounty_da.RemoveSplit(fakeCountyID);

            if (chkSplit.Checked == true)
            {
                int nextID = DataLogic.DBA.DataLogic.GetNextID("SplitCounty", "pk_splitID");

                string eastwest, northsouth;
                double latitude, longitude;
                int parentCounty;

                parentCounty = DDA.DataAccess.Location_da.GetCountyID(cboSplitParentCounty.Text, stateID);
                fakeCountyID = DDA.DataAccess.Location_da.GetCountyID(lblSplitSelectedCounty.Text, stateID);

                if (txtSplitLatitude.Text == "")
                    txtSplitLatitude.Text = "0.00";

                if (txtSplitLongitude.Text == "")
                    txtSplitLongitude.Text = "0.00";

                latitude = Convert.ToDouble(txtSplitLatitude.Text);
                longitude = Convert.ToDouble(txtSplitLongitude.Text);
                eastwest = cboSplitEastWest.Text;
                northsouth = cboSplitNorthSouth.Text;

                try
                {
                    DDA.DataAccess.SplitCounty_da.AddSplit(nextID, fakeCountyID, parentCounty, longitude, latitude, northsouth, eastwest);

                    MessageBox.Show("The split was added/updated successfully!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while saving the split.  Error: " + ex.Message);

                }


            }
            else
            {
                MessageBox.Show("County split removed successfully!");
            }
            
        }

        
    }
}