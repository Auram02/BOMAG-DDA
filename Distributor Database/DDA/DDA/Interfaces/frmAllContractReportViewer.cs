using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Collections;
using System.Xml;
using System.IO;
using DDA.DataObjects;

using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;

using System.Text.RegularExpressions;

// split/merge multiple pdf documents
using Gios.Pdf.SplitMerge;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace DDA.Interfaces
{
    public partial class frmAllContractReportViewer : Form
    {
        DDA.Report_Datasources.rptCIV_SingleItemData tmpData = new DDA.Report_Datasources.rptCIV_SingleItemData();
        public static ReportDocument rptDoc;
        public static PdfSplitterMerger psm;
        public static PdfDocument outputDocument = new PdfDocument();

        public frmAllContractReportViewer()
        {
            InitializeComponent();

            this.Load += new EventHandler(frmAllContractReportViewer_Load);
        }

        void frmAllContractReportViewer_Load(object sender, EventArgs e)
        {
        }

        private void PrintAllContracts()
        {
            string contractNumber = "";
            int distID = 0;

            DataSet ds;
            ds = DDA.DataAccess.Distributor_da.GetDistributorList("Main", "");

            DataSet dsCont;

            outputDocument = new PdfSharp.Pdf.PdfDocument();


            progressBar1.Minimum = 0;
            progressBar1.Maximum = DDA.DataAccess.Contract_da.GetContractCount(false);
            progressBar1.Value = 0;
            progressBar1.Step = 1;

            // initialize report
            string rptPath;
            rptPath = Application.StartupPath + "\\Reports\\rptContractInfo.rpt";
            rptDoc = new ReportDocument();
            rptDoc.Load(rptPath);
            int contractTotal = 0;
            List<string> contractsTemp = new List<string>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                rptDoc = new ReportDocument();
                rptDoc.Load(rptPath);


                distID = Convert.ToInt32(ds.Tables[0].Rows[i]["pk_DistributorID"].ToString());
                
                dsCont = DDA.DataAccess.Contract_da.GetContractList(distID);


                for (int j = 0; j < dsCont.Tables[0].Rows.Count; j++)
                {
                    //cboContractNumber.Items.Add(ds.Tables[0].Rows[i]["ContractNumber"]);

                    contractNumber = dsCont.Tables[0].Rows[j]["ContractNumber"].ToString();
                    Application.DoEvents();

                    SetupReport(contractNumber, distID, true, "");

                    progressBar1.PerformStep();  // perform a step
                    Application.DoEvents();

                    contractTotal += 1;
                    contractsTemp.Add(contractNumber);
                }

                rptDoc.Close();

                long modResult = (Convert.ToInt64(i) % Convert.ToInt64(20));
                if (modResult == 0)
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

            }

            string filename = "AllContractReport_" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day + "-" + System.DateTime.Now.Year + ".pdf";

            System.IO.File.Delete(filename);

            // Save the document...
            outputDocument.Save(filename);
            // ...and start a viewer.
            //Process.Start(filename);
            outputDocument.Close();
            outputDocument.Dispose();

            // open the file
            System.Diagnostics.Process.Start(filename);

        }

        public void SetupReport(string contractNumber, int distID, bool instantPrint, string printerName)
        {
            DDA.DataAccess.Contract_da.GetContract(contractNumber);
            GetData("");

            try
            {
                rptDoc.SetDataSource(tmpData);
            }
            catch (Exception ex)
            {
                string rptPath;
                rptPath = Application.StartupPath + "\\Reports\\rptContractInfo.rpt";
                rptDoc = new ReportDocument();

                rptDoc.Load(rptPath);
                rptDoc.SetDataSource(tmpData);

                try
                {

                    outputDocument.Save("errorOutput.pdf");
                }
                catch
                {
                }

            }

            if (instantPrint == true)
            {
                PdfDocument inputDocument = PdfReader.Open(rptDoc.ExportToStream(ExportFormatType.PortableDocFormat), PdfDocumentOpenMode.Import);

                // Iterate pages
                int count = inputDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    // Get the page from the external document...
                    PdfPage page = inputDocument.Pages[idx];
                    // ...and add it to the output document.
                    outputDocument.AddPage(page);
                }

                tmpData.Clear();

            }

        }

        private void SetupDataLoad()
        {
            string states = "";
            string categories = "";
            string distributors = "";
            string branch_locations = "";
            string counties = "";
            string countyID = "";
            string distributorID = "";

            string _CityName;
            string _StateAbbr;
            _CityName = "";
            _StateAbbr = "";
            int i;

            string distributorBranchNames = "";
            string distributorBranchIDs = "";

            
            
            //for (i = 0; i < dsBranches; i++)

            string sql = "SELECT * FROM ContractDistributor INNER JOIN Distributor on ContractDistributor.fk_DistributorId = Distributor.pk_DistributorId ";
            sql += " WHERE ManufacturerRep = 0 AND fk_contractID = " + DDA.DataObjects.AppData.CurrentContract.ContractID;

            DataSet dsBranches  = DataLogic.DBA.DataLogic.Read(sql);

            foreach (DataRow drBranch in dsBranches.Tables[0].Rows)
            {
                if (distributorBranchIDs != "")
                {
                    distributorBranchNames = distributorBranchNames + ", ";
                    distributorBranchIDs = distributorBranchIDs + ", ";
                }

                //lstDistName.Items.Add(DDA.DataAccess.Distributor_da.GetDistributorName(Convert.ToInt32(DDA.DataObjects.AppData.CurrentContract.Branches[i].ToString())));

                distributorBranchNames = distributorBranchNames + drBranch["DistName"].ToString();
                distributorBranchIDs = distributorBranchIDs + Convert.ToInt32(drBranch["pk_DistributorID"].ToString());

            }

            DataSet ds = DDA.DataAccess.Location_da.GetDistributorLocationList(distributorBranchIDs);
            try
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (branch_locations != "")
                    {
                        branch_locations = branch_locations + ", ";
                    }

                    _CityName = ds.Tables[0].Rows[i][0].ToString();
                    _StateAbbr = ds.Tables[0].Rows[i][1].ToString();

                    DDA.DataObjects.AppData.CurrentContract._SalesLocation.Add(_CityName + ", " + _StateAbbr);

                    branch_locations = branch_locations + "(" + _CityName + "," + _StateAbbr + ")";
                }
            }
            catch (Exception ex)
            {
                // do nothing
            }



            string countyIdList = "";
            for (i = 0; i < DDA.DataObjects.AppData.CurrentContract.Counties.Count; i++)
            {
                if (countyIdList.Length > 0)
                    countyIdList += ",";

                countyIdList += Convert.ToString(DDA.DataObjects.AppData.CurrentContract.Counties[i]);

            }

            DataSet dsCountyStateAbbreviation = DDA.DataAccess.Location_da.GetCountyStateAbbreviationList(countyIdList);

            foreach (DataRow dr in dsCountyStateAbbreviation.Tables[0].Rows)
            {
                string countyEntry = dr["CountyName"].ToString() + ", " + dr["Abbreviation"].ToString();
                
                DDA.DataObjects.AppData.CurrentContract._Counties.Add(countyEntry);
            }

            ds = DDA.DataAccess.Location_da.GetStateList(countyIdList);
            try
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (states != "")
                    {
                        states = states + ", ";
                    }

                    DDA.DataObjects.AppData.CurrentContract._State.Add(ds.Tables[0].Rows[i][0]);

                    states = states + ds.Tables[0].Rows[i][0];

                }
            }
            catch
            {

            }

            for (i = 0; i < DDA.DataObjects.AppData.CurrentContract.Categories.Count; i++)
            {
                if (categories != "")
                {
                    categories = categories + ", ";
                }

                DDA.DataObjects.AppData.CurrentContract._Category.Add(DDA.DataAccess.Category_da.GetCategoryName(Convert.ToInt32(DDA.DataObjects.AppData.CurrentContract.Categories[i].ToString())));

                categories = categories + DDA.DataAccess.Category_da.GetCategoryName(Convert.ToInt32(DDA.DataObjects.AppData.CurrentContract.Categories[i].ToString()));
            }
        }

        private void GetData(string reportSource)
        {
            ReportDataSource rds;
            tmpData = new Report_Datasources.rptCIV_SingleItemData();
            DDA.DataObjects.AppData.CurrentContract.ClearReportData();

            int childContractID = DDA.DataAccess.Contract_da.GetChildContractId(DDA.DataObjects.AppData.CurrentContract.ContractNumber);

            DataTable dt;
            dt = new DataTable();

            dt.Columns.Add("Branches");



            string branches = "";

            SetupDataLoad();

            foreach (string item in DDA.DataObjects.AppData.CurrentContract._SalesLocation)
            {
                if (branches != "")
                    branches = branches + "     ";

                branches = branches + item;

            }

            tmpData.dtBranches.Rows.Add(branches);
            // ds = objArray.ToDataSet();

            rds = new ReportDataSource("rptCIV_SingleItemData_dtBranches", dt);
            reportViewer1.LocalReport.DataSources.Add(rds);


            DataTable dt2;
            dt2 = new DataTable();
            dt2.Columns.Add("Category");

            string category = "";

            foreach (string item in DDA.DataObjects.AppData.CurrentContract._Category)
            {
                if (category != "")
                    category = category + "     ";

                category = category + item;

            }

            tmpData.dtCategory.Rows.Add(category);

            rds = new ReportDataSource("rptCIV_SingleItemData_dtCategory", dt2);
            reportViewer1.LocalReport.DataSources.Add(rds);


            DataTable dt3;
            dt3 = new DataTable();
            dt3.Columns.Add("ContractNumber");
            dt3.Columns.Add("DistributorName");
            dt3.Columns.Add("ModifiedDate");
            dt3.Columns.Add("ContractDate");
            dt3.Columns.Add("ChildContractNumber");

            DataRow dr = dt3.NewRow();

            dr[0] = DDA.DataObjects.AppData.CurrentContract.ContractNumber;

            if (DDA.DataObjects.AppData.CurrentContract.Branches.Count > 0)
                dr[1] = DDA.DataAccess.Distributor_da.GetDistributorName(Convert.ToInt32(DDA.DataObjects.AppData.CurrentContract.Branches[0]));
            else
                dr[1] = DDA.DataAccess.Distributor_da.GetDistributorName(DDA.DataObjects.AppData.DistributorID);
            
            dr[2] = DDA.DataObjects.AppData.CurrentContract.ModifyDate;
            dr[3] = DDA.DataObjects.AppData.CurrentContract.ContractDate;
            dr[4] = "None";

            // childContractID
            if (childContractID > -1)
            {
                string contractNumber = DDA.DataAccess.Contract_da.GetContractNumber(childContractID);
                dr[4] = contractNumber;
            }

            tmpData.dtSingleData.Rows.Add(dr.ItemArray);

            rds = new ReportDataSource("rptCIV_SingleItemData_dtSingleData", dt3);
            reportViewer1.LocalReport.DataSources.Add(rds);


            dt2 = new DataTable();
            dt2.Columns.Add("CountyName");

            string countyName = "";

            foreach (string item in DDA.DataObjects.AppData.CurrentContract._Counties)
            {
                if (countyName != "")
                {
                    countyName = countyName + "     ";
                }

                countyName = countyName + item;

            }
            tmpData.dtCounty.Rows.Add(countyName);

            rds = new ReportDataSource("rptCIV_SingleItemData_dtCounty", dt2);
            reportViewer1.LocalReport.DataSources.Add(rds);



            dt2 = new DataTable();
            dt2.Columns.Add("StateName");

            string states = "";


            foreach (string item in DDA.DataObjects.AppData.CurrentContract._State)
            {
                if (states != "")
                    states = states + "     ";

                states = states + item;
            }

            tmpData.dtState.Rows.Add(states);

            rds = new ReportDataSource("rptCIV_SingleItemData_dtState", dt2);
            reportViewer1.LocalReport.DataSources.Add(rds);


            dt3 = new DataTable();
            dt3.Columns.Add("Category");
            category = "";

            if (childContractID > -1)
            {
                DataSet dsChild = DDA.DataAccess.Contract_da.GetContractCategories(childContractID);

                foreach (DataRow drChild in dsChild.Tables[0].Rows)
                {
                    if (category != "")
                        category = category + "     ";

                    category = category + drChild["CategoryName"];
                }
            }

            tmpData.dtChildCategories.Rows.Add(category);

            rds = new ReportDataSource("rptCIV_SingleItemData_dtChildCategories", dt3);
            reportViewer1.LocalReport.DataSources.Add(rds);
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerateAllReport_Click(object sender, EventArgs e)
        {
            PrintAllContracts();
        }
    }
}
