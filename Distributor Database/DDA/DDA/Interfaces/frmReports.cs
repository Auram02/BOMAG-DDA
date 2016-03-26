using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices; // For COMException
using Microsoft.Office.Interop.Excel;
using System.Collections;
using System.Reflection; // For Missing.Value and BindingFlags

namespace DDA.Interfaces
{
    public partial class frmReports : Form
    {
        public frmReports()
        {
            InitializeComponent();
            DDA.DataObjects.AppData.DisableCloseButton(Handle);
            lblWait.Text = "";
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            this.Close();

            DDA.DataObjects.AppData.activeMain.Show();
        }

        private void btnLabelReports_Click(object sender, EventArgs e)
        {
            frmLabelReports flr = new frmLabelReports();
            this.Hide();

            flr.ShowDialog();

            this.Show();
        }

        private void btnDistByState_Click(object sender, EventArgs e)
        {
            StateListingReport(1);
        }

        private void btnSalesLocationsByState_Click(object sender, EventArgs e)
        {
            StateListingReport(0);
        }

        private void StateListingReport(int p_mainDist)
        {
            //ds.WriteXml("C:\\temp\\MainDistributorList.xls");

            DataSet ds;
            DataSet dsDist;

            ds = DDA.DataAccess.Category_da.GetCategoryList();

            DataSet dsResult;
            dsResult = new DataSet();

            dsDist = DDA.DataAccess.Distributor_da.GetDistributorList("Main", "");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("STATE");
                dt.Columns.Add("DISTRIBUTOR NAME");
                dt.Columns.Add("CITY");
                dt.Columns.Add("ADDRESS");
                dt.Columns.Add("ZIP");
                dt.Columns.Add("PRODUCT");
                dt.Columns.Add("PHONE");
                dt.Columns.Add("FAX");

                if (p_mainDist == 0)
                {

                    dt.Columns.Add("TERRITORY MANAGER");
                    dt.Columns.Add("fk_ServiceRepID");
                    dt.Columns.Add("PRODUCT SUPPORT");
                }

                System.Data.DataTable dtTemp = new System.Data.DataTable();

                bool bOutput;

                // Need a loop here
                for (int j = 0; j < dsDist.Tables[0].Rows.Count; j++)
                {
                    dtTemp = DDA.DataObjects.Reports.DistributorsByState(p_mainDist,
                                        Convert.ToInt32(ds.Tables[0].Rows[i]["CategoryID"]),
                                        Convert.ToInt32(dsDist.Tables[0].Rows[j]["pk_DistributorID"]));
                    if (p_mainDist == 0)
                        dtTemp.Columns.Add("PRODUCT SUPPORT");  // add an extra row

                    DataRow dr = dt.NewRow();
                    dr[0] = DDA.DataObjects.AppData.ToTitleCase(Convert.ToString(dsDist.Tables[0].Rows[j]["DISTRIBUTOR NAME"]));

                    bOutput = false;

                    string debugstr;

                    for (int k = 0; k < dtTemp.Rows.Count; k++)
                    {
                        // If true, then do a lookup for the "Product Support"
                        if (p_mainDist == 0)
                        {
                            dtTemp.Rows[k]["PRODUCT SUPPORT"] = DDA.DataAccess.Representative_da.GetRepresentativeName(Convert.ToInt32(dtTemp.Rows[k]["fk_ServiceRepID"]));

                        }

                        //if (k == 0)
                        dtTemp.Rows[k][0] = DDA.DataObjects.AppData.ToTitleCase(Convert.ToString(dsDist.Tables[0].Rows[j]["DISTRIBUTOR NAME"]));

                        dt.Rows.Add(dtTemp.Rows[k].ItemArray);

                        bOutput = true;
                    }

                    //if (bOutput == false)
                    //    dt.Rows.Add(dr);  // add the previous new row ONLY if no data was outputted for this distributor

                    if (bOutput == true)
                    {
                        dr = dt.NewRow();
                        //dt.Rows.Add(dr); // spacer row
                    }
                }

                if (p_mainDist == 0)
                    dt.Columns.Remove("fk_ServiceRepID");

                dt.TableName = Convert.ToString(ds.Tables[0].Rows[i][0]);

                System.Data.DataTable dt2 = dt.Copy();

                dsResult.Tables.Add(dt2);

                string replaceBadCharacters;
                replaceBadCharacters = Convert.ToString(ds.Tables[0].Rows[i][0]);
                replaceBadCharacters = replaceBadCharacters.Replace("/", " & ");
                ds.Tables[0].Rows[i][0] = replaceBadCharacters;

            }

            // post processing - sort routines

            Microsoft.Office.Interop.Excel.ApplicationClass excel = new ApplicationClass();
            excel.Application.Workbooks.Add(true);

            Cursor.Current = Cursors.WaitCursor;
            lblWait.Text = "Please wait while your report is generated...";


            string holder;
            holder = "";



            for (int i = 0; i < dsResult.Tables.Count; i++)
            {


                // need to swap data
                for (int j = 0; j < dsResult.Tables[i].Rows.Count; j++)
                {
                    holder = dsResult.Tables[i].Rows[j][0].ToString();
                    dsResult.Tables[i].Rows[j][0] = dsResult.Tables[i].Rows[j][1].ToString();

                    dsResult.Tables[i].Rows[j][1] = holder;

                }

                System.Data.DataTable table = dsResult.Tables[i];

                DDA.DataAccess.DataViewEx demoFilter = new DDA.DataAccess.DataViewEx(dsResult.Tables[i]);
                demoFilter.Sort = "[State] desc, [DISTRIBUTOR NAME] desc";
                //new string[] { "ContactTitle", "CompanyName", "ContactName" }

                string[] columnNames = new string[9];

                columnNames[0] = "DISTRIBUTOR NAME";
                columnNames[1] = "CITY";
                columnNames[2] = "ADDRESS";
                columnNames[3] = "ZIP";
                columnNames[4] = "PRODUCT";
                columnNames[5] = "PHONE";
                columnNames[6] = "FAX";
                columnNames[7] = "TERRITORY MANAGER";
                columnNames[8] = "PRODUCT SUPPORT";

                System.Data.DataTable tabletemp = new System.Data.DataTable();
                //tabletemp = demoFilter.ToTable(true, columnNames);

                DataView testView = new DataView(table);
                testView.Sort = "[State] Asc, [DISTRIBUTOR NAME] Asc";
                table = testView.ToTable();

                string distName, distName2;
                distName = "";
                distName2 = "";  // for removing doing business as
                for (int n = 0; n < table.Rows.Count; n++)
                {
                    if (table.Rows[n]["DISTRIBUTOR NAME"].ToString() == distName)
                    {
                        table.Rows[n]["DISTRIBUTOR NAME"] = "";
                    }
                    else
                    {
                        distName = table.Rows[n]["DISTRIBUTOR NAME"].ToString();
                    }


                    // For DBA (DOING BUSINESS AS) logic
                    distName2 = table.Rows[n]["DISTRIBUTOR NAME"].ToString();

                    // Do some processing for distributor name DBA (doing business as)
                    if (distName2.ToUpper().IndexOf(" DBA ") > -1)
                    {
                        table.Rows[n]["DISTRIBUTOR NAME"] = distName2.Substring(distName2.ToUpper().IndexOf(" DBA ") + 5);
                    }

                }


                //System.Data.DataTable dt_table = dsResult.Tables[i];

                //DataView table;
                //table = new DataView(dt_table);
                //table.Sort = "[State] Desc, [DISTRIBUTOR NAME] Desc";


                Worksheet wkSheetLast;
                wkSheetLast = new Worksheet();

                //excel.Application.Worksheets.Add(0,,1,null);
                Microsoft.Office.Interop.Excel.Worksheet sheet = null;

                if (sheet != null)
                    sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.ActiveWorkbook.Worksheets.Add(Missing.Value, sheet, Missing.Value, Missing.Value);
                else
                    sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.ActiveSheet;

                string tempSheetName = Convert.ToString(ds.Tables[0].Rows[i][0]);

                if (tempSheetName.Contains("TANDEM & COMBINATION ROLLERS"))
                    tempSheetName = tempSheetName.Replace("TANDEM & COMBINATION ROLLERS", "TANDEM & COMBO ROLLERS");

                if (tempSheetName.Length > 31)
                    tempSheetName = tempSheetName.Substring(0, 31);

                sheet.Name = tempSheetName;


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

                if (i < dsResult.Tables.Count - 1)
                    excel.Application.ActiveWorkbook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            }

            Cursor.Current = Cursors.Default;
            lblWait.Text = "";
            excel.Visible = true;

        }

        private void btnMainEmailList_Click(object sender, EventArgs e)
        {
            EmailListReport(1);
        }

        private void btnSalesEmailList_Click(object sender, EventArgs e)
        {
            EmailListReport(0);
        }

        private void EmailListReport(int p_mainDist)
        {

            DataSet dsResult;
            dsResult = new DataSet();

            dsResult = DDA.DataAccess.Distributor_da.GetDistributorEmailList(p_mainDist);

            string distName = "";

            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
            {
                dsResult.Tables[0].Rows[i]["DISTRIBUTOR"] = DDA.DataObjects.AppData.ToTitleCase(dsResult.Tables[0].Rows[i]["DISTRIBUTOR"].ToString());

                // For DBA (DOING BUSINESS AS) logic
                distName = dsResult.Tables[0].Rows[i]["DISTRIBUTOR"].ToString();

                // Do some processing for distributor name DBA (doing business as)
                if (distName.ToUpper().IndexOf(" DBA ") > -1)
                {
                    dsResult.Tables[0].Rows[i]["DISTRIBUTOR"] = distName.Substring(distName.ToUpper().IndexOf(" DBA ") + 5);
                }

            }

            Microsoft.Office.Interop.Excel.ApplicationClass excel = new ApplicationClass();
            excel.Application.Workbooks.Add(true);

            Cursor.Current = Cursors.WaitCursor;
            lblWait.Text = "Please wait while your report is generated...";


            for (int i = 0; i < dsResult.Tables.Count; i++)
            {

                System.Data.DataTable table = dsResult.Tables[i];

                Worksheet wkSheetLast;
                wkSheetLast = new Worksheet();

                //excel.Application.Worksheets.Add(0,,1,null);
                Microsoft.Office.Interop.Excel.Worksheet sheet = null;

                if (sheet != null)
                    sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.ActiveWorkbook.Worksheets.Add(Missing.Value, sheet, Missing.Value, Missing.Value);
                else
                    sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.ActiveSheet;


                sheet.Name = "EMAIL LIST REPORT";


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

                if (i < dsResult.Tables.Count - 1)
                    excel.Application.ActiveWorkbook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            }

            Cursor.Current = Cursors.Default;
            lblWait.Text = "";
            excel.Visible = true;

        }

        private void btnBefore03_Click(object sender, EventArgs e)
        {
            RenewalLetterReport(false);
        }

        private void btnAfter03_Click(object sender, EventArgs e)
        {
            RenewalLetterReport(true);
        }

        private void RenewalLetterReport(bool after03)
        {
            string sql;
            DataSet ds;
            DataSet dsDist;

            dsDist = DDA.DataAccess.Distributor_da.GetMainDistributorNames();
            int distributorID;
            int day, month, year;
            DateTime dtCreation;

            bool useContract;

            System.Data.DataTable dtExcel = new System.Data.DataTable();
            dtExcel.Columns.Add("Contract Number");
            dtExcel.Columns.Add("Prefix");
            dtExcel.Columns.Add("First Name");
            dtExcel.Columns.Add("Last Name");
            dtExcel.Columns.Add("Title");
            dtExcel.Columns.Add("Main Distributor Name");
            dtExcel.Columns.Add("Billing Address");
            dtExcel.Columns.Add("City");
            dtExcel.Columns.Add("State");
            dtExcel.Columns.Add("Zip");
            dtExcel.Columns.Add("Creation Date");
            dtExcel.Columns.Add("Categories");


            // Variables for the report
            string r_ContractNumber;
            string r_Prefix;
            string r_FirstName;
            string r_LastName;
            string r_Title;

            string r_Main_Distributor_Name;
            string r_Billing_Address;
            string r_City;
            string r_State;
            string r_Zip;
            string r_CreationDate;
            string r_Categories;

            // report name
            string renewalReportDate = "";

            for (int i = 0; i < dsDist.Tables[0].Rows.Count; i++)
            {
                distributorID = Convert.ToInt32(dsDist.Tables[0].Rows[i]["pk_DistributorID"]);

                //sql = "SELECT Distributor.pk_DistributorID, ContractNumber, ContractDate, Distributor.DistName, Distributor.BillingAddress, Distributor.CityName, State.Abbreviation, Distributor.fk_ZipID" +
                //         " FROM Contract, Distributor, State" +
                //         " WHERE Contract.MainDistributorID LIKE Distributor.pk_DistributorID" +
                //         " AND State.StateID = Distributor.fk_StateID" +
                //         " AND Distributor.pk_DistributorID = " + distributorID +
                //         " ORDER BY DistName, ContractNumber";

                sql = "SELECT DISTINCT Distributor.pk_DistributorID, ContractNumber, ContractDate, Distributor.DistName, Distributor.BillingAddress, Distributor.CityName, State.Abbreviation, Distributor.fk_ZipID " +
                        " FROM Contract, Distributor, State, ContractCategory, Category" +
                        " WHERE Contract.MainDistributorID LIKE Distributor.pk_DistributorID " +
                        " AND State.StateID = Distributor.fk_StateID" +
                        " AND Distributor.pk_DistributorID = " + distributorID +
                        " AND Contract.ContractID = ContractCategory.fk_ContractID" +
                        " AND Category.CategoryID = ContractCategory.fk_CategoryID" +
                        " AND Category.AllowTerritoryOverlap = false" +
                        " AND Category.AllowCountyOverlap = false" +
                        " GROUP BY Distributor.pk_DistributorID, ContractNumber, ContractDate, Distributor.DistName, Distributor.BillingAddress, Distributor.CityName, State.Abbreviation, Distributor.fk_ZipID " +
                        " ORDER BY DistName, ContractNumber";

                ds = DataLogic.DBA.DataLogic.Read(sql);

                string primContactName, primContactTitle;

                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    useContract = false;
                    // create excel doc here
                    // Do date check here
                    dtCreation = DateTime.Parse(ds.Tables[0].Rows[j]["ContractDate"].ToString());

                    DataSet dsPrimary;
                    dsPrimary = DDA.DataAccess.Distributor_da.GetPrimaryContact(Convert.ToInt32(ds.Tables[0].Rows[j]["pk_DistributorID"].ToString()));

                    try
                    {
                        primContactName = dsPrimary.Tables[0].Rows[0]["ContactName"].ToString();
                        primContactTitle = dsPrimary.Tables[0].Rows[0]["ContactTitle"].ToString();
                    }
                    catch
                    {
                        primContactName = "";
                        primContactTitle = "";
                    }

                    if (after03 == true)
                    {
                        if (dtCreation.Date > DateTime.Parse("12/30/03"))
                        {
                            useContract = true;
                            renewalReportDate = "Contracts On+After Dec 31, 03";
                        }
                    }
                    else
                    {
                        if (dtCreation.Date < DateTime.Parse("12/31/03"))
                        {
                            useContract = true;
                            renewalReportDate = "Contracts On+Before Dec 30, 03";
                        }
                    }

                    // Can we use the contract?
                    if (useContract == true)
                    {
                        r_Billing_Address = ds.Tables[0].Rows[j]["BillingAddress"].ToString();
                        //r_Categories = ds.Tables[0].Rows[j][""].ToString();
                        r_City = ds.Tables[0].Rows[j]["CityName"].ToString();
                        r_ContractNumber = ds.Tables[0].Rows[j]["ContractNumber"].ToString();
                        r_CreationDate = ds.Tables[0].Rows[j]["ContractDate"].ToString();
                        //r_FirstName = ds.Tables[0].Rows[j]["ContactName"].ToString();
                        r_FirstName = primContactName;

                        if (r_FirstName != "")
                        {
                            r_LastName = r_FirstName.Substring(r_FirstName.IndexOf(" ") + 1);
                            r_FirstName = r_FirstName.Substring(0, r_FirstName.IndexOf(" "));
                        }
                        else
                        {
                            r_LastName = "";
                            r_FirstName = "";
                        }

                        r_Main_Distributor_Name = DDA.DataObjects.AppData.ToTitleCase(ds.Tables[0].Rows[j]["DistName"].ToString());
                        r_Prefix = "Mr.";
                        r_State = ds.Tables[0].Rows[j]["Abbreviation"].ToString();
                        //r_Title = ds.Tables[0].Rows[j]["ContactTitle"].ToString();
                        r_Title = primContactTitle;
                        r_Zip = ds.Tables[0].Rows[j]["fk_ZipID"].ToString();

                        // now add the data to excel if true
                        DataSet dsCategories;
                        dsCategories = DDA.DataAccess.Contract_da.GetContractCategories(r_ContractNumber, true);
                        r_Categories = "";
                        for (int k = 0; k < dsCategories.Tables[0].Rows.Count; k++)
                        {
                            if (r_Categories != "")
                                r_Categories = r_Categories + ", ";

                            r_Categories = r_Categories + dsCategories.Tables[0].Rows[k]["CategoryName"].ToString();
                        }

                        // should have all the data now
                        // start kicking it to the dataset we'll be sending to excel

                        DataRow dr = dtExcel.NewRow();
                        dr["Contract Number"] = r_ContractNumber;
                        dr["Prefix"] = r_Prefix;
                        dr["First Name"] = r_FirstName;
                        dr["Last Name"] = r_LastName;
                        dr["Title"] = r_Title;
                        dr["Main Distributor Name"] = r_Main_Distributor_Name;
                        dr["Billing Address"] = r_Billing_Address;
                        dr["City"] = r_City;
                        dr["State"] = r_State;
                        dr["Zip"] = r_Zip;
                        dr["Creation Date"] = r_CreationDate;
                        dr["Categories"] = r_Categories;

                        dtExcel.Rows.Add(dr);

                    }
                }

            }

            Microsoft.Office.Interop.Excel.ApplicationClass excel = new ApplicationClass();
            excel.Application.Workbooks.Add(true);

            System.Data.DataTable table = dtExcel.Copy();

            Worksheet wkSheetLast;
            wkSheetLast = new Worksheet();

            //excel.Application.Worksheets.Add(0,,1,null);
            Microsoft.Office.Interop.Excel.Worksheet sheet = null;

            if (sheet != null)
                sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.ActiveWorkbook.Worksheets.Add(Missing.Value, sheet, Missing.Value, Missing.Value);
            else
                sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.ActiveSheet;



            //sheet.Name = "EMAIL LIST REPORT";
            sheet.Name = renewalReportDate; ;

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

        /// <summary>
        /// Prints a quick product view report which includes each sales location for each main distributor
        /// and a mark to designate which product types they can sell (based on associations in every contract)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuickProductView_Click(object sender, EventArgs e)
        {
            DataSet dsMainDist;
            dsMainDist = DDA.DataAccess.Distributor_da.GetDistributorList("Main", "");

            DataSet dsCategory;
            dsCategory = DDA.DataAccess.Category_da.GetCategoryList();


            // Create and preprare output table
            DataSet dsResult = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();

            string blankColumn = " ";

            // distributor name/location column
            dt.Columns.Add(blankColumn);

            for (int i = 0; i < dsCategory.Tables[0].Rows.Count; i++)
            {
                blankColumn = blankColumn + " "; // add blank columns to seperate
                dt.Columns.Add(blankColumn);
                dt.Columns.Add(Convert.ToString(dsCategory.Tables[0].Rows[i]["CategoryName"]));
            }

            dsResult.Tables.Add(dt);

            // End of creation/prepping of output table


            int mainDistID;
            string mainDistName;

            DataSet dsBranchList = new DataSet();
            DataSet dsCategoryList = new DataSet();
            string catName;
            catName = "";

            string listCatName;
            listCatName = "";


            DataRow dr;

            string cityName, stateName;
            cityName = "";
            stateName = "";



            // processing for data
            for (int i = 0; i < dsMainDist.Tables[0].Rows.Count; i++)
            {
                mainDistID = Convert.ToInt32(dsMainDist.Tables[0].Rows[i]["pk_DistributorID"]);
                mainDistName = DDA.DataObjects.AppData.ToTitleCase(Convert.ToString(dsMainDist.Tables[0].Rows[i]["DISTRIBUTOR NAME"]));


                // Do some processing for distributor name DBA (doing business as)
                if (mainDistName.ToUpper().IndexOf(" DBA ") > -1)
                {
                    mainDistName = mainDistName.Substring(mainDistName.ToUpper().IndexOf(" DBA ") + 5);
                }


                // Get all branches
                // Branch Name, Address, City, State, pk_DistributorID
                dsBranchList = DDA.DataAccess.Distributor_da.GetDistributorBranchList(mainDistID, "");

                dsCategoryList = new DataSet();


                // Add MainDistributor Row to dataset
                // Add the new row to the result table
                dr = dsResult.Tables[0].NewRow();
                dr[0] = mainDistName;
                dsResult.Tables[0].Rows.Add(dr);



                for (int j = 0; j < dsBranchList.Tables[0].Rows.Count; j++)
                {
                    // make a new row for each sales location
                    dr = dsResult.Tables[0].NewRow();

                    cityName = Convert.ToString(dsBranchList.Tables[0].Rows[j]["City"]);
                    stateName = Convert.ToString(dsBranchList.Tables[0].Rows[j]["State"]);
                    dr[0] = cityName + ", " + stateName;

                    dsCategoryList = DDA.DataAccess.Distributor_da.GetContractCategoryList(Convert.ToInt32(dsBranchList.Tables[0].Rows[j]["pk_DistributorID"]));

                    for (int k = 0; k < dsCategory.Tables[0].Rows.Count; k++)
                    {
                        catName = Convert.ToString(dsCategory.Tables[0].Rows[k]["CategoryName"]);

                        for (int l = 0; l < dsCategoryList.Tables[0].Rows.Count; l++)
                        {
                            listCatName = Convert.ToString(dsCategoryList.Tables[0].Rows[l]["CategoryName"]);

                            // there is a match
                            if (catName == listCatName)
                            {

                                dr[(k + 1) * 2] = "X";

                            }
                        }
                    }


                    // Add the new row to the result table
                    dsResult.Tables[0].Rows.Add(dr);
                }

                // Add a spacer row after each main distributor's sales location list
                dr = dsResult.Tables[0].NewRow();
                dsResult.Tables[0].Rows.Add(dr);
            }


            // Excel processing
            Microsoft.Office.Interop.Excel.ApplicationClass excel = new ApplicationClass();
            excel.Application.Workbooks.Add(true);

            Cursor.Current = Cursors.WaitCursor;
            lblWait.Text = "Please wait while your report is generated...";

            for (int i = 0; i < dsResult.Tables.Count; i++)
            {

                System.Data.DataTable table = dsResult.Tables[i];

                Worksheet wkSheetLast;
                wkSheetLast = new Worksheet();

                //excel.Application.Worksheets.Add(0,,1,null);
                Microsoft.Office.Interop.Excel.Worksheet sheet = null;

                if (sheet != null)
                    sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.ActiveWorkbook.Worksheets.Add(Missing.Value, sheet, Missing.Value, Missing.Value);
                else
                    sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.ActiveSheet;


                sheet.Name = "BOMAG AMERICAS DISTRIBUTORS";


                // special stuff.

                sheet.Cells[1, 1] = "BOMAG AMERICAS DISTRIBUTORS";

                int ColumnIndex = 0;
                foreach (DataColumn col in table.Columns)
                {
                    ColumnIndex++;
                    sheet.Cells[3, ColumnIndex] = col.ColumnName;
                }

                int rowIndex = 3;
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

                if (i < dsResult.Tables.Count - 1)
                    excel.Application.ActiveWorkbook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            }



            Cursor.Current = Cursors.Default;
            lblWait.Text = "";
            excel.Visible = true;
        }

        private void btnMainDistributorSpreadsheet_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature has not been implemented yet");
        }

        private void frmReports_Load(object sender, EventArgs e)
        {

        }

        private void btnContractNumberLabel_Click(object sender, EventArgs e)
        {
            frmContractNumberLabel fcnl = new frmContractNumberLabel();
            this.Hide();
            fcnl.ShowDialog();
            this.Show();  // Restore the window
        }

        private void btnFullContractReport_All_Click(object sender, EventArgs e)
        {
            DDA.DataObjects.AppData.ReportName = "Full Contract Report";
            //frmFullContractReportViewer fvr = new frmFullContractReportViewer();
            frmFullContractReport fvr = new frmFullContractReport();

            this.Hide();
            fvr.ShowDialog();
            this.Show();
        }

        private void btnSalesLocationRepReport_Click(object sender, EventArgs e)
        {
            DataSet dsResult = new DataSet();

            string sql = "";
            DataSet dsDistList = new DataSet();
            dsDistList = DDA.DataAccess.Distributor_da.GetDistributorList("Sales", "");

            System.Data.DataTable dt = new System.Data.DataTable();

            dt.Columns.Add("Distributor Name");
            dt.Columns.Add("City");
            dt.Columns.Add("State");
            dt.Columns.Add("Has Service Rep");
            dt.Columns.Add("Has Territory Rep");
            dsResult.Tables.Add(dt);

            foreach (DataRow dr in dsDistList.Tables[0].Rows)
            {
                DataSet dsTemp = new DataSet();

                DataSet dsTemp2 = new DataSet();

                sql = "select COUNT(fk_servicerepid) AS [RepCount] " +
                        "from DistributorRepresentative WHERE fk_DistributorID = " + dr["pk_DistributorID"] + " AND fk_servicerepid > 0 ";

                dsTemp = DataLogic.DBA.DataLogic.Read(sql);

                sql = "SELECT COUNT(fk_TerritoryRepID) AS [RepCount] FROM DistributorRepresentative WHERE fk_DistributorID = " + dr["pk_DistributorID"] +
                    " AND fk_TerritoryRepID > 0";

                dsTemp2 = DataLogic.DBA.DataLogic.Read(sql);


                bool hasServiceRep = true;
                bool hasTerritoryRep = true;

                if (Convert.ToInt32(dsTemp.Tables[0].Rows[0]["RepCount"].ToString()) == 0)
                    hasServiceRep = false;

                if (Convert.ToInt32(dsTemp2.Tables[0].Rows[0]["RepCount"].ToString()) == 0)
                    hasTerritoryRep = false;

                if (hasServiceRep == false || hasTerritoryRep == false)
                {
                    // add to result table
                    DataRow drAdd = dsResult.Tables[0].NewRow();

                    drAdd["Distributor Name"] = dr["DISTRIBUTOR NAME"];
                    drAdd["City"] = dr["CITY"];
                    drAdd["State"] = dr["STATE"];
                    drAdd["Has Service Rep"] = hasServiceRep.ToString();
                    drAdd["Has Territory Rep"] = hasTerritoryRep.ToString();

                    dsResult.Tables[0].Rows.Add(drAdd);

                }

            }


            Microsoft.Office.Interop.Excel.ApplicationClass excel = new ApplicationClass();
            excel.Application.Workbooks.Add(true);

            Cursor.Current = Cursors.WaitCursor;
            lblWait.Text = "Please wait while your report is generated...";


            for (int i = 0; i < dsResult.Tables.Count; i++)
            {

                System.Data.DataTable table = dsResult.Tables[i];

                Worksheet wkSheetLast;
                wkSheetLast = new Worksheet();

                //excel.Application.Worksheets.Add(0,,1,null);
                Microsoft.Office.Interop.Excel.Worksheet sheet = null;

                if (sheet != null)
                    sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.ActiveWorkbook.Worksheets.Add(Missing.Value, sheet, Missing.Value, Missing.Value);
                else
                    sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.ActiveSheet;


                sheet.Name = "SALES LOCATION REP REPORT";


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

                if (i < dsResult.Tables.Count - 1)
                    excel.Application.ActiveWorkbook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            }

            Cursor.Current = Cursors.Default;
            lblWait.Text = "";
            excel.Visible = true;

        }

        private void btnAllRepresentatives_Click(object sender, EventArgs e)
        {


            DataSet dsResult;
            dsResult = new DataSet();

            dsResult = DDA.DataAccess.Distributor_da.GetDistributorRepReport();


            Microsoft.Office.Interop.Excel.ApplicationClass excel = new ApplicationClass();
            excel.Application.Workbooks.Add(true);

            Cursor.Current = Cursors.WaitCursor;
            lblWait.Text = "Please wait while your report is generated...";


            for (int i = 0; i < dsResult.Tables.Count; i++)
            {

                System.Data.DataTable table = dsResult.Tables[i];

                Worksheet wkSheetLast;
                wkSheetLast = new Worksheet();

                //excel.Application.Worksheets.Add(0,,1,null);
                Microsoft.Office.Interop.Excel.Worksheet sheet = null;

                if (sheet != null)
                    sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.ActiveWorkbook.Worksheets.Add(Missing.Value, sheet, Missing.Value, Missing.Value);
                else
                    sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.ActiveSheet;


                sheet.Name = "REPRESENTATIVE LIST REPORT";


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

                if (i < dsResult.Tables.Count - 1)
                    excel.Application.ActiveWorkbook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            }

            Cursor.Current = Cursors.Default;
            lblWait.Text = "";
            excel.Visible = true;

        }

        private void btnProductViewByState_Click(object sender, EventArgs e)
        {
            //DataSet dsMainDist;
            //dsMainDist = DDA.DataAccess.Distributor_da.GetDistributorList("Main", "");

            DataSet dsCategory;
            dsCategory = DDA.DataAccess.Category_da.GetCategoryList();


            // Create and preprare output table
            DataSet dsResult = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();

            string blankColumn = " ";

            // distributor name/location column
            dt.Columns.Add(blankColumn);

            for (int i = 0; i < dsCategory.Tables[0].Rows.Count; i++)
            {
                blankColumn = blankColumn + " "; // add blank columns to seperate
                dt.Columns.Add(blankColumn);
                dt.Columns.Add(Convert.ToString(dsCategory.Tables[0].Rows[i]["CategoryName"]));
            }

            blankColumn = blankColumn + " ";
            dt.Columns.Add(blankColumn);
            dt.Columns.Add("T-Rep");

            blankColumn = blankColumn + " ";

            dt.Columns.Add(blankColumn);
            dt.Columns.Add("S-Rep");


            dsResult.Tables.Add(dt);

            // End of creation/prepping of output table


            int mainDistID;
            string mainDistName;

            DataSet dsBranchList = new DataSet();
            DataSet dsCategoryList = new DataSet();
            string catName;
            catName = "";

            string listCatName;
            listCatName = "";


            DataRow dr;

            string cityName, stateName;
            cityName = "";
            stateName = "";

            System.Data.DataTable oldTable = dsResult.Tables[0].Clone();

            DataSet stateDS = DataAccess.Location_da.GetStateAbbrList();

            for (int stateLooper = 0; stateLooper < stateDS.Tables[0].Rows.Count; stateLooper++)
            {
                DataRow drCurrentState = stateDS.Tables[0].Rows[stateLooper];

                dr = dsResult.Tables[0].NewRow();
                dr[0] = drCurrentState["FullName"] + " - " + drCurrentState["Abbreviation"];
                dsResult.Tables[0].Rows.Add(dr);


                //// processing for data
                //for (int i = 0; i < dsMainDist.Tables[0].Rows.Count; i++)
                //{

                DataSet dsMainDist_Filtered = DDA.DataAccess.Distributor_da.GetDistributorList_SellingLocationsInState(drCurrentState["Abbreviation"].ToString());
                // processing for data
                for (int i = 0; i < dsMainDist_Filtered.Tables[0].Rows.Count; i++)
                {

                    if (dsMainDist_Filtered.Tables[0].Rows.Count > 0)
                    {
                        mainDistID = Convert.ToInt32(dsMainDist_Filtered.Tables[0].Rows[i]["DistID"]);
                        mainDistName = DDA.DataObjects.AppData.ToTitleCase(Convert.ToString(dsMainDist_Filtered.Tables[0].Rows[i]["DISTRIBUTOR NAME"]));


                        // Do some processing for distributor name DBA (doing business as)
                        if (mainDistName.ToUpper().IndexOf(" DBA ") > -1)
                        {
                            mainDistName = mainDistName.Substring(mainDistName.ToUpper().IndexOf(" DBA ") + 5);
                        }


                        // Get all branches
                        // Branch Name, Address, City, State, pk_DistributorID
                        dsBranchList = DDA.DataAccess.Distributor_da.GetDistributorBranchListByState(mainDistID, "", drCurrentState["Abbreviation"].ToString());

                        dsCategoryList = new DataSet();


                        // Add MainDistributor Row to dataset
                        // Add the new row to the result table
                        dr = dsResult.Tables[0].NewRow();
                        dr[0] = mainDistName;
                        dsResult.Tables[0].Rows.Add(dr);



                        for (int j = 0; j < dsBranchList.Tables[0].Rows.Count; j++)
                        {

                            cityName = Convert.ToString(dsBranchList.Tables[0].Rows[j]["City"]);
                            stateName = Convert.ToString(dsBranchList.Tables[0].Rows[j]["State"]);

                            //if (stateName == drCurrentState["Abbreviation"].ToString())
                            //{
                            // make a new row for each sales location
                            dr = oldTable.NewRow();

                            dr[0] = cityName + ", " + stateName;

                            dsCategoryList = DDA.DataAccess.Distributor_da.GetContractCategoryList(Convert.ToInt32(dsBranchList.Tables[0].Rows[j]["pk_DistributorID"]));

                            for (int k = 0; k < dsCategory.Tables[0].Rows.Count; k++)
                            {
                                catName = Convert.ToString(dsCategory.Tables[0].Rows[k]["CategoryName"]);

                                for (int l = 0; l < dsCategoryList.Tables[0].Rows.Count; l++)
                                {
                                    listCatName = Convert.ToString(dsCategoryList.Tables[0].Rows[l]["CategoryName"]);

                                    // there is a match
                                    if (catName == listCatName)
                                    {

                                        dr[(k + 1) * 2] = "X";

                                    }
                                    // }
                                }
                            }

                            dr[oldTable.Columns.Count - 3] = dsBranchList.Tables[0].Rows[j]["terrrep"].ToString();  // trep
                            dr[oldTable.Columns.Count - 1] = dsBranchList.Tables[0].Rows[j]["srvcrep"].ToString();  // srep

                            // Add the new row to the result table

                            DataRow drResultNew = dsResult.Tables[0].NewRow();
                            drResultNew.ItemArray = dr.ItemArray;
                            dsResult.Tables[0].Rows.Add(drResultNew);
                            //dsResult.Tables[0].ImportRow(dr);
                        }

                        // Add a spacer row after each main distributor's sales location list
                        dr = dsResult.Tables[0].NewRow();
                        dsResult.Tables[0].Rows.Add(dr);
                    }
                }
            }

            // Excel processing
            Microsoft.Office.Interop.Excel.ApplicationClass excel = new ApplicationClass();
            excel.Application.Workbooks.Add(true);

            Cursor.Current = Cursors.WaitCursor;
            lblWait.Text = "Please wait while your report is generated...";

            for (int i = 0; i < dsResult.Tables.Count; i++)
            {

                System.Data.DataTable table = dsResult.Tables[i];

                Worksheet wkSheetLast;
                wkSheetLast = new Worksheet();

                //excel.Application.Worksheets.Add(0,,1,null);
                Microsoft.Office.Interop.Excel.Worksheet sheet = null;

                if (sheet != null)
                    sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.ActiveWorkbook.Worksheets.Add(Missing.Value, sheet, Missing.Value, Missing.Value);
                else
                    sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.ActiveSheet;


                sheet.Name = "BOMAG AMERICAS DISTRIBUTORS";


                // special stuff.

                sheet.Cells[1, 1] = "BOMAG AMERICAS DISTRIBUTORS";

                int ColumnIndex = 0;
                foreach (DataColumn col in table.Columns)
                {
                    ColumnIndex++;
                    sheet.Cells[3, ColumnIndex] = col.ColumnName;
                }

                int rowIndex = 3;
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

                if (i < dsResult.Tables.Count - 1)
                    excel.Application.ActiveWorkbook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            }



            Cursor.Current = Cursors.Default;
            lblWait.Text = "";
            excel.Visible = true;
        }

        private void btnAllContractPages_Click(object sender, EventArgs e)
        {
            DDA.DataObjects.AppData.ReportName = "Full Contract Report";

            frmAllContractReportViewer fvr = new frmAllContractReportViewer();

            this.Hide();
            fvr.ShowDialog();
            this.Show();
        }

        private void btnMapReports_Click(object sender, EventArgs e)
        {
            DDA.Interfaces.frmMapReport frmMapReport = new DDA.Interfaces.frmMapReport();
            this.Hide();
            frmMapReport.ShowDialog();
            this.Show();
        }






    }
}