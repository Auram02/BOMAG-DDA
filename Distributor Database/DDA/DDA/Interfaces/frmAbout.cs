using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DDA.Interfaces
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
            DDA.DataObjects.AppData.DisableCloseButton(Handle);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {

            string aboutText = "";


            aboutText = "Developer: Neil Wilson - Task Software\nContact: nwilson@nwilson.org\n\nDescription: Distributor Database Application (DDA) was developed for Bomag Americas with the intent to provide an interface to manage all of the data that pertains to a business contract.  This data includes Distributors, Product Categories, Counties, Contacts, and more.  Additionally, multiple reports are featured in this application.";


            txtAbout1.Text = aboutText;
        }
    }
}