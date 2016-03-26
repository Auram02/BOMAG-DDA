using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace DDA.Interfaces
{
    public partial class frmLogin : Form
    {

        public frmLogin()
        {
            InitializeComponent();
            DDA.DataObjects.AppData.ExitApplication = false;

            DDA.DataObjects.AppData.DisableCloseButton(Handle);
        }



        private void button1_Click(object sender, EventArgs e)
        {

            DDA.BusinessLogic.InitializeProgram.InitializeGlobals();
            
            bool isAdmin;
            isAdmin = false;

            bool result;
            this.Close();
            result = DDA.DataAccess.User_da.DoLogin(txtLogin.Text, txtPassword.Text, ref isAdmin);

            if (result == true)
            {
                DDA.DataObjects.AppData.IsLoggedIn = true;
                
                // If they are an administrator, backup the database.
                if (isAdmin == true)
                {

                    try
                    {
                        string dbLoc;
                        dbLoc = DataLogic.DataAccessVariables.database_location;
                        // backup database on successful login
                        //System.IO.File.Copy(Application.StartupPath + "\\Data\\DDA.mdb", Application.StartupPath + "\\Data\\_DDA.mdb", true);
                        //System.IO.File.Copy(Application.StartupPath + "\\Data\\DDA.mdb", Application.StartupPath + "\\Data\\_DDA.mdb", true);
                        System.IO.FileInfo db = new System.IO.FileInfo(dbLoc);

                        db.CopyTo(dbLoc + ".bak", true);

                    }
                    catch
                    {
                        MessageBox.Show("An error has occurred while backing up DDA.mdb.  The original DDA.mdb file was not affected in any way.  Please contact the developer if this error continues to occur");
                    }
                }

                this.Close();

            }
            else
            {
                DDA.DataObjects.AppData.IsLoggedIn = false;
                MessageBox.Show("Invalid Login or Password.  Please try to Login again.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DDA.DataObjects.AppData.ExitApplication = true;
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {


            DataSet dsDist;
            DataSet dsContacts;

            dsDist = DDA.DataAccess.Distributor_da.GetDistributorListContacts();

            for (int i = 0; i < dsDist.Tables[0].Rows.Count; i++)
            {
                string contacts = dsDist.Tables[0].Rows[i]["contacts"].ToString();

                ArrayList contactArr = new ArrayList();
                string[] conArr;

                conArr = contacts.Split('^');
                //contactArr = contacts.Split('^');

                string conName, conTitle;

                foreach (string sArr in conArr)
                {
                    if (sArr != "")
                    {
                        conName = sArr.Substring(0, sArr.IndexOf(","));
                        conTitle = sArr.Substring(sArr.IndexOf(",") + 2);

                        DDA.DataAccess.Distributor_da.AddContact(Convert.ToInt32(dsDist.Tables[0].Rows[i][0].ToString()), conName, conTitle, 0);
                    }
                }
            }


        }

        private void lnkSetDatabaseLocation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDatabaseLocation fdl = new frmDatabaseLocation();
            this.Hide();
            fdl.ShowDialog();
            this.Show();

            DDA.BusinessLogic.InitializeProgram.InitializeGlobals();
        }

    }
}