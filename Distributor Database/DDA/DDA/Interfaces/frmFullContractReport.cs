using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Xml;
using System.IO;
using DDA.DataObjects;


using System.Text.RegularExpressions;

// split/merge multiple pdf documents
using Gios.Pdf.SplitMerge;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace DDA.Interfaces
{
    public partial class frmFullContractReport : Form
    {
        public frmFullContractReport()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmFullContractReport_Load(object sender, EventArgs e)
        {
            DataSet ds;
            ds = DDA.DataAccess.Distributor_da.GetMainDistributorNames();  // get main distributors

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lstDistributors.Items.Add(ds.Tables[0].Rows[i]["Distributor Name"].ToString());
            }

            
            //for (int i = 0; i < System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count; i++)
            //    cmbPrinter.Items.Add(System.Drawing.Printing.PrinterSettings.InstalledPrinters[i].ToString());

            
            //cmbPrinter.SelectedIndex = 0;
        }

        void lstDistributors_Click(object sender, System.EventArgs e)
        {
            DataSet ds;
            int distID;
            distID = 0;

            distID = DDA.DataAccess.Distributor_da.GetMainDistributorID(Convert.ToString(lstDistributors.SelectedItem));


            ds = DDA.DataAccess.Contract_da.GetContractList(distID);

            lstContractNumbers.Items.Clear();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lstContractNumbers.Items.Add(ds.Tables[0].Rows[i]["ContractNumber"].ToString());
            }
        }


        private void lstDistributors_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                string contractNumber = lstContractNumbers.SelectedItem.ToString();
                int distID = DDA.DataAccess.Distributor_da.GetDistributorID(lstDistributors.SelectedItem.ToString());

                frmFullContractReportViewer fvr = new frmFullContractReportViewer(contractNumber, distID, false);
                this.Hide();

                fvr.ShowDialog();

                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Select a Distributor and a Contract Number Before Proceeding");
                
                //                MessageBox.Show("An error has occurred.  Please make a note of what you were doing when this error occurred and contact the developer.  Error: " + ex.Message);
            }
        }

        private void btnPrintAll_Click(object sender, EventArgs e)
        {

            PrintAllContracts(false);

        }

        private void btnPrintDate_Click(object sender, EventArgs e)
        {

            PrintAllContracts(true);
        }

        private void PrintAllContracts(bool byModifiedDate)
        {
            string contractNumber = "";
            int distID = 0;

            DataSet ds;
            ds = DDA.DataAccess.Distributor_da.GetDistributorList("Main", "");

            DataSet dsCont;

            DDA.Interfaces.frmFullContractReportViewer.outputDocument = new PdfSharp.Pdf.PdfDocument();

            
            progressBar1.Minimum = 0;
            progressBar1.Maximum = DDA.DataAccess.Contract_da.GetContractCount();
            progressBar1.Value = 0;
            progressBar1.Step = 1;

            // initialize report
            string rptPath;
            rptPath = Application.StartupPath + "\\Reports\\rptFullContract.rpt";
            DDA.Interfaces.frmFullContractReportViewer.rptDoc = new ReportDocument();
            DDA.Interfaces.frmFullContractReportViewer.rptDoc.Load(rptPath);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DDA.Interfaces.frmFullContractReportViewer.rptDoc = new ReportDocument();
                DDA.Interfaces.frmFullContractReportViewer.rptDoc.Load(rptPath);


                distID = Convert.ToInt32(ds.Tables[0].Rows[i]["pk_DistributorID"].ToString());


                dsCont = DDA.DataAccess.Contract_da.GetContractList(distID);


                for (int j = 0; j < dsCont.Tables[0].Rows.Count; j++)
                {
                    //cboContractNumber.Items.Add(ds.Tables[0].Rows[i]["ContractNumber"]);

                    contractNumber = dsCont.Tables[0].Rows[j]["ContractNumber"].ToString();
                    Application.DoEvents();

                    if (byModifiedDate == true)
                    {
                        // by modified date
                        string startDate;
                        string endDate;
                        startDate = dtBegin.Text;
                        endDate = dtEnd.Text;

                        string conDate;
                        conDate = DDA.DataAccess.Contract_da.GetContractModifiedDate(contractNumber);

                        // If the contract date is great than or equal to our start range date and it is less than our end date then
                        // print the contract
                        // Sample Syntax (from ExpertsExchange): If (DateTime.Today < DateTime.Parse("01/8/2005") && DateTime.Today > DateTime.Parse("06/1/2005"))
                        if ((DateTime.Parse(conDate) >= DateTime.Parse(startDate)) && (DateTime.Parse(endDate) > DateTime.Parse(conDate)))
                        {
                            Application.DoEvents();
                            DDA.Interfaces.frmFullContractReportViewer.PrintOnly(contractNumber, distID, true, "");
                        } 

                    }
                    else
                    {
                        Application.DoEvents();
                        DDA.Interfaces.frmFullContractReportViewer.PrintOnly(contractNumber, distID, true, "");
                    }


                    progressBar1.PerformStep();  // perform a step
                    Application.DoEvents();

                }

                DDA.Interfaces.frmFullContractReportViewer.rptDoc.Close();

                long modResult = (Convert.ToInt64(i) % Convert.ToInt64(20));
                if (modResult == 0)
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

            }

            // output pdf filename
            string extraFilename = "";
            if (byModifiedDate == true)
                extraFilename = "ModifiedDate_";
            else
                extraFilename = "All_";

            string filename = "FullContractReport_" + extraFilename + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day + "-" + System.DateTime.Now.Year + ".pdf";

            System.IO.File.Delete(filename);

            // Save the document...
            DDA.Interfaces.frmFullContractReportViewer.outputDocument.Save(filename);
            // ...and start a viewer.
            //Process.Start(filename);
            DDA.Interfaces.frmFullContractReportViewer.outputDocument.Close();
            DDA.Interfaces.frmFullContractReportViewer.outputDocument.Dispose();

            // open the file
            System.Diagnostics.Process.Start(filename);

        }
        
    }
}