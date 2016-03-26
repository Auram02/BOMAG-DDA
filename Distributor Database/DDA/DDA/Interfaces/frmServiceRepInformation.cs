using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DDA.DataObjects;

using DDA.DataAccess;


namespace DDA.Interfaces
{
    public partial class frmServiceRepInformation : Form
    {
        int repID;
        int distributorID;
        string formMode;

        public frmServiceRepInformation()
        {
            InitializeComponent();
            DDA.DataObjects.AppData.DisableCloseButton(Handle);

            distributorID = DDA.DataObjects.AppData.BranchID;


            Location_da.PopulateStateDropdown(ref txtState);

            if (DDA.DataObjects.AppData.RepMode == "Add")
            {

                formMode = "Add";

            }
            else
            {
                repID = DDA.DataObjects.AppData.RepID;
                formMode = "Edit";

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

            frmServiceRepList frmSRL = new frmServiceRepList();
            frmSRL.Show();
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (formMode == "Add")
            {
                Representative_da.AddRepresentative(distributorID, txtName.Text, txtAddress.Text, txtCity.Text,
                                                    txtCountry.Text, txtEmail.Text, txtFax.Text, txtPhone.Text,
                                                    txtState.Text, txtZip.Text, "Service", txtMobilePhone.Text);
            }
            else
            {
                Representative_da.UpdateRepresentative(repID, distributorID, txtName.Text, txtAddress.Text,
                                                        txtCity.Text, txtCountry.Text, txtEmail.Text, txtFax.Text,
                                                        txtPhone.Text, txtState.Text, txtZip.Text, "Service", txtMobilePhone.Text);
            }

            MessageBox.Show("Data Saved");

            frmServiceRepList frmSRL = new frmServiceRepList();
            frmSRL.Show();
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