using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DDA.BusinessLogic;
using System.Runtime.InteropServices;
using System.Reflection;

namespace DDA
{
    public partial class frmMain : Form
    {
        public int test;

        public frmMain()
        {

           
            InitializeComponent();

//            InitializeProgram.InitializeGlobals();

            DDA.Interfaces.frmSplash frmSplash = new DDA.Interfaces.frmSplash();
            frmSplash.ShowDialog();

            while (DDA.DataObjects.AppData.IsLoggedIn == false && DDA.DataObjects.AppData.ExitApplication == false)
            {
                DDA.Interfaces.frmLogin frmLogin = new DDA.Interfaces.frmLogin();
                frmLogin.ShowDialog();
            }

            if (DDA.DataObjects.AppData.ExitApplication == true)
            {
                Application.Exit();
                return;

            }


            DDA.DataObjects.AppData.activeMain = this;

            if (DDA.DataObjects.AppData.UserIsAdmin == false)
            {
                btnAdministerUsers.Enabled = false;
                btnDistributorDatabase.Enabled = false;
            }
            if (DDA.DataObjects.AppData.UserName == "nwilson")
                btnNeilAdmin.Visible = true;
            else
                btnNeilAdmin.Visible = false;


            lblVersionNumber.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();


        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (DDA.DataObjects.AppData.ExitApplication == true)
            {
                base.Dispose();
            }

        }



        private void btnEditCategories_Click(object sender, EventArgs e)
        {
        }

        private void btnDistributorDatabase_Click(object sender, EventArgs e)
        {
            DDA.Interfaces.frmDistributorList frmDL = new DDA.Interfaces.frmDistributorList();
            frmDL.Show();
            this.Hide();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            DDA.Interfaces.frmReports fr = new DDA.Interfaces.frmReports();
            fr.Show();
            this.Hide();
        }

        private void btnAdministerUsers_Click(object sender, EventArgs e)
        {
            DDA.Interfaces.frmUserManagement frmMU = new DDA.Interfaces.frmUserManagement();
            frmMU.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Dispose();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
//            MessageBox.Show("This feature has not been implemented yet.");

            DDA.Interfaces.frmAbout frma = new DDA.Interfaces.frmAbout();
            frma.ShowDialog();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DDA.Interfaces.frmManageCountyList fmcl = new DDA.Interfaces.frmManageCountyList();
            this.Hide();
            fmcl.ShowDialog();
            this.Show();

        }

        private void btnNeilAdmin_Click(object sender, EventArgs e)
        {
            DDA.Interfaces.frmNeilAdmin frmNeilAdmin = new DDA.Interfaces.frmNeilAdmin();
            this.Hide();
            frmNeilAdmin.ShowDialog();
            this.Show();
        }





    }
}