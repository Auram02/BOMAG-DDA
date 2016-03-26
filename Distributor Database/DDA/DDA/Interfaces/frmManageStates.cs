using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DDA.Interfaces
{
    public partial class frmManageStates : Form
    {

        int curStateID;


        public frmManageStates()
        {
            InitializeComponent();
            ListStates();
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

                if (btnUpdate.Text == "ADD")
                {
                    int stateID;

                    DDA.DataAccess.Location_da.AddState(txtStateName.Text, txtAbbreviation.Text);
                    lstCounties.Items.Add(txtStateName.Text);

                    lstCounties.Sorted = true;

                }
                else
                {

                    // curCountyID // for update
                    DDA.DataAccess.Location_da.UpdateState(curStateID, txtStateName.Text, txtAbbreviation.Text);
                    
                    
                }

                txtStateName.BackColor = System.Drawing.SystemColors.Window;
                txtAbbreviation.BackColor = System.Drawing.SystemColors.Window;

                btnUpdate.Text = "ADD";

                MessageBox.Show("Data Saved");
                txtStateName.Text = "";
                txtAbbreviation.Text = "";

                ListStates();

            }

        private void lstCounties_SelectedIndexChanged(object sender, EventArgs e)
        {


        
        }


        void lstCounties_DoubleClick(object sender, System.EventArgs e)
        {
            btnUpdate.Text = "UPDATE";
            txtStateName.Text = lstCounties.SelectedItem.ToString();
            txtStateName.BackColor = System.Drawing.Color.Yellow;

            curStateID = DDA.DataAccess.Location_da.GetStateID(txtStateName.Text);

            string abbr;
            abbr = DDA.DataAccess.Location_da.GetStateAbbreviation(curStateID);

            txtAbbreviation.Text = abbr;
            txtAbbreviation.BackColor = System.Drawing.Color.Yellow;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int curIndex;
            curIndex = lstCounties.SelectedIndex;

            if (btnUpdate.Text == "UPDATE")
            {
                MessageBox.Show("You cannot remove a state while in update mode");
            }
            else
            {
                int id;

                id = DDA.DataAccess.Location_da.GetStateID(lstCounties.SelectedItem.ToString());

                DataSet ds;
                ds = DDA.DataAccess.Location_da.GetCountyList(id);

                try
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        int contractCount;
                        contractCount = ds.Tables[0].Rows.Count;

                        if (MessageBox.Show("This state has counties currently associated with " + contractCount + " contract(s).  If you remove it, all contracts will no longer have it listed as a state nor any counties or contracts originally under it.  Are you sure you want to continue?", "Removal Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("This state has no counties associated with any contracts.  Are you sure you wish to continue removing it?", "Removal Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }

                    }

                    DDA.DataAccess.Location_da.RemoveState(id);
                    MessageBox.Show("State Removed Successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred removing the state.  Please contact the developer.  : " + ex.Message);
                }
                
            }
           
            lstCounties.Items.RemoveAt(curIndex);

            ListStates();
        }

        private void ListStates()
        {
            DataSet ds;
            ds = DDA.DataAccess.Location_da.GetStateList();

            lstCounties.Items.Clear();

            for (int i=0; i<ds.Tables[0].Rows.Count;i++)
                lstCounties.Items.Add(ds.Tables[0].Rows[i][1]);
        }


    }
}