using System;
using System.Collections.Generic;
using System.Text;
using DataLogic.DBA;
using System.Data;


namespace DDA.DataAccess
{
    class Category_da
    {

        public static int GetCategoryID(string p_Name)
        {
            string sql;
            int catID;

            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_Name);

            sql = "SELECT CategoryID FROM Category WHERE CategoryName = '" + p_Name + "'";

            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            catID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

            return catID;
        }

        public static bool DoLogin(string p_Name, string p_Password, ref bool isAdmin)
        {
            string sql;
            bool catID;

            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_Name);

            sql = "SELECT Count(userID) FROM [Users1] WHERE [Login1] = '" + p_Name + "' AND [Password1] = '" + p_Password + "'";

            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            catID = Convert.ToBoolean(ds.Tables[0].Rows[0][0]);

            return catID;
        }


        public static string GetCategoryName(int p_id)
        {
            string sql;
            string catName;

            sql = "SELECT CategoryName FROM Category WHERE CategoryID = " + p_id;

            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            catName = Convert.ToString(ds.Tables[0].Rows[0][0]);

            return catName;
        }

        public static int GetCategoryAllowTerritoryOverlap(int p_id)
        {
            string sql;
            int allowTerritoryOverlap;

            sql = "SELECT AllowTerritoryOverlap FROM Category WHERE CategoryID = " + p_id;

            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            if (ds.Tables[0].Rows[0][0].ToString() == "False")
            {
                allowTerritoryOverlap = 0;
            }
            else
            {
                allowTerritoryOverlap = 1;
            }

            return allowTerritoryOverlap;
        }

        public static int GetCategoryAllowTerritoryOverlap(string categoryName)
        {
            string sql;
            int allowTerritoryOverlap;

            sql = "SELECT AllowTerritoryOverlap FROM Category WHERE CategoryName = '" + categoryName + "'";

            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            if (ds.Tables[0].Rows[0][0].ToString() == "False")
            {
                allowTerritoryOverlap = 0;
            }
            else
            {
                allowTerritoryOverlap = 1;
            }

            return allowTerritoryOverlap;
        }

        public static int GetCategoryAllowCountyOverlap(int p_id)
        {
            string sql;
            int allowCountyOverlap;

            sql = "SELECT AllowCountyOverlap FROM Category WHERE CategoryID = " + p_id;

            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            if (ds.Tables[0].Rows[0][0].ToString() == "False")
            {
                allowCountyOverlap = 0;
            }
            else
            {
                allowCountyOverlap = 1;
            }

            return allowCountyOverlap;
        }

        public static int GetCategoryAllowCountyOverlap(string categoryName)
        {
            string sql;
            int allowCountyOverlap;

            sql = "SELECT AllowCountyOverlap FROM Category WHERE CategoryName = '" + categoryName + "'";

            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            if (ds.Tables[0].Rows[0][0].ToString() == "False")
            {
                allowCountyOverlap = 0;
            }
            else
            {
                allowCountyOverlap = 1;
            }

            return allowCountyOverlap;
        }
        
        public static DataSet GetCategoryListAttributes(string categories)
        {
            string sql;
            
            sql = "SELECT AllowCountyOverlap, AllowTerritoryOverlap FROM Category WHERE CategoryName IN (" + categories + ")";

            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }


        public static DataSet GetCategoryList(int allowTerritoryOverlap = -1)
        {
            DataSet ds;
            string sql;

            sql = "SELECT CategoryName, CategoryID FROM Category";
            
            if (allowTerritoryOverlap == 1) {
                sql += " WHERE AllowTerritoryOverlap = True";
            }

            sql += " ORDER BY Ordinal ASC";

            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }

        public static void RemoveCategory(int p_id)
        {
            string sql;

            sql = "DELETE FROM Category WHERE CategoryID = " + p_id;
            DataLogic.DBA.DataLogic.Update(sql);
            
            sql = "DELETE FROM ContractCategory WHERE fk_categoryID = " + p_id;
            DataLogic.DBA.DataLogic.Update(sql);

        }

        public static void AddCategory(int p_id, string p_name, int allowTerritoryOverlap, int allowCountyOverlap)
        {
            string sql;

            DataLogic.DBA.DataLogic.PrepareSQL(ref p_name);

            System.Data.OleDb.OleDbCommand cmd1 = new System.Data.OleDb.OleDbCommand();

            //cmd1.CommandText = "INSERT INTO Category VALUES(@P1, @P2)";

            sql = "INSERT INTO Category (CategoryID, CategoryName, AllowTerritoryOverlap, AllowCountyOverlap) VALUES(" + p_id + ",'" + p_name + "'," + allowTerritoryOverlap + "," + allowCountyOverlap + ")";
            DataLogic.DBA.DataLogic.Update(sql);

        }

        public static void UpdateCategory(int p_id, string p_name, int allowTerritoryOverlap, int allowCountyOverlap)
        {
            string sql;

            DataLogic.DBA.DataLogic.PrepareSQL(ref p_name);

            sql = "UPDATE Category SET CategoryName = '" + p_name + "', AllowTerritoryOverlap=" + allowTerritoryOverlap + ", AllowCountyOverlap = " + allowCountyOverlap + " WHERE CategoryID = " + p_id;
            DataLogic.DBA.DataLogic.Update(sql);

        }

        public static void UpdateOrdinal(int category1Id, int category2Id, int ordinal1, int ordinal2)
        {
            string sql;

            sql = "UPDATE Category SET Ordinal = " + ordinal1 + " WHERE CategoryID = " + category1Id;
            DataLogic.DBA.DataLogic.Update(sql);

            sql = "UPDATE Category SET Ordinal = " + ordinal2 + " WHERE CategoryID = " + category2Id;
            DataLogic.DBA.DataLogic.Update(sql);

        }
        
    }
}
