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
    public partial class frmContractNumberLabel : Form
    {
        public IDymoAddIn4 DymoAddIn;
        public IDymoLabels DymoLabels;

        public frmContractNumberLabel()
        {
            InitializeComponent();
            DDA.DataObjects.AppData.DisableCloseButton(Handle);


        }

        private void frmContractNumberLabel_Load(object sender, EventArgs e)
        {
            DataSet ds;
            ds = DDA.DataAccess.Distributor_da.GetMainDistributorNames();  // get main distributors

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                lstDistributors.Items.Add(ds.Tables[0].Rows[i]["Distributor Name"].ToString());
            }

            try
            {

                // ###########################
                // # Label writer setup
                // ###########################

                // create DYMO COM objects
                DymoAddIn = new DymoAddInClass();
                DymoLabels = new DymoLabelsClass();
                string fileName;
                fileName = Application.StartupPath + "\\contractnumber_label.LWL";
                bool result;
                DymoAddIn.Open2(fileName);

                // show the file name
                FileNameEdit.Text = DymoAddIn.FileName;

                // populate label objects
                SetupLabelObject();

                // obtain the currently selected printer
                SetupLabelWriterSelection(true);


            }
            catch
            {
                if (LabelWriterCmb.Text == "")
                {
                    MessageBox.Show("No Labelwriter Printer Detected.  You will not be able to print labels from this computer.");
                    btnSelectContract.Enabled = false;
                }
            }
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {			
            
            // clean up DYMO COM objects
            DymoAddIn = null;
            DymoLabels = null;
            this.Close();
        }

        private void lstDistributors_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstDistributors_Click(object sender, System.EventArgs e)
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

        private void btnSelectContract_Click(object sender, EventArgs e)
        {
            DataSet dsStates;

            if (lstContractNumbers.SelectedIndex > -1)
            {

                dsStates = DDA.DataAccess.Contract_da.GetContractStates(lstContractNumbers.SelectedItem.ToString());

                string states;
                states = "";

                for (int i = 0; i < dsStates.Tables[0].Rows.Count; i++)
                {
                    if (states != "")
                        states = states + ", ";

                    states = states + dsStates.Tables[0].Rows[i]["Abbreviation"].ToString();

                }

                DataSet dsCat;
                dsCat = DDA.DataAccess.Contract_da.GetContractCategories(lstContractNumbers.SelectedItem.ToString());

                string categories;
                categories = "";

                for (int i = 0; i < dsCat.Tables[0].Rows.Count; i++)
                {
                    if (categories != "")
                        categories = categories + ", ";

                    categories = categories + dsCat.Tables[0].Rows[i]["CategoryName"].ToString();
                }


                // Set the object attributes
                DymoLabels.SetField("DistributorName", DDA.DataObjects.AppData.ToTitleCase(lstDistributors.SelectedItem.ToString()));
                DymoLabels.SetField("States", states);
                DymoLabels.SetField("Categories", categories);
                DymoLabels.SetField("ContractNumber", lstContractNumbers.SelectedItem.ToString());


                //MessageBox.Show(states + "  -  " + categories);

                // ATTENTION: This call is very important if you're making mutiple calls to the Print() or Print2() function!
                // It's a good idea to always wrap StartPrintJob() and EndPrintJob() around a call to Print() or Print2() function.
                DymoAddIn.StartPrintJob();

                // print two copies
                DymoAddIn.Print2(1, false, 1);

                // ATTENTION: Every StartPrintJob() must have a matching EndPrintJob()
                DymoAddIn.EndPrintJob();
            }
            else
            {
                MessageBox.Show("Please Make Sure You Have Selected Both a Distributor and a Contract Number Before Printing");
            }
        }



        // ################################################
        // # LABEL WRITER STUFF
        // ################################################

        //private void SetupLabelWriterSelection(bool InitCmb)
        //{
        //    // get the objects on the label
        //    if (InitCmb)
        //    {
        //        // clear all items first
        //        LabelWriterCmb.Items.Clear();

        //        string PrtNames = DymoAddIn.GetDymoPrinters();

        //        if (PrtNames != null)
        //        {
        //            // parse the result
        //            int i = PrtNames.IndexOf('|');
        //            while (i >= 0)
        //            {
        //                LabelWriterCmb.Items.Add(PrtNames.Substring(0, i));
        //                PrtNames = PrtNames.Remove(0, i + 1);
        //                i = PrtNames.IndexOf('|');
        //            }
        //            if (PrtNames.Length > 0)
        //                LabelWriterCmb.Items.Add(PrtNames);

        //            PrtNames = DymoAddIn.GetCurrentPrinterName();
        //            if (PrtNames != null)
        //                LabelWriterCmb.SelectedIndex = LabelWriterCmb.Items.IndexOf(PrtNames);
        //            else
        //                LabelWriterCmb.SelectedIndex = 0;
        //        }
        //    }
        //}

        private void SetupLabelObject()
        {


            // get the objects on the label
            string ObjNames = DymoLabels.GetObjectNames(true);

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



    }
}