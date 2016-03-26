using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DDA.Interfaces
{
    public partial class frmDatabaseLocation : Form
    {
        XmlConfig.Config xcfg = new XmlConfig.Config();
        
        public frmDatabaseLocation()
        {
            InitializeComponent();

            DDA.DataObjects.AppData.DisableCloseButton(Handle);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ofd1.Filter = "Access Database (*.mdb) | *.mdb";
            ofd1.ShowDialog();
            txtLocation.Text = ofd1.FileName;
            
        }

        private void btnUpdateFileLocation_Click(object sender, EventArgs e)
        {
            try
            {
                xcfg.SetValue("//Settings//DatabasePath", txtLocation.Text);
                MessageBox.Show("Location Updated Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error Has Occurred Saving the New Database Location: " + ex.Message);
            }

        }

        private void frmDatabaseLocation_Load(object sender, EventArgs e)
        {
            LoadXMLData();
        }

        private void LoadXMLData()
        {
            xcfg.cfgFile = "Settings.xml";
            txtLocation.Text = xcfg.GetValue("//Settings//DatabasePath");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}