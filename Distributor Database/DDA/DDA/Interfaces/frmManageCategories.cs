using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DDA.Interfaces
{
    public partial class frmManageCategories : Form
    {
        int curIndex;
        string oldName;

        public frmManageCategories()
        {
            InitializeComponent();
            DDA.DataObjects.AppData.DisableCloseButton(Handle);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmTerritory frmTerr = new frmTerritory();
            frmTerr.Show();
            this.Close();
            
        }

        private void frmManageCategories_Load(object sender, EventArgs e)
        {
            DataSet ds;
            ds = DDA.DataAccess.Category_da.GetCategoryList();

            int i;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
               lstCategory.Items.Add(ds.Tables[0].Rows[i]["CategoryName"]);
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string catName;
            if (btnSave.Text != "UPDATE")
            {
                try
                {

                    catName = lstCategory.SelectedItem.ToString();

                    int catID;

                    // stupid
                    DataLogic.DBA.DataLogic.PrepareSQL(ref catName);

                    catID = DDA.DataAccess.Category_da.GetCategoryID(catName);

                    if (MessageBox.Show("You are about to remove the category '" + catName + "'.  Are you sure you want to continue?", "Confirm Remove", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DDA.DataAccess.Category_da.RemoveCategory(catID);
                        lstCategory.Items.RemoveAt(lstCategory.SelectedIndex);

                        DDA.DataAccess.MapReport_da.DeleteMapReport(catID);

                        curIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show("Remove Cancelled.");
                    }
                }
                catch
                {

                }
            }
            else
            {
                MessageBox.Show("You cannot remove a category while you are in edit mode");
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {

                string catName;
                catName = txtName.Text;

                int allowTerritoryOverlap = 0;
                int allowCountyOverlap = 0;

                if (chkAllowTerritoryOverlap.Checked)
                {
                    allowTerritoryOverlap = 1;
                }

                if (chkAllowCountyOverlap.Checked)
                    allowCountyOverlap = 1;
                
                if (btnSave.Text == "UPDATE")
                {
                    // update
                    int oldID;

                    oldID = DDA.DataAccess.Category_da.GetCategoryID(oldName);


                    DDA.DataAccess.Category_da.UpdateCategory(oldID, catName, allowTerritoryOverlap, allowCountyOverlap);
                    lstCategory.Items[curIndex] = txtName.Text;
                    

                }
                else
                {
                    int catID;
                    catID = DataLogic.DBA.DataLogic.GetNextID("Category", "CategoryID");

                    DDA.DataAccess.Category_da.AddCategory(catID, catName, allowTerritoryOverlap, allowCountyOverlap);
                    DDA.DataAccess.MapReport_da.InsertMapReport(catID, false);


                    lstCategory.Items.Add(catName);

                }


                MessageBox.Show("Data Saved");
            }
            else
            {
                MessageBox.Show("Please enter a category name before proceeding.");
            }

            curIndex = -1;
            txtName.Text = "";
            txtName.BackColor = System.Drawing.SystemColors.Window;
            btnSave.Text = "ADD";
            chkAllowTerritoryOverlap.Checked = false;
            chkAllowCountyOverlap.Checked = false;
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            frmDistributorList frmDistList = new frmDistributorList();

            frmDistList.Show();
            this.Close();
        }

        private void lstCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstCategory_DoubleClick(object sender, EventArgs e)
        {
            
            //int allowTerritoryOverlap = Category

            txtName.Text = lstCategory.SelectedItem.ToString();
            txtName.BackColor = System.Drawing.Color.Yellow;

            int categoryId = DDA.DataAccess.Category_da.GetCategoryID(txtName.Text);
            int allowTerritoryOverlap = DDA.DataAccess.Category_da.GetCategoryAllowTerritoryOverlap(categoryId);
            int allowCountyOverlap = DDA.DataAccess.Category_da.GetCategoryAllowCountyOverlap(categoryId);
                       
            if (allowTerritoryOverlap == 0)
                chkAllowTerritoryOverlap.Checked = false;
            else
                chkAllowTerritoryOverlap.Checked = true;
            
            if (allowCountyOverlap == 0)
                chkAllowCountyOverlap.Checked = false;
            else
                chkAllowCountyOverlap.Checked = true;
            

            btnSave.Text = "UPDATE";
            curIndex = lstCategory.SelectedIndex;
            oldName = txtName.Text;

        }

        private void chkAllowTerritoryOverlap_CheckedChanged(object sender, EventArgs e)
        {

            if (chkAllowTerritoryOverlap.Checked && chkAllowCountyOverlap.Checked)
            {
                chkAllowTerritoryOverlap.Checked = false;
                MessageBox.Show("A category can only be Territory Overlap or County overlap, but not both");
            }
            else
            {

            }

        }

        private void chkAllowCountyOverlap_CheckedChanged(object sender, EventArgs e)
        {

            if (chkAllowTerritoryOverlap.Checked && chkAllowCountyOverlap.Checked)
            {
                chkAllowCountyOverlap.Checked = false;
                MessageBox.Show("A category can only be Territory Overlap or County overlap, but not both");
            }
            else
            {
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (MoveItem(-1))
            {
                int categoryId1 = DDA.DataAccess.Category_da.GetCategoryID(lstCategory.SelectedItem.ToString());
                int categoryId2 = DDA.DataAccess.Category_da.GetCategoryID(lstCategory.Items[lstCategory.SelectedIndex + 1].ToString());
                DDA.DataAccess.Category_da.UpdateOrdinal(categoryId1, categoryId2, lstCategory.SelectedIndex + 1, lstCategory.SelectedIndex + 2);
            }

        }
        
        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (MoveItem(1))
            {
                int categoryId1 = DDA.DataAccess.Category_da.GetCategoryID(lstCategory.SelectedItem.ToString());
                int categoryId2 = DDA.DataAccess.Category_da.GetCategoryID(lstCategory.Items[lstCategory.SelectedIndex - 1].ToString());
                DDA.DataAccess.Category_da.UpdateOrdinal(categoryId1, categoryId2, lstCategory.SelectedIndex + 1, lstCategory.SelectedIndex  );
            }
        }

        public bool MoveItem(int direction)
        {
            // Checking selected item
            if (lstCategory.SelectedItem == null || lstCategory.SelectedIndex < 0)
                return false; // No selected item - nothing to do

            // Calculate new index using move direction
            int newIndex = lstCategory.SelectedIndex + direction;

            // Checking bounds of the range
            if (newIndex < 0 || newIndex >= lstCategory.Items.Count)
                return false; // Index out of range - nothing to do

            object selected = lstCategory.SelectedItem;

            // Removing removable element
            lstCategory.Items.Remove(selected);
            // Insert it in new position
            lstCategory.Items.Insert(newIndex, selected);
            // Restore selection
            lstCategory.SetSelected(newIndex, true);

            return true;
        }

    }
}