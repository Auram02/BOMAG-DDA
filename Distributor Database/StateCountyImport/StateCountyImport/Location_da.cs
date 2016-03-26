using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DDA.DataAccess
{
    class Location_da
    {
        public static int GetCityID(string p_City)
        {
            string sql;
            sql = "SELECT CityID FROM City WHERE CityName = " + p_City;
            DataSet ds = new DataSet();
            ds = DDA.StateCountyImport.DataLogic.Read(sql);

            

            int id;
            id = Convert.ToInt32(ds.Tables[0].Rows[0]["CityID"]);

            return id;

        }

        public static int GetStateID(string p_State)
        {
            string sql;
            sql = "SELECT StateID FROM State WHERE FullName = '" + p_State + "'";
            DataSet ds = new DataSet();
            ds = DDA.StateCountyImport.DataLogic.Read(sql);

            int id;
            id = Convert.ToInt32(ds.Tables[0].Rows[0]["StateID"]);

            return id;

        }

        public static int GetCountyID(string p_Country)
        {
            string sql;
            sql = "SELECT CountryID FROM Country WHERE CountryName = " + p_Country;
            DataSet ds = new DataSet();
            ds = DDA.StateCountyImport.DataLogic.Read(sql);

            int id;
            id = Convert.ToInt32(ds.Tables[0].Rows[0]["CountryID"]);

            return id;

        }

        public static void PopulateStateDropdown(ref System.Windows.Forms.ComboBox cmbState)
        {
            DataSet ds = new DataSet();
            ds = GetStateList();

            cmbState.DataSource = ds.Tables[0];
            cmbState.DisplayMember = "FullName";
            cmbState.ValueMember = "StateID";


        }

        public static DataSet GetStateList()
        {
            string sql;
            DataSet ds = new DataSet();

            sql = "SELECT StateID, FullName FROM State ORDER BY FullName";

            ds = DDA.StateCountyImport.DataLogic.Read(sql);

            return ds;
        }

        public static void PopulateCityDropdown(ref System.Windows.Forms.ComboBox cmbCity)
        {
            DataSet ds = new DataSet();
            ds = GetCityList();

            cmbCity.DataSource = ds.Tables[0];
            cmbCity.DisplayMember = "CityName";
            cmbCity.ValueMember = "CityID";


        }

        public static DataSet GetCityList()
        {
            string sql;
            DataSet ds = new DataSet();

            sql = "SELECT CityID, CityName FROM State ORDER BY CityName";

            ds = DDA.StateCountyImport.DataLogic.Read(sql);

            return ds;
        }

    }
}
