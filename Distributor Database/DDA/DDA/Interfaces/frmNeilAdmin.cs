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
    public partial class frmNeilAdmin : Form
    {
        public frmNeilAdmin()
        {
            InitializeComponent();

            DataSet ds;
            ds = DDA.DataAccess.Category_da.GetCategoryList(DDA.DataObjects.AppData.IsManufacturerRep);

            int i;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                chkCategoriesFrom.Items.Add(ds.Tables[0].Rows[i]["CategoryName"]);
                chkCategoriesTo.Items.Add(ds.Tables[0].Rows[i]["CategoryName"]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtConversionResults.Text = "";

            string checkItemList = "";

            bool countyOverlap = false;
            bool territoryOverlap = false;

            for (int z = 0; z < chkCategoriesFrom.CheckedItems.Count; z++)
            {
                string tempCheckText = chkCategoriesFrom.CheckedItems[z].ToString();

                if (checkItemList.Length > 0)
                    checkItemList += ",";

                checkItemList += "'" + tempCheckText + "'";

            }

            DataSet dsFrom = DDA.DataAccess.Contract_da.GetContractListByCategoryList(checkItemList);
            DataSet dsDistributors = DDA.DataAccess.Distributor_da.GetDistributorList();

            DataSet dsCategory = DDA.DataAccess.Category_da.GetCategoryListAttributes(checkItemList);

            // Determine if county or territory overlap
            foreach (DataRow dr in dsCategory.Tables[0].Rows)
            {
                if (dr["AllowCountyOverlap"].ToString() == "True")
                    countyOverlap = true;

                if (dr["AllowTerritoryOverlap"].ToString() == "True")
                    territoryOverlap = true;

            }

            checkItemList = "";
            for (int z = 0; z < chkCategoriesTo.CheckedItems.Count; z++)
            {
                string tempCheckText = chkCategoriesTo.CheckedItems[z].ToString();

                if (checkItemList.Length > 0)
                    checkItemList += ",";


                int tempCategoryID = DDA.DataAccess.Category_da.GetCategoryID(tempCheckText);

                checkItemList += tempCategoryID.ToString();

            }

            int conversionCount = 0;
            // Have the 'from' contracts in dsFrom.
            // If it has territoryoverlap, have it create a new contract, prefaced with TO-   (and auto)
            // and have it inherit all counties from all other contracts for that distributor

            // If it has countyoverlap, have it create a new contract, prefaced with CO-   (and auto)
            foreach (DataRow dr in dsFrom.Tables[0].Rows)
            {

                DDA.DataAccess.Contract_da.GetContract(Convert.ToInt32(dr["fk_ContractID"].ToString()));
                //if (Convert.ToInt32(dr["fk_ContractID"].ToString()) == 226 || Convert.ToInt32(dr["fk_ContractID"].ToString()) == 46)
                if (1 == 1)
                {
                    // Check if distributorid exists
                    DataSet dsDistributorExists = DDA.DataAccess.Distributor_da.GetDistributorInformation(DDA.DataObjects.AppData.DistributorID);
                    bool distributorExists = false;
                    string distributorName = "";
                    foreach (DataRow drDist in dsDistributors.Tables[0].Rows)
                    {
                        if (DDA.DataObjects.AppData.DistributorID == Convert.ToInt32(drDist["pk_DistributorID"].ToString()))
                        {
                            distributorExists = true;
                            distributorName = drDist["DISTRIBUTOR NAME"].ToString();
                            break;
                        }
                    }

                    if (distributorExists)
                    {
                        txtConversionResults.Text += "Distributor: " + DDA.DataAccess.Distributor_da.GetDistributorName(AppData.DistributorID);
                        RemoveOldCategories();

                        if (countyOverlap)
                        {
                            if (DDA.DataObjects.AppData.CurrentContract.Categories.Count > 0 && DDA.DataObjects.AppData.CurrentContract.ContractNumber.StartsWith("CO-") == false)
                            {
                                AppData.CurrentContract.IsAuto = true;

                                DDA.DataAccess.Contract_da.UpdateContract();  // remove old categories from old contracts
                                LogEvent("Contract Saved");
                                string originalContractNumber = AppData.CurrentContract.ContractNumber;
                                AppData.CurrentContract.ContractNumber = "CO-" + AppData.CurrentContract.CreateNewContractNumber(DDA.DataObjects.AppData.DistributorID);
                                LogEvent("New Contract Number Generated");

                                AppData.CurrentContract.Categories.Clear();
                                LogEvent("All Existing Categories Removed");


                                AppData.CurrentContract.ParentContractNumber = originalContractNumber;
                                LogEvent("ParentContractNumber set to " + originalContractNumber);

                                int newID = Convert.ToInt32(DataLogic.DBA.DataLogic.GetNextID("Contract", "ContractID"));
                                AppData.CurrentContract.ContractID = newID;

                                AddNewCategories();

                                DDA.DataAccess.Contract_da.AddContract();
                                LogEvent("Contract Saved");

                                conversionCount++;
                            }
                        }
                        else if (territoryOverlap)
                        {
                            if (DDA.DataObjects.AppData.CurrentContract.Categories.Count > 0 && DDA.DataObjects.AppData.CurrentContract.ContractNumber.StartsWith("TO-") == false)
                            {
                                DDA.DataAccess.Contract_da.UpdateContract();  // remove old categories from old contracts
                                LogEvent("Contract Saved");

                                AppData.CurrentContract.IsAuto = true;

                                AppData.CurrentContract.ContractNumber = "TO-" + AppData.CurrentContract.CreateNewContractNumber(DDA.DataObjects.AppData.DistributorID);
                                LogEvent("New Contract Number Generated");

                                AppData.CurrentContract.Counties.Clear();
                                bool result = DDA.DataAccess.Contract_da.CloneDistributorCounties(DDA.DataObjects.AppData.DistributorID);
                                LogEvent("Counties Cloned from All Eligible Distributor Contracts");
                                int newID = Convert.ToInt32(DataLogic.DBA.DataLogic.GetNextID("Contract", "ContractID"));
                                AppData.CurrentContract.ContractID = newID;

                                AppData.CurrentContract.Categories.Clear();
                                LogEvent("All Existing Categories Removed");
                                AddNewCategories();

                                DDA.DataAccess.Contract_da.AddContract();
                                LogEvent("Contract Saved");

                                conversionCount++;
                            }
                        }
                        else
                        {
                            AppData.CurrentContract.IsAuto = false;
                            AddNewCategories();
                            DDA.DataAccess.Contract_da.UpdateContract();  // save
                            LogEvent("Contract Saved");

                            conversionCount++;
                        }

                        txtConversionResults.Text += Environment.NewLine + Environment.NewLine + "-------------------------------------------" + Environment.NewLine + Environment.NewLine;
                    }
                }
            }

            txtConversionResults.Text += "Total Contracts Converted: " + conversionCount;


            DDA.DataObjects.Contract contract = new DataObjects.Contract();

        }

        private void LogEvent(string eventText)
        {
            txtConversionResults.Text += Environment.NewLine + AppData.CurrentContract.ContractNumber + " - " + eventText;

        }

        private void RemoveOldCategories()
        {
            string catList = "";
            // just update the categories assigned to the contract
            for (int z = 0; z < chkCategoriesFrom.CheckedItems.Count; z++)
            {

                string tempCheckText = chkCategoriesFrom.CheckedItems[z].ToString();
                AppData.CurrentContract.RemoveCategory(tempCheckText);

                if (catList.Length > 0)
                    catList += ", ";

                catList += tempCheckText;
            }

            LogEvent("Removed Categories: " + catList);
        }

        private void AddNewCategories()
        {
            string catList = "";
            for (int z = 0; z < chkCategoriesTo.CheckedItems.Count; z++)
            {
                string tempCheckText = chkCategoriesTo.CheckedItems[z].ToString();
                AppData.CurrentContract.AddCategory(tempCheckText);

                if (catList.Length > 0)
                    catList += ", ";

                catList += tempCheckText;
            }

            LogEvent("Added Categories: " + catList);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string sql;
            try
            {
                sql = "SELECT ContractID, ContractNumber, ParentContract, COUNT(fk_CountyID) as CountyCount " +
                        " FROM Contract, ContractCounty " +
                        " WHERE Contract.ContractID = ContractCounty.fk_ContractID " +
                        " AND ContractNumber LIKE 'CO-%' AND IsAuto = true " +
                        " GROUP BY  ContractID, ContractNumber, ParentContract ";


                //sql = "SELECT fk_ContractID FROM ContractCategory WHERE fk_CategoryID IN (" + catList + ")";

                DataSet dsCOCounties = DataLogic.DBA.DataLogic.Read(sql);

                string output = "";
                string output2 = "";
                string output3 = "";
                string output4 = "";

                foreach (DataRow dr in dsCOCounties.Tables[0].Rows)
                {




                    sql = " SELECT ContractID, ContractNumber, COUNT(fk_CountyID) as CountyCount " +
                        " FROM Contract, ContractCounty " +
                        " WHERE Contract.ContractID = ContractCounty.fk_ContractID " +
                        " AND ContractNumber = '" + dr["ParentContract"].ToString() + "'" +
                        " GROUP BY  ContractID, ContractNumber";

                    DataSet dsParentCounty = DataLogic.DBA.DataLogic.Read(sql);

                    if (dsParentCounty.Tables[0].Rows.Count == 0)
                    {
                        output += " Parent Contract '" + dr["ParentContract"] + "' not found";
                    }
                    else
                    {
                        int coCount = Convert.ToInt32(dr["CountyCount"].ToString());
                        int parentCount = Convert.ToInt32(dsParentCounty.Tables[0].Rows[0]["CountyCount"].ToString());
                        DataRow drParent = dsParentCounty.Tables[0].Rows[0];

                        if (coCount != parentCount)
                        {
                            if (output.Length > 0)
                                output += Environment.NewLine;

                            output += dr["ContractNumber"] + " - " + dsParentCounty.Tables[0].Rows[0]["ContractNumber"] + " - " + dr["CountyCount"].ToString() + " - " + dsParentCounty.Tables[0].Rows[0]["CountyCount"].ToString();
                            output += "  MISMATCH";

                            output2 += dr["ContractID"].ToString() + ",";

                            output3 += dr["ContractID"].ToString() + " " + drParent["ContractID"] + Environment.NewLine;

                            if (chkCorrectCOContracts.Checked)
                            {
                                sql = "SELECT * FROM ContractCounty WHERE fk_ContractID = " + drParent["ContractID"].ToString();
                                DataSet dsParentCounties = DataLogic.DBA.DataLogic.Read(sql);

                                sql = "";

                                sql = "DELETE FROM ContractCounty WHERE fk_ContractID = " + dr["ContractID"].ToString();
                                DataLogic.DBA.DataLogic.Update(sql);

                                foreach (DataRow drParCounty in dsParentCounties.Tables[0].Rows)
                                {
                                    if (sql.Length > 0)
                                        sql += ",";

                                    sql = "INSERT INTO ContractCounty (fk_contractid, fk_countyID) VALUES (" + dr["ContractID"].ToString() + "," + drParCounty["fk_CountyID"].ToString() + ")";
                                    DataLogic.DBA.DataLogic.Update(sql);

                                }






                            }
                        }
                        else
                        {

                            //output += dr["ContractNumber"] + " - " + dsParentCounty.Tables[0].Rows[0]["ContractNumber"] + " - " + dr["CountyCount"].ToString() + " - " + dsParentCounty.Tables[0].Rows[0]["CountyCount"].ToString();

                        }


                    }
                }

                txtConversionResults.Text = output;
                txtConversionResults.Text += Environment.NewLine + Environment.NewLine + output2 + Environment.NewLine + Environment.NewLine + output3;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql;
            DataSet dsCOCount;

            try
            {
                sql = "SELECT ContractID, ContractNumber, ParentContract " +
                         " FROM Contract" +
                         " WHERE ContractNumber LIKE 'CO-%' AND IsAuto = true ";



                //sql = "SELECT fk_ContractID FROM ContractCategory WHERE fk_CategoryID IN (" + catList + ")";

                DataSet dsCOCounties = DataLogic.DBA.DataLogic.Read(sql);

                string output = "";
                string output2 = "";
                string output3 = "";
                string output4 = "";

                foreach (DataRow dr in dsCOCounties.Tables[0].Rows)
                {

                    sql = "SELECT count(fk_distributorid) as CountyCount FROM ContractDistributor WHERE fk_contractid = " + dr["ContractID"].ToString();

                    dsCOCount = DataLogic.DBA.DataLogic.Read(sql);

                    sql = " SELECT ContractID, ContractNumber, COUNT(fk_DistributorID) as CountyCount " +
                        " FROM Contract, ContractDistributor " +
                        " WHERE Contract.ContractID = ContractDistributor.fk_ContractID " +
                        " AND ContractNumber = '" + dr["ParentContract"].ToString() + "'" +
                        " GROUP BY  ContractID, ContractNumber";

                    DataSet dsParentCounty = DataLogic.DBA.DataLogic.Read(sql);

                    if (dsParentCounty.Tables[0].Rows.Count == 0)
                    {
                        output += " Parent Contract '" + dr["ParentContract"] + "' not found";
                    }
                    else
                    {
                        int coCount = Convert.ToInt32(dsCOCount.Tables[0].Rows[0]["CountyCount"].ToString());
                        int parentCount = Convert.ToInt32(dsParentCounty.Tables[0].Rows[0]["CountyCount"].ToString());
                        DataRow drParent = dsParentCounty.Tables[0].Rows[0];

                        if (coCount != parentCount)
                        {
                            if (output.Length > 0)
                                output += Environment.NewLine;

                            output += dr["ContractNumber"] + " - " + dsParentCounty.Tables[0].Rows[0]["ContractNumber"] + " - " + coCount.ToString() + " - " + parentCount.ToString();
                            output += "  MISMATCH";

                            output2 += dr["ContractID"].ToString() + ",";

                            output3 += dr["ContractID"].ToString() + " " + drParent["ContractID"] + Environment.NewLine;

                            if (chkCorrectCOContracts.Checked)
                            {
                                sql = "SELECT * FROM ContractDistributor WHERE fk_ContractID = " + drParent["ContractID"].ToString();
                                DataSet dsParentCounties = DataLogic.DBA.DataLogic.Read(sql);

                                sql = "";

                                sql = "DELETE FROM ContractDistributor WHERE fk_ContractID = " + dr["ContractID"].ToString();
                                DataLogic.DBA.DataLogic.Update(sql);

                                foreach (DataRow drParCounty in dsParentCounties.Tables[0].Rows)
                                {
                                    if (sql.Length > 0)
                                        sql += ",";

                                    sql = "INSERT INTO ContractDistributor (fk_contractid, fk_DistributorID) VALUES (" + dr["ContractID"].ToString() + "," + drParCounty["fk_DistributorID"].ToString() + ")";
                                    DataLogic.DBA.DataLogic.Update(sql);

                                }






                            }
                        }
                        else
                        {

                            output += dr["ContractNumber"] + " - " + dsParentCounty.Tables[0].Rows[0]["ContractNumber"] + " - " + coCount + " - " + dsParentCounty.Tables[0].Rows[0]["CountyCount"].ToString() + Environment.NewLine;

                        }


                    }
                }

                txtConversionResults.Text = output;
                txtConversionResults.Text += Environment.NewLine + Environment.NewLine + output2 + Environment.NewLine + Environment.NewLine + output3;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sql;
            DataSet dsTOCount;

            try
            {
                sql = "SELECT ContractID, ContractNumber, ParentContract, MainDistributorID " +
                         " FROM Contract" +
                         " WHERE ContractNumber LIKE 'TO-%' AND IsAuto = true ";



                //sql = "SELECT fk_ContractID FROM ContractCategory WHERE fk_CategoryID IN (" + catList + ")";

                DataSet dsTOCounties = DataLogic.DBA.DataLogic.Read(sql);

                string output = "";
                string output2 = "";
                string output3 = "";
                string output4 = "";

                foreach (DataRow dr in dsTOCounties.Tables[0].Rows)
                {

                    sql = "SELECT count(fk_distributorid) as TOCount FROM ContractDistributor WHERE fk_contractid = " + dr["ContractID"].ToString();

                    dsTOCount = DataLogic.DBA.DataLogic.Read(sql);

                    sql = " SELECT DISTINCT fk_DistributorID " +
                            " FROM Contract, ContractDistributor cd " +
                            " WHERE MainDistributorID LIKE " + dr["MainDistributorID"].ToString() +
                            " AND Contract.ContractID = cd.fk_ContractID " +
                            " AND ContractNumber NOT LIKE 'TO%' ";


                    DataSet dsParentCounty = DataLogic.DBA.DataLogic.Read(sql);

                    if (dsParentCounty.Tables[0].Rows.Count == 0)
                    {
                        output += " No Distributors found for non-TO contracts for MainDistributorID: '" + dr["MainDistributorID"].ToString() + "'";
                    }
                    else
                    {
                        int coCount = Convert.ToInt32(dsTOCount.Tables[0].Rows[0]["TOCount"].ToString());
                        int parentCount = Convert.ToInt32(dsParentCounty.Tables[0].Rows.Count.ToString());
                        DataRow drParent = dsParentCounty.Tables[0].Rows[0];

                        if (coCount != parentCount)
                        {
                            if (output.Length > 0)
                                output += Environment.NewLine;

                            output += dr["ContractNumber"] + " - " + coCount.ToString() + " - " + parentCount.ToString();
                            output += "  MISMATCH" + Environment.NewLine;
                            
                            if (chkCorrectTOContracts.Checked)
                            {
                                sql = "DELETE FROM ContractDistributor WHERE fk_ContractID = " + dr["ContractID"].ToString();
                                DataLogic.DBA.DataLogic.Update(sql);

                                foreach (DataRow drParCounty in dsParentCounty.Tables[0].Rows)
                                {
                                    if (sql.Length > 0)
                                        sql += ",";

                                    sql = "INSERT INTO ContractDistributor (fk_contractid, fk_DistributorID) VALUES (" + dr["ContractID"].ToString() + "," + drParCounty["fk_DistributorID"].ToString() + ")";
                                    DataLogic.DBA.DataLogic.Update(sql);

                                }






                            }
                        }
                        else
                        {

                            output += dr["ContractNumber"] + " - " + coCount + " - " + parentCount + Environment.NewLine;

                        }


                    }
                }

                txtConversionResults.Text = output;
                txtConversionResults.Text += Environment.NewLine + Environment.NewLine + output2 + Environment.NewLine + Environment.NewLine + output3;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
