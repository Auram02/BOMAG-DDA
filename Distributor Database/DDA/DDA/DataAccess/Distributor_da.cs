using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataLogic;

namespace DDA.DataAccess
{
    class Distributor_da
    {

        public static DataSet GetDistributorList()
        {
            return GetDistributorList("none", "");
        }

        public static DataSet GetDistributorListContacts()
        {
            String sql;


            sql = "SELECT Dist.pk_DistributorID, Contacts " +
                    "FROM Distributor Dist ";

            DataSet dsList;
            dsList = DataLogic.DBA.DataLogic.Read(sql);

            return dsList;
        }

        public static DataSet GetMainDistributorNames()
        {
            String sql;

            //sql = "SELECT DistName as [DISTRIBUTOR NAME], CityName as [CITY], State.Abbreviation as [STATE], Dist.Phone as [PHONE], Rep.RepName as [CONTACT], Dist.pk_DistributorID " +
            //        "FROM Distributor Dist, State, Representative Rep " +
            //        "WHERE Dist.fk_StateID = StateID " +
            //        "AND Dist.fk_RepresentativeID = Rep.RepID ";
            sql = "SELECT DistName as [DISTRIBUTOR NAME], pk_DistributorID " +
                    "FROM Distributor Dist " +
                    "WHERE MainDistributor = 1" +
                    " ORDER BY DistName";

            DataSet dsList;
            dsList = DataLogic.DBA.DataLogic.Read(sql);

            return dsList;
        }

        public static bool IsMainDistributor(int p_id)
        {
            String sql;
            bool returnVal = false;

            //sql = "SELECT DistName as [DISTRIBUTOR NAME], CityName as [CITY], State.Abbreviation as [STATE], Dist.Phone as [PHONE], Rep.RepName as [CONTACT], Dist.pk_DistributorID " +
            //        "FROM Distributor Dist, State, Representative Rep " +
            //        "WHERE Dist.fk_StateID = StateID " +
            //        "AND Dist.fk_RepresentativeID = Rep.RepID ";
            sql = "SELECT pk_DistributorID " +
                    "FROM Distributor Dist " +
                    "WHERE MainDistributor = 1" +
                    " AND pk_DistributorID = " + p_id +
                    " ORDER BY DistName";

            DataSet dsList;
            dsList = DataLogic.DBA.DataLogic.Read(sql);

            try
            {
                if (dsList.Tables[0].Rows.Count > 0)
                    returnVal = true;

            }
            catch
            {
                returnVal = false;
            }

            return returnVal;

        }


        public static DataSet GetDistributorList(string listMode, string searchKey)
        {
            String sql;

            //sql = "SELECT DistName as [DISTRIBUTOR NAME], CityName as [CITY], State.Abbreviation as [STATE], Dist.Phone as [PHONE], Rep.RepName as [CONTACT], Dist.pk_DistributorID " +
            //        "FROM Distributor Dist, State, Representative Rep " +
            //        "WHERE Dist.fk_StateID = StateID " +
            //        "AND Dist.fk_RepresentativeID = Rep.RepID ";
            sql = "SELECT DistName as [DISTRIBUTOR NAME], CityName as [CITY], State.Abbreviation as [STATE], Dist.Phone as [PHONE], Dist.pk_DistributorID, ManufacturerRep " +
        "FROM Distributor Dist, State ";

            if (listMode == "Sales")
                sql += " , DistributorBranch ";

            sql += "WHERE Dist.fk_StateID = StateID ";

            if (listMode == "Main")
            {
                sql = sql + " AND MainDistributor = 1";
            }

            if (listMode == "Sales")
            {

                sql += " and fk_BranchDistID = CStr(pk_DistributorID)  ";

                //    sql = sql + " AND pk_DistributorID = 0";
            }

            if (searchKey != "")
            {
                sql = sql + " AND DistName LIKE '%" + searchKey + "%'";
            }

            sql = sql + " ORDER BY DistName";

            DataSet dsList;
            dsList = DataLogic.DBA.DataLogic.Read(sql);

            return dsList;
        }

        public static DataSet GetDistributorList_SellingLocationsInState(string StateAbbr)
        {
            String sql;

            //sql = "SELECT DistName as [DISTRIBUTOR NAME], CityName as [CITY], State.Abbreviation as [STATE], Dist.Phone as [PHONE], Rep.RepName as [CONTACT], Dist.pk_DistributorID " +
            //        "FROM Distributor Dist, State, Representative Rep " +
            //        "WHERE Dist.fk_StateID = StateID " +
            //        "AND Dist.fk_RepresentativeID = Rep.RepID ";


            sql = "select  distname AS [DISTRIBUTOR NAME], State.Abbreviation AS [STATE], Phone as [PHONE], pk_distributorid " +
                    "FROM distributor, DistributorBranch, state " +
                    "WHERE Distributor.pk_DistributorID LIKE distributorbranch.fk_branchdistid " +
                    "AND Distributor.fk_stateid = state.stateid " +
                    "and state.abbreviation = '" + StateAbbr + "'";



            sql = sql + " ORDER BY DistName";

            DataSet dsList;
            dsList = DataLogic.DBA.DataLogic.Read(sql);

            string branchList = "";
            // have branchid's, now get main
            for (int i = 0; i < dsList.Tables[0].Rows.Count; i++)
            {
                if (branchList != "")
                    branchList += ",";
                branchList += "'" + dsList.Tables[0].Rows[i]["pk_distributorID"].ToString() + "'";
            }

            if (branchList == "")
                branchList = "''";

            //sql = "SELECT DISTINCT (fk_maindistid) FROM distributorbranch " +
            //        "WHERE fk_branchdistid IN (" + branchList +")";

            sql = "SELECT fk_maindistid AS [DistID], distname AS [DISTRIBUTOR NAME]  FROM distributorbranch, distributor " +
"WHERE fk_branchdistid IN (" + branchList + ") " +
"AND fk_branchdistid like distributor.pk_distributorid " +
"group by fk_maindistid, distname " +
"ORDER BY distname";


            DataSet dsMainIds = new DataSet();
            dsMainIds = DataLogic.DBA.DataLogic.Read(sql);
            return dsMainIds;
        }

        public static DataSet GetDistributorEmailList(int p_mainDist)
        {
            DataSet ds;
            string sql;

            //sql = "SELECT DistName AS [DISTRIBUTOR], ShippingAddress AS [ADDRESS], CityName AS [CITY], Abbreviation AS [STATE], fk_ZipID AS [ZIP], PersonName AS [CONTACT NAME], Email AS [EMAIL ADDRESS] " +
            //        " FROM Distributor, DistributorEmail, State" +
            //        " WHERE Distributor.pk_DistributorID = DistributorEmail.fk_DistributorID" +
            //        " AND Distributor.fk_StateID = State.StateID " +
            //        " AND MainDistributor = " + p_mainDist +
            //        " ORDER BY DistName, Abbreviation, CityName";

            sql = "SELECT DistName AS [DISTRIBUTOR], BillingAddress AS [ADDRESS], CityName AS [CITY], Abbreviation AS [STATE], fk_ZipID AS [ZIP], PersonName AS [CONTACT NAME], Email AS [EMAIL ADDRESS] " +
                   " FROM Distributor, DistributorEmail, State" +
                   " WHERE Distributor.pk_DistributorID = DistributorEmail.fk_DistributorID" +
                   " AND Distributor.fk_StateID = State.StateID " +
                   " AND MainDistributor = " + p_mainDist +
                   " ORDER BY DistName, Abbreviation, CityName";

            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }

        public static DataSet GetDistributorRepReport()
        {
            DataSet ds;
            string sql;

            //sql = "SELECT DistName AS [DISTRIBUTOR], ShippingAddress AS [ADDRESS], CityName AS [CITY], Abbreviation AS [STATE], fk_ZipID AS [ZIP], PersonName AS [CONTACT NAME], Email AS [EMAIL ADDRESS] " +
            //        " FROM Distributor, DistributorEmail, State" +
            //        " WHERE Distributor.pk_DistributorID = DistributorEmail.fk_DistributorID" +
            //        " AND Distributor.fk_StateID = State.StateID " +
            //        " AND MainDistributor = " + p_mainDist +
            //        " ORDER BY DistName, Abbreviation, CityName";

            sql = "select Distributor.DistName as [Distributor Name], Distributor.CityName AS [Shipping City Name], Distributor.BillingCityName as [Billing City Name] , TR.RepName as [Territory Rep], SR.RepName as [Service Rep] from distributor,distributorRepresentative dr,Representative TR, Representative SR " +
                    " WHERE dr.fk_distributorid = distributor.pk_DistributorID " +
                    " AND TR.RepID = dr.fk_TerritoryRepID" +
                    " AND SR.RepID = dr.fk_ServiceRepID " +
                    " GROUP BY DistName, Distributor.CityName, Distributor.BillingCityName, TR.RepName, SR.RepName";

            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }

        public static DataSet GetDistributorInformation(int p_distrituborID)
        {
            String sql;

            //sql = "SELECT * FROM Distributor WHERE pk_DistributorID = " + p_distrituborID;

            //sql = "SELECT SAP, Node, Distributor.Email, DistName, BillingAddress, BillingCityName, fk_BillingStateID, fk_BillingZipID, fk_BillingCountryID, ShippingAddress, Distributor.Phone, " +
            //        "Distributor.Fax, Distributor.CityName, State.FullName, Country.CountryName, " +
            //        "Representative.RepName, Distributor.fk_ZipID " +
            //        "FROM Distributor, City, State, Country, Representative " +
            //        "WHERE State.StateID = Distributor.fk_StateID " +
            //        "AND Country.CountryID = Distributor.fk_CountryID " +
            //        "AND Representative.RepID = Distributor.fk_RepresentativeID " +
            //        "AND pk_DistributorID = " + p_distrituborID;
            sql = "SELECT SAP, Node, DistName, BillingAddress, BillingCityName, fk_BillingStateID, fk_BillingZipID, fk_BillingCountryID, ShippingAddress, Distributor.Phone, " +
                    "Distributor.Fax, Distributor.CityName, State.FullName, Country.CountryName, " +
                    "Contacts, Distributor.fk_ZipID, Distributor.PartsOnly, Distributor.ManufacturerRep, Distributor.MainDistributor " +
                    "FROM Distributor, City, State, Country " +
                    "WHERE State.StateID = Distributor.fk_StateID " +
                    "AND Country.CountryID = Distributor.fk_CountryID " +
                    "AND pk_DistributorID = " + p_distrituborID;

            DataSet dsDI;
            dsDI = DataLogic.DBA.DataLogic.Read(sql);

            return dsDI;
        }

        public static void DeleteDistributor(int p_id)
        {

            string sql;

            //sql = "DELETE FROM Distributor WHERE Distributor.pk_DistributorID = distributorbranch.fk_BranchDistID AND DistributorBranch.fk_MainDistID = " + p_id;
            //DataLogic.DBA.DataLogic.Update(sql);

            //sql = "DELETE FROM DistributorBranch WHERE fk_MainDistID = " + p_id;
            //DataLogic.DBA.DataLogic.Update(sql);

            sql = "SELECT fk_ContractID FROM ContractDistributor WHERE fk_DistributorID = " + p_id;
            DataSet ds;
            ds = DataLogic.DBA.DataLogic.Read(sql);

            int i;
            int contractID;


            bool isMainDist = DataAccess.Distributor_da.IsMainDistributor(p_id);

            if (isMainDist)  // if they are a main distributor, then delete the contract
            {

                //--for each contract id, delete all stuff associated.
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    contractID = Convert.ToInt32(ds.Tables[0].Rows[i]["fk_ContractID"].ToString());
                    DDA.DataAccess.Contract_da.DeleteContract(contractID);
                }
            }


            sql = "DELETE FROM DistributorRepresentative WHERE fk_DistributorID = " + p_id;
            DataLogic.DBA.DataLogic.Update(sql);

            sql = "DELETE FROM DistributorEmail WHERE fk_DistributorID = " + p_id;
            DataLogic.DBA.DataLogic.Update(sql);

            sql = "(select DISTINCT(DistributorBranch.fk_BranchDistID) " +
                    "FROM Distributor, DistributorBranch WHERE DistributorBranch.fk_MainDistID = " + p_id + ")";




            DataSet ds4 = DataLogic.DBA.DataLogic.Read(sql);

            foreach (DataRow dr in ds4.Tables[0].Rows)
            {
                int BranchID = Convert.ToInt32(dr["fk_BranchDistID"].ToString());

                sql = "SELECT fk_ContractID FROM ContractDistributor WHERE fk_DistributorID = " + BranchID;
                DataSet dsBranchContract;
                dsBranchContract = DataLogic.DBA.DataLogic.Read(sql);

                //--Delete all contracts for each of the branches
                for (i = 0; i < dsBranchContract.Tables[0].Rows.Count; i++)
                {
                    contractID = Convert.ToInt32(dsBranchContract.Tables[0].Rows[i]["fk_ContractID"].ToString());
                    DDA.DataAccess.Contract_da.DeleteContract(contractID);
                }

            }

            sql = "";


            foreach (DataRow dr in ds4.Tables[0].Rows)
            {

                sql = "DELETE FROM Distributor WHERE Distributor.pk_DistributorID LIKE '" + dr["fk_BranchDistID"].ToString() + "';";
                DataLogic.DBA.DataLogic.Update(sql);

            }



            sql = "DELETE FROM DistributorBranch WHERE fk_MainDistID = " + p_id;
            DataLogic.DBA.DataLogic.Update(sql);

            sql = "DELETE FROM Distributor WHERE pk_DistributorID = " + p_id;
            DataLogic.DBA.DataLogic.Update(sql);



        }


        public static void DeleteBranchDistributor(int p_id)
        {

            string sql;

            //sql = "DELETE FROM Distributor WHERE Distributor.pk_DistributorID = distributorbranch.fk_BranchDistID AND DistributorBranch.fk_MainDistID = " + p_id;
            //DataLogic.DBA.DataLogic.Update(sql);

            //sql = "DELETE FROM DistributorBranch WHERE fk_MainDistID = " + p_id;
            //DataLogic.DBA.DataLogic.Update(sql);

            sql = "DELETE FROM ContractDistributor WHERE fk_DistributorID = " + p_id;

            DataLogic.DBA.DataLogic.Update(sql);



            int i;
            int contractID;


            bool isMainDist = DataAccess.Distributor_da.IsMainDistributor(p_id);


            // no matter what, remove them from the distributor branches list
            sql = "DELETE FROM DistributorBranch WHERE fk_BranchDistID = '" + p_id + "'";
            DataLogic.DBA.DataLogic.Update(sql);


            if (isMainDist == false)
            {
                sql = "DELETE FROM Distributor WHERE pk_DistributorID = " + p_id;
                DataLogic.DBA.DataLogic.Update(sql);


                sql = "DELETE FROM DistributorRepresentative WHERE fk_DistributorID = " + p_id;
                DataLogic.DBA.DataLogic.Update(sql);

                sql = "DELETE FROM DistributorEmail WHERE fk_DistributorID = " + p_id;
                DataLogic.DBA.DataLogic.Update(sql);
            }



        }

        public static string AddBranch(string p_ParentBranch, string p_name, string p_BillingAddress, string p_ShippingAddress,
                                                    string p_city, string p_country, string p_fax,
                                                    string p_phone, string p_state, string p_zip, string p_repName, string p_BillingCity, string p_BStateID,
                                                    string p_BZipID, string p_BCountryID, string p_SAP, string p_Node, int IsMainDist, int IsPartsOnly, int isManufacturerRep)
        {
            string returnVal = "";
            string sql;

            DataLogic.DBA.DataLogic.PrepareSQL(ref p_name);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_BillingAddress);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_ShippingAddress);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_city);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_country);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_fax);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_phone);
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_state);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_zip);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_repName);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_ParentBranch);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_BillingCity);
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_BStateID);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_BZipID);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_BCountryID);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_Node);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_SAP);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_repName);

            int cityID, stateID, countryID, billingStateID, billingCountryID;
            //cityID = Location_da.GetCityID(p_city);
            stateID = Location_da.GetStateID(p_state);
            countryID = Location_da.GetCountryID(p_country);
            billingCountryID = Location_da.GetCountryID(p_BCountryID);
            billingStateID = Location_da.GetStateID(p_BStateID);

            int nextBranchID;
            nextBranchID = DataLogic.DBA.DataLogic.GetNextID("Distributor", "pk_DistributorID");

            //int repID;
            //repID = Representative_da.GetRepID(p_repName);


            int p_ParentBranchID;

            string p_Replace;
            p_Replace = p_ParentBranch;

            p_Replace = p_Replace.Replace("'", "");

            if (p_Replace == "")
            {
                p_ParentBranchID = nextBranchID;
            }
            else
            {
                p_ParentBranchID = GetDistributorID(p_ParentBranch);
            }

            sql = "INSERT INTO Distributor(pk_DistributorID, DistName, BillingAddress, ShippingAddress, CityName, fk_StateID, fk_ZipID, " +
                                    "fk_CountryID, Phone, Fax, Contacts, BillingCityName, fk_BillingStateID, fk_BillingZipID, fk_BillingCountryID, " +
                                    "SAP, Node, MainDistributor, PartsOnly, ManufacturerRep) " +
                                    "VALUES (" + nextBranchID + ",'" + p_name + "','" + p_BillingAddress + "','" + p_ShippingAddress +
                                    "','" + p_city + "','" + stateID +
                                    "','" + p_zip + "','" + countryID + "','" + p_phone + "','" + p_fax +
                                    "','" + p_repName + "','" + p_BillingCity + "','" + billingStateID + "','" + p_BZipID + "','" + billingCountryID + "','" + p_SAP + "','" + p_Node + "','" + IsMainDist + "','" + IsPartsOnly + "','" + isManufacturerRep + "')";

            DataLogic.DBA.DataLogic.Update(sql);

            returnVal = Convert.ToString(nextBranchID);
            return returnVal;
        }


        public static string UpdateBranch(int p_DistID, string p_ParentBranch, string p_name, string p_BillingAddress, string p_ShippingAddress,
                                                    string p_city, string p_country, string p_fax,
                                                    string p_phone, string p_state, string p_zip, string p_repName, string p_BillingCity, string p_BStateID,
                                                    string p_BZipID, string p_BCountryID, string p_SAP, string p_Node, int IsPartsOnly, int isManufacturerRep, int isMainDistributor)
        {
            string returnVal = "";
            string sql;

            DataLogic.DBA.DataLogic.PrepareSQL(ref p_name);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_BillingAddress);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_ShippingAddress);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_city);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_country);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_fax);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_phone);
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_state);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_zip);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_repName);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_ParentBranch);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_BillingCity);
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_BStateID);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_BZipID);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_BCountryID);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_Node);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_SAP);
            DataLogic.DBA.DataLogic.PrepareSQL(ref p_repName);

            int cityID, stateID, countryID, billingStateID, billingCountryID;
            //cityID = Location_da.GetCityID(p_city);
            stateID = Location_da.GetStateID(p_state);
            countryID = Location_da.GetCountryID(p_country);
            billingCountryID = Location_da.GetCountryID(p_BCountryID);
            billingStateID = Location_da.GetStateID(p_BStateID);

            int repID;
            //repID = Representative_da.GetRepID(p_repName);

            int p_ParentBranchID;

            string parentTemp;
            parentTemp = p_ParentBranch;
            parentTemp = parentTemp.Replace("'", "");

            if (parentTemp == "")
            {
                // do nothing, no changes
            }
            else
            {
                p_ParentBranchID = GetDistributorID(p_ParentBranch);
                // update the parent branch listing here
            }

            sql = "UPDATE Distributor SET " +
                    " DistName = '" + p_name + "', BillingAddress = '" + p_BillingAddress + "', ShippingAddress = '" + p_ShippingAddress + "', CityName = '" + p_city +
                    "' , fk_StateID = '" + stateID + "', fk_ZipID = '" + p_zip + "', " +
                    " fk_CountryID = '" + countryID + "', Phone = '" + p_phone + "', Fax = '" + p_fax +
                    "' , Contacts = '" + p_repName + "', BillingCityName = '" + p_BillingCity + "', fk_BillingStateID = '" + billingStateID + "', fk_BillingZipID = '" + p_BZipID + "', fk_BillingCountryID = '" + billingCountryID +
                    "' , SAP = '" + p_SAP + "', Node = '" + p_Node + "', PartsOnly = " + IsPartsOnly + ", ManufacturerRep = " + isManufacturerRep + " " +
                    " WHERE pk_DistributorID = " + p_DistID;

            DataLogic.DBA.DataLogic.Update(sql);

            sql = "SELECT fk_BranchDistID FROM DistributorBranch WHERE fk_MainDistID LIKE '" + p_DistID + "'";
            DataSet ds = DataLogic.DBA.DataLogic.Read(sql);

            string idlist = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (idlist != "")
                    idlist = idlist + ",";

                idlist = idlist + dr[0].ToString();

            }

            // if this distributor has branches, update their names as well
            if (idlist != "")
            {
                sql = "UPDATE DISTRIBUTOR SET DistName = '" + p_name + "' WHERE pk_DistributorID IN " +
                    "(" + idlist + ")";
                DataLogic.DBA.DataLogic.Update(sql);

                if (isMainDistributor == 1)
                {
                    sql = "UPDATE DISTRIBUTOR SET ManufacturerRep = " + isManufacturerRep + " WHERE pk_DistributorID IN " +
                        "(" + idlist + ")";
                    DataLogic.DBA.DataLogic.Update(sql);
                }
            }


            return returnVal;
        }



        public static void AddBranch(int p_MainID, int p_BranchID)
        {
            string returnVal = "";
            string sql;

            int nextID;

            nextID = DataLogic.DBA.DataLogic.GetNextID("DistributorBranch", "DistributorBranchID");

            sql = "INSERT INTO DistributorBranch VALUES(" + nextID + "," + p_MainID + "," + p_BranchID + ")";


            DataLogic.DBA.DataLogic.Update(sql);

        }

        public static void ClearEmailAddresses(int p_BranchID)
        {
            string sql;

            sql = "DELETE FROM DistributorEmail WHERE fk_DistributorID = " + p_BranchID;
            DataLogic.DBA.DataLogic.Update(sql);

        }



        public static void AddEmailAddress(int p_BranchID, string p_Email, string p_PersonName)
        {
            string sql;

            sql = "INSERT INTO DistributorEmail VALUES(" + p_BranchID + ",'" + p_Email + "','" + p_PersonName + "')";

            DataLogic.DBA.DataLogic.Update(sql);

        }

        public static DataSet GetEmailAddresses(int p_BranchID)
        {
            string sql;
            DataSet ds;

            sql = "SELECT Email, PersonName FROM DistributorEmail WHERE fk_DistributorID = " + p_BranchID;

            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }

        public static int ContactPrimary(int p_distID, string p_ContactName)
        {
            string sql;
            DataSet ds;

            sql = "SELECT Primary FROM Contacts WHERE fk_DistributorID = " + p_distID + " AND ContactName = '" + p_ContactName + "'";

            ds = DataLogic.DBA.DataLogic.Read(sql);

            int result;

            try
            {
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            catch
            {
                result = -1;
            }

            return result;
        }

        public static void ClearContacts(int p_distID)
        {
            string sql;
            sql = "DELETE FROM CONTACTS WHERE fk_DistributorID = " + p_distID;

            DataLogic.DBA.DataLogic.Update(sql);
        }

        public static void AddContact(int p_distID, string p_name, string p_title, int p_primary)
        {

            int nextContactID;
            nextContactID = DataLogic.DBA.DataLogic.GetNextID("Contacts", "pk_ContactID");

            string sql;
            sql = "INSERT INTO CONTACTS VALUES (" + nextContactID + "," + p_distID + ", '" + p_name + "', '" + p_title + "'," + p_primary + ")";

            DataLogic.DBA.DataLogic.Update(sql);
        }

        public static DataSet GetContacts(int p_BranchID)
        {
            string sql;
            DataSet ds;

            sql = "SELECT ContactTitle, ContactName, Primary FROM Contacts WHERE fk_DistributorID = " + p_BranchID;
            sql = sql + " ORDER BY Primary DESC, ContactName";

            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }

        public static DataSet GetPrimaryContact(int p_BranchID)
        {
            string sql;
            DataSet ds;

            sql = "SELECT ContactTitle, ContactName FROM Contacts WHERE fk_DistributorID = " + p_BranchID;
            sql = sql + " AND Primary = 1";

            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }


        public static int GetDistributorID(string p_DistName)
        {
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_DistName);

            string sql;
            sql = "SELECT pk_DistributorID FROM Distributor WHERE DistName = '" + p_DistName + "'";

            int id;
            id = Convert.ToInt32(DataLogic.DBA.DataLogic.Read(sql).Tables[0].Rows[0]["pk_DistributorID"]);

            return id;
        }

        public static int GetMainDistributorID(string p_DistName)
        {
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_DistName);

            string sql;
            sql = "SELECT pk_DistributorID FROM Distributor WHERE DistName = '" + p_DistName + "' AND MainDistributor = 1";

            int id;
            id = Convert.ToInt32(DataLogic.DBA.DataLogic.Read(sql).Tables[0].Rows[0]["pk_DistributorID"]);

            return id;
        }


        public static string GetDistributorName(int p_id)
        {

            string sql;
            sql = "SELECT DistName FROM Distributor WHERE pk_DistributorID = " + p_id;

            string distName;
            distName = Convert.ToString(DataLogic.DBA.DataLogic.Read(sql).Tables[0].Rows[0]["DistName"]);

            return distName;
        }

        public static DataSet GetDistributorBranchList(int p_MainDistID, string searchKey, int isManufacturerRep = -1)
        {

            DataSet ds = new DataSet();

            try
            {
                string sql;


                sql = "SELECT fk_BranchDistID, DistName FROM DistributorBranch, Distributor " +
                " WHERE CStr(pk_DistributorID) = fk_BranchDistID " +
                " AND fk_MainDistID = " + p_MainDistID.ToString();

                ds = DataLogic.DBA.DataLogic.Read(sql);

                int i;

                string distList = "";

                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (distList != "")
                    {
                        distList = distList + ",";  // add a new item
                    }

                    distList = distList + ds.Tables[0].Rows[i][0];

                }

                if (distList != "")
                {
                    //sql = "SELECT DistName as [BRANCH NAME], City.CityName as [CITY], State.Abbreviation as [STATE], Dist.Phone as [PHONE], Rep.RepName as [CONTACT], Dist.pk_DistributorID, SPACE(30) AS srvcrep, SPACE(30) AS terrrep " +
                    //        "FROM Distributor Dist, City, State, Representative Rep " +
                    //        "WHERE City.CityID = Dist.fk_CityID " +
                    //        "AND State.StateID = Dist.fk_StateID " +
                    //        "AND Rep.RepID = Dist.fk_RepresentativeID " +
                    //        "AND pk_DistributorID IN ( " + distList + ")";

                    //sql = "SELECT DistName, City.CityName, Distributor.Phone, " +
                    //        "Distributor.Fax, State.FullName, Country.CountryName, " +
                    //        "Representative.RepName, Distributor.fk_ZipID " +
                    //        "FROM Distributor, City, State, Country, Representative " +
                    //        "WHERE City.CityID = Distributor.fk_CityID " +
                    //        "AND State.StateID = Distributor.fk_StateID " +
                    //        "AND Country.CountryID = Distributor.fk_CountryID " +
                    //        "AND Representative.RepID = Distributor.fk_RepresentativeID " +
                    //        "AND pk_DistributorID IN ( " + distList + ")";

                    sql = "SELECT DistName as [BRANCH NAME], BillingAddress AS [ADDRESS], CityName as [CITY], State.Abbreviation as [STATE], Dist.Phone as [PHONE], Dist.pk_DistributorID, SPACE(30) AS srvcrep, SPACE(30) AS terrrep " +
                            " ,fk_ZipID, ShippingAddress, State.FullName " +
                            "FROM Distributor Dist, State " +
                            "WHERE State.StateID = Dist.fk_StateID " +
                            "AND pk_DistributorID IN ( " + distList + ") ";

                    if (isManufacturerRep > -1)
                    {
                        sql += "AND ManufacturerRep = " + isManufacturerRep;
                    }

                    sql += " ORDER BY Abbreviation, CityName";

                    if (searchKey != "")
                        sql = sql + " AND DistName LIKE '%" + searchKey + "%'";

                    ds = DataLogic.DBA.DataLogic.Read(sql);
                }
            }
            catch (Exception ex)
            {

            }

            return ds;
        }

        public static DataSet GetDistributorBranchListWithContractStates(int p_MainDistID, int categoryID, string stateListString = "")
        {

            DataSet ds = new DataSet();

            try
            {
                string sql;


                sql = " SELECT pk_DistributorID, Distname, s2.FullName, s2.Abbreviation, ShippingAddress, CityName, fk_ZipID, s1.FullName as ContractState" +
                        " FROM ContractCounty cc, ContractCategory cCat, ContractDistributor cDist, Distributor dist, State s1, County, State s2, DistributorBranch distBranch" +
                        " WHERE cc.fk_countyID IN (   SELECT DISTINCT CountyID " +
                        "                                    FROM County WHERE fk_StateID IN ( SELECT StateID FROM [State] WHERE FullName ";

                if (stateListString.Length > 0)
                    sql += " in ( " + stateListString + " ) ";
                else
                    sql += " = FullName ";

                sql += ") ) AND cCat.fk_CategoryID = " + categoryID +
                        " AND cCat.fk_contractID = cc.fk_contractID" +
                        " AND cCat.fk_contractID = cDist.fk_contractID" +
                        " AND dist.pk_DistributorID LIKE cDist.fk_DistributorID" +
                        " AND County.CountyID = cc.fk_countyID" +
                        " AND s1.StateID = County.fk_StateID" +
                        " AND s2.StateID = dist.fk_StateID" +
                        " AND Dist.pk_DistributorID LIKE distBranch.fk_BranchDistID" +
                        " AND distBranch.fk_MainDistID = " + p_MainDistID.ToString() +
                        " GROUP BY  pk_DistributorID, Distname, s2.FullName, s2.Abbreviation, ShippingAddress, CityName, fk_ZipID, s1.FullName ";

                ds = DataLogic.DBA.DataLogic.Read(sql);

            }
            catch (Exception ex)
            {

            }

            ds.Tables[0].Columns.Add("CountyName");

            return ds;
        }


        public static string GetManufacturerRepName(int manufacturerRepId)
        {
            string repName = string.Empty;

            try
            {
                string sql;

                sql = "SELECT * FROM Distributor WHERE pk_DistributorId = " +
                        Convert.ToString(manufacturerRepId);

                DataSet ds = DataLogic.DBA.DataLogic.Read(sql);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    repName = ds.Tables[0].Rows[0]["DistName"].ToString();
                }
            }
            catch (Exception ex)
            {

            }


            return repName;
        }


        public static DataSet GetManufacturerRepList(int p_MainDistID)
        {

            DataSet ds = new DataSet();

            try
            {
                string sql;

                sql = "SELECT fk_BranchDistID FROM DistributorBranch WHERE fk_MainDistID = " +
                        Convert.ToString(p_MainDistID);

                ds = DataLogic.DBA.DataLogic.Read(sql);

                int i;

                string distList = "";

                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (distList != "")
                    {
                        distList = distList + ",";  // add a new item
                    }

                    distList = distList + ds.Tables[0].Rows[i][0];

                }

                if (distList != "")
                {
                    sql = "SELECT pk_DistributorId, DistName as [MANUFACTURER REP NAME] " +
                            "FROM Distributor Dist, State " +
                            "WHERE State.StateID = Dist.fk_StateID " +
                            "AND pk_DistributorID IN ( " + distList + ") " +
                            "AND ManufacturerRep = 1 " +
                            " ORDER BY Abbreviation, CityName";

                    ds = DataLogic.DBA.DataLogic.Read(sql);
                }
            }
            catch (Exception ex)
            {

            }

            return ds;
        }


        public static DataSet GetDistributorBranchListByState(int p_MainDistID, string searchKey, string stateAbbreviation)
        {

            DataSet ds = new DataSet();

            try
            {
                string sql;

                sql = "SELECT fk_BranchDistID FROM DistributorBranch WHERE fk_MainDistID = " +
                        Convert.ToString(p_MainDistID);

                ds = DataLogic.DBA.DataLogic.Read(sql);

                int i;

                string distList = "";

                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (distList != "")
                    {
                        distList = distList + ",";  // add a new item
                    }

                    distList = distList + ds.Tables[0].Rows[i][0];

                }

                if (distList != "")
                {
                    //sql = "SELECT DistName as [BRANCH NAME], City.CityName as [CITY], State.Abbreviation as [STATE], Dist.Phone as [PHONE], Rep.RepName as [CONTACT], Dist.pk_DistributorID, SPACE(30) AS srvcrep, SPACE(30) AS terrrep " +
                    //        "FROM Distributor Dist, City, State, Representative Rep " +
                    //        "WHERE City.CityID = Dist.fk_CityID " +
                    //        "AND State.StateID = Dist.fk_StateID " +
                    //        "AND Rep.RepID = Dist.fk_RepresentativeID " +
                    //        "AND pk_DistributorID IN ( " + distList + ")";

                    //sql = "SELECT DistName, City.CityName, Distributor.Phone, " +
                    //        "Distributor.Fax, State.FullName, Country.CountryName, " +
                    //        "Representative.RepName, Distributor.fk_ZipID " +
                    //        "FROM Distributor, City, State, Country, Representative " +
                    //        "WHERE City.CityID = Distributor.fk_CityID " +
                    //        "AND State.StateID = Distributor.fk_StateID " +
                    //        "AND Country.CountryID = Distributor.fk_CountryID " +
                    //        "AND Representative.RepID = Distributor.fk_RepresentativeID " +
                    //        "AND pk_DistributorID IN ( " + distList + ")";


                    //            sql = "select Distributor.DistName as [Distributor Name], Distributor.CityName AS [Shipping City Name], Distributor.BillingCityName as [Billing City Name] , 
                    //TR.RepName as [Territory Rep], SR.RepName as [Service Rep] from distributor,distributorRepresentative dr,Representative TR, Representative SR " +
                    //" WHERE dr.fk_distributorid = distributor.pk_DistributorID " +
                    //" AND TR.RepID = dr.fk_TerritoryRepID" +
                    //" AND SR.RepID = dr.fk_ServiceRepID " +
                    //" GROUP BY DistName, Distributor.CityName, Distributor.BillingCityName, TR.RepName, SR.RepName";

                    sql = "SELECT DistName as [BRANCH NAME], BillingAddress AS [ADDRESS], CityName as [CITY], State.Abbreviation as [STATE], Dist.Phone as [PHONE], Dist.pk_DistributorID,SR.RepName AS srvcrep, TR.RepName AS terrrep " +
"FROM Distributor Dist, State, " +
"Representative TR, Representative SR, DistributorRepresentative DR " +
"WHERE State.StateID = Dist.fk_StateID  " +
                            "AND pk_DistributorID IN ( " + distList + ") " +
"AND TR.RepID = dr.fk_TerritoryRepID " +
"AND SR.RepID = dr.fk_ServiceRepID " +
"AND dr.fk_DistributorID = Dist.pk_DistributorID " +
"AND State.Abbreviation = '" + stateAbbreviation + "' ";

                    //sql = "SELECT DistName as [BRANCH NAME], BillingAddress AS [ADDRESS], CityName as [CITY], State.Abbreviation as [STATE], " +
                    //        "Dist.Phone as [PHONE], Dist.pk_DistributorID, SR.RepName AS srvcrep, TR.RepName AS terrrep " +
                    //        "FROM Distributor Dist, State, Representative,  Representative TR, Representative SR, distributorRepresentative dr  " +
                    //        "WHERE State.StateID = Dist.fk_StateID " +
                    //        "AND pk_DistributorID IN ( " + distList + ") " +
                    //        " AND dr.fk_distributorid = distributor.pk_DistributorID " +
                    //        " AND TR.RepID = dr.fk_TerritoryRepID " +
                    //        " AND SR.RepID = dr.fk_ServiceRepID " +
                    //        "AND State.Abbreviation = '" + stateAbbreviation + "' ";
                    if (searchKey != "")
                        sql = sql + " AND DistName LIKE '%" + searchKey + "%'";

                    sql = sql + " ORDER BY Abbreviation, CityName";







                    ds = DataLogic.DBA.DataLogic.Read(sql);
                }
            }
            catch (Exception ex)
            {

            }

            return ds;
        }

        public static DataSet GetContractCategoryList(int p_DistID)
        {
            string sql;
            sql = "SELECT DISTINCT Category.CategoryName FROM Contract, ContractDistributor, Category, ContractCategory" +
                    " WHERE ContractCategory.fk_CategoryID = Category.CategoryID " +
                    " AND ContractCategory.fk_contractID = Contract.ContractID " +
                    " AND ContractDistributor.fk_contractID = Contract.ContractID " +
                    " AND fk_DistributorID = " + p_DistID;

            DataSet ds;
            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }



    }
}
