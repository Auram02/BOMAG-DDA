
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Dymo;



namespace DDA.Interfaces
{

    public partial class frmContractInformation : Form
    {

        public IDymoAddIn4 DymoAddIn;
        public IDymoLabels DymoLabels;

        public frmContractInformation()
        {
            InitializeComponent();
            DDA.DataObjects.AppData.DisableCloseButton(Handle);

            if (DDA.DataObjects.AppData.ViewContractMode == true)
            {
            }
            else
            {
                btnPrint.Visible = false;
                btnPrintLabels.Visible = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // to update the date that this record was modified
            DDA.DataObjects.AppData.CurrentContract.ContractDate = dtpContract.Value.ToShortDateString();
            DDA.DataObjects.AppData.CurrentContract.ModifyDate = lblModifyDate.Text;

            if (DDA.DataObjects.AppData.CurrentContract.ContractMode == "UPDATE")
            {
               string result = DDA.DataAccess.Contract_da.UpdateContract();
            }
            else if (DDA.DataObjects.AppData.CurrentContract.ContractMode == "NEW")
            {
                string result = DDA.DataAccess.Contract_da.AddContract();
            }

            MessageBox.Show("Data Saved");

            // Cleanup
            frmTerritory frmTerritory = new frmTerritory();
            frmTerritory.Show();
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmTerritory frmTerritory = new frmTerritory();
            frmTerritory.Show();
            this.Close();
        }

        private void frmContractInformation_Load(object sender, EventArgs e)
        {
            DDA.DataObjects.AppData.CurrentContract.ClearReportData();
         
            lblContractNumber.Text = DDA.DataObjects.AppData.CurrentContract.ContractNumber;


            if (DDA.DataObjects.AppData.CurrentContract.ContractMode == "NEW")
            {
                lblModifyDate.Text = Convert.ToString(System.DateTime.Now.ToShortDateString());
                dtpContract.Text = Convert.ToString(System.DateTime.Now.ToShortDateString());
            }
            else
            {
                // load from contract
                dtpContract.Text = DDA.DataObjects.AppData.CurrentContract.ContractDate;
                lblContractDate.Text = dtpContract.Text;

                if (DDA.DataObjects.AppData.DataChanged == true)
                {
                    lblModifyDate.Text = Convert.ToString(System.DateTime.Now.ToShortDateString());
                }
                else
                {
                    lblModifyDate.Text = DDA.DataObjects.AppData.CurrentContract.ModifyDate;
                }

            }
            
            if (DDA.DataObjects.AppData.ViewContractMode == true)
            {
                dtpContract.Visible = false;
                lblContractDate.Visible = true;
                btnSave.Visible = false;
            }
            else
            {
                dtpContract.Visible = true;
                lblContractDate.Visible = false;
            }

            string states;
            string categories;
            string distributors;
            string branch_locations;
            string counties;
            string countyID;
            string distributorID;

            counties = "";
            countyID = "";
            categories = "";
            states = "";
            distributors = "";
            branch_locations = "";
            distributorID = "";

            int i;
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
                lstCounties.Items.Add(countyEntry);
                DDA.DataObjects.AppData.CurrentContract._Counties.Add(countyEntry);
            }

            for (i = 0; i < DDA.DataObjects.AppData.CurrentContract.Categories.Count; i++)
            {
                if (categories != "")
                {
                    categories = categories + ", ";
                }

                lstCategory.Items.Add(DDA.DataAccess.Category_da.GetCategoryName(Convert.ToInt32(DDA.DataObjects.AppData.CurrentContract.Categories[i].ToString())));
                DDA.DataObjects.AppData.CurrentContract._Category.Add(DDA.DataAccess.Category_da.GetCategoryName(Convert.ToInt32(DDA.DataObjects.AppData.CurrentContract.Categories[i].ToString())));

                categories = categories + DDA.DataAccess.Category_da.GetCategoryName(Convert.ToInt32(DDA.DataObjects.AppData.CurrentContract.Categories[i].ToString()));
            }
            //lblCategory.Text = categories;
            lblCategory.Text = "";

            for (i = 0; i < DDA.DataObjects.AppData.CurrentContract.Branches.Count; i++)
            {
                if (distributors != "")
                {
                    distributors = distributors + ", ";
                    distributorID = distributorID + ", ";
                }

                //lstDistName.Items.Add(DDA.DataAccess.Distributor_da.GetDistributorName(Convert.ToInt32(DDA.DataObjects.AppData.CurrentContract.Branches[i].ToString())));

                distributors = distributors + DDA.DataAccess.Distributor_da.GetDistributorName(Convert.ToInt32(DDA.DataObjects.AppData.CurrentContract.Branches[i].ToString()));
                distributorID = distributorID + Convert.ToInt32(DDA.DataObjects.AppData.CurrentContract.Branches[i].ToString());

            }

            try
            {
                lblDistributorName.Text = DDA.DataAccess.Distributor_da.GetDistributorName(DDA.DataObjects.AppData.DistributorID);
            }
            catch
            {
                lblDistributorName.Text = "";
            }


            DataSet ds = new DataSet();
            ds = DDA.DataAccess.Location_da.GetStateList(countyIdList);
            try
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (states != "")
                    {
                        states = states + ", ";
                    }

                    lstState.Items.Add(ds.Tables[0].Rows[i][0]);
                    DDA.DataObjects.AppData.CurrentContract._State.Add(ds.Tables[0].Rows[i][0]);

                    states = states + ds.Tables[0].Rows[i][0];

                }
            }
            catch
            {

            }

            //lblState.Text = states;
            lblState.Text = "";

            string _CityName;
            string _StateAbbr;
            _CityName = "";
            _StateAbbr = "";

            if (distributorID == "")
                distributorID = Convert.ToInt32(DDA.DataObjects.AppData.DistributorID).ToString();

            ds = DDA.DataAccess.Location_da.GetDistributorLocationList(distributorID);
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

                    lstBranchLocations.Items.Add(_CityName + ", " + _StateAbbr);
                    DDA.DataObjects.AppData.CurrentContract._SalesLocation.Add(_CityName + ", " + _StateAbbr);

                    branch_locations = branch_locations + "(" + _CityName + "," + _StateAbbr + ")";
                }
            }
            catch (Exception ex)
            {
                // do nothing
            }


            //lblBranchLocations.Text = branch_locations;
            lblBranchLocations.Text = "";

            try
            {
                // ######################################
                // #  SETUP DYMO PRINTER INFO
                // ######################################
                // create DYMO COM objects
                DymoAddIn = new DymoAddInClass();
                DymoLabels = new DymoLabelsClass();

                string fileName;
                fileName = Application.StartupPath + "\\contractnumber_label.LWL";
                bool result;
                DymoAddIn.Open(fileName);

                // show the file name

                FileNameEdit.Text = DymoAddIn.FileName;

                // FileNameEdit.Text = 

                // populate label objects
                SetupLabelObject();

                // obtain the currently selected printer
                SetupLabelWriterSelection(true);

            }
            catch (Exception ex)
            {
                if (LabelWriterCmb.Text == "")
                {
                    MessageBox.Show("No Labelwriter Printer Detected.  You will not be able to print labels from this computer.");
                    btnPrintLabels.Enabled = false;
                }
            }


        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            frmDistributorList frmDistList = new frmDistributorList();

            frmDistList.Show();
            this.Close();
        }

        /////

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern bool BitBlt(
IntPtr hdcDest, // handle to destination DC
int nXDest, // x-coord of destination upper-left corner
int nYDest, // y-coord of destination upper-left corner
int nWidth, // width of destination rectangle
int nHeight, // height of destination rectangle
IntPtr hdcSrc, // handle to source DC
int nXSrc, // x-coordinate of source upper-left corner
int nYSrc, // y-coordinate of source upper-left corner
System.Int32 dwRop // raster operation code
);


        Bitmap MemoryImage;


        //        Private Sub CaptureScreen()
        //   Dim mygraphics As Graphics = Me.CreateGraphics()
        //   Dim s As Size = Me.Size
        //   memoryImage = New Bitmap(s.Width, s.Height, mygraphics)
        //   Dim memoryGraphics As Graphics = Graphics.FromImage(memoryImage)
        //   Dim dc1 As IntPtr = mygraphics.GetHdc
        //   Dim dc2 As IntPtr = memoryGraphics.GetHdc
        //   BitBlt(dc2, 0, 0, Me.ClientRectangle.Width, _
        //      Me.ClientRectangle.Height, dc1, 0, 0, 13369376)
        //   mygraphics.ReleaseHdc(dc1)
        //   memoryGraphics.ReleaseHdc(dc2)
        //End Sub

        private void CaptureScreen()
        {
            Graphics mygraphics = this.CreateGraphics();
            Size s = this.Size;
            MemoryImage = new Bitmap(s.Width, s.Height, mygraphics);

            Graphics memoryGraphics = Graphics.FromImage(MemoryImage);
            IntPtr dc1 = mygraphics.GetHdc();
            IntPtr dc2 = memoryGraphics.GetHdc();
            BitBlt(dc2, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height, dc1, 0, 0, 13369376);
            mygraphics.ReleaseHdc(dc1);
            memoryGraphics.ReleaseHdc(dc2);

        }
        private void ThePrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(MemoryImage, 0, 0);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {


            DDA.DataObjects.AppData.ReportName = "Contract Information Quickview";
            frmViewReport fvr = new frmViewReport();
            this.Hide();
            fvr.ShowDialog();
            this.Show();

            // This was a temporary feature fix.  It has been replace by the above code
            //// capture current screen
            //CaptureScreen();

            //try
            //{

            //    PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog(); // instantiate new print preview dialog
            //    printPreviewDialog1.Document = this.ThePrintDocument;

            //    printPreviewDialog1.ShowDialog(); // Show the print preview dialog, uses print page event to draw preview screen
            //}
            //catch (Exception exp)
            //{
            //    MessageBox.Show("Error in Print Preview: " + exp.Message);
            //}

        }

        private void btnPrintLabels_Click(object sender, EventArgs e)
        {

            DataSet dsStates = new DataSet();
            dsStates = DDA.DataAccess.Contract_da.GetContractStates(lblContractNumber.Text);

            string states;
            states = "";

            for (int i = 0; i < dsStates.Tables[0].Rows.Count; i++)
            {
                if (states != "")
                    states = states + ", ";

                states = states + dsStates.Tables[0].Rows[i]["Abbreviation"].ToString();

            }

            DataSet dsCat;
            dsCat = DDA.DataAccess.Contract_da.GetContractCategories(lblContractNumber.Text);

            string categories;
            categories = "";

            for (int i = 0; i < dsCat.Tables[0].Rows.Count; i++)
            {
                if (categories != "")
                    categories = categories + ", ";

                categories = categories + dsCat.Tables[0].Rows[i]["CategoryName"].ToString();
            }


            // Set the object attributes
            DymoLabels.SetField("DistributorName", lblDistributorName.Text);
            DymoLabels.SetField("States", states);
            DymoLabels.SetField("Categories", categories);
            DymoLabels.SetField("ContractNumber", lblContractNumber.Text);


            //MessageBox.Show(states + "  -  " + categories);

            // ATTENTION: This call is very important if you're making mutiple calls to the Print() or Print2() function!
            // It's a good idea to always wrap StartPrintJob() and EndPrintJob() around a call to Print() or Print2() function.
            DymoAddIn.StartPrintJob();

            // print two copies
            DymoAddIn.Print2(1, false, 1);

            DymoAddIn.EndPrintJob();

        }



        // ############################
        // #  DYMO Functions
        // ###########################
        private void SetupLabelObject()
        {


            // get the objects on the label
            string ObjNames = DymoLabels.GetObjectNames(false);

            // parse the result
            if (ObjNames != null)
            {
                int i = ObjNames.IndexOf('|');
                while (i >= 0)
                {
                    //ObjectNameCmb.Items.Add(ObjNames.Substring(0, i));
                    ObjNames = ObjNames.Remove(0, i + 1);
                    i = ObjNames.IndexOf('|');
                }
                //if (ObjNames.Length > 0)
                //ObjectNameCmb.Items.Add(ObjNames);

                //ObjectNameCmb.SelectedIndex = 0;
            }
        }


        private void SetupLabelWriterSelection(bool InitCmb)
        {
            // get the objects on the label
            if (InitCmb)
            {
                // clear all items first
                LabelWriterCmb.Items.Clear();

                string PrtNames = DymoAddIn.GetDymoPrinters();

                if (PrtNames != null)
                {
                    // parse the result
                    int i = PrtNames.IndexOf('|');
                    while (i >= 0)
                    {
                        LabelWriterCmb.Items.Add(PrtNames.Substring(0, i));
                        PrtNames = PrtNames.Remove(0, i + 1);
                        i = PrtNames.IndexOf('|');
                    }
                    if (PrtNames.Length > 0)
                        LabelWriterCmb.Items.Add(PrtNames);

                    PrtNames = DymoAddIn.GetCurrentPrinterName();
                    if (PrtNames != null)
                        LabelWriterCmb.SelectedIndex = LabelWriterCmb.Items.IndexOf(PrtNames);
                    else
                        LabelWriterCmb.SelectedIndex = 0;
                }
            }

            // check if selected/current printer is a twin turbo printer
            TrayCmb.Enabled = DymoAddIn.IsTwinTurboPrinter(LabelWriterCmb.Text);
            if (TrayCmb.Enabled)
            {
                // show the current tray selection if the printer
                // is a twin turbo
                switch (DymoAddIn.GetCurrentPaperTray())
                {
                    case 0: // left tray
                        TrayCmb.SelectedIndex = 0;
                        break;

                    case 1: // right tray
                        TrayCmb.SelectedIndex = 1;
                        break;

                    case 2: // auto switch
                        TrayCmb.SelectedIndex = 2;
                        break;

                    default: // tray selection not set, so default to auto switch
                        TrayCmb.SelectedIndex = 2;
                        break;
                }
            }
        }

        private void LabelWriterCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            DymoAddIn.SelectPrinter(LabelWriterCmb.Text);
            SetupLabelWriterSelection(false);
        }

        private void BrowseBtn_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // open user selected label file
                if (DymoAddIn.Open(openFileDialog1.FileName))
                {
                    // show the file name
                    FileNameEdit.Text = openFileDialog1.FileName;

                    // populate label objects
                    SetupLabelObject();

                    // setup paper tray selection
                    SetupLabelWriterSelection(false);
                }
            }
        }

        private void btnPrintMap_Click(object sender, EventArgs e)
        {

            com.findbomag.www.MapReportService svc = new com.findbomag.www.MapReportService();

            DataSet dsStates = new DataSet();
            dsStates = DDA.DataAccess.Contract_da.GetContractStates(lblContractNumber.Text);

            List<string> states = new List<string>();
            for (int i = 0; i < dsStates.Tables[0].Rows.Count; i++)
            {
                states.Add(dsStates.Tables[0].Rows[i]["FullName"].ToString());

            }


            for (int i = 0; i < DDA.DataObjects.AppData.CurrentContract.Categories.Count; i++)
            {
                int categoryID = Convert.ToInt32(DDA.DataObjects.AppData.CurrentContract.Categories[i].ToString());
                string categoryName = DDA.DataAccess.Category_da.GetCategoryName(categoryID);

                DataSet ds = DDA.DataObjects.Reports.GenerateContractListByCategoryListStateList(categoryID, states);

                string reportID = svc.UploadReportData(ds, categoryName, states.ToArray());
                reportID = reportID.Substring(0, reportID.IndexOf(".json"));

                foreach (string stateName in states)
                {
                    int stateID = DDA.DataAccess.Location_da.GetStateID(stateName);
                    DDA.DataAccess.MapReport_da.UpdateMapReport(stateID, categoryID, true, true);
                }

                System.Diagnostics.Process.Start("http://www.findbomag.com/admin/Reports/MapReport.aspx?id=" + reportID);
            }


            
            
        }


    }

}