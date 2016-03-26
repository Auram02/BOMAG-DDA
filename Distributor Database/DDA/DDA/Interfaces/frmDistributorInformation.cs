using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DDA.DataAccess;
using DDA.DataObjects;

namespace DDA.Interfaces
{
    public partial class frmDistributorInformation : Form
    {
        private int distributorID;
        private int contactIndex;
        private int emailIndex;

        private string primaryName;
        private string primaryTitle;

        private void ShiftItemsUp()
        {


            label6.Location = new Point((int)(label6.Location.X), (int)(label6.Location.Y - 33));
            txtBillingAddress.Location = new Point((int)(txtBillingAddress.Location.X), (int)(txtBillingAddress.Location.Y - 33));
            label15.Location = new Point((int)(label15.Location.X), (int)(label15.Location.Y - 33));
            txtBillingCity.Location = new Point((int)(txtBillingCity.Location.X), (int)(txtBillingCity.Location.Y - 33));
            label14.Location = new Point((int)(label14.Location.X), (int)(label14.Location.Y - 33));
            cmbBillingState.Location = new Point((int)(cmbBillingState.Location.X), (int)(cmbBillingState.Location.Y - 33));
            label13.Location = new Point((int)(label13.Location.X), (int)(label13.Location.Y - 33));
            txtBillingZip.Location = new Point((int)(txtBillingZip.Location.X), (int)(txtBillingZip.Location.Y - 33));
            gpShippingInfo.Location = new Point((int)(gpShippingInfo.Location.X), (int)(gpShippingInfo.Location.Y - 33));
            label9.Location = new Point((int)(label9.Location.X), (int)(label9.Location.Y - 33));
            txtPhone.Location = new Point((int)(txtPhone.Location.X), (int)(txtPhone.Location.Y - 33));
            label10.Location = new Point((int)(label10.Location.X), (int)(label10.Location.Y - 33));
            txtFax.Location = new Point((int)(txtFax.Location.X), (int)(txtFax.Location.Y - 33));
            label12.Location = new Point((int)(label12.Location.X), (int)(label12.Location.Y - 33));
            txtEmail.Location = new Point((int)(txtEmail.Location.X), (int)(txtEmail.Location.Y - 33));
            txtState.Location = new Point((int)(txtState.Location.X), (int)(txtState.Location.Y - 33));
            lstEmail.Location = new Point((int)(lstEmail.Location.X), (int)(lstEmail.Location.Y - 33));
            btnRemoveEmail.Location = new Point((int)(btnRemoveEmail.Location.X), (int)(btnRemoveEmail.Location.Y - 33));
            btnAddEmail.Location = new Point((int)(btnAddEmail.Location.X), (int)(btnAddEmail.Location.Y - 33));
            label20.Location = new Point((int)(label20.Location.X), (int)(label20.Location.Y - 33));
            label21.Location = new Point((int)(label21.Location.X), (int)(label21.Location.Y - 33));
            txtEmailName.Location = new Point((int)(txtEmailName.Location.X), (int)(txtEmailName.Location.Y - 33));


            txtContact.Location = new Point((int)(txtContact.Location.X), (int)(txtContact.Location.Y - 33));
            lstContacts.Location = new Point((int)(lstContacts.Location.X), (int)(lstContacts.Location.Y - 33));
            txtContactTitle.Location = new Point((int)(txtContactTitle.Location.X), (int)(txtContactTitle.Location.Y - 33));
            btnAddContact.Location = new Point((int)(btnAddContact.Location.X), (int)(btnAddContact.Location.Y - 33));
            btnRemoveContact.Location = new Point((int)(btnRemoveContact.Location.X), (int)(btnRemoveContact.Location.Y - 33));
            btnCopyBillingInformation.Location = new Point((int)(btnCopyBillingInformation.Location.X), (int)(btnCopyBillingInformation.Location.Y - 33));

            label22.Location = new Point((int)(label22.Location.X), (int)(label22.Location.Y - 33));
            label11.Location = new Point((int)(label11.Location.X), (int)(label11.Location.Y - 33));
            label18.Location = new Point((int)(label18.Location.X), (int)(label18.Location.Y - 33));
            label19.Location = new Point((int)(label19.Location.X), (int)(label19.Location.Y - 33));
            label1.Location = new Point((int)(label1.Location.X), (int)(label1.Location.Y - 33));
            label16.Location = new Point((int)(label16.Location.X), (int)(label16.Location.Y - 33));
            txtSAP.Location = new Point((int)(txtSAP.Location.X), (int)(txtSAP.Location.Y - 33));

        }

        public frmDistributorInformation()
        {



            InitializeComponent();

            Location_da.PopulateStateDropdown(ref cmbState);
            Location_da.PopulateStateDropdown(ref cmbBillingState);

            DDA.DataObjects.AppData.DisableCloseButton(Handle);

            if (DDA.DataObjects.AppData.DistributorEditType == "Main")
            {
                lblTitle.Text = "DISTRIBUTOR INFORMATION";

                btnTerritoryReps.Visible = false;
                btnSalesReps.Visible = false;

                if (DDA.DataObjects.AppData.DistributorMode == "Edit")
                {
                    distributorID = DDA.DataObjects.AppData.DistributorID;
                    populateDistributorInformation(distributorID);

                    // move items up



                }
                else
                {
                    // Add mode...do nothing.

                }
            }
            else
            {
                lblTitle.Text = "SALES LOCATION INFORMATION";
                btnBranch.Visible = false;
                btnTerritory.Visible = false;
                chkPrimary.Visible = false;

            }


            if (DDA.DataObjects.AppData.DistributorMode == "Add")
            {
                btnBranch.Enabled = false;
                btnSalesReps.Enabled = false;
                btnTerritory.Enabled = false;
                btnTerritoryReps.Enabled = false;

                if (DDA.DataObjects.AppData.DistributorEditType != "Main")
                {
                    // hide node and everything else we need hidden
                    // branch
                    distributorID = DDA.DataObjects.AppData.BranchID;
                    label17.Visible = false;
                    txtNode.Visible = false;
                    txtNode.Text = "none";

                    string distName;
                    distName = DDA.DataAccess.Distributor_da.GetDistributorName(DDA.DataObjects.AppData.DistributorID);

                    lblBranchDistributorName.Visible = true;
                    lblBranchDistributorName.Text = distName;


                    // hide the distributor name text box
                    txtName.Visible = false;
                    txtName.Text = distName;  // for saving


                    ShiftItemsUp();
                    lblPartsOnly.Visible = true;
                    chkPartsOnly.Visible = true;
                }
            }
            else
            {
                if (DDA.DataObjects.AppData.DistributorEditType == "Main")
                {
                    distributorID = DDA.DataObjects.AppData.DistributorID;
                }
                else
                {
                    // branch
                    distributorID = DDA.DataObjects.AppData.BranchID;
                    label17.Visible = false;
                    txtNode.Visible = false;
                    txtNode.Text = "none";

                    string distName;
                    distName = DDA.DataAccess.Distributor_da.GetDistributorName(DDA.DataObjects.AppData.DistributorID);

                    lblBranchDistributorName.Visible = true;
                    lblBranchDistributorName.Text = distName;


                    // hide the distributor name text box
                    txtName.Visible = false;
                    txtName.Text = distName;  // for saving


                    ShiftItemsUp();
                    lblPartsOnly.Visible = true;
                    chkPartsOnly.Visible = true;


                }


                populateDistributorInformation(distributorID);
            }






        }


        private void populateDistributorInformation(int p_distributorID)
        {

            //Distributor objDist = new Distributor(p_distributorID);
            Distributor.NewDistributor(p_distributorID);  // new object

            txtName.Text = Distributor.Name;
            txtBillingAddress.Text = Distributor.BillingAddress;
            txtShippingAddress.Text = Distributor.ShippingAddress;
            txtCity.Text = Distributor.City;
            cmbState.Text = Distributor.State;
            txtZip.Text = Convert.ToString(Distributor.Zip);
            txtCountry.Text = Distributor.Country;
            txtPhone.Text = Distributor.Phone;
            txtFax.Text = Distributor.Fax;
            //txtContact.Text = Distributor.Contact;


            txtBillingCity.Text = Distributor.BillingCity;
            cmbBillingState.Text = DDA.DataAccess.Location_da.GetStateFullName2(Convert.ToInt32(Distributor.BillingState));
            txtBillingZip.Text = Distributor.BillingZip;
            txtNode.Text = Distributor.Node;
            txtSAP.Text = Distributor.SAP;

            Location_da.PopulateStateDropdown(ref cmbState);
            cmbState.Text = Distributor.State;

            if (Distributor.PartsOnly == 0)
            {
                chkPartsOnly.CheckState = CheckState.Unchecked;
            }
            else
            {
                chkPartsOnly.CheckState = CheckState.Checked;
            }


            if (Distributor.ManufacturerRep == 0)
            {
                chkManufacturerRep.CheckState = CheckState.Unchecked;
            }
            else
            {
                chkManufacturerRep.CheckState = CheckState.Checked;
            }

            // Only allow main distributors to change the ManufacturerRep status
            if (Distributor.MainDistributor == 0)
            {
                chkManufacturerRep.Visible = false;
                lblManufacturerRepCheckBox.Visible = false;
            }

            try
            {

                lstContacts.Items.Clear();  // Clear contacts
                foreach (string sArr in Distributor.Contacts)
                {
                    int isPrimary;
                    string tempContactName = sArr.Remove(sArr.IndexOf(","));
                    string tempTitle = sArr.Substring(sArr.IndexOf(", ") + 2);

                    isPrimary = DDA.DataAccess.Distributor_da.ContactPrimary(distributorID, tempContactName);

                    if (isPrimary == 1)
                    {
                        primaryName = tempContactName;
                        primaryTitle = tempTitle;
                    }


                    lstContacts.Items.Add(sArr);
                }

                ArrayList emailArr = new ArrayList();
                emailArr = Distributor.Emails;

                int i;
                lstEmail.Items.Clear();
                for (i = 0; i < emailArr.Count; i++)
                {
                    lstEmail.Items.Add(emailArr[i].ToString());
                }

            }
            catch (Exception ex)
            {

            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            if (DDA.DataObjects.AppData.DistributorEditType == "Branch")
            {
                //DDA.DataObjects.AppData.DistributorMode = "Add";
                //DDA.DataObjects.AppData.DistributorID = 0;

                frmDistributorBranchList frmDistBranchList = new frmDistributorBranchList();

                frmDistBranchList.Show();
                this.Close();
            }
            else
            {

                DDA.DataObjects.AppData.DistributorMode = "Add";
                //DDA.DataObjects.AppData.DistributorID = 0;

                frmDistributorList frmDBL = new frmDistributorList();

                frmDBL.Show();
                this.Close();
            }
        }



        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtZip.Text == "")
                txtZip.Text = "0";

            if (txtBillingZip.Text == "")
                txtBillingZip.Text = "0";

            bool IsFormFilledOut;


            string formMode;
            formMode = AppData.DistributorMode;

            //int stateID = Convert.ToInt32(cmbState.SelectedValue);

            string contacts;
            contacts = "";

            int i;


            string contactName, contactTitle;
            int isPrimary;



            int IsPartsOnly;
            if (chkPartsOnly.CheckState == CheckState.Checked)
            {
                IsPartsOnly = 1;
            }
            else
            {
                IsPartsOnly = 0;
            }

            int isManufacturerRep;
            if (chkManufacturerRep.CheckState == CheckState.Checked)
            {
                isManufacturerRep = 1;
            }
            else
            {
                isManufacturerRep = 0;
            }

            string nextBranchID;
            nextBranchID = "";

            int isMainDist;

            if (DDA.DataObjects.AppData.DistributorEditType == "Main")
            {
                isMainDist = 1;
            }
            else
            {
                isMainDist = 0;
            }

            if (formMode == "Add")
            {


                nextBranchID = Distributor_da.AddBranch("", txtName.Text, txtBillingAddress.Text, txtShippingAddress.Text,
                                            txtCity.Text, txtCountry.Text, txtFax.Text,
                                            txtPhone.Text, cmbState.Text, txtZip.Text, contacts, txtBillingCity.Text,
                                            cmbBillingState.Text, txtBillingZip.Text, txtCountry.Text, txtSAP.Text, txtNode.Text, isMainDist, IsPartsOnly, isManufacturerRep);

                // once it has been saved then update the distributor
                btnBranch.Enabled = true;
                btnSalesReps.Enabled = true;
                btnTerritory.Enabled = true;
                btnTerritoryReps.Enabled = true;

                if (DDA.DataObjects.AppData.DistributorEditType == "Branch")
                {
                    // Add the branch to the list of distributors
                    Distributor_da.AddBranch(DDA.DataObjects.AppData.DistributorID, Convert.ToInt32(nextBranchID));
                    distributorID = Convert.ToInt32(nextBranchID);
                }
                else
                {
                    DDA.DataObjects.AppData.DistributorID = Convert.ToInt32(nextBranchID);
                    distributorID = Convert.ToInt32(nextBranchID);

                    if (MessageBox.Show("Will this be a Selling Location?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // add itself as a selling location
                        Distributor_da.AddBranch(DDA.DataObjects.AppData.DistributorID, DDA.DataObjects.AppData.DistributorID);

                    }

                }

            }
            else
            {
                Distributor_da.UpdateBranch(distributorID, "", txtName.Text, txtBillingAddress.Text, txtShippingAddress.Text,
                                            txtCity.Text, txtCountry.Text, txtFax.Text,
                                            txtPhone.Text, cmbState.Text, txtZip.Text, contacts, txtBillingCity.Text,
                                            cmbBillingState.Text, txtBillingZip.Text, txtCountry.Text, txtSAP.Text, txtNode.Text, IsPartsOnly, isManufacturerRep, isMainDist);

                if (isMainDist == 1)
                {
                    // update all distributor branches for the manufacturerrep flag

                }

                // this should get the "current" distributor's ID that was used to update this record.
                nextBranchID = distributorID.ToString();
            }

            //string nextBranchID;
            //distributorID;

            DDA.DataAccess.Distributor_da.ClearEmailAddresses(Convert.ToInt32(nextBranchID));

            string sEmail;
            string sName;

            string[] sArr;
            for (i = 0; i < lstEmail.Items.Count; i++)
            {
                sEmail = lstEmail.Items[i].ToString();
                sEmail = sEmail.Replace(" - ", "^");
                sArr = sEmail.Split('^');

                sEmail = sArr[1];
                sName = sArr[0];

                DDA.DataAccess.Distributor_da.AddEmailAddress(Convert.ToInt32(nextBranchID), sEmail, sName);
            }

            // clear all contacts from the database
            DDA.DataAccess.Distributor_da.ClearContacts(Convert.ToInt32(nextBranchID));

            for (i = 0; i < lstContacts.Items.Count; i++)
            {
                //        private string primaryName;
                //        private string primaryTitle;
                contacts = lstContacts.Items[i].ToString();

                contactName = contacts.Substring(0, contacts.IndexOf(","));
                contactTitle = contacts.Substring(contacts.IndexOf(",") + 2);
                isPrimary = 0;

                if (contactName == primaryName && contactTitle == primaryTitle)
                {
                    isPrimary = 1;
                }

                DDA.DataAccess.Distributor_da.AddContact(Convert.ToInt32(nextBranchID), contactName, contactTitle, isPrimary);

            }



            MessageBox.Show("Data Saved");

            Distributor.ManufacturerRep = isManufacturerRep;
            DDA.DataObjects.AppData.DistributorMode = "Edit";
            DDA.DataObjects.AppData.BranchID = Convert.ToInt32(nextBranchID);

            //frmDistributorBranchList frmDBL = new frmDistributorBranchList();
            //frmDBL.Show();
            //this.Close();
        }

        private void btnBranch_Click(object sender, EventArgs e)
        {
            AppData.PreviousForm = "frmDistributorInformation";
            AppData.DistributorBranchListContractMode = false;

            frmDistributorBranchList frmDBL = new frmDistributorBranchList();
            frmDBL.Show();
            this.Close();

        }

        private void btnTerritoryReps_Click(object sender, EventArgs e)
        {
            frmTerritoryRepList frmTRL;

            frmTRL = new frmTerritoryRepList();

            frmTRL.Show();
            this.Close();
        }

        private void btnSalesReps_Click(object sender, EventArgs e)
        {


            frmServiceRepList frmSRL = new frmServiceRepList();
            frmSRL.Show();
            this.Close();
        }

        private void btnTerritory_Click(object sender, EventArgs e)
        {

            DDA.DataObjects.AppData.CurrentContract.ClearContractData();
            DDA.DataObjects.AppData.IsManufacturerRep = Distributor.ManufacturerRep;

            frmTerritory frmTerritory = new frmTerritory();
            frmTerritory.Show();
            this.Close();

        }

        private void frmDistributorInformation_Load(object sender, EventArgs e)
        {

        }


        private void btnRemoveContact_Click(object sender, EventArgs e)
        {
            if (btnAddContact.Text != "UPDATE")
            {
                lstContacts.Items.RemoveAt(lstContacts.SelectedIndex);
            }
            else
            {
                MessageBox.Show("You cannot remove a contact while in update mode");
            }
        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            if (txtContact.Text != "" && txtContactTitle.Text != "")
            {
                if (btnAddContact.Text == "ADD")
                {
                    lstContacts.Items.Add(txtContact.Text + ", " + txtContactTitle.Text);
                }
                else
                {
                    // Update the list
                    lstContacts.Items[contactIndex] = (txtContact.Text + ", " + txtContactTitle.Text);

                    btnAddContact.Text = "ADD";
                }

                if (chkPrimary.CheckState == CheckState.Checked)
                {
                    primaryName = txtContact.Text;
                    primaryTitle = txtContactTitle.Text;
                }

                txtContact.Text = "";
                txtContactTitle.Text = "";
                chkPrimary.CheckState = CheckState.Unchecked;
                txtContactTitle.BackColor = System.Drawing.SystemColors.Window;
                txtContact.BackColor = System.Drawing.SystemColors.Window;
            }
            else
            {
                MessageBox.Show("Please enter a contact name and title before proceeding.");
            }
        }

        private void lstContacts_DoubleClick(object sender, EventArgs e)
        {
            txtContact.BackColor = System.Drawing.Color.Yellow;
            txtContactTitle.BackColor = System.Drawing.Color.Yellow;

            string sItem;

            try
            {
                sItem = lstContacts.SelectedItem.ToString();
                txtContact.BackColor = System.Drawing.Color.Yellow;
                txtContactTitle.BackColor = System.Drawing.Color.Yellow;
                btnAddContact.Text = "UPDATE";
            }
            catch
            {

                txtContact.BackColor = System.Drawing.SystemColors.Window;
                txtContactTitle.BackColor = System.Drawing.SystemColors.Window;
                return;
            }


            contactIndex = Convert.ToInt32(lstContacts.SelectedIndex);

            try
            {
                txtContact.Text = sItem.Substring(0, sItem.IndexOf(","));
                txtContactTitle.Text = sItem.Substring(sItem.IndexOf(",") + 2, sItem.Length - sItem.IndexOf(",") - 2);
            }
            catch
            {
                txtContact.Text = sItem;
            }

            int isPrimary;
            isPrimary = DDA.DataAccess.Distributor_da.ContactPrimary(distributorID, txtContact.Text);

            if (isPrimary == 1)
            {
                chkPrimary.CheckState = CheckState.Checked;
            }
            else if (isPrimary == 0)
            {
                chkPrimary.CheckState = CheckState.Unchecked;
            }
            else if (isPrimary == -1)
            {
                //unsaved person was double clicked on.  see if they are saved as the primary
                if (txtContact.Text == primaryName && txtContactTitle.Text == primaryTitle)
                {
                    chkPrimary.CheckState = CheckState.Checked;
                }
                else
                {
                    chkPrimary.CheckState = CheckState.Unchecked;
                }
            }

        }

        private void btnCopyBillingInformation_Click(object sender, EventArgs e)
        {
            txtShippingAddress.Text = txtBillingAddress.Text;
            txtCity.Text = txtBillingCity.Text;
            cmbState.Text = cmbBillingState.Text;
            txtZip.Text = txtBillingZip.Text;
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            frmDistributorList frmDistList = new frmDistributorList();

            frmDistList.Show();
            this.Close();
        }

        private void lstContacts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddEmail_Click(object sender, EventArgs e)
        {
            // save routine
            if (txtEmail.Text != "" && txtEmailName.Text != "")
            {
                string sEmail;

                sEmail = txtEmailName.Text + " - " + txtEmail.Text;

                if (btnAddEmail.Text == "UPDATE")
                {
                    // Updated
                    if (emailIndex > -1)
                    {

                        lstEmail.Items[emailIndex] = sEmail;

                        // update email address?
                    }
                }
                else
                {
                    // Add
                    lstEmail.Items.Add(sEmail);
                }

                btnAddEmail.Text = "ADD";
                txtEmail.BackColor = System.Drawing.SystemColors.Window;
                txtEmail.Text = "";
                txtEmailName.Text = "";
                txtEmailName.BackColor = System.Drawing.SystemColors.Window;

                emailIndex = -1;  // reset email index
            }
            else
            {
                MessageBox.Show("Please enter a name and email before proceeding");
            }


        }

        private void lstEmail_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstEmail_DoubleClick(object sender, System.EventArgs e)
        {
            string sEmail, sName;
            string[] sArr;

            try
            {

                sEmail = lstEmail.SelectedItem.ToString();
                sEmail = sEmail.Replace(" - ", "^");
                sArr = sEmail.Split('^');

                sEmail = sArr[1];
                sName = sArr[0];


                txtEmail.Text = sEmail;
                txtEmailName.Text = sName;
                txtEmail.BackColor = System.Drawing.Color.Yellow;
                txtEmailName.BackColor = System.Drawing.Color.Yellow;

                emailIndex = lstEmail.SelectedIndex;
                btnAddEmail.Text = "UPDATE";
            }
            catch
            {
                btnAddEmail.Text = "ADD";
                txtEmail.Text = "";
                txtEmailName.Text = "";

                txtEmail.BackColor = System.Drawing.SystemColors.Window;
                txtEmailName.BackColor = System.Drawing.SystemColors.Window;

                MessageBox.Show("An error occurred while trying to load the email for editing.  Please take a screenshot or make a record of what data you were entering and contact the developer.");

            }

        }

        private void btnRemoveEmail_Click(object sender, EventArgs e)
        {

            if (btnAddEmail.Text != "UPDATE")
            {
                int index;

                index = lstEmail.SelectedIndex;
                if (index > -1)
                {
                    lstEmail.Items.RemoveAt(index);
                }
            }
            else
            {
                MessageBox.Show("You cannot remove an email while in update mode");
            }
        }

        private void cmbBillingState_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void cmbBillingState_TextChanged(object sender, System.EventArgs e)
        {

        }

    }
}