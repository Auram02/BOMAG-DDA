using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DDA.DataObjects
{
    class MapReport
    {
        public static string GetMapReport()
        {
            DataSet ds = DDA.DataAccess.MapReport_da.GetOutdatedMapReport();

            Dictionary<string, List<string>> catStates = new Dictionary<string, List<string>>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string stateName = dr["FullName"].ToString();
                string categoryName = dr["CategoryName"].ToString();

                if (catStates.ContainsKey(categoryName))
                {
                    catStates[categoryName].Add(stateName);
                }
                else
                {
                    List<string> states = new List<string>();
                    states.Add(stateName);

                    catStates.Add(categoryName, states);
                }

            }


            string message = "";

            foreach (string categoryName in catStates.Keys)
            {
                if (message.Length > 0)
                    message += Environment.NewLine + Environment.NewLine;

                message += categoryName + " for ";

                string stateMessage = "";
                foreach (string stateName in catStates[categoryName])
                {
                    if (stateMessage.Length > 0)
                        stateMessage += ", ";

                    stateMessage += stateName;
                }

                message += stateMessage;
            }

            return message;
        }

        public static void UpdateMapReportStatus()
        {
            List<int> modifiedStateList = new List<int>();
            foreach (int countyID in DDA.DataObjects.AppData.CurrentContract.ModifiedCounties)
            {
                int stateID = DDA.DataAccess.Location_da.GetStateID(DDA.DataAccess.Location_da.GetStateFullName(countyID));

                if (modifiedStateList.Contains(stateID) == false)
                    modifiedStateList.Add(stateID);
            }

            List<int> stateList = new List<int>();
            foreach (int countyID in DDA.DataObjects.AppData.CurrentContract.Counties)
            {
                if (countyID > -1)
                {
                    int stateID = DDA.DataAccess.Location_da.GetStateID(DDA.DataAccess.Location_da.GetStateFullName(countyID));

                    if (stateList.Contains(stateID) == false)
                        stateList.Add(stateID);
                }
            }

            foreach (int categoryID in DDA.DataObjects.AppData.CurrentContract.ModifiedCategories)
            {
                foreach (int stateID in stateList)
                {
                    DDA.DataAccess.MapReport_da.UpdateMapReport(stateID, categoryID, false);
                }

            }

            foreach (int stateID in modifiedStateList)
            {
                foreach (int categoryID in DDA.DataObjects.AppData.CurrentContract.Categories)
                {
                    DDA.DataAccess.MapReport_da.UpdateMapReport(stateID, categoryID, false);
                }
            }
        }
    }
}
