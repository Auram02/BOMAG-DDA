using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace StateCountyImport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ofd1.InitialDirectory = "C:\\Caspar\\Development\\Bomag Americas\\Distributor Database\\StateCountyImport\\StateCountyImport\\bin\\Debug\\Data";
            
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            ofd1.ShowDialog();

            txtFilename.Text = ofd1.FileName;

            

        }

        private void btnGo_Click(object sender, EventArgs e)
        {

            string sa;
            DDA.StateCountyImport.DataLogic.SetupConnection();

            string sql;

            sa = ofd1.FileName;

            sa = sa.Substring(sa.Length - 6, 2);

            txtStateAbbreviation.Text = sa;

            int id;

            sql = "SELECT StateID FROM State WHERE Abbreviation = '" + txtStateAbbreviation.Text + "'";

            
            id = Convert.ToInt32(DDA.StateCountyImport.DataLogic.Read(sql).Tables[0].Rows[0][0]);

            txtStateID.Text = Convert.ToString(id);

            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtFilename.Text + ";Extended Properties=Excel 8.0");
            OleDbDataAdapter da = new OleDbDataAdapter("select * from [Sheet1$]", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            int i;
            for (i = 0; i < dt.Rows.Count; i++)
            {
                sql = "INSERT INTO County VALUES(" + txtNumOn.Text + ",'" + dt.Rows[i][0] + "'," + txtStateID.Text + ")";
                DDA.StateCountyImport.DataLogic.Update(sql);

                txtNumOn.Text = Convert.ToString(Convert.ToInt32(txtNumOn.Text) + 1);

            }

            dg1.DataSource = dt;
        }
    }
}