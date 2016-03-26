using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace DDA.Interfaces
{
    public partial class frmCounties : Form
    {
        private int currentStateID;
        private DataSet dsChosenCounties;
        private bool isLoading;
        private bool LoadingForm;

        private ArrayList _curCountyList = new ArrayList();

        public frmCounties()
        {
            InitializeComponent();
            DDA.DataObjects.AppData.DisableCloseButton(Handle);
            DDA.DataObjects.AppData.PreviousForm = "frmCounties";
            LoadingForm = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int countyID;
            int stateID;

            stateID = DDA.DataAccess.Location_da.GetStateID(cboState.Text);



            DDA.DataAccess.Contract_da.DeleteCounties_byState_ContractID(stateID, DDA.DataObjects.AppData.CurrentContract.ContractID);

            //DDA.DataObjects.AppData.CurrentContract.ClearCounties();

            int i;
            string tempStr;
            tempStr = "";

            ArrayList checkedCounties = new ArrayList();

            for (i = 0; i < chklbCounties.Items.Count; i++)
            {

                // Debug only
                //if (chklbCounties.GetItemCheckState(i) == CheckState.Indeterminate)
                //    MessageBox.Show(chklbCounties.Items[i].ToString());

                if (chklbCounties.GetItemCheckState(i) == CheckState.Checked)
                {
                    // Add in state restriction
                    countyID = DDA.DataAccess.Location_da.GetCountyID(Convert.ToString(chklbCounties.Items[i].ToString()), currentStateID);

                    DDA.DataObjects.AppData.CurrentContract.AddCounty(countyID);
                    tempStr = tempStr + "," + Convert.ToString(countyID);

                    checkedCounties.Add(countyID);

                }
            }

            BuildModifiedCountyList(checkedCounties);

            // debug only
            //MessageBox.Show(tempStr);

            // If this is a manufacturer rep contract, automatically add all branches to the contract
            if (DDA.DataObjects.AppData.IsManufacturerRep == 1)
            {

                DataSet ds = DDA.DataAccess.Distributor_da.GetDistributorBranchList(DDA.DataObjects.AppData.DistributorID, "", DDA.DataObjects.AppData.IsManufacturerRep);

                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DDA.DataObjects.AppData.CurrentContract.AddBranch(Convert.ToInt32(ds.Tables[0].Rows[i]["pk_DistributorID"].ToString()));

                }

                frmContractInformation frmCI = new frmContractInformation();
                frmCI.Show();
                this.Close();
            }
            else
            {
                DDA.DataObjects.AppData.DistributorBranchListContractMode = true;
                frmDistributorBranchList frmDBL = new frmDistributorBranchList();
                frmDBL.Show();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            frmTerritory frmTerritory = new frmTerritory();
            frmTerritory.Show();
            this.Close();
        }

        private void frmCounties_Load(object sender, EventArgs e)
        {
            LoadingForm = true;
            isLoading = true;

            DataSet ds;
            ds = DDA.DataAccess.Location_da.GetStateList();

            int i;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                cboState.Items.Add(ds.Tables[0].Rows[i]["FullName"]);
            }

            cboState.Text = DDA.DataObjects.AppData.CurrentContract.StateName;

            int stateID;
            stateID = DDA.DataObjects.AppData.CurrentContract.StateID;

            ds = DDA.DataAccess.Location_da.GetCountyList(stateID);


            //for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    chklbCounties.Items.Add(ds.Tables[0].Rows[i]["CountyName"]);

            //}

            int j;

            ArrayList counties;
            counties = DDA.DataObjects.AppData.CurrentContract.Counties;
            string countyName;
            int CountyID;

            for (i = 0; i < counties.Count; i++)
            {
                CountyID = Convert.ToInt32(counties[i]);

                if (DDA.DataAccess.Location_da.CheckIfCountyIDIsInState(CountyID, currentStateID) == true)
                {
                    countyName = Convert.ToString(DDA.DataAccess.Location_da.GetCountyName(CountyID));

                    for (j = 0; j < chklbCounties.Items.Count; j++)
                    {
                        if (countyName == chklbCounties.Items[j].ToString())
                        {
                            chklbCounties.SetItemCheckState(j, CheckState.Checked);
                            _curCountyList.Add(CountyID);
                        }
                    }

                }
            }


            CheckOccupiedCounties(currentStateID);


        }

        private void CheckOccupiedCounties(int currentStateID)
        {
            string countyName;
            int i, j;
            bool allowAllCategoriesToOverlap = true;
            bool allowAllCountyOverlap = true;
            bool isManufacturerRep = false;

            //DataSet ds = DDA.DataAccess.Contract_da.GetContractDistributorDataset(DDA.DataObjects.AppData.CurrentContract.ContractID);

            //if (Convert.ToInt32(ds.Tables[0].Rows[0]["ManufacturerRep"].ToString()) == 1)
            //{
            //    isManufacturerRep = true;
            //}

            dsChosenCounties = DDA.DataAccess.Contract_da.GetOccupiedCounties(currentStateID, DDA.DataObjects.AppData.CurrentContract.Categories, DDA.DataObjects.AppData.CurrentContract.ContractID, DDA.DataObjects.AppData.CurrentContract.DeleteCounties, DDA.DataObjects.AppData.IsManufacturerRep);
            DataSet dsOverlapCounties = new DataSet();

            foreach (int categoryId in DDA.DataObjects.AppData.CurrentContract.Categories)
            {
                if (DDA.DataAccess.Category_da.GetCategoryAllowTerritoryOverlap(categoryId) == 0)
                {
                    allowAllCategoriesToOverlap = false;
                }

                if (DDA.DataAccess.Category_da.GetCategoryAllowCountyOverlap(categoryId) == 0)
                    allowAllCountyOverlap = false;
            }

            for (i = 0; i < dsChosenCounties.Tables[0].Rows.Count; i++)
            {
                countyName = dsChosenCounties.Tables[0].Rows[i][0].ToString();

                for (j = 0; j < chklbCounties.Items.Count; j++)
                {
                    if (countyName == chklbCounties.Items[j].ToString())
                    {

                        //if (allowAllCategoriesToOverlap == false && isManufacturerRep)
                        //{
                        if (allowAllCountyOverlap == false)
                            chklbCounties.SetItemCheckState(j, CheckState.Indeterminate);
                        //}
                    }
                }

            }

            if (allowAllCategoriesToOverlap)
            {
                dsOverlapCounties = DDA.DataAccess.Contract_da.GetCountiesForOverlap(currentStateID, DDA.DataObjects.AppData.DistributorID);

                foreach (DataRow dr in dsOverlapCounties.Tables[0].Rows)
                {
                    countyName = dr["CountyName"].ToString();

                    for (j = 0; j < chklbCounties.Items.Count; j++)
                    {
                        if (countyName == chklbCounties.Items[j].ToString())
                        {
                            chklbCounties.SetItemCheckState(j, CheckState.Checked);
                            break;
                        }
                    }
                }
            }
        }

        private void chklbCounties_Click(object sender, EventArgs e)
        {

            // Check to see which counties have been chosen for the category already
            // see if the item that was clicked is the same as one in the list
            // if so, prompt the user to reassociate
            //            chklbCounties.SetItemCheckState(j, CheckState.Checked);

            // put in the stupid logic to handle how to change the checkstate...ugh.

            if (!(DDA.DataObjects.AppData.CurrentContract.IsCountyOverlap || DDA.DataObjects.AppData.CurrentContract.IsTerritoryOverlap))
            {

                try
                {

                    string selectedCounty;
                    string tempCounty;
                    selectedCounty = chklbCounties.SelectedItem.ToString();

                    int i;

                    for (i = 0; i < dsChosenCounties.Tables[0].Rows.Count; i++)
                    {
                        tempCounty = dsChosenCounties.Tables[0].Rows[i][0].ToString();
                        if (selectedCounty == tempCounty)
                        {
                            string distName;
                            string contractNum;
                            int contractID;
                            contractID = Convert.ToInt32(dsChosenCounties.Tables[0].Rows[i]["fk_ContractID"].ToString());

                            distName = DDA.DataAccess.Contract_da.GetContractDistributor2(contractID);
                            contractNum = DDA.DataAccess.Contract_da.GetContractNumber(contractID);

                            int isManufacturerRep = DDA.DataAccess.Contract_da.IsManufacturerRep(contractID);

                            string contractTypeMessage = "";

                            if (isManufacturerRep == 1)
                            {
                                contractTypeMessage = " (Manufacturer Rep Contract) ";
                            }

                            string message = "This county is currently associated to contract number '" + contractNum + "' under the distributor '" + distName + "' within this category" + contractTypeMessage + ".  Are you sure you wish to continue?";

                            if (MessageBox.Show(message, "Confirm Re-association", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {

                                // remove the row we found
                                DDA.DataObjects.AppData.CurrentContract.AddDeleteCounty(selectedCounty, currentStateID);
                                dsChosenCounties.Tables[0].Rows.RemoveAt(i);
                                chklbCounties.SetItemCheckState(chklbCounties.SelectedIndex, CheckState.Checked);
                                _curCountyList.Add(DDA.DataAccess.Location_da.GetCountyID(selectedCounty, currentStateID));
                                return;

                            }
                            else
                            {
                                // they decided not to re-associate.  continue
                                chklbCounties.SetItemCheckState(chklbCounties.SelectedIndex, CheckState.Indeterminate);
                                return;
                            }

                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void chklbCounties_ItemCheck(object sender, EventArgs e)
        {
            if (LoadingForm == false)
            {
                DDA.DataObjects.AppData.DataChanged = true;  // Modified
            }

            LoadingForm = true;
        }

        private void cboState_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void StateChanged()
        {
            //DDA.DataObjects.AppData.CurrentContract.ClearCounties();

            int i;
            int countyID;
            int stateID;

            stateID = DDA.DataAccess.Location_da.GetStateID(cboState.Text);

            if (isLoading == false)
            {
                //DDA.DataAccess.Contract_da.DeleteCounties_byState_ContractID(stateID, DDA.DataObjects.AppData.CurrentContract.ContractID);

            }

            ArrayList checkedCounties = new ArrayList();

            for (i = 0; i < chklbCounties.Items.Count; i++)
            {
                if (chklbCounties.GetItemCheckState(i) == CheckState.Checked)
                {
                    countyID = DDA.DataAccess.Location_da.GetCountyID(Convert.ToString(chklbCounties.Items[i]), currentStateID);
                    if (countyID > -1)
                    {
                        DDA.DataObjects.AppData.CurrentContract.AddCounty(countyID);
                        checkedCounties.Add(countyID);
                    }
                }
            }

            BuildModifiedCountyList(checkedCounties);
            
            //DDA.DataObjects.AppData.DistributorBranchListContractMode = true;
            //frmDistributorBranchList frmDBL = new frmDistributorBranchList();
            //frmDBL.Show();
            //this.Close();

            currentStateID = DDA.DataAccess.Location_da.GetStateID(cboState.Text);


            // Now load new counties into the list
            chklbCounties.Items.Clear();

            DataSet ds = new DataSet();

            ds = DDA.DataAccess.Location_da.GetCountyList(currentStateID);


            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                chklbCounties.Items.Add(ds.Tables[0].Rows[i]["CountyName"]);
            }

            int j;

            ArrayList counties;
            counties = DDA.DataObjects.AppData.CurrentContract.Counties;
            string countyName;
            int CountyID;
            _curCountyList.Clear();

            for (i = 0; i < counties.Count; i++)
            {
                CountyID = Convert.ToInt32(counties[i]);

                if (DDA.DataAccess.Location_da.CheckIfCountyIDIsInState(CountyID, currentStateID) == true)
                {
                    countyName = Convert.ToString(DDA.DataAccess.Location_da.GetCountyName(CountyID));

                    for (j = 0; j < chklbCounties.Items.Count; j++)
                    {
                        if (countyName == chklbCounties.Items[j].ToString())
                        {
                            chklbCounties.SetItemCheckState(j, CheckState.Checked);
                            _curCountyList.Add(CountyID);
                        }
                    }

                }
            }

            CheckOccupiedCounties(currentStateID);
            isLoading = false;
        }

        private void BuildModifiedCountyList(ArrayList checkedCounties)
        {
            ArrayList newCounties = new ArrayList();
            ArrayList removedCounties = new ArrayList();

            if (checkedCounties.Count > 0)
            {
                foreach (int checkedCountyID in checkedCounties)
                {
                    bool removedCounty = false;
                    if (_curCountyList.Count > 0)
                    {
                        foreach (int curCountyID in _curCountyList)
                        {
                            bool addedCounty = true;

                            if (curCountyID == checkedCountyID)
                                removedCounty = true;


                            foreach (int checkedCountyID2 in checkedCounties)
                            {
                                if (checkedCountyID2 == curCountyID)
                                {
                                    addedCounty = false;
                                    break;
                                }
                            }

                            if (addedCounty && newCounties.Contains(curCountyID) == false)
                                newCounties.Add(curCountyID);
                        }

                        if (removedCounty == false && removedCounties.Contains(checkedCountyID) == false)
                            removedCounties.Add(checkedCountyID);
                    }
                    else
                    {
                        if (newCounties.Contains(checkedCountyID) == false)
                            newCounties.Add(checkedCountyID);
                    }
                }
            }
            else
            {
                foreach (int curCountyID in _curCountyList)
                    if (removedCounties.Contains(curCountyID) == false)
                        removedCounties.Add(curCountyID);
            }

            foreach (int newCountyID in newCounties)
            {
                if (DDA.DataObjects.AppData.CurrentContract.ModifiedCounties.Contains(newCountyID) == false)
                    DDA.DataObjects.AppData.CurrentContract.ModifiedCounties.Add(newCountyID);
            }

            foreach (int removedCountyID in removedCounties)
            {
                if (DDA.DataObjects.AppData.CurrentContract.ModifiedCounties.Contains(removedCountyID) == false)
                    DDA.DataObjects.AppData.CurrentContract.ModifiedCounties.Add(removedCountyID);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            int i, j;

            for (i = 0; i < chklbCounties.Items.Count; i++)
            {
                if (chklbCounties.GetItemCheckState(i) == CheckState.Indeterminate)
                {
                    if (MessageBox.Show("One or more of the counties have already been selected in another contract for one of the categories in use.  This action will re-associate those counties with this contract.  Are you sure you want to continue?", "Re-association Alert", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        i = chklbCounties.Items.Count + 2;
                    }
                }
            }

            string tempCounty;
            string selectedCounty;

            for (i = 0; i < chklbCounties.Items.Count; i++)
            {
                // special processing if indeterminate
                if (chklbCounties.GetItemCheckState(i) == CheckState.Indeterminate)
                {
                    selectedCounty = chklbCounties.Items[i].ToString();

                    for (j = 0; j < dsChosenCounties.Tables[0].Rows.Count; j++)
                    {
                        tempCounty = dsChosenCounties.Tables[0].Rows[j][0].ToString();

                        if (tempCounty == selectedCounty)  // found a match
                        {
                            DDA.DataObjects.AppData.CurrentContract.AddDeleteCounty(selectedCounty, currentStateID);
                            dsChosenCounties.Tables[0].Rows.RemoveAt(j);
                        }
                    }
                }
                // check each item as we loop through
                chklbCounties.SetItemCheckState(i, CheckState.Checked);
            }



        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            int i;

            for (i = 0; i < chklbCounties.Items.Count; i++)
            {
                if (chklbCounties.GetItemCheckState(i) != CheckState.Indeterminate)
                {
                    chklbCounties.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }

        private void chklbCounties_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            frmDistributorList frmDistList = new frmDistributorList();

            frmDistList.Show();
            this.Close();
        }



        void chklbCounties_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LoadingForm = false;
        }

        private void cboState_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            StateChanged();
        }

        private void btnEditList_Click(object sender, EventArgs e)
        {
            frmManageCountyList fmcl = new frmManageCountyList();
            this.Hide();
            fmcl.ShowDialog();
            this.Show();

            StateChanged();
        }

    }
}