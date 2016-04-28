using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;

namespace DDA.DataAccess
{
    class Contract_da
    {
        public static DataSet GetContractList(int p_id, bool showOverlaps = false)
        {
            DataSet ds = new DataSet();
            string sql;

            sql = "SELECT ContractNumber FROM Contract WHERE MainDistributorID = '" + p_id + "'";

            if (!showOverlaps)
            {
                sql += " AND Contract.ContractNumber NOT LIKE 'CO-%' ";
                sql += " AND Contract.ContractNumber NOT LIKE 'TO-%' ";
            }

            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }

        public static DataSet GetContractListByCategoryList(string categoryList)
        {
            DataSet ds = new DataSet();
            string sql;

            sql = "SELECT Distinct ContractCategory.fk_ContractID, fk_CategoryID, Contract.MainDistributorID FROM ContractCategory, Category, Contract ";
            sql += " WHERE ContractCategory.fk_CategoryID = Category.CategoryID";
            sql += " AND Contract.ContractID = ContractCategory.fk_ContractID";
            sql += " AND Category.CategoryName IN (" + categoryList + ")";
            sql += " GROUP BY ContractCategory.fk_ContractID, fk_CategoryID, Contract.MainDistributorID";
            sql += " ORDER BY Contract.MainDistributorID";

            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }

        public static DataSet GetContractListByCategoryListStateList(string categoryList, string stateList)
        {
            DataSet ds = new DataSet();
            string sql;

            sql = "SELECT ContractID, ContractNumber, ContractDate, ModifyDate, MainDistributorID,   IsManufacturerRepContract, CategoryID, " +
                    " CategoryName, AllowTerritoryOverlap, pk_DistributorID, Distname, CountyName, CountyID, s1.FullName, s1.Abbreviation, ShippingAddress, CityName, fk_ZipID, " +
                    " '' AS GroupStateName, '' AS GroupStateAbbreviation, Distributor.fk_StateID " +
                    " FROM Contract, County, ContractCounty, ContractCategory cc, Category cat, Distributor, State s1, State s2 " +
                    " WHERE Contract.ContractID = ContractCounty.fk_ContractID " +
                    " AND County.CountyID = ContractCounty.fk_CountyID " +
                    " AND County.CountyID IN ( " +
                            " SELECT DISTINCT CountyID FROM County WHERE fk_StateID IN ( SELECT StateID FROM [State] WHERE FullName in (" + stateList + ") ) " +
                    " ) " +
                    " AND cc.fk_ContractID = ContractID " +
                    " AND CategoryID = cc.fk_CategoryID " +
                    " AND CategoryID IN ( " + categoryList + " ) " +
                    " AND Distributor.pk_DistributorID LIKE MainDistributorID " +
                    " AND s1.StateID = County.fk_StateID " +
                    " GROUP BY ContractID, ContractNumber, ContractDate, ModifyDate, MainDistributorID,   IsManufacturerRepContract, CategoryID, CategoryName, AllowTerritoryOverlap, pk_DistributorID, " +
                    " Distname, CountyName, CountyID, s1.FullName, s1.Abbreviation, ShippingAddress, CityName, fk_ZipID, Distributor.fk_StateID ";

            ds = DataLogic.DBA.DataLogic.Read(sql);
            ds = FillMapReportStates(ds);

            return ds;
        }

        public static DataSet GetContractDataByContractNumberCategoryID(string contractNumber, int categoryID)
        {
            DataSet ds = new DataSet();
            string sql;

            sql = "SELECT ContractID, ContractNumber, ContractDate, ModifyDate, MainDistributorID,   IsManufacturerRepContract, CategoryID, " +
                    " CategoryName, AllowTerritoryOverlap, pk_DistributorID, Distname, CountyName, CountyID, s1.FullName, s1.Abbreviation, ShippingAddress, CityName, fk_ZipID, " +
                    " '' AS GroupStateName, '' AS GroupStateAbbreviation, Distributor.fk_StateID " +
                    " FROM Contract, County, ContractCounty, ContractCategory cc, Category cat, Distributor, State s1 " +
                    " WHERE Contract.ContractID = ContractCounty.fk_ContractID " +
                    " AND County.CountyID = ContractCounty.fk_CountyID " +
                    " AND cc.fk_ContractID = ContractID " +
                    " AND CategoryID = cc.fk_CategoryID " +
                    " AND CategoryID IN ( " + categoryID + " ) " +
                    " AND Contract.ContractNumber LIKE '" + contractNumber + "' " +
                    " AND Distributor.pk_DistributorID LIKE MainDistributorID " +
                    " AND s1.StateID = County.fk_StateID " +
                    " GROUP BY ContractID, ContractNumber, ContractDate, ModifyDate, MainDistributorID,   IsManufacturerRepContract, CategoryID, CategoryName, AllowTerritoryOverlap, pk_DistributorID, " +
                    " Distname, CountyName, CountyID, s1.FullName, s1.Abbreviation, ShippingAddress, CityName, fk_ZipID, Distributor.fk_StateID ";

            ds = DataLogic.DBA.DataLogic.Read(sql);
            ds = FillMapReportStates(ds);

            return ds;
        }

        public static DataSet FillMapReportStates(DataSet ds)
        {
            DataSet dsStates = DDA.DataAccess.Location_da.GetStateList();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                foreach (DataRow drState in dsStates.Tables[0].Rows)
                {
                    if (drState["StateID"].ToString() == dr["fk_StateID"].ToString())
                    {
                        dr["GroupStateName"] = drState["FullName"].ToString();
                        dr["GroupStateAbbreviation"] = drState["Abbreviation"].ToString();

                        break;
                    }
                }
            }

            return ds;
        }

        public static int GetContractCount(bool includeChildContracts = true)
        {
            DataSet ds = new DataSet();
            string sql;

            sql = "SELECT Count(ContractNumber) FROM Contract";

            if (includeChildContracts == false)
            {
                sql += " WHERE Contract.ContractNumber NOT LIKE 'CO-%' ";
                sql += " AND Contract.ContractNumber NOT LIKE 'TO-%' ";
            }

            ds = DataLogic.DBA.DataLogic.Read(sql);

            int returnCount;

            returnCount = (Int32)ds.Tables[0].Rows[0][0];
            return returnCount;
        }


        public static int GetChildContractId(string parentContractNumber)
        {
            DataSet ds = new DataSet();
            string sql;

            sql = "SELECT ContractID FROM Contract WHERE ParentContract = '" + parentContractNumber + "'";

            ds = DataLogic.DBA.DataLogic.Read(sql);

            int childContractId = -1;

            if (ds.Tables[0].Rows.Count > 0)
                childContractId = (Int32)ds.Tables[0].Rows[0][0];

            return childContractId;
        }


        public static void DeleteContract(int p_id)
        {
            // Contract, ContractDistributor, ContractCategory, ContractCounty
            string sql;

            sql = "SELECT ContractID FROM Contract WHERE ParentContract LIKE (SELECT ContractNumber FROM Contract WHERE ContractID = " + p_id.ToString() + ")";
            DataSet ds = new DataSet();
            ds = DataLogic.DBA.DataLogic.Read(sql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int coContractID = Convert.ToInt32(dr["ContractID"]);
                    DeleteContractPrivate(coContractID);
                }

            }

            DeleteContractPrivate(p_id);

            // update territoryOverlapContract
            string territoryOverlapResult = UpdateTerritoryOverlapContracts(DDA.DataObjects.AppData.DistributorID);

        }

        private static void DeleteContractPrivate(int contractID)
        {
            string sql;

            string contractNumber = DDA.DataAccess.Contract_da.GetContractNumber(contractID);
            if (contractNumber.Length > 0)
            {
                DataSet dsStates = DDA.DataAccess.Contract_da.GetContractStates(contractNumber);
                DataSet dsCategories = DDA.DataAccess.Contract_da.GetContractCategories(contractID, true);

                foreach (DataRow drState in dsStates.Tables[0].Rows)
                {
                    foreach (DataRow drCat in dsCategories.Tables[0].Rows)
                    {
                        int stateID = DDA.DataAccess.Location_da.GetStateID(drState["FullName"].ToString());
                        int categoryID = Convert.ToInt32(drCat["CategoryID"].ToString());

                        DDA.DataAccess.MapReport_da.UpdateMapReport(stateID, categoryID, false);
                    }
                }


                sql = "DELETE FROM ContractDistributor WHERE fk_ContractID = " + contractID;
                DataLogic.DBA.DataLogic.Update(sql);

                sql = "DELETE FROM ContractCategory WHERE fk_ContractID = " + contractID;
                DataLogic.DBA.DataLogic.Update(sql);

                sql = "DELETE FROM ContractCounty WHERE fk_ContractID = " + contractID;
                DataLogic.DBA.DataLogic.Update(sql);

                sql = "DELETE FROM Contract WHERE ContractID = " + contractID;
                DataLogic.DBA.DataLogic.Update(sql);
            }

        }

        public static void GetContract(string p_contractNumber)
        {

            int contractID;

            contractID = GetContractID(p_contractNumber);

            GetContract(contractID);
        }

        public static void GetContract(int contractID)
        {

            //            DataLogic.DBA.DataLogic.PrepareSQL(ref p_contractNumber);

            DataSet ds = new DataSet();
            string sql;

            sql = "SELECT contractID, contractNumber, contractDate, ModifyDate, MainDistributorID, IsManufacturerRepContract, ParentContract, IsAuto " +
                    "FROM contract " +
                    "WHERE contractID = " + contractID;

            ds = DataLogic.DBA.DataLogic.Read(sql);

            DDA.DataObjects.AppData.CurrentContract.ContractID = Convert.ToInt32(ds.Tables[0].Rows[0]["contractID"]);
            DDA.DataObjects.AppData.CurrentContract.ContractNumber = Convert.ToString(ds.Tables[0].Rows[0]["contractNumber"]);
            DDA.DataObjects.AppData.CurrentContract.ContractDate = Convert.ToString(ds.Tables[0].Rows[0]["contractDate"]);
            DDA.DataObjects.AppData.CurrentContract.ModifyDate = Convert.ToString(ds.Tables[0].Rows[0]["modifyDate"]);
            DDA.DataObjects.AppData.DistributorID = Convert.ToInt32(ds.Tables[0].Rows[0]["MainDistributorID"]);
            DDA.DataObjects.AppData.IsManufacturerRep = Convert.ToInt32(ds.Tables[0].Rows[0]["IsManufacturerRepContract"]);
            DDA.DataObjects.AppData.CurrentContract.ParentContractNumber = ds.Tables[0].Rows[0]["ParentContract"].ToString();

            if (ds.Tables[0].Rows[0]["IsAuto"].ToString() == "True")
                DDA.DataObjects.AppData.CurrentContract.IsAuto = true;
            else
                DDA.DataObjects.AppData.CurrentContract.IsAuto = false;

            int i;


            sql = "SELECT * FROM ContractCategory WHERE fk_contractID = " + contractID;
            ds = DataLogic.DBA.DataLogic.Read(sql);

            DDA.DataObjects.AppData.CurrentContract.ClearCounties();
            DDA.DataObjects.AppData.CurrentContract.ClearDeleteCounties();
            DDA.DataObjects.AppData.CurrentContract.ClearBranches();
            DDA.DataObjects.AppData.CurrentContract.ClearDeleteBranches();
            DDA.DataObjects.AppData.CurrentContract.ClearStates();
            DDA.DataObjects.AppData.CurrentContract.ClearSalesLocations();

            DDA.DataObjects.AppData.CurrentContract.ClearCategories();
            string categoryIds = "";
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (categoryIds.Length > 0)
                    categoryIds = categoryIds + ",";

                categoryIds += ds.Tables[0].Rows[i]["fk_categoryID"].ToString();

                DDA.DataObjects.AppData.CurrentContract.Categories.Add(ds.Tables[0].Rows[i]["fk_categoryID"]);
            }


            sql = "SELECT * FROM ContractCounty WHERE fk_contractID = " + contractID;
            sql += " AND fk_CountyID > -1 ";

            ds = DataLogic.DBA.DataLogic.Read(sql);

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DDA.DataObjects.AppData.CurrentContract.Counties.Add(ds.Tables[0].Rows[i]["fk_countyID"]);
            }

            sql = "SELECT * FROM ContractDistributor INNER JOIN Distributor on ContractDistributor.fk_DistributorId = Distributor.pk_DistributorId ";
            sql += " WHERE ManufacturerRep = 0 AND fk_contractID = " + contractID;

            ds = DataLogic.DBA.DataLogic.Read(sql);

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DDA.DataObjects.AppData.CurrentContract.Branches.Add(ds.Tables[0].Rows[i]["fk_distributorID"]);
            }

            sql = "SELECT * FROM Category WHERE CategoryId IN (" + categoryIds + ") ORDER BY Ordinal ASC";
            ds = DataLogic.DBA.DataLogic.Read(sql);

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];

                if (dr["AllowTerritoryOverlap"].ToString() == "True")
                    DDA.DataObjects.AppData.CurrentContract.IsTerritoryOverlap = true;

                if (dr["AllowCountyOverlap"].ToString() == "True")
                    DDA.DataObjects.AppData.CurrentContract.IsCountyOverlap = true;

            }

            //sql = "SELECT * FROM ContractDistributor INNER JOIN Distributor on ContractDistributor.fk_DistributorId = Distributor.pk_DistributorId ";
            //sql += " WHERE ManufacturerRep = 1 AND fk_contractID = " + contractID;

            //ds = DataLogic.DBA.DataLogic.Read(sql);

            //int manuRep = 0;

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    manuRep = Convert.ToInt32(ds.Tables[0].Rows[0]["fk_distributorID"]);
            //}

            //DDA.DataObjects.AppData.IsManufacturerRep = manuRep;


        }
        public static int IsManufacturerRep(int contractId)
        {

            //            DataLogic.DBA.DataLogic.PrepareSQL(ref p_contractNumber);

            DataSet ds = new DataSet();
            string sql;

            sql = "SELECT * FROM ContractDistributor INNER JOIN Distributor on ContractDistributor.fk_DistributorId = Distributor.pk_DistributorId ";
            sql += " WHERE ManufacturerRep = 1 AND fk_contractID = " + contractId;

            ds = DataLogic.DBA.DataLogic.Read(sql);

            int manuRep = 0;

            if (ds.Tables[0].Rows.Count > 0)
            {
                manuRep = 1;
            }

            return manuRep;


        }

        public static DataSet GetContractCategories(string p_contractNumber, bool includeOverlapCounties = false)
        {
            int contractID = DDA.DataAccess.Contract_da.GetContractID(p_contractNumber);
            return GetContractCategories(contractID, includeOverlapCounties);
        }

        public static DataSet GetContractCategories(int contractID, bool includeOverlapCounties = false)
        {
            DataSet ds;
            string sql;

            int distributorId = DDA.DataAccess.Contract_da.GetMainDistributorID(contractID);

            DataSet dsOverlapContractData = GetTerritoryOverlapContractData(distributorId);

            sql = "SELECT DISTINCT CategoryName, CategoryID, Ordinal FROM Category, ContractCategory " +
                    " WHERE ( ContractCategory.fk_CategoryID = Category.CategoryID" +
                    " AND ContractCategory.fk_ContractID = " + contractID + ")";

            string overlapCategories = "";

            foreach (DataRow dr in dsOverlapContractData.Tables[0].Rows)
            {
                if (overlapCategories.Length > 0)
                    overlapCategories += ",";

                overlapCategories += "'" + dr["CategoryName"].ToString() + "'";
            }

            if (includeOverlapCounties && overlapCategories.Length > 0)
            {
                sql += " OR ";
                sql += " Category.CategoryName IN ( " + overlapCategories + " )";  // Get overlap county contracts...need to get distributor id?
            }

            sql += " GROUP BY CategoryName, CategoryID, Ordinal ";
            sql += " ORDER BY Ordinal";

            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }

        public static DataSet GetContractStates(string p_contractNumber)
        {
            DataSet ds;
            string sql;

            int contractID;

            contractID = DDA.DataAccess.Contract_da.GetContractID(p_contractNumber);

            sql = "SELECT DISTINCT Abbreviation, FullName " +
                    " FROM State, ContractCounty, County" +
                    " WHERE ContractCounty.fk_countyID = County.CountyID" +
                    " AND County.fk_StateID = State.StateID" +
                    " AND ContractCounty.fk_contractID = " + contractID;

            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }


        public static DataSet GetCounties(int contractID)
        {
            DataSet dsReturn;
            string sql;
            string cats;
            cats = "";

            int i;

            sql = "SELECT DISTINCT CountyID, CountyName FROM ContractCounty, County " +
                    " WHERE ContractCounty.fk_contractID = County.CountyID " +
                    " AND fk_contractID = " + contractID.ToString();

            dsReturn = DataLogic.DBA.DataLogic.Read(sql);

            return dsReturn;
        }

        public static DataSet GetCountiesForOverlap(int p_state, int distributorId)
        {
            DataSet dsReturn;
            string sql;
            string cats;
            cats = "";

            int i;

            sql = "SELECT fk_ContractID FROM ContractCategory ";
            sql += " INNER JOIN Contract ON Contract.ContractID = ContractCategory.fk_ContractID ";
            sql += " WHERE Contract.MainDistributorID LIKE " + distributorId;

            dsReturn = DataLogic.DBA.DataLogic.Read(sql);

            string contractIDs;
            contractIDs = "";

            for (i = 0; i < dsReturn.Tables[0].Rows.Count; i++)
            {
                if (contractIDs != "")
                    contractIDs = contractIDs + ",";

                contractIDs = contractIDs + dsReturn.Tables[0].Rows[i]["fk_ContractID"];
            }

            sql = "SELECT DISTINCT(CountyName) AS CountyName, ContractCounty.fk_ContractID FROM County, ContractCounty, State " +
                    "WHERE ContractCounty.fk_CountyID = County.CountyID " +
                    "AND State.StateID = County.fk_StateID ";

            if (contractIDs != "")
            {
                sql = sql + "AND ContractCounty.fk_ContractID IN (" + contractIDs + ") ";
            }
            else
            {
                sql = sql + "AND ContractCounty.fk_ContractID IN (-1) ";  // Account for no categories having been saved previously
            }

            sql = sql + " GROUP BY CountyName, fk_ContractID";

            dsReturn = DataLogic.DBA.DataLogic.Read(sql);

            return dsReturn;
        }



        public static DataSet GetOccupiedCounties(int p_state, ArrayList p_categories, int contractID, ArrayList p_deleteCounties, int isManufacturerRep)
        {
            DataSet dsReturn;
            string sql;
            string cats;
            cats = "";

            int i;

            for (i = 0; i < p_categories.Count; i++)
            {
                if (cats != "")
                {
                    cats = cats + ",";
                }

                cats = cats + p_categories[i].ToString();
            }

            string delCounties;
            delCounties = "";

            for (i = 0; i < p_deleteCounties.Count; i++)
            {
                if (delCounties != "")
                    delCounties = delCounties + ",";

                delCounties = delCounties + p_deleteCounties[i].ToString();
            }

            sql = "SELECT fk_ContractID FROM ContractCategory ";
            sql += " INNER JOIN Contract ON Contract.ContractID = ContractCategory.fk_ContractID ";
            sql += " WHERE fk_CategoryID IN (" + cats + ")";
            sql += " AND IsManufacturerRepContract = " + isManufacturerRep;

            dsReturn = DataLogic.DBA.DataLogic.Read(sql);

            string contractIDs;
            contractIDs = "";

            for (i = 0; i < dsReturn.Tables[0].Rows.Count; i++)
            {
                if (contractIDs != "")
                    contractIDs = contractIDs + ",";

                contractIDs = contractIDs + dsReturn.Tables[0].Rows[i]["fk_ContractID"];
            }

            //            int stateID;

            //          stateID = DDA.DataAccess.Location_da.GetStateID(p_state);

            //sql = "SELECT DISTINCT(CountyName) FROM County, ContractCategory, ContractCounty " +
            //"WHERE ContractCounty.fk_CountyID = County.CountyID " +
            //"AND ContractCounty.fk_CountyID IN (SELECT CountyID FROM County, ContractCounty " +
            //    "WHERE County.CountyID = ContractCounty.fk_CountyID AND County.fk_StateID = " + p_state + ")" +
            //"AND ContractCategory.fk_CategoryID IN (" + cats + ") ";

            sql = "SELECT DISTINCT(CountyName), ContractCounty.fk_ContractID FROM County, ContractCounty, State " +
                    "WHERE ContractCounty.fk_CountyID = County.CountyID " +
                    "AND State.StateID = County.fk_StateID ";

            if (contractIDs != "")
            {
                sql = sql + "AND ContractCounty.fk_ContractID IN (" + contractIDs + ") ";
            }
            else
            {
                sql = sql + "AND ContractCounty.fk_ContractID IN (-1) ";  // Account for no categories having been saved previously
            }

            if (delCounties != "")
                sql = sql + "AND County.CountyID NOT IN (" + delCounties + ") ";

            if (DDA.DataObjects.AppData.CurrentContract.ContractID > -1)
            {
                //sql = sql + "AND ContractCategory.fk_ContractID NOT IN (" + contractID + ") " +
                //"AND ContractCounty.fk_ContractID NOT IN (" + contractID + ")";
                sql = sql + "AND ContractCounty.fk_ContractID NOT IN (" + contractID + ")";
            }

            sql = sql + " AND State.StateID = " + p_state.ToString();
            sql = sql + " GROUP BY CountyName, fk_ContractID";

            dsReturn = DataLogic.DBA.DataLogic.Read(sql);

            return dsReturn;
        }

        public static DataSet GetDistributorContract(int p_branchID)
        {
            string sql;
            DataSet dsReturn;

            string cats;
            cats = "";
            int i;

            for (i = 0; i < DDA.DataObjects.AppData.CurrentContract.Categories.Count; i++)
            {
                if (cats != "")
                    cats = cats + ",";

                cats = cats + DDA.DataObjects.AppData.CurrentContract.Categories[i].ToString();
            }

            sql = "SELECT * FROM ContractCategory WHERE fk_ContractID IN " +
                    "(SELECT fk_ContractID FROM ContractDistributor WHERE fk_DistributorID = " + p_branchID + ") ";


            if (cats != "")
                sql = sql + "AND fk_CategoryID IN (" + cats + ")";


            dsReturn = DataLogic.DBA.DataLogic.Read(sql);
            return dsReturn;
        }

        public static int GetContractID(string p_number)
        {
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_number);

            string sql;
            int p_id = -1;
            DataSet ds = new DataSet();

            sql = "SELECT contractID FROM contract WHERE contractNumber = '" + p_number + "'";

            ds = DataLogic.DBA.DataLogic.Read(sql);
            
            if (ds.Tables[0].Rows.Count > 0)
                p_id = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

            return p_id;

        }

        public static string GetContractModifiedDate(string p_number)
        {
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_number);

            string sql;
            string p_id;
            DataSet ds = new DataSet();

            sql = "SELECT ModifyDate FROM contract WHERE contractNumber = '" + p_number + "'";

            ds = DataLogic.DBA.DataLogic.Read(sql);

            p_id = ds.Tables[0].Rows[0][0].ToString();

            return p_id;

        }

        public static string GetContractNumber(int p_id)
        {
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_number);

            string sql;
            string contractNumber = string.Empty;
            DataSet ds = new DataSet();

            sql = "SELECT contractNumber FROM contract WHERE contractID = " + p_id;

            ds = DataLogic.DBA.DataLogic.Read(sql);
            
            if (ds.Tables[0].Rows.Count > 0)
                contractNumber = ds.Tables[0].Rows[0][0].ToString();

            return contractNumber;

        }


        public static int GetCountyContractCount(int p_id)
        {
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_number);

            string sql;
            int count;
            DataSet ds = new DataSet();

            sql = "SELECT COUNT(fk_contractID) FROM ContractCounty WHERE fk_countyID = " + p_id;

            ds = DataLogic.DBA.DataLogic.Read(sql);

            count = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

            return count;

        }

        public static void DeleteCountyFromContract(int p_id)
        {

            string sql;

            sql = "DELETE FROM ContractCounty WHERE fk_countyID = " + p_id;
            DataLogic.DBA.DataLogic.Update(sql);

            sql = "DELETE FROM County WHERE CountyID = " + p_id;
            DataLogic.DBA.DataLogic.Update(sql);

        }

        //SELECT DistName FROM Contract, Distributor WHERE Contract.MainDistributorID LIKE Distributor.pk_DistributorID and Contract.ContractID = 6



        public static string GetContractDistributor2(int p_id)
        {
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_number);

            string sql;
            string distName;
            DataSet ds = new DataSet();

            sql = "SELECT DistName FROM Contract, Distributor WHERE Contract.MainDistributorID LIKE Distributor.pk_DistributorID and Contract.ContractID = " + p_id;

            ds = DataLogic.DBA.DataLogic.Read(sql);

            distName = ds.Tables[0].Rows[0][0].ToString();

            return distName;

        }

        public static string GetContractDistributor(int p_id)
        {
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_number);

            string sql;
            string distName;
            DataSet ds = new DataSet();

            sql = "SELECT DistName FROM ContractDistributor, Distributor WHERE Distributor.pk_DistributorID = ContractDistributor.fk_DistributorID AND fk_contractID = " + p_id;

            ds = DataLogic.DBA.DataLogic.Read(sql);

            distName = ds.Tables[0].Rows[0][0].ToString();

            return distName;

        }

        public static int GetMainDistributorID(int p_id)
        {
            string sql;
            DataSet ds = new DataSet();

            sql = "SELECT MainDistributorID FROM Contract WHERE Contract.ContractID = " + p_id;

            ds = DataLogic.DBA.DataLogic.Read(sql);

            return Convert.ToInt32(ds.Tables[0].Rows[0]["MainDistributorID"].ToString());
        }

        public static DataSet GetContractDistributorDataset(int p_id)
        {
            //DataLogic.DBA.DataLogic.PrepareSQL(ref p_number);

            string sql;
            DataSet ds = new DataSet();

            sql = "SELECT * FROM Contract, ContractDistributor, Distributor WHERE Contract.ContractID = ContractDistributor.fk_ContractID AND  Distributor.pk_DistributorID = ContractDistributor.fk_DistributorID AND fk_contractID = " + p_id;

            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;

        }
        
        public static void DeleteCounties_byState_ContractID(int p_state, int p_contractID)
        {
            string sql;

            sql = "SELECT CountyID FROM County WHERE fk_StateID = " + p_state;

            DataSet ds;
            ds = DataLogic.DBA.DataLogic.Read(sql);

            int i;
            int j;
            ArrayList removeList = new ArrayList();


            for (j = 0; j < DDA.DataObjects.AppData.CurrentContract.Counties.Count; j++)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    // if they match, remove the county from the list
                    if (ds.Tables[0].Rows[i]["CountyID"].ToString() == DDA.DataObjects.AppData.CurrentContract.Counties[j].ToString())
                    {
                        removeList.Add(Convert.ToInt32(DDA.DataObjects.AppData.CurrentContract.Counties[j].ToString()));

                    }
                }
            }

            for (i = 0; i < removeList.Count; i++)
            {
                DDA.DataObjects.AppData.CurrentContract.RemoveCounty(Convert.ToInt32(removeList[i].ToString()));
            }

        }

        public static bool CloneContractCounties(string contractNumber)
        {
            bool result = true;


            try
            {
                string sql;

                sql = "SELECT DISTINCT fk_CountyID FROM ContractCounty, Contract  WHERE Contract.ContractNumber = '" + contractNumber.ToString() + "' AND Contract.ContractID LIKE ContractCounty.fk_contractID ";

                //sql = "SELECT fk_ContractID FROM ContractCategory WHERE fk_CategoryID IN (" + catList + ")";

                DataSet dsContractCounties = DataLogic.DBA.DataLogic.Read(sql);

                foreach (DataRow dr in dsContractCounties.Tables[0].Rows)
                {
                    int countyID = Convert.ToInt32(dr["fk_CountyID"]);
                    DDA.DataObjects.AppData.CurrentContract.Counties.Add(countyID);
                    DDA.DataObjects.AppData.CurrentContract.ModifiedCounties.Add(countyID);
                }

            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }

            return result;
        }


        public static bool CloneContractSalesLocations(string contractNumber)
        {
            bool result = true;


            try
            {
                string sql;

                sql = "SELECT DISTINCT fk_DistributorID FROM ContractDistributor, Contract  WHERE Contract.ContractNumber = '" + contractNumber.ToString() + "' AND Contract.ContractID LIKE ContractDistributor.fk_contractID ";

                //sql = "SELECT fk_ContractID FROM ContractCategory WHERE fk_CategoryID IN (" + catList + ")";

                DataSet dsContractCounties = DataLogic.DBA.DataLogic.Read(sql);

                foreach (DataRow dr in dsContractCounties.Tables[0].Rows)
                    DDA.DataObjects.AppData.CurrentContract.Branches.Add(Convert.ToInt32(dr["fk_DistributorID"]));

            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }

            return result;
        }

        public static bool CloneDistributorCounties(int distributorID)
        {
            bool result = true;


            try
            {
                string sql;
                sql = "SELECT DISTINCT fk_CountyID FROM ContractCounty ";
                sql += " INNER JOIN Contract ON Contract.ContractID = ContractCounty.fk_ContractID ";
                sql += " WHERE Contract.MainDistributorID LIKE " + distributorID.ToString();

                //sql = "SELECT fk_ContractID FROM ContractCategory WHERE fk_CategoryID IN (" + catList + ")";

                DataSet dsParentCounties = DataLogic.DBA.DataLogic.Read(sql);

                foreach (DataRow dr in dsParentCounties.Tables[0].Rows)
                {
                    int countyID = Convert.ToInt32(dr["fk_CountyID"]);
                    DDA.DataObjects.AppData.CurrentContract.Counties.Add(countyID);
                    DDA.DataObjects.AppData.CurrentContract.ModifiedCounties.Add(countyID);
                }

            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }

            return result;
        }


        public static bool CloneDistributorSalesLocations(int distributorID)
        {
            bool result = true;


            try
            {
                string sql;
                sql = "SELECT DISTINCT fk_DistributorID FROM ContractDistributor ";
                sql += " INNER JOIN Contract ON Contract.ContractID = ContractDistributor.fk_ContractID ";
                sql += " WHERE Contract.MainDistributorID LIKE " + distributorID.ToString();

                //sql = "SELECT fk_ContractID FROM ContractCategory WHERE fk_CategoryID IN (" + catList + ")";

                DataSet dsParentDistributors = DataLogic.DBA.DataLogic.Read(sql);

                foreach (DataRow dr in dsParentDistributors.Tables[0].Rows)
                    DDA.DataObjects.AppData.CurrentContract.Branches.Add(Convert.ToInt32(dr["fk_DistributorID"]));

            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }

            return result;
        }

        public static string UpdateContract()
        {
            string contractDate;
            string modifyDate;
            int contractID;


            contractID = DDA.DataObjects.AppData.CurrentContract.ContractID;
            modifyDate = DDA.DataObjects.AppData.CurrentContract.ModifyDate;

            int i;
            string sql;

            DeleteOldData();

            // do a delete first, then an insert
            sql = "DELETE FROM ContractCategory WHERE fk_ContractID = " + contractID + "";
            DataLogic.DBA.DataLogic.Update(sql);

            for (i = 0; i < DDA.DataObjects.AppData.CurrentContract.Categories.Count; i++)
            {

                // Do the insert
                sql = "INSERT INTO ContractCategory VALUES ('" + DDA.DataObjects.AppData.CurrentContract.Categories[i].ToString() + "','" + contractID + "')";
                DataLogic.DBA.DataLogic.Update(sql);
            }

            // If has child contract (county overlap (auto)), then get child contract id and update with same counties
            // If distributor has any territory overlap (auto), then re-calculate it's that territory overlap's counties after update
            UpdateCounties(contractID);
            int childContractID = GetChildContractId(DDA.DataObjects.AppData.CurrentContract.ContractNumber);

            if (childContractID > -1)
                UpdateCounties(childContractID);

            UpdateSalesLocations(contractID);

            if (childContractID > -1)
                UpdateSalesLocations(childContractID);
            
            // update territoryOverlapContract
            string territoryOverlapResult = "";

            territoryOverlapResult = UpdateTerritoryOverlapContracts(DDA.DataObjects.AppData.DistributorID);

            contractDate = DDA.DataObjects.AppData.CurrentContract.ContractDate;
            int autoValue = 0;

            if (DDA.DataObjects.AppData.CurrentContract.IsAuto)
                autoValue = 1;

            sql = "Update Contract SET ModifyDate = '" + modifyDate + "', ContractDate = '" + contractDate + "', IsManufacturerRepContract = '" + DDA.DataObjects.AppData.IsManufacturerRep + "', IsAuto = " + autoValue.ToString() +
                    " WHERE ContractID = " + contractID;

            DataLogic.DBA.DataLogic.Update(sql);

            DataObjects.MapReport.UpdateMapReportStatus();

            DDA.DataObjects.AppData.CurrentContract.ClearBranches();
            DDA.DataObjects.AppData.CurrentContract.ClearCounties();
            DDA.DataObjects.AppData.CurrentContract.ClearStates();
            DDA.DataObjects.AppData.CurrentContract.ClearDeleteBranches();
            DDA.DataObjects.AppData.CurrentContract.ClearDeleteCounties();

            return territoryOverlapResult;
        }

        private static void UpdateCounties(int contractID)
        {
            int i;
            string sql = "";

            // do a delete first, then an insert
            sql = "DELETE FROM ContractCounty WHERE fk_ContractID = " + contractID + "";
            DataLogic.DBA.DataLogic.Update(sql);
            try
            {

                for (i = 0; i < DDA.DataObjects.AppData.CurrentContract.Counties.Count; i++)
                {
                    // Do the insert
                    sql = "INSERT INTO ContractCounty VALUES ('" + DDA.DataObjects.AppData.CurrentContract.Counties[i].ToString() + "','" + contractID + "')";
                    DataLogic.DBA.DataLogic.Update(sql);

                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            sql = "UPDATE Contract SET ModifyDate = '" + DDA.DataObjects.AppData.CurrentContract.ModifyDate + "' WHERE ContractID = " + contractID;
            DataLogic.DBA.DataLogic.Update(sql);
        }

        private static void UpdateSalesLocations(int contractID)
        {
            int i;
            string sql = "";

            // do a delete first, then an insert
            sql = "DELETE FROM ContractDistributor WHERE fk_ContractID = " + contractID + "";
            DataLogic.DBA.DataLogic.Update(sql);

            for (i = 0; i < DDA.DataObjects.AppData.CurrentContract.Branches.Count; i++)
            {
                // Insert new values
                sql = "INSERT INTO ContractDistributor VALUES ('" + DDA.DataObjects.AppData.CurrentContract.Branches[i].ToString() + "','" + contractID + "')";
                DataLogic.DBA.DataLogic.Update(sql);
            }

            sql = "UPDATE Contract SET ModifyDate = '" + DDA.DataObjects.AppData.CurrentContract.ModifyDate + "' WHERE ContractID = " + contractID;
            DataLogic.DBA.DataLogic.Update(sql);
        }

        public static DataSet GetOverlapContractIdList(int distributorId)
        {
            DataSet ds = new DataSet();
            string sql;

            sql = "SELECT Contract.ContractID, ContractNumber FROM Contract, ContractCategory, Category ";
            sql += " WHERE ContractCategory.fk_ContractID = Contract.ContractID ";
            sql += " AND Category.CategoryID = ContractCategory.fk_categoryID ";
            sql += " AND Contract.MainDistributorID = '" + distributorId + "'";
            sql += " AND (Category.AllowCountyOverlap = true OR Category.AllowTerritoryOverlap = true)";

            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }


        public static DataSet GetTerritoryOverlapContractIdList(int distributorId)
        {
            DataSet ds = new DataSet();
            string sql;

            sql = "SELECT Contract.ContractID, ContractNumber, IsAuto FROM Contract, ContractCategory, Category ";
            sql += " WHERE ContractCategory.fk_ContractID = Contract.ContractID ";
            sql += " AND Category.CategoryID = ContractCategory.fk_categoryID ";
            sql += " AND Contract.MainDistributorID = '" + distributorId + "'";
            sql += " AND Category.AllowTerritoryOverlap = true";
            sql += " AND ContractNumber LIKE 'TO-%'";

            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }


        public static DataSet GetTerritoryOverlapContractData(int distributorId)
        {
            DataSet ds = new DataSet();
            string sql;

            sql = "SELECT Contract.ContractID, ContractNumber, Category.CategoryName FROM Contract, ContractCategory, Category ";
            sql += " WHERE ContractCategory.fk_ContractID = Contract.ContractID ";
            sql += " AND Category.CategoryID = ContractCategory.fk_categoryID ";
            sql += " AND Contract.MainDistributorID = '" + distributorId + "'";
            sql += " AND Category.AllowTerritoryOverlap = true";

            ds = DataLogic.DBA.DataLogic.Read(sql);

            return ds;
        }

        public static string UpdateTerritoryOverlapContracts(int mainDistributorID)
        {
            DataSet ds = new DataSet();
            string sql;
            string result = "";
            string resultContracts = "";
            string resultDistributorContracts = "";

            DataSet dsContracts = GetTerritoryOverlapContractIdList(DDA.DataObjects.AppData.DistributorID);

            if (dsContracts.Tables[0].Rows.Count > 0)
            {
                string territoryOverlapContractIdList = "";

                foreach (DataRow dr in dsContracts.Tables[0].Rows)
                {
                    if (territoryOverlapContractIdList.Length > 0)
                        territoryOverlapContractIdList += ",";

                    if (dr["IsAuto"].ToString() == "True")
                        territoryOverlapContractIdList += dr["ContractID"].ToString();
                }

                if (territoryOverlapContractIdList.Length == 0)
                    territoryOverlapContractIdList = "999999999";

                sql = "SELECT DISTINCT CountyID, ContractNumber FROM County, ContractCounty, Contract, ContractCategory, Category  ";
                sql += " WHERE ContractCounty.fk_CountyID = County.CountyID";
                sql += " AND Contract.ContractID = ContractCounty.fk_ContractID ";
                sql += " AND ContractCategory.fk_ContractID = Contract.ContractID AND ContractCategory.fk_CategoryID = CategoryID AND AllowTerritoryOverlap = false AND AllowCountyOverlap = false ";
                sql += " AND Contract.MainDistributorID = '" + mainDistributorID + "'";
                sql += " AND Contract.ContractID NOT IN (" + territoryOverlapContractIdList + ")";

                ds = DataLogic.DBA.DataLogic.Read(sql);

                sql = "DELETE FROM ContractCounty WHERE fk_ContractID IN (" + territoryOverlapContractIdList + ")";

                DataLogic.DBA.DataLogic.Update(sql);

                try
                {

                    foreach (DataRow drContract in dsContracts.Tables[0].Rows)
                    {
                        if (resultContracts.Length > 0)
                            resultContracts += ", ";

                        resultContracts += drContract["ContractNumber"].ToString();

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            sql = "INSERT INTO ContractCounty VALUES ('" + dr["CountyID"] + "','" + drContract["ContractID"] + "')";
                            DataLogic.DBA.DataLogic.Update(sql);

                        }

                        sql = "UPDATE Contract SET ModifyDate = '" + DDA.DataObjects.AppData.CurrentContract.ModifyDate + "' WHERE ContractID = " + drContract["ContractID"];
                        DataLogic.DBA.DataLogic.Update(sql);
                    }

                }
                catch (Exception ex)
                {

                }

                
                try
                {

                    foreach (DataRow drContract in dsContracts.Tables[0].Rows)
                    {
                        if (resultDistributorContracts.Length > 0)
                            resultDistributorContracts += ", ";

                        resultDistributorContracts += drContract["ContractNumber"].ToString();

                        sql = " SELECT DISTINCT fk_DistributorID " +
                           " FROM Contract, ContractDistributor cd " +
                           " WHERE MainDistributorID LIKE " + mainDistributorID + 
                           " AND Contract.ContractID = cd.fk_ContractID " +
                           " AND ContractNumber NOT LIKE 'TO%' ";

                        DataSet dsParentCounty = DataLogic.DBA.DataLogic.Read(sql);

                        sql = "DELETE FROM ContractDistributor WHERE fk_ContractID IN (" + territoryOverlapContractIdList + ")";

                        DataLogic.DBA.DataLogic.Update(sql);

                        foreach (DataRow drParCounty in dsParentCounty.Tables[0].Rows)
                        {
                            if (sql.Length > 0)
                                sql += ",";

                            foreach (DataRow dr in dsContracts.Tables[0].Rows)
                            {
                                sql = "INSERT INTO ContractDistributor (fk_contractid, fk_DistributorID) VALUES (" + dr["ContractID"].ToString() + "," + drParCounty["fk_DistributorID"].ToString() + ")";
                                DataLogic.DBA.DataLogic.Update(sql);
                            }

                        }

                        sql = "UPDATE Contract SET ModifyDate = '" + DDA.DataObjects.AppData.CurrentContract.ModifyDate + "' WHERE ContractID = " + drContract["ContractID"];
                        DataLogic.DBA.DataLogic.Update(sql);
                    }

                }
                catch (Exception ex)
                {

                }

                if (resultContracts.Length > 0)
                {
                    result = "The following Overlap Territory contracts were updated to include these county changes:" + Environment.NewLine + Environment.NewLine;
                    result += resultContracts;
                }
                if (resultDistributorContracts.Length > 0)
                {
                    result = "The following Overlap Territory contracts were updated to include these sale location changes:" + Environment.NewLine + Environment.NewLine;
                    result += resultDistributorContracts;
                }
            }

            return result;

        }

        private static void DeleteOldData()
        {
            string contractNumber;
            string contractDate;
            string modifyDate;
            string mainDistributorID;
            string contractID;

            // Don't need to do this as we should already have the id
            //            DDA.DataObjects.AppData.CurrentContract.ContractID = DataLogic.DBA.DataLogic.GetNextID("Contract", "ContractID");

            contractID = Convert.ToString(DDA.DataObjects.AppData.CurrentContract.ContractID);
            //contractNumber = DDA.DataObjects.AppData.CurrentContract.ContractNumber;
            //contractDate = DDA.DataObjects.AppData.CurrentContract.ContractDate;
            modifyDate = DDA.DataObjects.AppData.CurrentContract.ModifyDate;
            //mainDistributorID = Convert.ToString(DDA.DataObjects.AppData.DistributorID);

            string countyList, catList, delCountyList, delBranchList;
            countyList = "";
            catList = "";
            delBranchList = "";
            delCountyList = "";

            string distID;
            string catID;
            string countyID;

            int i;
            string sql;

            for (i = 0; i < DDA.DataObjects.AppData.CurrentContract.Counties.Count; i++)
            {
                if (countyList != "")
                    countyList = countyList + ",";

                countyList = countyList + DDA.DataObjects.AppData.CurrentContract.Counties[i].ToString();
            }

            for (i = 0; i < DDA.DataObjects.AppData.CurrentContract.Categories.Count; i++)
            {
                if (catList != "")
                    catList = catList + ",";

                catList = catList + DDA.DataObjects.AppData.CurrentContract.Categories[i].ToString();
            }

            DataSet dsContract_County;

            sql = "SELECT fk_ContractID FROM ContractCategory ";
            sql += " INNER JOIN Contract ON Contract.ContractID = ContractCategory.fk_ContractID ";
            sql += " WHERE fk_CategoryID IN (" + catList + ")";
            sql += " AND IsManufacturerRepContract = " + DDA.DataObjects.AppData.IsManufacturerRep;

            //sql = "SELECT fk_ContractID FROM ContractCategory WHERE fk_CategoryID IN (" + catList + ")";

            dsContract_County = DataLogic.DBA.DataLogic.Read(sql);
            string contractIDList;
            contractIDList = "";

            for (i = 0; i < dsContract_County.Tables[0].Rows.Count; i++)
            {
                if (contractIDList != "")
                    contractIDList = contractIDList + ",";

                contractIDList = contractIDList + dsContract_County.Tables[0].Rows[i]["fk_ContractID"];
            }


            for (i = 0; i < DDA.DataObjects.AppData.CurrentContract.DeleteCounties.Count; i++)
            {
                if (delCountyList != "")
                    delCountyList = delCountyList + ",";

                delCountyList = delCountyList + DDA.DataObjects.AppData.CurrentContract.DeleteCounties[i].ToString();
            }

            for (i = 0; i < DDA.DataObjects.AppData.CurrentContract.DeleteBranches.Count; i++)
            {
                if (delBranchList != "")
                    delBranchList = delBranchList + ",";

                delBranchList = delBranchList + DDA.DataObjects.AppData.CurrentContract.DeleteBranches[i].ToString();
            }

            if (delBranchList != "")
            {
                sql = "DELETE FROM ContractDistributor WHERE fk_DistributorID IN (" + delBranchList + ") " +
                        "AND fk_ContractID IN (" + contractIDList + ")";
                DataLogic.DBA.DataLogic.Update(sql);
            }

            if (delCountyList != "")
            {
                sql = "DELETE FROM ContractCounty WHERE fk_CountyID IN (" + delCountyList + ") AND fk_ContractID IN (" + contractIDList + ")";
                //sql = "DELETE FROM ContractCounty WHERE fk_CountyID IN (" + delCountyList + ") " +
                //      "AND fk_ContractID IN (SELECT fk_ContractID FROM ContractCategory WHERE fk_CategoryID IN (" + catList + ")";
                DataLogic.DBA.DataLogic.Update(sql);
            }

            // #######################

        }

        public static string AddContract()
        {

            string contractNumber;
            string contractDate;
            string modifyDate;
            string mainDistributorID;
            string contractID;

            DDA.DataObjects.AppData.CurrentContract.ContractID = DataLogic.DBA.DataLogic.GetNextID("Contract", "ContractID");

            contractID = Convert.ToString(DDA.DataObjects.AppData.CurrentContract.ContractID);
            contractNumber = DDA.DataObjects.AppData.CurrentContract.ContractNumber;
            contractDate = DDA.DataObjects.AppData.CurrentContract.ContractDate;
            modifyDate = DDA.DataObjects.AppData.CurrentContract.ModifyDate;
            mainDistributorID = Convert.ToString(DDA.DataObjects.AppData.DistributorID);


            string distID;
            string catID;
            string countyID;

            int i;
            string sql;

            DeleteOldData();

            for (i = 0; i < DDA.DataObjects.AppData.CurrentContract.Categories.Count; i++)
            {

                sql = "INSERT INTO ContractCategory VALUES ('" + DDA.DataObjects.AppData.CurrentContract.Categories[i].ToString() + "','" + contractID + "')";

                DataLogic.DBA.DataLogic.Update(sql);
            }


            for (i = 0; i < DDA.DataObjects.AppData.CurrentContract.Counties.Count; i++)
            {

                sql = "INSERT INTO ContractCounty VALUES ('" + DDA.DataObjects.AppData.CurrentContract.Counties[i].ToString() + "','" + contractID + "')";

                DataLogic.DBA.DataLogic.Update(sql);

            }

            for (i = 0; i < DDA.DataObjects.AppData.CurrentContract.Branches.Count; i++)
            {
                sql = "INSERT INTO ContractDistributor VALUES ('" + DDA.DataObjects.AppData.CurrentContract.Branches[i].ToString() + "','" + contractID + "')";

                DataLogic.DBA.DataLogic.Update(sql);
            }

            //            string contractNumber;
            //string contractDate;
            //string modifyDate;
            //string mainDistributorID;
            //string contractID;

            int autoValue = 0;

            if (DDA.DataObjects.AppData.CurrentContract.IsAuto)
                autoValue = 1;

            sql = "INSERT INTO Contract VALUES('" + contractID + "','" + contractNumber + "','" + contractDate + "','" + modifyDate + "','" + mainDistributorID + "','" + DDA.DataObjects.AppData.IsManufacturerRep + "','" + DDA.DataObjects.AppData.CurrentContract.ParentContractNumber + "'," + autoValue + ")";

            DataLogic.DBA.DataLogic.Update(sql);

            // update territoryOverlapContract
            string territoryOverlapResult = UpdateTerritoryOverlapContracts(DDA.DataObjects.AppData.DistributorID);

            DataObjects.MapReport.UpdateMapReportStatus();

            DDA.DataObjects.AppData.CurrentContract.ClearBranches();
            DDA.DataObjects.AppData.CurrentContract.ClearCounties();
            DDA.DataObjects.AppData.CurrentContract.ClearStates();
            DDA.DataObjects.AppData.CurrentContract.ClearDeleteBranches();
            DDA.DataObjects.AppData.CurrentContract.ClearDeleteCounties();

            return territoryOverlapResult;

        }

    }
}
