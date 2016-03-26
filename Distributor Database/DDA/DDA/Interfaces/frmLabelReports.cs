using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices; // For COMException
using Microsoft.Office.Interop.Excel;

using System.Reflection; // For Missing.Value and BindingFlags

namespace DDA.Interfaces
{
    public partial class frmLabelReports : Form
    {


        public frmLabelReports() 
        {
            InitializeComponent();
            DDA.DataObjects.AppData.DisableCloseButton(Handle);  // disable "X"
            InitializeData();
        }

        private void InitializeData()
        {
                
            // Categories
            DataSet ds;
            ds = DDA.DataAccess.Category_da.GetCategoryList();

            int i;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                chkProductType.Items.Add(ds.Tables[0].Rows[i]["CategoryName"]);
            }


            // Distributor types
            cboDistType.Sorted = false;
            cboDistType.Items.Add("MAIN");
            cboDistType.Items.Add("SALES");
            cboDistType.Items.Add("ALL");


            // States
            ds = DDA.DataAccess.Location_da.GetStateList();
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                chkState.Items.Add(ds.Tables[0].Rows[i]["FullName"]);
            }

            // Rep Type
            cboRepType.Items.Add("T-REP");
            cboRepType.Items.Add("S-REP");

            // Rep Name
            cboRepName.Items.Add("None");

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CloseScreen();
        }

        private void btnCancel2_Click(object sender, EventArgs e)
        {
            CloseScreen();
        }

        private void CloseScreen()
        {
            this.Close();
        }

        private void btnCreateGeneralLabel_Click(object sender, EventArgs e)
        {
//            MessageBox.Show("This features has not been implemented yet.");
            

            // Ensure we have good data
            if (txtAttnGeneral.Text != "" && chkProductType.CheckedItems.Count > 0 && chkState.CheckedItems.Count > 0 && cboDistType.Text !="")
            {

                System.Data.DataTable dt = new System.Data.DataTable();

                // Get list of states
                string sStates = "", abbreviation = "";
                for (int i = 0; i < chkState.CheckedItems.Count; i++)
                {
                    if (sStates != "")
                        sStates = sStates + ", ";

                    abbreviation = DDA.DataAccess.Location_da.GetStateAbbreviation(chkState.CheckedItems[i].ToString());
                    sStates = sStates + "'" + abbreviation + "'";
                }

                // Get list of cats
                string sCats = "";

                for (int i = 0; i < chkProductType.CheckedItems.Count; i++)
                {
                    if (sCats != "")
                        sCats = sCats + ", ";

                    sCats = sCats + "'" + chkProductType.CheckedItems[i].ToString() + "'";
                }

                // This is screwy...but whatever.  If it works, cool.
                // Sending 0 to the getmaindistributor method for "maindist" seems to return all main distributors.
                // I do not want to bother explaining why right now, but basically it works.  When I send 1, I don't know what happens but
                // it isn't right
                // Update: actually, the reason is because those main dist's arn't sellers and therefore not part of the contract.
                // --just send 0 with the queries.

                switch (cboDistType.Text)
                {
                    case "MAIN":
                        // method call looks like it should have 1 as the last parameter, but ignore that
                        dt = GetMainDistributor(sStates, sCats, 0);
                        

                        break;

                    case "SALES":


                        dt = CreateGeneralLabelReport("SALES",0,sStates, sCats);

                        break;

                    case "ALL":
                        dt = GetMainDistributor(sStates, sCats, 0);
                        System.Data.DataTable dt2 = new System.Data.DataTable();
                        dt2 = CreateGeneralLabelReport("SALES", 0, sStates, sCats);

                        bool skip;

                        // add the table to the list
                        foreach (DataRow _drLoop in dt2.Rows)
                        {
                            skip = false;

                            // Check for duplicates
                            foreach (DataRow _drCheck in dt.Rows)
                            {
                                if (_drCheck[0].ToString() == "Nortrax Equipment Company - South")
                                {
                                    if (1 == 1)
                                    {

                                    }
                                }

                                // if name or address 1 is duplicate, throw out
                                if (_drLoop[2].ToString() == _drCheck[2].ToString() && _drLoop[0].ToString() == _drCheck[0].ToString())
                                {
                                    skip = true;
                                    break;
                                }
                            }


                            if (skip == false)
                            {
                                DataRow drTemp2Me;
                                drTemp2Me = dt.NewRow();

                                drTemp2Me[0] = _drLoop[0];
                                drTemp2Me[1] = _drLoop[1];
                                drTemp2Me[2] = _drLoop[2];
                                drTemp2Me[3] = _drLoop[3];
                                drTemp2Me[4] = _drLoop[4];
                                drTemp2Me[5] = _drLoop[5];
                                drTemp2Me[6] = _drLoop[6];

                                //DataRow drtemp3 = new DataRow();
                                dt.Rows.Add(drTemp2Me);
                                //dtTemp.Rows.Add(_drLoop);

                            }
                        }

                        break;

                    default:

                        break;
                }




                GenerateReport("GENERAL", dt);

                // cleanup
                ClearGeneralLabelData();
            }
            else
            {
                MessageBox.Show("Please Enter all Data Before Proceeding");
            }
        }

        private System.Data.DataTable GetMainDistributor(string sStates, string sCats, int p_MainDist)
        {
            DataSet ds;
            System.Data.DataTable dtTemp = new System.Data.DataTable();

            ds = DDA.DataAccess.Distributor_da.GetMainDistributorNames();
            int distID;

            try
            {
                foreach (DataRow _dr in ds.Tables[0].Rows)
                {
                    string emailAddresses = "";


                    // get current distributor id
                    distID = Convert.ToInt32(_dr["pk_DistributorID"].ToString());



                    string sql;

                    // Get all branch id's
                    sql = "SELECT fk_BranchDistID FROM DistributorBranch WHERE fk_MainDistID = " + distID;
                    string branchID = "";

                    DataSet ds2 = DataLogic.DBA.DataLogic.Read(sql);

                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drTemp in ds2.Tables[0].Rows)
                        {
                            if (branchID != "")
                                branchID = branchID + ",";

                            branchID = branchID + "" + drTemp["fk_BranchDistID"] + "";

                            DataSet dsEmails = DDA.DataAccess.Distributor_da.GetEmailAddresses(Convert.ToInt32(drTemp["fk_BranchDistID"].ToString()));

                            foreach (DataRow drEmail in dsEmails.Tables[0].Rows)
                            {
                                if (emailAddresses.Length > 0)
                                    emailAddresses += ", ";

                                emailAddresses += drEmail["Email"].ToString();
                            }
                        }


                        sql = "SELECT DISTINCT Distributor.BillingAddress, Distributor.DistName,Distributor.BillingCityName, Abbreviation, Distributor.fk_BillingZipID, Phone, Fax " +
                                " FROM Distributor, State, ContractDistributor, Contract, ContractCategory, Category, ContractCounty, County " +
                                " WHERE Distributor.fk_StateID = State.StateID " +
                                " AND Contract.ContractID = ContractDistributor.fk_ContractID " +
                                " AND ContractDistributor.fk_ContractID = ContractCategory.fk_ContractID " +
                                " AND Category.CategoryID = ContractCategory.fk_CategoryID " +
                                " AND ContractCounty.fk_ContractID = ContractDistributor.fk_ContractID " +
                                " AND County.CountyID = ContractCounty.fk_CountyID " +
                                " AND Category.CategoryName IN (" + sCats + ") " +
                                " AND (ContractDistributor.fk_DistributorID = Distributor.pk_DistributorID)" +
                                " AND ContractDistributor.fk_DistributorID IN (" + branchID + ")" +
                                " AND County.fk_StateID IN (SELECT State.StateID FROM State WHERE Abbreviation IN (" + sStates + ")) ";

                        if (p_MainDist == 1)
                            sql = sql + " AND MainDistributor = 1 ";

                        sql = sql + " ORDER BY DistName, Abbreviation, BillingCityName";


                        ds2 = new DataSet();
                        ds2 = DataLogic.DBA.DataLogic.Read(sql);

                        if (ds2.Tables[0].Rows.Count > 0)
                        {

                            sql = "SELECT DISTINCT Distributor.BillingAddress, Distributor.DistName,Distributor.BillingCityName, Abbreviation, Distributor.fk_BillingZipID, Phone, Fax" +
                                " FROM Distributor, State" +
                                " WHERE Distributor.fk_StateID = State.StateID" +
                                " AND Distributor.pk_DistributorID = " + distID;
                            ds2 = DataLogic.DBA.DataLogic.Read(sql);

                            if (dtTemp.Columns.Count == 0)
                            {
                                dtTemp.Columns.Add("DISTRIBUTOR NAME");
                                dtTemp.Columns.Add("ATTN");
                                dtTemp.Columns.Add("ADDRESS 1");
                                dtTemp.Columns.Add("ADDRESS 2");
                                dtTemp.Columns.Add("EMAIL");
                                dtTemp.Columns.Add("PHONE");
                                dtTemp.Columns.Add("FAX");
                            }

                            string distName = "";
                            // add the table to the list
                            foreach (DataRow _drLoop in ds2.Tables[0].Rows)
                            {
                                DataRow drTemp2Me;
                                drTemp2Me = dtTemp.NewRow();

                                distName = DDA.DataObjects.AppData.ToTitleCase(_drLoop["DistName"].ToString());

                                // Do some processing for distributor name DBA (doing business as)
                                if (distName.ToUpper().IndexOf(" DBA ") > -1)
                                {
                                    distName = distName.Substring(distName.ToUpper().IndexOf(" DBA ") + 5);
                                }

                                drTemp2Me["DISTRIBUTOR NAME"] = distName;
                                drTemp2Me["ATTN"] = "ATTN: " + txtAttnGeneral.Text;
                                drTemp2Me["ADDRESS 1"] = _drLoop["BillingAddress"].ToString();
                                drTemp2Me["ADDRESS 2"] = _drLoop["BillingCityName"].ToString() + ", " + _drLoop["Abbreviation"].ToString() + " " + _drLoop["fk_BillingZipID"].ToString();
                                drTemp2Me["EMAIL"] = emailAddresses;
                                drTemp2Me["PHONE"] = _drLoop["Phone"].ToString();
                                drTemp2Me["FAX"] = _drLoop["Fax"].ToString();



                                //drTemp2Me[0] = _drLoop[0];
                                //drTemp2Me[1] = _drLoop[1];
                                //drTemp2Me[2] = _drLoop[2];
                                //drTemp2Me[3] = _drLoop[3];
                                //drTemp2Me[4] = _drLoop[4];

                                //DataRow drtemp3 = new DataRow();
                                dtTemp.Rows.Add(drTemp2Me);
                                //dtTemp.Rows.Add(_drLoop);
                            }
                        }


                    }

                }
            }
            catch
            {

            }
            return dtTemp;
        }

        private System.Data.DataTable CreateGeneralLabelReport(string p_distType, int p_distID, string sStates, string sCats)
        {
            string sql;
            DataSet ds;
            // email, phone, fax
            sql = "SELECT DISTINCT Distributor.BillingAddress, Distributor.DistName,Distributor.BillingCityName, Abbreviation, Distributor.fk_BillingZipID, Phone, Fax, pk_DistributorID" +
                    " FROM Distributor, State, ContractDistributor, Contract, ContractCategory, Category, ContractCounty, County " +
                    " WHERE Distributor.fk_StateID = State.StateID" +
                    " AND ContractDistributor.fk_DistributorID = Distributor.pk_DistributorID" +
                    " AND Contract.ContractID = ContractDistributor.fk_ContractID" +
                    " AND ContractDistributor.fk_ContractID = ContractCategory.fk_ContractID" +
                    " AND Category.CategoryID = ContractCategory.fk_CategoryID" +
                    " AND ContractCounty.fk_ContractID = ContractDistributor.fk_ContractID" +
                    " AND County.CountyID = ContractCounty.fk_CountyID";

            
            sql = sql + " AND Category.CategoryName IN (" + sCats + ")";
            
        
            // This should fix everything
            // 12-21-06
            //if (cboDistType.Text != "ALL")
            //{
            //    sql = sql + " AND MainDistributor = ";

            //    if (cboDistType.Text == "MAIN")
            //        sql = sql + "1";
            //    else 
            //        sql = sql + "0";
                
            //}

            


            sql = sql + " AND County.fk_StateID IN (SELECT State.StateID FROM State WHERE Abbreviation IN (" + sStates + "))";
            //sql = sql + " AND State.Abbreviation IN (" + sStates +")";
            sql = sql + " ORDER BY DistName, Abbreviation, BillingCityName";

            ds = DataLogic.DBA.DataLogic.Read(sql);

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("DISTRIBUTOR NAME");
            dt.Columns.Add("ATTN");
            dt.Columns.Add("ADDRESS 1");
            dt.Columns.Add("ADDRESS 2");
            dt.Columns.Add("EMAIL");
            dt.Columns.Add("PHONE");
            dt.Columns.Add("FAX");


            string distName;
            distName = "";

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                DataRow dr = dt.NewRow();

                distName = DDA.DataObjects.AppData.ToTitleCase(ds.Tables[0].Rows[i]["DistName"].ToString());

                // Do some processing for distributor name DBA (doing business as)
                if (distName.ToUpper().IndexOf(" DBA ") > -1)
                {
                    distName = distName.Substring(distName.ToUpper().IndexOf(" DBA ") + 5);
                }

                DataSet dsEmails = DDA.DataAccess.Distributor_da.GetEmailAddresses(Convert.ToInt32(ds.Tables[0].Rows[i]["pk_DistributorID"].ToString()));
                
                string emailAddresses = "";

                foreach (DataRow drEmail in dsEmails.Tables[0].Rows)
                {
                    if (emailAddresses.Length > 0)
                        emailAddresses += ", ";

                    emailAddresses += drEmail["Email"].ToString();
                }


                dr["DISTRIBUTOR NAME"] = distName;
                dr["ATTN"] = "ATTN: " + txtAttnGeneral.Text;
                dr["ADDRESS 1"] = ds.Tables[0].Rows[i]["BillingAddress"].ToString();
                dr["ADDRESS 2"] = ds.Tables[0].Rows[i]["BillingCityName"].ToString() + ", " + ds.Tables[0].Rows[i]["Abbreviation"].ToString() + " " + ds.Tables[0].Rows[i]["fk_BillingZipID"].ToString();
                dr["EMAIL"] = emailAddresses;
                dr["PHONE"] = ds.Tables[0].Rows[i]["Phone"].ToString();
                dr["FAX"] = ds.Tables[0].Rows[i]["Fax"].ToString();

                dt.Rows.Add(dr);
            }

            return dt;
        }

        private void GenerateReport(string p_labelType, System.Data.DataTable table)
        {
            Microsoft.Office.Interop.Excel.ApplicationClass excel = new ApplicationClass();
            excel.Application.Workbooks.Add(true);

            //System.Data.DataTable table = dtExcel.Copy();

            Worksheet wkSheetLast;
            wkSheetLast = new Worksheet();

            //excel.Application.Worksheets.Add(0,,1,null);
            Microsoft.Office.Interop.Excel.Worksheet sheet = null;

            if (sheet != null)
                sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.ActiveWorkbook.Worksheets.Add(Missing.Value, sheet, Missing.Value, Missing.Value);
            else
                sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.ActiveSheet;


            sheet.Name = p_labelType + " LABELS";


            int ColumnIndex = 0;
            foreach (DataColumn col in table.Columns)
            {
                ColumnIndex++;
                sheet.Cells[1, ColumnIndex] = col.ColumnName;
            }
            int rowIndex = 0;
            foreach (DataRow row in table.Rows)
            {
                rowIndex++;
                ColumnIndex = 0;
                foreach (DataColumn col in table.Columns)
                {
                    ColumnIndex++;
                    //.Cells[col.ColumnName].Text
                    sheet.Cells[rowIndex + 1, ColumnIndex] = Convert.ToString(row[col.ColumnName]);

                }
            }


            excel.Visible = true;
        }

        private void btnCreateTerritoryLabel_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This features has not been implemented yet.");

            // cleanup

            if (txtAttnTerritory.Text != "" && cboRepName.Text != "" && cboRepType.Text != "")
            {
                CreateTerritoryLabelReport();
                ClearTerritoryLabelData();
            }
            else
            {
                MessageBox.Show("Please Enter all Data Before Proceeding");
            }
        }

        private void CreateTerritoryLabelReport()
        {

            string sql;
            DataSet ds;

            string repType;

            if (cboRepType.Text == "T-REP")
                repType = "Territory";
            else
                repType = "Service";

            sql = "SELECT DISTINCT Distributor.BillingAddress, Distributor.DistName,Distributor.BillingCityName, Abbreviation, Distributor.fk_BillingZipID" + 
                    " FROM Distributor, State, DistributorRepresentative, Representative" + 
                    " WHERE Distributor.fk_StateID = State.StateID" + 
                    " AND Distributor.pk_DistributorID = DistributorRepresentative.fk_DistributorID" + 
                    " AND DistributorRepresentative.fk_" + repType + "RepID = Representative.RepID" + 
                    " AND Representative.RepName = '" + cboRepName.Text + "'";

            ds = DataLogic.DBA.DataLogic.Read(sql);



            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("DISTRIBUTOR NAME");
            dt.Columns.Add("ATTN");
            dt.Columns.Add("ADDRESS 1");
            dt.Columns.Add("ADDRESS 2");


            string distName = "";

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                DataRow dr = dt.NewRow();

                distName = DDA.DataObjects.AppData.ToTitleCase(ds.Tables[0].Rows[i]["DistName"].ToString());

                // Do some processing for distributor name DBA (doing business as)
                if (distName.ToUpper().IndexOf(" DBA ") > -1)
                {
                    distName = distName.Substring(distName.ToUpper().IndexOf(" DBA ") + 5);
                }

                dr["DISTRIBUTOR NAME"] = distName;
                dr["ATTN"] = "ATTN: " + txtAttnTerritory.Text;
                dr["ADDRESS 1"] = ds.Tables[0].Rows[i]["BillingAddress"].ToString();
                dr["ADDRESS 2"] = ds.Tables[0].Rows[i]["BillingCityName"].ToString() + ", " + ds.Tables[0].Rows[i]["Abbreviation"].ToString() + " " + ds.Tables[0].Rows[i]["fk_BillingZipID"].ToString();

                dt.Rows.Add(dr);
            }


            GenerateReport("TERRITORY", dt);
        }

        private void ClearGeneralLabelData()
        {
            txtAttnGeneral.Text = "";
            UnCheckStates();

            for (int i = 0; i < chkProductType.Items.Count; i++)
                chkProductType.SetItemCheckState(i, CheckState.Unchecked);

            cboDistType.Text = "";
        }

        private void ClearTerritoryLabelData()
        {
            txtAttnTerritory.Text = "";
            cboRepName.Text = "";
            cboRepName.Items.Clear();
            cboRepType.Text = "";
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkState.Items.Count; i++)
            {
                chkState.SetItemCheckState(i, CheckState.Checked);
            }
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            UnCheckStates();
        }

        private void UnCheckStates()
        {
            for (int i = 0; i < chkState.Items.Count; i++)
            {
                chkState.SetItemCheckState(i, CheckState.Unchecked);
            }
        }

        private void cboRepType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string repType;

            cboRepName.Text = "";

            repType = cboRepType.SelectedItem.ToString();
            if (repType == "T-REP")
            {
                repType = "Territory";
            }
            else
            {
                repType = "Service";
            }

            DataSet ds;
            ds = DDA.DataAccess.Representative_da.GetRepresentativeList(repType);

            cboRepName.Items.Clear();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                cboRepName.Items.Add(Convert.ToString(ds.Tables[0].Rows[i]["Rep Name"]));
            }
        }

        private void btnSelectAllProductType_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkProductType.Items.Count; i++)
            {
                chkProductType.SetItemCheckState(i, CheckState.Checked);
            }
        }

        private void btnDeselectAllProductType_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkProductType.Items.Count; i++)
            {
                chkProductType.SetItemCheckState(i, CheckState.Unchecked);
            }
        }


    }
}