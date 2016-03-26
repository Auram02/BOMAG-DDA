using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DDA.Interfaces
{
    public partial class frmUserManagement : Form
    {
        public frmUserManagement()
        {
            InitializeComponent();

            ShowUsers();
        }

        private void ShowUsers()
        {
            DataSet ds;

            ds = DDA.DataAccess.User_da.GetUsers();

            int i;

            //for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    // replace all passwords chars with *'s
            //    string stars;
            //    stars = "";
            //    string password;

            //    password = ds.Tables[0].Rows[i]["Password"].ToString();

            //    for (int j = 0; j < password.Length; j++)
            //    {
            //        stars = stars + "*";
            //    }

            //    ds.Tables[0].Rows[i]["Password"] = stars;

            //}

            dgUsers.DataSource = ds.Tables[0];
            dgUsers.Columns[0].Visible = false; // hide the id column
            dgUsers.Columns["Password"].Visible = false; // hide the password column;
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            this.Close();

            DDA.DataObjects.AppData.activeMain.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "ADD")
            {

                int checkState;

                if (chkAdministrator.CheckState == CheckState.Checked)
                    checkState = 1;
                else
                    checkState = 0;

                DDA.DataAccess.User_da.AddUser(txtLogin.Text, txtPassword.Text, checkState);
            }
            else
            {
                int id;

                int checkState;

                if (chkAdministrator.CheckState == CheckState.Checked)
                    checkState = 1;
                else
                    checkState = 0;

                id = Convert.ToInt32(dgUsers.CurrentRow.Cells["userID"].Value);
                DDA.DataAccess.User_da.UpdateUser(id, txtLogin.Text, txtPassword.Text, checkState);

            }

            btnSave.Text = "ADD";
            txtLogin.Text = "";
            txtPassword.Text = "";

            chkAdministrator.Checked = false;

            txtLogin.BackColor = System.Drawing.SystemColors.Window;
            txtPassword.BackColor = System.Drawing.SystemColors.Window;

            // refresh the datagrid
            ShowUsers();

            MessageBox.Show("Data Saved");
        }
        

        void dgUsers_DoubleClick(object sender, System.EventArgs e)
        {
            int rowIndex;
            rowIndex = dgUsers.CurrentRow.Index;



            txtLogin.Text = dgUsers.Rows[rowIndex].Cells["Login"].Value.ToString();
            txtPassword.Text = dgUsers.Rows[rowIndex].Cells["Password"].Value.ToString();

            if (Convert.ToInt32(dgUsers.Rows[rowIndex].Cells["Administrator"].Value) == 0)
            {
                chkAdministrator.CheckState = CheckState.Unchecked;
            }
            else
            {
                chkAdministrator.CheckState = CheckState.Checked;
            }

            txtLogin.BackColor = System.Drawing.Color.Yellow;
            txtPassword.BackColor = System.Drawing.Color.Yellow;

            btnSave.Text = "UPDATE";

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you wish to continue removing this user?", "Confirm Removal", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int id;
                id = Convert.ToInt32(dgUsers.CurrentRow.Cells["UserID"].Value.ToString());

                dgUsers.Rows.RemoveAt(dgUsers.CurrentRow.Index);
                DDA.DataAccess.User_da.RemoveUser(id);

                MessageBox.Show("User Removed");

            } else {
                MessageBox.Show("Remove Cancelled");
            }
        }

    }
}