using System;
using System.Collections.Generic;
using System.Text;
using DataLogic.DBA;
using System.Data;

namespace DDA.DataAccess
{
    class MapReport_da
    {

        /// <summary>
        /// Gets Outdated Map Categories, Counties, and States.  LIGHT EQUIPMENT is explicitly excluded from this list
        /// </summary>
        /// <returns></returns>
        public static DataSet GetOutdatedMapReport()
        {
            string sql;
            
            sql = "SELECT * FROM MapReport WHERE IsMapCurrent = 0";

            sql = "SELECT MapReportID, MapReport.StateID, MapReport.CategoryID, TimeStamp, IsMapCurrent, FullName, CategoryName " +
                    " FROM MapReport, State, Category " +
                    " WHERE IsMapCurrent = 0 " +
                    " AND State.StateID = MapReport.StateID " +
                    " AND Category.CategoryID = MapReport.CategoryID" +
                    " AND Category.CategoryID <> 7 " +  // Exclude LIGHT EQUIPMENT
                    " ORDER BY CategoryName, FullName";

            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }

        public static void UpdateMapReport(int stateID, int categoryID, bool isMapCurrent = false, bool updateTimeStamp = false)
        {
            string sql;

            int setMapCurrent = isMapCurrent ? 1 : 0;

            sql = "SELECT * FROM MapReport WHERE StateID = " + stateID + " AND CategoryID = " + categoryID;
            DataSet ds = DataLogic.DBA.DataLogic.Read(sql);

            if (ds.Tables[0].Rows.Count == 0)
            {
                sql = "INSERT INTO MapReport (StateID, CategoryID, IsMapCurrent) VALUES (" + stateID + ", " + categoryID + ", " + setMapCurrent.ToString() + ")";
                DataLogic.DBA.DataLogic.Update(sql);
            }
            else
            {

                sql = "UPDATE MapReport SET StateID = " + stateID + ", CategoryID =" + categoryID + ", IsMapCurrent = " + setMapCurrent.ToString();

                if (updateTimeStamp)
                    sql += ", [TimeStamp] = '" + DateTime.Now.ToString() + "'";

                sql += " WHERE StateID = " + stateID + " AND CategoryID = " + categoryID;
                DataLogic.DBA.DataLogic.Update(sql);
            }
        }

        public static void InsertMapReport(int categoryID, bool isMapCurrent = false)
        {
            string sql;

            int setMapCurrent = isMapCurrent ? 1 : 0;

            DataSet ds = DDA.DataAccess.Location_da.GetStateList();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int stateID = Convert.ToInt32(dr["StateID"].ToString());

                sql = "INSERT INTO MapReport (StateID, CategoryID, IsMapCurrent) VALUES (" + stateID + ", " + categoryID + ", " + setMapCurrent.ToString() + ")";
                DataLogic.DBA.DataLogic.Update(sql);
            }

        }


        public static void DeleteMapReport(int categoryID)
        {
            string sql;

            sql = "DELETE FROM MapReport WHERE CategoryID = " + categoryID;
            DataLogic.DBA.DataLogic.Update(sql);

        }
    }
}
