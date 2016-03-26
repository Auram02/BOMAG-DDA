using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DDA.DataObjects
{
    class Reports
    {

        ////public static DataTable DistributorsByState(int p_MainDistributor, int p_catID)
        ////{
        ////    string sql;

        ////    //sql = "Select State.Abbreviation as [STATE], Distributor.DistName as [DISTRIBUTOR], Distributor.ShippingAddress as [ADDRESS], Distributor.fk_ZipID as [ZIP], County.CountyName as [COUNTY], Category.CategoryName as [PRODUCT], Distributor.Phone as [PHONE], Distributor.Fax as [FAX], Rep.RepName as [TERRITORY MANAGER], ContractCounty.fk_ContractID" + 
        ////    //        " FROM ContractCounty, ContractCategory, Category,  Distributor, ContractDistributor, County, State, DistributorRepresentative DR, Representative Rep" + 
        ////    //        " WHERE ContractCounty.fk_contractID = ContractCategory.fk_contractID" + 
        ////    //        " AND County.CountyID = ContractCounty.fk_countyID" + 
        ////    //        " AND ContractCategory.fk_CategoryID = Category.CategoryID" + 
        ////    //        " AND Distributor.pk_DistributorID = ContractDistributor.fk_DistributorID" + 
        ////    //        " AND ContractDistributor.fk_ContractID = ContractCounty.fk_contractID" + 
        ////    //        " AND State.StateID = County.fk_StateID" + 
        ////    //        " AND Distributor.pk_DistributorID = DR.fk_DistributorID" + 
        ////    //        " AND DR.fk_TerritoryRepID = Rep.RepID" +
        ////    //        " AND Distributor.MainDistributor = " + p_MainDistributor + 
        ////    //        " ORDER BY Abbreviation, CategoryName, DistName, CountyName";

        ////    string selectClause, whereClause, fromClause, orderbyClause;

        ////    selectClause = "Select State.Abbreviation as [STATE], Distributor.DistName as [DISTRIBUTOR], Distributor.CityName AS [CITY],Distributor.BillingAddress as [ADDRESS], Distributor.fk_ZipID as [ZIP],Category.CategoryName as [PRODUCT], Distributor.Phone as [PHONE], Distributor.Fax as [FAX] ";

        ////    if (p_MainDistributor == 0)
        ////        selectClause = selectClause + ", Rep.RepName as [TERRITORY MANAGER], DR.fk_ServiceRepID as [PRODUCT SUPPORT]";

        ////    fromClause = " FROM  ContractCategory, Category,  Distributor, ContractDistributor, State, DistributorRepresentative DR,  Representative Rep ";

        ////    whereClause = " WHERE ContractCategory.fk_CategoryID = Category.CategoryID" +
        ////            " AND Distributor.pk_DistributorID = ContractDistributor.fk_DistributorID" +
        ////            " AND ContractDistributor.fk_ContractID = ContractCategory.fk_contractID" +
        ////            " AND Distributor.pk_DistributorID = DR.fk_DistributorID" +
        ////            " AND DR.fk_TerritoryRepID = Rep.RepID" +
        ////            " AND Category.CategoryID = " + p_catID;

        ////    if (p_MainDistributor == 1)
        ////    {
        ////        whereClause = whereClause + " AND Distributor.MainDistributor = " + p_MainDistributor;
        ////    }
        ////    else
        ////    {
        ////        fromClause = fromClause + ", DistributorBranch";
        ////        whereClause = whereClause + " AND Distributor.pk_DistributorID LIKE DistributorBranch.fk_BranchDistId";
        ////    }

        ////    orderbyClause = " ORDER BY Abbreviation, DistName, CategoryName, CityName";


        ////    DataSet ds;

        ////    sql = selectClause + fromClause + whereClause + orderbyClause;

        ////    ds = DataLogic.DBA.DataLogic.Read(sql);

        ////    return ds.Tables[0];
        ////}

        public static DataSet GenerateContractListByCategoryListStateList(int categoryID, List<string> stateList)
        {
            string stateListString = "";

            foreach (string state in stateList)
            {
                if (stateListString.Length > 0)
                    stateListString += ",";

                stateListString += "'" + state + "'";
            }

            DataTable dtSource = DataAccess.Contract_da.GetContractListByCategoryListStateList(categoryID.ToString(), stateListString).Tables[0];

            return GenerateGroupData(dtSource, categoryID, stateListString);
        }

        public static DataSet GetContractDataByContractNumberCategoryID(string contractNumber, int categoryID)
        {
            DataTable dtSource = DDA.DataAccess.Contract_da.GetContractDataByContractNumberCategoryID(contractNumber, categoryID).Tables[0];
            return GenerateGroupData(dtSource, categoryID);
        }

        public static DataTable GetGroupDataTemplate()
        {
            DataTable dt = new DataTable();
            dt.TableName = "GroupCountyData";
            dt.Columns.Add("Name");
            dt.Columns.Add("GroupID");
            dt.Columns.Add("CountyID");
            dt.Columns.Add("Color");
            dt.Columns.Add("Abbreviation");
            dt.Columns.Add("CountyName");
            dt.Columns.Add("StateName");
            dt.Columns.Add("GroupStateName");
            dt.Columns.Add("GroupStateAbbreviation");
            dt.Columns.Add("Address");
            dt.Columns.Add("City");
            dt.Columns.Add("Zip");

            dt.Columns.Add("IsSplit");
            dt.Columns.Add("SplitType");
            dt.Columns.Add("SplitLine");
            dt.Columns.Add("SplitCountyName");
            
            return dt;
        }


        public static DataTable GetSubGroupDataTemplate()
        {
            DataTable dt = new DataTable();
            dt.TableName = "SubGroupData";
            dt.Columns.Add("GroupID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Address");
            dt.Columns.Add("City");
            dt.Columns.Add("Abbreviation");
            dt.Columns.Add("StateName");
            dt.Columns.Add("Zip");
            dt.Columns.Add("ContractState");

            dt.Columns.Add("Lat");
            dt.Columns.Add("Lng");
            dt.Columns.Add("GeoLastUpdated");
            dt.Columns.Add("pk_DistributorID");

            return dt;
        }


        public static DataSet GenerateGroupData(DataTable dtSource, int categoryID, string stateListString = "")
        {
            DataSet ds = new DataSet();
            DataTable dt = GetGroupDataTemplate();
            DataTable dtSubGroups = GetSubGroupDataTemplate();

            List<string> states = new List<string>();

            List<int> distIDs = new List<int>();

            DataSet dsSplit = DDA.DataAccess.SplitCounty_da.GetSplitList();

            foreach (DataRow dr in dtSource.Rows)
            {

                string groupName = dr["DistName"].ToString();
                int groupID = Convert.ToInt32(dr["pk_DistributorID"].ToString());
                int countyID = Convert.ToInt32(dr["CountyID"].ToString());
                string color = "";
                string abbreviation = dr["Abbreviation"].ToString();
                string countyName = dr["CountyName"].ToString();
                string stateName = dr["FullName"].ToString();
                string groupStateAbbreviation = dr["GroupStateAbbreviation"].ToString();
                string groupStateName = dr["GroupStateName"].ToString();
                string city = dr["CityName"].ToString();
                string address = dr["ShippingAddress"].ToString();
                string zip = dr["fk_ZipID"].ToString();

                DataRow newRow = dt.NewRow();
                newRow["Name"] = groupName;
                newRow["GroupID"] = groupID;
                newRow["CountyID"] = countyID;
                newRow["Abbreviation"] = abbreviation;
                newRow["CountyName"] = countyName;
                newRow["StateName"] = stateName;
                newRow["GroupStateName"] = groupStateName;
                newRow["GroupStateAbbreviation"] = groupStateAbbreviation;
                newRow["Color"] = color;
                newRow["Address"] = address;
                newRow["City"] = city;
                newRow["Zip"] = zip;
                newRow["IsSplit"] = false;
                newRow["SplitType"] = "";
                newRow["SplitLine"] = "";
                newRow["SplitCountyName"] = "";

                foreach (DataRow drSplit in dsSplit.Tables[0].Rows)
                {
                    if (drSplit["fk_fakeCountyID"].ToString() == countyID.ToString())
                    {
                        newRow["IsSplit"] = true;
                        string splitType = "";
                        string splitLine = "";

                        if (drSplit["NorthSouth"].ToString() == "North")
                        {
                            splitType = "North";
                            splitLine = drSplit["latitude"].ToString();
                        }

                        if (drSplit["NorthSouth"].ToString() == "South")
                        {
                            splitType = "South";
                            splitLine = drSplit["latitude"].ToString();
                        }

                        if (drSplit["EastWest"].ToString() == "East")
                        {
                            splitType = "East";
                            splitLine = drSplit["longitude"].ToString();
                        }

                        if (drSplit["EastWest"].ToString() == "West")
                        {
                            splitType = "West";
                            splitLine = drSplit["longitude"].ToString();
                        }
                        newRow["SplitType"] = splitType;
                        newRow["SplitLine"] = splitLine;

                        newRow["SplitCountyName"] = drSplit["CountyName"].ToString();

                    }
                }


                dt.Rows.Add(newRow);


                if (distIDs.Contains(groupID) == false)
                {
                    distIDs.Add(groupID);
                    DataSet dsBranches = DDA.DataAccess.Distributor_da.GetDistributorBranchListWithContractStates(groupID, categoryID, stateListString);

                    foreach (DataRow drSubGroup in dsBranches.Tables[0].Rows)
                    {
                        DataRow drNewSubGroup = dtSubGroups.NewRow();

                        drNewSubGroup["GroupID"] = groupID;
                        drNewSubGroup["Name"] = drSubGroup["DistName"].ToString();
                        drNewSubGroup["Address"] = drSubGroup["ShippingAddress"].ToString();
                        drNewSubGroup["City"] = drSubGroup["CityName"];
                        drNewSubGroup["Abbreviation"] = drSubGroup["Abbreviation"];
                        drNewSubGroup["StateName"] = drSubGroup["FullName"];
                        drNewSubGroup["Zip"] = drSubGroup["fk_ZipID"];
                        drNewSubGroup["ContractState"] = drSubGroup["ContractState"];

                        drNewSubGroup["Lat"] = 0;
                        drNewSubGroup["Lng"] = 0;
                        drNewSubGroup["GeoLastUpdated"] = null;
                        drNewSubGroup["pk_DistributorID"] = groupID;


                        dtSubGroups.Rows.Add(drNewSubGroup);
                    }
                }

            }


            ds.Tables.Add(dt);
            ds.Tables.Add(dtSubGroups);
            return ds;

        }

        public static DataTable DistributorsByState(int p_MainDistributor, int p_catID, int p_distID)
        {
            string sql;

            string selectClause, whereClause, fromClause, orderbyClause;

            selectClause = "SELECT DISTINCT (fk_BranchDistID), Abbreviation AS [STATE], CityName AS [CITY], ShippingAddress AS [ADDRESS], Distributor.fk_ZipID AS [ZIP], Category.CategoryName AS [PRODUCT], Distributor.Phone as [PHONE], Distributor.Fax AS [FAX] ";

            if (p_MainDistributor == 0)
                selectClause = selectClause + ", Rep.RepName as [TERRITORY MANAGER], DR.fk_ServiceRepID";

            fromClause = " FROM Distributor, DistributorBranch, Contract, ContractCategory, Category, State, ContractDistributor, DistributorRepresentative DR, Representative Rep ";


            whereClause = " WHERE Distributor.pk_DistributorID LIKE DistributorBranch.fk_BranchDistId " +
                            " AND Contract.ContractID = ContractCategory.fk_ContractID" +
                            " AND ContractCategory.fk_CategoryID = Category.CategoryID" + 
                            " AND State.StateID = Distributor.fk_StateID" + 
                            " AND ContractDistributor.fk_ContractID = ContractCategory.fk_ContractID" + 
                            " AND Distributor.pk_DistributorID = ContractDistributor.fk_DistributorID" + 
                            " AND Distributor.pk_DistributorID = DR.fk_DistributorID" + 
                            " AND DR.fk_TerritoryRepID = Rep.RepID" +
                            " AND Category.CategoryID = " + p_catID +
                            " AND Contract.MainDistributorID LIKE " + p_distID;


            if (p_MainDistributor == 1)
            {
                whereClause = whereClause + " AND Distributor.MainDistributor = " + p_MainDistributor;
            }

            orderbyClause = " ORDER BY Abbreviation, CategoryName, CityName";


            DataSet ds;

            sql = selectClause + fromClause + whereClause + orderbyClause;

            ds = DataLogic.DBA.DataLogic.Read(sql);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                ds.Tables[0].Rows[i][0] = "";

            //ds.Tables[0].Columns.RemoveAt(0);

            return ds.Tables[0];
        }

    }
}
