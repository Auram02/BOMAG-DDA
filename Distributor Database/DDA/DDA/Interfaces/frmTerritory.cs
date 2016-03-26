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
    public partial class frmTerritory : Form
    {


        private bool LoadingForm;


        public frmTerritory()
        {
            InitializeComponent();

            DDA.DataObjects.AppData.DisableCloseButton(Handle);
            DDA.DataObjects.AppData.ViewContractMode = false;

            if (DDA.DataObjects.AppData.FromMainDistList == true)
            {
                btnCancel.Visible = false;
            }

            DDA.DataObjects.AppData.DataChanged = false;

        }


        private void frmTerritory_Load(object sender, EventArgs e)
        {
            LoadingForm = true;

            DataSet ds;
            ds = DDA.DataAccess.Category_da.GetCategoryList(DDA.DataObjects.AppData.IsManufacturerRep);

            int i;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                chkCategories.Items.Add(ds.Tables[0].Rows[i]["CategoryName"]);
            }

            ds = DDA.DataAccess.Contract_da.GetContractList(DDA.DataObjects.AppData.DistributorID, true);

            cboContractNumber.Items.Add("NEW");

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                cboContractNumber.Items.Add(ds.Tables[0].Rows[i]["ContractNumber"]);
                cboParentContractNumber.Items.Add(ds.Tables[0].Rows[i]["ContractNumber"]);
            }


            // Populate state dropdown
            ds = DDA.DataAccess.Location_da.GetStateList();
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                cboState.Items.Add(ds.Tables[0].Rows[i]["FullName"]);
            }


            LoadSavedData();

            lblMainDistributor.Text = DDA.DataAccess.Distributor_da.GetDistributorName(DDA.DataObjects.AppData.DistributorID);

        }

        private void LoadSavedData()
        {
            DDA.DataObjects.AppData.DistributorEditType = "Main";
            DDA.DataObjects.AppData.DistributorMode = "Edit";

            if (DDA.DataObjects.AppData.CurrentContract.ContractNumber != "")
            {

                // load the data we have stored in current contract
                cboState.Text = DDA.DataObjects.AppData.CurrentContract.StateName;
                cboContractNumber.Text = DDA.DataObjects.AppData.CurrentContract.ContractNumber;

                int i;
                int j;
                string catName;

                ArrayList categories;

                categories = DDA.DataObjects.AppData.CurrentContract.Categories;

                for (i = 0; i < categories.Count; i++)
                {
                    catName = DDA.DataAccess.Category_da.GetCategoryName(Convert.ToInt32(categories[i]));

                    for (j = 0; j < chkCategories.Items.Count; j++)
                    {
                        if (catName == chkCategories.Items[j].ToString())
                        {
                            chkCategories.SetItemChecked(j, true);
                        }
                    }
                }

                if (DDA.DataObjects.AppData.CurrentContract.Counties.Count > 0)
                {
                    cboState.Text = DDA.DataAccess.Location_da.GetStateFullName(Convert.ToInt32(DDA.DataObjects.AppData.CurrentContract.Counties[0].ToString()));
                }

                autoRadio.Visible = false;
                manualRadio.Visible = false;

                if (DDA.DataObjects.AppData.CurrentContract.IsCountyOverlap || DDA.DataObjects.AppData.CurrentContract.IsTerritoryOverlap)
                {
                    autoRadio.Visible = true;
                    manualRadio.Visible = true;
                }

                if (DDA.DataObjects.AppData.CurrentContract.IsAuto)
                { 
                    autoRadio.Checked = true;
                    lblChooseState.Visible = false;
                    cboState.Visible = false;
                }
                else
                {
                    manualRadio.Checked = true;
                    lblChooseState.Visible = true;
                    cboState.Visible = true;
                }

                if (DDA.DataObjects.AppData.CurrentContract.IsAuto && DDA.DataObjects.AppData.CurrentContract.IsCountyOverlap)
                {
                    lblParentContractNumber.Visible = true;
                    cboParentContractNumber.SelectedText = DDA.DataObjects.AppData.CurrentContract.ParentContractNumber;
                    lblParentContractNum.Text = DDA.DataObjects.AppData.CurrentContract.ParentContractNumber;

                    if (cboContractNumber.Text == "NEW")
                    {
                        cboParentContractNumber.Visible = true;
                        lblParentContractNum.Visible = false;
                    }
                    else
                    {
                        cboParentContractNumber.Visible = false;
                        lblParentContractNum.Visible = true;
                    }

                    
                }
                else
                {
                    lblParentContractNumber.Visible = false;
                    cboParentContractNumber.Visible = false;
                    lblParentContractNum.Visible = false;
                }

                DDA.DataObjects.AppData.CurrentContract.ClearDeleteCounties();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            frmDistributorInformation frmDI = new frmDistributorInformation();
            frmDI.Show();
            this.Close();

        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            bool auto = false;

            if (autoRadio.Visible && autoRadio.Checked)
                auto = true;

            ArrayList curCats = new ArrayList();

            foreach (int curCatID in DDA.DataObjects.AppData.CurrentContract.Categories)
            {
                curCats.Add(curCatID);
            }

            ArrayList newCats = new ArrayList();
            ArrayList removedCats = new ArrayList();
            DDA.DataObjects.AppData.CurrentContract.ClearCategories();
            DDA.DataObjects.AppData.CurrentContract.ParentContractNumber = "";
            
            if (cboState.Text != "" || auto)
            {
                if (chkCategories.CheckedItems.Count > 0)
                {
                    int i;

                    if (cboContractNumber.Text == "NEW")
                    {


                        int newID;

                        newID = Convert.ToInt32(DataLogic.DBA.DataLogic.GetNextID("Contract", "ContractID"));


                        DDA.DataObjects.AppData.CurrentContract.ContractID = newID;

                        DDA.DataObjects.AppData.CurrentContract.ClearContractData();
                        DDA.DataObjects.AppData.ContractMode = "Add";
                        DDA.DataObjects.AppData.CurrentContract.ContractNumber = lblNewContractNumber.Text;

                        DDA.DataObjects.AppData.CurrentContract.ContractDate = Convert.ToString(System.DateTime.Now.ToShortDateString());
                        DDA.DataObjects.AppData.CurrentContract.ModifyDate = Convert.ToString(System.DateTime.Now.ToShortDateString());

                        if (auto)
                        {
                            DDA.DataObjects.AppData.CurrentContract.ParentContractNumber = cboParentContractNumber.Text;
                        }

                    }
                    else if (cboContractNumber.Text == "")
                    {
                        MessageBox.Show("Please select \"New\" or a contract number before proceeding.");
                        return;
                    }
                    else
                    {
                        // Add in logic for editing
                        // Get contract id and add it

                        DDA.DataObjects.AppData.ContractMode = "Edit";
                        DDA.DataObjects.AppData.CurrentContract.ContractNumber = cboContractNumber.Text;
                        // write a routine here to load the previous data
                    }

                    if ( cboState.Text.Length > 0 )
                        DDA.DataObjects.AppData.CurrentContract.StateName = cboState.Text;

                    //ArrayList newCats = new ArrayList();
                    //ArrayList removedCats = new ArrayList();
                    foreach (int curCatID in curCats)
                    {
                        bool removedCatFound = false;

                        for (i = 0; i < chkCategories.CheckedItems.Count; i++)
                        {
                            bool addedCatFound = true;
                            int checkedCatID = DDA.DataAccess.Category_da.GetCategoryID(chkCategories.CheckedItems[i].ToString());

                            if (curCatID == checkedCatID)
                                removedCatFound = true;

                            foreach (int curCatID2 in curCats)
                            {
                                if (curCatID2 == checkedCatID)
                                {
                                    addedCatFound = false;
                                    break;
                                }
                            }

                            if (addedCatFound && newCats.Contains(checkedCatID) == false)
                                newCats.Add(checkedCatID);
                        }

                        if (removedCatFound == false && removedCats.Contains(curCatID) == false)
                            removedCats.Add(curCatID);

                    }

                    foreach (int removedCatID in removedCats)
                    {
                        DDA.DataObjects.AppData.CurrentContract.ModifiedCategories.Add(removedCatID);
                    }

                    foreach (int addedCatID in newCats)
                    {
                        DDA.DataObjects.AppData.CurrentContract.ModifiedCategories.Add(addedCatID);
                    }

                    for (i = 0; i < chkCategories.CheckedItems.Count; i++)
                    {
                        DDA.DataObjects.AppData.CurrentContract.AddCategory(chkCategories.CheckedItems[i].ToString());
                    }

                    string checkItemList = "";

                    for (int z = 0; z < chkCategories.CheckedItems.Count; z++)
                    {
                        string tempCheckText = chkCategories.CheckedItems[z].ToString();

                        if (checkItemList.Length > 0)
                            checkItemList += ",";

                        checkItemList += "'" + tempCheckText + "'";

                    }

                    if (checkItemList.Length == 0)
                        checkItemList = "''";

                    DataSet ds = DDA.DataAccess.Category_da.GetCategoryListAttributes(checkItemList);

                    bool currentAllowCategoryOverlap = false;// currentCategoryOverlap = 0;
                    bool currentAllowCountyOverlap = false; // int currentCountyOverlap = 0;
                    
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["AllowCountyOverlap"].ToString() == "True")
                            currentAllowCountyOverlap = true;

                        if (dr["AllowTerritoryOverlap"].ToString() == "True")
                            currentAllowCategoryOverlap = true;

                    }

                    DDA.DataObjects.AppData.CurrentContract.IsTerritoryOverlap = (currentAllowCategoryOverlap);
                    DDA.DataObjects.AppData.CurrentContract.IsCountyOverlap = (currentAllowCountyOverlap);
                    DDA.DataObjects.AppData.CurrentContract.IsAuto = auto;

                    // if autocounties, then skip frmCounties for CATEGORY OVERLAP
                    // if countyoverlap, do not enforce the overlap rule. Auto: Child contract categories/machines/etc. would be added to any DDA reports
                    // if categoryoverlap, do not enforce the overlap rule.  Auto: All counties for all other contracts auto-assigned
                    if (auto)
                    {
                        // TODO: Set up contract to indicate autocounties
                        // Auto-assign appropriate counties to contract

                        if (currentAllowCategoryOverlap)
                        {
                            bool result = DDA.DataAccess.Contract_da.CloneDistributorCounties(DDA.DataObjects.AppData.DistributorID);

                            // If this is a manufacturer rep contract, automatically add all branches to the contract
                            if (DDA.DataObjects.AppData.IsManufacturerRep == 1)
                            {

                                ds = DDA.DataAccess.Distributor_da.GetDistributorBranchList(DDA.DataObjects.AppData.DistributorID, "", DDA.DataObjects.AppData.IsManufacturerRep);

                                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    DDA.DataObjects.AppData.CurrentContract.AddBranch(Convert.ToInt32(ds.Tables[0].Rows[i]["pk_DistributorID"].ToString()));

                                }

                                frmContractInformation frmCI = new frmContractInformation();
                                frmCI.Show();
                                this.Close();
                                return;
                            }
                            else
                            {

                                if (result)
                                {
                                    result = DDA.DataAccess.Contract_da.CloneDistributorSalesLocations(DDA.DataObjects.AppData.DistributorID);

                                    if (result)
                                    {
                                        DDA.DataObjects.AppData.DistributorBranchListContractMode = true;
                                        frmContractInformation frmCI = new frmContractInformation();
                                        frmCI.Show();
                                        this.Close();
                                        return;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error cloning Sales Locations for TO contract");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Error cloning Counties for TO contract");
                                }
                            }
                        }
                        else if (currentAllowCountyOverlap)
                        {
                            bool result = DDA.DataAccess.Contract_da.CloneContractCounties(DDA.DataObjects.AppData.CurrentContract.ParentContractNumber);

                            if (result)
                            {
                                result = DDA.DataAccess.Contract_da.CloneContractSalesLocations(DDA.DataObjects.AppData.CurrentContract.ParentContractNumber);

                                if (result)
                                {
                                    DDA.DataObjects.AppData.DistributorBranchListContractMode = true;
                                    frmContractInformation frmCI = new frmContractInformation();
                                    frmCI.Show();
                                    this.Close();
                                    return;
                                }
                                else
                                {

                                    MessageBox.Show("Error cloning Sales Locations for CO contract - Parent Contract: " + DDA.DataObjects.AppData.CurrentContract.ParentContractNumber);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Error cloning Counties for CO contract - Parent Contract: " + DDA.DataObjects.AppData.CurrentContract.ParentContractNumber);
                            }
                        }
                    }


                    frmCounties frmCounties = new frmCounties();
                    frmCounties.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please select at least one category before proceeding.");
                }
            } else {
                MessageBox.Show("Please select a state before proceeding.");
            }
        }

        private void cboContractNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            LoadingForm = true;

            // if the index changes, clear the counties
            DDA.DataObjects.AppData.CurrentContract.ClearCounties();
            DDA.DataObjects.AppData.CurrentContract.ClearStates();
            DDA.DataObjects.AppData.CurrentContract.IsAuto = false;
            DDA.DataObjects.AppData.CurrentContract.IsCountyOverlap = false;
            DDA.DataObjects.AppData.CurrentContract.IsTerritoryOverlap = false;

            int i;
            // clear the checks
            for (i = 0; i < chkCategories.Items.Count; i++)
                chkCategories.SetItemCheckState(i, CheckState.Unchecked);

            lblParentContractNumber.Visible = false;
            cboParentContractNumber.Visible = false;
            lblParentContractNum.Visible = false;
            lblChooseState.Visible = true;
            cboState.Visible = true;

            
            if (cboContractNumber.Text == "NEW")
            {
                
                btnView.Enabled = false;
                btnDelete.Enabled = false;
                lblNewContractNumber.Text =  DDA.DataObjects.AppData.CurrentContract.CreateNewContractNumber(DDA.DataObjects.AppData.DistributorID);
                lblNewContractNumberCaption.Visible = true;
                lblNewContractNumber.Visible = true;

                DDA.DataObjects.AppData.CurrentContract.ContractID = -1;
                DDA.DataObjects.AppData.CurrentContract.ContractMode = "NEW";
            } else
            {

                
                DDA.DataAccess.Contract_da.GetContract(cboContractNumber.Text);

                LoadSavedData();

                btnView.Enabled = true;
                btnDelete.Enabled = true;
                lblNewContractNumberCaption.Visible = false;
                lblNewContractNumber.Visible = false;
                DDA.DataObjects.AppData.CurrentContract.ContractMode = "UPDATE";
            }

        }

        private void btnEditCategories_Click(object sender, EventArgs e)
        {
            frmManageCategories frmMC = new frmManageCategories();

            frmMC.Show();
            this.Close();
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            frmDistributorList frmDistList = new frmDistributorList();

            frmDistList.Show();
            this.Close();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (cboContractNumber.Text == "")
            {
                MessageBox.Show("Please select a contract number before proceeding.");
                return;
            }
            else
            {
                // Add in logic for editing
                // Get contract id and add it

                DDA.DataObjects.AppData.ViewContractMode = true;
                DDA.DataObjects.AppData.ContractMode = "Edit";
                DDA.DataObjects.AppData.CurrentContract.ContractNumber = cboContractNumber.Text;
                frmContractInformation frmCI = new frmContractInformation();
                frmCI.Show();
                this.Close();
                // write a routine here to load the previous data
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // this will delete a contract
            // Contract, ContractDistributor, ContractCategory, ContractCounty
            if (MessageBox.Show("Are you sure you want to delete this contract?  This action is irreversible.", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int ContractID;
                ContractID = DDA.DataAccess.Contract_da.GetContractID(cboContractNumber.Text);

                DDA.DataAccess.Contract_da.DeleteContract(ContractID);
                int indexToRemove = cboContractNumber.SelectedIndex;

                cboContractNumber.SelectedIndex = 0;
                cboContractNumber.Items.RemoveAt(indexToRemove);

                MessageBox.Show("Delete successful");
            }
            else
            {
                MessageBox.Show("Delete cancelled");
            }


        }

        private void chkCategories_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void chkCategories_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            string itemText = chkCategories.Items[e.Index].ToString();

            if (e.NewValue == CheckState.Checked && chkCategories.SelectedItem != null)
            {
                int countyOverlap = DDA.DataAccess.Category_da.GetCategoryAllowCountyOverlap(itemText);
                int categoryOverlap = DDA.DataAccess.Category_da.GetCategoryAllowTerritoryOverlap(itemText);

                DataSet ds;
                int currentTerritoryOverlap;
                int currentCountyOverlap;
                GetCurrentTerritoryCountyOverlap(itemText, out currentTerritoryOverlap, out currentCountyOverlap);

                bool validCheck = true;

                if (currentTerritoryOverlap > 0)
                {
                    if (categoryOverlap == 0)
                        validCheck = false;
                }

                if (currentCountyOverlap > 0)
                {
                    if (countyOverlap == 0)
                        validCheck = false;
                }

                if (currentTerritoryOverlap == 0 && currentCountyOverlap == 0 && chkCategories.CheckedItems.Count > 0)
                {
                    if (categoryOverlap > 0 || countyOverlap > 0)
                        validCheck = false;
                }

                if (!validCheck)
                {
                    e.NewValue = e.CurrentValue;
                    MessageBox.Show("A contract may only contain categories that are exclusively one of the following: 1) Non-overlapping, 2) Allow Territory Overlap, 3) Allow County Overlap");
                    return;
                }

                ds = DDA.DataAccess.Category_da.GetCategoryListAttributes("'" + chkCategories.SelectedItem.ToString() + "'");

                bool isSelectedCountyOverlap = false;
                bool isSelectedTerritoryOverlap = false;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["AllowCountyOverlap"].ToString() == "True")
                        isSelectedCountyOverlap = true;

                    if (ds.Tables[0].Rows[0]["AllowTerritoryOverlap"].ToString() == "True")
                        isSelectedTerritoryOverlap = true;
                }

                manualRadio.Visible = false;
                autoRadio.Visible = false;
                
                if (isSelectedCountyOverlap || isSelectedTerritoryOverlap)
                {
                    manualRadio.Visible = true;
                    autoRadio.Visible = true;

                    manualRadio.Checked = true;

                }

            }
            else
            {
                lblParentContractNumber.Visible = false;
                cboParentContractNumber.Visible = false;
                lblParentContractNum.Visible = false;
                
                int currentTerritoryOverlap;
                int currentCountyOverlap;
                GetCurrentTerritoryCountyOverlap(itemText, out currentTerritoryOverlap, out currentCountyOverlap);

                if (currentCountyOverlap == 0 && currentTerritoryOverlap == 0)
                {
                    autoRadio.Visible = false;
                    manualRadio.Visible = false;

                    lblNewContractNumber.Text = lblNewContractNumber.Text.Replace("TO-", "");
                    lblNewContractNumber.Text = lblNewContractNumber.Text.Replace("CO-", "");
                }
            }

            if (LoadingForm == false)
            {

                DDA.DataObjects.AppData.DataChanged = true;
            }

            LoadingForm = true;
        }

        private void GetCurrentTerritoryCountyOverlap(string itemText, out int currentTerritoryOverlap, out int currentCountyOverlap)
        {
            string checkItemList = "";
            DataSet ds = new DataSet();

            for (int i = 0; i < chkCategories.CheckedItems.Count; i++)
            {
                string tempCheckText = chkCategories.CheckedItems[i].ToString();

                if (tempCheckText != itemText)
                {

                    if (checkItemList.Length > 0)
                        checkItemList += ",";

                    checkItemList += "'" + tempCheckText + "'";
                }
            }

            if (checkItemList.Length == 0)
                checkItemList = "''";

            ds = DDA.DataAccess.Category_da.GetCategoryListAttributes(checkItemList);

            currentTerritoryOverlap = 0;
            currentCountyOverlap = 0;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["AllowTerritoryOverlap"].ToString() == "True")
                    currentTerritoryOverlap++;

                if (dr["AllowCountyOverlap"].ToString() == "True")
                    currentCountyOverlap++;


            }
        }

        void chkCategories_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LoadingForm = false;
        }

        private void cboState_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void autoRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (autoRadio.Checked)
            {
                int contractCount = cboContractNumber.Items.Count;
                bool isSelectedCountyOverlap = false;
                bool isSelectedTerritoryOverlap = false;

                foreach (string item in chkCategories.CheckedItems)
                {
                    DataSet ds = DDA.DataAccess.Category_da.GetCategoryListAttributes("'" + item.ToString() + "'");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["AllowCountyOverlap"].ToString() == "True")
                            isSelectedCountyOverlap = true;

                        if (ds.Tables[0].Rows[0]["AllowTerritoryOverlap"].ToString() == "True")
                            isSelectedTerritoryOverlap = true;
                    }
                }
                
                if (contractCount == 1)
                {
                    autoRadio.Checked = false;
                    MessageBox.Show("At least one contract must exist before creating a County Overlap or Territory Overlap contract with Auto selected");
                    lblChooseState.Visible = true;
                    cboState.Visible = true;

                }
                else
                {
                    if (isSelectedTerritoryOverlap)
                    {
                        lblNewContractNumber.Text = "TO-" + lblNewContractNumber.Text;

                        lblChooseState.Visible = false;
                        cboState.Visible = false;
                    }

                    if (isSelectedCountyOverlap)
                    {
                        lblParentContractNumber.Visible = true;
                        cboParentContractNumber.Visible = true;
                        lblParentContractNum.Visible = true;

                        if (cboContractNumber.Text == "NEW")
                            cboParentContractNumber.Visible = true;
                        else
                            lblParentContractNum.Visible = true;

                        lblNewContractNumber.Text = "CO-" + lblNewContractNumber.Text;
                        
                        lblChooseState.Visible = false;
                        cboState.Visible = false;

                    }

                }
            }
        }

        private void manualRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (manualRadio.Checked)
            {
                lblNewContractNumber.Text = lblNewContractNumber.Text.Replace("TO-", "").Replace("CO-", "");

                lblParentContractNumber.Visible = false;
                cboParentContractNumber.Visible = false;
                lblParentContractNum.Visible = false;
                lblChooseState.Visible = true;
                cboState.Visible = true;

            }
        }


    }
}