using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DDA.DataAccess
{
    class SplitCounty_da
    {

        public static void RemoveSplit(int fakeCountyID)
        {
            string sql = "DELETE FROM SplitCounty WHERE fk_fakeCountyID = " + fakeCountyID.ToString();

            DataLogic.DBA.DataLogic.Update(sql);
        }

        public static void AddSplit(int splitID, int fakeCountyID, int countyID, double longitude, double latitude, string NorthSouth, string EastWest)
        {

            string sql = "INSERT INTO SplitCounty (pk_splitID, fk_countyID, fk_fakeCountyID, latitude, longitude, NorthSouth, EastWest) VALUES " +
                        "(" + splitID + ", " + countyID + ", " + fakeCountyID + ", " + latitude + ", " + longitude + ", '" + NorthSouth + "', '" + EastWest + "')";

            DataLogic.DBA.DataLogic.Update(sql);

        }

        public static DataSet GetSplit(int fakeCountyID)
        {

            string sql = "SELECT * FROM SplitCounty WHERE fk_fakeCountyID = " + fakeCountyID;

            return DataLogic.DBA.DataLogic.Read(sql); ;

        }

        public static DataSet GetSplitList()
        {

            string sql = "SELECT sc.pk_splitID, sc.fk_countyID, sc.fk_fakeCountyID, sc.latitude, sc.longitude, sc.NorthSouth, sc.EastWest, cty.CountyName " +
                            " FROM SplitCounty sc, County cty " +
                            " WHERE cty.CountyID = sc.fk_countyID";


            return DataLogic.DBA.DataLogic.Read(sql); ;

        }


    }
}
