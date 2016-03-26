using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DDA.Interfaces
{
    public partial class frmMapReport : Form
    {
        public frmMapReport()
        {
            InitializeComponent();
            DataSet ds;
            ds = DDA.DataAccess.Category_da.GetCategoryList(DDA.DataObjects.AppData.IsManufacturerRep);

            int i;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                chkCategories.Items.Add(ds.Tables[0].Rows[i]["CategoryName"]);
            }

            ds = DDA.DataAccess.Location_da.GetStateList();

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                chkStates.Items.Add(ds.Tables[0].Rows[i]["FullName"]);
            }

            txtMapReportStatus.Text = DDA.DataObjects.MapReport.GetMapReport();
        }

        private void btnCreateMaps_Click(object sender, EventArgs e)
        {
            List<string> stateList = new List<string>();
            com.findbomag.www.MapReportService svc = new com.findbomag.www.MapReportService();
            
            for (int i = 0; i < chkStates.CheckedItems.Count; i++)
            {
                stateList.Add(chkStates.CheckedItems[i].ToString());
            }

            for (int z = 0; z < chkCategories.CheckedItems.Count; z++)
            {
                string categoryName = chkCategories.CheckedItems[z].ToString();
                int categoryID = DDA.DataAccess.Category_da.GetCategoryID(categoryName);

                DataSet ds = new DataSet();
                ds = DDA.DataObjects.Reports.GenerateContractListByCategoryListStateList(categoryID, stateList);

                
                
                string reportID = svc.UploadReportData(ds, categoryName, stateList.ToArray());

                reportID = reportID.Substring(0, reportID.IndexOf(".json"));

                System.Diagnostics.Process.Start("http://www.findbomag.com/admin/Reports/MapReport.aspx?id=" + reportID);

                foreach (string stateName in stateList)
                {
                    int stateID = DDA.DataAccess.Location_da.GetStateID(stateName);
                    DDA.DataAccess.MapReport_da.UpdateMapReport(stateID, categoryID, true, true);
                }

            }

            txtMapReportStatus.Text = DDA.DataObjects.MapReport.GetMapReport();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkStates.Items.Count; i++)
                chkStates.SetItemChecked(i,true);
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkStates.Items.Count; i++)
                chkStates.SetItemChecked(i, false);
        }
    }
}
