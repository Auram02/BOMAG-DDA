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
           // DataLogic.DBA.DataLogic.PrepareSQL(ref p_City);

            string sql;
            sql = "SELECT CityID FROM City WHERE CityName = '" + p_City + "'";
            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            int id;
            id = Convert.ToInt32(ds.Tables[0].Rows[0]["CityID"]);

            return id;

        }

        public static string GetCityName(int p_id)
        {
            string sql;
            sql = "SELECT CityName FROM City WHERE CityID = " + p_id;
            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            string cityName;
            cityName = Convert.ToString(ds.Tables[0].Rows[0]["CityName"]);

            return cityName;

        }

        public static int GetStateID(string p_State)
        {
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_State);

            string sql;
            sql = "SELECT StateID FROM State WHERE FullName = '" + p_State + "'";
            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            int id;
            id = Convert.ToInt32(ds.Tables[0].Rows[0]["StateID"]);

            return id;

        }

        public static int GetStateIDByAbbreviation(string p_State)
        {
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_State);

            string sql;
            sql = "SELECT StateID FROM State WHERE Abbreviation = '" + p_State + "'";
            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            int id;
            id = Convert.ToInt32(ds.Tables[0].Rows[0]["StateID"]);

            return id;

        }

        public static string GetStateFullName(int county_id)
        {
            string sql;
            sql = "SELECT FullName FROM State INNER JOIN County ON County.fk_StateID = State.StateID " +
                    " WHERE County.CountyID = " + county_id;
            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            string fullName = String.Empty;

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                fullName = Convert.ToString(ds.Tables[0].Rows[0]["FullName"]);

            return fullName;

        }

        public static string GetStateFullName2(int state_id)
        {
            string sql;
            sql = "SELECT FullName FROM State WHERE State.StateID = " + state_id;

            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            string fullName;
            fullName = Convert.ToString(ds.Tables[0].Rows[0]["FullName"]);

            return fullName;

        }


        public static string GetStateAbbreviation(int p_id)
        {
            string sql;
            sql = "SELECT Abbreviation FROM State WHERE StateID = " + p_id;
            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            string abbr;
            abbr = Convert.ToString(ds.Tables[0].Rows[0]["Abbreviation"]);

            return abbr;

        }

        public static string GetStateAbbreviation(string p_FullName)
        {
            string sql;
            sql = "SELECT Abbreviation FROM State WHERE FullName = '" + p_FullName + "'";
            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            string abbr;
            abbr = Convert.ToString(ds.Tables[0].Rows[0]["Abbreviation"]);

            return abbr;

        }

        public static string GetStateAbbreviation2(string p_FullName)
        {
            string sql;
            sql = "SELECT Abbreviation FROM State WHERE FullName = '" + p_FullName + "'";
            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            string abbr;
            abbr = Convert.ToString(ds.Tables[0].Rows[0]["Abbreviation"]);

            return abbr;

        }

        public static int GetCountryID(string p_Country)
        {
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_Country);

            string sql;
            sql = "SELECT CountryID FROM Country WHERE CountryName = '" + p_Country + "'";
            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

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

        public static void PopulateStateDropdown(ref MattBerther.Controls.AutoCompleteComboBox cmbState)
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

            sql = "SELECT StateID, FullName, Abbreviation FROM State ORDER BY FullName";

            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }

        public static DataSet GetStateAbbrList()
        {
            string sql;
            DataSet ds = new DataSet();

            sql = "SELECT StateID, Abbreviation, FullName FROM State ORDER BY FullName";

            ds = DataLogic.DBA.DataLogic.Read(sql);

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

            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }

        public static DataSet GetCountyList(int p_StateID)
        {
            string sql;
            DataSet ds;

            sql = "SELECT CountyName, CountyID FROM County WHERE fk_StateID = " + p_StateID + " ORDER BY CountyName, CountyID";

            ds = DataLogic.DBA.DataLogic.Read(sql);
            return ds;
        }

        public static int GetCountyID(string p_County, int p_StateID)
        {
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_County);

            string sql;

            DataLogic.DBA.DataLogic.PrepareSQL(ref p_County);

            sql = "SELECT CountyID FROM County WHERE CountyName = '" + p_County + "' AND fk_StateID = " + p_StateID;
            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);
            
                int id;

            try
            {
                id = Convert.ToInt32(ds.Tables[0].Rows[0]["CountyID"]);
            } catch {
                id = -1;
            }

            return id;

        }

        public static void UpdateCounty(int p_id, string p_name)
        {

            string sql;
            sql = "UPDATE County SET CountyName = '" + p_name + "' WHERE CountyID = " + p_id;

            DataLogic.DBA.DataLogic.Update(sql);

        }

        public static void UpdateState(int p_id, string p_name, string p_abb)
        {

            string sql;
            sql = "UPDATE State SET FullName = '" + p_name + "', Abbreviation = '" + p_abb + "' WHERE StateID = " + p_id;

            DataLogic.DBA.DataLogic.Update(sql);

        }

        

        public static void RemoveState(int p_id)
        {

            string sql;

            DataSet ds;

            ds = DDA.DataAccess.Location_da.GetCountyList(p_id);    

            string countyID;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
  //              if (countyID != "")
    //                countyID = countyID + ",";

                // remove county from contract
                DDA.DataAccess.Contract_da.DeleteCountyFromContract(Convert.ToInt32(ds.Tables[0].Rows[i][1]));

//                countyID = countyID + ds.Tables[0].Rows[i][0].ToString();
            }



            //DeleteCountyFromContract
            sql = "DELETE FROM County WHERE fk_stateID = " + p_id;
            DataLogic.DBA.DataLogic.Update(sql);


            sql = "DELETE FROM State WHERE StateID = " + p_id;
            DataLogic.DBA.DataLogic.Update(sql);

        }

        public static void AddCounty(string p_name, int p_stateID)
        {

            string sql;
            int id;

            id = DataLogic.DBA.DataLogic.GetNextID("County", "CountyID");

            sql = "INSERT INTO County VALUES(" + id + ",'" + p_name + "'," + p_stateID + ")";
            DataLogic.DBA.DataLogic.Update(sql);

        }


        public static void AddState(string p_name, string p_abb)
        {

            string sql;
            int id;

            id = DataLogic.DBA.DataLogic.GetNextID("State", "StateID");

            sql = "INSERT INTO State VALUES(" + id + ",'" + p_abb + "','" + p_name + "')";
            DataLogic.DBA.DataLogic.Update(sql);

        }

        public static bool CheckIfCountyIDIsInState(int p_cID, int p_sID)
        {

            string sql;
            DataSet ds = new DataSet();

            sql = "SELECT CountyName FROM County WHERE CountyID = " + p_cID + " AND fk_StateID = " + p_sID;
            ds = DataLogic.DBA.DataLogic.Read(sql);

            bool result;

            try
            {
                string cName;

                // Try to force a null exception error
                cName = Convert.ToString(ds.Tables[0].Rows[0]["CountyName"]);

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }



            return result;

        }

        public static string GetCountyName(int p_id)
        {

            if (p_id > -1)
            {
                string sql;
                sql = "SELECT CountyName FROM County WHERE CountyID = " + p_id;
                DataSet ds = new DataSet();
                ds = DataLogic.DBA.DataLogic.Read(sql);

                string countyName;
                countyName = Convert.ToString(ds.Tables[0].Rows[0]["CountyName"]);

                return countyName;
            }
            else
            {
                return "";
            }
        }

        public static string GetCountyStateAbbreviation(int p_id)
        {
            if (p_id > -1)
            {
                string sql;
                sql = "SELECT Abbreviation FROM County, State WHERE CountyID = " + p_id + " AND County.fk_StateID = State.StateID";
                DataSet ds = new DataSet();
                ds = DataLogic.DBA.DataLogic.Read(sql);

                string countyName;
                countyName = Convert.ToString(ds.Tables[0].Rows[0]["Abbreviation"]);

                return countyName;
            }
            else
            {
                return "";
            } 
        }


        public static DataSet GetStateList(string countyID)
        {
           // DataLogic.DBA.DataLogic.PrepareSQL(ref countyID);

            string sql;
            DataSet ds = new DataSet();

            if (countyID != "")
            {
                sql = "SELECT DISTINCT Abbreviation FROM State, County WHERE CountyID IN (" + countyID + ") AND County.fk_StateID = State.StateID";

                ds = DataLogic.DBA.DataLogic.Read(sql);
            }

            return ds;
        }

        public static DataSet GetCountyList(string countyID)
        {
            // DataLogic.DBA.DataLogic.PrepareSQL(ref countyID);

            string sql;
            sql = "SELECT DISTINCT CountyName FROM County WHERE CountyID IN (" + countyID + ")";

            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }

        public static DataSet GetCountyStateAbbreviationList(string countyID)
        {
            // DataLogic.DBA.DataLogic.PrepareSQL(ref countyID);

            string sql;
            sql = "SELECT DISTINCT CountyName, Abbreviation FROM County, State WHERE CountyID IN (" + countyID + ") AND County.fk_StateID = State.StateID" + 
                  " ORDER BY Abbreviation, CountyName ";

            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }

        public static DataSet GetDistributorLocationList(string distributorID)
        {
          //  DataLogic.DBA.DataLogic.PrepareSQL(ref distributorID);

            string sql;

            sql = "SELECT CityName, Abbreviation FROM Distributor, State WHERE pk_DistributorID IN (" + distributorID + ") AND fk_StateID = StateID";

            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }

    }
}
