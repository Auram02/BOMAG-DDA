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

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace DDA.Interfaces
{
    public partial class frmViewReport : Form
    {
        DDA.Report_Datasources.rptCIV_SingleItemData tmpData = new DDA.Report_Datasources.rptCIV_SingleItemData();

        public frmViewReport()
        {
            InitializeComponent();
            DDA.DataObjects.AppData.DisableCloseButton(Handle);
            
        }

        
        private void frmViewReport_Load(object sender, EventArgs e)
        {

            
             //set the local report path (note: the properties of the project post-build event
            // include this command in order to use this embedded report from the bin directory:
            // copy "$(ProjectDir)\Report1.rdlc" "$(TargetDir)"

            ////////this.reportViewer1.Clear();

            //////////this.reportViewer1.LocalReport.ReportPath = "rptContractInfoQuickview.rdlc";
            ////////        // reportViewer1.LocalReport.DataSources.Add(GetData("rptCIV_SingleItemData_dtSingleItems"));
            ////////reportViewer1.Refresh();
            ////////reportViewer1.LocalReport.DataSources.Clear();
            ////////reportViewer1.LocalReport.Refresh();


            GetData("");

            ReportDocument rptDoc = new ReportDocument();
            string rptPath;
            rptPath = Application.StartupPath + "\\Reports\\rptContractInfo.rpt";

            rptDoc.Load(rptPath);


            rptDoc.SetDataSource(tmpData);

            
            this.crystalReportViewer1.ReportSource = rptDoc;
            ////////this.reportViewer1.RefreshReport();
        }


        private void GetData(string reportSource)
        {
            ReportDataSource rds;
            int childContractID = DDA.DataAccess.Contract_da.GetChildContractId(DDA.DataObjects.AppData.CurrentContract.ContractNumber);

            DataTable dt;
            dt = new DataTable();

            dt.Columns.Add("Branches");

            string branches = "";

            foreach(string item in DDA.DataObjects.AppData.CurrentContract._SalesLocation)
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
    }
}