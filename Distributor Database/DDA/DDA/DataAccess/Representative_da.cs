using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataLogic.DBA;

namespace DDA.DataAccess
{
    class Representative_da
    {
        public static int GetActiveRepForDistributor(int p_distID, string p_type)
        {
            string sql;
            DataSet ds;

            sql = "SELECT fk_" + p_type + "RepID FROM DistributorRepresentative WHERE fk_DistributorID = " + p_distID;

            ds = DataLogic.DBA.DataLogic.Read(sql);

            int returnInt;
            returnInt = -1;

            try
            {
                returnInt = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            catch
            {
                returnInt = -1;
            }

            return returnInt;
        }


        public static DataSet GetRepresentativeList(string p_Type)
        {
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_Type);

            string sql;

            // fk_CityID is really a string
            sql = "SELECT DISTINCT RepID, RepName as [REP NAME], fk_CityID as [CITY], " +
                    "State.Abbreviation as [STATE], Phone  as [PHONE], fk_DistributorID as [DistID] " +
                    "FROM Representative, City, State, RepresentativeType  " +
                    "WHERE Representative.fk_StateID = State.StateID " + 
                    "AND Representative.fk_RepTypeID = RepresentativeType.RepTypeID " +
                    "AND RepresentativeType.Description = '" + p_Type + "' " + 
                    "ORDER BY RepName";

            DataSet dsRep;
            dsRep = DataLogic.DBA.DataLogic.Read(sql);

            return dsRep;
        }

        public static void RemoveRepresentative(int p_repID, string p_type)
        {
            string sql;

            // fk_CityID is really a string
            sql = "UPDATE DistributorRepresentative SET fk_" + p_type + "RepID = 0 WHERE fk_" + p_type + "RepID = " + p_repID;
            DataLogic.DBA.DataLogic.Update(sql);
            sql = "DELETE FROM Representative WHERE RepID = " + p_repID;
            DataLogic.DBA.DataLogic.Update(sql);
        }



        public static void ReassignRepresentative(int p_repID, string p_type, int p_NewRepID, bool noDelete)
        {
            string sql;

            // fk_CityID is really a string
            sql = "UPDATE DistributorRepresentative SET fk_" + p_type + "RepID = " + p_NewRepID + " WHERE fk_" + p_type + "RepID = " + p_repID;
            DataLogic.DBA.DataLogic.Update(sql);

            if (noDelete == false)
            {
                sql = "DELETE FROM Representative WHERE RepID = " + p_repID;
                DataLogic.DBA.DataLogic.Update(sql);
            }
        }

        public static int GetRepresentativeCount(string p_Type, int p_distID)
        {
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_Type);

            string sql;

            //sql = "SELECT COUNT(RepID) " +
            //        "FROM Representative, State, RepresentativeType, DistributorRepresentative  " +
            //        "WHERE Representative.fk_StateID = State.StateID " +
            //        "AND Representative.fk_RepTypeID = RepresentativeType.RepTypeID " +
            //        "AND RepresentativeType.Description = '" + p_Type + "'" +
            //        "AND DistributorRepresentative.fk_DistributorID = " + p_distID;

            sql = "SELECT COUNT(RepID) " +
        "FROM Representative, State, RepresentativeType, DistributorRepresentative  " +
        "WHERE Representative.fk_StateID = State.StateID " +
        "AND Representative.fk_RepTypeID = RepresentativeType.RepTypeID " +
        "AND RepresentativeType.Description = '" + p_Type + "'" +
        "AND DistributorRepresentative.fk_DistributorID = " + p_distID;

            DataSet dsRep;
            dsRep = DataLogic.DBA.DataLogic.Read(sql);

            int iCount = 0;

            try
            {
                iCount = Convert.ToInt32(dsRep.Tables[0].Rows[0][0]);
            }
            catch (Exception ex)
            {
                iCount = 0;

            }

            return iCount;
        }

        /// <summary>
        /// Determine if the repID is present for the distId supplied
        /// </summary>
        /// <param name="p_Type"></param>
        /// <param name="p_distID"></param>
        /// <param name="p_repID"></param>
        /// <returns>1 if association is present, 0 if not</returns>
        public static int GetRepresentativeCount_Active(string p_Type, int p_distID, int p_repID)
        {
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_Type);

            string sql;

            //sql = "SELECT COUNT(RepID) " +
            //        "FROM Representative, State, RepresentativeType, DistributorRepresentative  " +
            //        "WHERE Representative.fk_StateID = State.StateID " +
            //        "AND Representative.fk_RepTypeID = RepresentativeType.RepTypeID " +
            //        "AND RepresentativeType.Description = '" + p_Type + "'" +
            //        "AND DistributorRepresentative.fk_DistributorID = " + p_distID;

            sql = "SELECT COUNT(fk_" + p_Type + "RepID) " +
        "FROM DistributorRepresentative  " +
        "WHERE DistributorRepresentative.fk_DistributorID = " + p_distID +
        " AND DistributorRepresentative.fk_" + p_Type + "RepID = " + p_repID;

            DataSet dsRep;
            dsRep = DataLogic.DBA.DataLogic.Read(sql);

            int iCount = 0;

            try
            {
                iCount = Convert.ToInt32(dsRep.Tables[0].Rows[0][0]);
            }
            catch (Exception ex)
            {
                iCount = 0;

            }

            return iCount;
        }

        /// <summary>
        /// Gets the number of distributors that this representative is assigned to
        /// </summary>
        /// <param name="p_Type"></param>
        /// <param name="p_repID"></param>
        /// <returns>Number of distributors this rep is assigned to</returns>
        public static int GetRepresentativeCount_Active(string p_Type, int p_repID)
        {
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_Type);

            string sql;

            //sql = "SELECT COUNT(RepID) " +
            //        "FROM Representative, State, RepresentativeType, DistributorRepresentative  " +
            //        "WHERE Representative.fk_StateID = State.StateID " +
            //        "AND Representative.fk_RepTypeID = RepresentativeType.RepTypeID " +
            //        "AND RepresentativeType.Description = '" + p_Type + "'" +
            //        "AND DistributorRepresentative.fk_DistributorID = " + p_distID;

            sql = "SELECT COUNT(fk_" + p_Type + "RepID) " +
        "FROM DistributorRepresentative  " +
        "WHERE DistributorRepresentative.fk_" + p_Type + "RepID = " + p_repID;

            DataSet dsRep;
            dsRep = DataLogic.DBA.DataLogic.Read(sql);

            int iCount = 0;

            try
            {
                iCount = Convert.ToInt32(dsRep.Tables[0].Rows[0][0]);
            }
            catch (Exception ex)
            {
                iCount = 0;

            }

            return iCount;
        }


        public static DataSet GetRepresentativeInformation(int p_ID)
        {
            string sql;

            
            sql = "SELECT RepID, RepName, Address, fk_CityID AS [CityName], RepresentativeType.Description, " + 
                    "State.FullName, Country.CountryName, Phone, Fax, Email, fk_ZipID, MobilePhone " +
                    "FROM Representative, RepresentativeType, State, Country " +
                    "WHERE Representative.fk_RepTypeID = RepresentativeType.RepTypeID " +
                    "AND Representative.fk_StateID = State.StateID " +
                    "AND Representative.fk_CountryID = Country.CountryID " +
                    "AND Representative.RepID = " + p_ID;

            DataSet dsRep;
            dsRep = DataLogic.DBA.DataLogic.Read(sql);

            return dsRep;
        }

        public static string GetRepresentativeName(int p_ID)
        {
            string sql;


            sql = "SELECT RepName FROM Representative " +
                    "WHERE Representative.RepID = " + p_ID;

            DataSet dsRep;
            dsRep = DataLogic.DBA.DataLogic.Read(sql);

            string sReturn;

            try
            {
                sReturn = Convert.ToString(dsRep.Tables[0].Rows[0]["RepName"]);
            }
            catch
            {
                sReturn = "";
            }

            return sReturn;
        }

        public static string AddRepresentative(int p_DistributorID, string p_name, string p_address, 
                                                    string p_city, string p_country, string p_email, string p_fax, 
                                                    string p_phone, string p_state, string p_zip, string p_repType, string p_mobilePhone)
        {
            string returnVal = "";
            string sql;

           
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_DistributorID);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_name);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_address);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_city);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_country);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_email);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_fax);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_phone);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_state);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_zip);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_repType);

            int cityID, stateID, countryID;
            //cityID = Location_da.GetCityID(p_city);
            stateID = Location_da.GetStateID(p_state);
            countryID = Location_da.GetCountryID(p_country);

            int nextRepID;
            nextRepID = DataLogic.DBA.DataLogic.GetNextID("Representative", "RepID");

            int repTypeID = 0;
            repTypeID = GetRepTypeID(p_repType);


            sql = "INSERT INTO Representative(RepID, RepName, Address, fk_CityID, fk_StateID, fk_ZipID, " +
                                                "fk_CountryID, Phone, Fax, Email, fk_RepTypeID, MobilePhone) " +
                                                "VALUES ('" + nextRepID + "','" + p_name + "','" + p_address + 
                                                "','" + p_city + "','" + stateID +
                                                "','" + p_zip + "','" + countryID + "','" + p_phone + "','" + p_fax +
                                                "','" + p_email + "','" + repTypeID + "','" + p_mobilePhone + "')";


            DataLogic.DBA.DataLogic.Update(sql);                                


            return returnVal;
        }

        public static string UpdateRepresentative(int p_ID, int p_DistributorID, string p_name, 
                                                    string p_address, string p_city, string p_country, string p_email,
                                                    string p_fax, string p_phone, string p_state, string p_zip, string p_repType, string p_mobilePhone)
        {
            string returnVal = "";
            string sql;

            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_DistributorID);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_name);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_address);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_city);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_country);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_email);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_fax);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_phone);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_state);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_zip);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_repType);

            int cityID, stateID, countryID;
            //cityID = Location_da.GetCityID(p_city);
            stateID = Location_da.GetStateID(p_state);
            countryID = Location_da.GetCountryID(p_country);

            int nextRepID;
            nextRepID = DataLogic.DBA.DataLogic.GetNextID("Representative", "RepID");

            int repTypeID = 0;
            repTypeID = GetRepTypeID(p_repType);

            sql = "UPDATE Representative SET " +
                    " RepName = '" + p_name + "', Address = '" + p_address + "', fk_CityID = '" + p_city + 
                    "' , fk_StateID = '" + stateID + "', fk_ZipID = '" + p_zip + "', " +
                    " fk_CountryID = '" + countryID + "', Phone = '" + p_phone + "', Fax = '" + p_fax + 
                    "' , Email = '" + p_email + "', fk_RepTypeID = '" + repTypeID + "', MobilePhone = '" + p_mobilePhone + 
                    "' WHERE RepID = " + p_ID;
            

            DataLogic.DBA.DataLogic.Update(sql);

            return returnVal;
        }




        public static string GetRepresentativeDistributorCount(int p_DistributorID)
        {
            string returnVal = "";
            string sql;

            sql = "SELECT COUNT (fk_DistributorID) FROM DistributorRepresentative WHERE fk_DistributorID = " + p_DistributorID + "";
            DataSet ds;

            ds = DataLogic.DBA.DataLogic.Read(sql);

            return (Convert.ToString(ds.Tables[0].Rows[0][0].ToString()));

        }

        public static string InsertRepresentativeDistributor(int p_ID, int p_DistributorID, string sMode)
        {
            string returnVal = "";
            string sql;

            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_DistributorID);


            //sql = "UPDATE Representative SET " +
            //" fk_DistributorID = " + p_DistributorID +
            //" WHERE RepID = " + p_ID;
            string sCol;

            if (sMode == "service")
            {
                sCol = "fk_ServiceRepID";
            }
            else
            {
                sCol = "fk_TerritoryRepID";
            }

            sql = "INSERT INTO DistributorRepresentative (fk_DistributorID, " + sCol + ") VALUES ('" + p_DistributorID + "'," + p_ID + ")";

            DataLogic.DBA.DataLogic.Update(sql);

            return returnVal;
        }

        public static string UpdateRepresentativeDistributor(int p_ID, int p_DistributorID, string sMode)
        {
            string returnVal = "";
            string sql;

            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_DistributorID);


            //sql = "UPDATE Representative SET " +
            //" fk_DistributorID = " + p_DistributorID +
            //" WHERE RepID = " + p_ID;
            string sCol;

            if (sMode == "service")
            {
                sCol = "fk_ServiceRepID";
            }
            else
            {
                sCol = "fk_TerritoryRepID";
            }

            sql = "UPDATE DistributorRepresentative SET " + sCol + " = " + p_ID + " WHERE fk_DistributorID = " + p_DistributorID;

            DataLogic.DBA.DataLogic.Update(sql);

            return returnVal;
        }


        private static int GetRepTypeID(string p_repType)
        {
//            DataLogic.DBA.DataLogic.PrepareSQL(ref p_repType);

            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_repType);

            string sql;
            sql = "SELECT RepTypeID FROM RepresentativeType WHERE [Description] = '" + p_repType + "'";

            int id;
            id = Convert.ToInt32(DataLogic.DBA.DataLogic.Read(sql).Tables[0].Rows[0]["RepTypeID"]);

            return id;
        }

        public static int GetRepID(string p_repName)
        {
            //            DataLogic.DBA.DataLogic.PrepareSQL(ref p_repName);

            string sql;
            sql = "SELECT RepID FROM Representative WHERE RepName = '" + p_repName + "'";

            int id;
            id = Convert.ToInt32(DataLogic.DBA.DataLogic.Read(sql).Tables[0].Rows[0]["RepID"]);

            return id;
        }

    }
}
