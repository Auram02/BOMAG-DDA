using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;

namespace Dealer_Locator.DA
{
    public class MapReport
    {
        public DataTable GetContracts(int categoryID, string stateList)
        {
            //[ContractDistributorSelectByCategory]
            DataSet ds = new DataSet();

            //SqlConnection sqlConn = Dealer_Locator.DA.DataAccess.GetDatabaseConnection();

            //SqlDataAdapter da = new SqlDataAdapter("", sqlConn);
            //SqlCommand comm = new SqlCommand("ContractDistributorSelectByCategory", sqlConn);
            //comm.CommandType = CommandType.StoredProcedure;

            //comm.Parameters.AddWithValue("@CategoryID", categoryID);
            //comm.Parameters.AddWithValue("@States", stateList);

            //da.SelectCommand = comm;
            //da.Fill(ds);

            return ds.Tables[0];
        }

    }
}