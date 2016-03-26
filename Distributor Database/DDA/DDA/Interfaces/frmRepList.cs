using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DDA.Interfaces
{
    public partial class frmRepList : Form
    {
        public frmRepList()
        {
            InitializeComponent();
        }
        public frmRepList(string sType, int id)
        {
            InitializeComponent();

            label1.Text = sType + " REPRESENTATIVE LIST";

            DataSet ds;

            ds = DDA.DataAccess.Representative_da.GetRepresentativeList(sType);

            int i;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // don't want to list the guy we're removing!
                if (ds.Tables[0].Rows[i]["RepID"].ToString() != Convert.ToString(id))
                {
                    lstRep.Items.Add(ds.Tables[0].Rows[i]["REP NAME"].ToString());
                }
            }
        }

        private void btnSelectRep_Click(object sender, EventArgs e)
        {
            DDA.DataObjects.AppData.NewRepName = lstRep.SelectedItem.ToString();
            this.Close();
        }
    }
}