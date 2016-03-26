using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Data;
using DDA.DataObjects;

using DDA.DataAccess;

namespace DDA.Interfaces
{
    public partial class frmTerritoryRepInformation : Form
    {

        private int repID;
        private int distributorID;
        private string formMode;

        public frmTerritoryRepInformation()
        {
            InitializeComponent();
            DDA.DataObjects.AppData.DisableCloseButton(Handle);

            // Load states
            Location_da.PopulateStateDropdown(ref txtState);
            
            distributorID = DDA.DataObjects.AppData.BranchID;
            
            if (DDA.DataObjects.AppData.RepMode == "Add")
            {
                formMode = "Add";
            }
            else
            {

                formMode = "Edit";
                repID = DDA.DataObjects.AppData.RepID;

                Representative objRep = new Representative(repID);

                txtName.Text = objRep.Name;
                txtAddress.Text = objRep.Address;
                txtCity.Text = objRep.City;
                txtCountry.Text = objRep.Country;
                txtEmail.Text = objRep.Email;
                txtFax.Text = objRep.Fax;
                txtPhone.Text = objRep.Phone;
                txtState.Text = objRep.State;
                txtZip.Text = objRep.Zip.ToString();
                txtMobilePhone.Text = objRep.MobilePhone;
            
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            frmTerritoryRepList frmTRL = new frmTerritoryRepList();
            frmTRL.Show();
            this.Close();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (formMode == "Add")
            {
                Representative_da.AddRepresentative(distributorID, txtName.Text, txtAddress.Text, txtCity.Text, 
                                                    txtCountry.Text, txtEmail.Text, txtFax.Text, txtPhone.Text, 
                                                    txtState.Text, txtZip.Text, "Territory", txtMobilePhone.Text);
            }
            else
            {
                Representative_da.UpdateRepresentative(repID, distributorID, txtName.Text, txtAddress.Text, 
                                                        txtCity.Text, txtCountry.Text, txtEmail.Text, txtFax.Text,
                                                        txtPhone.Text, txtState.Text, txtZip.Text, "Territory", txtMobilePhone.Text);
            }

            MessageBox.Show("Data Saved");

            frmTerritoryRepList frmTRL = new frmTerritoryRepList();
            frmTRL.Show();
            this.Close();
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            frmDistributorList frmDistList = new frmDistributorList();

            frmDistList.Show();
            this.Close();
        }

    }
}